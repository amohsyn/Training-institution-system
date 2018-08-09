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
    public class RoleAppsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public RoleAppsControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first RoleApp instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public RoleApp CreateOrLouadFirstRoleApp(UnitOfWork unitOfWork)
        {
            RoleAppBLO roleappBLO = new RoleAppBLO(unitOfWork);
           
		   RoleApp entity = null;
            if (roleappBLO.FindAll()?.Count > 0)
                entity = roleappBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp RoleApp for Test
                entity = this.CreateValideRoleAppInstance();
                roleappBLO.Save(entity);
            }
            return entity;
        }

        public RoleApp CreateValideRoleAppInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            RoleApp  Valide_RoleApp = this._Fixture.Create<RoleApp>();
            Valide_RoleApp.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_RoleApp;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide RoleApp can't exist</returns>
        public RoleApp CreateInValideRoleAppInstance(UnitOfWork unitOfWork = null)
        {
            RoleApp roleapp = this.CreateValideRoleAppInstance(unitOfWork);
             
			// Required   
 
			roleapp.Code = null;
            //Unique
			var existant_RoleApp = this.CreateOrLouadFirstRoleApp(new UnitOfWork());
            
            return roleapp;
        }


		  public RoleApp CreateInValideRoleAppInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            RoleApp roleapp = this.CreateOrLouadFirstRoleApp(unitOfWork);
             
			// Required   
 
			roleapp.Code = null;
            //Unique
			var existant_RoleApp = this.CreateOrLouadFirstRoleApp(new UnitOfWork());
            
            return roleapp;
        }
    }
}

