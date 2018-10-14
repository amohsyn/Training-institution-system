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
    public class BaseSpecialtyTestDataFactory : ITestDataFactory<Specialty>
    {
        private Fixture _Fixture = null;
		protected List<Specialty> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseSpecialtyTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<Specialty> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<Specialty> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first Specialty instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Specialty CreateOrLouadFirstSpecialty()
        {
            SpecialtyBLO specialtyBLO = new SpecialtyBLO(UnitOfWork,GAppContext);
           
			Specialty entity = null;
            if (specialtyBLO.FindAll()?.Count > 0)
                entity = specialtyBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Specialty for Test
                entity = this.CreateValideSpecialtyInstance();
                specialtyBLO.Save(entity);
            }
            return entity;
        }

        public virtual Specialty CreateValideSpecialtyInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Specialty  Valide_Specialty = this._Fixture.Create<Specialty>();
            Valide_Specialty.Id = 0;
            // Many to One 
            //
			// Sector
			var Sector = new SectorTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSector();
            Valide_Specialty.Sector = null;
            Valide_Specialty.SectorId = Sector.Id;
			// TrainingLevel
			var TrainingLevel = new TrainingLevelTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainingLevel();
            Valide_Specialty.TrainingLevel = null;
            Valide_Specialty.TrainingLevelId = TrainingLevel.Id;
            // One to Many
            //
            return Valide_Specialty;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Specialty can't exist</returns>
        public virtual Specialty CreateInValideSpecialtyInstance()
        {
            Specialty specialty = this.CreateValideSpecialtyInstance();
             
			// Required   
 
			specialty.SectorId = 0;
 
			specialty.TrainingLevelId = 0;
 
			specialty.Code = null;
 
			specialty.Name = null;
            //Unique
			var existant_Specialty = this.CreateOrLouadFirstSpecialty();
			specialty.Code = existant_Specialty.Code;
			specialty.Reference = existant_Specialty.Reference;
 
            return specialty;
        }


		public virtual Specialty CreateInValideSpecialtyInstance_ForEdit()
        {
            Specialty specialty = this.CreateOrLouadFirstSpecialty();
			// Required   
 
			specialty.SectorId = 0;
 
			specialty.TrainingLevelId = 0;
 
			specialty.Code = null;
 
			specialty.Name = null;
            //Unique
			var existant_Specialty = this.CreateOrLouadFirstSpecialty();
			specialty.Code = existant_Specialty.Code;
			specialty.Reference = existant_Specialty.Reference;
            return specialty;
        }
    }

	public partial class SpecialtyTestDataFactory : BaseSpecialtyTestDataFactory{
	
		public SpecialtyTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
