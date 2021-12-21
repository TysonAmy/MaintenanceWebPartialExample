using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceLibrary.Models
{
    public class AppUserModel
    {
        /// <summary>
        /// Id representing AppUser(Employee)
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Username of AppUser(Employee)
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// First Name of AppUser(Employee)
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name of AppUser(Employee)
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Phone number of AppUser(Employee)
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Id of Shift AppUser(Employee) is on.
        /// </summary>
        public int ShiftId { get; set; }

        /// <summary>
        /// <see cref="ShiftModel"/> that represents Shift AppUser(Employee) is on
        /// </summary>
        public ShiftModel Shift { get; set; }

        /// <summary>
        /// Id of Supervisor of AppUser(Employee)
        /// </summary>
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? SupervisorId { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        /// <summary>
        /// <see cref="AppUserModel"/> that represents Supervisor of AppUser(Employee)
        /// </summary>
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public AppUserModel? Supervisor { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        /// <summary>
        /// Email of AppUser(Employee)
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// AppUser(Employee) is Active
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Full name of AppUser(Employee)
        /// </summary>
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }


        /// <summary>
        /// Number of Open Supervisor's Note Follow Up with associated Supervisor's Note that is not complete
        /// </summary>
        public int Open { get; set; }

        /// <summary>
        /// Number of Open Supervisor's Note Follow Up with associated Supervisor's Note that is complete
        /// </summary>
        public int Closed { get; set; }
    }
}
