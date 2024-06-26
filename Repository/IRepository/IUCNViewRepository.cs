using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CSV.Repository
{
    public interface IUCNViewRepository<T>
    {
        void SetParameter(string ParameterName, DataTable ParameterValue, ParameterDirection Direction, SqlDbType dbType ,string tableTypeName);
        List<T> ExecuteStoredProcedureList(string commandText, int storedProcedureTimeOut, int? indexOutParameter, out int totalRowCount);

    }
}