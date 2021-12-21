using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceLibrary.Models
{
    public class DowntimeIssueModel
    {
        /// <summary>
        /// Id representing a Downtime Issue
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> downtime issue was created
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// <see cref="string"/> Id that represences the AppUser(Employee) that created this downtime issue
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// <see cref="AppUserModel"/> model of AppUser(Employee) that created this downtime issue.
        /// </summary>
        public AppUserModel Employee { get; set; }
        
        /// <summary>
        /// <see cref="int"/> Id that represences the Equipment this downtime issue is about.
        /// </summary>
        public int EquipmentId { get; set; }

        /// <summary>
        /// <see cref="EquipmentModel"/> that represences the Equipment this downtime issue is about.
        /// </summary>
        public EquipmentModel Equipment { get; set; }

        /// <summary>
        /// The resolution for the downtime issue
        /// </summary>
        public string IssueResolution { get; set; }

        /// <summary>
        /// <see cref="int"/> minutes of downtime issue took.
        /// </summary>
        public int DownTime { get; set; }

        /// <summary>
        /// List of <see cref="DowntimeIssue_FollowupModel"/> associated with this Downtime Issue
        /// </summary>
        public List<DowntimeIssue_FollowupModel> DowntimeIssue_Followups { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> Downtime Issue Follow Ups were complete, if still being followed up on
        /// this this value will be null.
        /// </summary>
        public DateTime Completed { get; set; }
    }
}
