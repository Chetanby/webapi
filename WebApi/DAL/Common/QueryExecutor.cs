using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DAL.Common
{
    public class QueryExecutor
    {
        #region Query Executors with diffrent return types

        public static int ExecuteNonQuery(string strStoredProcedureName, string[] strParameterNames, object[] objParameterValues)
        {
            int returnValue;
            using (SqlConnection objConnection = DatabaseConnection.CreateDatabaseConnection())
            {
                using (var objSqlCommand = new SqlCommand())
                {
                    FillSqlParameter(objSqlCommand, strParameterNames, objParameterValues);
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.CommandText = strStoredProcedureName;
                    objSqlCommand.Connection = objConnection;
                    returnValue = objSqlCommand.ExecuteNonQuery();
                    objSqlCommand.Parameters.Clear();
                }
            }
            return returnValue;
        }


        public static object ExecuteScalar(string strStoredProcedureName, string[] strParameterNames, object[] objParameterValues)
        {
            object returnValue;
            using (SqlConnection objConnection = DatabaseConnection.CreateDatabaseConnection())
            {
                using (var objSqlCommand = new SqlCommand())
                {
                    FillSqlParameter(objSqlCommand, strParameterNames, objParameterValues);
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.CommandText = strStoredProcedureName;
                    objSqlCommand.Connection = objConnection;
                    //objSqlCommand.Connection.Open();
                    returnValue = objSqlCommand.ExecuteScalar();
                    objSqlCommand.Parameters.Clear();
                }
            }
            return returnValue;
        }


        private static void FillSqlParameter(SqlCommand objSqlCommand, string[] strParameterNames, object[] objParameterValues)
        {
            if ((strParameterNames == null) || (strParameterNames.Length <= 0)) return;
            int intNumberOfParameters = strParameterNames.Length;
            for (int intParameterIndex = 0; intParameterIndex < intNumberOfParameters; intParameterIndex++)
            {
                var objSqlParameter = new SqlParameter { ParameterName = strParameterNames[intParameterIndex], Value = objParameterValues[intParameterIndex] };
                if (objSqlCommand != null)
                {
                    objSqlCommand.Parameters.Add(objSqlParameter);
                }
            }
        }


        public static DataTable ExecuteDatatable(string strStoredProcedureName, string[] strParameterNames, object[] objParameterValues)
        {
            using (SqlConnection objConnection = DatabaseConnection.CreateDatabaseConnection())
            {
                var dataTable = new DataTable();
                var objSqlCommand = new SqlCommand();

                var sqlDataAdapter = new SqlDataAdapter();
                objSqlCommand.Connection = objConnection;

                FillSqlParameter(objSqlCommand, strParameterNames, objParameterValues);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.CommandText = strStoredProcedureName;
                //objSqlCommand.Connection = DatabaseConnection.CreateDatabaseConnection();
                sqlDataAdapter.SelectCommand = objSqlCommand;
                sqlDataAdapter.Fill(dataTable);
                objSqlCommand.Parameters.Clear();
                return dataTable;
            }
        }

        public static IEnumerable<SqlDataRecord> ExecuteDataSet(string strStoredProcedureName, string[] strParameterNames, object[] objParameterValues)
        {
            using (SqlConnection objConnection = DatabaseConnection.CreateDatabaseConnection())
            {
                var dataSet = new DataSet();
                var objSqlCommand = new SqlCommand();

                var sqlDataAdapter = new SqlDataAdapter();
                objSqlCommand.Connection = objConnection;

                FillSqlParameter(objSqlCommand, strParameterNames, objParameterValues);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.CommandText = strStoredProcedureName;
                //objSqlCommand.Connection = DatabaseConnection.CreateDatabaseConnection();
                sqlDataAdapter.SelectCommand = objSqlCommand;
                sqlDataAdapter.Fill(dataSet);
                objSqlCommand.Parameters.Clear();
                return dataSet;
            }
        }

        #endregion
    }
}
