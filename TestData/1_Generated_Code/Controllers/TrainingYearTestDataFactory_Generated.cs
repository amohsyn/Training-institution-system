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
    public class BaseTrainingYearTestDataFactory : ITestDataFactory<TrainingYear>
    {
        private Fixture _Fixture = null;
		protected List<TrainingYear> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseTrainingYearTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<TrainingYear> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<TrainingYear> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first TrainingYear instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual TrainingYear CreateOrLouadFirstTrainingYear()
        {
            TrainingYearBLO trainingyearBLO = new TrainingYearBLO(UnitOfWork,GAppContext);
           
			TrainingYear entity = null;
            if (trainingyearBLO.FindAll()?.Count > 0)
                entity = trainingyearBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp TrainingYear for Test
                entity = this.CreateValideTrainingYearInstance();
                trainingyearBLO.Save(entity);
            }
            return entity;
        }

        public virtual TrainingYear CreateValideTrainingYearInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            TrainingYear  Valide_TrainingYear = this._Fixture.Create<TrainingYear>();
            Valide_TrainingYear.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_TrainingYear;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide TrainingYear can't exist</returns>
        public virtual TrainingYear CreateInValideTrainingYearInstance()
        {
            TrainingYear trainingyear = this.CreateValideTrainingYearInstance();
             
			// Required   
 
			trainingyear.Code = null;
 
			trainingyear.StartDate = DateTime.Now;
 
			trainingyear.EndtDate = DateTime.Now;
            //Unique
			var existant_TrainingYear = this.CreateOrLouadFirstTrainingYear();
			trainingyear.Code = existant_TrainingYear.Code;
			trainingyear.Reference = existant_TrainingYear.Reference;
 
            return trainingyear;
        }


		public virtual TrainingYear CreateInValideTrainingYearInstance_ForEdit()
        {
            TrainingYear trainingyear = this.CreateOrLouadFirstTrainingYear();
			// Required   
 
			trainingyear.Code = null;
 
			trainingyear.StartDate = DateTime.Now;
 
			trainingyear.EndtDate = DateTime.Now;
            //Unique
			var existant_TrainingYear = this.CreateOrLouadFirstTrainingYear();
			trainingyear.Code = existant_TrainingYear.Code;
			trainingyear.Reference = existant_TrainingYear.Reference;
            return trainingyear;
        }
    }

	public partial class TrainingYearTestDataFactory : BaseTrainingYearTestDataFactory{
	
		public TrainingYearTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
