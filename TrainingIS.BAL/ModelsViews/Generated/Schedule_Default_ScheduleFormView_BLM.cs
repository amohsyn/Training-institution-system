using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_ScheduleFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_ScheduleFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Schedule ConverTo_Schedule(Default_ScheduleFormView Default_ScheduleFormView)
        {
			Schedule Schedule = null;
            if (Default_ScheduleFormView.Id != 0)
            {
                Schedule = new ScheduleBLO(this.UnitOfWork).FindBaseEntityByID(Default_ScheduleFormView.Id);
            }
            else
            {
                Schedule = new Schedule();
            } 
			Schedule.TrainingYearId = Default_ScheduleFormView.TrainingYearId;
			Schedule.StartDate = DefaultDateTime_If_Empty(Default_ScheduleFormView.StartDate);
			Schedule.EndtDate = DefaultDateTime_If_Empty(Default_ScheduleFormView.EndtDate);
			Schedule.Description = Default_ScheduleFormView.Description;
			Schedule.Id = Default_ScheduleFormView.Id;
            return Schedule;
        }
        public virtual Default_ScheduleFormView ConverTo_Default_ScheduleFormView(Schedule Schedule)
        {  
			Default_ScheduleFormView Default_ScheduleFormView = new Default_ScheduleFormView();
			Default_ScheduleFormView.toStringValue = Schedule.ToString();
			Default_ScheduleFormView.TrainingYearId = Schedule.TrainingYearId;
			Default_ScheduleFormView.StartDate = DefaultDateTime_If_Empty(Schedule.StartDate);
			Default_ScheduleFormView.EndtDate = DefaultDateTime_If_Empty(Schedule.EndtDate);
			Default_ScheduleFormView.Description = Schedule.Description;
			Default_ScheduleFormView.Id = Schedule.Id;
            return Default_ScheduleFormView;            
        }

		public virtual Default_ScheduleFormView CreateNew()
        {
            Schedule Schedule = new Schedule();
            Default_ScheduleFormView Default_ScheduleFormView = this.ConverTo_Default_ScheduleFormView(Schedule);
            return Default_ScheduleFormView;
        } 
    }

	public partial class Default_ScheduleFormViewBLM : BaseDefault_ScheduleFormViewBLM
	{
		public Default_ScheduleFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
