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
    public class BaseTrainingLevelTestDataFactory : EntityTestData<TrainingLevel>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new TrainingLevelBLO(UnitOfWork, GAppContext);
        }

        public BaseTrainingLevelTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<TrainingLevel> Generate_TestData()
        {
            List<TrainingLevel> Data = base.Generate_TestData();
            if(Data == null) Data = new List<TrainingLevel>();
			TrainingLevel TrainingLevel = this.CreateValideTrainingLevelInstance();
            TrainingLevel.Reference = "ValideTrainingLevelInstance";
            Data.Add(TrainingLevel);
            return Data;
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
