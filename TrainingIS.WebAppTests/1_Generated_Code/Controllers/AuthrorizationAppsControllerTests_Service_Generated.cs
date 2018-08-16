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
    public class BaseAuthrorizationAppsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseAuthrorizationAppsControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first AuthrorizationApp instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual AuthrorizationApp CreateOrLouadFirstAuthrorizationApp(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            AuthrorizationAppBLO authrorizationappBLO = new AuthrorizationAppBLO(unitOfWork,GAppContext);
           
			AuthrorizationApp entity = null;
            if (authrorizationappBLO.FindAll()?.Count > 0)
                entity = authrorizationappBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp AuthrorizationApp for Test
                entity = this.CreateValideAuthrorizationAppInstance(unitOfWork,GAppContext);
                authrorizationappBLO.Save(entity);
            }
            return entity;
        }

        public virtual AuthrorizationApp CreateValideAuthrorizationAppInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            AuthrorizationApp  Valide_AuthrorizationApp = this._Fixture.Create<AuthrorizationApp>();
            Valide_AuthrorizationApp.Id = 0;
            // Many to One 
            //
			// ControllerApp
			var ControllerApp = new ControllerAppsControllerTests_Service().CreateOrLouadFirstControllerApp(unitOfWork,GAppContext);
            Valide_AuthrorizationApp.ControllerApp = null;
            Valide_AuthrorizationApp.ControllerAppId = ControllerApp.Id;
			// RoleApp
			var RoleApp = new RoleAppsControllerTests_Service().CreateOrLouadFirstRoleApp(unitOfWork,GAppContext);
            Valide_AuthrorizationApp.RoleApp = null;
            Valide_AuthrorizationApp.RoleAppId = RoleApp.Id;
            // One to Many
            //
			Valide_AuthrorizationApp.ActionControllerApps = null;
            return Valide_AuthrorizationApp;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide AuthrorizationApp can't exist</returns>
        public virtual AuthrorizationApp CreateInValideAuthrorizationAppInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            AuthrorizationApp authrorizationapp = this.CreateValideAuthrorizationAppInstance(unitOfWork, GAppContext);
             
			// Required   
 
			authrorizationapp.RoleAppId = 0;
 
			authrorizationapp.ControllerAppId = 0;
 
			authrorizationapp.isAllAction = false;
            //Unique
			var existant_AuthrorizationApp = this.CreateOrLouadFirstAuthrorizationApp(new UnitOfWork<TrainingISModel>(),GAppContext);
 
            return authrorizationapp;
        }


		public virtual AuthrorizationApp CreateInValideAuthrorizationAppInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            AuthrorizationApp authrorizationapp = this.CreateOrLouadFirstAuthrorizationApp(unitOfWork, GAppContext);
			// Required   
 
			authrorizationapp.RoleAppId = 0;
 
			authrorizationapp.ControllerAppId = 0;
 
			authrorizationapp.isAllAction = false;
            //Unique
			var existant_AuthrorizationApp = this.CreateOrLouadFirstAuthrorizationApp(new UnitOfWork<TrainingISModel>(), GAppContext);
            return authrorizationapp;
        }
    }

	public partial class AuthrorizationAppsControllerTests_Service : BaseAuthrorizationAppsControllerTests_Service{}
}
