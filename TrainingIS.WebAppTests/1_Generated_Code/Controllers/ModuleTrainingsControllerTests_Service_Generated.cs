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
using GApp.Core.Context;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Tests.Services 
{
    public class BaseModuleTrainingsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseModuleTrainingsControllerTests_Service()
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
        public virtual ModuleTraining CreateOrLouadFirstModuleTraining(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            ModuleTrainingBLO moduletrainingBLO = new ModuleTrainingBLO(unitOfWork,GAppContext);
           
			ModuleTraining entity = null;
            if (moduletrainingBLO.FindAll()?.Count > 0)
                entity = moduletrainingBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp ModuleTraining for Test
                entity = this.CreateValideModuleTrainingInstance(unitOfWork,GAppContext);
                moduletrainingBLO.Save(entity);
            }
            return entity;
        }

        public virtual ModuleTraining CreateValideModuleTrainingInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            ModuleTraining  Valide_ModuleTraining = this._Fixture.Create<ModuleTraining>();
            Valide_ModuleTraining.Id = 0;
            // Many to One 
            //
			// Metier
			var Metier = new MetiersControllerTests_Service().CreateOrLouadFirstMetier(unitOfWork,GAppContext);
            Valide_ModuleTraining.Metier = null;
            Valide_ModuleTraining.MetierId = Metier.Id;
			// Specialty
			var Specialty = new SpecialtiesControllerTests_Service().CreateOrLouadFirstSpecialty(unitOfWork,GAppContext);
            Valide_ModuleTraining.Specialty = null;
            Valide_ModuleTraining.SpecialtyId = Specialty.Id;
			// YearStudy
			var YearStudy = new YearStudiesControllerTests_Service().CreateOrLouadFirstYearStudy(unitOfWork,GAppContext);
            Valide_ModuleTraining.YearStudy = null;
            Valide_ModuleTraining.YearStudyId = YearStudy.Id;
            // One to Many
            //
            return Valide_ModuleTraining;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide ModuleTraining can't exist</returns>
        public virtual ModuleTraining CreateInValideModuleTrainingInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            ModuleTraining moduletraining = this.CreateValideModuleTrainingInstance(unitOfWork, GAppContext);
             
			// Required   
 
			moduletraining.SpecialtyId = 0;
 
			moduletraining.MetierId = 0;
 
			moduletraining.YearStudyId = 0;
 
			moduletraining.Name = null;
            //Unique
			var existant_ModuleTraining = this.CreateOrLouadFirstModuleTraining(new UnitOfWork<TrainingISModel>(),GAppContext);
 
            return moduletraining;
        }


		public virtual ModuleTraining CreateInValideModuleTrainingInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            ModuleTraining moduletraining = this.CreateOrLouadFirstModuleTraining(unitOfWork, GAppContext);
			// Required   
 
			moduletraining.SpecialtyId = 0;
 
			moduletraining.MetierId = 0;
 
			moduletraining.YearStudyId = 0;
 
			moduletraining.Name = null;
            //Unique
			var existant_ModuleTraining = this.CreateOrLouadFirstModuleTraining(new UnitOfWork<TrainingISModel>(), GAppContext);
            return moduletraining;
        }
    }

	public partial class ModuleTrainingsControllerTests_Service : BaseModuleTrainingsControllerTests_Service{}
}
