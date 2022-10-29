using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Server.Data
{
    public class SeedData
    {
        public static void EnsureSeedData(string defaultConnection)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<ASPNetIdentityDbContext>(op => op.UseSqlServer(defaultConnection));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ASPNetIdentityDbContext>().AddDefaultTokenProviders();

            services.AddOperationalDbContext(options =>
            {
                options.ConfigureDbContext = db => db.UseSqlServer(defaultConnection,
                sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.GetName().FullName));
            });

            services.AddConfigurationDbContext(options =>
            {
                options.ConfigureDbContext = db => db.UseSqlServer(defaultConnection,
                sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.GetName().FullName));
            });

            var serviceProvider = services.BuildServiceProvider();
            using var scop = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            scop.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();
            var context = scop.ServiceProvider.GetService<ConfigurationDbContext>();
            context.Database.Migrate();
            EnsureSeedData(context);
            var ctx = scop.ServiceProvider.GetService<ASPNetIdentityDbContext>();
            ctx.Database.Migrate();
            EnsureUsers(scop);

        }
        public static void EnsureUsers(IServiceScope scope)
        {
            var userMng = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
            var user = userMng.FindByNameAsync("TempUser").Result;
            if (user == null)
            {
                var newUser = new IdentityUser

                {
                    UserName = "Ahmed99",
                    Email = "ahmed.baker5101995@gmail.com",
                    EmailConfirmed = true
                };
                var result = userMng.CreateAsync(newUser, "NewP@ssw0rd").Result;
                if (!result.Succeeded)
                {
                    throw new Exception();
                }
                var claimResult = userMng.AddClaimsAsync(
                      newUser, new Claim[] {
                      new Claim(JwtClaimTypes.Name,"Ahmed"),
                      new Claim(JwtClaimTypes.IdentityProvider,"ahmed@Luftborn.com"),
                      new Claim("SpecialClaim","My Special Clam")
                      }
                      ).Result;

                if (!claimResult.Succeeded)
                {
                    throw new Exception();
                }

            }

        }

        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<ConfigurationDbContext>())
                {
                    EnsureSeedData(context);
                }
            }
        }

        private static void EnsureSeedData(ConfigurationDbContext context)
        {
            Console.WriteLine("Seeding database...");

            if (!context.Clients.Any())
            {
                Console.WriteLine("Clients being populated");
                foreach (var client in Config.Clients)
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Clients already populated");
            }

            if (!context.IdentityResources.Any())
            {
                Console.WriteLine("IdentityResources being populated");
                foreach (var resource in Config.IdentityResources)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("IdentityResources already populated");
            }

            if (!context.ApiResources.Any())
            {
                Console.WriteLine("ApiResources being populated");
                foreach (var resource in Config.ApiResources)
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("ApiResources already populated");
            }

            if (!context.ApiScopes.Any())
            {
                Console.WriteLine("Scopes being populated");
                foreach (var resource in Config.ApiScopes)
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Scopes already populated");
            }

            Console.WriteLine("Done seeding database.");
            Console.WriteLine();
        }
    }

}
