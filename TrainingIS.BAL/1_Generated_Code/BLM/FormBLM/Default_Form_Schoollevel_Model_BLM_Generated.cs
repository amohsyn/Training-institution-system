//modelType = Default_Form_Schoollevel_Model

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
	public partial class BaseDefault_Form_Schoollevel_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Schoollevel_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Schoollevel ConverTo_Schoollevel(Default_Form_Schoollevel_Model Default_Form_Schoollevel_Model)
        {
			Schoollevel Schoollevel = null;
            if (Default_Form_Schoollevel_Model.Id != 0)
            {
                Schoollevel = new SchoollevelBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Schoollevel_Model.Id);
            }
            else
            {
                Schoollevel = new Schoollevel();
            } 
			Schoollevel.Code = Default_Form_Schoollevel_Model.Code;
			Schoollevel.Name = Default_Form_Schoollevel_Model.Name;
			Schoollevel.Description = Default_Form_Schoollevel_Model.Description;
			Schoollevel.Reference = Default_Form_Schoollevel_Model.Reference;
			Schoollevel.Id = Default_Form_Schoollevel_Model.Id;
            return Schoollevel;
        }
        public virtual void ConverTo_Default_Form_Schoollevel_Model(Default_Form_Schoollevel_Model Default_Form_Schoollevel_Model, Schoollevel Schoollevel)
        {  
			 
			Default_Form_Schoollevel_Model.toStringValue = Schoollevel.ToString();
			Default_Form_Schoollevel_Model.Code = Schoollevel.Code;
			Default_Form_Schoollevel_Model.Name = Schoollevel.Name;
			Default_Form_Schoollevel_Model.Description = Schoollevel.Description;
			Default_Form_Schoollevel_Model.Id = Schoollevel.Id;
			Default_Form_Schoollevel_Model.Reference = Schoollevel.Reference;
                     
        }

    }

	public partial class Default_Form_Schoollevel_ModelBLM : BaseDefault_Form_Schoollevel_Model_BLM
	{
		public Default_Form_Schoollevel_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
