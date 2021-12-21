using MaintenanceWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Controllers
{
    /// <summary>
    /// Displays the Index Page, Downtime Issue Search or Dashboards Main, depending on if and who user is login as.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// Constructer for Home Controller
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/></param>
        /// <param name="userManager"><see cref="UserManager{TUser}"/></param>
        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        /// <summary>
        /// Queries for current user. 
        /// If null send user to index view.
        /// Else If user has not reset there initial password, they are redirected to password reset.
        /// Else If user has the Machanic Role then user is redirected to Downtime Issue Search
        /// Else send user to Dashboards Main
        /// </summary>
        /// <returns>A Task that returns a <see cref="ActionResult"/> that sends user to either Index View,
        /// redirects to Initial Password, redirects to Downtime Issue Search or Dashboards Main. See Summary</returns>
        public async Task<IActionResult> IndexAsync()
        {
            AppUser appUser = await GetCurrentUserAsync();
            if (appUser is null)
            {
                return View();
            }
            else if (!appUser.PasswordReset)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                return RedirectToPage("/Account/Manage/ChangePassword", new { area = "Identity" });
            }
            else if(User.IsInRole("Mechanic"))
            {
                return RedirectToAction("Search", "DowntimeIssue");
            }
            else if(User.IsInRole("Admin") || User.IsInRole("Supervisor"))
            {
                return RedirectToAction("Main", "Dashboards");
            }
            else if(User.IsInRole("MRO Supervisor") || User.IsInRole("MRO"))
            {
                return RedirectToAction("Search", "RepairPart");
            }
            else
            {
                return View();
            }

        }

        /// <summary>
        /// Displays Privacy View.
        /// This function is not in use as of 12-10-2021.
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Shows user an error page.
        /// </summary>
        /// <returns>A <see cref="IActionResult"/> show user the Error Page.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Returns the currently logged in user.
        /// </summary>
        /// <returns>A Task that represent the asynchronous operation. The Task result contains <see cref="AppUser"/></returns>
        private Task<AppUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
