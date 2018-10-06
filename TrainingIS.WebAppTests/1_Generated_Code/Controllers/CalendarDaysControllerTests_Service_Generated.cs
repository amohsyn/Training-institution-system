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
    public class BaseCalendarDaysControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseCalendarDaysControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first CalendarDay instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual CalendarDay CreateOrLouadFirstCalendarDay(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            CalendarDayBLO calendardayBLO = new CalendarDayBLO(unitOfWork,GAppContext);
           
			CalendarDay entity = null;
            if (calendardayBLO.FindAll()?.Count > 0)
                entity = calendardayBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp CalendarDay for Test
                entity = this.CreateValideCalendarDayInstance(unitOfWork,GAppContext);
                calendardayBLO.Save(entity);
            }
            return entity;
        }

        public virtual CalendarDay CreateValideCalendarDayInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            CalendarDay  Valide_CalendarDay = this._Fixture.Create<CalendarDay>();
            Valide_CalendarDay.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_CalendarDay;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide CalendarDay can't exist</returns>
        public virtual CalendarDay CreateInValideCalendarDayInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            CalendarDay calendarday = this.CreateValideCalendarDayInstance(unitOfWork, GAppContext);
             
			// Required   
            //Unique
			var existant_CalendarDay = this.CreateOrLouadFirstCalendarDay(new UnitOfWork<TrainingISModel>(),GAppContext);
			calendarday.Reference = existant_CalendarDay.Reference;
 
            return calendarday;
        }


		public virtual CalendarDay CreateInValideCalendarDayInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            CalendarDay calendarday = this.CreateOrLouadFirstCalendarDay(unitOfWork, GAppContext);
			// Required   
            //Unique
			var existant_CalendarDay = this.CreateOrLouadFirstCalendarDay(new UnitOfWork<TrainingISModel>(), GAppContext);
			calendarday.Reference = existant_CalendarDay.Reference;
            return calendarday;
        }
    }

	public partial class CalendarDaysControllerTests_Service : BaseCalendarDaysControllerTests_Service{}
}
