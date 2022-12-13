using Autofac;
using Nlayer.Repository.DataContext;
using Nlayer.Repository.Repositories;
using Nlayer.Repository.UnitOfWorks;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Mapping;
using System.Reflection;
using Module = Autofac.Module;

namespace NLayer.API.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // Generic olduğu için bu şekilde eklendi
            builder.RegisterGeneric(typeof(IService<,>)).As(typeof(IService<,>)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var respoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));//oo katmandaki herhangi bir isim

            builder.RegisterAssemblyTypes(apiAssembly, respoAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Respository")).AsImplementedInterfaces()
                .InstancePerLifetimeScope();//scope

            builder.RegisterAssemblyTypes(apiAssembly, respoAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces()
                .InstancePerLifetimeScope();//scope
        }
    }
}
