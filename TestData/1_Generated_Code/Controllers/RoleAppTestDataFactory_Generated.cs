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
    public class BaseRoleAppTestDataFactory : EntityTestData<RoleApp>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new RoleAppBLO(UnitOfWork, GAppContext);
        }

        public BaseRoleAppTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<RoleApp> Generate_TestData()
        {
            List<RoleApp> Data = base.Generate_TestData();
            if(Data == null) Data = new List<RoleApp>();
			RoleApp RoleApp = this.CreateValideRoleAppInstance();
            RoleApp.Reference = "ValideRoleAppInstance";
            Data.Add(RoleApp);
            return Data;
        }
	
		/// <summary>
        /// Find the first RoleApp instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual RoleApp CreateOrLouadFirstRoleApp()
        {
            RoleAppBLO roleappBLO = new RoleAppBLO(UnitOfWork,GAppContext);
           
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

        public virtual RoleApp CreateValideRoleAppInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
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
        public virtual RoleApp CreateInValideRoleAppInstance()
        {
            RoleApp roleapp = this.CreateValideRoleAppInstance();
             
			// Required   
 
			roleapp.Code = null;
            //Unique
			var existant_RoleApp = this.CreateOrLouadFirstRoleApp();
			roleapp.Reference = existant_RoleApp.Reference;
 
            return roleapp;
        }


		public virtual RoleApp CreateInValideRoleAppInstance_ForEdit()
        {
            RoleApp roleapp = this.CreateOrLouadFirstRoleApp();
			// Required   
 
			roleapp.Code = null;
            //Unique
			var existant_RoleApp = this.CreateOrLouadFirstRoleApp();
			roleapp.Reference = existant_RoleApp.Reference;
            return roleapp;
        }
    }

	public partial class RoleAppTestDataFactory : BaseRoleAppTestDataFactory{
	
		public RoleAppTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
