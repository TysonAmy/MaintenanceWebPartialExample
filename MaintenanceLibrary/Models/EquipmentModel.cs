using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceLibrary.Models
{
    public class EquipmentModel
    {
        /// <summary>
        /// Id the represent the Equipment
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the Equipment
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id that represents what Area this Equipment is in.
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// <see cref="AreaModel"/> that represence the Area this Equipment is in.
        /// </summary>
        public AreaModel Area { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> equipment stopped being recorded in Web App, or null if still active.
        /// </summary>
        public DateTime? Deactivate { get; set; }
    }
}
