using ClosedXML.Excel;
using GApp.BLL.Enums;
using GApp.BLL.Services;
using GApp.DAL.ReadExcel;
using GApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.Resources.ScheduleResources;
using TrainingIS.WebApp.Manager.Views.msgs;

namespace TrainingIS.WebApp.Controllers
{
    public partial class SchedulesController
    {

        public override ActionResult Edit(long? id)
        {
            bool dataBaseException = false;
            msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Schedule Schedule = ScheduleBLO.FindBaseEntityByID((long)id);
            if (Schedule == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Schedule.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            Default_Schedule_Edit_Model Default_Form_Schedule_Model = new Default_Schedule_Edit_ModelBLM(this._UnitOfWork, this.GAppContext)
                                                                .ConverTo_Default_Schedule_Edit_Model(Schedule);

            this.Fill_Edit_ViewBag(Default_Form_Schedule_Model);
            ViewBag.Reference = Schedule.Reference;
            return View(Default_Form_Schedule_Model);


        }

        /// <summary>
        /// Import Ismontic Time Table : Emploi_*
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Ismontic_Import(string scheduleReference)
        {
            Schedule schedule = this.ScheduleBLO.FindBaseEntityByReference(scheduleReference);

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
                SeancePlanningBLO seancePlanningBLO = new SeancePlanningBLO(this._UnitOfWork, this.GAppContext);
                List<ImportReport> importReports = seancePlanningBLO.Ismontic_Import_Time_Table(firstTable, schedule.Reference, FileName);


                DataSet DataSet_report = importReports.First().get_DataSet_Report();
               
                // Save Ismontic Data ExcelRepport file to Server
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
                    Session["repport_name"] = "Emploi du temps";
                    wb.SaveAs(path_repport);
                }

                // Add DownLoad Link to Repport
                string a_download = "<a href=\"/Schedules/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
            return RedirectToAction("Edit",new { Id = schedule.Id });
        }

 
    }
}