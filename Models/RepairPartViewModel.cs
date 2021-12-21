using MaintenanceLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Models
{
    public class RepairPartViewModel
    {
        public static implicit operator RepairPartViewModel(RepairPartModel repairPartIssue)
        {

            return new RepairPartViewModel
            {
                Id = repairPartIssue.Id,
                VendorId = repairPartIssue.VendorId,
                Description = repairPartIssue.Description,
                CreatedDate = repairPartIssue.CreatedDate,
                PromiseDate = repairPartIssue.PromiseDate,
                SAPPartNum = repairPartIssue.SAPPartNum,
                DateCompleted = repairPartIssue.DateCompleted,
                RMANum = repairPartIssue.RMANum,
                Vendor = repairPartIssue.Vendor
            };
        }
        public static implicit operator RepairPartModel(RepairPartViewModel repairPartsViewModel)
        {

            return new RepairPartModel
            {
                Id = repairPartsViewModel.Id,
                CreatedDate = repairPartsViewModel.CreatedDate,
                PromiseDate = repairPartsViewModel.PromiseDate,
                SAPPartNum = repairPartsViewModel.SAPPartNum,
                Description = repairPartsViewModel.Description,
                RMANum = repairPartsViewModel.RMANum,
                DateCompleted = repairPartsViewModel.DateCompleted,
                VendorId = repairPartsViewModel.VendorId
            };
        }


        public int Id { get; set; }

        [Required]
        [DisplayName("Vendor")]
        public string VendorId { get; set; }

        public VendorModel Vendor { get; set; }

        [Required]
        [DisplayName("RMA Number")]
        public string RMANum { get; set; }

        [Required]
        [DisplayName("Promise Date")]
        [DataType(DataType.Date)]
        public DateTime PromiseDate { get; set; }

        [DisplayName("Date Created")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("SAP Part Number")]
        public string SAPPartNum { get; set; }

        [DisplayName("Date Completed")]
        [DataType(DataType.DateTime)]
        public DateTime? DateCompleted { get; set; }
    }
}
