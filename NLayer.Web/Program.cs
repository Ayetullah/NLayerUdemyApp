using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Nlayer.Repository.DataContext;
using Nlayer.Repository.Repositories;
using Nlayer.Repository.UnitOfWorks;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using NLayer.Service.Validations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(x =>
    x.RegisterValidatorsFromAssemblyContaining<ProductCreateDtoValidator>());

#region AddScoped
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // Generic oldu�u i�in bu �ekilde eklendi
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>(); // cahe yap�s� i�in kald�r�ld�

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
#endregion

builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), opt =>
    {
        opt.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);//AppDbContext in oldu�u katman� bulmas� ve migration � yapmas�n� sa�lad�k
    });
});

#region Repo ve service scope bir yerde toplamak i�in
//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new RepoServiceModule()));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

app.Run();
