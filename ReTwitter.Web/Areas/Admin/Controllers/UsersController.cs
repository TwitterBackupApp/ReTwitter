using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Web.Areas.Admin.Models.Users;

namespace ReTwitter.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrators")]
    public class UsersController : Controller
    {
        private readonly IAdminUserService userService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ICascadeDeleteService cascadeDeleteService;
        
        public UsersController(
            IAdminUserService userService,
            RoleManager<IdentityRole> roleManager,
            ICascadeDeleteService cascadeDeleteService)
        {
            this.userService = userService;
            this.roleManager = roleManager;
            this.cascadeDeleteService = cascadeDeleteService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await this.userService.AllAsync();
            var roles = await this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync();

            return View(new UserListingsViewModel
            {
                Users = users,
                Roles = roles
            });
        }

        public IActionResult Delete(string userId, string userName)
        {
            this.cascadeDeleteService.DeleteUserAndHisEntities(userId);

            TempData["Success-Message"] = $"User {userName} deleted successfully!";

            return RedirectToAction("Index");
        }

    }
}
