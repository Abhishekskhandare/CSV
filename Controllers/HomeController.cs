using CSV.EFModel;
using System;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.IO;
//using CsvHelper;
using OfficeOpenXml;
using System.Collections.Generic;

namespace CSV.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult CsvUpload(HttpPostedFileBase abhifile2)
        {
            if (abhifile2 != null && abhifile2.ContentLength > 0)
            {
                try
                {
                    using (var package = new ExcelPackage(abhifile2.InputStream))
                    {
                        var worksheet = package.Workbook.Worksheets[0]; // assuming data is in the first sheet
                        var rowCount = worksheet.Dimension.Rows;
                        var colCount = worksheet.Dimension.Columns;

                        List<YourDataClass> dataList = new List<YourDataClass>();

                        for (int row = 2; row <= rowCount; row++) // assuming the first row contains headers
                        {
                            var data = new YourDataClass
                            {
                                Property1 = worksheet.Cells[row, 1].Value?.ToString().Trim(),
                                Property2 = worksheet.Cells[row, 2].Value?.ToString().Trim()
                                //Property2 = int.Parse(worksheet.Cells[row, 2].Value?.ToString().Trim())
                                // Add more properties as needed
                            };
                            dataList.Add(data);
                        }

                        // Process the data or save to the database
                        foreach (var record in dataList)
                        {
                            // Perform your database operations here
                        }
                    }
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Error: " + ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "No file uploaded";
            }
            return View();
        }
    }

    public class YourDataClass
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        // Add more properties as needed
    }

}
