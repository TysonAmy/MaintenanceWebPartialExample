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
    public class SupervisorsNoteViewModel
    {
        /// <summary>
        /// Converts <see cref="SupervisorsNoteModel"/> to <see cref="SupervisorsNoteViewModel"/>
        /// So Model coming from the database can be converted into the ViewModel which can be displayed in the view.
        /// </summary>
        /// <param name="supervisorsNote">A <see cref="SupervisorsNoteModel"/> that is used to represent 
        /// Supervisor's Note from the database</param>
        public static implicit operator SupervisorsNoteViewModel(SupervisorsNoteModel supervisorsNote)
        {
            List<SupervisorsNotes_FollowUpViewModel> supervisorsNote_Followups = new List<SupervisorsNotes_FollowUpViewModel>();
            int orderOfFollowups = 1;
            foreach (SupervisorsNotes_FollowUpViewModel SupervisorsNote_Followup in supervisorsNote.SupervisorsNote_Followups)
            {
                SupervisorsNote_Followup.ListNumber = orderOfFollowups++; // set follow up number and go to next.
                supervisorsNote_Followups.Add(SupervisorsNote_Followup);
            }

            return new SupervisorsNoteViewModel
            {
                Id = supervisorsNote.Id,
                Area = supervisorsNote.Area,
                AreaId = supervisorsNote.AreaId,
                DateCreated = supervisorsNote.DateCreated,
                Employee = supervisorsNote.Employee,
                EmployeeId = supervisorsNote.EmployeeId,
                Equipment = supervisorsNote.Equipment,
                EquipmentId = supervisorsNote.EquipmentId,
                Issue = supervisorsNote.Issue,
                SupervisorsNote_Followups = supervisorsNote_Followups,
                Completed = supervisorsNote.Completed


            };
        }

        /// <summary>
        /// Converts <see cref="SupervisorsNoteViewModel"/> to <see cref="SupervisorsNoteModel"/>
        /// So ViewModel coming from the view can be converted into the Model which can be saved to the database
        /// </summary>
        /// <param name="supervisorNoteViewModel">A <see cref="SupervisorsNoteViewModel"/> that is used to display 
        /// Supervisor's Note data in view</param>
        public static implicit operator SupervisorsNoteModel(SupervisorsNoteViewModel supervisorNoteViewModel)
        {
            return new SupervisorsNoteModel
            {
                Id = supervisorNoteViewModel.Id,
                DateCreated = supervisorNoteViewModel.DateCreated,
                EquipmentId = supervisorNoteViewModel.EquipmentId,
                EmployeeId = supervisorNoteViewModel.EmployeeId,
                Issue = supervisorNoteViewModel.Issue,
                Completed = supervisorNoteViewModel.Completed
            };
        }

        /// <summary>
        /// Id assocated with Supervisor's Note
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> Supervisor's Note was created
        /// </summary>
        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Id of Employee who created this Supervisor's Note
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// A <see cref="EmployeeViewModel"/> that represents the employee who created this Supervisor's Note
        /// </summary>
        public EmployeeViewModel Employee { get; set; }

        /// <summary>
        /// The Id of the Equipment that is associated with the Supervisor's Note
        /// </summary>
        [Required]
        public int EquipmentId { get; set; }

        /// <summary>
        /// The <see cref="EquipmentViewModel"/> that represents the Equipment associated with the Supervisor's Note
        /// </summary>
        public EquipmentViewModel Equipment { get; set; }

        /// <summary>
        /// The Id of the Area where the Equipment, associated this this Supervisor's Note, is located
        /// </summary>
        [Required]
        public int AreaId { get; set; }

        /// <summary>
        /// The <see cref="AreaViewModel"/> the represents the Area where the Equipment, associated with this 
        /// Supervisor's Note, is located.
        /// </summary>
        public AreaViewModel Area { get; set; }

        /// <summary>
        /// Issue that was raised in the Supervisor's note
        /// </summary>
        [Required]
        public string Issue { get; set; }


        /// <summary>
        /// Used to tell if the first Supervisor's Note follow up should be created at the same time Supervisor's Note is
        /// created
        /// ONLY true if first Supervisor's Note follow up should be created the same time Supervisor's Note is
        /// created.
        /// </summary>
        [DisplayName("Has Follow Up")]
        public bool HasFollowUp { get; set; }

        /// <summary>
        /// <see cref="string"/> Id that represents User to be followed up with
        /// ONLY used if HasFollowUp is true, therefore first follow up being created at the same time
        /// </summary>
        [DisplayName("Employee Following up with")]
        public string FollowUpEmployeeId { get; set; }

        /// <summary>
        /// Reason for first Supervisor's Note follow up
        /// ONLY used if HasFollowUp is true, therefore first follow up being created at the same time
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// List of Supervisor's Note Follow Ups that Supervisor's Note has
        /// </summary>
        public List<SupervisorsNotes_FollowUpViewModel> SupervisorsNote_Followups { get; set; }

        /// <summary>
        /// This is true if the submit button is pressed creating a Supervisor's Note
        /// </summary>
        public string Submit { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> last Follow Up was complete
        /// This data is also stored on the last Supervisor's Note Follow Up Completed Field
        /// </summary>
        public DateTime Completed { get; set; }
    }
}
