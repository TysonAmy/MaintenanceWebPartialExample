using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace MaintenanceLibrary.DataAccess
{
    public class SQLDataAccess
    {
        private static string _defaultConnection;
        
        /// <summary>
        /// By setting this value in MaintenanceWebsite Startup.cs function Configure
        /// This connects MaintenanceWebsite to MaintenanceLibrary
        /// </summary>
        public static string DefaultConnection
        {
            get
            {
                return _defaultConnection;
            }
            set
            {
                _defaultConnection = value;
            }
        }

        /// <summary>
        /// This is a extra read function for DefaultConnection
        /// </summary>
        /// <returns><see cref="string"/> Connection String</returns>
        public static string GetConnectionString()
        {
            return _defaultConnection;
        }
    }
}
