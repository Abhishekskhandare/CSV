using CSV.EFModel;
using System;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.IO;
using OfficeOpenXml;
using System.Collections.Generic;
using CSV.Agent;

namespace CSV.Controllers
{
    public class HomeController : Controller
    {
        HomeAgent homeAgent = new HomeAgent();
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
        public ActionResult CsvUpload(HttpPostedFileBase excelFile)
        {
            if (excelFile != null && excelFile.ContentLength > 0)
            {
                try
                {
                    homeAgent.csvUpload(excelFile);
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

}
