using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using AutoFixture;
using TrainingIS.BLL;
using TrainingIS.DAL;
using System.ComponentModel.DataAnnotations;
using GApp.WebApp.Manager.Views;
using GApp.DAL;
using GApp.Entities;
using GApp.Core.Context;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;
using GApp.UnitTest.TestData;
using GApp.UnitTest.TestData.Enums;

namespace TestData
{
    public class BaseSeancePlanningTestDataFactory : ITestDataFactory<SeancePlanning>
    {
        private Fixture _Fixture = null;
		protected List<SeancePlanning> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseSeancePlanningTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<SeancePlanning> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<SeancePlanning> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first SeancePlanning instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual SeancePlanning CreateOrLouadFirstSeancePlanning()
        {
            SeancePlanningBLO seanceplanningBLO = new SeancePlanningBLO(UnitOfWork,GAppContext);
           
			SeancePlanning entity = null;
            if (seanceplanningBLO.FindAll()?.Count > 0)
                entity = seanceplanningBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp SeancePlanning for Test
                entity = this.CreateValideSeancePlanningInstance();
                seanceplanningBLO.Save(entity);
            }
            return entity;
        }

        public virtual SeancePlanning CreateValideSeancePlanningInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            SeancePlanning  Valide_SeancePlanning = this._Fixture.Create<SeancePlanning>();
            Valide_SeancePlanning.Id = 0;
            // Many to One 
            //
			// Classroom
			var Classroom = new ClassroomTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstClassroom();
            Valide_SeancePlanning.Classroom = null;
            Valide_SeancePlanning.ClassroomId = Classroom.Id;
			// Schedule
			var Schedule = new ScheduleTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSchedule();
            Valide_SeancePlanning.Schedule = null;
            Valide_SeancePlanning.ScheduleId = Schedule.Id;
			// SeanceDay
			var SeanceDay = new SeanceDayTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSeanceDay();
            Valide_SeancePlanning.SeanceDay = null;
            Valide_SeancePlanning.SeanceDayId = SeanceDay.Id;
			// SeanceNumber
			var SeanceNumber = new SeanceNumberTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSeanceNumber();
            Valide_SeancePlanning.SeanceNumber = null;
            Valide_SeancePlanning.SeanceNumberId = SeanceNumber.Id;
			// Training
			var Training = new TrainingTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTraining();
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
        public virtual SeancePlanning CreateInValideSeancePlanningInstance()
        {
            SeancePlanning seanceplanning = this.CreateValideSeancePlanningInstance();
             
			// Required   
 
			seanceplanning.ScheduleId = 0;
 
			seanceplanning.TrainingId = 0;
 
			seanceplanning.SeanceDayId = 0;
 
			seanceplanning.SeanceNumberId = 0;
 
			seanceplanning.ClassroomId = 0;
            //Unique
			var existant_SeancePlanning = this.CreateOrLouadFirstSeancePlanning();
			seanceplanning.Reference = existant_SeancePlanning.Reference;
 
            return seanceplanning;
        }


		public virtual SeancePlanning CreateInValideSeancePlanningInstance_ForEdit()
        {
            SeancePlanning seanceplanning = this.CreateOrLouadFirstSeancePlanning();
			// Required   
 
			seanceplanning.ScheduleId = 0;
 
			seanceplanning.TrainingId = 0;
 
			seanceplanning.SeanceDayId = 0;
 
			seanceplanning.SeanceNumberId = 0;
 
			seanceplanning.ClassroomId = 0;
            //Unique
			var existant_SeancePlanning = this.CreateOrLouadFirstSeancePlanning();
			seanceplanning.Reference = existant_SeancePlanning.Reference;
            return seanceplanning;
        }
    }

	public partial class SeancePlanningTestDataFactory : BaseSeancePlanningTestDataFactory{
	
		public SeancePlanningTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
