﻿using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL.Reports;

namespace TrainingIS.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Director")]
    public class ReportsController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        //GET: Reports
        public FileResult ExportExcel()
        {
            NumberOfAbsencesPerTrainee numberOfAbsencesPerTrainee = new NumberOfAbsencesPerTrainee();
            DataTable dataTable = numberOfAbsencesPerTrainee.getDataTable();

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "numberOfAbsencesPerTrainee" + ".xlsx");
                }
            }
        }
    }
}