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
    public class BaseWarningTraineesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseWarningTraineesControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first WarningTrainee instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual WarningTrainee CreateOrLouadFirstWarningTrainee(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            WarningTraineeBLO warningtraineeBLO = new WarningTraineeBLO(unitOfWork,GAppContext);
           
			WarningTrainee entity = null;
            if (warningtraineeBLO.FindAll()?.Count > 0)
                entity = warningtraineeBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp WarningTrainee for Test
                entity = this.CreateValideWarningTraineeInstance(unitOfWork,GAppContext);
                warningtraineeBLO.Save(entity);
            }
            return entity;
        }

        public virtual WarningTrainee CreateValideWarningTraineeInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            WarningTrainee  Valide_WarningTrainee = this._Fixture.Create<WarningTrainee>();
            Valide_WarningTrainee.Id = 0;
            // Many to One 
            //
			// Category_WarningTrainee
			var Category_WarningTrainee = new Category_WarningTraineesControllerTests_Service().CreateOrLouadFirstCategory_WarningTrainee(unitOfWork,GAppContext);
            Valide_WarningTrainee.Category_WarningTrainee = null;
            Valide_WarningTrainee.Category_WarningTraineeId = Category_WarningTrainee.Id;
			// Trainee
			var Trainee = new TraineesControllerTests_Service().CreateOrLouadFirstTrainee(unitOfWork,GAppContext);
            Valide_WarningTrainee.Trainee = null;
            Valide_WarningTrainee.TraineeId = Trainee.Id;
            // One to Many
            //
            return Valide_WarningTrainee;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide WarningTrainee can't exist</returns>
        public virtual WarningTrainee CreateInValideWarningTraineeInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            WarningTrainee warningtrainee = this.CreateValideWarningTraineeInstance(unitOfWork, GAppContext);
             
			// Required   
 
			warningtrainee.TraineeId = 0;
 
			warningtrainee.WarningDate = DateTime.Now;
 
			warningtrainee.Category_WarningTraineeId = 0;
            //Unique
			var existant_WarningTrainee = this.CreateOrLouadFirstWarningTrainee(new UnitOfWork<TrainingISModel>(),GAppContext);
			warningtrainee.Reference = existant_WarningTrainee.Reference;
 
            return warningtrainee;
        }


		public virtual WarningTrainee CreateInValideWarningTraineeInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            WarningTrainee warningtrainee = this.CreateOrLouadFirstWarningTrainee(unitOfWork, GAppContext);
			// Required   
 
			warningtrainee.TraineeId = 0;
 
			warningtrainee.WarningDate = DateTime.Now;
 
			warningtrainee.Category_WarningTraineeId = 0;
            //Unique
			var existant_WarningTrainee = this.CreateOrLouadFirstWarningTrainee(new UnitOfWork<TrainingISModel>(), GAppContext);
			warningtrainee.Reference = existant_WarningTrainee.Reference;
            return warningtrainee;
        }
    }

	public partial class WarningTraineesControllerTests_Service : BaseWarningTraineesControllerTests_Service{}
}
