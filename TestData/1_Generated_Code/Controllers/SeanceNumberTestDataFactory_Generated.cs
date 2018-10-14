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
    public class BaseSeanceNumberTestDataFactory : ITestDataFactory<SeanceNumber>
    {
        private Fixture _Fixture = null;
		protected List<SeanceNumber> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseSeanceNumberTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<SeanceNumber> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<SeanceNumber> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first SeanceNumber instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual SeanceNumber CreateOrLouadFirstSeanceNumber()
        {
            SeanceNumberBLO seancenumberBLO = new SeanceNumberBLO(UnitOfWork,GAppContext);
           
			SeanceNumber entity = null;
            if (seancenumberBLO.FindAll()?.Count > 0)
                entity = seancenumberBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp SeanceNumber for Test
                entity = this.CreateValideSeanceNumberInstance();
                seancenumberBLO.Save(entity);
            }
            return entity;
        }

        public virtual SeanceNumber CreateValideSeanceNumberInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            SeanceNumber  Valide_SeanceNumber = this._Fixture.Create<SeanceNumber>();
            Valide_SeanceNumber.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_SeanceNumber;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide SeanceNumber can't exist</returns>
        public virtual SeanceNumber CreateInValideSeanceNumberInstance()
        {
            SeanceNumber seancenumber = this.CreateValideSeanceNumberInstance();
             
			// Required   
 
			seancenumber.Code = null;
 
			seancenumber.StartTime = DateTime.Now;
 
			seancenumber.EndTime = DateTime.Now;
            //Unique
			var existant_SeanceNumber = this.CreateOrLouadFirstSeanceNumber();
			seancenumber.Code = existant_SeanceNumber.Code;
			seancenumber.Reference = existant_SeanceNumber.Reference;
 
            return seancenumber;
        }


		public virtual SeanceNumber CreateInValideSeanceNumberInstance_ForEdit()
        {
            SeanceNumber seancenumber = this.CreateOrLouadFirstSeanceNumber();
			// Required   
 
			seancenumber.Code = null;
 
			seancenumber.StartTime = DateTime.Now;
 
			seancenumber.EndTime = DateTime.Now;
            //Unique
			var existant_SeanceNumber = this.CreateOrLouadFirstSeanceNumber();
			seancenumber.Code = existant_SeanceNumber.Code;
			seancenumber.Reference = existant_SeanceNumber.Reference;
            return seancenumber;
        }
    }

	public partial class SeanceNumberTestDataFactory : BaseSeanceNumberTestDataFactory{
	
		public SeanceNumberTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
