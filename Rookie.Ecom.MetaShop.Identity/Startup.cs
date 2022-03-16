using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rookie.Ecom.MetaShop.Identity.Data;
using System.Linq;
using System.Reflection;

namespace Rookie.Ecom.MetaShop.Identity
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();


                if (!context.Clients.Any())
                {
                    foreach (var client in Config.Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.IdentityResources)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var resource in Config.ApiScopes)
                    {
                        context.ApiScopes.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }

        public string AllOrigins = "AllowAllOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var defaultConnString = Configuration.GetConnectionString("DefaultConnection");

            services.AddControllersWithViews();

            services.AddCors(options =>
            {
                options.AddPolicy(name: AllOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });



            services.AddAuthentication()
           .AddGoogle("Google", options =>
           {
               options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
               options.ClientId = Configuration["Authentication:Google:ClientId"];
               options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
           })
           .AddFacebook("Facebook", options =>
           {
               options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
               options.ClientId = Configuration["Authentication:Facebook:ClientId"];
               options.ClientSecret = Configuration["Authentication:Facebook:ClientSecret"];
           }).AddMicrosoftAccount("Microsoft", options =>
           {
               options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
               options.ClientId = Configuration["Authentication:Microsoft:ClientId"];
               options.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
           });

            services.AddDbContext<AspNetIdentityDbContext>(options =>
            {

                options.UseSqlServer(defaultConnString,
                    b => b.MigrationsAssembly(migrationsAssembly));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
            );

            services.AddIdentity<MetaIdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AspNetIdentityDbContext>()
            .AddDefaultTokenProviders();
            services.AddIdentityLayer(Configuration);


            // configure identity server with sqlserver stores, keys, clients and scopes
            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddAspNetIdentity<MetaIdentityUser>()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b =>
                b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b =>
                b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(migrationsAssembly));
            });




            builder.AddDeveloperSigningCredential();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                InitializeDatabase(app);

            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(AllOrigins);
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
