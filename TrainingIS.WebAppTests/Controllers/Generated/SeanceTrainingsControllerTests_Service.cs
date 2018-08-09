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
using TrainingIS.WebApp.Helpers.AlertMessages;
using GApp.WebApp.Tests;
using GApp.WebApp.Manager.Views;
using TrainingIS.WebApp.Tests.TestUtilities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    public class SeanceTrainingsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public SeanceTrainingsControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first SeanceTraining instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public SeanceTraining CreateOrLouadFirstSeanceTraining(UnitOfWork unitOfWork)
        {
            SeanceTrainingBLO seancetrainingBLO = new SeanceTrainingBLO(unitOfWork);
           
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

        public SeanceTraining CreateValideSeanceTrainingInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            SeanceTraining  Valide_SeanceTraining = this._Fixture.Create<SeanceTraining>();
            Valide_SeanceTraining.Id = 0;
            // Many to One 
            //
			// SeancePlanning
			var SeancePlanning = new SeancePlanningsControllerTests_Service().CreateOrLouadFirstSeancePlanning(unitOfWork);
            Valide_SeanceTraining.SeancePlanning = null;
            Valide_SeanceTraining.SeancePlanningId = SeancePlanning.Id;
            // One to Many
            //
            return Valide_SeanceTraining;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide SeanceTraining can't exist</returns>
        public SeanceTraining CreateInValideSeanceTrainingInstance(UnitOfWork unitOfWork = null)
        {
            SeanceTraining seancetraining = this.CreateValideSeanceTrainingInstance(unitOfWork);
             
			// Required   
 
			seancetraining.SeanceDate = null;
 
			seancetraining.SeancePlanningId = 0;
            //Unique
			var existant_SeanceTraining = this.CreateOrLouadFirstSeanceTraining(new UnitOfWork());
            
            return seancetraining;
        }


		  public SeanceTraining CreateInValideSeanceTrainingInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            SeanceTraining seancetraining = this.CreateOrLouadFirstSeanceTraining(unitOfWork);
             
			// Required   
 
			seancetraining.SeanceDate = null;
 
			seancetraining.SeancePlanningId = 0;
            //Unique
			var existant_SeanceTraining = this.CreateOrLouadFirstSeanceTraining(new UnitOfWork());
            
            return seancetraining;
        }
    }
}

