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
    public class AreasProcessor
    {
        /// <summary>
        /// Queries for a list of Active Areas 
        /// </summary>
        /// <returns>A list of <see cref="AreaModel"/>s where Deactivate is null</returns>
        public static List<AreaModel> GetAreas()
        {
            return GetArea("Exec [GetAreas]").Distinct<AreaModel>().ToList();
        }

        /// <summary>
        /// Queries an Area with the inputed Area Id.
        /// </summary>
        /// <param name="areaId">Id representing the Area</param>
        /// <returns>A <see cref="AreaModel"/></returns>
        public static AreaModel GetAreaByAreaId(int areaId)
        {
            return GetArea("Exec [GetAreaByAreaId] @AreaId", new { AreaId = areaId }).FirstOrDefault();
        }

        /// <summary>
        /// Inserts an Area
        /// </summary>
        /// <param name="area">A <see cref="AreaModel"/> that represents the new Area</param>
        public static void Insert(AreaModel area)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                cnn.Execute("[InsertArea] @Name", new { Name = area.Name });
            }

        }

        /// <summary>
        /// Updates an Area
        /// </summary>
        /// <param name="area">A <see cref="AreaModel"/> that represents the Area being updated</param>
        public static void Update(AreaModel area)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                cnn.Execute("[UpdateArea] @Id, @Name, @Deactivate", 
                    new {
                        Id = area.Id
                        ,Name = area.Name 
                        ,Deactivate = area.Deactivate
                    });
            }

        }

        public static string Deactivate(AreaModel area)
        {
            if(EquipmentProcessor.GetEquipmentByAreaId(area.Id).Count == 0)
            {
                area.Deactivate = DateTime.Now;
                Update(area);
                return "Success";
            }
            else
            {
                return "Can not delete, because Equipment is assigned to that area.";
            }
        }
/***********************************************
 * PRIVATE
 **********************************************/

        private static Func<AreaModel, EquipmentModel, AreaModel> GetArea()
        {
            var Areas = new Dictionary<int, AreaModel>();
            var Equipment = new Dictionary<int, EquipmentModel>();
            return (area, equipment) =>
            {
                AreaModel AreaEntity;
                if (!Areas.TryGetValue(area.Id, out AreaEntity))
                {
                    Areas.Add(area.Id, AreaEntity = area);
                }
                if (AreaEntity.Equipment is null)
                    AreaEntity.Equipment = new List<EquipmentModel>();
                if(equipment != null)
                {
                    equipment.AreaId = AreaEntity.Id;
//                    equipment.Area = AreaEntity;
                    AreaEntity.Equipment.Add(equipment);
                }

                return AreaEntity;
            };
        }

        private static IEnumerable<AreaModel> GetArea(string SQL, object param = null)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                return cnn.Query<AreaModel, EquipmentModel, AreaModel>(
                    SQL, 
                    GetArea(), 
                    param: param
                    );
            }
        }

        private static void Delete(int areaId)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                cnn.Execute("[DeleteArea] @Id",
                    new
                    {
                        Id = areaId
                    });
            }

        }

    }
}
