using Futuristic.Data;
using Futuristic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Futuristic.Seeding
{
    public class UsersSeeder : ISeeder
    {
        public async Task<bool> SeedDatabase(
            ApplicationDbContext applicationDbContext,
            IServiceProvider serviceProvider
        )
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var databaseLoadedUsers = applicationDbContext.Users.IgnoreQueryFilters().ToList();

            bool isUsersSeedingTriggered = false;

            string[] usernames =
            {
                GlobalConstants.IdentityConstants.AdministratorUsername,
                GlobalConstants.IdentityConstants.EditorUsername,
                GlobalConstants.IdentityConstants.UploaderUsername,
                GlobalConstants.IdentityConstants.ExampleUserUsername
            };

            string[] emails = {
                GlobalConstants.IdentityConstants.AdministratorEmail,
                GlobalConstants.IdentityConstants.EditorEmail,
                GlobalConstants.IdentityConstants.UploaderEmail,
                GlobalConstants.IdentityConstants.ExampleUserEmail
            };

            string[] passwords =
            {
                GlobalConstants.IdentityConstants.AdministratorPassword,
                GlobalConstants.IdentityConstants.EditorPassword,
                GlobalConstants.IdentityConstants.UploaderPassword,
                GlobalConstants.IdentityConstants.ExampleUserPassword
            };

            string[] roles =
            {
                GlobalConstants.IdentityConstants.AdministratorRoleName,
                GlobalConstants.IdentityConstants.EditorRoleName,
                GlobalConstants.IdentityConstants.UploaderRoleName,
                GlobalConstants.IdentityConstants.NormalUserRole
            };

            int usersToSeedCount = usernames.Length;

            int createdUsersCounter = 0;

            for (int i = 0; i < usersToSeedCount; i++)
            {
                var user = databaseLoadedUsers
                    .FirstOrDefault(user => user.Email == emails[i] || user.UserName == usernames[i]);

                if (user == null)
                {
                    if (!await SeedUserAsync(userManager, usernames[i], emails[i], passwords[i], roles[i]))
                    {
                        break;
                    }
                    else
                    {
                        createdUsersCounter++;
                    }
                }
            }

            if (createdUsersCounter > 0)
            {
                isUsersSeedingTriggered = true;
            }

            return isUsersSeedingTriggered;
        }

        public static async Task<bool> SeedUserAsync(
            UserManager<ApplicationUser> userManager,
            string username, string email, string password, string role
        )
        {
            bool isUserCreated = true;

            ApplicationUser userToRegister = new ApplicationUser()
            {
                Email = email,
                UserName = username,
                EmailConfirmed = true

            };

            var result = await userManager.CreateAsync(userToRegister, password);

            var addUserToRoleResult = await userManager.AddToRoleAsync(userToRegister, role);

            if (!result.Succeeded || !addUserToRoleResult.Succeeded)
            {
                isUserCreated = false;

                throw new Exception(string.Join(
                    Environment.NewLine,
                    result.Errors.Select(e => e.Description)
                ));
            }

            return isUserCreated;
        }
    }
}
