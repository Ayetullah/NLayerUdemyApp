using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using System.Reflection;

namespace Nlayer.Repository.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//Configurations dosyalarını tara
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            UpdateChangeTracker();
            return base.SaveChanges();  
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateChangeTracker();
            return base.SaveChangesAsync(cancellationToken);
        }

        public void UpdateChangeTracker()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entitiyReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            Entry(entitiyReference).Property(x => x.UpdatedDate).IsModified = false;
                            entitiyReference.CreatedDate = DateTime.Now;
                            break;
                        case EntityState.Modified:
                            Entry(entitiyReference).Property(x => x.CreatedDate).IsModified = false;
                            entitiyReference.UpdatedDate = DateTime.Now;
                            break;
                    }
                }
            }
        }
    }
}
