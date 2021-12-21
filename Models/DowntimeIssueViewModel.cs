using MaintenanceLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Models
{
    public class DowntimeIssueViewModel
    {
        /// <summary>
        /// Used to convert <see cref="DowntimeIssueModel"/> to <see cref="DowntimeIssueViewModel"/>
        /// So Model coming from the database can be converted into the ViewModel which can be displayed in the view.
        /// </summary>
        /// <param name="downtimeIssue">A <see cref="DowntimeIssueModel"/> that represent the Downtime Issue coming from the database</param>
        public static implicit operator DowntimeIssueViewModel(DowntimeIssueModel downtimeIssue)
        {
            List<DowntimeIssue_FollowupViewModel> downtimeIssue_FollowupViewModels = new List<DowntimeIssue_FollowupViewModel>();
            int orderOfFollowups = 1;
            if(downtimeIssue.DowntimeIssue_Followups is not null)
            {
                foreach (DowntimeIssue_FollowupViewModel downtimeIssue_Followup in downtimeIssue.DowntimeIssue_Followups)
                {
                    downtimeIssue_Followup.ListNumber = orderOfFollowups++; // set follow up number and go to next.
                    downtimeIssue_FollowupViewModels.Add(downtimeIssue_Followup);
                }

            }
            return new DowntimeIssueViewModel
            {
                Id = downtimeIssue.Id,
                Created = downtimeIssue.Created,
                DownTime = downtimeIssue.DownTime,
                Employee = downtimeIssue.Employee,
                EmployeeId = downtimeIssue.EmployeeId,
                Equipment = downtimeIssue.Equipment,
                EquipmentId = downtimeIssue.EquipmentId,
                IssueResolution = downtimeIssue.IssueResolution,
                DowntimeIssue_Followups = downtimeIssue_FollowupViewModels,
                Completed = downtimeIssue.Completed
               
            };
        }

        /// <summary>
        /// Used to convert <see cref="DowntimeIssueViewModel"/> to <see cref="DowntimeIssueModel"/>
        /// So ViewModel coming from the view can be converted into the Model which can be saved to the database
        /// </summary>
        /// <param name="downtimeIssueViewModel">A <see cref="DowntimeIssueViewModel"/> that represents what is coming from the View</param>
        public static implicit operator DowntimeIssueModel(DowntimeIssueViewModel downtimeIssueViewModel)
        {

            return new DowntimeIssueModel
            {
                Id = downtimeIssueViewModel.Id,
                Created = downtimeIssueViewModel.Created,
                DownTime = downtimeIssueViewModel.DownTime,
                EmployeeId = downtimeIssueViewModel.EmployeeId,
                EquipmentId = downtimeIssueViewModel.EquipmentId,
                IssueResolution = downtimeIssueViewModel.IssueResolution,
                Completed = downtimeIssueViewModel.Completed
            };
        }

        /// <summary>
        /// Id that represents the Downtime Issue
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> downtime issue was created
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Id that represent the Employee who created this downtime issue
        /// </summary>
        [DisplayName("Employee")]
        public string EmployeeId { get; set; }

        /// <summary>
        /// <see cref="AppUserModel"/> the represents the Employee who created this downtime issue
        /// </summary>
        public AppUserModel Employee { get; set; }

        /// <summary>
        /// Id of the Area where the Equipment referred to is located
        /// </summary>
        [DisplayName("Area")]
        public int AreaId { get; set; }

        /// <summary>
        /// Id of the Equipment being referred.
        /// </summary>
        [Required]
        [DisplayName("Equipment")]
        public int EquipmentId { get; set; }

        /// <summary>
        /// <see cref="EquipmentViewModel"/> representing Equipment Downtime issue is in reference of.
        /// </summary>
        public EquipmentViewModel Equipment { get; set; }

        /// <summary>
        /// Resultion for the downtime issue
        /// </summary>
        [Required]
        [DisplayName("Issue Resolution")]
        public string IssueResolution { get; set; }

        /// <summary>
        /// Number of minutes equipment was down
        /// </summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a whole number")]
        [DisplayName("Downtime Minutes")]
        public int DownTime { get; set; }

        /// <summary>
        /// Used to tell if the first downtime issue follow should be created at the same time downtime issue is
        /// created
        /// ONLY true if first downtime issue follow up should be created the same time downtime issue is
        /// created.
        /// </summary>
        [DisplayName("Has Follow Up")]
        public bool HasFollowUp { get; set; }

        /// <summary>
        /// Reason for first downtime issue follow up
        /// ONLY used if HasFollowUp is true, therefore first follow up being created at the same time
        /// </summary>
        [DisplayName("Follow Up Reason")]
        public string FollowingUpReason { get; set; }

        /// <summary>
        /// <see cref="string"/> Id that represents User to be followed up with
        /// ONLY used if HasFollowUp is true, therefore first follow up being created at the same time
        /// </summary>
        [DisplayName("Supervisor Following Up With")]
        public string FollowUpSupervisorId { get; set; }

        /// <summary>
        /// List of <see cref="DowntimeIssue_FollowupViewModel"/> that represent Downtime Issue Follows 
        /// associated with this downtime issue follow up
        /// </summary>
        public List<DowntimeIssue_FollowupViewModel> DowntimeIssue_Followups { get; set; }

        /// <summary>
        /// This is true if the submit button is pressed creating a downtime issue
        /// </summary>
        public string Submit { get; set; }

        /// <summary>
        /// The <see cref="DateTime"/> that last follow up was set to completed.
        /// </summary>
        public DateTime Completed { get; set; }
    }
}
