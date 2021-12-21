using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceLibrary.Models
{
    public class AreaModel
    {
        /// <summary>
        /// Id of Area
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of Area
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Area not active
        /// </summary>
        public DateTime? Deactivate { get; set; }

        /// <summary>
        /// List of <see cref="EquipmentModel"/> of active equipment in area.
        /// </summary>
        public List<EquipmentModel> Equipment { get; set; }

    }
}
