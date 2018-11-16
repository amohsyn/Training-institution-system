using GApp.BLL.Enums;
using GApp.DAL.Exceptions;
using GApp.Exceptions;
using GApp.Models.DataAnnotations;
using GApp.Models.GAppComponents;
using GApp.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL;
using TrainingIS.BLL.Exceptions;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.Resources.ModuleTrainingResources;
using TrainingIS.Entities.Resources.SeanceTrainingResources;
using TrainingIS.Models.SeanceInfos;
using TrainingIS.Models.SeanceTrainings;
using TrainingIS.WebApp.Manager.Views.msgs;

namespace TrainingIS.WebApp.Controllers
{
    public partial class SeanceTrainingsController
    {
       


        protected override void InitFilter(Index_GAppPage index_page, string FilterBy, string SearchBy)
        {
            Former current_former = new FormerBLO(this._UnitOfWork, this.GAppContext).Get_Current_Former();

            var filters_by_infos = DataTable_GAppComponent.ParseFilterBy(FilterBy).ToList();


            PropertyInfo model_property = null;

            model_property = typeof(SeanceInfo).GetProperty(nameof(SeanceInfo.Group));
            FilterItem_GAppComponent FilterItem_Group = new FilterItem_GAppComponent();
            FilterItem_Group.Id = "Group.Id_Filter";
            FilterItem_Group.Label = model_property.getLocalName();
            FilterItem_Group.Placeholder = model_property.getLocalName();
            FilterItem_Group.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
            var filter_info_Group = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_Group.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if (filter_info_Group != null)
            {
                FilterItem_Group.Selected = filter_info_Group.Value;
            }

            List<Group> All_Data_Group = null;

            if (current_former != null)
            {
                All_Data_Group = new TrainingBLO(this._UnitOfWork, this.GAppContext).Get_Groups_Of_Former(current_former);
            }
            else
            {
                All_Data_Group = new GroupBLO(this._UnitOfWork, this.GAppContext).FindAll();
            }

            string All_Group_msg = string.Format("tous les {0}", msg_SeanceTraining.PluralName.ToLower());
            All_Data_Group.Insert(0, new Group { Id = 0, ToStringValue = All_Group_msg });
            FilterItem_Group.Data = All_Data_Group.ToDictionary(entity => entity.Id.ToString(), entity => entity.ToStringValue);
            index_page.Filter.FilterItems.Add(FilterItem_Group);


            model_property = typeof(SeanceInfo).GetProperty(nameof(SeanceInfo.ModuleTraining));
            FilterItem_GAppComponent FilterItem_ModuleTraining = new FilterItem_GAppComponent();
            FilterItem_ModuleTraining.Id = "ModuleTraining.Id_Filter";
            FilterItem_ModuleTraining.Label = model_property.getLocalName();
            FilterItem_ModuleTraining.Placeholder = model_property.getLocalName();
            FilterItem_ModuleTraining.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
            var filter_info_ModuleTraining = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_ModuleTraining.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if (filter_info_ModuleTraining != null)
            {
                FilterItem_ModuleTraining.Selected = filter_info_ModuleTraining.Value;
            }

            List<ModuleTraining> All_Data_ModuleTraining = null;
            if (current_former != null)
            {
                All_Data_ModuleTraining = new TrainingBLO(this._UnitOfWork, this.GAppContext).Get_ModuleTraining_Of_Former(current_former);
            }
            else
            {
                All_Data_ModuleTraining = new ModuleTrainingBLO(this._UnitOfWork, this.GAppContext).FindAll();
            }


            string All_ModuleTraining_msg = string.Format("tous les {0}", msg_ModuleTraining.PluralName.ToLower());
            All_Data_ModuleTraining.Insert(0, new ModuleTraining { Id = 0, ToStringValue = All_ModuleTraining_msg });

            FilterItem_ModuleTraining.Data = All_Data_ModuleTraining.ToDictionary(entity => entity.Id.ToString(), entity =>
            {
                if (!string.IsNullOrEmpty(entity.Code))
                {
                    return string.Format("{0}:{1}", entity.Code, entity.Name);
                }
                else
                {
                    return entity.ToStringValue;
                }
            }


            );
            index_page.Filter.FilterItems.Add(FilterItem_ModuleTraining);


            //model_property = typeof(Index_SeanceTraining_Model).GetProperty(nameof(Index_SeanceTraining_Model.FormerValidation));
            //FilterItem_GAppComponent FilterItem_FormerValidation = new FilterItem_GAppComponent();
            //FilterItem_FormerValidation.Id = "FormerValidation_Filter";
            //FilterItem_FormerValidation.Label = model_property.getLocalName();
            //FilterItem_FormerValidation.Placeholder = model_property.getLocalName();
            //FilterItem_FormerValidation.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Boolean;
            //var filter_info_FormerValidation = filters_by_infos
            //    .Where(f => f.PropertyName == FilterItem_FormerValidation.Id.RemoveFromEnd("_Filter"))
            //    .FirstOrDefault();
            //if (filter_info_FormerValidation != null)
            //{
            //    FilterItem_FormerValidation.Selected = filter_info_FormerValidation.Value;
            //}

            //index_page.Filter.FilterItems.Add(FilterItem_FormerValidation);


            FilterItem_GAppComponent SeachFilter = new FilterItem_GAppComponent();
            SeachFilter.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Search;
            SeachFilter.Label = "Recherche";
            SeachFilter.Selected = SearchBy;
            SeachFilter.Placeholder = SeachFilter.Label;
            index_page.Filter.FilterItems.Add(SeachFilter);

            // Selected Values
            var Filter_Items = DataTable_GAppComponent.ParseFilterBy(FilterBy);
        }


