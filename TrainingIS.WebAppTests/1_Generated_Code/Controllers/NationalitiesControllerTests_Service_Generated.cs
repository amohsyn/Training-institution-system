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

namespace TrainingIS.WebApp.Tests.Services 
{
    public class BaseNationalitiesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseNationalitiesControllerTests_Service()
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
        public virtual Nationality CreateOrLouadFirstNationality(UnitOfWork<TrainingISModel> unitOfWork)
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

        public virtual Nationality CreateValideNationalityInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
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
        public virtual Nationality CreateInValideNationalityInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            Nationality nationality = this.CreateValideNationalityInstance(unitOfWork);
             
			// Required   
 
			nationality.Code = null;
 
			nationality.Name = null;
            //Unique
			var existant_Nationality = this.CreateOrLouadFirstNationality(new UnitOfWork<TrainingISModel>());
			nationality.Code = existant_Nationality.Code;
 
            return nationality;
        }


		public virtual Nationality CreateInValideNationalityInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            Nationality nationality = this.CreateOrLouadFirstNationality(unitOfWork);
			// Required   
 
			nationality.Code = null;
 
			nationality.Name = null;
            //Unique
			var existant_Nationality = this.CreateOrLouadFirstNationality(new UnitOfWork<TrainingISModel>());
			nationality.Code = existant_Nationality.Code;
            return nationality;
        }
    }

	public partial class NationalitiesControllerTests_Service : BaseNationalitiesControllerTests_Service{}
}
