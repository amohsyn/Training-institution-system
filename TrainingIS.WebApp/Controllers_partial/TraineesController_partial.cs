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

            //Save to database
            var excelData = new ExcelData(path); // link to other project
            DataTable firstTable = excelData.getFirstTable();

            try
            {
                ImportReport importReport = traineeBLO.Import_1(firstTable);

                // Save Excel Repport
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Import_Repport.xlsx");
                        Session["path_repport"] = path_repport;

                       // var fileStream = new FileStream(path_repport, FileMode.Create, FileAccess.Write);
                        wb.SaveAs(path_repport);
                       // fileStream.Dispose();

                        

                        string a_download = "<a href=\"/Trainees/LastRepportFile\">Télécharger le rapport d'importation</a>";
                        importReport.AddMessage(a_download, MessagesService.MessageTypes.Meta_msg);
                    }
                }

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

            if(Session["path_repport"] != null)
            {
                string path = Session["path_repport"] as string;
              


                var fileStream = new FileStream(path, FileMode.Open);
                return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Import_Repport" + ".xlsx");
                
            }
            return null;
            
        }
    }
}