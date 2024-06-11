using Futuristic.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Futuristic.Seeding
{
    public class RolesSeeder : ISeeder
    {
        public async Task<bool> SeedDatabase(
           ApplicationDbContext applicationDbContext,
           IServiceProvider serviceProvider
       )
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            bool isRolesSeedingTriggered = false;

            string[] roleNames = {
                GlobalConstants.IdentityConstants.AdministratorRoleName,
                GlobalConstants.IdentityConstants.EditorRoleName,
                GlobalConstants.IdentityConstants.UploaderRoleName,
                GlobalConstants.IdentityConstants.NormalUserRole
            };

            int createdRolesCounter = 0;

            foreach (var roleName in roleNames)
            {
                var role = await roleManager.FindByNameAsync(roleName);

                if (role == null)
                {
                    if (!await SeedRoleAsync(applicationDbContext, roleManager, roleName))
                    {
                        break;
                    }
                    else
                    {
                        createdRolesCounter++;
                    }
                }
            }

            if (createdRolesCounter > 0)
            {
                isRolesSeedingTriggered = true;
            }

            return isRolesSeedingTriggered;
        }

        public static async Task<bool> SeedRoleAsync(
            ApplicationDbContext applicationDbContext,
            RoleManager<IdentityRole> roleManager, string roleName
        )
        {
            bool isRoleCreated = true;

            var newApplicationRole = new IdentityRole(roleName);

            applicationDbContext.Entry(newApplicationRole).State = EntityState.Added;

            var roleCreationResult = await roleManager.CreateAsync(newApplicationRole);

            await applicationDbContext.SaveChangesAsync();

            if (!roleCreationResult.Succeeded)
            {
                isRoleCreated = false;

                throw new Exception(string.Join(
                    Environment.NewLine,
                    roleCreationResult.Errors.Select(e => e.Description)
                ));
            }

            return isRoleCreated;
        }
    }
}
