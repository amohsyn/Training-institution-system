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
			WorkGroup.Code = Default_Form_WorkGroup_Model.Code;
			WorkGroup.Name = Default_Form_WorkGroup_Model.Name;
			// Former
            FormerBLO FormerBLO = new FormerBLO(this.UnitOfWork,this.GAppContext);

			if (WorkGroup.Formers != null)
                WorkGroup.Formers.Clear();
            else
                WorkGroup.Formers = new List<Former>();

			if(Default_Form_WorkGroup_Model.Selected_Formers != null)
			{
				foreach (string Selected_Former_Id in Default_Form_WorkGroup_Model.Selected_Formers)
				{
					Int64 Selected_Former_Id_Int64 = Convert.ToInt64(Selected_Former_Id);
					Former Former =FormerBLO.FindBaseEntityByID(Selected_Former_Id_Int64);
					WorkGroup.Formers.Add(Former);
				}
			}
	
			// Administrator
            AdministratorBLO AdministratorBLO = new AdministratorBLO(this.UnitOfWork,this.GAppContext);

			if (WorkGroup.Administrators != null)
                WorkGroup.Administrators.Clear();
            else
                WorkGroup.Administrators = new List<Administrator>();

			if(Default_Form_WorkGroup_Model.Selected_Administrators != null)
			{
				foreach (string Selected_Administrator_Id in Default_Form_WorkGroup_Model.Selected_Administrators)
				{
					Int64 Selected_Administrator_Id_Int64 = Convert.ToInt64(Selected_Administrator_Id);
					Administrator Administrator =AdministratorBLO.FindBaseEntityByID(Selected_Administrator_Id_Int64);
					WorkGroup.Administrators.Add(Administrator);
				}
			}
	
			// Trainee
            TraineeBLO TraineeBLO = new TraineeBLO(this.UnitOfWork,this.GAppContext);

			if (WorkGroup.Trainees != null)
                WorkGroup.Trainees.Clear();
            else
                WorkGroup.Trainees = new List<Trainee>();

			if(Default_Form_WorkGroup_Model.Selected_Trainees != null)
			{
				foreach (string Selected_Trainee_Id in Default_Form_WorkGroup_Model.Selected_Trainees)
				{
					Int64 Selected_Trainee_Id_Int64 = Convert.ToInt64(Selected_Trainee_Id);
					Trainee Trainee =TraineeBLO.FindBaseEntityByID(Selected_Trainee_Id_Int64);
					WorkGroup.Trainees.Add(Trainee);
				}
			}
	
			// Mission_Working_Group
            Mission_Working_GroupBLO Mission_Working_GroupBLO = new Mission_Working_GroupBLO(this.UnitOfWork,this.GAppContext);

			if (WorkGroup.Mission_Working_Groups != null)
                WorkGroup.Mission_Working_Groups.Clear();
            else
                WorkGroup.Mission_Working_Groups = new List<Mission_Working_Group>();

			if(Default_Form_WorkGroup_Model.Selected_Mission_Working_Groups != null)
			{
				foreach (string Selected_Mission_Working_Group_Id in Default_Form_WorkGroup_Model.Selected_Mission_Working_Groups)
				{
					Int64 Selected_Mission_Working_Group_Id_Int64 = Convert.ToInt64(Selected_Mission_Working_Group_Id);
					Mission_Working_Group Mission_Working_Group =Mission_Working_GroupBLO.FindBaseEntityByID(Selected_Mission_Working_Group_Id_Int64);
					WorkGroup.Mission_Working_Groups.Add(Mission_Working_Group);
				}
			}
	
			WorkGroup.Description = Default_Form_WorkGroup_Model.Description;
			WorkGroup.Id = Default_Form_WorkGroup_Model.Id;
            return WorkGroup;
        }
        public virtual Default_Form_WorkGroup_Model ConverTo_Default_Form_WorkGroup_Model(WorkGroup WorkGroup)
        {  
			Default_Form_WorkGroup_Model Default_Form_WorkGroup_Model = new Default_Form_WorkGroup_Model();
			Default_Form_WorkGroup_Model.toStringValue = WorkGroup.ToString();
			Default_Form_WorkGroup_Model.Code = WorkGroup.Code;
			Default_Form_WorkGroup_Model.Name = WorkGroup.Name;

			// Former
            if (WorkGroup.Formers != null && WorkGroup.Formers.Count > 0)
            {
                Default_Form_WorkGroup_Model.Selected_Formers = WorkGroup
                                                        .Formers
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_Form_WorkGroup_Model.Selected_Formers = new List<string>();
            }			

			// Administrator
            if (WorkGroup.Administrators != null && WorkGroup.Administrators.Count > 0)
            {
                Default_Form_WorkGroup_Model.Selected_Administrators = WorkGroup
                                                        .Administrators
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_Form_WorkGroup_Model.Selected_Administrators = new List<string>();
            }			

			// Trainee
            if (WorkGroup.Trainees != null && WorkGroup.Trainees.Count > 0)
            {
                Default_Form_WorkGroup_Model.Selected_Trainees = WorkGroup
                                                        .Trainees
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_Form_WorkGroup_Model.Selected_Trainees = new List<string>();
            }			

			// Mission_Working_Group
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
			Default_Form_WorkGroup_Model.Description = WorkGroup.Description;
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
