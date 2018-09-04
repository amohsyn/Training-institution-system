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
    public class BaseActionControllerAppsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseActionControllerAppsControllerTests_Service()
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
        public virtual ActionControllerApp CreateOrLouadFirstActionControllerApp(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            ActionControllerAppBLO actioncontrollerappBLO = new ActionControllerAppBLO(unitOfWork,GAppContext);
           
			ActionControllerApp entity = null;
            if (actioncontrollerappBLO.FindAll()?.Count > 0)
                entity = actioncontrollerappBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp ActionControllerApp for Test
                entity = this.CreateValideActionControllerAppInstance(unitOfWork,GAppContext);
                actioncontrollerappBLO.Save(entity);
            }
            return entity;
        }

        public virtual ActionControllerApp CreateValideActionControllerAppInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            ActionControllerApp  Valide_ActionControllerApp = this._Fixture.Create<ActionControllerApp>();
            Valide_ActionControllerApp.Id = 0;
            // Many to One 
            //
			// ControllerApp
			var ControllerApp = new ControllerAppsControllerTests_Service().CreateOrLouadFirstControllerApp(unitOfWork,GAppContext);
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
        public virtual ActionControllerApp CreateInValideActionControllerAppInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            ActionControllerApp actioncontrollerapp = this.CreateValideActionControllerAppInstance(unitOfWork, GAppContext);
             
			// Required   
 
			actioncontrollerapp.Code = null;
 
			actioncontrollerapp.Name = null;
 
			actioncontrollerapp.ControllerAppId = 0;
            //Unique
			var existant_ActionControllerApp = this.CreateOrLouadFirstActionControllerApp(new UnitOfWork<TrainingISModel>(),GAppContext);
			actioncontrollerapp.Reference = existant_ActionControllerApp.Reference;
 
            return actioncontrollerapp;
        }


		public virtual ActionControllerApp CreateInValideActionControllerAppInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            ActionControllerApp actioncontrollerapp = this.CreateOrLouadFirstActionControllerApp(unitOfWork, GAppContext);
			// Required   
 
			actioncontrollerapp.Code = null;
 
			actioncontrollerapp.Name = null;
 
			actioncontrollerapp.ControllerAppId = 0;
            //Unique
			var existant_ActionControllerApp = this.CreateOrLouadFirstActionControllerApp(new UnitOfWork<TrainingISModel>(), GAppContext);
			actioncontrollerapp.Reference = existant_ActionControllerApp.Reference;
            return actioncontrollerapp;
        }
    }

	public partial class ActionControllerAppsControllerTests_Service : BaseActionControllerAppsControllerTests_Service{}
}
