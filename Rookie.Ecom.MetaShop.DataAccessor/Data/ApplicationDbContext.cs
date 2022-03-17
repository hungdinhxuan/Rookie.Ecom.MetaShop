using Microsoft.EntityFrameworkCore;
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

        public DbSet<Product> Products { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ProductPicture> ProductPictures { get; set; }

        public DbSet<ProductRating> ProductRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasMany(c => c.Products).WithOne(p => p.Category).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>().HasMany(p => p.ProductPictures).WithOne(p => p.Product).OnDelete(DeleteBehavior.Cascade);

        }

        private void EnsureSeedCategory(ref ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Name = "Cell Phones & Accessories",
                    Desc = "A mobile phone, cellular phone, cell phone, cellphone, handphone, or hand phone, sometimes shortened to simply mobile, cell, or just phone, is a portable ...Cell Phone",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/rookie-metashop.appspot.com/o/Categories%2F31234a27876fb89cd522d7e3db1ba5ca_tn.png?alt=media&token=ee7e6b03-3343-485c-b347-e68782f591e8"
                }
                )
                ;
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Name = "Mom, Kids & Babies",
                    Desc = "Abbott, Huggies, Bobby, pampers, ...",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/rookie-metashop.appspot.com/o/Categories%2F099edde1ab31df35bc255912bab54a5e_tn.png?alt=media&token=540ba861-52be-43de-90cf-e927d359b4b4"
                }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Name = "Bags",
                    Desc = "A bag is a kind of soft container. It can hold or carry things. It may be made from cloth, leather, plastic, or paper.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/rookie-metashop.appspot.com/o/Categories%2F18fd9d878ad946db2f1bf4e33760c86f_tn.png?alt=media&token=1d24e442-8750-4302-b181-7f9e2be88841"
                }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Name = "Home & Living",
                    Desc = "Reference site about Lorem Ipsum, giving information on its origins, as well as a random Lipsum generator",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/rookie-metashop.appspot.com/o/Categories%2F24b194a695ea59d384768b7b471d563f_tn.png?alt=media&token=dc6924ba-6285-47d0-ad63-cac0f4253954"
                }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Name = "Book & Stationery",
                    Desc = "Stationery refers to commercially manufactured writing materials, including cut paper, envelopes, writing implements, continuous form pape",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/rookie-metashop.appspot.com/o/Categories%2F36013311815c55d303b0e6c62d6a8139_tn.png?alt=media&token=81348f4e-e096-460c-b9cd-a3d6ff45a3e1"
                }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Name = "Automotive",
                    Desc = "Automotive service technicians maintain, repair, and inspect cars, light trucks, and other vehicles. They may also be referred to as a service tech, ...",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/rookie-metashop.appspot.com/o/Categories%2F3fb459e3449905545701b418e8220334_tn.png?alt=media&token=29663648-028e-4d9e-8ea9-2994f78e4ba7"
                }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Name = "Woman Clothes",
                    Desc = "Here are some free Product Description Examples for Women's clothing and shoes.",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/rookie-metashop.appspot.com/o/Categories%2F4540f87aa3cbe99db739f9e8dd2cdaf0_tn.png?alt=media&token=7cb4a49d-ad59-460b-a143-4f10e51568be"
                }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Name = "Sport & Outdoors",
                    Desc = "A mobile phone, cellular phone, cell phone, cellphone, handphone, or hand phone, sometimes shortened to simply mobile, cell, or just phone, is a portable ...Cell Phone",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/rookie-metashop.appspot.com/o/Categories%2F6cb7e633f8b63757463b676bd19a50e4_tn.png?alt=media&token=c883dc52-7f91-41c3-89eb-0326b310085a"
                }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Name = "Cameras",
                    Desc = "A mobile phone, cellular phone, cell phone, cellphone, handphone, or hand phone, sometimes shortened to simply mobile, cell, or just phone, is a portable ...Cell Phone",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/rookie-metashop.appspot.com/o/Categories%2Fec14dd4fc238e676e43be2a911414d4d_tn.png?alt=media&token=ca07e886-f32e-4bc0-89a8-02d96c182b97"
                }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Name = "Pets",
                    Desc = "A mobile phone, cellular phone, cell phone, cellphone, handphone, or hand phone, sometimes shortened to simply mobile, cell, or just phone, is a portable ...Cell Phone",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/rookie-metashop.appspot.com/o/Categories%2Fcdf21b1bf4bfff257efe29054ecea1ec_tn.png?alt=media&token=806e2dff-3cbf-423a-a3db-f7dadea4d3ce"
                }
                );
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