        protected override List<Header_DataTable_GAppComponent> Get_GAppDataTable_Header_Text_And_Ids()
        {
            List<Header_DataTable_GAppComponent> herders = new List<Header_DataTable_GAppComponent>();

            foreach (PropertyInfo model_property in typeof(SeanceInfo).GetProperties(typeof(GAppDataTableAttribute)))
            {
                GAppDataTableAttribute gappDataTableAttribute = model_property.GetCustomAttribute(typeof(GAppDataTableAttribute)) as GAppDataTableAttribute;
                string OrderBy = string.IsNullOrEmpty(gappDataTableAttribute.OrderBy) ? model_property.Name : gappDataTableAttribute.OrderBy;

                Header_DataTable_GAppComponent header = new Header_DataTable_GAppComponent();
                header.Id = OrderBy;
                header.Name = model_property.getLocalName();
                header.ShortName = model_property.getLocalShortName();
                herders.Add(header);
            }
            return herders;
        }

        /// <summary>
        /// Index by SeanceInfo Model
        /// </summary>
        /// <param name="filterRequestParams"></param>
        /// <returns></returns>
        public override ActionResult Index(FilterRequestParams filterRequestParams)
        {
            msgHelper.Index(msg);
            Int32 _TotalRecords = 0;
            List<string> SearchCreteria = this.SeanceTrainingBLO.GetSearchCreteria();

            List<SeanceInfo> _ListIndex_SeanceInfo_Model = null;
            try
            {
                filterRequestParams = this.Save_OR_Load_filterRequestParams_State(filterRequestParams);

                _ListIndex_SeanceInfo_Model = new SeanceInfoBLM(this._UnitOfWork, this.GAppContext)
                    .Find(filterRequestParams, SearchCreteria, out _TotalRecords);

            }
            catch (Exception ex)
            {
                filterRequestParams = new FilterRequestParams();
                this.Delete_filterRequestParams_State();
                _ListIndex_SeanceInfo_Model = new SeanceInfoBLM(this._UnitOfWork, this.GAppContext)
                  .Find(filterRequestParams, SearchCreteria, out _TotalRecords);
                Alert(ex.Message, NotificationType.warning);
            }

            Index_GAppPage index_page = new Index_GAppPage(filterRequestParams, this.Get_GAppDataTable_Header_Text_And_Ids(), _TotalRecords);
            index_page.Title = msg["Index_Title"];
            this.InitFilter(index_page, filterRequestParams.FilterBy, filterRequestParams.SearchBy);

            ViewBag.index_page = index_page;

            return View(_ListIndex_SeanceInfo_Model);
        }

        [NonAction]
        public override ActionResult Create()
        {
            return Create(DateTime.Now.ToShortDateString(), null);
        }


