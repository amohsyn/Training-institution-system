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
    public class SeanceDaysControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public SeanceDaysControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first SeanceDay instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public SeanceDay CreateOrLouadFirstSeanceDay(UnitOfWork<TrainingISModel> unitOfWork)
        {
            SeanceDayBLO seancedayBLO = new SeanceDayBLO(unitOfWork);
           
		   SeanceDay entity = null;
            if (seancedayBLO.FindAll()?.Count > 0)
                entity = seancedayBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp SeanceDay for Test
                entity = this.CreateValideSeanceDayInstance();
                seancedayBLO.Save(entity);
            }
            return entity;
        }

        public SeanceDay CreateValideSeanceDayInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            SeanceDay  Valide_SeanceDay = this._Fixture.Create<SeanceDay>();
            Valide_SeanceDay.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_SeanceDay;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide SeanceDay can't exist</returns>
        public SeanceDay CreateInValideSeanceDayInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            SeanceDay seanceday = this.CreateValideSeanceDayInstance(unitOfWork);
             
			// Required   
 
			seanceday.Name = null;
 
			seanceday.Code = null;
            //Unique
			var existant_SeanceDay = this.CreateOrLouadFirstSeanceDay(new UnitOfWork<TrainingISModel>());
			seanceday.Code = existant_SeanceDay.Code;
            
            return seanceday;
        }


		  public SeanceDay CreateInValideSeanceDayInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            SeanceDay seanceday = this.CreateOrLouadFirstSeanceDay(unitOfWork);
             
			// Required   
 
			seanceday.Name = null;
 
			seanceday.Code = null;
            //Unique
			var existant_SeanceDay = this.CreateOrLouadFirstSeanceDay(new UnitOfWork<TrainingISModel>());
			seanceday.Code = existant_SeanceDay.Code;
            
            return seanceday;
        }
    }
}

