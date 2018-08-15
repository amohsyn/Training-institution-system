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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    public class ModuleTrainingsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public ModuleTrainingsControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first ModuleTraining instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public ModuleTraining CreateOrLouadFirstModuleTraining(UnitOfWork<TrainingISModel> unitOfWork)
        {
            ModuleTrainingBLO moduletrainingBLO = new ModuleTrainingBLO(unitOfWork);
           
		   ModuleTraining entity = null;
            if (moduletrainingBLO.FindAll()?.Count > 0)
                entity = moduletrainingBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp ModuleTraining for Test
                entity = this.CreateValideModuleTrainingInstance();
                moduletrainingBLO.Save(entity);
            }
            return entity;
        }

        public ModuleTraining CreateValideModuleTrainingInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            ModuleTraining  Valide_ModuleTraining = this._Fixture.Create<ModuleTraining>();
            Valide_ModuleTraining.Id = 0;
            // Many to One 
            //
			// Specialty
			var Specialty = new SpecialtiesControllerTests_Service().CreateOrLouadFirstSpecialty(unitOfWork);
            Valide_ModuleTraining.Specialty = null;
            Valide_ModuleTraining.SpecialtyId = Specialty.Id;
            // One to Many
            //
            return Valide_ModuleTraining;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide ModuleTraining can't exist</returns>
        public ModuleTraining CreateInValideModuleTrainingInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            ModuleTraining moduletraining = this.CreateValideModuleTrainingInstance(unitOfWork);
             
			// Required   
 
			moduletraining.SpecialtyId = 0;
 
			moduletraining.Name = null;
            //Unique
			var existant_ModuleTraining = this.CreateOrLouadFirstModuleTraining(new UnitOfWork<TrainingISModel>());
            
            return moduletraining;
        }


		  public ModuleTraining CreateInValideModuleTrainingInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            ModuleTraining moduletraining = this.CreateOrLouadFirstModuleTraining(unitOfWork);
             
			// Required   
 
			moduletraining.SpecialtyId = 0;
 
			moduletraining.Name = null;
            //Unique
			var existant_ModuleTraining = this.CreateOrLouadFirstModuleTraining(new UnitOfWork<TrainingISModel>());
            
            return moduletraining;
        }
    }
}

