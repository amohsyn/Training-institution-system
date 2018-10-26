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
using TrainingIS.Models.SeanceTrainings;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;
using GApp.UnitTest.TestData;
using GApp.UnitTest.TestData.Enums;

namespace TestData
{
    public class BaseSeanceTrainingTestDataFactory : EntityTestData<SeanceTraining>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new SeanceTrainingBLO(UnitOfWork, GAppContext);
        }

        public BaseSeanceTrainingTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<SeanceTraining> Generate_TestData()
        {
            List<SeanceTraining> Data = base.Generate_TestData();
            if(Data == null) Data = new List<SeanceTraining>();
			SeanceTraining SeanceTraining = this.CreateValideSeanceTrainingInstance();
            SeanceTraining.Reference = "ValideSeanceTrainingInstance";
            Data.Add(SeanceTraining);
            return Data;
        }
	
		/// <summary>
        /// Find the first SeanceTraining instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual SeanceTraining CreateOrLouadFirstSeanceTraining()
        {
            SeanceTrainingBLO seancetrainingBLO = new SeanceTrainingBLO(UnitOfWork,GAppContext);
           
			SeanceTraining entity = null;
            if (seancetrainingBLO.FindAll()?.Count > 0)
                entity = seancetrainingBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp SeanceTraining for Test
                entity = this.CreateValideSeanceTrainingInstance();
                seancetrainingBLO.Save(entity);
            }
            return entity;
        }

        public virtual SeanceTraining CreateValideSeanceTrainingInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            SeanceTraining  Valide_SeanceTraining = this._Fixture.Create<SeanceTraining>();
            Valide_SeanceTraining.Id = 0;
            // Many to One 
            //   
			// SeancePlanning
			var SeancePlanning = new SeancePlanningTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSeancePlanning();
            Valide_SeanceTraining.SeancePlanning = SeancePlanning;
						 Valide_SeanceTraining.SeancePlanningId = SeancePlanning.Id;
			           
            // One to Many
            //
			Valide_SeanceTraining.Absences = null;
            return Valide_SeanceTraining;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide SeanceTraining can't exist</returns>
        public virtual SeanceTraining CreateInValideSeanceTrainingInstance()
        {
            SeanceTraining seancetraining = this.CreateValideSeanceTrainingInstance();
             
			// Required   
 
			seancetraining.SeanceDate = null;
 
			seancetraining.SeancePlanningId = 0;
            //Unique
			var existant_SeanceTraining = this.CreateOrLouadFirstSeanceTraining();
			seancetraining.Reference = existant_SeanceTraining.Reference;
 
            return seancetraining;
        }


		public virtual SeanceTraining CreateInValideSeanceTrainingInstance_ForEdit()
        {
            SeanceTraining seancetraining = this.CreateOrLouadFirstSeanceTraining();
			// Required   
 
			seancetraining.SeanceDate = null;
 
			seancetraining.SeancePlanningId = 0;
            //Unique
			var existant_SeanceTraining = this.CreateOrLouadFirstSeanceTraining();
			seancetraining.Reference = existant_SeanceTraining.Reference;
            return seancetraining;
        }
    }

	public partial class SeanceTrainingTestDataFactory : BaseSeanceTrainingTestDataFactory{
	
		public SeanceTrainingTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
