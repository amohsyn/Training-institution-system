//modelType = Default_Group_Export_Model

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
	public partial class BaseDefault_Group_Export_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Group_Export_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Group ConverTo_Group(Default_Group_Export_Model Default_Group_Export_Model)
        {
			Group Group = null;
            if (Default_Group_Export_Model.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Group_Export_Model.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.TrainingType = Default_Group_Export_Model.TrainingType;
			Group.TrainingYear = Default_Group_Export_Model.TrainingYear;
			Group.Specialty = Default_Group_Export_Model.Specialty;
			Group.YearStudy = Default_Group_Export_Model.YearStudy;
			Group.Code = Default_Group_Export_Model.Code;
			Group.Description = Default_Group_Export_Model.Description;
			Group.Trainees = Default_Group_Export_Model.Trainees;
			Group.Id = Default_Group_Export_Model.Id;
            return Group;
        }
        public virtual Default_Group_Export_Model ConverTo_Default_Group_Export_Model(Group Group)
        {  
			Default_Group_Export_Model Default_Group_Export_Model = new Default_Group_Export_Model();
			Default_Group_Export_Model.toStringValue = Group.ToString();
			Default_Group_Export_Model.TrainingType = Group.TrainingType;
			Default_Group_Export_Model.TrainingYear = Group.TrainingYear;
			Default_Group_Export_Model.Specialty = Group.Specialty;
			Default_Group_Export_Model.YearStudy = Group.YearStudy;
			Default_Group_Export_Model.Code = Group.Code;
			Default_Group_Export_Model.Description = Group.Description;
			Default_Group_Export_Model.Trainees = Group.Trainees;
			Default_Group_Export_Model.Id = Group.Id;
            return Default_Group_Export_Model;            
        }

		public virtual Default_Group_Export_Model CreateNew()
        {
            Group Group = new GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Group_Export_Model Default_Group_Export_Model = this.ConverTo_Default_Group_Export_Model(Group);
            return Default_Group_Export_Model;
        } 

		public virtual List<Default_Group_Export_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            GroupBLO entityBLO = new GroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Group> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Group_Export_Model> ls_models = new List<Default_Group_Export_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Group_Export_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Group_Export_ModelBLM : BaseDefault_Group_Export_Model_BLM
	{
		public Default_Group_Export_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
