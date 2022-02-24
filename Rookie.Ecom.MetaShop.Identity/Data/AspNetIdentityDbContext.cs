using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Rookie.Ecom.MetaShop.Identity.Data
{
    public class AspNetIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public AspNetIdentityDbContext(DbContextOptions<AspNetIdentityDbContext> options)
          : base(options)
        {
        }
    }
}
