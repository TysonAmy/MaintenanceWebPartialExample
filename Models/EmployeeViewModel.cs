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
    /// <summary>
    /// Used to model data that will be displayed in a view
    /// </summary>
    public class EmployeeViewModel
    {
        /// <summary>
        /// Used to convert a <see cref="AppUserModel"/> to at <see cref="EmployeeViewModel"/>
        /// So Model coming from the database can be converted into the ViewModel which can be displayed in the view.
        /// </summary>
        /// <param name="appUserModel">A <see cref="AppUserModel"/> that represents a user coming from the database</param>
        public static implicit operator EmployeeViewModel(AppUserModel appUserModel)
        {
            return new EmployeeViewModel
            {
                EmailAddress = appUserModel.Email,
                FirstName = appUserModel.FirstName,
                Id = appUserModel.Id,
                LastName = appUserModel.LastName,
                Shift = appUserModel.Shift,
                ShiftId = appUserModel.ShiftId,
                Supervisor = appUserModel.Supervisor,
                SupervisorId = appUserModel.SupervisorId,
                IsEnabled = appUserModel.IsEnabled,
                Open = appUserModel.Open,
                Closed = appUserModel.Closed
            };
        }

        /// <summary>
        /// Used to convert a <see cref="EmployeeViewModel"/> to <see cref="AppUserModel"/>
        /// So ViewModel coming from the view can be converted into the Model which can be saved to the database
        /// </summary>
        /// <param name="employeeViewModel">A <see cref="EmployeeViewModel"/> that represent user information
        /// coming from a view</param>
        public static implicit operator AppUserModel(EmployeeViewModel employeeViewModel)
        {
            return new AppUserModel
            {
                Email = employeeViewModel.EmailAddress,
                FirstName = employeeViewModel.FirstName,
                Id = employeeViewModel.Id,
                LastName = employeeViewModel.LastName,
                Shift = employeeViewModel.Shift,
                ShiftId = employeeViewModel.ShiftId,
                Supervisor = employeeViewModel.Supervisor,
                SupervisorId = employeeViewModel.SupervisorId,
                IsEnabled = employeeViewModel.IsEnabled
            };
        }


        /// <summary>
        /// <see cref="string"/> Id representing the Employee.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Employee's First Name
        /// </summary>
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Employee's Last Name
        /// </summary>
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Id of Shift Employee is on
        /// </summary>
        [Required]
        [DisplayName("Shift")]
        public int ShiftId { get; set; }

        /// <summary>
        /// A <see cref="ShiftModel"/> representing the shift employee is on.
        /// </summary>
        public ShiftModel Shift { get; set; }

        /// <summary>
        /// <see cref="string"/> Id of Supervisor of the employee
        /// </summary>
        [DisplayName("Supervisor")]
        public string? SupervisorId { get; set; }

        /// <summary>
        /// <see cref="AppUserModel"/> representing Employee's Supervisor if they have one.
        /// </summary>
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        [DisplayName("Supervisor's Name")]
        public AppUserModel? Supervisor { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    
        /// <summary>
        /// Email of Employee
        /// </summary>
        [Required]
        [EmailAddress]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// How many Supervisor Note's with follow up associated with employee, that are not complete.
        /// </summary>
        [DisplayName("Open")]
        public int Open { get; set; }

        /// <summary>
        /// How many Supervisor Note's with follow up associated with employee, that are complete.
        /// </summary>
        [DisplayName("Closed")]
        public int Closed { get; set; }

        /// <summary>
        /// Is currently a Employee
        /// </summary>
        [DisplayName("Is Enabled")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Full Name of Employee
        /// </summary>
        [DisplayName("Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

    }
}
