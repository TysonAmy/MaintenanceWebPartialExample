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
    public class RepairPartSearchViewModel
    {
        /// <summary>
        /// Initializes list for RepairParts
        /// </summary>
        public RepairPartSearchViewModel()
        {
            this.repairPartsVM = new List<RepairPartViewModel>();
        }

        /// <summary>
        /// <see cref="DateTime"/> to start search of repair parts.
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        /// <summary>
        /// <see cref="DateTime"/> to end search of repair parts.
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
        /// Whether to search for a RMANum 
        /// </summary>
        [DisplayName("Search RMA #")]
        public bool SearchRMANum { get; set; } = false;

        /// <summary>
        /// Whether to search for a RMANum 
        /// </summary>
        [DisplayName("Search Vendor")]
        public bool SearchVendorId { get; set; } = false;


        /// <summary>
        /// Used to populate Vendor Dropdown box
        /// </summary>
        public List<SelectListItem> Vendors { get; set; } = new();

        /// <summary>
        /// Id associated with Vendor to search
        /// </summary>
        [DisplayName("Vendor")]
        public int VendorId { get; set; }

        /// <summary>
        /// RMA # searching For
        /// </summary>
        [DisplayName("RMA #")]
        public string RMANum { get; set; }

        /// <summary>
        /// Used to Model data in View
        /// </summary>
        public List<RepairPartViewModel> repairPartsVM;

        internal void Query()
        {
            List<RepairPartModel> repairParts;
            if (this.SearchDate && SearchRMANum && SearchVendorId)
            {
                repairParts = MaintenanceLibrary.BusinessLogic.RepairPartsProcessor
                    .GetRepairPartsBy_StartDate_EndDate_RMANum_VendorId(StartDate, EndDate, RMANum, VendorId);
            }
            else if (this.SearchDate && SearchRMANum && !SearchVendorId)
            {
                repairParts = MaintenanceLibrary.BusinessLogic.RepairPartsProcessor
                    .GetRepairPartsBy_StartDate_EndDate_RMANum(StartDate, EndDate, RMANum);
            }
            else if (this.SearchDate && !SearchRMANum && SearchVendorId)
            {
                repairParts = MaintenanceLibrary.BusinessLogic.RepairPartsProcessor
                    .GetRepairPartsBy_StartDate_EndDate_VendorId(StartDate, EndDate, VendorId);
            }
            else if (this.SearchDate && !SearchRMANum && !SearchVendorId)
            {
                repairParts = MaintenanceLibrary.BusinessLogic.RepairPartsProcessor
                    .GetRepairPartsBy_StartDate_EndDate(StartDate, EndDate);
            }
            else if (!this.SearchDate && SearchRMANum && SearchVendorId)
            {
                repairParts = MaintenanceLibrary.BusinessLogic.RepairPartsProcessor
                    .GetRepairPartsBy_RMANum_VendorId(RMANum, VendorId);
            }
            else if (!this.SearchDate && SearchRMANum && !SearchVendorId)
            {
                repairParts = MaintenanceLibrary.BusinessLogic.RepairPartsProcessor
                    .GetRepairPartsBy_RMANum(RMANum);
            }
            else if (!this.SearchDate && !SearchRMANum && SearchVendorId)
            {
                repairParts = MaintenanceLibrary.BusinessLogic.RepairPartsProcessor
                    .GetRepairPartsBy_VendorId(VendorId);
            }
            else
            {
                repairParts = MaintenanceLibrary.BusinessLogic.RepairPartsProcessor
                    .GetRepairParts();
            }

            repairPartsVM = new List<RepairPartViewModel>();

            foreach (var repairPart in repairParts)
            {
                repairPartsVM.Add(repairPart);
            }

        }

    }
}
