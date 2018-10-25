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
    public class BaseSectorTestDataFactory : EntityTestData<Sector>
    {
        public BaseSectorTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Sector> Generate_TestData()
        {
            List<Sector> Data = base.Generate_TestData();
            if(Data == null) Data = new List<Sector>();
            Data.Add(this.CreateValideSectorInstance());
            return Data;
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
