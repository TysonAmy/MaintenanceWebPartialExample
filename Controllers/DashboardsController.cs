using MaintenanceLibrary.Models;
using MaintenanceWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Controllers
{
    /// <summary>
    /// Controls the querying for <see cref="AppUser"/>, <see cref="SupervisorsNoteModel"/> models and <see cref="DowntimeIssueModel"/> models.
    /// Using <see cref="AppUser"/> id field to query for Supervisor Notes and Downtimes Issues along with their follow ups to display as
    /// <see cref="SupervisorsNotes_FollowUpModel"/> and <see cref="DowntimeIssueViewModel"/>
    /// </summary>
    [Authorize]
    public class DashboardsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// Constructor for Dashboard Controller
        /// </summary>
        /// <param name="userManager"><see cref="UserManager{TUser}"/></param>
        public DashboardsController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Queries for current users downtime issues and supervisor note that the user has follow ups too, or a redirect to the home page if user is not logged in.
        /// </summary>
        /// <param name="errorMessage">Normally shows if an email errors out.</param>
        /// <returns>A Task that represent the asynchronous operation. The Task result contains <see cref="IActionResult"/> that displays downtime issues and supervisor notes view. If user is not logged in a redirect is to home is returned.</returns>
        public async Task<IActionResult> MainAsync(string errorMessage = "")
        {
            ViewBag.ErrorMessage = errorMessage;
            var appUser = await GetCurrentUserAsync();
            if(appUser != null)
            {
                var userId = appUser.Id;
                DashboardMainViewModel vm = new DashboardMainViewModel();
                vm.downtimeIssues = new List<DowntimeIssueViewModel>();
                vm.supervisorsNotes = new List<SupervisorsNoteViewModel>();

                foreach (DowntimeIssueViewModel downtimeIssueViewModel in MaintenanceLibrary.BusinessLogic.DowntimeIssuesProcessor.GetDowntimeIssuesByEmployeeId(userId))
                {
                    vm.downtimeIssues.Add(downtimeIssueViewModel);
                }
                List<SupervisorsNoteModel> supervisorsNotes = MaintenanceLibrary.BusinessLogic.SupervisorsNoteProcessor.getOpenSupervisorsNotesByEmployeeId(userId);
                foreach (SupervisorsNoteViewModel supervisorsNotevm in supervisorsNotes)
                {
                    vm.supervisorsNotes.Add(supervisorsNotevm);
                }
                return View(vm);
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }
 
        /// <summary>
        /// Returns the currently logged in user.
        /// </summary>
        /// <returns>A Task that represent the asynchronous operation. The Task result contains <see cref="AppUser"/></returns>
        private Task<AppUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
