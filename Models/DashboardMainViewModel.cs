using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Models
{
    /// <summary>
    /// Use to model data for the Dashboards Main View
    /// </summary>
    public class DashboardMainViewModel
    {
        /// <summary>
        /// List of <see cref="DowntimeIssueViewModel"/>
        /// </summary>
        public List<DowntimeIssueViewModel> downtimeIssues { get; set; }

        /// <summary>
        /// List of <see cref="SupervisorsNoteViewModel"/>
        /// </summary>
        public List<SupervisorsNoteViewModel> supervisorsNotes { get; set; }        
    }
}
