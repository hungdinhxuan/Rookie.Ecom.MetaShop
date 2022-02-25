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

    }
}