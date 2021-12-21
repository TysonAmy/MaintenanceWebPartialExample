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
    /// <summary>
    /// Used to keep track of when users are loginning in.
    /// </summary>
    public class LoginProcessor
    {
        //InsertLoginByEmail
        /// <summary>
        /// Inserts email address and datetime user is logging in.
        /// </summary>
        /// <param name="Email">Email address of user logging in.</param>
        public static void Insert(string Email)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                cnn.Execute("[InsertLoginByEmail] @Email", new { Email = Email });
            }
        }

        /// <summary>
        /// Gets Login Datetime before current login, or null if none.
        /// </summary>
        /// <param name="userId">A Id that represents the user.</param>
        /// <returns>A <see cref="DateTime"/> User last logged in, or null if record doesn't exist.</returns>
        public static DateTime? GetPreviousLogin(string userId)
        {
            using (IDbConnection cnn = new SqlConnection(DataAccess.SQLDataAccess.GetConnectionString()))
            {
                List<dynamic> dates = new();

                dates = cnn.Query("[GetPreviousLogin] @UserId", new { UserId = userId }).ToList<dynamic>();
                if (dates.Count == 1)
                {
                    var DateDynamic = dates[0];
                    return DateDynamic.LoginTime;
                }
                else
                {
                    return null;
                }
            }

        }

    }
}
