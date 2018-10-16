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
    public class BaseStateOfAbseceTestDataFactory : ITestDataFactory<StateOfAbsece>
    {
        private Fixture _Fixture = null;
		protected List<StateOfAbsece> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseStateOfAbseceTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<StateOfAbsece> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<StateOfAbsece> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first StateOfAbsece instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual StateOfAbsece CreateOrLouadFirstStateOfAbsece()
        {
            StateOfAbseceBLO stateofabseceBLO = new StateOfAbseceBLO(UnitOfWork,GAppContext);
           
			StateOfAbsece entity = null;
            if (stateofabseceBLO.FindAll()?.Count > 0)
                entity = stateofabseceBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp StateOfAbsece for Test
                entity = this.CreateValideStateOfAbseceInstance();
                stateofabseceBLO.Save(entity);
            }
            return entity;
        }

        public virtual StateOfAbsece CreateValideStateOfAbseceInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            StateOfAbsece  Valide_StateOfAbsece = this._Fixture.Create<StateOfAbsece>();
            Valide_StateOfAbsece.Id = 0;
            // Many to One 
            //
			// Trainee
			var Trainee = new TraineeTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainee();
            Valide_StateOfAbsece.Trainee = null;
            Valide_StateOfAbsece.TraineeId = Trainee.Id;
            // One to Many
            //
            return Valide_StateOfAbsece;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide StateOfAbsece can't exist</returns>
        public virtual StateOfAbsece CreateInValideStateOfAbseceInstance()
        {
            StateOfAbsece stateofabsece = this.CreateValideStateOfAbseceInstance();
             
			// Required   
 
			stateofabsece.Name = null;
 
			stateofabsece.Category = StateOfAbseceCategories.TrainingYear;
 
			stateofabsece.Value = 0;
 
			stateofabsece.TraineeId = 0;
            //Unique
			var existant_StateOfAbsece = this.CreateOrLouadFirstStateOfAbsece();
			stateofabsece.Reference = existant_StateOfAbsece.Reference;
 
            return stateofabsece;
        }


		public virtual StateOfAbsece CreateInValideStateOfAbseceInstance_ForEdit()
        {
            StateOfAbsece stateofabsece = this.CreateOrLouadFirstStateOfAbsece();
			// Required   
 
			stateofabsece.Name = null;
 
			stateofabsece.Category = StateOfAbseceCategories.TrainingYear;
 
			stateofabsece.Value = 0;
 
			stateofabsece.TraineeId = 0;
            //Unique
			var existant_StateOfAbsece = this.CreateOrLouadFirstStateOfAbsece();
			stateofabsece.Reference = existant_StateOfAbsece.Reference;
            return stateofabsece;
        }
    }

	public partial class StateOfAbseceTestDataFactory : BaseStateOfAbseceTestDataFactory{
	
		public StateOfAbseceTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}