using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceLibrary.BusinessLogic
{
    public static class OtherProcessor
    {
        /// <summary>
        /// Gets number of Follow Ups (Supervisor's Note and Downtime Issue) that are associated with the
        /// Equipment, and the Supervisor's Note or Downtime issue are not complete
        /// </summary>
        /// <param name="equipmentId">A Id associated with the Equipment</param>
        /// <returns>A <see cref="int"/> with number of active follow ups associated with the Equipment</returns>
        public static int TotalOutstandingFollowUpsByEquipmentId(int equipmentId )
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                return cnn.Query<int>("Exec [GetCurrentFollowNumberOfActiveFollowupsByEquipmentId] @EquipmentId", new { EquipmentId = equipmentId }).First();
            }

        }
    
        /// <summary>
        /// Gets number of Follow Ups (Supervisor's Note and Downtime issue) that are associated with the 
        /// Employee, and the Supervisor's Note or Downtime Issue are not complete.
        /// </summary>
        /// <param name="employeeId">A Id associated with the Employee</param>
        /// <returns>A <see cref="int"/> with number of active follow ups with the Employee</returns>
        public static int TotalOutstandingFollowUpsByEmployeeId(string employeeId)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                return cnn.Query<int>("Exec [GetCurrentFollowNumberOfActiveFollowupsByEmployeesId] @EmployeeId", new { EmployeeId = employeeId }).First();
            }

        }
    }
}
