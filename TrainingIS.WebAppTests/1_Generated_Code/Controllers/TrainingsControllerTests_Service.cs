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
using TrainingIS.Entities.ModelsViews.Trainings;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    public class TrainingsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public TrainingsControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first Training instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public Training CreateOrLouadFirstTraining(UnitOfWork unitOfWork)
        {
            TrainingBLO trainingBLO = new TrainingBLO(unitOfWork);
           
		   Training entity = null;
            if (trainingBLO.FindAll()?.Count > 0)
                entity = trainingBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp Training for Test
                entity = this.CreateValideTrainingInstance();
                trainingBLO.Save(entity);
            }
            return entity;
        }

        public Training CreateValideTrainingInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            Training  Valide_Training = this._Fixture.Create<Training>();
            Valide_Training.Id = 0;
            // Many to One 
            //
			// Former
			var Former = new FormersControllerTests_Service().CreateOrLouadFirstFormer(unitOfWork);
            Valide_Training.Former = null;
            Valide_Training.FormerId = Former.Id;
			// Group
			var Group = new GroupsControllerTests_Service().CreateOrLouadFirstGroup(unitOfWork);
            Valide_Training.Group = null;
            Valide_Training.GroupId = Group.Id;
			// ModuleTraining
			var ModuleTraining = new ModuleTrainingsControllerTests_Service().CreateOrLouadFirstModuleTraining(unitOfWork);
            Valide_Training.ModuleTraining = null;
            Valide_Training.ModuleTrainingId = ModuleTraining.Id;
			// TrainingYear
			var TrainingYear = new TrainingYearsControllerTests_Service().CreateOrLouadFirstTrainingYear(unitOfWork);
            Valide_Training.TrainingYear = null;
            Valide_Training.TrainingYearId = TrainingYear.Id;
            // One to Many
            //
            return Valide_Training;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Training can't exist</returns>
        public Training CreateInValideTrainingInstance(UnitOfWork unitOfWork = null)
        {
            Training training = this.CreateValideTrainingInstance(unitOfWork);
             
			// Required   
 
			training.TrainingYearId = 0;
 
			training.ModuleTrainingId = 0;
 
			training.FormerId = 0;
 
			training.GroupId = 0;
            //Unique
			var existant_Training = this.CreateOrLouadFirstTraining(new UnitOfWork());
            
            return training;
        }


		  public Training CreateInValideTrainingInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            Training training = this.CreateOrLouadFirstTraining(unitOfWork);
             
			// Required   
 
			training.TrainingYearId = 0;
 
			training.ModuleTrainingId = 0;
 
			training.FormerId = 0;
 
			training.GroupId = 0;
            //Unique
			var existant_Training = this.CreateOrLouadFirstTraining(new UnitOfWork());
            
            return training;
        }
    }
}

