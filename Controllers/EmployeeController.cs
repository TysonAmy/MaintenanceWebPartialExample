using MaintenanceLibrary.Models;
using MaintenanceWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MaintenanceWebsite.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Constructor for EmployeeController
        /// </summary>
        /// <param name="userManager"><see cref="UserManager{TUser}"/></param>
        /// <param name="roleManager"><see cref="RoleManager{TRole}"/></param>
        public EmployeeController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Queries and Returns a display of all current employess.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> that displays a list of current employees</returns>
        public IActionResult Index()
        {
            List<AppUserModel> employees = MaintenanceLibrary.BusinessLogic.AppUserProcessor.GetAllCurrentEmployeesWithShiftAndSupervisor();
            List<EmployeeViewModel> employeeVM = new();
            foreach (EmployeeViewModel employeeViewModel in employees)
            {
                employeeVM.Add(employeeViewModel);
            }
            return View(employeeVM);
        }

        /// <summary>
        /// Shows the detail about a single Employee
        /// </summary>
        /// <param name="id">Id associated with Employee</param>
        /// <returns></returns>
        public IActionResult Details(string id)
        {
            return View((EmployeeViewModel)MaintenanceLibrary.BusinessLogic.AppUserProcessor.GetEmployeeById(id));
        }


        // GET: Employee/Edit/5
        /// <summary>
        /// Queries for record associated with the Employee's id.
        /// </summary>
        /// <param name="id">Id associated with Employee</param>
        /// <returns>A Task the return a <see cref="ActionResult"/> that displays information about Employee aka User</returns>
        public async Task<ActionResult> EditAsync(string id)
        {
            AppUser appUser = await GetUserByIdAsync(id);
            

            return View(appUser);
        }

        // POST: Employee/Edit/5
        /// <summary>
        /// If model is valid it updates queried user information and returns user to Employee Index page.
        /// else Edit view is return with model errors.
        /// </summary>
        /// <param name="id">Id  representing AppUser</param>
        /// <param name="appUser">A <see cref="AppUser"/> model that represents the changes to queried user.</param>
        /// <returns>A Task that returns a <see cref="ActionResult"/> that if model is valid returns to index page, else returns to edit page showing model errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(string id, AppUser appUser)
        {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByIdAsync(id);
                    user.FirstName = appUser.FirstName;
                    user.LastName = appUser.LastName;
                    user.ShiftId = appUser.ShiftId;
                    user.SupervisorId = appUser.SupervisorId;
                    await SaveAppUser(user);
                    return RedirectToAction(nameof(Index));
                }
                return View(appUser);
        }

        /// <summary>
        /// Populates Roles in select list, query user for id sent. Add user id, email, query add users current role to <see cref="RoleUserViewModel"/>
        /// model. Returns a <see cref="ActionResult"/> of view of old role and new role.
        /// </summary>
        /// <param name="id">Id associated with user whose role is being changed</param>
        /// <returns>A <see cref="ActionResult"/> model that display user email, old and dropdown box to select new role.</returns>
        public async Task<ActionResult> ChangeRoleAsync(string id)
        {
            var roles = _roleManager.Roles.ToList();
            ViewData["rolesSL"]  = new SelectList(roles, "Name", "Name");
            RoleUserViewModel rsvm = new();
            rsvm.UserId = id;
            AppUser user = await _userManager.FindByIdAsync(id);
            rsvm.User = user;
            rsvm.UserId = user.Id;
            rsvm.Email = user.Email;
            IList<string> role = await _userManager.GetRolesAsync(user);
            rsvm.OldRoleName = role[0];
            rsvm.RoleName = role[0];
            return View(rsvm);
        }

        /// <summary>
        /// Saves removes user's old role and add user's new role.
        /// Returns user to Role Index.
        /// </summary>
        /// <param name="id">Id associated with queried user</param>
        /// <param name="roleUserViewModel">A <see cref="RoleUserViewModel"/> that represents the users 
        /// old and new role</param>
        /// <returns>A task that returns an <see cref="ActionResult"/> that redirect user
        /// to Role Index.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeRoleAsync(string id, RoleUserViewModel roleUserViewModel)
        {
            var user = await _userManager.FindByIdAsync(roleUserViewModel.UserId);
            await _userManager.RemoveFromRoleAsync(user, roleUserViewModel.OldRoleName);
            await _userManager.AddToRoleAsync(user, roleUserViewModel.RoleName);
            return RedirectToAction("Index", "Role");

        }

        /// <summary>
        /// Returns the currently logged in user.
        /// </summary>
        /// <returns>A Task that represent the asynchronous operation. The Task result contains <see cref="AppUser"/></returns>
        private Task<AppUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        /// <summary>
        /// Queries user with given id.
        /// </summary>
        /// <param name="id">Id the represents the user being queried.</param>
        /// <returns>A Task that represents the asynchronous operation. 
        /// The Task result contains <see cref="AppUser"/></returns>
        private Task<AppUser> GetUserByIdAsync(string id) => _userManager.FindByIdAsync(id);

        /// <summary>
        /// Saves the user represented in the <see cref="AppUser"/>
        /// </summary>
        /// <param name="appUser">A <see cref="AppUser"/> model the represents the user information being updated.</param>
        /// <returns>The Task results contains <see cref="IdentityResult"/></returns>
        private Task<IdentityResult> SaveAppUser(AppUser appUser) => _userManager.UpdateAsync(appUser);

    }
}
