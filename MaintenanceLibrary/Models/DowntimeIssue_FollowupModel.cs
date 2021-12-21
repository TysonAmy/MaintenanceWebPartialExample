using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceLibrary.Models
{
    public class DowntimeIssue_FollowupModel
    {
        /// <summary>
        /// Id of Downtime Issue Follow Up
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of Parent Downtime Issue
        /// </summary>
        public int DowntimeIssueId { get; set; }

        /// <summary>
        /// A <see cref="DowntimeIssueModel"/> that represents the parent Downtime Issue
        /// </summary>
        public DowntimeIssueModel DowntimeIssue { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> Downtime Issue Follow up was created.
        /// </summary>
        public DateTime FollowUpRequest { get; set; } // When request for follow up was made (Auto Fill)

        /// <summary>
        /// Reason downtime issue follow up was requested.
        /// </summary>
        public string FollowingUpReason { get; set; }
       
        /// <summary>
        /// Id representing Employee that requested the Downtime Issue Follow Up.
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// <see cref="Employee"/> representing Employee that requested the Downtime Issue Follow Up
        /// </summary>
        public AppUserModel Employee { get; set; }

        /// <summary>
        /// Comment from Supervisor following up
        /// </summary>
        public string SupervisorComments { get; set; } = "";

        /// <summary>
        /// <see cref="DateTime"/> next Follow Up was requested.
        /// </summary>
        public DateTime? SupervisorFollowUp { get; set; } // Date/Time Supervisor told the next Supervisor to follow up or follow up change is complete

    }
}
