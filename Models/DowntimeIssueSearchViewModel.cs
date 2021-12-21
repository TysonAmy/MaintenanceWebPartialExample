using MaintenanceLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Models
{
    /// <summary>
    /// Used to model that data used in Downtime Issue Search View
    /// </summary>
    public class DowntimeIssueSearchViewModel
    {
        /// <summary>
        /// Initializes list for Areas, Equipment, and Downtime Issues
        /// </summary>
        public DowntimeIssueSearchViewModel()
        {
            this.Areas = new List<SelectListItem>();
            this.Equipment = new List<SelectListItem>();
            this.downtimeIssuesVM = new List<DowntimeIssueViewModel>();
        }

        /// <summary>
        /// <see cref="DateTime"/> to start search of downtime issues.
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        /// <summary>
        /// <see cref="DateTime"/> to end search of downtime issues.
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Whether to search dates from start date to end date
        /// </summary>
        [DisplayName("Search Date")]
        public bool SearchDate { get; set; } = false;

        /// <summary>
        /// Whether to search for a downtime issues that involve a individual equipment
        /// </summary>
        [DisplayName("Search Equipment")]
        public bool SearchEquipment { get; set; } = false;

        /// <summary>
        /// Used to populate Area Dropdown box
        /// </summary>
        public List<SelectListItem> Areas { get; set; }

        /// <summary>
        /// Used to populate Equipment Dropdown box
        /// </summary>
        public List<SelectListItem> Equipment { get; set; }
        
        /// <summary>
        /// Id associated with Area that we would like to filter
        /// Equipment list by
        /// </summary>
        [DisplayName("Area")]
        public int AreaId { get; set; }

        /// <summary>
        /// Id associated with Equipment to search
        /// </summary>
        [DisplayName("Equipment")]
        public int EquipmentId { get; set; }

        /// <summary>
        /// Used to Model data in View
        /// </summary>
        public List<DowntimeIssueViewModel> downtimeIssuesVM;


        internal void Query()
        {
             List<DowntimeIssueModel> downtimeIssues;
            if (this.SearchDate && SearchEquipment)
            {
                downtimeIssues = MaintenanceLibrary.BusinessLogic.DowntimeIssuesProcessor
                    .GetDowntimeIssuesBy_StartDate_EndDate_EquipmentId(StartDate, EndDate, EquipmentId);
            }
            else if (SearchDate && !SearchEquipment)
            {
                downtimeIssues = MaintenanceLibrary.BusinessLogic.DowntimeIssuesProcessor
                    .GetDowntimeIssuesBy_StartDate_EndDate(StartDate, EndDate);
            }
            else if (!SearchDate && SearchEquipment)
            {
                downtimeIssues = MaintenanceLibrary.BusinessLogic.DowntimeIssuesProcessor
                    .GetDowntimeIssuesByEquipmentId(EquipmentId);
            }
            else
            {
                downtimeIssues = MaintenanceLibrary.BusinessLogic.DowntimeIssuesProcessor
                    .GetDowntimeIssues();
            }
            downtimeIssuesVM = new List<DowntimeIssueViewModel>();

            foreach (var downtimeIssue in downtimeIssues)
            {
                downtimeIssuesVM.Add(downtimeIssue);                
            }

        }
    }
}
