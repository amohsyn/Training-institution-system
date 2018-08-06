﻿using System;
using System.Collections.Generic; 
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrainingIS.DAL;
using TrainingIS.Entities;
using TrainingIS.BLL;
using TrainingIS.BLL.Exceptions;
using GApp.DAL.ReadExcel;
using ClosedXML.Excel;
using System.IO;     
using static TrainingIS.WebApp.Enums.Enums;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions; 
using GApp.Entities;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
namespace TrainingIS.WebApp.Controllers
{  
    // Generated by Manager v 0.2.0 
    public class BaseGroupsController : BaseController
    {
        protected GroupBLO GroupBLO = null;

		public BaseGroupsController()
        {
            this.msgHelper = new MessagesService(typeof(Group));
			this.GroupBLO = new GroupBLO(this._UnitOfWork);
        }

	    public virtual ActionResult Index()
        {
		    msgHelper.Index(msg);
            List<IndexGroupView> listIndexGroupView = new List<IndexGroupView>();
			foreach (var item in GroupBLO.FindAll()){
                IndexGroupView IndexGroupView = new IndexGroupViewBLM(this._UnitOfWork)
                    .ConverTo_IndexGroupView(item);
                listIndexGroupView.Add(IndexGroupView);
            }
			return View(listIndexGroupView);
		}

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create([Bind(Include = "TrainingYearId,SpecialtyId,TrainingTypeId,YearStudyId,Code")] CreateGroupView CreateGroupView)
        {
			Group Group = null ;
			Group = new CreateGroupViewBLM(this._UnitOfWork)
										.ConverTo_Group(CreateGroupView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    GroupBLO.Save(Group);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle(), msg_Group.SingularName, Group), NotificationType.success);
					return RedirectToAction("Index");
                }
                catch (GAppDbException ex)
                {
					dataBaseException = true;
                    Alert(ex.Message, NotificationType.error);
                }
            }
			if (!dataBaseException)
            {
                Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            }
			msgHelper.Create(msg);
			ViewBag.SpecialtyId = new SelectList(new SpecialtyBLO(this._UnitOfWork).FindAll(), "Id", "Code", Group.SpecialtyId);
			ViewBag.TrainingTypeId = new SelectList(new TrainingTypeBLO(this._UnitOfWork).FindAll(), "Id", "Code", Group.TrainingTypeId);
			ViewBag.TrainingYearId = new SelectList(new TrainingYearBLO(this._UnitOfWork).FindAll(), "Id", "Code", Group.TrainingYearId);
			ViewBag.YearStudyId = new SelectList(new YearStudyBLO(this._UnitOfWork).FindAll(), "Id", "Code", Group.YearStudyId);
			return View(CreateGroupView);
        }


		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			ViewBag.SpecialtyId = new SelectList(new SpecialtyBLO(this._UnitOfWork).FindAll(), "Id", "Code");
			ViewBag.TrainingTypeId = new SelectList(new TrainingTypeBLO(this._UnitOfWork).FindAll(), "Id", "Code");
			ViewBag.TrainingYearId = new SelectList(new TrainingYearBLO(this._UnitOfWork).FindAll(), "Id", "Code");
			ViewBag.YearStudyId = new SelectList(new YearStudyBLO(this._UnitOfWork).FindAll(), "Id", "Code");
            Group group = new Group();
            CreateGroupView creategroupview = new CreateGroupViewBLM(this._UnitOfWork)
                                        .ConverTo_CreateGroupView(group);
            return View(creategroupview);
        } 
		 
       


		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group Group = GroupBLO.FindBaseEntityByID((long)id);
            if (Group == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Group.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }			 
			EditGroupView EditGroupView = new EditGroupViewBLM(this._UnitOfWork)
                                                                .ConverTo_EditGroupView(Group) ;

			ViewBag.SpecialtyId = new SelectList(new SpecialtyBLO(this._UnitOfWork).FindAll(), "Id", "Code", EditGroupView.SpecialtyId);
			ViewBag.TrainingTypeId = new SelectList(new TrainingTypeBLO(this._UnitOfWork).FindAll(), "Id", "Code", EditGroupView.TrainingTypeId);
			ViewBag.TrainingYearId = new SelectList(new TrainingYearBLO(this._UnitOfWork).FindAll(), "Id", "Code", EditGroupView.TrainingYearId);
			ViewBag.YearStudyId = new SelectList(new YearStudyBLO(this._UnitOfWork).FindAll(), "Id", "Code", EditGroupView.YearStudyId);
 
			return View(EditGroupView);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit([Bind(Include = "TrainingYearId,SpecialtyId,TrainingTypeId,YearStudyId,Code,Id")] EditGroupView EditGroupView)	
        {
			Group Group = new EditGroupViewBLM(this._UnitOfWork)
                .ConverTo_Group( EditGroupView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    GroupBLO.Save(Group);
					Alert(string.Format(msgManager.The_entity_has_been_changed,msgHelper.DefinitArticle(), msg_Group.SingularName, Group), NotificationType.success);
					return RedirectToAction("Index");
                }
                catch (GAppDbException ex)
                {
					dataBaseException = true;
                    Alert(ex.Message, NotificationType.error);
                }
            }
			if (!dataBaseException) 
            {
                Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            }
			msgHelper.Edit(msg);

			ViewBag.SpecialtyId = new SelectList(new SpecialtyBLO(this._UnitOfWork).FindAll(), "Id", "Code", EditGroupView.SpecialtyId);
			ViewBag.TrainingTypeId = new SelectList(new TrainingTypeBLO(this._UnitOfWork).FindAll(), "Id", "Code", EditGroupView.TrainingTypeId);
			ViewBag.TrainingYearId = new SelectList(new TrainingYearBLO(this._UnitOfWork).FindAll(), "Id", "Code", EditGroupView.TrainingYearId);
			ViewBag.YearStudyId = new SelectList(new YearStudyBLO(this._UnitOfWork).FindAll(), "Id", "Code", EditGroupView.YearStudyId);
			return View(EditGroupView);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group Group = GroupBLO.FindBaseEntityByID((long) id);
            if (Group == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Group.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
			DetailsGroupView DetailsGroupView = new DetailsGroupView();
		    DetailsGroupView = new DetailsGroupViewBLM(this._UnitOfWork)
                .ConverTo_DetailsGroupView(Group);


			return View(DetailsGroupView);
        } 

		 public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group Group = GroupBLO.FindBaseEntityByID((long)id);
            if (Group == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Group.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			DetailsGroupView DetailsGroupView = new DetailsGroupViewBLM(this._UnitOfWork)
							.ConverTo_DetailsGroupView(Group);


			 return View(DetailsGroupView);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			Group Group = GroupBLO.FindBaseEntityByID((long)id);
			if (Group == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Group.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            
			try
            {
                GroupBLO.Delete(Group);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, Group.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppDbException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed,msgHelper.DefinitArticle(), msg_Group.SingularName, Group), NotificationType.success);
            return RedirectToAction("Index");
        }

		public virtual ActionResult Import()
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
                ImportReport importReport = GroupBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/Groups/LastRepportFile\">Télécharger le rapport d'importation</a>";
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


        public virtual FileResult Export()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dataTable = GroupBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_Group.PluralName + ".xlsx");
                }
            }
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                GroupBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class GroupsController : BaseGroupsController{};
}

