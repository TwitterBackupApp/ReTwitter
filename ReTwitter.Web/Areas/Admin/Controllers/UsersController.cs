using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReTwitter.Data.Models;
using ReTwitter.Services.Data.Contracts;
using ReTwitter.Web.Areas.Admin.Models.Users;

namespace ReTwitter.Web.Areas.Admin.Controllers
{
    using static WebConstants;

    [Area(AdminArea)]
    [Authorize(Roles = AdminRole)]
    public class UsersController : Controller
    {
        private readonly IAdminUserService userService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly ICascadeDeleteService cascadeDeleteService;

        public UsersController(
            IAdminUserService userService,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            ICascadeDeleteService cascadeDeleteService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.cascadeDeleteService = cascadeDeleteService ?? throw new ArgumentNullException(nameof(cascadeDeleteService));
        }

        public async Task<IActionResult> Index()
        {
            var users = await this.userService.AllAsync();

            var roles = await this.roleManager
                .Roles
                .Where(r => r.Name != MasterAdminRole)
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync();

            var vm = new UserListingsViewModel
            {
                Users = users,
                Roles = roles
            };

            return this.View(vm);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var loggedUser = await this.userManager.GetUserAsync(HttpContext.User);

            if (loggedUser.Id == id)
            {
                return this.Json(false);
            }

            var loggedUserRoles = await this.userManager.GetRolesAsync(loggedUser);
            var userToDelete = await this.userService.SingleUserByIdAsync(id);
            var userToDeleteRoles = await this.userManager.GetRolesAsync(userToDelete);

            if (!loggedUserRoles.Contains(MasterAdminRole) && userToDeleteRoles.Contains(AdminRole))
            {
                return this.Json(false);
            }

            this.cascadeDeleteService.DeleteUserAndHisEntities(userToDelete.Id);

            return this.Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddUserToRoleViewModel model)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            if (!roleExists || !userExists)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid identity details.");
            }

            if (!ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            await this.userManager.AddToRoleAsync(user, model.Role);

            TempData["Success-Message"] = $"User {user.UserName} successfully added to the {model.Role} role.";

            return this.RedirectToAction(nameof(Index));
        }
    }
}
