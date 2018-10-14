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
    public class BaseControllerAppTestDataFactory : ITestDataFactory<ControllerApp>
    {
        private Fixture _Fixture = null;
		protected List<ControllerApp> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseControllerAppTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<ControllerApp> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<ControllerApp> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first ControllerApp instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual ControllerApp CreateOrLouadFirstControllerApp()
        {
            ControllerAppBLO controllerappBLO = new ControllerAppBLO(UnitOfWork,GAppContext);
           
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

        public virtual ControllerApp CreateValideControllerAppInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
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
        public virtual ControllerApp CreateInValideControllerAppInstance()
        {
            ControllerApp controllerapp = this.CreateValideControllerAppInstance();
             
			// Required   
 
			controllerapp.Code = null;
 
			controllerapp.Name = null;
            //Unique
			var existant_ControllerApp = this.CreateOrLouadFirstControllerApp();
			controllerapp.Reference = existant_ControllerApp.Reference;
 
            return controllerapp;
        }


		public virtual ControllerApp CreateInValideControllerAppInstance_ForEdit()
        {
            ControllerApp controllerapp = this.CreateOrLouadFirstControllerApp();
			// Required   
 
			controllerapp.Code = null;
 
			controllerapp.Name = null;
            //Unique
			var existant_ControllerApp = this.CreateOrLouadFirstControllerApp();
			controllerapp.Reference = existant_ControllerApp.Reference;
            return controllerapp;
        }
    }

	public partial class ControllerAppTestDataFactory : BaseControllerAppTestDataFactory{
	
		public ControllerAppTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
