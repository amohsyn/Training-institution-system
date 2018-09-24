//modelType = Default_Details_Classroom_Model

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
	public partial class BaseDefault_Details_Classroom_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Classroom_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Classroom ConverTo_Classroom(Default_Details_Classroom_Model Default_Details_Classroom_Model)
        {
			Classroom Classroom = null;
            if (Default_Details_Classroom_Model.Id != 0)
            {
                Classroom = new ClassroomBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Classroom_Model.Id);
            }
            else
            {
                Classroom = new Classroom();
            } 
			Classroom.Code = Default_Details_Classroom_Model.Code;
			Classroom.Name = Default_Details_Classroom_Model.Name;
			Classroom.ClassroomCategory = Default_Details_Classroom_Model.ClassroomCategory;
			Classroom.Description = Default_Details_Classroom_Model.Description;
			Classroom.Id = Default_Details_Classroom_Model.Id;
            return Classroom;
        }
        public virtual Default_Details_Classroom_Model ConverTo_Default_Details_Classroom_Model(Classroom Classroom)
        {  
			Default_Details_Classroom_Model Default_Details_Classroom_Model = new Default_Details_Classroom_Model();
			Default_Details_Classroom_Model.toStringValue = Classroom.ToString();
			Default_Details_Classroom_Model.Code = Classroom.Code;
			Default_Details_Classroom_Model.Name = Classroom.Name;
			Default_Details_Classroom_Model.ClassroomCategory = Classroom.ClassroomCategory;
			Default_Details_Classroom_Model.Description = Classroom.Description;
			Default_Details_Classroom_Model.Id = Classroom.Id;
            return Default_Details_Classroom_Model;            
        }

		public virtual Default_Details_Classroom_Model CreateNew()
        {
            Classroom Classroom = new ClassroomBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Classroom_Model Default_Details_Classroom_Model = this.ConverTo_Default_Details_Classroom_Model(Classroom);
            return Default_Details_Classroom_Model;
        } 

        public List<Default_Details_Classroom_Model> Find(string OrderBy, string FilterBy,  string SearchBy, List<string> SearchCreteria, int? CurrentPage, int? PageSize, out int totalRecords)
        {
            ClassroomBLO entityBLO = new ClassroomBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Classroom> Query_Entity = entityBLO
                .Find_as_Queryable(OrderBy, FilterBy, SearchBy, SearchCreteria, CurrentPage, PageSize, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Classroom_Model> ls_models = new List<Default_Details_Classroom_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Classroom_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Classroom_ModelBLM : BaseDefault_Details_Classroom_ModelBLM
	{
		public Default_Details_Classroom_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
