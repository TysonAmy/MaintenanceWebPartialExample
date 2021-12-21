using MaintenanceWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Controllers
{
    /// <summary>
    /// Allow a users to be changed between different roles. 
    /// Can display list Roles and what Users are in those Roles.
    /// There is a create ablity for this controller, but I is not implemented as of 12-10-2021.
    /// </summary>
    [Authorize]
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> roleManager;
        UserManager<AppUser> userManager;

        /// <summary>
        /// Inializes the RoleController
        /// </summary>
        /// <param name="roleManager"><see cref="RoleManager{TRole}"/></param>
        /// <param name="userManager"><see cref="UserManager{TUser}"/></param>
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        /// <summary>
        /// Displays a List of Roles with the users in them.
        /// </summary>
        /// <returns>A Task that returns a <see cref="IActionResult"/> that displays a List of Roles and 
        /// the Users in those roles.</returns>
        public async Task<IActionResult> IndexAsync()
        {
            RoleViewModel roleVM;
            List< RoleViewModel> RolesVM = new();
            List<IdentityRole> roles = roleManager.Roles.ToList<IdentityRole>();
            foreach (IdentityRole role in roles)
            {
                roleVM = new();
                roleVM.Id = role.Id;
                roleVM.Name = role.Name;
                var users = await userManager.GetUsersInRoleAsync(role.Name);
                roleVM.Users = users;
                RolesVM.Add(roleVM);
                
            }
            return View(RolesVM);
        }

        /// <summary>
        /// Shows a blank create form that allows someone to submit a new role.
        /// </summary>
        /// <returns>A <see cref="IActionResult"/> that displays a new Role.</returns>
        public IActionResult Create()
        {
            return View(new IdentityRole());
        }

        /// <summary>
        /// If <see cref="IdentityRole"/> model is valid it creates a new Role and redirects users to Role Index, 
        /// else it take user back to create form, showing errors.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            if(ModelState.IsValid)
            {
                await roleManager.CreateAsync(role);
                return RedirectToAction("Index");
            }
            return View(role);
        }
    }
}
