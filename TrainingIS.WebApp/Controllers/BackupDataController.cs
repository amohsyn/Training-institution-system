using ClosedXML.Excel;
using GApp.BLL.Enums;
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
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", this.Home_Controller + ".xlsx");
                }
            }
        }

        public virtual ActionResult Import()
        {
            //Save excel file to server
            HttpPostedFileBase parametersTemplate = Request.Files["import_objects"];

            // [Bug] if multiple user import the same file in the same moments
            string path = Server.MapPath("~/Content/Files/Upload" + parametersTemplate.FileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                parametersTemplate.SaveAs(path);
            }
            parametersTemplate.SaveAs(path);

            //Save new parameters to database
            var excelData = new ExcelData(path); // link to other project


            DataBaseBakupService dataBaseBakupService = new DataBaseBakupService(this._UnitOfWork, this.GAppContext) ;
            var DataSet = excelData.getDataSet();
            try
            {
               string msg = dataBaseBakupService.Import(DataSet);
               Message(msg, NotificationType.info);
            }
            catch (ImportException e)
            {
                Message(e.Message, NotificationType.info);
            }
            return RedirectToAction("Index");
        }

        public void SaveDataSetToDataBase()
        {

        }
    }
}