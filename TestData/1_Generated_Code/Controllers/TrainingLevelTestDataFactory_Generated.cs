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
    public class BaseTrainingLevelTestDataFactory : ITestDataFactory<TrainingLevel>
    {
        private Fixture _Fixture = null;
		protected List<TrainingLevel> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseTrainingLevelTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<TrainingLevel> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<TrainingLevel> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first TrainingLevel instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual TrainingLevel CreateOrLouadFirstTrainingLevel()
        {
            TrainingLevelBLO traininglevelBLO = new TrainingLevelBLO(UnitOfWork,GAppContext);
           
			TrainingLevel entity = null;
            if (traininglevelBLO.FindAll()?.Count > 0)
                entity = traininglevelBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp TrainingLevel for Test
                entity = this.CreateValideTrainingLevelInstance();
                traininglevelBLO.Save(entity);
            }
            return entity;
        }

        public virtual TrainingLevel CreateValideTrainingLevelInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            TrainingLevel  Valide_TrainingLevel = this._Fixture.Create<TrainingLevel>();
            Valide_TrainingLevel.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_TrainingLevel;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide TrainingLevel can't exist</returns>
        public virtual TrainingLevel CreateInValideTrainingLevelInstance()
        {
            TrainingLevel traininglevel = this.CreateValideTrainingLevelInstance();
             
			// Required   
 
			traininglevel.Code = null;
 
			traininglevel.Name = null;
            //Unique
			var existant_TrainingLevel = this.CreateOrLouadFirstTrainingLevel();
			traininglevel.Reference = existant_TrainingLevel.Reference;
 
            return traininglevel;
        }


		public virtual TrainingLevel CreateInValideTrainingLevelInstance_ForEdit()
        {
            TrainingLevel traininglevel = this.CreateOrLouadFirstTrainingLevel();
			// Required   
 
			traininglevel.Code = null;
 
			traininglevel.Name = null;
            //Unique
			var existant_TrainingLevel = this.CreateOrLouadFirstTrainingLevel();
			traininglevel.Reference = existant_TrainingLevel.Reference;
            return traininglevel;
        }
    }

	public partial class TrainingLevelTestDataFactory : BaseTrainingLevelTestDataFactory{
	
		public TrainingLevelTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
