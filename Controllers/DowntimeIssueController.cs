using Hangfire;
using MaintenanceLibrary.Models;
using MaintenanceWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaintenanceWebsite.OtherMethods;
using MaintenanceWebsite.Services;

namespace MaintenanceWebsite.Controllers
{
    /// <summary>
    /// Controls the querying for <see cref="DowntimeIssueModel"/> and 
    /// displaying of <see cref="DowntimeIssueViewModel"/> in verious views.
    /// </summary>
    [Authorize]
    public class DowntimeIssueController : Controller
    {
        IEmailSender _emailSender;
        private readonly UserManager<AppUser> _userManager;
        IBackgroundJobClient _backgroundJobs;

        /// <summary>
        /// Downtime Issue Constructor
        /// </summary>
        /// <param name="userManager"><see cref="UserManager{TUser}"/></param>
        /// <param name="emailSender"><see cref="IEmailSender"/></param>
        /// <param name="backgroundJobs"><see cref="IBackgroundJobClient"/></param>
        public DowntimeIssueController(
            UserManager<AppUser> userManager, 
            IEmailSender emailSender, 
            IBackgroundJobClient backgroundJobs)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _backgroundJobs = backgroundJobs;
        }

        // GET: DowntimeIssueController/Search
        /// <summary>
        /// Searches Downtime issues with perpopulated variables (starttime last time user has logged in). 
        /// Fills drop down box for Equipment, based on Area.and returns a view with search form and this downtime issues.
        /// </summary>
        /// <returns>A Task that returns a <see cref="ActionResult"/> that shows search form.</returns>
        [HttpGet]
        public async Task<ActionResult> SearchAsync()
        {
            DowntimeIssueSearchViewModel model = new DowntimeIssueSearchViewModel();
            foreach (AreaModel areaModel in MaintenanceLibrary.BusinessLogic.AreasProcessor.GetAreas())
            {
                model.Areas.Add(new SelectListItem { Text = areaModel.Name, Value = areaModel.Id.ToString() });
            }
            DateTime datetime = DateTime.Now;
            var User = await GetCurrentUserAsync();
            DateTime? startDatetemp = MaintenanceLibrary.BusinessLogic.LoginProcessor.GetPreviousLogin(User.Id);
            DateTime startDate;
            if(startDatetemp is null)
            {
                startDate = datetime.AddDays(-365).AddMilliseconds(-datetime.Millisecond).AddSeconds(-datetime.Second);
            }
            else
            {
                startDate = (DateTime)startDatetemp;
                startDate = startDate.AddMilliseconds(-startDate.Millisecond).AddSeconds(-startDate.Second);
            }
            model.StartDate = (DateTime)startDate;
            model.EndDate = datetime.AddMilliseconds(-datetime.Millisecond).AddSeconds(-datetime.Second);
            model.SearchDate = true;
            model.Query();
            return View(model);
        }


        // POST: DowntimeIssueController/Search
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AreaId">Area Id being searched for Equipment dropdown list.</param>
        /// <param name="EquipmentId">Equipment Id being search.</param>
        /// <param name="model">An <see cref="DowntimeIssue_FollowupViewModel"/> that represent fields and search results for downtime issues</param>
        /// <returns>The <see cref="ActionResult"/> that displays view with search form and downtime issues</returns>
        [HttpPost]
        public ActionResult Search(int? AreaId, int? EquipmentId, DowntimeIssueSearchViewModel model )
        {
            foreach (AreaModel areaModel in MaintenanceLibrary.BusinessLogic.AreasProcessor.GetAreas())
            {
                model.Areas.Add(new SelectListItem { Text = areaModel.Name, Value = areaModel.Id.ToString() });
            }

            if (AreaId.HasValue)
            {
                foreach (EquipmentModel equipment in MaintenanceLibrary.BusinessLogic.EquipmentProcessor.GetEquipmentByAreaId((int)AreaId))
                {
                    model.Equipment.Add(new SelectListItem { Text = equipment.Name, Value = equipment.Id.ToString() });
                }
            }
            if (model.Equipment.Where(item => item.Value == model.EquipmentId.ToString()).ToList().Count > 0 
                || !model.SearchEquipment)
            {
                //If equipment is in area then we need to query model
                model.Query();
            }
            return View(model);
        }


