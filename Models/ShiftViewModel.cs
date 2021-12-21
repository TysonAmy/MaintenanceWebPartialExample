using MaintenanceLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Models
{
    /// <summary>
    /// Represents the Shift data in a view
    /// </summary>
    public class ShiftViewModel
    {
        /// <summary>
        /// Converts a <see cref="ShiftModel"/> to <see cref="ShiftViewModel"/>
        /// So Model coming from the database can be converted into the ViewModel which can be displayed in the view.
        /// </summary>
        /// <param name="shift">Represents <see cref="ShiftModel"/> Shift data coming from the database</param>
        public static implicit operator ShiftViewModel(ShiftModel shift)
        {
            return new ShiftViewModel
            {
                Id = shift.Id,
                Initials = shift.Initials,
                Name = shift.Name
            };
        }

        /// <summary>
        /// Converts a <see cref="ShiftViewModel"/> to <see cref="ShiftModel"/>
        /// So ViewModel coming from the view can be converted into the Model which can be saved to the database
        /// </summary>
        /// <param name="shift">A <see cref="ShiftViewModel"/> is returned from the view.</param>
        public static implicit operator ShiftModel(ShiftViewModel shift)
        {
            return new ShiftModel
            {
                Id = shift.Id,
                Name = shift.Name,
                Initials = shift.Initials
            };
        }

        /// <summary>
        /// Id representing the shift
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// A shorten name for the shift.
        /// As of 12-14-2021, this is not being used, except in the initial setup and edit of Shift
        /// </summary>
        [DisplayName("Shift Initials")]
        public string Initials { get; set; }

        /// <summary>
        /// Name of the shift
        /// </summary>
        [DisplayName("Shift Name")]
        public string Name { get; set; }

    }
}
