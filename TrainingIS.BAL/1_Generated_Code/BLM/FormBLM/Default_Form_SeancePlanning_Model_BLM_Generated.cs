//modelType = Default_Form_SeancePlanning_Model

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
	public partial class BaseDefault_Form_SeancePlanning_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_SeancePlanning_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SeancePlanning ConverTo_SeancePlanning(Default_Form_SeancePlanning_Model Default_Form_SeancePlanning_Model)
        {
			SeancePlanning SeancePlanning = null;
            if (Default_Form_SeancePlanning_Model.Id != 0)
            {
                SeancePlanning = new SeancePlanningBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_SeancePlanning_Model.Id);
            }
            else
            {
                SeancePlanning = new SeancePlanning();
            } 
			SeancePlanning.ScheduleId = Default_Form_SeancePlanning_Model.ScheduleId;
			SeancePlanning.Schedule = new ScheduleBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_SeancePlanning_Model.ScheduleId)) ;
			SeancePlanning.TrainingId = Default_Form_SeancePlanning_Model.TrainingId;
			SeancePlanning.Training = new TrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_SeancePlanning_Model.TrainingId)) ;
			SeancePlanning.SeanceDayId = Default_Form_SeancePlanning_Model.SeanceDayId;
			SeancePlanning.SeanceDay = new SeanceDayBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_SeancePlanning_Model.SeanceDayId)) ;
			SeancePlanning.SeanceNumberId = Default_Form_SeancePlanning_Model.SeanceNumberId;
			SeancePlanning.SeanceNumber = new SeanceNumberBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_SeancePlanning_Model.SeanceNumberId)) ;
			SeancePlanning.ClassroomId = Default_Form_SeancePlanning_Model.ClassroomId;
			SeancePlanning.Classroom = new ClassroomBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_SeancePlanning_Model.ClassroomId)) ;
			SeancePlanning.Description = Default_Form_SeancePlanning_Model.Description;
			SeancePlanning.Reference = Default_Form_SeancePlanning_Model.Reference;
			SeancePlanning.Id = Default_Form_SeancePlanning_Model.Id;
            return SeancePlanning;
        }
        public virtual void ConverTo_Default_Form_SeancePlanning_Model(Default_Form_SeancePlanning_Model Default_Form_SeancePlanning_Model, SeancePlanning SeancePlanning)
        {  
			 
			Default_Form_SeancePlanning_Model.toStringValue = SeancePlanning.ToString();
			Default_Form_SeancePlanning_Model.ScheduleId = SeancePlanning.ScheduleId;
			Default_Form_SeancePlanning_Model.TrainingId = SeancePlanning.TrainingId;
			Default_Form_SeancePlanning_Model.SeanceDayId = SeancePlanning.SeanceDayId;
			Default_Form_SeancePlanning_Model.SeanceNumberId = SeancePlanning.SeanceNumberId;
			Default_Form_SeancePlanning_Model.ClassroomId = SeancePlanning.ClassroomId;
			Default_Form_SeancePlanning_Model.Description = SeancePlanning.Description;
			Default_Form_SeancePlanning_Model.Id = SeancePlanning.Id;
			Default_Form_SeancePlanning_Model.Reference = SeancePlanning.Reference;
                     
        }

    }

	public partial class Default_Form_SeancePlanning_ModelBLM : BaseDefault_Form_SeancePlanning_Model_BLM
	{
		public Default_Form_SeancePlanning_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
