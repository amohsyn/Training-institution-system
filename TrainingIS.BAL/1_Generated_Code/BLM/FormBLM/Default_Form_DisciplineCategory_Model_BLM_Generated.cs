//modelType = Default_Form_DisciplineCategory_Model

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
	public partial class BaseDefault_Form_DisciplineCategory_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_DisciplineCategory_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual DisciplineCategory ConverTo_DisciplineCategory(Default_Form_DisciplineCategory_Model Default_Form_DisciplineCategory_Model)
        {
			DisciplineCategory DisciplineCategory = null;
            if (Default_Form_DisciplineCategory_Model.Id != 0)
            {
                DisciplineCategory = new DisciplineCategoryBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_DisciplineCategory_Model.Id);
            }
            else
            {
                DisciplineCategory = new DisciplineCategory();
            } 
			DisciplineCategory.Code = Default_Form_DisciplineCategory_Model.Code;
			DisciplineCategory.Name = Default_Form_DisciplineCategory_Model.Name;
			DisciplineCategory.System_DisciplineCategy = Default_Form_DisciplineCategory_Model.System_DisciplineCategy;
			DisciplineCategory.Description = Default_Form_DisciplineCategory_Model.Description;
			DisciplineCategory.Reference = Default_Form_DisciplineCategory_Model.Reference;
			DisciplineCategory.Id = Default_Form_DisciplineCategory_Model.Id;
            return DisciplineCategory;
        }
        public virtual void ConverTo_Default_Form_DisciplineCategory_Model(Default_Form_DisciplineCategory_Model Default_Form_DisciplineCategory_Model, DisciplineCategory DisciplineCategory)
        {  
			 
			Default_Form_DisciplineCategory_Model.toStringValue = DisciplineCategory.ToString();
			Default_Form_DisciplineCategory_Model.Code = DisciplineCategory.Code;
			Default_Form_DisciplineCategory_Model.Name = DisciplineCategory.Name;
			Default_Form_DisciplineCategory_Model.System_DisciplineCategy = DisciplineCategory.System_DisciplineCategy;
			Default_Form_DisciplineCategory_Model.Description = DisciplineCategory.Description;
			Default_Form_DisciplineCategory_Model.Id = DisciplineCategory.Id;
			Default_Form_DisciplineCategory_Model.Reference = DisciplineCategory.Reference;
                     
        }

    }

	public partial class Default_Form_DisciplineCategory_ModelBLM : BaseDefault_Form_DisciplineCategory_Model_BLM
	{
		public Default_Form_DisciplineCategory_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
