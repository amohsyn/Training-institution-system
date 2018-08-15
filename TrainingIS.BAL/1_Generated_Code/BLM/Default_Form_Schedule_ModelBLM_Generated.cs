//modelType = Default_Form_Schedule_Model

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
	public partial class BaseDefault_Form_Schedule_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Form_Schedule_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Schedule ConverTo_Schedule(Default_Form_Schedule_Model Default_Form_Schedule_Model)
        {
			Schedule Schedule = null;
            if (Default_Form_Schedule_Model.Id != 0)
            {
                Schedule = new ScheduleBLO(this.UnitOfWork).FindBaseEntityByID(Default_Form_Schedule_Model.Id);
            }
            else
            {
                Schedule = new Schedule();
            } 
			Schedule.TrainingYearId = Default_Form_Schedule_Model.TrainingYearId;
			Schedule.StartDate = DefaultDateTime_If_Empty(Default_Form_Schedule_Model.StartDate);
			Schedule.EndtDate = DefaultDateTime_If_Empty(Default_Form_Schedule_Model.EndtDate);
			Schedule.Description = Default_Form_Schedule_Model.Description;
			Schedule.Id = Default_Form_Schedule_Model.Id;
            return Schedule;
        }
        public virtual Default_Form_Schedule_Model ConverTo_Default_Form_Schedule_Model(Schedule Schedule)
        {  
			Default_Form_Schedule_Model Default_Form_Schedule_Model = new Default_Form_Schedule_Model();
			Default_Form_Schedule_Model.toStringValue = Schedule.ToString();
			Default_Form_Schedule_Model.TrainingYearId = Schedule.TrainingYearId;
			Default_Form_Schedule_Model.StartDate = DefaultDateTime_If_Empty(Schedule.StartDate);
			Default_Form_Schedule_Model.EndtDate = DefaultDateTime_If_Empty(Schedule.EndtDate);
			Default_Form_Schedule_Model.Description = Schedule.Description;
			Default_Form_Schedule_Model.Id = Schedule.Id;
            return Default_Form_Schedule_Model;            
        }

		public virtual Default_Form_Schedule_Model CreateNew()
        {
            Schedule Schedule = new Schedule();
            Default_Form_Schedule_Model Default_Form_Schedule_Model = this.ConverTo_Default_Form_Schedule_Model(Schedule);
            return Default_Form_Schedule_Model;
        } 
    }

	public partial class Default_Form_Schedule_ModelBLM : BaseDefault_Form_Schedule_ModelBLM
	{
		public Default_Form_Schedule_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
