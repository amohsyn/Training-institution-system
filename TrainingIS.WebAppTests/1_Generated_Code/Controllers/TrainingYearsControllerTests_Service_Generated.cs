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
    public class BaseTrainingYearsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseTrainingYearsControllerTests_Service()
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
        public virtual TrainingYear CreateOrLouadFirstTrainingYear(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            TrainingYearBLO trainingyearBLO = new TrainingYearBLO(unitOfWork,GAppContext);
           
			TrainingYear entity = null;
            if (trainingyearBLO.FindAll()?.Count > 0)
                entity = trainingyearBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp TrainingYear for Test
                entity = this.CreateValideTrainingYearInstance(unitOfWork,GAppContext);
                trainingyearBLO.Save(entity);
            }
            return entity;
        }

        public virtual TrainingYear CreateValideTrainingYearInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
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
        public virtual TrainingYear CreateInValideTrainingYearInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            TrainingYear trainingyear = this.CreateValideTrainingYearInstance(unitOfWork, GAppContext);
             
			// Required   
 
			trainingyear.Code = null;
 
			trainingyear.StartDate = null;
 
			trainingyear.EndtDate = null;
            //Unique
			var existant_TrainingYear = this.CreateOrLouadFirstTrainingYear(new UnitOfWork<TrainingISModel>(),GAppContext);
			trainingyear.Code = existant_TrainingYear.Code;
 
            return trainingyear;
        }


		public virtual TrainingYear CreateInValideTrainingYearInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            TrainingYear trainingyear = this.CreateOrLouadFirstTrainingYear(unitOfWork, GAppContext);
			// Required   
 
			trainingyear.Code = null;
 
			trainingyear.StartDate = null;
 
			trainingyear.EndtDate = null;
            //Unique
			var existant_TrainingYear = this.CreateOrLouadFirstTrainingYear(new UnitOfWork<TrainingISModel>(), GAppContext);
			trainingyear.Code = existant_TrainingYear.Code;
            return trainingyear;
        }
    }

	public partial class TrainingYearsControllerTests_Service : BaseTrainingYearsControllerTests_Service{}
}
