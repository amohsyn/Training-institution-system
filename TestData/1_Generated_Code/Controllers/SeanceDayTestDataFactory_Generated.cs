﻿using System;
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
    public class BaseSeanceDayTestDataFactory : ITestDataFactory<SeanceDay>
    {
        private Fixture _Fixture = null;
		protected List<SeanceDay> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseSeanceDayTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<SeanceDay> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<SeanceDay> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first SeanceDay instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual SeanceDay CreateOrLouadFirstSeanceDay()
        {
            SeanceDayBLO seancedayBLO = new SeanceDayBLO(UnitOfWork,GAppContext);
           
			SeanceDay entity = null;
            if (seancedayBLO.FindAll()?.Count > 0)
                entity = seancedayBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp SeanceDay for Test
                entity = this.CreateValideSeanceDayInstance();
                seancedayBLO.Save(entity);
            }
            return entity;
        }

        public virtual SeanceDay CreateValideSeanceDayInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            SeanceDay  Valide_SeanceDay = this._Fixture.Create<SeanceDay>();
            Valide_SeanceDay.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_SeanceDay;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide SeanceDay can't exist</returns>
        public virtual SeanceDay CreateInValideSeanceDayInstance()
        {
            SeanceDay seanceday = this.CreateValideSeanceDayInstance();
             
			// Required   
 
			seanceday.Name = null;
 
			seanceday.Code = null;
            //Unique
			var existant_SeanceDay = this.CreateOrLouadFirstSeanceDay();
			seanceday.Code = existant_SeanceDay.Code;
			seanceday.Day = existant_SeanceDay.Day;
			seanceday.Reference = existant_SeanceDay.Reference;
 
            return seanceday;
        }


		public virtual SeanceDay CreateInValideSeanceDayInstance_ForEdit()
        {
            SeanceDay seanceday = this.CreateOrLouadFirstSeanceDay();
			// Required   
 
			seanceday.Name = null;
 
			seanceday.Code = null;
            //Unique
			var existant_SeanceDay = this.CreateOrLouadFirstSeanceDay();
			seanceday.Code = existant_SeanceDay.Code;
			seanceday.Day = existant_SeanceDay.Day;
			seanceday.Reference = existant_SeanceDay.Reference;
            return seanceday;
        }
    }

	public partial class SeanceDayTestDataFactory : BaseSeanceDayTestDataFactory{
	
		public SeanceDayTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}