        public ActionResult Create(string SeanceDate, string SeancePlanningId)
        {
            if (string.IsNullOrEmpty(SeanceDate))
            {
                SeanceDate = DateTime.Now.Date.ToString();
            }

            DateTime SeanceDate_DateTime = Convert.ToDateTime(SeanceDate);
            Former former = new FormerBLO(this._UnitOfWork, this.GAppContext).Get_Current_Former();
            if (former == null)
            {
                // [Bug] Localization
                string msg_e = string.Format("Vous êtes pas un formateur, cette page est réservée pour les formateurs");
                Alert(msg_e, NotificationType.error);
                return RedirectToAction("Index");
            }

            msgHelper.Create(msg);
            Create_SeanceTraining_Model create_seancetraining_model = new Create_SeanceTraining_ModelBLM(this._UnitOfWork, this.GAppContext).CreateNew(SeanceDate_DateTime, former);

            if (!string.IsNullOrEmpty(SeancePlanningId))
            {
                SeancePlanning seancePlanning = new SeancePlanningBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(Convert.ToInt64(SeancePlanningId));
                if (seancePlanning != null)
                {
                    create_seancetraining_model.SeancePlanningId = seancePlanning.Id;
                    create_seancetraining_model.ModuleTrainingId = seancePlanning.Training.ModuleTrainingId;
                    create_seancetraining_model.SeanceNumberId = seancePlanning.SeanceNumberId;
                    create_seancetraining_model.ClassroomId = seancePlanning.ClassroomId;
                    create_seancetraining_model.GroupId = seancePlanning.Training.GroupId;
                }

            }

            return View(create_seancetraining_model);
        }

        public override ActionResult Create(Create_SeanceTraining_Model Create_SeanceTraining_Model)
        {
            SeanceTraining SeanceTraining = null;
            SeanceTraining = new Create_SeanceTraining_ModelBLM(this._UnitOfWork, this.GAppContext)
                                        .ConverTo_SeanceTraining(Create_SeanceTraining_Model);

            bool dataBaseException = false;
            if (ModelState.IsValid)
            {
                // If SeanceTraining Exist
                string reference = SeanceTraining.CalculateReference();
                SeanceTraining ExistantSeanceTraining = SeanceTrainingBLO
                    .Find(Create_SeanceTraining_Model.SeancePlanningId , Convert.ToDateTime( SeanceTraining.SeanceDate));

                if (ExistantSeanceTraining != null)
                {
                    return RedirectToAction("Edit", new { Id = ExistantSeanceTraining.Id });
                }

                try
                {

                    SeanceTrainingBLO.Save(SeanceTraining);
                    Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_SeanceTraining.SingularName.ToLower(), SeanceTraining), NotificationType.success);
                    return RedirectToAction("Edit", new { Id = SeanceTraining.Id });
                }
                catch (GAppDbException ex)
                {
                    dataBaseException = true;
                    Alert(ex.Message, NotificationType.error);
                }
                catch (GAppException ex)
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
            this.Fill_ViewBag_Create(Create_SeanceTraining_Model);
            Create_SeanceTraining_Model = new Create_SeanceTraining_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Create_SeanceTraining_Model(SeanceTraining);
            return View(Create_SeanceTraining_Model);
        }

        public override ActionResult Edit(long? id)
        {
            return base.Edit(id);
        }

        public override ActionResult DeleteConfirmed(long id)
        {
            string returnUrl = this.HttpContext.Request["returnUrl"] as string;
            if (returnUrl == null)
            {
                return base.DeleteConfirmed(id);
            }
            else
            {
                base.DeleteConfirmed(id);
                return Redirect(returnUrl);
            }

        }


        public ActionResult Calculate_Plurality()
        {
            if (this.GAppContext.Current_User_Name == RoleBLO.Root_ROLE)
            {
                this.SeanceTrainingBLO.Calculate_Plurality();
                string msg_e = string.Format("Le cumule de toutes les seances de formation sont calculées avec succès");
                Alert(msg_e, NotificationType.info);
            }
            else
            {
                string msg_e = string.Format("You must be root to execute this action");
                Alert(msg_e, NotificationType.info);
            }


            return RedirectToAction("Index");



        }

        //public ActionResult Create_Not_Created_SeanceTraining()
        //{
        //    // to not calculate the statisitque
        //    this.GAppContext.Session.Add(ImportService.IMPORT_PROCESS_KEY, "true");

        //    this.SeanceTrainingBLO.Create_Not_Created_SeanceTraining();

        //    string msg_e = string.Format("Tous les seances de formation sont crées");
        //    Alert(msg_e, NotificationType.info);
        //    return RedirectToAction("Index");
        //}
    }
}