using Futuristic.Models;
using Microsoft.AspNetCore.Identity;

namespace Futuristic.ViewModels
{
    public class AdministratorPageViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }
}
