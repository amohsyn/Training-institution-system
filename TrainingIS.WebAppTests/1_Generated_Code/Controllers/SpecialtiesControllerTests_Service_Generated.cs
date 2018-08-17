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
    public class BaseSpecialtiesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseSpecialtiesControllerTests_Service()
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
        public virtual Specialty CreateOrLouadFirstSpecialty(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            SpecialtyBLO specialtyBLO = new SpecialtyBLO(unitOfWork,GAppContext);
           
			Specialty entity = null;
            if (specialtyBLO.FindAll()?.Count > 0)
                entity = specialtyBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Specialty for Test
                entity = this.CreateValideSpecialtyInstance(unitOfWork,GAppContext);
                specialtyBLO.Save(entity);
            }
            return entity;
        }

        public virtual Specialty CreateValideSpecialtyInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            Specialty  Valide_Specialty = this._Fixture.Create<Specialty>();
            Valide_Specialty.Id = 0;
            // Many to One 
            //
			// TrainingLevel
			var TrainingLevel = new TrainingLevelsControllerTests_Service().CreateOrLouadFirstTrainingLevel(unitOfWork,GAppContext);
            Valide_Specialty.TrainingLevel = null;
            Valide_Specialty.TrainingLevelId = TrainingLevel.Id;
            // One to Many
            //
            return Valide_Specialty;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Specialty can't exist</returns>
        public virtual Specialty CreateInValideSpecialtyInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Specialty specialty = this.CreateValideSpecialtyInstance(unitOfWork, GAppContext);
             
			// Required   
 
			specialty.TrainingLevelId = 0;
 
			specialty.Code = null;
 
			specialty.Name = null;
            //Unique
			var existant_Specialty = this.CreateOrLouadFirstSpecialty(new UnitOfWork<TrainingISModel>(),GAppContext);
			specialty.Code = existant_Specialty.Code;
 
            return specialty;
        }


		public virtual Specialty CreateInValideSpecialtyInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Specialty specialty = this.CreateOrLouadFirstSpecialty(unitOfWork, GAppContext);
			// Required   
 
			specialty.TrainingLevelId = 0;
 
			specialty.Code = null;
 
			specialty.Name = null;
            //Unique
			var existant_Specialty = this.CreateOrLouadFirstSpecialty(new UnitOfWork<TrainingISModel>(), GAppContext);
			specialty.Code = existant_Specialty.Code;
            return specialty;
        }
    }

	public partial class SpecialtiesControllerTests_Service : BaseSpecialtiesControllerTests_Service{}
}
