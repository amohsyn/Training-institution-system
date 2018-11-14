//modelType = Default_WorkGroup_Details_Model

using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.DAL;
using GApp.Core.Context;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_WorkGroup_Details_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_WorkGroup_Details_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual WorkGroup ConverTo_WorkGroup(Default_WorkGroup_Details_Model Default_WorkGroup_Details_Model)
        {
			WorkGroup WorkGroup = null;
            if (Default_WorkGroup_Details_Model.Id != 0)
            {
                WorkGroup = new WorkGroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_WorkGroup_Details_Model.Id);
            }
            else
            {
                WorkGroup = new WorkGroup();
            } 
			WorkGroup.Name = Default_WorkGroup_Details_Model.Name;
			WorkGroup.Code = Default_WorkGroup_Details_Model.Code;
			WorkGroup.Description = Default_WorkGroup_Details_Model.Description;
			WorkGroup.President_Former = Default_WorkGroup_Details_Model.President_Former;
			WorkGroup.President_Trainee = Default_WorkGroup_Details_Model.President_Trainee;
			WorkGroup.President_Administrator = Default_WorkGroup_Details_Model.President_Administrator;
			WorkGroup.VicePresident_Former = Default_WorkGroup_Details_Model.VicePresident_Former;
			WorkGroup.VicePresident_Trainee = Default_WorkGroup_Details_Model.VicePresident_Trainee;
			WorkGroup.VicePresident_Administrator = Default_WorkGroup_Details_Model.VicePresident_Administrator;
			WorkGroup.Protractor_Former = Default_WorkGroup_Details_Model.Protractor_Former;
			WorkGroup.Protractor_Administrator = Default_WorkGroup_Details_Model.Protractor_Administrator;
			WorkGroup.Protractor_Trainee = Default_WorkGroup_Details_Model.Protractor_Trainee;
			WorkGroup.MemebersFormers = Default_WorkGroup_Details_Model.MemebersFormers;
			WorkGroup.MemebersAdministrators = Default_WorkGroup_Details_Model.MemebersAdministrators;
			WorkGroup.MemebersTrainees = Default_WorkGroup_Details_Model.MemebersTrainees;
			WorkGroup.GuestFormers = Default_WorkGroup_Details_Model.GuestFormers;
			WorkGroup.GuestTrainees = Default_WorkGroup_Details_Model.GuestTrainees;
			WorkGroup.GuestAdministrator = Default_WorkGroup_Details_Model.GuestAdministrator;
			WorkGroup.Mission_Working_Groups = Default_WorkGroup_Details_Model.Mission_Working_Groups;
			WorkGroup.President = Default_WorkGroup_Details_Model.President;
			WorkGroup.VicePresident = Default_WorkGroup_Details_Model.VicePresident;
			WorkGroup.Protractor = Default_WorkGroup_Details_Model.Protractor;
			WorkGroup.Id = Default_WorkGroup_Details_Model.Id;
            return WorkGroup;
        }
        public virtual Default_WorkGroup_Details_Model ConverTo_Default_WorkGroup_Details_Model(WorkGroup WorkGroup)
        {  
			Default_WorkGroup_Details_Model Default_WorkGroup_Details_Model = new Default_WorkGroup_Details_Model();
			Default_WorkGroup_Details_Model.toStringValue = WorkGroup.ToString();
			Default_WorkGroup_Details_Model.Name = WorkGroup.Name;
			Default_WorkGroup_Details_Model.Code = WorkGroup.Code;
			Default_WorkGroup_Details_Model.Description = WorkGroup.Description;
			Default_WorkGroup_Details_Model.President_Former = WorkGroup.President_Former;
			Default_WorkGroup_Details_Model.President_Trainee = WorkGroup.President_Trainee;
			Default_WorkGroup_Details_Model.President_Administrator = WorkGroup.President_Administrator;
			Default_WorkGroup_Details_Model.VicePresident_Former = WorkGroup.VicePresident_Former;
			Default_WorkGroup_Details_Model.VicePresident_Trainee = WorkGroup.VicePresident_Trainee;
			Default_WorkGroup_Details_Model.VicePresident_Administrator = WorkGroup.VicePresident_Administrator;
			Default_WorkGroup_Details_Model.Protractor_Former = WorkGroup.Protractor_Former;
			Default_WorkGroup_Details_Model.Protractor_Administrator = WorkGroup.Protractor_Administrator;
			Default_WorkGroup_Details_Model.Protractor_Trainee = WorkGroup.Protractor_Trainee;
			Default_WorkGroup_Details_Model.MemebersFormers = WorkGroup.MemebersFormers;
			Default_WorkGroup_Details_Model.MemebersAdministrators = WorkGroup.MemebersAdministrators;
			Default_WorkGroup_Details_Model.MemebersTrainees = WorkGroup.MemebersTrainees;
			Default_WorkGroup_Details_Model.GuestFormers = WorkGroup.GuestFormers;
			Default_WorkGroup_Details_Model.GuestTrainees = WorkGroup.GuestTrainees;
			Default_WorkGroup_Details_Model.GuestAdministrator = WorkGroup.GuestAdministrator;
			Default_WorkGroup_Details_Model.Mission_Working_Groups = WorkGroup.Mission_Working_Groups;
			Default_WorkGroup_Details_Model.President = WorkGroup.President;
			Default_WorkGroup_Details_Model.VicePresident = WorkGroup.VicePresident;
			Default_WorkGroup_Details_Model.Protractor = WorkGroup.Protractor;
			Default_WorkGroup_Details_Model.Id = WorkGroup.Id;
            return Default_WorkGroup_Details_Model;            
        }

		public virtual Default_WorkGroup_Details_Model CreateNew()
        {
            WorkGroup WorkGroup = new WorkGroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_WorkGroup_Details_Model Default_WorkGroup_Details_Model = this.ConverTo_Default_WorkGroup_Details_Model(WorkGroup);
            return Default_WorkGroup_Details_Model;
        } 

		public virtual List<Default_WorkGroup_Details_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            WorkGroupBLO entityBLO = new WorkGroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<WorkGroup> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_WorkGroup_Details_Model> ls_models = new List<Default_WorkGroup_Details_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_WorkGroup_Details_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_WorkGroup_Details_ModelBLM : BaseDefault_WorkGroup_Details_Model_BLM
	{
		public Default_WorkGroup_Details_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
