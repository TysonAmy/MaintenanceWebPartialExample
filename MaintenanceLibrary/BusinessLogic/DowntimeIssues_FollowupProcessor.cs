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
    public class DowntimeIssues_FollowupProcessor
    {
        /// <summary>
        /// Queries for all follow ups that the refered downtime issue has.
        /// </summary>
        /// <param name="downtimeIssueId">Id representing the downtime issue</param>
        /// <returns>List of <see cref="DowntimeIssue_FollowupModel"/> that have downtime issue id</returns>
        public static List<DowntimeIssue_FollowupModel> GetDowntimeIssue_Followups_ByDowntimeIssueId(int downtimeIssueId)
        {
            try
            {
                return  GetDowntimeIssue_FollowUp("Exec  GetDowntimeIssue_Followups_ByDowntimeIssueId @DowntimeIssueId", new { DowntimeIssueId = downtimeIssueId }).ToList<DowntimeIssue_FollowupModel>();
                
            }
            catch (ArgumentNullException e)
            {
                return null;
            }
        }

        /// <summary>
        /// Queries for all follow ups, with <see cref="DowntimeIssueModel"/> referenced
        /// As of 12-10-2021 this is not used
        /// </summary>
        /// <param name="employeeId">Id of Employee</param>
        /// <returns>A List of <see cref="DowntimeIssue_FollowupModel"/> for represented employee</returns>
        public static List<DowntimeIssue_FollowupModel> GetDowntimeIssue_FollowUpsByEmployeeId(string employeeId)
        {
            return GetDowntimeIssue_FollowUp("[GetDowntimeIssue_FollowupByEmployeeId] @EmployeeId", new { EmployeeId = employeeId });
        }

        /// <summary>
        /// Gets the second to the last follow up or returns null
        /// Used to find follow up whose passdown or complete field needs to be updated.
        /// After the new follow up has been created.
        /// </summary>
        /// <param name="downtimeIssueId">Id for representing downtime issue whose follow ups are being queried for</param>
        /// <returns>A <see cref="DowntimeIssue_FollowupModel"/> representing the second to the last follow up, if none then null is returned</returns>
        public static DowntimeIssue_FollowupModel GetDowntimeIssue_Followup_LastDowntimeIssueToBeFollowedUp_ByDowntimeIssueId(int downtimeIssueId)
        {
            try
            {
                var downtimeIssue_Followups = GetDowntimeIssue_FollowUp("Exec  GetDowntimeIssue_Followup_LastDowntimeIssueToBeFollowedUp_ByDowntimeIssueId @DowntimeIssueId", new { DowntimeIssueId = downtimeIssueId }).ToList<DowntimeIssue_FollowupModel>();
                return downtimeIssue_Followups.Single<DowntimeIssue_FollowupModel>();
            }
            catch (ArgumentNullException e)
            {
                return null;
            }
        }


        /// <summary>
        /// Gets the last follow up added
        /// Used to find follow up whose passdown or complete field needs to be updated.
        /// Before the new follow up has been created.
        /// </summary>
        /// <param name="downtimeIssueId">Id representing the downtime issue whose follow up we are looking for.</param>
        /// <returns>A <see cref="DowntimeIssue_FollowupModel"/> that is the last follow up created, if none then null is returned.</returns>
        public static DowntimeIssue_FollowupModel GetDowntimeIssue_Followup_LastFollowup_ByDowntimeIssueId(int downtimeIssueId)
        {
            try
            {
                var downtimeIssue_Followups = GetDowntimeIssue_FollowUp("Exec [GetDowntimeIssue_Followup_LastFollowup_ByDowntimeIssueId] @DowntimeIssueId", new { DowntimeIssueId = downtimeIssueId }).ToList<DowntimeIssue_FollowupModel>();
                if (downtimeIssue_Followups.Count > 0)
                    return downtimeIssue_Followups.Single<DowntimeIssue_FollowupModel>();
                else
                    return null;
            }
            catch (ArgumentNullException e)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the <see cref="DowntimeIssue_FollowupModel"/> that has the id inputed.
        /// </summary>
        /// <param name="downtimeIssue_FollowupId">Id representing the Downtime Issue Followup</param>
        /// <returns>A <see cref="DowntimeIssue_FollowupModel"/> with the inputed id</returns>
        public static DowntimeIssue_FollowupModel getDowntimeIssue_FollowupByDowntimeIssue_FollowupId(int downtimeIssue_FollowupId)
        {
                var followups = GetDowntimeIssue_FollowUp("Exec [GetDowntimeIssue_Followup_ByDowntimeIssue_FollowUpId] @DowntimeIssue_FollowUpId", new { DowntimeIssue_FollowUpId = downtimeIssue_FollowupId });
                if (followups.Count > 0)
                    return followups.First<DowntimeIssue_FollowupModel>();
                else
                    return null;
        }

        /// <summary>
        /// Check if a downtime issue follow has an id of downtime issue id then updates the last downtime issue
        /// follow up with the datetime of when the passdown was completed.
        /// Inserts a new downtime issue follow up
        /// </summary>
        /// <param name="DowntimeIssue_Followup">A <see cref="DowntimeIssue_FollowupModel"/> representing the new 
        /// downtime issue follow up</param>
        public static void Insert(DowntimeIssue_FollowupModel DowntimeIssue_Followup)
        {
            DateTime followUpRequestTimeDate = DateTime.Now;
            DowntimeIssue_FollowupModel lastRequest = GetDowntimeIssue_Followup_LastFollowup_ByDowntimeIssueId(DowntimeIssue_Followup.DowntimeIssueId);
            if(lastRequest is not null)
            {
                lastRequest.SupervisorFollowUp = followUpRequestTimeDate;
                Update(lastRequest);
            }
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                cnn.Execute("[InsertDowntimeIssues_Followup] @DowntimeIssueId, @FollowUpRequest, @FollowingUpReason, @SupervisorComments, @EmployeeId, @SupervisorFollowUp",
                    new {
                        DowntimeIssueId = DowntimeIssue_Followup.DowntimeIssueId,
                        FollowUpRequest = followUpRequestTimeDate,
                        FollowingUpReason = DowntimeIssue_Followup.FollowingUpReason,
                        SupervisorComments = DowntimeIssue_Followup.SupervisorComments,
                        EmployeeId = DowntimeIssue_Followup.EmployeeId,
                        SupervisorFollowUp = DowntimeIssue_Followup.SupervisorFollowUp });
            }

        }

        /// <summary>
        /// Updates the downtime issue follow up.
        /// </summary>
        /// <param name="DowntimeIssue_Followup">A <see cref="DowntimeIssue_FollowupModel"/> that represent the new downtime issue follow up</param>
        public static void Update(DowntimeIssue_FollowupModel DowntimeIssue_Followup)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                cnn.Execute("[UpdateDowntimeIssues_Followup] @Id, @DowntimeIssueId, @FollowUpRequest, @FollowingUpReason, @SupervisorComments, @EmployeeId, @SupervisorFollowUp",
                    new
                    {
                        Id = DowntimeIssue_Followup.Id,
                        DowntimeIssueId = DowntimeIssue_Followup.DowntimeIssueId,
                        FollowUpRequest = DowntimeIssue_Followup.FollowUpRequest,
                        FollowingUpReason = DowntimeIssue_Followup.FollowingUpReason,
                        SupervisorComments = DowntimeIssue_Followup.SupervisorComments,
                        EmployeeId = DowntimeIssue_Followup.EmployeeId,
                        SupervisorFollowUp = DowntimeIssue_Followup.SupervisorFollowUp
                    });
            }
        }


