using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrainingIS.Entities;
using AutoFixture;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.WebApp.Tests.ViewModels;
using System.ComponentModel.DataAnnotations;
using GApp.WebApp.Tests;
using GApp.WebApp.Manager.Views;
using TrainingIS.WebApp.Tests.TestUtilities;
using GApp.DAL;
using GApp.Entities;
using GApp.Core.Context;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Tests.Services 
{
    public class BaseSeancePlanningsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseSeancePlanningsControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first SeancePlanning instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual SeancePlanning CreateOrLouadFirstSeancePlanning(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            SeancePlanningBLO seanceplanningBLO = new SeancePlanningBLO(unitOfWork,GAppContext);
           
			SeancePlanning entity = null;
            if (seanceplanningBLO.FindAll()?.Count > 0)
                entity = seanceplanningBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp SeancePlanning for Test
                entity = this.CreateValideSeancePlanningInstance(unitOfWork,GAppContext);
                seanceplanningBLO.Save(entity);
            }
            return entity;
        }

        public virtual SeancePlanning CreateValideSeancePlanningInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            SeancePlanning  Valide_SeancePlanning = this._Fixture.Create<SeancePlanning>();
            Valide_SeancePlanning.Id = 0;
            // Many to One 
            //
			// Classroom
			var Classroom = new ClassroomsControllerTests_Service().CreateOrLouadFirstClassroom(unitOfWork,GAppContext);
            Valide_SeancePlanning.Classroom = null;
            Valide_SeancePlanning.ClassroomId = Classroom.Id;
			// Schedule
			var Schedule = new SchedulesControllerTests_Service().CreateOrLouadFirstSchedule(unitOfWork,GAppContext);
            Valide_SeancePlanning.Schedule = null;
            Valide_SeancePlanning.ScheduleId = Schedule.Id;
			// SeanceDay
			var SeanceDay = new SeanceDaysControllerTests_Service().CreateOrLouadFirstSeanceDay(unitOfWork,GAppContext);
            Valide_SeancePlanning.SeanceDay = null;
            Valide_SeancePlanning.SeanceDayId = SeanceDay.Id;
			// SeanceNumber
			var SeanceNumber = new SeanceNumbersControllerTests_Service().CreateOrLouadFirstSeanceNumber(unitOfWork,GAppContext);
            Valide_SeancePlanning.SeanceNumber = null;
            Valide_SeancePlanning.SeanceNumberId = SeanceNumber.Id;
			// Training
			var Training = new TrainingsControllerTests_Service().CreateOrLouadFirstTraining(unitOfWork,GAppContext);
            Valide_SeancePlanning.Training = null;
            Valide_SeancePlanning.TrainingId = Training.Id;
            // One to Many
            //
			Valide_SeancePlanning.Absences = null;
            return Valide_SeancePlanning;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide SeancePlanning can't exist</returns>
        public virtual SeancePlanning CreateInValideSeancePlanningInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            SeancePlanning seanceplanning = this.CreateValideSeancePlanningInstance(unitOfWork, GAppContext);
             
			// Required   
 
			seanceplanning.ScheduleId = 0;
 
			seanceplanning.TrainingId = 0;
 
			seanceplanning.SeanceDayId = 0;
 
			seanceplanning.SeanceNumberId = 0;
 
			seanceplanning.ClassroomId = 0;
            //Unique
			var existant_SeancePlanning = this.CreateOrLouadFirstSeancePlanning(new UnitOfWork<TrainingISModel>(),GAppContext);
 
            return seanceplanning;
        }


		public virtual SeancePlanning CreateInValideSeancePlanningInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            SeancePlanning seanceplanning = this.CreateOrLouadFirstSeancePlanning(unitOfWork, GAppContext);
			// Required   
 
			seanceplanning.ScheduleId = 0;
 
			seanceplanning.TrainingId = 0;
 
			seanceplanning.SeanceDayId = 0;
 
			seanceplanning.SeanceNumberId = 0;
 
			seanceplanning.ClassroomId = 0;
            //Unique
			var existant_SeancePlanning = this.CreateOrLouadFirstSeancePlanning(new UnitOfWork<TrainingISModel>(), GAppContext);
            return seanceplanning;
        }
    }

	public partial class SeancePlanningsControllerTests_Service : BaseSeancePlanningsControllerTests_Service{}
}
