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
    public class EquipmentProcessor
    {
        /// <summary>
        /// Queries for equipment that is in a particular Area
        /// </summary>
        /// <param name="areaId">Id that represents an Area</param>
        /// <returns>A List of <see cref="EquipmentModel"/> that is in an Area</returns>
        public static List<EquipmentModel> GetEquipmentByAreaId(int areaId)
        {
            return GetEquipmentModels("Exec [GetEquipmentByAreaId] @AreaId",new { AreaId = areaId });
        }

        /// <summary>
        /// Queries for a partitular Equipment
        /// </summary>
        /// <param name="equipmentId">A Id associated with Equipment</param>
        /// <returns>A <see cref="EquipmentModel"/> that has the equipmentId</returns>
        public static EquipmentModel GetEquipmentByEquipmentId(int equipmentId)
        {
            return GetEquipmentModel("[GetEquipmentByEquipmentId] @EquipmentId", new { EquipmentId = equipmentId });
        }

        /// <summary>
        /// Gets all Equipment that is not inactivated
        /// </summary>
        /// <returns>A List of <see cref="EquipmentModel"/> that are not Deactivate.</returns>
        public static List<EquipmentModel> GetEquipments()
        {
            return GetEquipmentModels("Exec [GetEquipment] ");
        }

        /// <summary>
        /// Gets a all Deactivate Equipment
        /// </summary>
        /// <returns>A List of <see cref="EquipmentModel"/> that are deactivated</returns>
        public static List<EquipmentModel> GetDeactivatedEquipment()
        {
            return GetEquipmentModels("Exec [GetDeactivatedEquipment] ");
        }

        /// <summary>
        /// Inserts Equipment
        /// </summary>
        /// <param name="equipment">A <see cref="EquipmentModel"/> that represents the equipment being added.</param>
        public static void Insert(EquipmentModel equipment)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                cnn.Execute("[InsertEquipment] @Name, @AreaId", 
                    new { 
                        Name = equipment.Name,
                        AreaId = equipment.AreaId
                    });
            }
        }

        /// <summary>
        /// Updates equipment information
        /// </summary>
        /// <param name="equipment">A <see cref="EquipmentModel"/> that represents the Equipment being updated</param>
        public static void Update(EquipmentModel equipment)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                cnn.Execute("[UpdateEquipment] @Id, @Name, @AreaId, @Deactivate",
                    new
                    {
                        Id = equipment.Id,
                        Name = equipment.Name,
                        AreaId = equipment.AreaId,
                        Deactivate = equipment.Deactivate
                    });
            }

        }

        /// <summary>
        /// Deactivate Equipment of they have no outstanding follow up associated with them.
        /// </summary>
        /// <param name="equipment">A <see cref="EquipmentModel"/> that represents Equipment being added.</param>
        /// <returns>Return results. Success of Equipment doesn't have followup associated with it.</returns>
        public static string Deactivate(EquipmentModel equipment)
        {
            if(OtherProcessor.TotalOutstandingFollowUpsByEquipmentId(equipment.Id) == 0)
            {
                equipment.Deactivate = DateTime.Now;
                Update(equipment);
                return "Success";
            }
            else
            {
                return "Equipment can not be deactivated, because there are outstanding followups.";
            }
        }


        /***********************************
         * PRIVATE
         * **********************************/
        private static Func<EquipmentModel, AreaModel, EquipmentModel> GetEquipment()
        {
            return (equipment, area) =>
            {
                EquipmentModel equipmentEntity;
                equipmentEntity = equipment;
                equipmentEntity.Area = area;
                return equipmentEntity;
            };
        }

        private static EquipmentModel GetEquipmentModel(string SQL, object param = null)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                return cnn.Query<EquipmentModel, AreaModel, EquipmentModel>(SQL
                    , GetEquipment()
                , param: param
                , splitOn: "Areas").Single<EquipmentModel>();
            }

        }

        private static List<EquipmentModel> GetEquipmentModels(string SQL, object param = null)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                return cnn.Query<EquipmentModel, AreaModel, EquipmentModel>(SQL
                    , GetEquipment()
                , param: param
                , splitOn: "Areas").ToList<EquipmentModel>();
            }

        }

    }
}
