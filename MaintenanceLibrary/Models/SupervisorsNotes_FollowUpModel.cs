using System;

namespace MaintenanceLibrary.Models
{
    public class SupervisorsNotes_FollowUpModel
    {
        /// <summary>
        /// Id that represents the Supervisor's Note Follow Up
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id that represents the Supervisor's Note that the Follow Up is about.
        /// </summary>
        public int SupervisorsNoteId { get; set; }

        /// <summary>
        /// <see cref="SupervisorsNoteModel"/> that represents the Supervisor's Noet that the Follow Up 
        /// is about
        /// </summary>
        public SupervisorsNoteModel SupervisorsNote { get; set; }

        /// <summary>
        /// Id of the AppUser(Employee) that created this follow up
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// <see cref="AppUserModel"/> that represents the AppUser(Employee) that created this follow up.
        /// </summary>
        public AppUserModel Employee { get; set; }

        /// <summary>
        /// Comment that Supervisor that is following up leaves
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> Supervisor's Note Follow Up was Passed down
        /// </summary>
        public DateTime? PassDown { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> Supervisor's Note Follow Up was completed
        /// </summary>
        public DateTime? Completed { get; set; }
    }
}