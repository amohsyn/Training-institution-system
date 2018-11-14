//modelType = Default_Form_GPicture_Model

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
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_GPicture_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_GPicture_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual GPicture ConverTo_GPicture(Default_Form_GPicture_Model Default_Form_GPicture_Model)
        {
			GPicture GPicture = null;
            if (Default_Form_GPicture_Model.Id != 0)
            {
                GPicture = new GPictureBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_GPicture_Model.Id);
            }
            else
            {
                GPicture = new GPicture();
            } 
			GPicture.Name = Default_Form_GPicture_Model.Name;
			GPicture.Description = Default_Form_GPicture_Model.Description;
			GPicture.Original_Thumbnail = Default_Form_GPicture_Model.Original_Thumbnail;
			GPicture.Large_Thumbnail = Default_Form_GPicture_Model.Large_Thumbnail;
			GPicture.Medium_Thumbnail = Default_Form_GPicture_Model.Medium_Thumbnail;
			GPicture.Small_Thumbnail = Default_Form_GPicture_Model.Small_Thumbnail;
			GPicture.Old_Reference = Default_Form_GPicture_Model.Old_Reference;
			GPicture.Reference = Default_Form_GPicture_Model.Reference;
			GPicture.Id = Default_Form_GPicture_Model.Id;
            return GPicture;
        }
        public virtual void ConverTo_Default_Form_GPicture_Model(Default_Form_GPicture_Model Default_Form_GPicture_Model, GPicture GPicture)
        {  
			 
			Default_Form_GPicture_Model.toStringValue = GPicture.ToString();
			Default_Form_GPicture_Model.Name = GPicture.Name;
			Default_Form_GPicture_Model.Description = GPicture.Description;
			Default_Form_GPicture_Model.Original_Thumbnail = GPicture.Original_Thumbnail;
			Default_Form_GPicture_Model.Large_Thumbnail = GPicture.Large_Thumbnail;
			Default_Form_GPicture_Model.Medium_Thumbnail = GPicture.Medium_Thumbnail;
			Default_Form_GPicture_Model.Small_Thumbnail = GPicture.Small_Thumbnail;
			Default_Form_GPicture_Model.Old_Reference = GPicture.Old_Reference;
			Default_Form_GPicture_Model.Id = GPicture.Id;
			Default_Form_GPicture_Model.Reference = GPicture.Reference;
                     
        }

    }

	public partial class Default_Form_GPicture_ModelBLM : BaseDefault_Form_GPicture_Model_BLM
	{
		public Default_Form_GPicture_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
