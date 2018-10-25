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
    public class BaseScheduleTestDataFactory : EntityTestData<Schedule>
    {
        public BaseScheduleTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Schedule> Generate_TestData()
        {
            List<Schedule> Data = base.Generate_TestData();
            if(Data == null) Data = new List<Schedule>();
            Data.Add(this.CreateValideScheduleInstance());
            return Data;
        }
	
		/// <summary>
        /// Find the first Schedule instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Schedule CreateOrLouadFirstSchedule()
        {
            ScheduleBLO scheduleBLO = new ScheduleBLO(UnitOfWork,GAppContext);
           
			Schedule entity = null;
            if (scheduleBLO.FindAll()?.Count > 0)
                entity = scheduleBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Schedule for Test
                entity = this.CreateValideScheduleInstance();
                scheduleBLO.Save(entity);
            }
            return entity;
        }

        public virtual Schedule CreateValideScheduleInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Schedule  Valide_Schedule = this._Fixture.Create<Schedule>();
            Valide_Schedule.Id = 0;
            // Many to One 
            //   
			// TrainingYear
			var TrainingYear = new TrainingYearTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainingYear();
            Valide_Schedule.TrainingYear = TrainingYear;
						 Valide_Schedule.TrainingYearId = TrainingYear.Id;
			           
            // One to Many
            //
            return Valide_Schedule;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Schedule can't exist</returns>
        public virtual Schedule CreateInValideScheduleInstance()
        {
            Schedule schedule = this.CreateValideScheduleInstance();
             
			// Required   
 
			schedule.TrainingYearId = 0;
 
			schedule.StartDate = DateTime.Now;
 
			schedule.EndtDate = DateTime.Now;
            //Unique
			var existant_Schedule = this.CreateOrLouadFirstSchedule();
			schedule.Reference = existant_Schedule.Reference;
 
            return schedule;
        }


		public virtual Schedule CreateInValideScheduleInstance_ForEdit()
        {
            Schedule schedule = this.CreateOrLouadFirstSchedule();
			// Required   
 
			schedule.TrainingYearId = 0;
 
			schedule.StartDate = DateTime.Now;
 
			schedule.EndtDate = DateTime.Now;
            //Unique
			var existant_Schedule = this.CreateOrLouadFirstSchedule();
			schedule.Reference = existant_Schedule.Reference;
            return schedule;
        }
    }

	public partial class ScheduleTestDataFactory : BaseScheduleTestDataFactory{
	
		public ScheduleTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
