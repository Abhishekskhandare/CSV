using CSV.EFModel;
using System;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.IO;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Configuration;
using CSV.Repository;

namespace CSV.Agent
{
    public class HomeAgent
    {
        GiveeEntities giveeEntities = new GiveeEntities();
        public void csvUpload(HttpPostedFileBase excelFile)
        {
            using (var package = new ExcelPackage(excelFile.InputStream))
            {
                var worksheet = package.Workbook.Worksheets[0]; // assuming data is in the first sheet
                var rowCount = worksheet.Dimension.Rows;
                var colCount = worksheet.Dimension.Columns;

                DataTable userTable = new DataTable();
                userTable.Columns.Add("userid", typeof(int));
                userTable.Columns.Add("username", typeof(string));
                userTable.Columns.Add("email", typeof(string));
                userTable.Columns.Add("password", typeof(string));

                for (int row = 2; row <= rowCount; row++) // assuming the first row contains headers
                {
                    var data = new user
                    {
                        userid = 0,
                        username = worksheet.Cells[row, 1].Value?.ToString().Trim(),
                        email = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                        password = worksheet.Cells[row, 3].Value?.ToString().Trim()
                    };
                    userTable.Rows.Add(data.userid , data.username , data.email , data.password);
                }
                //old method 
                //var users  = giveeEntities.GetNonExistingUsers(userTable);

                IUCNViewRepository<user> objStoredProc = new UCNViewRepository<user>();
                objStoredProc.SetParameter("@UserTable" , userTable , ParameterDirection.Input , SqlDbType.Structured, "dbo.UserTableType");
                var users2 = objStoredProc.ExecuteStoredProcedureList("EXEC GetNonExistingUsers @UserTable", 0, null, out int totalRowCount);


            }
        }
    }
}