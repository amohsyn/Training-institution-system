using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL.Services;

namespace TrainingIS.WebApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class BackupDataController : BaseController
    {
        // GET: BackupData
        public ActionResult Index()
        {
            return View();
        }

        public virtual FileResult Export()
        {
            DataBaseBakupService dataBaseBakupService = new DataBaseBakupService(this._UnitOfWork);
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
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_Trainee.PluralName + ".xlsx");
                }
            }
        }

        public virtual ActionResult Import()
        {
            ////Save excel file to server
            //HttpPostedFileBase parametersTemplate = Request.Files["import_objects"];

            //// [Bug] if multiple user import the same file in the same moments
            //string path = Server.MapPath("~/Content/Files/Upload" + parametersTemplate.FileName);
            //if (System.IO.File.Exists(path))
            //{
            //    System.IO.File.Delete(path);
            //    parametersTemplate.SaveAs(path);
            //}
            //parametersTemplate.SaveAs(path);

            ////Save new parameters to database
            //var excelData = new ExcelData(path); // link to other project
            //DataTable firstTable = excelData.getFirstTable();

            //try
            //{
            //    string msg = traineeBLO.Import(firstTable);
            //    Message(msg, NotificationType.info);

            //}
            //catch (ImportLineException e)
            //{
            //    Message(e.Message, NotificationType.info);
            //}
            return RedirectToAction("Index");
        }
    }
}