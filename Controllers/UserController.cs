using MaintenanceWebsite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Controllers
{
    public class UserController : Controller
    {
        UserManager<AppUser> userManager;

        /// <summary>
        /// Allow view of users
        /// </summary>
        /// <param name="userManager"><see cref="UserManager{TUser}"/></param>
        public UserController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Creates a list of users, active and inactive.
        /// </summary>
        /// <returns>A <see cref="IActionResult"/> that displays a list of users.</returns>
        public IActionResult Index()
        {
            var users = userManager.Users.ToList();
            return View(users);
        }

    }
}
