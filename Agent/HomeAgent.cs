using CSV.EFModel;
using System;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.IO;
using OfficeOpenXml;
using System.Collections.Generic;

namespace CSV.Agent
{
    public class HomeAgent
    {
        public void csvUpload(HttpPostedFileBase excelFile)
        {
            using (var package = new ExcelPackage(excelFile.InputStream))
            {
                var worksheet = package.Workbook.Worksheets[0]; // assuming data is in the first sheet
                var rowCount = worksheet.Dimension.Rows;
                var colCount = worksheet.Dimension.Columns;

                List<user> dataList = new List<user>();

                for (int row = 2; row <= rowCount; row++) // assuming the first row contains headers
                {
                    var data = new user
                    {
                        username = worksheet.Cells[row, 1].Value?.ToString().Trim(),
                        email = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                        password = worksheet.Cells[row, 3].Value?.ToString().Trim()
                        //Property2 = int.Parse(worksheet.Cells[row, 2].Value?.ToString().Trim())
                    };
                    dataList.Add(data);
                }

                // Process the data or save to the database
                foreach (var record in dataList)
                {
                    // Perform your database operations here
                }
            }
        }
    }
}