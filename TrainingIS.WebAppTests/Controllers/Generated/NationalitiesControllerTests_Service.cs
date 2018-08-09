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
using TrainingIS.WebApp.Helpers.AlertMessages;
using GApp.WebApp.Tests;
using GApp.WebApp.Manager.Views;
using TrainingIS.WebApp.Tests.TestUtilities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    public class NationalitiesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public NationalitiesControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first Nationality instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public Nationality CreateOrLouadFirstNationality(UnitOfWork unitOfWork)
        {
            NationalityBLO nationalityBLO = new NationalityBLO(unitOfWork);
           
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

        public Nationality CreateValideNationalityInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
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
        public Nationality CreateInValideNationalityInstance(UnitOfWork unitOfWork = null)
        {
            Nationality nationality = this.CreateValideNationalityInstance(unitOfWork);
             
			// Required   
 
			nationality.Code = null;
 
			nationality.Name = null;
            //Unique
			var existant_Nationality = this.CreateOrLouadFirstNationality(new UnitOfWork());
			nationality.Code = existant_Nationality.Code;
            
            return nationality;
        }


		  public Nationality CreateInValideNationalityInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            Nationality nationality = this.CreateOrLouadFirstNationality(unitOfWork);
             
			// Required   
 
			nationality.Code = null;
 
			nationality.Name = null;
            //Unique
			var existant_Nationality = this.CreateOrLouadFirstNationality(new UnitOfWork());
			nationality.Code = existant_Nationality.Code;
            
            return nationality;
        }
    }
}

