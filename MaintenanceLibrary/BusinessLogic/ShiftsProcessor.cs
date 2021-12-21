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
    public class ShiftsProcessor
    {
        /// <summary>
        /// Get all shifts
        /// </summary>
        /// <returns>A List of <see cref="ShiftModel"/></returns>
        public static List<ShiftModel> GetShifts()
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                return cnn.Query<ShiftModel>("Exec [GetShifts]").ToList();
            }
        }

        /// <summary>
        /// Get a shift
        /// </summary>
        /// <param name="ShiftId">A id that is associated with shift</param>
        /// <returns>A <see cref="ShiftModel"/> that represent record with ShiftId</returns>
        public static ShiftModel GetShiftByShiftId(int ShiftId)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                return cnn.Query<ShiftModel>("Exec [GetShiftByShiftId] @ShiftId", new { ShiftId = ShiftId }).First<ShiftModel>();
            }
        }

        /// <summary>
        /// Inserts Shift
        /// </summary>
        /// <param name="shift">A <see cref="ShiftModel"/> that represents Shift being inserted.</param>
        public static void Insert(ShiftModel shift)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                cnn.Execute("[InsertShift] @Initials, @Name", 
                    new { 
                        Initials = shift.Initials,
                        Name = shift.Name
                    });
            }

        }

        /// <summary>
        /// Update Shift
        /// </summary>
        /// <param name="shift">A <see cref="ShiftModel"/> that represents shift being updated.</param>
        public static void Update(ShiftModel shift)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                cnn.Execute("[UpdateShift] @Id, @Initials, @Name",
                    new
                    {
                        Id = shift.Id,
                        Initials = shift.Initials,
                        Name = shift.Name
                    });
            }
        }

        /// <summary>
        /// Delete Shift
        /// </summary>
        /// <param name="shift">A <see cref="ShiftModel"/> representing the shift being deleted</param>
        /// <returns>A <see cref="string"/> saying "Success" if no active employee are in shift, otherwise returns error message <see cref="string"/></returns>
        public static string Delete(ShiftModel shift)
        {
            if (AppUserProcessor.GetAllCurrentEmployeesByShiftId(shift.Id).Count == 0)
            {
                Delete(shift.Id);
                return "Success";
            }
            else
            {
                return "You must remove all active employees from this shift before you delete it.";
            }
        }

        /// <summary>
        /// Deletes Shift
        /// </summary>
        /// <param name="Id">Id associated with shift.</param>
        public static void Delete(int Id)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                cnn.Execute("[DeleteShift] @Id",
                    new
                    {
                        Id = Id
                    });
            }

        }
    }
}
