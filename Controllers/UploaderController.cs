using Futuristic.Data;
using Futuristic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Futuristic.Controllers
{
    [Authorize(Roles ="Uploader, Administrator")]
    public class UploaderController : Controller
    {
        private ApplicationDbContext myDbContext;

        private UserManager<ApplicationUser> _userManager;

        public UploaderController(ApplicationDbContext myDbContext, UserManager<ApplicationUser> userManager)
        {
            this.myDbContext = myDbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post(string articleTitle, string articleContent, string uploaderId)
        {
            var user = _userManager.FindByIdAsync(uploaderId);

            if (string.IsNullOrEmpty(articleTitle) || string.IsNullOrEmpty(articleContent) || user == null)
            {
                return RedirectToAction("Error");
            }

            var addedArticle = myDbContext.articles.Add(new NewsArticle(articleTitle, articleContent, await user));

            await myDbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
