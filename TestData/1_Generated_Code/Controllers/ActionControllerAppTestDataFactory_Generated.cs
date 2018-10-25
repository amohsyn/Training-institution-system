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
    public class BaseActionControllerAppTestDataFactory : EntityTestData<ActionControllerApp>
    {
        public BaseActionControllerAppTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<ActionControllerApp> Generate_TestData()
        {
            List<ActionControllerApp> Data = base.Generate_TestData();
            if(Data == null) Data = new List<ActionControllerApp>();
            Data.Add(this.CreateValideActionControllerAppInstance());
            return Data;
        }
	
		/// <summary>
        /// Find the first ActionControllerApp instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual ActionControllerApp CreateOrLouadFirstActionControllerApp()
        {
            ActionControllerAppBLO actioncontrollerappBLO = new ActionControllerAppBLO(UnitOfWork,GAppContext);
           
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

        public virtual ActionControllerApp CreateValideActionControllerAppInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            ActionControllerApp  Valide_ActionControllerApp = this._Fixture.Create<ActionControllerApp>();
            Valide_ActionControllerApp.Id = 0;
            // Many to One 
            //   
			// ControllerApp
			var ControllerApp = new ControllerAppTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstControllerApp();
            Valide_ActionControllerApp.ControllerApp = ControllerApp;
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
        public virtual ActionControllerApp CreateInValideActionControllerAppInstance()
        {
            ActionControllerApp actioncontrollerapp = this.CreateValideActionControllerAppInstance();
             
			// Required   
 
			actioncontrollerapp.Code = null;
 
			actioncontrollerapp.Name = null;
 
			actioncontrollerapp.ControllerAppId = 0;
            //Unique
			var existant_ActionControllerApp = this.CreateOrLouadFirstActionControllerApp();
			actioncontrollerapp.Reference = existant_ActionControllerApp.Reference;
 
            return actioncontrollerapp;
        }


		public virtual ActionControllerApp CreateInValideActionControllerAppInstance_ForEdit()
        {
            ActionControllerApp actioncontrollerapp = this.CreateOrLouadFirstActionControllerApp();
			// Required   
 
			actioncontrollerapp.Code = null;
 
			actioncontrollerapp.Name = null;
 
			actioncontrollerapp.ControllerAppId = 0;
            //Unique
			var existant_ActionControllerApp = this.CreateOrLouadFirstActionControllerApp();
			actioncontrollerapp.Reference = existant_ActionControllerApp.Reference;
            return actioncontrollerapp;
        }
    }

	public partial class ActionControllerAppTestDataFactory : BaseActionControllerAppTestDataFactory{
	
		public ActionControllerAppTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
