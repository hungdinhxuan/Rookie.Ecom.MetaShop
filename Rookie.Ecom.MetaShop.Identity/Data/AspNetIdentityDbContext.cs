using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Rookie.Ecom.MetaShop.Identity.Data
{
    public class AspNetIdentityDbContext : IdentityDbContext<MetaIdentityUser>
    {
        public AspNetIdentityDbContext(DbContextOptions<AspNetIdentityDbContext> options)
          : base(options)
        {
        }
    }
}
