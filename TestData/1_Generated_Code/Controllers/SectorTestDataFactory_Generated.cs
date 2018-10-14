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
    public class BaseSectorTestDataFactory : ITestDataFactory<Sector>
    {
        private Fixture _Fixture = null;
		protected List<Sector> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseSectorTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<Sector> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<Sector> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first Sector instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Sector CreateOrLouadFirstSector()
        {
            SectorBLO sectorBLO = new SectorBLO(UnitOfWork,GAppContext);
           
			Sector entity = null;
            if (sectorBLO.FindAll()?.Count > 0)
                entity = sectorBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Sector for Test
                entity = this.CreateValideSectorInstance();
                sectorBLO.Save(entity);
            }
            return entity;
        }

        public virtual Sector CreateValideSectorInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Sector  Valide_Sector = this._Fixture.Create<Sector>();
            Valide_Sector.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_Sector;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Sector can't exist</returns>
        public virtual Sector CreateInValideSectorInstance()
        {
            Sector sector = this.CreateValideSectorInstance();
             
			// Required   
 
			sector.Code = null;
 
			sector.Name = null;
            //Unique
			var existant_Sector = this.CreateOrLouadFirstSector();
			sector.Reference = existant_Sector.Reference;
 
            return sector;
        }


		public virtual Sector CreateInValideSectorInstance_ForEdit()
        {
            Sector sector = this.CreateOrLouadFirstSector();
			// Required   
 
			sector.Code = null;
 
			sector.Name = null;
            //Unique
			var existant_Sector = this.CreateOrLouadFirstSector();
			sector.Reference = existant_Sector.Reference;
            return sector;
        }
    }

	public partial class SectorTestDataFactory : BaseSectorTestDataFactory{
	
		public SectorTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
