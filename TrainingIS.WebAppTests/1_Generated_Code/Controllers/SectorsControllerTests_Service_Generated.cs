using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrainingIS.Entities;
using AutoFixture;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.WebApp.Tests.ViewModels;
using System.ComponentModel.DataAnnotations;
using GApp.WebApp.Tests;
using GApp.WebApp.Manager.Views;
using TrainingIS.WebApp.Tests.TestUtilities;
using GApp.DAL;
using GApp.Entities;
using GApp.Core.Context;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Tests.Services 
{
    public class BaseSectorsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseSectorsControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first Sector instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Sector CreateOrLouadFirstSector(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            SectorBLO sectorBLO = new SectorBLO(unitOfWork,GAppContext);
           
			Sector entity = null;
            if (sectorBLO.FindAll()?.Count > 0)
                entity = sectorBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Sector for Test
                entity = this.CreateValideSectorInstance(unitOfWork,GAppContext);
                sectorBLO.Save(entity);
            }
            return entity;
        }

        public virtual Sector CreateValideSectorInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
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
        public virtual Sector CreateInValideSectorInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Sector sector = this.CreateValideSectorInstance(unitOfWork, GAppContext);
             
			// Required   
 
			sector.Code = null;
 
			sector.Name = null;
            //Unique
			var existant_Sector = this.CreateOrLouadFirstSector(new UnitOfWork<TrainingISModel>(),GAppContext);
			sector.Reference = existant_Sector.Reference;
 
            return sector;
        }


		public virtual Sector CreateInValideSectorInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Sector sector = this.CreateOrLouadFirstSector(unitOfWork, GAppContext);
			// Required   
 
			sector.Code = null;
 
			sector.Name = null;
            //Unique
			var existant_Sector = this.CreateOrLouadFirstSector(new UnitOfWork<TrainingISModel>(), GAppContext);
			sector.Reference = existant_Sector.Reference;
            return sector;
        }
    }

	public partial class SectorsControllerTests_Service : BaseSectorsControllerTests_Service{}
}
