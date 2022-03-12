# Rookie.Ecom.MetaShop
## Rookie.Ecom.MetaShop.Identity
### Migration
#### Add migration for ConfigurationDbContext, PersistedGrantDb, AspNetIdentityDbContext
add-migration -C ConfigurationDbContext initConfigurationDb
add-migration -C PersistedGrantDbContext initPersistedGrantDb
add-migration -C AspNetIdentityDbContext initAspNetIdentityDb
#### Update database from context
- Uncomment InitializeDatabase(app) at Startup.cs
- Update-Database -Context AspNetIdentityDbContext
#### Seed data (create roles, users, user_role)
dotnet run /seed
### Add client => config in Config.cs

