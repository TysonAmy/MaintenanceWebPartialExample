using Dapper;
using MaintenanceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceLibrary.BusinessLogic
{
    /// <summary>
    /// Used to retrieve <see cref="AppUserModel"/> model(s) from the database.
    /// </summary>
    public class AppUserProcessor
    {
        /// <summary>
        /// Queries the database for all supervisors and users that have been part of a follow up and the number of open and 
        /// closed supervisor's notes.
        /// </summary>
        /// <returns>A list of <see cref="AppUserModel"/>s and the number of Open and Close Supervisor's Note.</returns>
        public static List<AppUserModel> GetEmployeesSupervisorsNoteOpenClosed()
        {
            return getListOfEmployeesWithSupervisorsNoteOpenClosed("Exec GetAppUsersWithSupervisorsNotesOpenClosed");
        }

        /// <summary>
        /// Queries the database an Employee's Information.
        /// </summary>
        /// <param name="employeeId">Id representing an Employee.</param>
        /// <returns>A <see cref="AppUserModel"/> that represents the employee with the employeeid</returns>
        public static AppUserModel GetEmployeeById(string employeeId)
        {
            return getEmployee("Exec GetAppUserWithShiftAndSupervisorByEmployeeId @EmployeeId", new { EmployeeId = employeeId });
        }

        /// <summary>
        /// Queries for all Employees that have a supervisor id of supervisorId
        /// </summary>
        /// <param name="supervisorId">Id the represents the a Supervisor</param>
        /// <returns>A list of <see cref="AppUserModel"/>s that have a matching Supervisor Id</returns>
        public static List<AppUserModel> GetEmployeeBySupervisorId(int supervisorId)
        {
            return getListOfEmployees("Exec [GetAppUsersWithShiftAndSupervisorBySupervisorId] @SupervisorId", new { SupervisorId = supervisorId });
        }

        /// <summary>
        /// Queries for all Current Employees
        /// </summary>
        /// <returns>A List of <see cref="AppUserModel"/>s have an active status or active field is null.</returns>
        public static List<AppUserModel> GetAllCurrentEmployeesWithShiftAndSupervisor()
        {
            return getListOfEmployees("Exec [GetAppUsersWithShiftAndSupervisor]");
        }

        /// <summary>
        /// Queries for all Current Employees
        /// </summary>
        /// <returns>A List of <see cref="AppUserModel"/>s have an active status or active field is null.</returns>
        public static List<AppUserModel> GetAllCurrentEmployees()
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                return cnn.Query<AppUserModel>("Exec [GetAppUsers]").ToList<AppUserModel>();
            }

        }

        /// <summary>
        /// Returns all current employees that have one of 3 roles.
        /// </summary>
        /// <param name="RoleName1">A <see cref="string"/> name of the 1st Role searching</param>
        /// <param name="RoleName2">A <see cref="string"/> name of the 2nd Role searching</param>
        /// <param name="RoleName3">A <see cref="string"/> name of the 3rd Role searching</param>
        /// <returns></returns>
        public static List<AppUserModel> GetAllCurrentEmployeesByRoleName(string RoleName1, string RoleName2 = "", string RoleName3 = "")
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                return cnn.Query<AppUserModel>("Exec [GetAppUsersByRoleName] @RoleName1, @RoleName2, @RoleName3", new { RoleName1 = RoleName1, RoleName2 = RoleName2, RoleName3 = RoleName3 }).ToList<AppUserModel>();
            }
        }

        /// <summary>
        /// Query for all Inactive Employees
        /// </summary>
        /// <returns>A list of <see cref="AppUserModel"/>s whose active field is false.</returns>
        public static List<AppUserModel> GetAllTerminatedEmployees()
        {
            return getListOfEmployees("Exec [GetInActiveAppUsersWithShiftAndSupervisor]");
        }

        /// <summary>
        /// Query for All Current Employees on a partical shift.
        /// </summary>
        /// <param name="shiftId">Id representing a shift</param>
        /// <returns>A list of <see cref="AppUserModel"/>s that have the matching shift id.</returns>
        public static List<AppUserModel> GetAllCurrentEmployeesByShiftId(int shiftId)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                return cnn.Query<AppUserModel>("Exec [GetAppUsersByShiftId] @ShiftId", new { ShiftId = shiftId }).ToList<AppUserModel>();
            }
        }



        /*************************************
         * PRIVATE
         * **********************************/

        private static List<AppUserModel> getListOfEmployeesWithSupervisorsNoteOpenClosed(string SQL, object param = null)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                
                var a = cnn.Query<AppUserModel>( SQL
                , param: param
                ).ToList<AppUserModel>();
                return a;
            }

        }

        private static List<AppUserModel> getListOfEmployees(string SQL, object param = null)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                var remainingEmployees = new Dictionary<int, AppUserModel>();

                var a = cnn.Query<AppUserModel, ShiftModel, AppUserModel, AppUserModel>(SQL
                    , GetAppUserModelAndChildren()
                , param: param
                , splitOn: "SID, SUID").ToList<AppUserModel>();
                return a;
            }

        }

        private static Func<AppUserModel, ShiftModel, AppUserModel, AppUserModel> GetAppUserModelAndChildren()
        {
            return (employee, shift, supervisor) =>
            {
                AppUserModel appUserEntity;
                appUserEntity = employee;
                appUserEntity.Shift = shift;
                if (employee.SupervisorId is null)
                {
                    appUserEntity.Supervisor = null;
                }
                else
                {
                    appUserEntity.Supervisor = supervisor;
                }
                return appUserEntity;
            };
        }

        private static AppUserModel getEmployee(string SQL, object param = null)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                return cnn.Query<AppUserModel, ShiftModel, AppUserModel, AppUserModel>(SQL
                    , GetAppUserModelAndChildren()
                , param: param
                , splitOn: "SID, SUID").Single<AppUserModel>();
            }

        }

    }
}
