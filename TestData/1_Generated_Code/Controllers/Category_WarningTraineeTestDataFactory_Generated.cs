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
    public class BaseCategory_WarningTraineeTestDataFactory : ITestDataFactory<Category_WarningTrainee>
    {
        private Fixture _Fixture = null;
		protected List<Category_WarningTrainee> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseCategory_WarningTraineeTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<Category_WarningTrainee> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<Category_WarningTrainee> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first Category_WarningTrainee instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Category_WarningTrainee CreateOrLouadFirstCategory_WarningTrainee()
        {
            Category_WarningTraineeBLO category_warningtraineeBLO = new Category_WarningTraineeBLO(UnitOfWork,GAppContext);
           
			Category_WarningTrainee entity = null;
            if (category_warningtraineeBLO.FindAll()?.Count > 0)
                entity = category_warningtraineeBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Category_WarningTrainee for Test
                entity = this.CreateValideCategory_WarningTraineeInstance();
                category_warningtraineeBLO.Save(entity);
            }
            return entity;
        }

        public virtual Category_WarningTrainee CreateValideCategory_WarningTraineeInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Category_WarningTrainee  Valide_Category_WarningTrainee = this._Fixture.Create<Category_WarningTrainee>();
            Valide_Category_WarningTrainee.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_Category_WarningTrainee;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Category_WarningTrainee can't exist</returns>
        public virtual Category_WarningTrainee CreateInValideCategory_WarningTraineeInstance()
        {
            Category_WarningTrainee category_warningtrainee = this.CreateValideCategory_WarningTraineeInstance();
             
			// Required   
 
			category_warningtrainee.Name = null;
            //Unique
			var existant_Category_WarningTrainee = this.CreateOrLouadFirstCategory_WarningTrainee();
			category_warningtrainee.Reference = existant_Category_WarningTrainee.Reference;
 
            return category_warningtrainee;
        }


		public virtual Category_WarningTrainee CreateInValideCategory_WarningTraineeInstance_ForEdit()
        {
            Category_WarningTrainee category_warningtrainee = this.CreateOrLouadFirstCategory_WarningTrainee();
			// Required   
 
			category_warningtrainee.Name = null;
            //Unique
			var existant_Category_WarningTrainee = this.CreateOrLouadFirstCategory_WarningTrainee();
			category_warningtrainee.Reference = existant_Category_WarningTrainee.Reference;
            return category_warningtrainee;
        }
    }

	public partial class Category_WarningTraineeTestDataFactory : BaseCategory_WarningTraineeTestDataFactory{
	
		public Category_WarningTraineeTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}