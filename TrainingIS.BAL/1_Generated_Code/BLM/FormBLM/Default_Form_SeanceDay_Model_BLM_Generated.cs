//modelType = Default_Form_SeanceDay_Model

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
	public partial class BaseDefault_Form_SeanceDay_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_SeanceDay_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SeanceDay ConverTo_SeanceDay(Default_Form_SeanceDay_Model Default_Form_SeanceDay_Model)
        {
			SeanceDay SeanceDay = null;
            if (Default_Form_SeanceDay_Model.Id != 0)
            {
                SeanceDay = new SeanceDayBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_SeanceDay_Model.Id);
            }
            else
            {
                SeanceDay = new SeanceDay();
            } 
			SeanceDay.Name = Default_Form_SeanceDay_Model.Name;
			SeanceDay.Code = Default_Form_SeanceDay_Model.Code;
			SeanceDay.Day = Default_Form_SeanceDay_Model.Day;
			SeanceDay.Description = Default_Form_SeanceDay_Model.Description;
			SeanceDay.Reference = Default_Form_SeanceDay_Model.Reference;
			SeanceDay.Id = Default_Form_SeanceDay_Model.Id;
            return SeanceDay;
        }
        public virtual void ConverTo_Default_Form_SeanceDay_Model(Default_Form_SeanceDay_Model Default_Form_SeanceDay_Model, SeanceDay SeanceDay)
        {  
			 
			Default_Form_SeanceDay_Model.toStringValue = SeanceDay.ToString();
			Default_Form_SeanceDay_Model.Name = SeanceDay.Name;
			Default_Form_SeanceDay_Model.Code = SeanceDay.Code;
			Default_Form_SeanceDay_Model.Day = SeanceDay.Day;
			Default_Form_SeanceDay_Model.Description = SeanceDay.Description;
			Default_Form_SeanceDay_Model.Id = SeanceDay.Id;
			Default_Form_SeanceDay_Model.Reference = SeanceDay.Reference;
                     
        }

    }

	public partial class Default_Form_SeanceDay_ModelBLM : BaseDefault_Form_SeanceDay_Model_BLM
	{
		public Default_Form_SeanceDay_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
