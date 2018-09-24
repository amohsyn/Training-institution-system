//modelType = Default_Details_Group_Model

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

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_Group_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Group_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Group ConverTo_Group(Default_Details_Group_Model Default_Details_Group_Model)
        {
			Group Group = null;
            if (Default_Details_Group_Model.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Group_Model.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.TrainingType = Default_Details_Group_Model.TrainingType;
			Group.TrainingYear = Default_Details_Group_Model.TrainingYear;
			Group.Specialty = Default_Details_Group_Model.Specialty;
			Group.YearStudy = Default_Details_Group_Model.YearStudy;
			Group.Code = Default_Details_Group_Model.Code;
			Group.Description = Default_Details_Group_Model.Description;
			Group.Trainees = Default_Details_Group_Model.Trainees;
			Group.Id = Default_Details_Group_Model.Id;
            return Group;
        }
        public virtual Default_Details_Group_Model ConverTo_Default_Details_Group_Model(Group Group)
        {  
			Default_Details_Group_Model Default_Details_Group_Model = new Default_Details_Group_Model();
			Default_Details_Group_Model.toStringValue = Group.ToString();
			Default_Details_Group_Model.TrainingType = Group.TrainingType;
			Default_Details_Group_Model.TrainingYear = Group.TrainingYear;
			Default_Details_Group_Model.Specialty = Group.Specialty;
			Default_Details_Group_Model.YearStudy = Group.YearStudy;
			Default_Details_Group_Model.Code = Group.Code;
			Default_Details_Group_Model.Description = Group.Description;
			Default_Details_Group_Model.Trainees = Group.Trainees;
			Default_Details_Group_Model.Id = Group.Id;
            return Default_Details_Group_Model;            
        }

		public virtual Default_Details_Group_Model CreateNew()
        {
            Group Group = new GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Group_Model Default_Details_Group_Model = this.ConverTo_Default_Details_Group_Model(Group);
            return Default_Details_Group_Model;
        } 

        public List<Default_Details_Group_Model> Find(string OrderBy, string FilterBy,  string SearchBy, List<string> SearchCreteria, int? CurrentPage, int? PageSize, out int totalRecords)
        {
            GroupBLO entityBLO = new GroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Group> Query_Entity = entityBLO
                .Find_as_Queryable(OrderBy, FilterBy, SearchBy, SearchCreteria, CurrentPage, PageSize, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Group_Model> ls_models = new List<Default_Details_Group_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Group_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Group_ModelBLM : BaseDefault_Details_Group_ModelBLM
	{
		public Default_Details_Group_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
