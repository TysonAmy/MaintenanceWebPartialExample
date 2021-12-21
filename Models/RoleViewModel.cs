using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Models
{
    public class RoleViewModel
    {
        /// <summary>
        /// Id representing the Role
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the Role
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of <see cref="AppUser"/> representing Users who have that role
        /// </summary>
        public IList<AppUser> Users { get; set; }
    }
}
