using CSV.EFModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CSV.Repository
{
    public class UCNViewRepository<T> : IUCNViewRepository<T> where T : class
    {
        readonly List<SqlParameter> parameterList = new List<SqlParameter>();
        string ReturnParameter = string.Empty;
        GiveeEntities giveeEntities = new GiveeEntities();

        #region public methods
        
        public List<T> ExecuteStoredProcedureList(string commandText , int storedProcedureTimeOut , int? indexOutParameter , out int totalRowCount)
        {
            totalRowCount = 0;
            try
            {
                Database database = giveeEntities.GetDatabase();
                // set the time out value for the stored procedure, in seconds base on parameter,
                // instead of using default value of the underlying parameter.
                if(storedProcedureTimeOut >0)
                {
                    database.CommandTimeout = storedProcedureTimeOut;
                }

                var result = database.SqlQuery<T>(commandText, parameterList.ToArray()).ToList();

                if(!Equals(result , null) && indexOutParameter.HasValue)
                {
                    totalRowCount = Convert.ToInt32(parameterList[Convert.ToInt32(indexOutParameter)].Value);
                }
                return result.ToList();

            }
            catch (Exception ex)
            {
                var Error = ex.Message;
            }
            finally
            {
                clearParameters();
            }
            return null;
        }
     
        public void SetParameter(string parameterName, DataTable parameterValue, ParameterDirection direction, SqlDbType dbType, string tableTypeName)
        {
            SqlParameter parameter = new SqlParameter
            {
                ParameterName = parameterName,
                Value = parameterValue,
                SqlDbType = dbType,
                Direction = direction,
                TypeName = tableTypeName // Ensure this matches the table type name in SQL Server
            };

            if (direction == ParameterDirection.Output)
            {
                ReturnParameter = parameterName;
            }

            parameterList.Add(parameter);
        }

        #endregion

        #region Private Methods

        private void clearParameters()
        {
            parameterList.Clear();
        }
        #endregion

    }
}