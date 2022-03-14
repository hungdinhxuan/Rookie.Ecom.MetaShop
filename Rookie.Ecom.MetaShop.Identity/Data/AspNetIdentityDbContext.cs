using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rookie.Ecom.MetaShop.DataAccessor.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.Identity.Data
{
    public class AspNetIdentityDbContext : IdentityDbContext<MetaIdentityUser>
    {
        public AspNetIdentityDbContext(DbContextOptions<AspNetIdentityDbContext> options)
          : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ProductPicture> ProductPictures { get; set; }

        public DbSet<ProductRating> ProductRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasMany(c => c.Products).WithOne(p => p.Category).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>().HasMany(p => p.ProductPictures).WithOne(p => p.Product).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<MetaIdentityUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Categories)
                    .WithOne()
                    .HasForeignKey(uc => uc.CreatedBy)
                    .HasForeignKey(uc => uc.UpdatedBy);

                // Each User can have many UserLogins
                b.HasMany(e => e.Products)
                    .WithOne()
                    .HasForeignKey(uc => uc.CreatedBy)
                    .HasForeignKey(uc => uc.UpdatedBy);

                // Each User can have many UserTokens
                b.HasMany(e => e.OrderItems)
                    .WithOne()
                    .HasForeignKey(uc => uc.CreatedBy)
                    .HasForeignKey(uc => uc.UpdatedBy);

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.Orders)
                    .WithOne()
                    .HasForeignKey(uc => uc.CreatedBy)
                    .HasForeignKey(uc => uc.UpdatedBy);

                b.HasMany(e => e.ProductPictures)
                    .WithOne()
                    .HasForeignKey(uc => uc.CreatedBy)
                    .HasForeignKey(uc => uc.UpdatedBy);

                b.HasMany(e => e.ProductRatings)
                .WithOne()
                .HasForeignKey(uc => uc.CreatedBy)
                .HasForeignKey(uc => uc.UpdatedBy);
            });
        }

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken token = default)
        {
            foreach (var entity in ChangeTracker
                .Entries()
                .Where(x => x.Entity is BaseEntity && x.State == EntityState.Modified)
                .Select(x => x.Entity)
                .Cast<BaseEntity>())
            {
                entity.UpdatedDate = DateTime.Now;
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, token);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker
                .Entries()
                .Where(x => x.Entity is BaseEntity && x.State == EntityState.Modified)
                .Select(x => x.Entity)
                .Cast<BaseEntity>())
            {
                entity.UpdatedDate = DateTime.Now;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
