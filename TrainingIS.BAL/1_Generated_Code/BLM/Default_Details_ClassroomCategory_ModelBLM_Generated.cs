//modelType = Default_Details_ClassroomCategory_Model

using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.DAL;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_ClassroomCategory_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Details_ClassroomCategory_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual ClassroomCategory ConverTo_ClassroomCategory(Default_Details_ClassroomCategory_Model Default_Details_ClassroomCategory_Model)
        {
			ClassroomCategory ClassroomCategory = null;
            if (Default_Details_ClassroomCategory_Model.Id != 0)
            {
                ClassroomCategory = new ClassroomCategoryBLO(this.UnitOfWork).FindBaseEntityByID(Default_Details_ClassroomCategory_Model.Id);
            }
            else
            {
                ClassroomCategory = new ClassroomCategory();
            } 
			ClassroomCategory.Code = Default_Details_ClassroomCategory_Model.Code;
			ClassroomCategory.Name = Default_Details_ClassroomCategory_Model.Name;
			ClassroomCategory.Description = Default_Details_ClassroomCategory_Model.Description;
			ClassroomCategory.Id = Default_Details_ClassroomCategory_Model.Id;
            return ClassroomCategory;
        }
        public virtual Default_Details_ClassroomCategory_Model ConverTo_Default_Details_ClassroomCategory_Model(ClassroomCategory ClassroomCategory)
        {  
			Default_Details_ClassroomCategory_Model Default_Details_ClassroomCategory_Model = new Default_Details_ClassroomCategory_Model();
			Default_Details_ClassroomCategory_Model.toStringValue = ClassroomCategory.ToString();
			Default_Details_ClassroomCategory_Model.Code = ClassroomCategory.Code;
			Default_Details_ClassroomCategory_Model.Name = ClassroomCategory.Name;
			Default_Details_ClassroomCategory_Model.Description = ClassroomCategory.Description;
			Default_Details_ClassroomCategory_Model.Id = ClassroomCategory.Id;
            return Default_Details_ClassroomCategory_Model;            
        }

		public virtual Default_Details_ClassroomCategory_Model CreateNew()
        {
            ClassroomCategory ClassroomCategory = new ClassroomCategory();
            Default_Details_ClassroomCategory_Model Default_Details_ClassroomCategory_Model = this.ConverTo_Default_Details_ClassroomCategory_Model(ClassroomCategory);
            return Default_Details_ClassroomCategory_Model;
        } 
    }

	public partial class Default_Details_ClassroomCategory_ModelBLM : BaseDefault_Details_ClassroomCategory_ModelBLM
	{
		public Default_Details_ClassroomCategory_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
