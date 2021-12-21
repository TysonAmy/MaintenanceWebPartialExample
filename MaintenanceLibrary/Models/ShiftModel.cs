
using System.Collections.Generic;
using System.ComponentModel;

namespace MaintenanceLibrary.Models
{
    public class ShiftModel
    {
        /// <summary>
        /// Id representing shift
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Initials of Shift
        /// </summary>
        public string Initials { get; set; }

        /// <summary>
        /// Name of Shift
        /// </summary>
        [DisplayName("Shift Name")]
        public string Name { get; set; }

        /// <summary>
        /// List of <see cref="AppUserModel"/> that are current Employees in that shift.
        /// </summary>
        public List<AppUserModel> Employees { get; set; }
    }
}
