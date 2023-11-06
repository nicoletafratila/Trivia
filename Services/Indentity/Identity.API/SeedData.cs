using System.Security.Claims;
using Identity.API.Data;
using Identity.API.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Identity.API
{
    public class SeedData
    {
        public static async Task EnsureSeedData(WebApplication app)
        {
            using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                await context!.Database.MigrateAsync();
                await SeedRoles(scope);
                await SeedUsers(scope);
            }
        }

        private static async Task SeedUsers(IServiceScope scope)
        {
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var admin = await userMgr.FindByNameAsync("admin");
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@trivia.com",
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "Admin",
                    IsActive = true
                };
                var result = await userMgr.CreateAsync(admin, "Pass123$");
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = await userMgr.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, "Admin Trivia"),
                    new Claim(JwtClaimTypes.GivenName, "Admin"),
                    new Claim(JwtClaimTypes.FamilyName, "Admin"),
                    new Claim(JwtClaimTypes.WebSite, "http://admin.com")
                });
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                if (!await userMgr.IsInRoleAsync(admin, "admin"))
                {
                    var roleResult = await userMgr.AddToRoleAsync(admin, "admin");
                    if (!roleResult.Succeeded)
                    {
                        throw new Exception(roleResult.Errors.First().Description);
                    }
                    await userMgr.AddClaimAsync(admin, new Claim(ClaimTypes.Role, "admin"));
                }

                Log.Debug("admin created");
            }
            else
            {
                Log.Debug("admin already exists");
            }

            var member = await userMgr.FindByNameAsync("member");
            if (member == null)
            {
                member = new ApplicationUser
                {
                    UserName = "member",
                    Email = "member@trivia.com",
                    EmailConfirmed = true,
                    FirstName = "Member first name",
                    LastName = "Member last name",
                    IsActive = true
                };
                var result = await userMgr.CreateAsync(member, "Pass123$");
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = await userMgr.AddClaimsAsync(member, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, "Member Trivia"),
                    new Claim(JwtClaimTypes.GivenName, "Member"),
                    new Claim(JwtClaimTypes.FamilyName, "Member"),
                    new Claim(JwtClaimTypes.WebSite, "http://Member.com")
                });
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                if (!await userMgr.IsInRoleAsync(member, "member"))
                {
                    var roleResult = await userMgr.AddToRoleAsync(member, "member");
                    if (!roleResult.Succeeded)
                    {
                        throw new Exception(roleResult.Errors.First().Description);
                    }
                    await userMgr.AddClaimAsync(member, new Claim(ClaimTypes.Role, "member"));
                }

                Log.Debug("member created");
            }
            else
            {
                Log.Debug("member already exists");
            }
        }

        private static async Task SeedRoles(IServiceScope scope)
        {
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var admin = await roleMgr.FindByIdAsync("admin");
            if (admin == null)
            {
                admin = new IdentityRole
                {
                    Id = "admin",
                    Name = "admin"
                };
                var result = await roleMgr.CreateAsync(admin);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }

            var member = await roleMgr.FindByIdAsync("member");
            if (member == null)
            {
                member = new IdentityRole
                {
                    Id = "member",
                    Name = "member"
                };
                var result = await roleMgr.CreateAsync(member);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }
    }
}