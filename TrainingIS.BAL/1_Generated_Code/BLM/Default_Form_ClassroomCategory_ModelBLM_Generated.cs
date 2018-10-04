//modelType = Default_Form_ClassroomCategory_Model

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
	public partial class BaseDefault_Form_ClassroomCategory_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_ClassroomCategory_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ClassroomCategory ConverTo_ClassroomCategory(Default_Form_ClassroomCategory_Model Default_Form_ClassroomCategory_Model)
        {
			ClassroomCategory ClassroomCategory = null;
            if (Default_Form_ClassroomCategory_Model.Id != 0)
            {
                ClassroomCategory = new ClassroomCategoryBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_ClassroomCategory_Model.Id);
            }
            else
            {
                ClassroomCategory = new ClassroomCategory();
            } 
			ClassroomCategory.Code = Default_Form_ClassroomCategory_Model.Code;
			ClassroomCategory.Name = Default_Form_ClassroomCategory_Model.Name;
			ClassroomCategory.Description = Default_Form_ClassroomCategory_Model.Description;
			ClassroomCategory.Id = Default_Form_ClassroomCategory_Model.Id;
            return ClassroomCategory;
        }
        public virtual Default_Form_ClassroomCategory_Model ConverTo_Default_Form_ClassroomCategory_Model(ClassroomCategory ClassroomCategory)
        {  
			Default_Form_ClassroomCategory_Model Default_Form_ClassroomCategory_Model = new Default_Form_ClassroomCategory_Model();
			Default_Form_ClassroomCategory_Model.toStringValue = ClassroomCategory.ToString();
			Default_Form_ClassroomCategory_Model.Code = ClassroomCategory.Code;
			Default_Form_ClassroomCategory_Model.Name = ClassroomCategory.Name;
			Default_Form_ClassroomCategory_Model.Description = ClassroomCategory.Description;
			Default_Form_ClassroomCategory_Model.Id = ClassroomCategory.Id;
            return Default_Form_ClassroomCategory_Model;            
        }

		public virtual Default_Form_ClassroomCategory_Model CreateNew()
        {
            ClassroomCategory ClassroomCategory = new ClassroomCategoryBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_ClassroomCategory_Model Default_Form_ClassroomCategory_Model = this.ConverTo_Default_Form_ClassroomCategory_Model(ClassroomCategory);
            return Default_Form_ClassroomCategory_Model;
        } 

		public virtual List<Default_Form_ClassroomCategory_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ClassroomCategoryBLO entityBLO = new ClassroomCategoryBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ClassroomCategory> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_ClassroomCategory_Model> ls_models = new List<Default_Form_ClassroomCategory_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_ClassroomCategory_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_ClassroomCategory_ModelBLM : BaseDefault_Form_ClassroomCategory_ModelBLM
	{
		public Default_Form_ClassroomCategory_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
