using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rookie.Ecom.MetaShop.Identity.Data;
using System;
using System.Linq;
using System.Security.Claims;

namespace Rookie.Ecom.MetaShop.Identity
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<AspNetIdentityDbContext>(
                options => options.UseSqlServer(connectionString)
            );

            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AspNetIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddOperationalDbContext(
                options =>
                {
                    options.ConfigureDbContext = db =>
                        db.UseSqlServer(
                            connectionString,
                            sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName)
                        );
                }
            );
            services.AddConfigurationDbContext(
                options =>
                {
                    options.ConfigureDbContext = db =>
                        db.UseSqlServer(
                            connectionString,
                            sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName)
                        );
                }
            );

            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

            var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            context.Database.Migrate();

            EnsureSeedData(context);

            var ctx = scope.ServiceProvider.GetService<AspNetIdentityDbContext>();
            ctx.Database.Migrate();
            EnsureRoles(scope);
            EnsureUsers(scope);
        }

        private static void EnsureUsers(IServiceScope scope)
        {
            UserManager<IdentityUser> userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser admin = userMgr.FindByNameAsync("admin").Result;
            IdentityUser john = userMgr.FindByNameAsync("john").Result;
            if (admin == null)
            {
                admin = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@metashop.com",
                    EmailConfirmed = true
                };
                IdentityResult result = userMgr.CreateAsync(admin, "Str0ng!Passw0rd@").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddToRoleAsync(admin, "Admin").Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result =
                    userMgr.AddClaimsAsync(
                        admin,
                        new Claim[]
                        {
                            new Claim(JwtClaimTypes.Name, "admin"),
                            new Claim(JwtClaimTypes.GivenName, "admin"),
                            new Claim(JwtClaimTypes.FamilyName, "Freeman"),
                            new Claim(JwtClaimTypes.WebSite, "https://metashop.com"),
                            new Claim("location", "somewhere"),
                            new Claim(JwtClaimTypes.Role, "Admin")
                        }
                    ).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }

            if (john == null)
            {
                john = new IdentityUser
                {
                    UserName = "johnd",
                    Email = "john@metashop.com",
                    EmailConfirmed = true
                };
                IdentityResult result = userMgr.CreateAsync(john, "Str0ng!Passw0rd@").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddToRoleAsync(john, "Customer").Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result =
                    userMgr.AddClaimsAsync(
                        john,
                        new Claim[]
                        {
                            new Claim(JwtClaimTypes.Name, "johnny D"),
                            new Claim(JwtClaimTypes.GivenName, "john"),
                            new Claim(JwtClaimTypes.FamilyName, "D"),
                            new Claim(JwtClaimTypes.WebSite, "https://john.metashop.com"),
                            new Claim("address", "somewhere"),
                            new Claim(JwtClaimTypes.Role, "Customer")
                        }
                    ).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }

        private static void EnsureRoles(IServiceScope scope)
        {
            RoleManager<IdentityRole> roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            IdentityRole admin = roleMgr.FindByNameAsync("Admin").Result;
            IdentityRole customer = roleMgr.FindByNameAsync("Customer").Result;
            if (admin == null)
            {
                admin = new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "admin"
                };
                var result = roleMgr.CreateAsync(admin).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
            if (customer == null)
            {
                customer = new IdentityRole
                {
                    Name = "Customer",
                    NormalizedName = "customer"
                };
                var result = roleMgr.CreateAsync(customer).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }

        private static void EnsureSeedData(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients.ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources.ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in Config.ApiScopes.ToList())
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }

            /*if (!context.ApiResources.Any())
            {
                foreach (var resource in Config.ApiResources.ToList())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }*/
        }
    }
}
