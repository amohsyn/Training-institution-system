using ClosedXML.Excel;
using GApp.BLL.Enums;
using GApp.BLL.Services;
using GApp.DAL;
using GApp.DAL.ReadExcel;
using GApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.ModelsViews.Trainings;
using TrainingIS.Entities.Resources.TrainingResources;

namespace TrainingIS.WebApp.Controllers
{
    public partial class TrainingsController
    {
        /// <summary>
        /// Import Ismontic File : Matrice
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Ismontic_Import()
        {
            this.Create_Files_Directory_If_Not_Exist();
            string FileName = "Import_" + Guid.NewGuid().ToString() + ".xlsx";

            //Save excel file to the server
            HttpPostedFileBase postedFile = Request.Files["import_objects"];
            string path = Server.MapPath("~/Content/Files/" + FileName);
            postedFile.SaveAs(path);

            //Save to database
            var excelData = new ExcelData(path);
            DataTable firstTable = excelData.getFirstTable();
            try
            {
                List<ImportReport> importReports = TrainingBLO.Ismontic_Import(firstTable, FileName);

              
                DataSet DataSet_report = importReports.First().get_DataSet_Report();

                // Save Ismontic Data ExcelRepport file to Server
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
                    Session["repport_name"] = msg_Training.PluralName;
                    wb.SaveAs(path_repport);
                }

                // Add DownLoad Link to Repport
                string a_download = "<a href=\"/Trainings/LastRepportFile\">Télécharger le rapport d'importation</a>";
                importReports.First().AddMessage(a_download, MessagesService.MessageTypes.Meta_msg);

                // Show HTML Report
                string html_report = string.Join("", importReports.Select(i => i.get_HTML_Report()));

                Message(html_report, NotificationType.info);
            }
            catch (ImportException e)
            {
                Message(e.Message, NotificationType.error);
            }
            catch (GAppException e)
            {
                Message(e.Message, NotificationType.error);
            }
            return RedirectToAction("Index");
        }


        protected override void Fill_ViewBag_Create(Create_Training_Model Create_Training_Model)
        {
            ViewBag.FormerId = new SelectList(new FormerBLO(this._UnitOfWork, this.GAppContext).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Create_Training_Model.FormerId);
            ViewBag.GroupId = new SelectList(new GroupBLO(this._UnitOfWork, this.GAppContext).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Create_Training_Model.GroupId);
            ViewBag.TrainingYearId = new SelectList(new TrainingYearBLO(this._UnitOfWork, this.GAppContext).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Create_Training_Model.TrainingYearId);
            ViewBag.SpecialtyId = new SelectList(new SpecialtyBLO(this._UnitOfWork, this.GAppContext).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Create_Training_Model.SpecialtyId);
            ViewBag.ModuleTrainingId = new SelectList(new ModuleTrainingBLO(this._UnitOfWork, this.GAppContext).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Create_Training_Model.ModuleTrainingId);
            
            // Many 
        }

        protected override void Fill_Edit_ViewBag(Edit_Training_Model Edit_Training_Model)
        {
            ViewBag.FormerId = new SelectList(new FormerBLO(this._UnitOfWork, this.GAppContext).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Edit_Training_Model.FormerId);
            ViewBag.GroupId = new SelectList(new GroupBLO(this._UnitOfWork, this.GAppContext).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Edit_Training_Model.GroupId);
            ViewBag.TrainingYearId = new SelectList(new TrainingYearBLO(this._UnitOfWork, this.GAppContext).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Edit_Training_Model.TrainingYearId);
            ViewBag.SpecialtyId = new SelectList(new SpecialtyBLO(this._UnitOfWork, this.GAppContext).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Edit_Training_Model.SpecialtyId);
            ViewBag.ModuleTrainingId = new SelectList(new ModuleTrainingBLO(this._UnitOfWork, this.GAppContext).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Edit_Training_Model.ModuleTrainingId);

             
        }



    }
}