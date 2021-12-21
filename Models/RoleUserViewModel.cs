using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Models
{
    /// <summary>
    /// Used to help with user switching roles
    /// </summary>
    public class RoleUserViewModel
    {

        public int Id { get; set; } // Id is required to create a ViewModel, but this id is not used.

        /// <summary>
        /// New Role Name
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Id representing User whose role is being modified
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Email address of User whose role is being modified.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Full Name of User whose role is being modified
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Represents Old Role of User that will need to be removed.
        /// </summary>
        public string OldRoleName { get; set; }

        /// <summary>
        /// A <see cref="AppUser"/> representing the User whose role is being modified.
        /// </summary>
        public AppUser User { get; set; }
    }
}
