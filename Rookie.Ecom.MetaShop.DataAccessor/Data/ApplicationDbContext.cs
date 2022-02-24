using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Rookie.Ecom.MetaShop.DataAccessor.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rookie.Ecom.MetaShop.DataAccessor.Data

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>(builder =>
            {
                builder.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()");
                builder.Property(x => x.UpdatedDate).HasDefaultValueSql("getdate()");
            });
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            var now = DateTime.Now;

            foreach (var entry in entries)
            {
                // for entities that inherit from BaseEntity,
                // set UpdatedOn / CreatedOn appropriately
                if (entry.Entity is BaseEntity trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            // set the updated date to "now"
                            trackable.CreatedDate = now;

                            // mark property as "don't touch"
                            // we don't want to update on a Modify operation
                            entry.Property("CreatedOn").IsModified = false;
                            break;

                        case EntityState.Added:
                            // set both CreatedDate and created date to "now"
                            trackable.CreatedDate = now;
                            trackable.UpdatedDate = now;
                            break;
                    }
                }
            }
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}