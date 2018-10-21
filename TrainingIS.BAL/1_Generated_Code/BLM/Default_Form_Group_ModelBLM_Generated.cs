//modelType = Default_Form_Group_Model

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
	public partial class BaseDefault_Form_Group_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Group_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Group ConverTo_Group(Default_Form_Group_Model Default_Form_Group_Model)
        {
			Group Group = null;
            if (Default_Form_Group_Model.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Group_Model.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.TrainingTypeId = Default_Form_Group_Model.TrainingTypeId;
			Group.TrainingType = new TrainingTypeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Group_Model.TrainingTypeId)) ;
			Group.TrainingYearId = Default_Form_Group_Model.TrainingYearId;
			Group.TrainingYear = new TrainingYearBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Group_Model.TrainingYearId)) ;
			Group.SpecialtyId = Default_Form_Group_Model.SpecialtyId;
			Group.Specialty = new SpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Group_Model.SpecialtyId)) ;
			Group.YearStudyId = Default_Form_Group_Model.YearStudyId;
			Group.YearStudy = new YearStudyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Group_Model.YearStudyId)) ;
			Group.Code = Default_Form_Group_Model.Code;
			Group.Description = Default_Form_Group_Model.Description;
			// Trainees
            TraineeBLO TraineesBLO = new TraineeBLO(this.UnitOfWork,this.GAppContext);
			if (Group.Trainees != null)
                Group.Trainees.Clear();
            else
                Group.Trainees = new List<Trainee>();
			if(Default_Form_Group_Model.Selected_Trainees != null)
			{
				foreach (string Selected_Trainee_Id in Default_Form_Group_Model.Selected_Trainees)
				{
					Int64 Selected_Trainee_Id_Int64 = Convert.ToInt64(Selected_Trainee_Id);
					Trainee Trainee =TraineesBLO.FindBaseEntityByID(Selected_Trainee_Id_Int64);
					Group.Trainees.Add(Trainee);
				}
			}
	
			Group.Id = Default_Form_Group_Model.Id;
            return Group;
        }
        public virtual Default_Form_Group_Model ConverTo_Default_Form_Group_Model(Group Group)
        {  
			Default_Form_Group_Model Default_Form_Group_Model = new Default_Form_Group_Model();
			Default_Form_Group_Model.toStringValue = Group.ToString();
			Default_Form_Group_Model.TrainingTypeId = Group.TrainingTypeId;
			Default_Form_Group_Model.TrainingYearId = Group.TrainingYearId;
			Default_Form_Group_Model.SpecialtyId = Group.SpecialtyId;
			Default_Form_Group_Model.YearStudyId = Group.YearStudyId;
			Default_Form_Group_Model.Code = Group.Code;
			Default_Form_Group_Model.Description = Group.Description;

			// Trainees
            if (Group.Trainees != null && Group.Trainees.Count > 0)
            {
                Default_Form_Group_Model.Selected_Trainees = Group
                                                        .Trainees
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_Form_Group_Model.Selected_Trainees = new List<string>();
            }			
			Default_Form_Group_Model.Id = Group.Id;
            return Default_Form_Group_Model;            
        }

		public virtual Default_Form_Group_Model CreateNew()
        {
            Group Group = new GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Group_Model Default_Form_Group_Model = this.ConverTo_Default_Form_Group_Model(Group);
            return Default_Form_Group_Model;
        } 

		public virtual List<Default_Form_Group_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            GroupBLO entityBLO = new GroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Group> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_Group_Model> ls_models = new List<Default_Form_Group_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_Group_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_Group_ModelBLM : BaseDefault_Form_Group_ModelBLM
	{
		public Default_Form_Group_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
