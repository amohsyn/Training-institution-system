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
    public class StateOfAbsecesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public StateOfAbsecesControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first StateOfAbsece instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public StateOfAbsece CreateOrLouadFirstStateOfAbsece(UnitOfWork unitOfWork)
        {
            StateOfAbseceBLO stateofabseceBLO = new StateOfAbseceBLO(unitOfWork);
           
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

        public StateOfAbsece CreateValideStateOfAbseceInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            StateOfAbsece  Valide_StateOfAbsece = this._Fixture.Create<StateOfAbsece>();
            Valide_StateOfAbsece.Id = 0;
            // Many to One 
            //
			// Trainee
			var Trainee = new TraineesControllerTests_Service().CreateOrLouadFirstTrainee(unitOfWork);
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
        public StateOfAbsece CreateInValideStateOfAbseceInstance(UnitOfWork unitOfWork = null)
        {
            StateOfAbsece stateofabsece = this.CreateValideStateOfAbseceInstance(unitOfWork);
             
			// Required   
 
			stateofabsece.Name = null;
 
			stateofabsece.Category = StateOfAbseceCategories.Year;
 
			stateofabsece.Value = 0;
 
			stateofabsece.TraineeId = 0;
            //Unique
			var existant_StateOfAbsece = this.CreateOrLouadFirstStateOfAbsece(new UnitOfWork());
            
            return stateofabsece;
        }


		  public StateOfAbsece CreateInValideStateOfAbseceInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            StateOfAbsece stateofabsece = this.CreateOrLouadFirstStateOfAbsece(unitOfWork);
             
			// Required   
 
			stateofabsece.Name = null;
 
			stateofabsece.Category = StateOfAbseceCategories.Year;
 
			stateofabsece.Value = 0;
 
			stateofabsece.TraineeId = 0;
            //Unique
			var existant_StateOfAbsece = this.CreateOrLouadFirstStateOfAbsece(new UnitOfWork());
            
            return stateofabsece;
        }
    }
}

