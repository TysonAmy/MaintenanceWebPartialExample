using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Models
{
    /// <summary>
    /// Used to extend User in database
    /// </summary>
    public class AppUser : IdentityUser
    {
        /// <summary>
        /// Employee's Supervisor
        /// </summary>
        [DisplayName("User's Supervisor")]
        public string? SupervisorId { get; set; }

        /// <summary>
        /// Id that refers to the Shift
        /// </summary>
        [Required]
        [DisplayName("Shift")]
        public int ShiftId { get; set; }

        /// <summary>
        /// User is Enabled
        /// Note: Enabled you can have a value of true or null
        /// </summary>
        public bool? IsEnabled { get; set; }
 
        /// <summary>
        /// Users First Name
        /// </summary>
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Users Last Name
        /// </summary>
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Users Full Name
        /// </summary>
        [DisplayName("Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        /// <summary>
        /// User has reset there initial password
        /// </summary>
        public bool PasswordReset { get; set; }
    }
}
