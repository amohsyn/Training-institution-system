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
    public class BaseCategory_JustificationAbsencesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseCategory_JustificationAbsencesControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first Category_JustificationAbsence instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Category_JustificationAbsence CreateOrLouadFirstCategory_JustificationAbsence(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            Category_JustificationAbsenceBLO category_justificationabsenceBLO = new Category_JustificationAbsenceBLO(unitOfWork,GAppContext);
           
			Category_JustificationAbsence entity = null;
            if (category_justificationabsenceBLO.FindAll()?.Count > 0)
                entity = category_justificationabsenceBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Category_JustificationAbsence for Test
                entity = this.CreateValideCategory_JustificationAbsenceInstance(unitOfWork,GAppContext);
                category_justificationabsenceBLO.Save(entity);
            }
            return entity;
        }

        public virtual Category_JustificationAbsence CreateValideCategory_JustificationAbsenceInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            Category_JustificationAbsence  Valide_Category_JustificationAbsence = this._Fixture.Create<Category_JustificationAbsence>();
            Valide_Category_JustificationAbsence.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_Category_JustificationAbsence;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Category_JustificationAbsence can't exist</returns>
        public virtual Category_JustificationAbsence CreateInValideCategory_JustificationAbsenceInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Category_JustificationAbsence category_justificationabsence = this.CreateValideCategory_JustificationAbsenceInstance(unitOfWork, GAppContext);
             
			// Required   
 
			category_justificationabsence.Name = null;
            //Unique
			var existant_Category_JustificationAbsence = this.CreateOrLouadFirstCategory_JustificationAbsence(new UnitOfWork<TrainingISModel>(),GAppContext);
			category_justificationabsence.Reference = existant_Category_JustificationAbsence.Reference;
 
            return category_justificationabsence;
        }


		public virtual Category_JustificationAbsence CreateInValideCategory_JustificationAbsenceInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Category_JustificationAbsence category_justificationabsence = this.CreateOrLouadFirstCategory_JustificationAbsence(unitOfWork, GAppContext);
			// Required   
 
			category_justificationabsence.Name = null;
            //Unique
			var existant_Category_JustificationAbsence = this.CreateOrLouadFirstCategory_JustificationAbsence(new UnitOfWork<TrainingISModel>(), GAppContext);
			category_justificationabsence.Reference = existant_Category_JustificationAbsence.Reference;
            return category_justificationabsence;
        }
    }

	public partial class Category_JustificationAbsencesControllerTests_Service : BaseCategory_JustificationAbsencesControllerTests_Service{}
}
