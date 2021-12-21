using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MaintenanceLibrary.Models
{
    public class RepairPartModel
    {
        /// <summary>
        /// Id that represences the repair that is being done.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Vendor Id of part being repaired
        /// </summary>
        public string VendorId { get; set; }

        /// <summary>
        /// Vendor of part being repaired
        /// </summary>
        public VendorModel Vendor { get; set; }

        /// <summary>
        /// RMA Number of part being repaired
        /// </summary>
        public string RMANum { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> date part was promised to be back
        /// </summary>
        public DateTime PromiseDate { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> repair part was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// SAP Part Number
        /// </summary>
        public string SAPPartNum { get; set; }

        /// <summary>
        /// Description of Item being repaired.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> Completed
        /// </summary>
        public DateTime? DateCompleted { get; set; }
    }
}
