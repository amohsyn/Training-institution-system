//modelType = Default_Form_ControllerApp_Model

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
using GApp.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_ControllerApp_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_ControllerApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ControllerApp ConverTo_ControllerApp(Default_Form_ControllerApp_Model Default_Form_ControllerApp_Model)
        {
			ControllerApp ControllerApp = null;
            if (Default_Form_ControllerApp_Model.Id != 0)
            {
                ControllerApp = new ControllerAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_ControllerApp_Model.Id);
            }
            else
            {
                ControllerApp = new ControllerApp();
            } 
			ControllerApp.Code = Default_Form_ControllerApp_Model.Code;
			ControllerApp.Name = Default_Form_ControllerApp_Model.Name;
			ControllerApp.Description = Default_Form_ControllerApp_Model.Description;
			ControllerApp.Id = Default_Form_ControllerApp_Model.Id;
            return ControllerApp;
        }
        public virtual Default_Form_ControllerApp_Model ConverTo_Default_Form_ControllerApp_Model(ControllerApp ControllerApp)
        {  
			Default_Form_ControllerApp_Model Default_Form_ControllerApp_Model = new Default_Form_ControllerApp_Model();
			Default_Form_ControllerApp_Model.toStringValue = ControllerApp.ToString();
			Default_Form_ControllerApp_Model.Code = ControllerApp.Code;
			Default_Form_ControllerApp_Model.Name = ControllerApp.Name;
			Default_Form_ControllerApp_Model.Description = ControllerApp.Description;
			Default_Form_ControllerApp_Model.Id = ControllerApp.Id;
            return Default_Form_ControllerApp_Model;            
        }

		public virtual Default_Form_ControllerApp_Model CreateNew()
        {
            ControllerApp ControllerApp = new ControllerAppBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_ControllerApp_Model Default_Form_ControllerApp_Model = this.ConverTo_Default_Form_ControllerApp_Model(ControllerApp);
            return Default_Form_ControllerApp_Model;
        } 
    }

	public partial class Default_Form_ControllerApp_ModelBLM : BaseDefault_Form_ControllerApp_ModelBLM
	{
		public Default_Form_ControllerApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
