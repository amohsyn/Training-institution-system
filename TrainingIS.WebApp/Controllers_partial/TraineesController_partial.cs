using ClosedXML.Excel;
using GApp.DAL.ReadExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL;
using TrainingIS.Entities;
using static TrainingIS.WebApp.Enums.Enums;

namespace TrainingIS.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Supervisor,PedagogicalDirector")]
    public partial class TraineesController
    {
        [Authorize(Roles = "Supervisor")]
        public override ActionResult Create([Bind(Include = "Id,FirstName,LastName,FirstNameArabe,LastNameArabe,BirthPlace,Sex,CIN,Cellphone,TutorCellPhone,Email,Address,FaceBook,WebSite,CNE,CEF,isActif,DateRegistration,NationalityId,SchoollevelId,GroupId,CreateDate,UpdateDate,Birthdate")] Trainee trainee)
        {
            return base.Create(trainee);
        }

        [Authorize(Roles = "Supervisor")]
        public override ActionResult Create()
        {
            return base.Create();
        }

        [Authorize(Roles = "Supervisor")]
        public override ActionResult Delete(long? id)
        {
            return base.Delete(id);
        }

        [Authorize(Roles = "Supervisor")]
        public override ActionResult DeleteConfirmed(long id)
        {
            return base.DeleteConfirmed(id);
        }

        [Authorize(Roles = "Supervisor")]
        public override ActionResult Edit(long? id)
        {
            return base.Edit(id);
        }

        [Authorize(Roles = "Supervisor")]
        public override ActionResult Edit([Bind(Include = "Id,FirstName,LastName,FirstNameArabe,LastNameArabe,BirthPlace,Sex,CIN,Cellphone,TutorCellPhone,Email,Address,FaceBook,WebSite,CNE,CEF,isActif,DateRegistration,NationalityId,SchoollevelId,GroupId,CreateDate,UpdateDate,Birthdate")] Trainee trainee)
        {
            return base.Edit(trainee);
        }

        [Authorize(Roles = "Supervisor")]
        public override ActionResult Import()
        {


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
                ImportReport importReport = traineeBLO.Import_1(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/Trainees/LastRepportFile\">Télécharger le rapport d'importation</a>";
                    importReport.AddMessage(a_download, MessagesService.MessageTypes.Meta_msg);

                }

                // Show HTML Report
                Message(importReport.get_HTML_Report(), NotificationType.info);
            }
            catch (ImportException e)
            {
                Message(e.Message, NotificationType.error);
            }
            return RedirectToAction("Index");
        }

        private void SaveFileStream(String path, Stream stream)
        {
            var fileStream = new FileStream(path, FileMode.Open);
            stream.CopyTo(fileStream);
            fileStream.Dispose();
        }

        public FileResult LastRepportFile()
        {
            // [Bug] if the user try to Import multiple data in the same time
            if (Session["path_repport"] != null)
            {
                string path = Session["path_repport"] as string;
                var fileStream = new FileStream(path, FileMode.Open);
                string FileName = "Rapport d'importation - " + DateTime.Now.ToString();
                return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Import_Repport" + ".xlsx");

            }
            return null;

        }
    }
}