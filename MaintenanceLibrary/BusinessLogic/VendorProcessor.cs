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
    public class VendorProcessor
    {
        /// <summary>
        /// Queries for a list of Vendors 
        /// </summary>
        /// <returns>A list of <see cref="VendorModel"/>s</returns>
        public static IEnumerable<VendorModel> GetVendors()
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                return cnn.Query<VendorModel>("Exec [GetVendors]");
            }
        }

        /// <summary>
        /// Queries an Vendor with the inputed Vendor Id.
        /// </summary>
        /// <param name="vendorId">Id representing the Vendor</param>
        /// <returns>A <see cref="VendorModel"/></returns>
        public static VendorModel GetVendorByVendorId(int vendorId)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                return cnn.Query<VendorModel>("Exec [GetVendorByVendorId] @VendorId",new { vendorId = vendorId}).First();
            }
        }

        /// <summary>
        /// Inserts an Vendor
        /// </summary>
        /// <param name="vendor">A <see cref="VendorModel"/> that represents the new Vendor</param>
        public static void Insert(VendorModel vendor)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                cnn.Execute("[InsertVendor] @Name", new { Name = vendor.Name });
            }

        }

        /// <summary>
        /// Updates an Vendor
        /// </summary>
        /// <param name="vendor">A <see cref="VendorModel"/> that represents the Area being updated</param>
        public static void Update(VendorModel vendor)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                cnn.Execute("[UpdateVendor] @Id, @Name", 
                    new {
                        Id = vendor.Id
                        ,Name = vendor.Name 
                    });
            }

        }

/***********************************************
 * PRIVATE
 **********************************************/
    }
}
