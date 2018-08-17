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
using TrainingIS.Entities.ModelsViews.FormerModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Tests.Services 
{
    public class BaseFormersControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseFormersControllerTests_Service()
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
        public virtual Former CreateOrLouadFirstFormer(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            FormerBLO formerBLO = new FormerBLO(unitOfWork,GAppContext);
           
			Former entity = null;
            if (formerBLO.FindAll()?.Count > 0)
                entity = formerBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Former for Test
                entity = this.CreateValideFormerInstance(unitOfWork,GAppContext);
                formerBLO.Save(entity);
            }
            return entity;
        }

        public virtual Former CreateValideFormerInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            Former  Valide_Former = this._Fixture.Create<Former>();
            Valide_Former.Id = 0;
            // Many to One 
            //
			// FormerSpecialty
			var FormerSpecialty = new FormerSpecialtiesControllerTests_Service().CreateOrLouadFirstFormerSpecialty(unitOfWork,GAppContext);
            Valide_Former.FormerSpecialty = null;
            Valide_Former.FormerSpecialtyId = FormerSpecialty.Id;
			// Nationality
			var Nationality = new NationalitiesControllerTests_Service().CreateOrLouadFirstNationality(unitOfWork,GAppContext);
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
        public virtual Former CreateInValideFormerInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Former former = this.CreateValideFormerInstance(unitOfWork, GAppContext);
             
			// Required   
 
			former.RegistrationNumber = null;
 
			former.FormerSpecialtyId = 0;
 
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
			var existant_Former = this.CreateOrLouadFirstFormer(new UnitOfWork<TrainingISModel>(),GAppContext);
			former.RegistrationNumber = existant_Former.RegistrationNumber;
			former.CIN = existant_Former.CIN;
 
            return former;
        }


		public virtual Former CreateInValideFormerInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Former former = this.CreateOrLouadFirstFormer(unitOfWork, GAppContext);
			// Required   
 
			former.RegistrationNumber = null;
 
			former.FormerSpecialtyId = 0;
 
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
			var existant_Former = this.CreateOrLouadFirstFormer(new UnitOfWork<TrainingISModel>(), GAppContext);
			former.RegistrationNumber = existant_Former.RegistrationNumber;
			former.CIN = existant_Former.CIN;
            return former;
        }
    }

	public partial class FormersControllerTests_Service : BaseFormersControllerTests_Service{}
}
