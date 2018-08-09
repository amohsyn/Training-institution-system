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
using TrainingIS.Entities.ModelsViews.FormerModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    public class FormersControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public FormersControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first Former instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public Former CreateOrLouadFirstFormer(UnitOfWork unitOfWork)
        {
            FormerBLO formerBLO = new FormerBLO(unitOfWork);
           
		   Former entity = null;
            if (formerBLO.FindAll()?.Count > 0)
                entity = formerBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp Former for Test
                entity = this.CreateValideFormerInstance();
                formerBLO.Save(entity);
            }
            return entity;
        }

        public Former CreateValideFormerInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            Former  Valide_Former = this._Fixture.Create<Former>();
            Valide_Former.Id = 0;
            // Many to One 
            //
			// Nationality
			var Nationality = new NationalitiesControllerTests_Service().CreateOrLouadFirstNationality(unitOfWork);
            Valide_Former.Nationality = null;
            Valide_Former.NationalityId = Nationality.Id;
            // One to Many
            //
            return Valide_Former;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Former can't exist</returns>
        public Former CreateInValideFormerInstance(UnitOfWork unitOfWork = null)
        {
            Former former = this.CreateValideFormerInstance(unitOfWork);
             
			// Required   
 
			former.RegistrationNumber = null;
 
			former.Login = null;
 
			former.Password = null;
 
			former.FirstName = null;
 
			former.LastName = null;
 
			former.FirstNameArabe = null;
 
			former.LastNameArabe = null;
 
			former.Sex = SexEnum.man;
 
			former.Birthdate = DateTime.Now;
 
			former.NationalityId = 0;
 
			former.BirthPlace = null;
 
			former.CIN = null;
 
			former.Email = null;
            //Unique
			var existant_Former = this.CreateOrLouadFirstFormer(new UnitOfWork());
			former.RegistrationNumber = existant_Former.RegistrationNumber;
			former.CIN = existant_Former.CIN;
            
            return former;
        }


		  public Former CreateInValideFormerInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            Former former = this.CreateOrLouadFirstFormer(unitOfWork);
             
			// Required   
 
			former.RegistrationNumber = null;
 
			former.Login = null;
 
			former.Password = null;
 
			former.FirstName = null;
 
			former.LastName = null;
 
			former.FirstNameArabe = null;
 
			former.LastNameArabe = null;
 
			former.Sex = SexEnum.man;
 
			former.Birthdate = DateTime.Now;
 
			former.NationalityId = 0;
 
			former.BirthPlace = null;
 
			former.CIN = null;
 
			former.Email = null;
            //Unique
			var existant_Former = this.CreateOrLouadFirstFormer(new UnitOfWork());
			former.RegistrationNumber = existant_Former.RegistrationNumber;
			former.CIN = existant_Former.CIN;
            
            return former;
        }
    }
}