/****************************************************************
 * PRIVATE
 **************************************************************/
        private static void Delete(int downtimeIssueId)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                cnn.Execute("[DeleteDowntimeIssue_Followup] @Id",
                    new
                    {
                        Id = downtimeIssueId
                    });
            }

        }

        private static Func<DowntimeIssue_FollowupModel, DowntimeIssueModel, AppUserModel, AppUserModel, EquipmentModel, AreaModel, DowntimeIssue_FollowupModel> GetDowntimeIssue_FollowUp()
        {
            var remainingDowntimeIssues = new Dictionary<int, DowntimeIssueModel>();
            var remainingDowntimeIssues_FollowUps = new Dictionary<int, DowntimeIssue_FollowupModel>();
            var remainingEmployees = new Dictionary<string, AppUserModel>();

            return (downtimeIssue_Followup, downtimeIssue, submittingSupervisor, followingUpSupervisor, equipment, area) =>
            {
                DowntimeIssue_FollowupModel DowntimeIssue_FollowupEntity;
                if(!remainingDowntimeIssues_FollowUps.TryGetValue(downtimeIssue_Followup.Id, out DowntimeIssue_FollowupEntity ))
                {
                    remainingDowntimeIssues_FollowUps.Add(downtimeIssue_Followup.Id, DowntimeIssue_FollowupEntity = downtimeIssue_Followup);
                }

                DowntimeIssueModel DowntimeIssueEntity;
                if (!remainingDowntimeIssues.TryGetValue(downtimeIssue.Id, out DowntimeIssueEntity))
                {
                    remainingDowntimeIssues.Add(downtimeIssue.Id, DowntimeIssueEntity = downtimeIssue);
                }
                DowntimeIssue_FollowupEntity.DowntimeIssue = DowntimeIssueEntity;

                AppUserModel followingUpSupervisorEntity;
                if (!remainingEmployees.TryGetValue(followingUpSupervisor.Id, out followingUpSupervisorEntity))
                {
                    remainingEmployees.Add(followingUpSupervisor.Id, followingUpSupervisorEntity = followingUpSupervisor);
                }


                DowntimeIssue_FollowupEntity.Employee = followingUpSupervisorEntity;

                AppUserModel submittingSupervisorEntity;
                if (!remainingEmployees.TryGetValue(submittingSupervisor.Id, out submittingSupervisorEntity))
                {
                    remainingEmployees.Add(submittingSupervisor.Id, submittingSupervisorEntity = submittingSupervisor);
                }

                DowntimeIssue_FollowupEntity.DowntimeIssue.EmployeeId = submittingSupervisor.Id;
                downtimeIssue_Followup.DowntimeIssue.Employee = submittingSupervisorEntity;

                DowntimeIssue_FollowupEntity.DowntimeIssue.EquipmentId = equipment.Id;
                DowntimeIssue_FollowupEntity.DowntimeIssue.Equipment = equipment;

                DowntimeIssue_FollowupEntity.DowntimeIssue.Equipment.AreaId = area.Id;
                DowntimeIssue_FollowupEntity.DowntimeIssue.Equipment.Area = area;

                return DowntimeIssue_FollowupEntity;
            };
        }

        private static List<DowntimeIssue_FollowupModel> GetDowntimeIssue_FollowUp(string SQL, object param = null)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {

                var a = cnn.Query<DowntimeIssue_FollowupModel, DowntimeIssueModel, AppUserModel, AppUserModel, EquipmentModel, AreaModel, DowntimeIssue_FollowupModel>(SQL
                    , GetDowntimeIssue_FollowUp()
                , param: param
                , splitOn: "DowntimeIssues, SubmittingSupervisor, FollowingUpSupervisor, Equipment, Area").ToList<DowntimeIssue_FollowupModel>();
                return a;
            }

        }


    }
}
