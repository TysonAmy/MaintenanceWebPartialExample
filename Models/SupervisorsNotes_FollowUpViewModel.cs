using MaintenanceLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Models
{
    public class SupervisorsNotes_FollowUpViewModel
    {
        /// <summary>
        /// Converts <see cref="SupervisorsNotes_FollowUpModel"/> to <see cref="SupervisorsNotes_FollowUpViewModel"/>
        /// So Model coming from the database can be converted into the ViewModel which can be displayed in the view.
        /// </summary>
        /// <param name="supervisorsNotes_FollowUp">A <see cref="SupervisorsNotes_FollowUpModel"/> that is used to represent 
        /// Supervisor's Note Follow up from the database</param>
        public static implicit operator SupervisorsNotes_FollowUpViewModel(SupervisorsNotes_FollowUpModel supervisorsNotes_FollowUp)
        {

            return new SupervisorsNotes_FollowUpViewModel
            {
                Id = supervisorsNotes_FollowUp.Id,
                Comment = supervisorsNotes_FollowUp.Comment,
                Completed = supervisorsNotes_FollowUp.Completed,
                Employee = supervisorsNotes_FollowUp.Employee,
                EmployeeId = supervisorsNotes_FollowUp.EmployeeId,
                PassDown = supervisorsNotes_FollowUp.PassDown


            };
        }

        /// <summary>
        /// Converts <see cref="SupervisorsNotes_FollowUpViewModel"/> to <see cref="SupervisorsNotes_FollowUpModel"/>
        /// So ViewModel coming from the view can be converted into the Model which can be saved to the database
        /// </summary>
        /// <param name="supervisorNote_FollowUpViewModel"></param>
        public static implicit operator SupervisorsNotes_FollowUpModel(SupervisorsNotes_FollowUpViewModel supervisorNote_FollowUpViewModel)
        {

            return new SupervisorsNotes_FollowUpModel
            {
                Id = supervisorNote_FollowUpViewModel.Id,
                Comment = supervisorNote_FollowUpViewModel.Comment,
                Completed = supervisorNote_FollowUpViewModel.Completed,
                EmployeeId = supervisorNote_FollowUpViewModel.EmployeeId,
                PassDown = supervisorNote_FollowUpViewModel.PassDown,
                SupervisorsNoteId = supervisorNote_FollowUpViewModel.SupervisorsNoteId
                

            };
        }

        /// <summary>
        /// Id representing the Supervisor's Note Follow Up
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id represent the Supervisor's Note this is for
        /// </summary>
        public int SupervisorsNoteId { get; set; }

        /// <summary>
        /// Id representing the Employee who it receiving this follow up
        /// </summary>
        [DisplayName("Employee Following up with")]
        public string EmployeeId { get; set; }

        /// <summary>
        /// A <see cref="EmployeeViewModel"/> representing the employee who it receiving this follow up
        /// </summary>
        public EmployeeViewModel Employee { get; set; }

        /// <summary>
        /// Comment from the Supervisor who received this follow up
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// If exist the date that follow up was passdown to different supervisor
        /// </summary>
        public DateTime? PassDown { get; set; }

        /// <summary>
        /// If exist the <see cref="DateTime"/> that follow up was completed.
        /// This information is also recorded on Supervisor's Note
        /// </summary>
        public DateTime? Completed { get; set; }

        /// <summary>
        /// Used to identify this is the last entry(1) or a entry was done before that.
        /// </summary>
        public int ListNumber { get; set; } // Order the followups were put in, starting at 1 being the first.

    }
}
