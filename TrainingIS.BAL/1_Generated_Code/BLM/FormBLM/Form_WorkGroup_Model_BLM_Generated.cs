//modelType = Form_WorkGroup_Model

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
	public partial class BaseForm_WorkGroup_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseForm_WorkGroup_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual WorkGroup ConverTo_WorkGroup(Form_WorkGroup_Model Form_WorkGroup_Model)
        {
			WorkGroup WorkGroup = null;
            if (Form_WorkGroup_Model.Id != 0)
            {
                WorkGroup = new WorkGroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Form_WorkGroup_Model.Id);
            }
            else
            {
                WorkGroup = new WorkGroup();
            } 
			WorkGroup.Name = Form_WorkGroup_Model.Name;
			WorkGroup.Code = Form_WorkGroup_Model.Code;
			WorkGroup.Description = Form_WorkGroup_Model.Description;
			// MemebersFormers
            FormerBLO MemebersFormersBLO = new FormerBLO(this.UnitOfWork,this.GAppContext);
			if (WorkGroup.MemebersFormers != null)
                WorkGroup.MemebersFormers.Clear();
            else
                WorkGroup.MemebersFormers = new List<Former>();
			if(Form_WorkGroup_Model.Selected_MemebersFormers != null)
			{
				foreach (string Selected_Former_Id in Form_WorkGroup_Model.Selected_MemebersFormers)
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
			if(Form_WorkGroup_Model.Selected_MemebersAdministrators != null)
			{
				foreach (string Selected_Administrator_Id in Form_WorkGroup_Model.Selected_MemebersAdministrators)
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
			if(Form_WorkGroup_Model.Selected_MemebersTrainees != null)
			{
				foreach (string Selected_Trainee_Id in Form_WorkGroup_Model.Selected_MemebersTrainees)
				{
					Int64 Selected_Trainee_Id_Int64 = Convert.ToInt64(Selected_Trainee_Id);
					Trainee Trainee =MemebersTraineesBLO.FindBaseEntityByID(Selected_Trainee_Id_Int64);
					WorkGroup.MemebersTrainees.Add(Trainee);
				}
			}
	
			WorkGroup.GuestFormers = Form_WorkGroup_Model.GuestFormers;
			WorkGroup.GuestTrainees = Form_WorkGroup_Model.GuestTrainees;
			WorkGroup.GuestAdministrator = Form_WorkGroup_Model.GuestAdministrator;
			// Mission_Working_Groups
            Mission_Working_GroupBLO Mission_Working_GroupsBLO = new Mission_Working_GroupBLO(this.UnitOfWork,this.GAppContext);
			if (WorkGroup.Mission_Working_Groups != null)
                WorkGroup.Mission_Working_Groups.Clear();
            else
                WorkGroup.Mission_Working_Groups = new List<Mission_Working_Group>();
			if(Form_WorkGroup_Model.Selected_Mission_Working_Groups != null)
			{
				foreach (string Selected_Mission_Working_Group_Id in Form_WorkGroup_Model.Selected_Mission_Working_Groups)
				{
					Int64 Selected_Mission_Working_Group_Id_Int64 = Convert.ToInt64(Selected_Mission_Working_Group_Id);
					Mission_Working_Group Mission_Working_Group =Mission_Working_GroupsBLO.FindBaseEntityByID(Selected_Mission_Working_Group_Id_Int64);
					WorkGroup.Mission_Working_Groups.Add(Mission_Working_Group);
				}
			}
	
			WorkGroup.Id = Form_WorkGroup_Model.Id;
            return WorkGroup;
        }
        public virtual void ConverTo_Form_WorkGroup_Model(Form_WorkGroup_Model Form_WorkGroup_Model, WorkGroup WorkGroup)
        {  
			 
			Form_WorkGroup_Model.toStringValue = WorkGroup.ToString();
			Form_WorkGroup_Model.Name = WorkGroup.Name;
			Form_WorkGroup_Model.Code = WorkGroup.Code;
			Form_WorkGroup_Model.Description = WorkGroup.Description;

			// MemebersFormers
            if (WorkGroup.MemebersFormers != null && WorkGroup.MemebersFormers.Count > 0)
            {
                Form_WorkGroup_Model.Selected_MemebersFormers = WorkGroup
                                                        .MemebersFormers
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Form_WorkGroup_Model.Selected_MemebersFormers = new List<string>();
            }			

			// MemebersAdministrators
            if (WorkGroup.MemebersAdministrators != null && WorkGroup.MemebersAdministrators.Count > 0)
            {
                Form_WorkGroup_Model.Selected_MemebersAdministrators = WorkGroup
                                                        .MemebersAdministrators
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Form_WorkGroup_Model.Selected_MemebersAdministrators = new List<string>();
            }			

			// MemebersTrainees
            if (WorkGroup.MemebersTrainees != null && WorkGroup.MemebersTrainees.Count > 0)
            {
                Form_WorkGroup_Model.Selected_MemebersTrainees = WorkGroup
                                                        .MemebersTrainees
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Form_WorkGroup_Model.Selected_MemebersTrainees = new List<string>();
            }			
			Form_WorkGroup_Model.GuestFormers = WorkGroup.GuestFormers;
			Form_WorkGroup_Model.GuestTrainees = WorkGroup.GuestTrainees;
			Form_WorkGroup_Model.GuestAdministrator = WorkGroup.GuestAdministrator;

			// Mission_Working_Groups
            if (WorkGroup.Mission_Working_Groups != null && WorkGroup.Mission_Working_Groups.Count > 0)
            {
                Form_WorkGroup_Model.Selected_Mission_Working_Groups = WorkGroup
                                                        .Mission_Working_Groups
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Form_WorkGroup_Model.Selected_Mission_Working_Groups = new List<string>();
            }			
			Form_WorkGroup_Model.Id = WorkGroup.Id;
                     
        }

    }

	public partial class Form_WorkGroup_ModelBLM : BaseForm_WorkGroup_Model_BLM
	{
		public Form_WorkGroup_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
