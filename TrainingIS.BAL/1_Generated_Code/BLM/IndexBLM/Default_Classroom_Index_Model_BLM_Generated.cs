//modelType = Default_Classroom_Index_Model

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
	public partial class BaseDefault_Classroom_Index_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Classroom_Index_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Classroom ConverTo_Classroom(Default_Classroom_Index_Model Default_Classroom_Index_Model)
        {
			Classroom Classroom = null;
            if (Default_Classroom_Index_Model.Id != 0)
            {
                Classroom = new ClassroomBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Classroom_Index_Model.Id);
            }
            else
            {
                Classroom = new Classroom();
            } 
			Classroom.Code = Default_Classroom_Index_Model.Code;
			Classroom.Name = Default_Classroom_Index_Model.Name;
			Classroom.ClassroomCategory = Default_Classroom_Index_Model.ClassroomCategory;
			Classroom.Description = Default_Classroom_Index_Model.Description;
			Classroom.Id = Default_Classroom_Index_Model.Id;
            return Classroom;
        }
        public virtual Default_Classroom_Index_Model ConverTo_Default_Classroom_Index_Model(Classroom Classroom)
        {  
			Default_Classroom_Index_Model Default_Classroom_Index_Model = new Default_Classroom_Index_Model();
			Default_Classroom_Index_Model.toStringValue = Classroom.ToString();
			Default_Classroom_Index_Model.Code = Classroom.Code;
			Default_Classroom_Index_Model.Name = Classroom.Name;
			Default_Classroom_Index_Model.ClassroomCategory = Classroom.ClassroomCategory;
			Default_Classroom_Index_Model.Description = Classroom.Description;
			Default_Classroom_Index_Model.Id = Classroom.Id;
            return Default_Classroom_Index_Model;            
        }

		public virtual Default_Classroom_Index_Model CreateNew()
        {
            Classroom Classroom = new ClassroomBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Classroom_Index_Model Default_Classroom_Index_Model = this.ConverTo_Default_Classroom_Index_Model(Classroom);
            return Default_Classroom_Index_Model;
        } 

		public virtual List<Default_Classroom_Index_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ClassroomBLO entityBLO = new ClassroomBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Classroom> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Classroom_Index_Model> ls_models = new List<Default_Classroom_Index_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Classroom_Index_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Classroom_Index_ModelBLM : BaseDefault_Classroom_Index_Model_BLM
	{
		public Default_Classroom_Index_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
