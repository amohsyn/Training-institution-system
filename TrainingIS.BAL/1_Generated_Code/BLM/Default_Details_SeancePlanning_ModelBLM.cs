﻿using System;
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
	public partial class BaseDefault_Details_SeancePlanning_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Details_SeancePlanning_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual SeancePlanning ConverTo_SeancePlanning(Default_Details_SeancePlanning_Model Default_Details_SeancePlanning_Model)
        {
			SeancePlanning SeancePlanning = null;
            if (Default_Details_SeancePlanning_Model.Id != 0)
            {
                SeancePlanning = new SeancePlanningBLO(this.UnitOfWork).FindBaseEntityByID(Default_Details_SeancePlanning_Model.Id);
            }
            else
            {
                SeancePlanning = new SeancePlanning();
            } 
			SeancePlanning.Schedule = Default_Details_SeancePlanning_Model.Schedule;
			SeancePlanning.Training = Default_Details_SeancePlanning_Model.Training;
			SeancePlanning.SeanceDay = Default_Details_SeancePlanning_Model.SeanceDay;
			SeancePlanning.SeanceNumber = Default_Details_SeancePlanning_Model.SeanceNumber;
			SeancePlanning.Classroom = Default_Details_SeancePlanning_Model.Classroom;
			SeancePlanning.Description = Default_Details_SeancePlanning_Model.Description;
			SeancePlanning.Id = Default_Details_SeancePlanning_Model.Id;
            return SeancePlanning;
        }
        public virtual Default_Details_SeancePlanning_Model ConverTo_Default_Details_SeancePlanning_Model(SeancePlanning SeancePlanning)
        {  
			Default_Details_SeancePlanning_Model Default_Details_SeancePlanning_Model = new Default_Details_SeancePlanning_Model();
			Default_Details_SeancePlanning_Model.toStringValue = SeancePlanning.ToString();
			Default_Details_SeancePlanning_Model.Schedule = SeancePlanning.Schedule;
			Default_Details_SeancePlanning_Model.Training = SeancePlanning.Training;
			Default_Details_SeancePlanning_Model.SeanceDay = SeancePlanning.SeanceDay;
			Default_Details_SeancePlanning_Model.SeanceNumber = SeancePlanning.SeanceNumber;
			Default_Details_SeancePlanning_Model.Classroom = SeancePlanning.Classroom;
			Default_Details_SeancePlanning_Model.Description = SeancePlanning.Description;
			Default_Details_SeancePlanning_Model.Id = SeancePlanning.Id;
            return Default_Details_SeancePlanning_Model;            
        }

		public virtual Default_Details_SeancePlanning_Model CreateNew()
        {
            SeancePlanning SeancePlanning = new SeancePlanning();
            Default_Details_SeancePlanning_Model Default_Details_SeancePlanning_Model = this.ConverTo_Default_Details_SeancePlanning_Model(SeancePlanning);
            return Default_Details_SeancePlanning_Model;
        } 
    }

	public partial class Default_Details_SeancePlanning_ModelBLM : BaseDefault_Details_SeancePlanning_ModelBLM
	{
		public Default_Details_SeancePlanning_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}