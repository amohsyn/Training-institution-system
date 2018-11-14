//modelType = Default_ClassroomCategory_Export_Model

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
	public partial class BaseDefault_ClassroomCategory_Export_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_ClassroomCategory_Export_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ClassroomCategory ConverTo_ClassroomCategory(Default_ClassroomCategory_Export_Model Default_ClassroomCategory_Export_Model)
        {
			ClassroomCategory ClassroomCategory = null;
            if (Default_ClassroomCategory_Export_Model.Id != 0)
            {
                ClassroomCategory = new ClassroomCategoryBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_ClassroomCategory_Export_Model.Id);
            }
            else
            {
                ClassroomCategory = new ClassroomCategory();
            } 
			ClassroomCategory.Code = Default_ClassroomCategory_Export_Model.Code;
			ClassroomCategory.Name = Default_ClassroomCategory_Export_Model.Name;
			ClassroomCategory.Description = Default_ClassroomCategory_Export_Model.Description;
			ClassroomCategory.Id = Default_ClassroomCategory_Export_Model.Id;
            return ClassroomCategory;
        }
        public virtual Default_ClassroomCategory_Export_Model ConverTo_Default_ClassroomCategory_Export_Model(ClassroomCategory ClassroomCategory)
        {  
			Default_ClassroomCategory_Export_Model Default_ClassroomCategory_Export_Model = new Default_ClassroomCategory_Export_Model();
			Default_ClassroomCategory_Export_Model.toStringValue = ClassroomCategory.ToString();
			Default_ClassroomCategory_Export_Model.Code = ClassroomCategory.Code;
			Default_ClassroomCategory_Export_Model.Name = ClassroomCategory.Name;
			Default_ClassroomCategory_Export_Model.Description = ClassroomCategory.Description;
			Default_ClassroomCategory_Export_Model.Id = ClassroomCategory.Id;
            return Default_ClassroomCategory_Export_Model;            
        }

		public virtual Default_ClassroomCategory_Export_Model CreateNew()
        {
            ClassroomCategory ClassroomCategory = new ClassroomCategoryBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_ClassroomCategory_Export_Model Default_ClassroomCategory_Export_Model = this.ConverTo_Default_ClassroomCategory_Export_Model(ClassroomCategory);
            return Default_ClassroomCategory_Export_Model;
        } 

		public virtual List<Default_ClassroomCategory_Export_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ClassroomCategoryBLO entityBLO = new ClassroomCategoryBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ClassroomCategory> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_ClassroomCategory_Export_Model> ls_models = new List<Default_ClassroomCategory_Export_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_ClassroomCategory_Export_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_ClassroomCategory_Export_ModelBLM : BaseDefault_ClassroomCategory_Export_Model_BLM
	{
		public Default_ClassroomCategory_Export_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