        // GET: DowntimeIssueController
        /// <summary>
        /// Displays all downtime issues
        /// </summary>
        /// <returns>An <see cref="ActionResult"/> that displays all downtime Issues</returns>
        public ActionResult Index()
        {
            var downtimeIssues = MaintenanceLibrary.BusinessLogic.DowntimeIssuesProcessor.GetDowntimeIssues();
            List<DowntimeIssueViewModel> downtimeIssueViewModels = new List<DowntimeIssueViewModel>();
            foreach (DowntimeIssueViewModel downtimeIssueViewModel in downtimeIssues)
            {
                downtimeIssueViewModels.Add(downtimeIssueViewModel);
            }
            return View(downtimeIssueViewModels);
        }

        // GET: DowntimeIssueController/Details/5
        /// <summary>
        /// Gets associated downtime issue and downtime issues follows.
        /// Displays a downtime Issue, with downtime issue followups.
        /// </summary>
        /// <param name="id">The id associated with the downtime issue</param>
        /// <returns>An <see cref="ActionResult"/> that displays a downtime issue and its followups.</returns>
        public ActionResult Details(int id)
        {
            DowntimeIssueModel downtimeIssue = MaintenanceLibrary.BusinessLogic.DowntimeIssuesProcessor.GetDowntimeIssueByDowntimeIssueId(id);
            DowntimeIssueViewModel downtimeIssueVM = downtimeIssue;
            return View(downtimeIssueVM);
        }

        // GET: DowntimeIssueController/Create
        /// <summary>
        /// Displays a blank downtime issue form
        /// </summary>
        /// <returns>A task the returns an <see cref="ActionResult"/> that displays a blank create form.</returns>
        public async Task<ActionResult> CreateAsync()
        {
            AppUser appUser = await GetCurrentUserAsync();
            DowntimeIssueViewModel downtimeIssueVM = new DowntimeIssueViewModel
            {
                EmployeeId = appUser.Id,
                AreaId = 0
            };
            return View(downtimeIssueVM);
        }

