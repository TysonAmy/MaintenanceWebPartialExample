using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceLibrary.Models
{
    public class SupervisorsNoteModel
    {
        /// <summary>
        /// Id representing Supervisor's Note
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> Supervisor's Note was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// <see cref="string"/> Id of AppUser(Employee) that created Supervisor's Note
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// <see cref="AppUserModel"/> that represents employee that created Supervisor's Note
        /// </summary>
        public AppUserModel Employee { get; set; }

        /// <summary>
        /// Id of the Equipment Supervisor's Note is about.
        /// </summary>
        public int EquipmentId { get; set; }

        /// <summary>
        /// <see cref="EquipmentModel"/> representing Supervisor's Note is about.
        /// </summary>
        public EquipmentModel Equipment { get; set; }

        /// <summary>
        /// Id that represents Area Equipment associated with this Supervisor's Note is located.
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// <see cref="AreaModel"/> that represents Area Equipment associated with this Supervisor's
        /// Note is located.
        /// </summary>
        public AreaModel Area { get; set; }

        /// <summary>
        /// Issue that Supervisor's Note is addressing.
        /// </summary>
        public string Issue { get; set; }

        /// <summary>
        /// List of Follow ups for this Supervisor's Note
        /// </summary>
        public List<SupervisorsNotes_FollowUpModel> SupervisorsNote_Followups { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> Supervisor's Note Follow Ups were completed.
        /// </summary>
        public DateTime Completed { get; set; }
    }
}
