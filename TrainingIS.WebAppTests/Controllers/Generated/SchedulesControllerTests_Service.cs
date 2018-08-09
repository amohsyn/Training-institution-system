﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
using TrainingIS.WebApp.Helpers.AlertMessages;
using GApp.WebApp.Tests;
using GApp.WebApp.Manager.Views;
using TrainingIS.WebApp.Tests.TestUtilities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    public class SchedulesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public SchedulesControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first Schedule instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public Schedule CreateOrLouadFirstSchedule(UnitOfWork unitOfWork)
        {
            ScheduleBLO scheduleBLO = new ScheduleBLO(unitOfWork);
           
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

        public Schedule CreateValideScheduleInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            Schedule  Valide_Schedule = this._Fixture.Create<Schedule>();
            Valide_Schedule.Id = 0;
            // Many to One 
            //
			// TrainingYear
			var TrainingYear = new TrainingYearsControllerTests_Service().CreateOrLouadFirstTrainingYear(unitOfWork);
            Valide_Schedule.TrainingYear = null;
            Valide_Schedule.TrainingYearId = TrainingYear.Id;
            // One to Many
            //
            return Valide_Schedule;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Schedule can't exist</returns>
        public Schedule CreateInValideScheduleInstance(UnitOfWork unitOfWork = null)
        {
            Schedule schedule = this.CreateValideScheduleInstance(unitOfWork);
             
			// Required   
 
			schedule.TrainingYearId = 0;
 
			schedule.StartDate = DateTime.Now;
 
			schedule.EndtDate = DateTime.Now;
            //Unique
			var existant_Schedule = this.CreateOrLouadFirstSchedule(new UnitOfWork());
            
            return schedule;
        }


		  public Schedule CreateInValideScheduleInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            Schedule schedule = this.CreateOrLouadFirstSchedule(unitOfWork);
             
			// Required   
 
			schedule.TrainingYearId = 0;
 
			schedule.StartDate = DateTime.Now;
 
			schedule.EndtDate = DateTime.Now;
            //Unique
			var existant_Schedule = this.CreateOrLouadFirstSchedule(new UnitOfWork());
            
            return schedule;
        }
    }
}

