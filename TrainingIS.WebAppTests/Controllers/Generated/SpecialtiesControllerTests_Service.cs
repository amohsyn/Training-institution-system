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
    public class SpecialtiesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public SpecialtiesControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first Specialty instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public Specialty CreateOrLouadFirstSpecialty(UnitOfWork unitOfWork)
        {
            SpecialtyBLO specialtyBLO = new SpecialtyBLO(unitOfWork);
           
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

        public Specialty CreateValideSpecialtyInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            Specialty  Valide_Specialty = this._Fixture.Create<Specialty>();
            Valide_Specialty.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_Specialty;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Specialty can't exist</returns>
        public Specialty CreateInValideSpecialtyInstance(UnitOfWork unitOfWork = null)
        {
            Specialty specialty = this.CreateValideSpecialtyInstance(unitOfWork);
             
			// Required   
 
			specialty.Code = null;
 
			specialty.Name = null;
            //Unique
			var existant_Specialty = this.CreateOrLouadFirstSpecialty(new UnitOfWork());
			specialty.Code = existant_Specialty.Code;
            
            return specialty;
        }


		  public Specialty CreateInValideSpecialtyInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            Specialty specialty = this.CreateOrLouadFirstSpecialty(unitOfWork);
             
			// Required   
 
			specialty.Code = null;
 
			specialty.Name = null;
            //Unique
			var existant_Specialty = this.CreateOrLouadFirstSpecialty(new UnitOfWork());
			specialty.Code = existant_Specialty.Code;
            
            return specialty;
        }
    }
}

