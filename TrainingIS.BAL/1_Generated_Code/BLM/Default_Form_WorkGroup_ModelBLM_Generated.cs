//modelType = Default_Form_WorkGroup_Model

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
	public partial class BaseDefault_Form_WorkGroup_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_WorkGroup_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual WorkGroup ConverTo_WorkGroup(Default_Form_WorkGroup_Model Default_Form_WorkGroup_Model)
        {
			WorkGroup WorkGroup = null;
            if (Default_Form_WorkGroup_Model.Id != 0)
            {
                WorkGroup = new WorkGroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_WorkGroup_Model.Id);
            }
            else
            {
                WorkGroup = new WorkGroup();
            } 
			WorkGroup.Name = Default_Form_WorkGroup_Model.Name;
			WorkGroup.Code = Default_Form_WorkGroup_Model.Code;
			WorkGroup.Description = Default_Form_WorkGroup_Model.Description;
			// MemebersFormers
            FormerBLO MemebersFormersBLO = new FormerBLO(this.UnitOfWork,this.GAppContext);
			if (WorkGroup.MemebersFormers != null)
                WorkGroup.MemebersFormers.Clear();
            else
                WorkGroup.MemebersFormers = new List<Former>();
			if(Default_Form_WorkGroup_Model.Selected_MemebersFormers != null)
			{
				foreach (string Selected_Former_Id in Default_Form_WorkGroup_Model.Selected_MemebersFormers)
				{
					Int64 Selected_Former_Id_Int64 = Convert.ToInt64(Selected_Former_Id);
					Former Former =MemebersFormersBLO.FindBaseEntityByID(Selected_Former_Id_Int64);
					WorkGroup.MemebersFormers.Add(Former);
				}
			}
	
			// MemebersAdministrators
            AdministratorBLO MemebersAdministratorsBLO = new AdministratorBLO(this.UnitOfWork,this.GAppContext);
			if (WorkGroup.MemebersAdministrators != null)
                WorkGroup.MemebersAdministrators.Clear();
            else
                WorkGroup.MemebersAdministrators = new List<Administrator>();
			if(Default_Form_WorkGroup_Model.Selected_MemebersAdministrators != null)
			{
				foreach (string Selected_Administrator_Id in Default_Form_WorkGroup_Model.Selected_MemebersAdministrators)
				{
					Int64 Selected_Administrator_Id_Int64 = Convert.ToInt64(Selected_Administrator_Id);
					Administrator Administrator =MemebersAdministratorsBLO.FindBaseEntityByID(Selected_Administrator_Id_Int64);
					WorkGroup.MemebersAdministrators.Add(Administrator);
				}
			}
	
			// MemebersTrainees
            TraineeBLO MemebersTraineesBLO = new TraineeBLO(this.UnitOfWork,this.GAppContext);
			if (WorkGroup.MemebersTrainees != null)
                WorkGroup.MemebersTrainees.Clear();
            else
                WorkGroup.MemebersTrainees = new List<Trainee>();
			if(Default_Form_WorkGroup_Model.Selected_MemebersTrainees != null)
			{
				foreach (string Selected_Trainee_Id in Default_Form_WorkGroup_Model.Selected_MemebersTrainees)
				{
					Int64 Selected_Trainee_Id_Int64 = Convert.ToInt64(Selected_Trainee_Id);
					Trainee Trainee =MemebersTraineesBLO.FindBaseEntityByID(Selected_Trainee_Id_Int64);
					WorkGroup.MemebersTrainees.Add(Trainee);
				}
			}
	
			WorkGroup.GuestFormers = Default_Form_WorkGroup_Model.GuestFormers;
			WorkGroup.GuestTrainees = Default_Form_WorkGroup_Model.GuestTrainees;
			WorkGroup.GuestAdministrator = Default_Form_WorkGroup_Model.GuestAdministrator;
			// Mission_Working_Groups
            Mission_Working_GroupBLO Mission_Working_GroupsBLO = new Mission_Working_GroupBLO(this.UnitOfWork,this.GAppContext);
			if (WorkGroup.Mission_Working_Groups != null)
                WorkGroup.Mission_Working_Groups.Clear();
            else
                WorkGroup.Mission_Working_Groups = new List<Mission_Working_Group>();
			if(Default_Form_WorkGroup_Model.Selected_Mission_Working_Groups != null)
			{
				foreach (string Selected_Mission_Working_Group_Id in Default_Form_WorkGroup_Model.Selected_Mission_Working_Groups)
				{
					Int64 Selected_Mission_Working_Group_Id_Int64 = Convert.ToInt64(Selected_Mission_Working_Group_Id);
					Mission_Working_Group Mission_Working_Group =Mission_Working_GroupsBLO.FindBaseEntityByID(Selected_Mission_Working_Group_Id_Int64);
					WorkGroup.Mission_Working_Groups.Add(Mission_Working_Group);
				}
			}
	
			WorkGroup.President = Default_Form_WorkGroup_Model.President;
			WorkGroup.VicePresident = Default_Form_WorkGroup_Model.VicePresident;
			WorkGroup.Protractor = Default_Form_WorkGroup_Model.Protractor;
			WorkGroup.Id = Default_Form_WorkGroup_Model.Id;
            return WorkGroup;
        }
        public virtual Default_Form_WorkGroup_Model ConverTo_Default_Form_WorkGroup_Model(WorkGroup WorkGroup)
        {  
			Default_Form_WorkGroup_Model Default_Form_WorkGroup_Model = new Default_Form_WorkGroup_Model();
			Default_Form_WorkGroup_Model.toStringValue = WorkGroup.ToString();
			Default_Form_WorkGroup_Model.Name = WorkGroup.Name;
			Default_Form_WorkGroup_Model.Code = WorkGroup.Code;
			Default_Form_WorkGroup_Model.Description = WorkGroup.Description;

			// MemebersFormers
            if (WorkGroup.MemebersFormers != null && WorkGroup.MemebersFormers.Count > 0)
            {
                Default_Form_WorkGroup_Model.Selected_MemebersFormers = WorkGroup
                                                        .MemebersFormers
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_Form_WorkGroup_Model.Selected_MemebersFormers = new List<string>();
            }			

			// MemebersAdministrators
            if (WorkGroup.MemebersAdministrators != null && WorkGroup.MemebersAdministrators.Count > 0)
            {
                Default_Form_WorkGroup_Model.Selected_MemebersAdministrators = WorkGroup
                                                        .MemebersAdministrators
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_Form_WorkGroup_Model.Selected_MemebersAdministrators = new List<string>();
            }			

			// MemebersTrainees
            if (WorkGroup.MemebersTrainees != null && WorkGroup.MemebersTrainees.Count > 0)
            {
                Default_Form_WorkGroup_Model.Selected_MemebersTrainees = WorkGroup
                                                        .MemebersTrainees
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_Form_WorkGroup_Model.Selected_MemebersTrainees = new List<string>();
            }			
			Default_Form_WorkGroup_Model.GuestFormers = WorkGroup.GuestFormers;
			Default_Form_WorkGroup_Model.GuestTrainees = WorkGroup.GuestTrainees;
			Default_Form_WorkGroup_Model.GuestAdministrator = WorkGroup.GuestAdministrator;

			// Mission_Working_Groups
            if (WorkGroup.Mission_Working_Groups != null && WorkGroup.Mission_Working_Groups.Count > 0)
            {
                Default_Form_WorkGroup_Model.Selected_Mission_Working_Groups = WorkGroup
                                                        .Mission_Working_Groups
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_Form_WorkGroup_Model.Selected_Mission_Working_Groups = new List<string>();
            }			
			Default_Form_WorkGroup_Model.President = WorkGroup.President;
			Default_Form_WorkGroup_Model.VicePresident = WorkGroup.VicePresident;
			Default_Form_WorkGroup_Model.Protractor = WorkGroup.Protractor;
			Default_Form_WorkGroup_Model.Id = WorkGroup.Id;
            return Default_Form_WorkGroup_Model;            
        }

		public virtual Default_Form_WorkGroup_Model CreateNew()
        {
            WorkGroup WorkGroup = new WorkGroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_WorkGroup_Model Default_Form_WorkGroup_Model = this.ConverTo_Default_Form_WorkGroup_Model(WorkGroup);
            return Default_Form_WorkGroup_Model;
        } 

		public virtual List<Default_Form_WorkGroup_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            WorkGroupBLO entityBLO = new WorkGroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<WorkGroup> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_WorkGroup_Model> ls_models = new List<Default_Form_WorkGroup_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_WorkGroup_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_WorkGroup_ModelBLM : BaseDefault_Form_WorkGroup_ModelBLM
	{
		public Default_Form_WorkGroup_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
