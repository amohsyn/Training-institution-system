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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    public class TrainingYearsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public TrainingYearsControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first TrainingYear instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public TrainingYear CreateOrLouadFirstTrainingYear(UnitOfWork<TrainingISModel> unitOfWork)
        {
            TrainingYearBLO trainingyearBLO = new TrainingYearBLO(unitOfWork);
           
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

        public TrainingYear CreateValideTrainingYearInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
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
        public TrainingYear CreateInValideTrainingYearInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            TrainingYear trainingyear = this.CreateValideTrainingYearInstance(unitOfWork);
             
			// Required   
 
			trainingyear.Code = null;
 
			trainingyear.StartDate = null;
 
			trainingyear.EndtDate = null;
            //Unique
			var existant_TrainingYear = this.CreateOrLouadFirstTrainingYear(new UnitOfWork<TrainingISModel>());
			trainingyear.Code = existant_TrainingYear.Code;
            
            return trainingyear;
        }


		  public TrainingYear CreateInValideTrainingYearInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            TrainingYear trainingyear = this.CreateOrLouadFirstTrainingYear(unitOfWork);
             
			// Required   
 
			trainingyear.Code = null;
 
			trainingyear.StartDate = null;
 
			trainingyear.EndtDate = null;
            //Unique
			var existant_TrainingYear = this.CreateOrLouadFirstTrainingYear(new UnitOfWork<TrainingISModel>());
			trainingyear.Code = existant_TrainingYear.Code;
            
            return trainingyear;
        }
    }
}

