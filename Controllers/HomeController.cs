using Futuristic.Data;
using Futuristic.Models;
using Futuristic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace Futuristic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private ApplicationDbContext myDbContext;

        private readonly UserManager<ApplicationUser> _userManager;
        
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(
            ILogger<HomeController> logger, 
            ApplicationDbContext myDbContext, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> RoleManager
        )
        {
            _logger = logger;
            this.myDbContext = myDbContext;
            _userManager = userManager;
            _roleManager = RoleManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null) 
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                if (!userRoles.Any())
                {
                    await _userManager.AddToRoleAsync(user, "Normal User");
                }
            }

            await myDbContext.SaveChangesAsync();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AdministratorPage()
        {
            List<ApplicationUser> users = await myDbContext.Users.ToListAsync();
            List<IdentityRole> roles = await myDbContext.Roles.ToListAsync();

            var viewModel = new AdministratorPageViewModel
            {
                Users = users,
                Roles = roles
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> AdministratorEditRoles(string userId,string roles)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                RedirectToAction("Error");
            }

            if (await _roleManager.RoleExistsAsync(roles))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);

                if (!removeResult.Succeeded)
                {
                    return RedirectToAction("Error");
                }

                var addResult = await _userManager.AddToRoleAsync(user, roles);

                if (!addResult.Succeeded)
                {
                    return RedirectToAction("Error");
                }

            }

            myDbContext.SaveChanges();

            return RedirectToAction("AdministratorPage");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}