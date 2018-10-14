using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using AutoFixture;
using TrainingIS.BLL;
using TrainingIS.DAL;
using System.ComponentModel.DataAnnotations;
using GApp.WebApp.Manager.Views;
using GApp.DAL;
using GApp.Entities;
using GApp.Core.Context;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;
using GApp.UnitTest.TestData;
using GApp.UnitTest.TestData.Enums;

namespace TestData
{
    public class BaseAuthrorizationAppTestDataFactory : ITestDataFactory<AuthrorizationApp>
    {
        private Fixture _Fixture = null;
		protected List<AuthrorizationApp> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseAuthrorizationAppTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<AuthrorizationApp> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<AuthrorizationApp> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first AuthrorizationApp instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual AuthrorizationApp CreateOrLouadFirstAuthrorizationApp()
        {
            AuthrorizationAppBLO authrorizationappBLO = new AuthrorizationAppBLO(UnitOfWork,GAppContext);
           
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

        public virtual AuthrorizationApp CreateValideAuthrorizationAppInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            AuthrorizationApp  Valide_AuthrorizationApp = this._Fixture.Create<AuthrorizationApp>();
            Valide_AuthrorizationApp.Id = 0;
            // Many to One 
            //
			// ControllerApp
			var ControllerApp = new ControllerAppTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstControllerApp();
            Valide_AuthrorizationApp.ControllerApp = null;
            Valide_AuthrorizationApp.ControllerAppId = ControllerApp.Id;
			// RoleApp
			var RoleApp = new RoleAppTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstRoleApp();
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
        public virtual AuthrorizationApp CreateInValideAuthrorizationAppInstance()
        {
            AuthrorizationApp authrorizationapp = this.CreateValideAuthrorizationAppInstance();
             
			// Required   
 
			authrorizationapp.RoleAppId = 0;
 
			authrorizationapp.ControllerAppId = 0;
 
			authrorizationapp.isAllAction = false;
            //Unique
			var existant_AuthrorizationApp = this.CreateOrLouadFirstAuthrorizationApp();
			authrorizationapp.Reference = existant_AuthrorizationApp.Reference;
 
            return authrorizationapp;
        }


		public virtual AuthrorizationApp CreateInValideAuthrorizationAppInstance_ForEdit()
        {
            AuthrorizationApp authrorizationapp = this.CreateOrLouadFirstAuthrorizationApp();
			// Required   
 
			authrorizationapp.RoleAppId = 0;
 
			authrorizationapp.ControllerAppId = 0;
 
			authrorizationapp.isAllAction = false;
            //Unique
			var existant_AuthrorizationApp = this.CreateOrLouadFirstAuthrorizationApp();
			authrorizationapp.Reference = existant_AuthrorizationApp.Reference;
            return authrorizationapp;
        }
    }

	public partial class AuthrorizationAppTestDataFactory : BaseAuthrorizationAppTestDataFactory{
	
		public AuthrorizationAppTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
