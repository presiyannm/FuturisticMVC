using Futuristic.Models;
using Futuristic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Futuristic.Controllers
{
    [Authorize(Roles = "Uploader, Administrator, Editor")]
    public class DeleteController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> DeleteArticle(string userName, NewsArticle currentArticle)
        {
            var viewModel = new DeleteArticleViewModel
            {
                currentArticle = currentArticle,
                currentUser =  await _userManager.FindByNameAsync(userName)
            };
            return View(viewModel);
        }
    }
}
