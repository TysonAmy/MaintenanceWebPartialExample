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
    public static class DowntimeIssuesProcessor
    {
        /// <summary>
        /// Finds downtime issues by Equipment Id
        /// </summary>
        /// <param name="equipmentId">Id that represents a piece of equipment</param>
        /// <returns>A List of <see cref="DowntimeIssueModel"/> with matching equipment id</returns>
        public static List<DowntimeIssueModel> GetDowntimeIssuesByEquipmentId(int equipmentId)
        {
            return GetDowntimeIssue("exec [GetDowntimeIssuesByEquipmentId] @EquipmentId", new { EquipmentId = equipmentId }).ToList<DowntimeIssueModel>();
        }

        /// <summary>
        /// Finds downtime issues between a start and end datetime
        /// </summary>
        /// <param name="startDate"><see cref="DateTime"/> representing the start date</param>
        /// <param name="endDate"><see cref="DateTime"/> representing the end date</param>
        /// <returns></returns>
        public static List<DowntimeIssueModel> GetDowntimeIssuesBy_StartDate_EndDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0);
                endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
                return GetDowntimeIssue("exec [GetDowntimeIssuesBy_StartDate_EndDate] @StartDate, @EndDate", new { StartDate = startDate, EndDate = endDate}).ToList<DowntimeIssueModel>();
            }
            catch (ArgumentNullException a)
            {
                return null;
            }
        }

        /// <summary>
        /// Find downtime issues between the start and end datetime and with a equipment id
        /// </summary>
        /// <param name="startDate"><see cref="DateTime"/> representing the start date</param>
        /// <param name="endDate"><see cref="DateTime"/> representing the end date</param>
        /// <param name="equipmentId">Id that represents a piece of equipment</param>
        /// <returns>A List of <see cref="DowntimeIssueModel"/> between start and end date and has equipment id</returns>
        public static List<DowntimeIssueModel> GetDowntimeIssuesBy_StartDate_EndDate_EquipmentId(DateTime startDate, DateTime endDate, int equipmentId)
        {
            try
            {
                startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0);
                endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
                return GetDowntimeIssue("exec [GetDowntimeIssuesBy_StartDate_EndDate_EquipmentId] @StartDate, @EndDate, @EquipmentId", new { StartDate = startDate, EndDate = endDate, EquipmentId = equipmentId }).ToList<DowntimeIssueModel>();
            }
            catch (ArgumentNullException a)
            {
                return null;
            }
        }

        /// <summary>
        /// Find downtime issue by downtime issue id
        /// </summary>
        /// <param name="downtimeIssueId">Id that represent a downtime issue</param>
        /// <returns>A <see cref="DowntimeIssueModel"/> that has a matching downtime issue id</returns>
        public static DowntimeIssueModel GetDowntimeIssueByDowntimeIssueId(int downtimeIssueId)
        {
            try
            {
                return GetDowntimeIssue("exec [GetDowntimeIssueWithDowntimeIssue_FollowupByDowntimeIssue] @DowntimeIssueId", new { DowntimeIssueId = downtimeIssueId }).First<DowntimeIssueModel>();
            }
            catch (ArgumentNullException a)
            {
                return null;
            }
        }

        /// <summary>
        /// Get all Downtime issues with followups associated with them.
        /// </summary>
        /// <returns>A List of all <see cref="DowntimeIssueModel"/></returns>
        public static List<DowntimeIssueModel> GetDowntimeIssues()
        {
            return GetDowntimeIssue("exec [GetDowntimeIssueWithDowntimeIssue_Followup]").Distinct<DowntimeIssueModel>().ToList<DowntimeIssueModel>();
        }

        /// <summary>
        /// Show all downtime issues which have a follow up with an employeeId
        /// </summary>
        /// <param name="employeeId">A Id <see cref="string"/> associated with an employee</param>
        /// <returns>A List of <see cref="DowntimeIssueModel"/> where at least one follow up is assocatied with the employee</returns>
        public static List<DowntimeIssueModel> GetDowntimeIssuesByEmployeeId(string employeeId)
        {
            return GetDowntimeIssue("Exec [GetDowntimeIssueWithDowntimeIssue_FollowupByEmployeeId] @EmployeeId ", new { EmployeeId = employeeId }).Distinct<DowntimeIssueModel>().ToList<DowntimeIssueModel>();
        }

        /// <summary>
        /// Inserts downtime issue
        /// </summary>
        /// <param name="downtimeIssue">A <see cref="DowntimeIssueModel"/> model that represents
        /// the downtime issue being inserted.</param>
        /// <returns>The newly created Id associated with the inserted downtime issue.</returns>
        public static int Insert(DowntimeIssueModel downtimeIssue)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                return cnn.QueryFirst<int>("[InsertDowntimeIssue] @EmployeeId, @EquipmentId, @IssueResolution, @DownTime", 
                    new { 
                        EmployeeId = downtimeIssue.EmployeeId,
                        EquipmentId = downtimeIssue.EquipmentId,
                        IssueResolution = downtimeIssue.IssueResolution,
                        DownTime = downtimeIssue.DownTime
                    });
            }

        }

        /// <summary>
        /// Updates downtime issue
        /// </summary>
        /// <param name="downtimeIssue">A <see cref="DowntimeIssueModel"/> that represents the downtime
        /// issue being updated</param>
        public static void Update(DowntimeIssueModel downtimeIssue)
        {
                using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
                {
                    cnn.Execute("[UpdateDowntimeIssues] @Id, @EmployeeId, @EquipmentId, @IssueResolution, @DownTime, @Completed",
                        new
                        {
                            Id = downtimeIssue.Id,
                            EmployeeId = downtimeIssue.EmployeeId,
                            EquipmentId = downtimeIssue.EquipmentId,
                            IssueResolution = downtimeIssue.IssueResolution,
                            DownTime = downtimeIssue.DownTime,
                            Completed = downtimeIssue.Completed
                        });
                }

        }

        /// <summary>
        /// Deletes downtime issue, if downtime issue doesn't have a follow up.
        /// </summary>
        /// <param name="downtimeIssue">A <see cref="DowntimeIssueModel"/> that represents the downtime issue 
        /// being deleted</param>
        /// <returns>A <see cref="string"/> "Success" if successful or error message if not.</returns>
        public static string Delete(DowntimeIssueModel downtimeIssue)
        {
            var downtimeIssues_FollowupProcessor = DowntimeIssues_FollowupProcessor.GetDowntimeIssue_Followup_LastFollowup_ByDowntimeIssueId(downtimeIssue.Id);
            if (downtimeIssues_FollowupProcessor.DowntimeIssueId == 0)
            {
                Delete(downtimeIssue.Id);
                return "Success";
            }
            else
            {
                return "You can't delete Downtime Issue before all followups assocated with it have been deleted";
            }
        }

        /**********************************************
         * PRIVATE
         * *******************************************/
        private static void Delete(int Id)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                cnn.Execute("[DeleteDowntimeIssue] @Id",
                    new
                    {
                        Id = Id,
                    });
            }

        }

        private static Func<DowntimeIssueModel, DowntimeIssue_FollowupModel, AppUserModel, EquipmentModel, AreaModel, AppUserModel, DowntimeIssueModel> GetDowntimeIssue()
        {
            var remainingDowntimeIssues = new Dictionary<int, DowntimeIssueModel>();
            var remainingDowntimeIssues_FollowUps = new Dictionary<int, DowntimeIssueModel>();

            return (downtimeIssue, downtimeIssue_Followup, repairPerson, equipment, area, followingUpEmployee) =>
            {
                // DowntimeIssue
                DowntimeIssueModel DowntimeIssueEntity;
                if(!remainingDowntimeIssues.TryGetValue(downtimeIssue.Id,out DowntimeIssueEntity))
                {
                    remainingDowntimeIssues.Add(downtimeIssue.Id, DowntimeIssueEntity = downtimeIssue);
                }

                DowntimeIssueEntity.Employee = repairPerson;
                // downtimeIssuefollowup
                if(downtimeIssue_Followup.Id != 0)
                {
                    if (DowntimeIssueEntity.DowntimeIssue_Followups is null)
                        DowntimeIssueEntity.DowntimeIssue_Followups = new List<DowntimeIssue_FollowupModel>();
                    DowntimeIssueEntity.DowntimeIssue_Followups.Add(downtimeIssue_Followup);
                    downtimeIssue_Followup.Employee = followingUpEmployee;
                }

                DowntimeIssueEntity.Equipment = equipment;
                equipment.Area = area;


                return DowntimeIssueEntity;
            };
        }

        private static IEnumerable<DowntimeIssueModel> GetDowntimeIssue(string SQL, object param = null)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {

                return cnn.Query<DowntimeIssueModel, DowntimeIssue_FollowupModel, AppUserModel, EquipmentModel, AreaModel, AppUserModel, DowntimeIssueModel>(SQL
                    , GetDowntimeIssue()
                , param: param
                , splitOn: "DowntimeIssues_Followups, RepairPerson, Equipment, Areas, FollowingUpEmployee");
            }

        }



    }
}