        // POST: DowntimeIssueController/Create
        /// <summary>
        /// If the <see cref="DowntimeIssueViewModel"/> model is valid then, downtime issue is created, and if added to form, 
        /// the first downtime follow up is created. Then user is sent to Dashboards Main. If an error occurs while sending email, 
        /// then it is displayed on Dashboards Main.
        /// else if model is not valid then, user sent back to create form with errors showing.
        /// </summary>
        /// <param name="downtimeIssueViewModel">An <see cref="DowntimeIssueViewModel"/> representing the Downtime Issue and possible first followup.</param>
        /// <returns>A Task that returns an <see cref="ActionResult"/> that if model is valid returns user to Dashboards Main, with errors from sending email. 
        /// If model is not valid then will return downtime issue form with errors.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(DowntimeIssueViewModel downtimeIssueViewModel)
        {
            int downtimeIssueId;
            if (downtimeIssueViewModel.Submit is not null)
            {
                if (ModelState.IsValid)
                {
                    AppUser currentUser = await GetCurrentUserAsync();
                    downtimeIssueViewModel.EmployeeId = currentUser.Id;
                    downtimeIssueId = MaintenanceLibrary.BusinessLogic.DowntimeIssuesProcessor.Insert(downtimeIssueViewModel);
                    if(downtimeIssueViewModel.HasFollowUp)
                    {
                        DowntimeIssue_FollowupModel downtimeIssue_Followup = new();
                        downtimeIssue_Followup.DowntimeIssueId = downtimeIssueId;
                        downtimeIssue_Followup.FollowingUpReason = downtimeIssueViewModel.FollowingUpReason;
                        downtimeIssue_Followup.EmployeeId = downtimeIssueViewModel.FollowUpSupervisorId;
                        MaintenanceLibrary.BusinessLogic.DowntimeIssues_FollowupProcessor.Insert(downtimeIssue_Followup);
                        DowntimeIssueModel downtimeIssue = MaintenanceLibrary.BusinessLogic.DowntimeIssuesProcessor.GetDowntimeIssueByDowntimeIssueId(downtimeIssueId);
                        try
                        {
                            _backgroundJobs.Enqueue(() => EmailHelpers.SendEmailAboutDowntimeIssue((EmailSender)_emailSender, downtimeIssue, currentUser));
                        }
                        catch (System.Net.Mail.SmtpFailedRecipientException e)
                        {
                            Console.WriteLine(e.ToString());
                            return RedirectToAction("Search", "DowntimeIssue", new { errorMessage = "Error: " + e.Message + " " + e.FailedRecipient });
                        }

                    }
                    else
                    {
                        downtimeIssueViewModel.Id = downtimeIssueId;
                        downtimeIssueViewModel.Completed = DateTime.Now;
                        MaintenanceLibrary.BusinessLogic.DowntimeIssuesProcessor.Update(downtimeIssueViewModel);
                    }
                    return RedirectToAction("Search", "DowntimeIssue");
                }
                else
                {
                    return View(downtimeIssueViewModel);
                }
            }
            else
            {
                ModelState.ClearValidationState("EquipmentId");
                ModelState.ClearValidationState("EmployeeId");
                ModelState.ClearValidationState("IssueResolution");
                ModelState.ClearValidationState("DownTime");
                ModelState.ClearValidationState("FollowingUpReason");
                ModelState.ClearValidationState("FollowUpSupervisorId");

                return View(downtimeIssueViewModel);
            }

        }

        /// <summary>
        /// Displays a downtime issue that is being requested to set to complete.
        /// </summary>
        /// <param name="Id">Id associated with downtime issue</param>
        /// <returns>An <see cref="ActionResult"/> that displays the downtime issues and confirmation 
        /// to set follow ups as complete.</returns>
        public ActionResult Complete(int Id)
        {
            DowntimeIssueViewModel downtimeIssue = MaintenanceLibrary.BusinessLogic
                .DowntimeIssuesProcessor.GetDowntimeIssueByDowntimeIssueId(Id);
            return View(downtimeIssue);
        }

        /// <summary>
        /// Sets downtime issue to complete, by putting datetime is last follow ups complete field and putting datetime
        /// in downtime issue complete field.
        /// </summary>
        /// <param name="DowntimeIssueId">Id associated with downtime issue</param>
        /// <param name="downtimeIssue">An <see cref="DowntimeIssueViewModel"/> representing the downtime issue.</param>
        /// <returns>A <see cref="ActionResult"/> that redirects a user to Dashboards Main</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Complete(int DowntimeIssueId, DowntimeIssueViewModel downtimeIssue)
        {
            DowntimeIssue_FollowupModel downtimeIssue_Followup = MaintenanceLibrary.BusinessLogic
                .DowntimeIssues_FollowupProcessor.GetDowntimeIssue_Followup_LastFollowup_ByDowntimeIssueId(downtimeIssue.Id);
            downtimeIssue_Followup.SupervisorFollowUp = DateTime.Now;
            MaintenanceLibrary.BusinessLogic.DowntimeIssues_FollowupProcessor
                .Update(downtimeIssue_Followup);
            downtimeIssue.Completed = DateTime.Now;
            MaintenanceLibrary.BusinessLogic.DowntimeIssuesProcessor.Update(downtimeIssue);
            return RedirectToAction("Main", "Dashboards");

        }

        /// <summary>
        /// Returns the currently logged in user.
        /// </summary>
        /// <returns>A Task that represent the asynchronous operation. The Task result contains <see cref="AppUser"/></returns>
        private Task<AppUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
