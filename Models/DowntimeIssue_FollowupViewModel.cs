using MaintenanceLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Models
{
    public class DowntimeIssue_FollowupViewModel
    {
        /// <summary>
        /// Used to convert a <see cref="DowntimeIssue_FollowupModel"/> to <see cref="DowntimeIssue_FollowupViewModel"/>
        /// So Model coming from the database can be converted into the ViewModel which can be displayed in the view.
        /// </summary>
        /// <param name="downtimeIssue_Followup">A <see cref="DowntimeIssue_FollowupModel"/> that represents the Downtime Issue's
        /// Follow up coming from the database</param>
        public static implicit operator DowntimeIssue_FollowupViewModel(DowntimeIssue_FollowupModel downtimeIssue_Followup)
        {
            DowntimeIssue_FollowupViewModel downtimeIssue_FollowupViewModel =  new DowntimeIssue_FollowupViewModel
            {
                DowntimeIssueId = downtimeIssue_Followup.DowntimeIssueId,
                Employee = downtimeIssue_Followup.Employee,
                EmployeeId = downtimeIssue_Followup.EmployeeId,
                FollowingUpReason = downtimeIssue_Followup.FollowingUpReason,
                FollowUpRequest = downtimeIssue_Followup.FollowUpRequest,
                Id = downtimeIssue_Followup.Id,
                SupervisorComments = downtimeIssue_Followup.SupervisorComments,
                SupervisorFollowUp = downtimeIssue_Followup.SupervisorFollowUp
            };
            
            return downtimeIssue_FollowupViewModel;
        }

        /// <summary>
        /// Used to convert a <see cref="DowntimeIssue_FollowupViewModel"/> to a <see cref="DowntimeIssue_FollowupModel"/>
        /// So ViewModel coming from the view can be converted into the Model which can be saved to the database
        /// </summary>
        /// <param name="downtimeIssue_FollowupViewModel">A <see cref="DowntimeIssue_FollowupViewModel"/> that 
        /// represents what is coming from the View</param>
        public static implicit operator DowntimeIssue_FollowupModel(DowntimeIssue_FollowupViewModel downtimeIssue_FollowupViewModel)
        {
            return new DowntimeIssue_FollowupModel
            {
                DowntimeIssueId = downtimeIssue_FollowupViewModel.DowntimeIssueId,
                EmployeeId = downtimeIssue_FollowupViewModel.EmployeeId,
                FollowingUpReason = downtimeIssue_FollowupViewModel.FollowingUpReason,
                FollowUpRequest = downtimeIssue_FollowupViewModel.FollowUpRequest,
                Id = downtimeIssue_FollowupViewModel.Id,
                SupervisorComments = downtimeIssue_FollowupViewModel.SupervisorComments,
                SupervisorFollowUp = downtimeIssue_FollowupViewModel.SupervisorFollowUp
            };
        }

        /// <summary>
        /// Id associated with the Downtime Issue's Follow Up
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id associated with the Downtime Issue's Follow Up Parent, Downtime Issue
        /// </summary>
        public int DowntimeIssueId { get; set; }

        /// <summary>
        /// <see cref="DowntimeIssueViewModel"/> representing the parent, Downtime Issue, of this Downtime Issue Follow Up.
        /// </summary>
        public DowntimeIssueViewModel DowntimeIssue { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> downtime issue follow up was created.
        /// </summary>
        [DisplayName("Time Followup was Requested")]
        public DateTime FollowUpRequest { get; set; } // When request for follow up was made (Auto Fill)

        /// <summary>
        /// Reason downtime issue follow up was created
        /// </summary>
        [Required]
        [DisplayName("Follow Up Reason")]
        public string FollowingUpReason { get; set; }

        /// <summary>
        /// Id associated with the Employee who is being followed up with
        /// </summary>
        [Required]
        [DisplayName("Follow Up Supervisor")]
        public string EmployeeId { get; set; }

        /// <summary>
        /// <see cref="EmployeeViewModel"/> that represents Employee who is being follow up with
        /// </summary>
        public EmployeeViewModel Employee { get; set; }

        /// <summary>
        /// Comment from Employee being followed up with
        /// </summary>
        [DisplayName("Supervisor's Comments")]
        public string SupervisorComments { get; set; } = "";

        /// <summary>
        /// <see cref="DateTime"/> Supervisor told the next Supervisor to follow up 
        /// </summary>
        public DateTime? SupervisorFollowUp { get; set; }

        /// <summary>
        /// Used to count place in the list of Downtime Issue Follow up list.
        /// </summary>
        public int ListNumber { get; set; }
    }
}
