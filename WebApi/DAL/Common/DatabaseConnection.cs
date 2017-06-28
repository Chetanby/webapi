using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.DAL.Common
{
    public class DatabaseConnection
    {
        static string _connectionString;
        public DatabaseConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("ConnectionString:Database").Value;
        }

        public SqlConnection CreateDatabaseConnection()
        {
            var con = new SqlConnection { ConnectionString = _connectionString };

            con.Open();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            int intIteration = 0;
            switch (con.State)
            {
                case ConnectionState.Broken:
                case ConnectionState.Closed:
                case ConnectionState.Connecting:
                    while (intIteration < 20)
                    {
                        Thread.Sleep(1000);
                        if (con.State == ConnectionState.Open)
                        {
                            break;
                        }
                        intIteration++;
                    }
                    break;
                case ConnectionState.Executing:
                case ConnectionState.Fetching:
                    con = new SqlConnection(_connectionString);
                    con.Open();
                    break;
            }

            return con;
        }

    }
}
