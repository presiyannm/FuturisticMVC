using Futuristic.Data;
using Futuristic.Models;
using Futuristic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Futuristic.Controllers
{
    [Authorize(Roles = "Uploader, Administrator, Editor")]
    public class DeleteController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _myDbContext;

        public DeleteController(UserManager<ApplicationUser> userManager, ApplicationDbContext myDbContext)
        {
            _userManager = userManager;
            _myDbContext = myDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> DeleteArticle(string userName, int currentArticleId)
        {
            var currentUser = await _userManager.FindByNameAsync(userName);
            var currentArticle = await _myDbContext.articles.FindAsync(currentArticleId);

            if (currentArticle == null || currentUser == null)
            {
                return NotFound();
            }

            var viewModel = new DeleteArticleViewModel
            {
                currentArticle = currentArticle,
                currentUser = currentUser
            };

            return View(viewModel);
        }
    }
}
