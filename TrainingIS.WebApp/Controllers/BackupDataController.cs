using ClosedXML.Excel;
using GApp.BLL.Enums;
using GApp.BLL.Services;
using GApp.DAL.ReadExcel;
using GApp.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL;
using TrainingIS.BLL.Services;
using TrainingIS.DAL;
using TrainingIS.Entities.Resources.TraineeResources;

namespace TrainingIS.WebApp.Controllers
{
    public class BackupDataController : BaseController<TrainingISModel>
    {
        // GET: BackupData
        public ActionResult Index()
        {
            return View();
        }

        public virtual FileResult Export()
        {
            DataBaseBakupService dataBaseBakupService = new DataBaseBakupService(this._UnitOfWork, this.GAppContext) ;
            DataSet dataSet = dataBaseBakupService.Export();

            using (XLWorkbook wb = new XLWorkbook())
            {

                foreach (DataTable dataTable in dataSet.Tables)
                {
                    wb.Worksheets.Add(dataTable);
                }
              
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", dataSet.DataSetName + ".xlsx");
                }
            }
        }

        public virtual ActionResult Import()
        {

            string FileName = "Import_All_Data" + Guid.NewGuid().ToString() + ".xlsx";

            //Save excel file to the server
            HttpPostedFileBase postedFile = Request.Files["import_objects"];
            string path = Server.MapPath("~/Content/Files/" + FileName);
            postedFile.SaveAs(path);

            //Save new parameters to database
            var excelData = new ExcelData(path); // link to other project

            //Save to database
            DataBaseBakupService dataBaseBakupService = new DataBaseBakupService(this._UnitOfWork, this.GAppContext) ;
            var DataSet = excelData.getDataSet();
            try
            {
                List<ImportReport> importReports = dataBaseBakupService.Import(DataSet);

                // Save ExcelRepport file to Server
                DataSet DataSet_reports = new DataSet();
                foreach (ImportReport importReport in importReports)
                {
                    DataSet dataSet_report = importReport.get_DataSet_Report();
                    DataSet_reports.Merge(dataSet_report);
                }
             
 
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_reports);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
                    wb.SaveAs(path_repport);

            

                }

                // Add DownLoad Link to Repport
                string a_download = string.Format( "<a href=\"{0}BackupData/LastRepportFile\">Télécharger le rapport d'importation</a>",Url.Content("~/"));
                importReports.First().AddMessage(a_download, MessagesService.MessageTypes.Meta_msg);
                string html_report = string.Join("", importReports.Select(i => i.get_HTML_Report()));
                // Show HTML Report
                Message(html_report, NotificationType.info);
            }
            catch (ImportException e)
            {
                Message(e.Message, NotificationType.error);
            }
            return RedirectToAction("Index");


            //DataBaseBakupService dataBaseBakupService = new DataBaseBakupService(this._UnitOfWork, this.GAppContext) ;
            //var DataSet = excelData.getDataSet();
            //try
            //{
            //   string msg = dataBaseBakupService.Import(DataSet);
            //   Message(msg, NotificationType.info);
            //}
            //catch (ImportException e)
            //{
            //    Message(e.Message, NotificationType.info);
            //}
            //return RedirectToAction("Index");
        }

        public void SaveDataSetToDataBase()
        {

        }

        public FileResult LastRepportFile()
        {
            // [Bug] if the user try to Import multiple data in the same time
            if (Session["path_repport"] != null)
            {
                string path = Session["path_repport"] as string;
                var fileStream = new FileStream(path, FileMode.Open);
                string FileName = "Rapport d'importation - " + DateTime.Now.ToString();
                return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName + ".xlsx");

            }
            return null;

        }
    }
}