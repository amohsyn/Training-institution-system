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
using GApp.WebApp.Tests;
using GApp.WebApp.Manager.Views;
using TrainingIS.WebApp.Tests.TestUtilities;
using GApp.DAL;
using GApp.Entities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Tests.Services 
{
    public class BaseRoleAppsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseRoleAppsControllerTests_Service()
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
        public virtual RoleApp CreateOrLouadFirstRoleApp(UnitOfWork<TrainingISModel> unitOfWork)
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

        public virtual RoleApp CreateValideRoleAppInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
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
        public virtual RoleApp CreateInValideRoleAppInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            RoleApp roleapp = this.CreateValideRoleAppInstance(unitOfWork);
             
			// Required   
 
			roleapp.Code = null;
            //Unique
			var existant_RoleApp = this.CreateOrLouadFirstRoleApp(new UnitOfWork<TrainingISModel>());
 
            return roleapp;
        }


		public virtual RoleApp CreateInValideRoleAppInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            RoleApp roleapp = this.CreateOrLouadFirstRoleApp(unitOfWork);
			// Required   
 
			roleapp.Code = null;
            //Unique
			var existant_RoleApp = this.CreateOrLouadFirstRoleApp(new UnitOfWork<TrainingISModel>());
            return roleapp;
        }
    }

	public partial class RoleAppsControllerTests_Service : BaseRoleAppsControllerTests_Service{}
}
