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
    public class BaseCalendarDayTestDataFactory : EntityTestData<CalendarDay>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new CalendarDayBLO(UnitOfWork, GAppContext);
        }

        public BaseCalendarDayTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<CalendarDay> Generate_TestData()
        {
            List<CalendarDay> Data = base.Generate_TestData();
            if(Data == null) Data = new List<CalendarDay>();
			CalendarDay CalendarDay = this.CreateValideCalendarDayInstance();
            CalendarDay.Reference = "ValideCalendarDayInstance";
            Data.Add(CalendarDay);
            return Data;
        }
	
		/// <summary>
        /// Find the first CalendarDay instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual CalendarDay CreateOrLouadFirstCalendarDay()
        {
            CalendarDayBLO calendardayBLO = new CalendarDayBLO(UnitOfWork,GAppContext);
           
			CalendarDay entity = null;
            if (calendardayBLO.FindAll()?.Count > 0)
                entity = calendardayBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp CalendarDay for Test
                entity = this.CreateValideCalendarDayInstance();
                calendardayBLO.Save(entity);
            }
            return entity;
        }

        public virtual CalendarDay CreateValideCalendarDayInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
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
        public virtual CalendarDay CreateInValideCalendarDayInstance()
        {
            CalendarDay calendarday = this.CreateValideCalendarDayInstance();
             
			// Required   
            //Unique
			var existant_CalendarDay = this.CreateOrLouadFirstCalendarDay();
			calendarday.Reference = existant_CalendarDay.Reference;
 
            return calendarday;
        }


		public virtual CalendarDay CreateInValideCalendarDayInstance_ForEdit()
        {
            CalendarDay calendarday = this.CreateOrLouadFirstCalendarDay();
			// Required   
            //Unique
			var existant_CalendarDay = this.CreateOrLouadFirstCalendarDay();
			calendarday.Reference = existant_CalendarDay.Reference;
            return calendarday;
        }
    }

	public partial class CalendarDayTestDataFactory : BaseCalendarDayTestDataFactory{
	
		public CalendarDayTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
