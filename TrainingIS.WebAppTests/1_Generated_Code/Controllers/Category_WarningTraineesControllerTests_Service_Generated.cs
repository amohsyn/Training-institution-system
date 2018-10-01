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
    public class BaseCategory_WarningTraineesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseCategory_WarningTraineesControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first Category_WarningTrainee instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Category_WarningTrainee CreateOrLouadFirstCategory_WarningTrainee(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            Category_WarningTraineeBLO category_warningtraineeBLO = new Category_WarningTraineeBLO(unitOfWork,GAppContext);
           
			Category_WarningTrainee entity = null;
            if (category_warningtraineeBLO.FindAll()?.Count > 0)
                entity = category_warningtraineeBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Category_WarningTrainee for Test
                entity = this.CreateValideCategory_WarningTraineeInstance(unitOfWork,GAppContext);
                category_warningtraineeBLO.Save(entity);
            }
            return entity;
        }

        public virtual Category_WarningTrainee CreateValideCategory_WarningTraineeInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            Category_WarningTrainee  Valide_Category_WarningTrainee = this._Fixture.Create<Category_WarningTrainee>();
            Valide_Category_WarningTrainee.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_Category_WarningTrainee;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Category_WarningTrainee can't exist</returns>
        public virtual Category_WarningTrainee CreateInValideCategory_WarningTraineeInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Category_WarningTrainee category_warningtrainee = this.CreateValideCategory_WarningTraineeInstance(unitOfWork, GAppContext);
             
			// Required   
 
			category_warningtrainee.Name = null;
            //Unique
			var existant_Category_WarningTrainee = this.CreateOrLouadFirstCategory_WarningTrainee(new UnitOfWork<TrainingISModel>(),GAppContext);
			category_warningtrainee.Reference = existant_Category_WarningTrainee.Reference;
 
            return category_warningtrainee;
        }


		public virtual Category_WarningTrainee CreateInValideCategory_WarningTraineeInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Category_WarningTrainee category_warningtrainee = this.CreateOrLouadFirstCategory_WarningTrainee(unitOfWork, GAppContext);
			// Required   
 
			category_warningtrainee.Name = null;
            //Unique
			var existant_Category_WarningTrainee = this.CreateOrLouadFirstCategory_WarningTrainee(new UnitOfWork<TrainingISModel>(), GAppContext);
			category_warningtrainee.Reference = existant_Category_WarningTrainee.Reference;
            return category_warningtrainee;
        }
    }

	public partial class Category_WarningTraineesControllerTests_Service : BaseCategory_WarningTraineesControllerTests_Service{}
}
