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
    public class BaseNationalityTestDataFactory : ITestDataFactory<Nationality>
    {
        private Fixture _Fixture = null;
		protected List<Nationality> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseNationalityTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<Nationality> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<Nationality> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first Nationality instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Nationality CreateOrLouadFirstNationality()
        {
            NationalityBLO nationalityBLO = new NationalityBLO(UnitOfWork,GAppContext);
           
			Nationality entity = null;
            if (nationalityBLO.FindAll()?.Count > 0)
                entity = nationalityBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Nationality for Test
                entity = this.CreateValideNationalityInstance();
                nationalityBLO.Save(entity);
            }
            return entity;
        }

        public virtual Nationality CreateValideNationalityInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Nationality  Valide_Nationality = this._Fixture.Create<Nationality>();
            Valide_Nationality.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_Nationality;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Nationality can't exist</returns>
        public virtual Nationality CreateInValideNationalityInstance()
        {
            Nationality nationality = this.CreateValideNationalityInstance();
             
			// Required   
 
			nationality.Code = null;
 
			nationality.Name = null;
            //Unique
			var existant_Nationality = this.CreateOrLouadFirstNationality();
			nationality.Code = existant_Nationality.Code;
			nationality.Reference = existant_Nationality.Reference;
 
            return nationality;
        }


		public virtual Nationality CreateInValideNationalityInstance_ForEdit()
        {
            Nationality nationality = this.CreateOrLouadFirstNationality();
			// Required   
 
			nationality.Code = null;
 
			nationality.Name = null;
            //Unique
			var existant_Nationality = this.CreateOrLouadFirstNationality();
			nationality.Code = existant_Nationality.Code;
			nationality.Reference = existant_Nationality.Reference;
            return nationality;
        }
    }

	public partial class NationalityTestDataFactory : BaseNationalityTestDataFactory{
	
		public NationalityTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
