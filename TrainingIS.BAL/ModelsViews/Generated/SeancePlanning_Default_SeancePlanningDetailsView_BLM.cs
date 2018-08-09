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
	public partial class BaseDefault_SeancePlanningDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_SeancePlanningDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual SeancePlanning ConverTo_SeancePlanning(Default_SeancePlanningDetailsView Default_SeancePlanningDetailsView)
        {
			SeancePlanning SeancePlanning = null;
            if (Default_SeancePlanningDetailsView.Id != 0)
            {
                SeancePlanning = new SeancePlanningBLO(this.UnitOfWork).FindBaseEntityByID(Default_SeancePlanningDetailsView.Id);
            }
            else
            {
                SeancePlanning = new SeancePlanning();
            } 
			SeancePlanning.Schedule = Default_SeancePlanningDetailsView.Schedule;
			SeancePlanning.Training = Default_SeancePlanningDetailsView.Training;
			SeancePlanning.SeanceDay = Default_SeancePlanningDetailsView.SeanceDay;
			SeancePlanning.SeanceNumber = Default_SeancePlanningDetailsView.SeanceNumber;
			SeancePlanning.Classroom = Default_SeancePlanningDetailsView.Classroom;
			SeancePlanning.Description = Default_SeancePlanningDetailsView.Description;
			SeancePlanning.Id = Default_SeancePlanningDetailsView.Id;
            return SeancePlanning;
        }
        public virtual Default_SeancePlanningDetailsView ConverTo_Default_SeancePlanningDetailsView(SeancePlanning SeancePlanning)
        {  
			Default_SeancePlanningDetailsView Default_SeancePlanningDetailsView = new Default_SeancePlanningDetailsView();
			Default_SeancePlanningDetailsView.toStringValue = SeancePlanning.ToString();
			Default_SeancePlanningDetailsView.Schedule = SeancePlanning.Schedule;
			Default_SeancePlanningDetailsView.Training = SeancePlanning.Training;
			Default_SeancePlanningDetailsView.SeanceDay = SeancePlanning.SeanceDay;
			Default_SeancePlanningDetailsView.SeanceNumber = SeancePlanning.SeanceNumber;
			Default_SeancePlanningDetailsView.Classroom = SeancePlanning.Classroom;
			Default_SeancePlanningDetailsView.Description = SeancePlanning.Description;
			Default_SeancePlanningDetailsView.Id = SeancePlanning.Id;
            return Default_SeancePlanningDetailsView;            
        }

		public virtual Default_SeancePlanningDetailsView CreateNew()
        {
            SeancePlanning SeancePlanning = new SeancePlanning();
            Default_SeancePlanningDetailsView Default_SeancePlanningDetailsView = this.ConverTo_Default_SeancePlanningDetailsView(SeancePlanning);
            return Default_SeancePlanningDetailsView;
        } 
    }

	public partial class Default_SeancePlanningDetailsViewBLM : BaseDefault_SeancePlanningDetailsViewBLM
	{
		public Default_SeancePlanningDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
