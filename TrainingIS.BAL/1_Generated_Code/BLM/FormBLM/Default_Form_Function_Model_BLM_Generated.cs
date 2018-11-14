//modelType = Default_Form_Function_Model

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
	public partial class BaseDefault_Form_Function_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Function_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Function ConverTo_Function(Default_Form_Function_Model Default_Form_Function_Model)
        {
			Function Function = null;
            if (Default_Form_Function_Model.Id != 0)
            {
                Function = new FunctionBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Function_Model.Id);
            }
            else
            {
                Function = new Function();
            } 
			Function.Code = Default_Form_Function_Model.Code;
			Function.Name = Default_Form_Function_Model.Name;
			Function.Description = Default_Form_Function_Model.Description;
			Function.Reference = Default_Form_Function_Model.Reference;
			Function.Id = Default_Form_Function_Model.Id;
            return Function;
        }
        public virtual void ConverTo_Default_Form_Function_Model(Default_Form_Function_Model Default_Form_Function_Model, Function Function)
        {  
			 
			Default_Form_Function_Model.toStringValue = Function.ToString();
			Default_Form_Function_Model.Code = Function.Code;
			Default_Form_Function_Model.Name = Function.Name;
			Default_Form_Function_Model.Description = Function.Description;
			Default_Form_Function_Model.Id = Function.Id;
			Default_Form_Function_Model.Reference = Function.Reference;
                     
        }

    }

	public partial class Default_Form_Function_ModelBLM : BaseDefault_Form_Function_Model_BLM
	{
		public Default_Form_Function_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
