﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
using TrainingIS.Entities.ModelsViews.Authorizations;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    public class AuthrorizationAppsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public AuthrorizationAppsControllerTests_Service()
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
        public AuthrorizationApp CreateOrLouadFirstAuthrorizationApp(UnitOfWork unitOfWork)
        {
            AuthrorizationAppBLO authrorizationappBLO = new AuthrorizationAppBLO(unitOfWork);
           
		   AuthrorizationApp entity = null;
            if (authrorizationappBLO.FindAll()?.Count > 0)
                entity = authrorizationappBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp AuthrorizationApp for Test
                entity = this.CreateValideAuthrorizationAppInstance();
                authrorizationappBLO.Save(entity);
            }
            return entity;
        }

        public AuthrorizationApp CreateValideAuthrorizationAppInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            AuthrorizationApp  Valide_AuthrorizationApp = this._Fixture.Create<AuthrorizationApp>();
            Valide_AuthrorizationApp.Id = 0;
            // Many to One 
            //
			// ControllerApp
			var ControllerApp = new ControllerAppsControllerTests_Service().CreateOrLouadFirstControllerApp(unitOfWork);
            Valide_AuthrorizationApp.ControllerApp = null;
            Valide_AuthrorizationApp.ControllerAppId = ControllerApp.Id;
			// RoleApp
			var RoleApp = new RoleAppsControllerTests_Service().CreateOrLouadFirstRoleApp(unitOfWork);
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
        public AuthrorizationApp CreateInValideAuthrorizationAppInstance(UnitOfWork unitOfWork = null)
        {
            AuthrorizationApp authrorizationapp = this.CreateValideAuthrorizationAppInstance(unitOfWork);
             
			// Required   
 
			authrorizationapp.RoleAppId = 0;
 
			authrorizationapp.ControllerAppId = 0;
 
			authrorizationapp.isAllAction = false;
            //Unique
			var existant_AuthrorizationApp = this.CreateOrLouadFirstAuthrorizationApp(new UnitOfWork());
            
            return authrorizationapp;
        }


		  public AuthrorizationApp CreateInValideAuthrorizationAppInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            AuthrorizationApp authrorizationapp = this.CreateOrLouadFirstAuthrorizationApp(unitOfWork);
             
			// Required   
 
			authrorizationapp.RoleAppId = 0;
 
			authrorizationapp.ControllerAppId = 0;
 
			authrorizationapp.isAllAction = false;
            //Unique
			var existant_AuthrorizationApp = this.CreateOrLouadFirstAuthrorizationApp(new UnitOfWork());
            
            return authrorizationapp;
        }
    }
}

