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
    public class ActionControllerAppsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public ActionControllerAppsControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first ActionControllerApp instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public ActionControllerApp CreateOrLouadFirstActionControllerApp(UnitOfWork<TrainingISModel> unitOfWork)
        {
            ActionControllerAppBLO actioncontrollerappBLO = new ActionControllerAppBLO(unitOfWork);
           
		   ActionControllerApp entity = null;
            if (actioncontrollerappBLO.FindAll()?.Count > 0)
                entity = actioncontrollerappBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp ActionControllerApp for Test
                entity = this.CreateValideActionControllerAppInstance();
                actioncontrollerappBLO.Save(entity);
            }
            return entity;
        }

        public ActionControllerApp CreateValideActionControllerAppInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            ActionControllerApp  Valide_ActionControllerApp = this._Fixture.Create<ActionControllerApp>();
            Valide_ActionControllerApp.Id = 0;
            // Many to One 
            //
			// ControllerApp
			var ControllerApp = new ControllerAppsControllerTests_Service().CreateOrLouadFirstControllerApp(unitOfWork);
            Valide_ActionControllerApp.ControllerApp = null;
            Valide_ActionControllerApp.ControllerAppId = ControllerApp.Id;
            // One to Many
            //
			Valide_ActionControllerApp.AuthrorizationApps = null;
            return Valide_ActionControllerApp;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide ActionControllerApp can't exist</returns>
        public ActionControllerApp CreateInValideActionControllerAppInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            ActionControllerApp actioncontrollerapp = this.CreateValideActionControllerAppInstance(unitOfWork);
             
			// Required   
 
			actioncontrollerapp.Code = null;
 
			actioncontrollerapp.Name = null;
 
			actioncontrollerapp.ControllerAppId = 0;
            //Unique
			var existant_ActionControllerApp = this.CreateOrLouadFirstActionControllerApp(new UnitOfWork<TrainingISModel>());
            
            return actioncontrollerapp;
        }


		  public ActionControllerApp CreateInValideActionControllerAppInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            ActionControllerApp actioncontrollerapp = this.CreateOrLouadFirstActionControllerApp(unitOfWork);
             
			// Required   
 
			actioncontrollerapp.Code = null;
 
			actioncontrollerapp.Name = null;
 
			actioncontrollerapp.ControllerAppId = 0;
            //Unique
			var existant_ActionControllerApp = this.CreateOrLouadFirstActionControllerApp(new UnitOfWork<TrainingISModel>());
            
            return actioncontrollerapp;
        }
    }
}

