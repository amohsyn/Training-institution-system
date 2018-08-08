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
	public partial class BaseDefault_ScheduleDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_ScheduleDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Schedule ConverTo_Schedule(Default_ScheduleDetailsView Default_ScheduleDetailsView)
        {
			Schedule Schedule = null;
            if (Default_ScheduleDetailsView.Id != 0)
            {
                Schedule = new ScheduleBLO(this.UnitOfWork).FindBaseEntityByID(Default_ScheduleDetailsView.Id);
            }
            else
            {
                Schedule = new Schedule();
            } 
			Schedule.TrainingYear = Default_ScheduleDetailsView.TrainingYear;
			Schedule.StartDate = DefaultDateTime_If_Empty(Default_ScheduleDetailsView.StartDate);
			Schedule.EndtDate = DefaultDateTime_If_Empty(Default_ScheduleDetailsView.EndtDate);
			Schedule.Description = Default_ScheduleDetailsView.Description;
			Schedule.Id = Default_ScheduleDetailsView.Id;
            return Schedule;
        }
        public virtual Default_ScheduleDetailsView ConverTo_Default_ScheduleDetailsView(Schedule Schedule)
        {  
			Default_ScheduleDetailsView Default_ScheduleDetailsView = new Default_ScheduleDetailsView();
			Default_ScheduleDetailsView.toStringValue = Schedule.ToString();
			Default_ScheduleDetailsView.TrainingYear = Schedule.TrainingYear;
			Default_ScheduleDetailsView.StartDate = DefaultDateTime_If_Empty(Schedule.StartDate);
			Default_ScheduleDetailsView.EndtDate = DefaultDateTime_If_Empty(Schedule.EndtDate);
			Default_ScheduleDetailsView.Description = Schedule.Description;
			Default_ScheduleDetailsView.Id = Schedule.Id;
            return Default_ScheduleDetailsView;            
        }

		public virtual Default_ScheduleDetailsView CreateNew()
        {
            Schedule Schedule = new Schedule();
            Default_ScheduleDetailsView Default_ScheduleDetailsView = this.ConverTo_Default_ScheduleDetailsView(Schedule);
            return Default_ScheduleDetailsView;
        } 
    }

	public partial class Default_ScheduleDetailsViewBLM : BaseDefault_ScheduleDetailsViewBLM
	{
		public Default_ScheduleDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
