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
    public class BaseTrainingsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseTrainingsControllerTests_Service()
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
        public virtual Training CreateOrLouadFirstTraining(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            TrainingBLO trainingBLO = new TrainingBLO(unitOfWork,GAppContext);
           
			Training entity = null;
            if (trainingBLO.FindAll()?.Count > 0)
                entity = trainingBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Training for Test
                entity = this.CreateValideTrainingInstance(unitOfWork,GAppContext);
                trainingBLO.Save(entity);
            }
            return entity;
        }

        public virtual Training CreateValideTrainingInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            Training  Valide_Training = this._Fixture.Create<Training>();
            Valide_Training.Id = 0;
            // Many to One 
            //
			// Former
			var Former = new FormersControllerTests_Service().CreateOrLouadFirstFormer(unitOfWork,GAppContext);
            Valide_Training.Former = null;
            Valide_Training.FormerId = Former.Id;
			// Group
			var Group = new GroupsControllerTests_Service().CreateOrLouadFirstGroup(unitOfWork,GAppContext);
            Valide_Training.Group = null;
            Valide_Training.GroupId = Group.Id;
			// ModuleTraining
			var ModuleTraining = new ModuleTrainingsControllerTests_Service().CreateOrLouadFirstModuleTraining(unitOfWork,GAppContext);
            Valide_Training.ModuleTraining = null;
            Valide_Training.ModuleTrainingId = ModuleTraining.Id;
			// TrainingYear
			var TrainingYear = new TrainingYearsControllerTests_Service().CreateOrLouadFirstTrainingYear(unitOfWork,GAppContext);
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
        public virtual Training CreateInValideTrainingInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Training training = this.CreateValideTrainingInstance(unitOfWork, GAppContext);
             
			// Required   
 
			training.TrainingYearId = 0;
 
			training.ModuleTrainingId = 0;
 
			training.FormerId = 0;
 
			training.GroupId = 0;
            //Unique
			var existant_Training = this.CreateOrLouadFirstTraining(new UnitOfWork<TrainingISModel>(),GAppContext);
			training.Reference = existant_Training.Reference;
 
            return training;
        }


		public virtual Training CreateInValideTrainingInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Training training = this.CreateOrLouadFirstTraining(unitOfWork, GAppContext);
			// Required   
 
			training.TrainingYearId = 0;
 
			training.ModuleTrainingId = 0;
 
			training.FormerId = 0;
 
			training.GroupId = 0;
            //Unique
			var existant_Training = this.CreateOrLouadFirstTraining(new UnitOfWork<TrainingISModel>(), GAppContext);
			training.Reference = existant_Training.Reference;
            return training;
        }
    }

	public partial class TrainingsControllerTests_Service : BaseTrainingsControllerTests_Service{}
}
