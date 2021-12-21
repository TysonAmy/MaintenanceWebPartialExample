using Hangfire;
using MaintenanceLibrary.Models;
using MaintenanceWebsite.Models;
using MaintenanceWebsite.OtherMethods;
using MaintenanceWebsite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Controllers
{
    /// <summary>
    /// Controls the query for <see cref="DowntimeIssue_FollowupModel"/>.
    /// Display for edit form of <see cref="DowntimeIssue_FollowupViewModel"/>.
    /// Display of create form.
    /// </summary>
    [Authorize]
    public class DowntimeIssue_FollowupController : Controller
    {
        IEmailSender emailSender;
        private readonly UserManager<AppUser> _userManager;
        IBackgroundJobClient backgroundJobs;

        /// <summary>
        /// Constructor for Downtime Issue Followups
        /// </summary>
        /// <param name="userManager"><see cref="UserManager{TUser}"/></param>
        /// <param name="emailSender"><see cref="IEmailSender"/></param>
        /// <param name="backgroundJobs"><see cref="IBackgroundJobClient"/></param>
        public DowntimeIssue_FollowupController(UserManager<AppUser> userManager, IEmailSender emailSender, IBackgroundJobClient backgroundJobs)
        {
            this.emailSender = emailSender;
            _userManager = userManager;
            this.backgroundJobs = backgroundJobs;

        }

        // GET: DowntimeIssue_FollowupController/Create
        /// <summary>
        /// Creates a form, populated with the Parent Downtime Issue Id
        /// </summary>
        /// <param name="Id">Associated Downtime Issue Id</param>
        /// <returns>An Instance of <see cref="ActionResult"/> displays an empty Create Downtime Issue Form, with Downtime Issue Id as a hidden field</returns>
        [HttpGet]
        public ActionResult Create(int Id) // Id is for DowntimeIssue
        {
            DowntimeIssue_FollowupViewModel downtimeIssue_FollowupVM = new DowntimeIssue_FollowupViewModel
            {
                DowntimeIssueId = Id
            };
            return View(downtimeIssue_FollowupVM);
        }

        // POST: DowntimeIssue_FollowupController/Create
        /// <summary>
        /// If model is valid to creates a new downtime Issue and returns user to main dashboard with no errors showing. 
        /// If after creating if email fails, user will be returned to Dashboards Main showing the error
        /// If model is not valid, then user is returned to the create form.
        /// </summary>
        /// <param name="downtimeIssue_FollowupVM">An <see cref="DowntimeIssue_FollowupViewModel"/> representing the downtime issue</param>
        /// <returns>A task that returns a <see cref="ActionResult"/> that returns user to Dashboard Main, or returns user to downtime issue create</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(DowntimeIssue_FollowupViewModel downtimeIssue_FollowupVM)
        {
                if(ModelState.IsValid)
                {
                    int downtimeIssueId;
                    DowntimeIssueModel downtimeIssue;
                    DowntimeIssue_FollowupModel downtimeIssue_Followup = downtimeIssue_FollowupVM;
                    // Note: Insert adds time to previous followup time, and creates new followup.
                    downtimeIssueId = downtimeIssue_FollowupVM.DowntimeIssueId;
                    MaintenanceLibrary.BusinessLogic.DowntimeIssues_FollowupProcessor.Insert(downtimeIssue_Followup);
                    downtimeIssue = MaintenanceLibrary.BusinessLogic.DowntimeIssuesProcessor.GetDowntimeIssueByDowntimeIssueId(downtimeIssueId);
                    AppUser currentUser = await GetCurrentUserAsync();
                    try
                    {
                        backgroundJobs.Enqueue(() => EmailHelpers.SendEmailAboutDowntimeIssue((EmailSender)emailSender, downtimeIssue, currentUser));

                    }
                    catch (System.Net.Mail.SmtpFailedRecipientException e)
                    {
                        return RedirectToAction("Main", "Dashboards", new { errorMessage = e.Message + " " + e.FailedRecipient });
                    }
                    return RedirectToAction("Main", "Dashboards");
                }
                else
                {
                    return View(downtimeIssue_FollowupVM);

                }
        }

        // GET: DowntimeIssue_FollowupController/Edit/5
        /// <summary>
        /// Looks up the downtime issue followup and displays a Edit Form, Prepopulated with downtime issue information.
        /// </summary>
        /// <param name="id">Id for Downtime issue followup</param>
        /// <returns>An <see cref="ActionResult"/> that shows the edit form prepopulated</returns>
        public ActionResult Edit(int id)
        {
            // Save Comment
            DowntimeIssue_FollowupViewModel downtimeIssue_Followupvm = MaintenanceLibrary.BusinessLogic.DowntimeIssues_FollowupProcessor.getDowntimeIssue_FollowupByDowntimeIssue_FollowupId(id);
            return View(downtimeIssue_Followupvm);
        }

        // POST: DowntimeIssue_FollowupController/Edit/5
        /// <summary>
        ///  If <see cref="DowntimeIssue_FollowupViewModel"/> model is valid updates downtime issue followup 
        ///  and redirects to Dashboards Main
        ///  Else returns edit form with errors.
        /// </summary>
        /// <param name="id">The Id associated with the downtime issue followup</param>
        /// <param name="downtimeIssue_FollowupVM">An <see cref="DowntimeIssue_FollowupViewModel"/> representing downtime issue followup</param>
        /// <returns><see cref="ActionResult"/> If model is valid, then redirect to Main Dashboard else returns return user to edit form with errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DowntimeIssue_FollowupViewModel downtimeIssue_FollowupVM)
        {
                if (ModelState.IsValid)
                {
                    // Save Comment
                    DowntimeIssue_FollowupModel downtimeIssue_Followup = downtimeIssue_FollowupVM;
                    MaintenanceLibrary.BusinessLogic.DowntimeIssues_FollowupProcessor.Update(downtimeIssue_Followup);
                    return RedirectToAction("Main", "Dashboards");
                }
                else
                {
                    return View(downtimeIssue_FollowupVM);
                }
        }

        /// <summary>
        /// Returns the currently logged in user.
        /// </summary>
        /// <returns>A Task that represent the asynchronous operation. The Task result contains <see cref="AppUser"/></returns>
        private Task<AppUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


    }
}
