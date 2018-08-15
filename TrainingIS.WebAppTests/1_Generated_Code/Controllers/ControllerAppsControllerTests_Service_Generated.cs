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
    public class ControllerAppsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public ControllerAppsControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first ControllerApp instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public ControllerApp CreateOrLouadFirstControllerApp(UnitOfWork<TrainingISModel> unitOfWork)
        {
            ControllerAppBLO controllerappBLO = new ControllerAppBLO(unitOfWork);
           
		   ControllerApp entity = null;
            if (controllerappBLO.FindAll()?.Count > 0)
                entity = controllerappBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp ControllerApp for Test
                entity = this.CreateValideControllerAppInstance();
                controllerappBLO.Save(entity);
            }
            return entity;
        }

        public ControllerApp CreateValideControllerAppInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            ControllerApp  Valide_ControllerApp = this._Fixture.Create<ControllerApp>();
            Valide_ControllerApp.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_ControllerApp;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide ControllerApp can't exist</returns>
        public ControllerApp CreateInValideControllerAppInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            ControllerApp controllerapp = this.CreateValideControllerAppInstance(unitOfWork);
             
			// Required   
 
			controllerapp.Code = null;
 
			controllerapp.Name = null;
            //Unique
			var existant_ControllerApp = this.CreateOrLouadFirstControllerApp(new UnitOfWork<TrainingISModel>());
            
            return controllerapp;
        }


		  public ControllerApp CreateInValideControllerAppInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            ControllerApp controllerapp = this.CreateOrLouadFirstControllerApp(unitOfWork);
             
			// Required   
 
			controllerapp.Code = null;
 
			controllerapp.Name = null;
            //Unique
			var existant_ControllerApp = this.CreateOrLouadFirstControllerApp(new UnitOfWork<TrainingISModel>());
            
            return controllerapp;
        }
    }
}

