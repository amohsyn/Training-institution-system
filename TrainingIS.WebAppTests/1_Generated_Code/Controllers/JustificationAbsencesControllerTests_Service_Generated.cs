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
    public class BaseJustificationAbsencesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseJustificationAbsencesControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first JustificationAbsence instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual JustificationAbsence CreateOrLouadFirstJustificationAbsence(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            JustificationAbsenceBLO justificationabsenceBLO = new JustificationAbsenceBLO(unitOfWork,GAppContext);
           
			JustificationAbsence entity = null;
            if (justificationabsenceBLO.FindAll()?.Count > 0)
                entity = justificationabsenceBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp JustificationAbsence for Test
                entity = this.CreateValideJustificationAbsenceInstance(unitOfWork,GAppContext);
                justificationabsenceBLO.Save(entity);
            }
            return entity;
        }

        public virtual JustificationAbsence CreateValideJustificationAbsenceInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            JustificationAbsence  Valide_JustificationAbsence = this._Fixture.Create<JustificationAbsence>();
            Valide_JustificationAbsence.Id = 0;
            // Many to One 
            //
			// Category_JustificationAbsence
			var Category_JustificationAbsence = new Category_JustificationAbsencesControllerTests_Service().CreateOrLouadFirstCategory_JustificationAbsence(unitOfWork,GAppContext);
            Valide_JustificationAbsence.Category_JustificationAbsence = null;
            Valide_JustificationAbsence.Category_JustificationAbsenceId = Category_JustificationAbsence.Id;
			// Trainee
			var Trainee = new TraineesControllerTests_Service().CreateOrLouadFirstTrainee(unitOfWork,GAppContext);
            Valide_JustificationAbsence.Trainee = null;
            Valide_JustificationAbsence.TraineeId = Trainee.Id;
            // One to Many
            //
            return Valide_JustificationAbsence;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide JustificationAbsence can't exist</returns>
        public virtual JustificationAbsence CreateInValideJustificationAbsenceInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            JustificationAbsence justificationabsence = this.CreateValideJustificationAbsenceInstance(unitOfWork, GAppContext);
             
			// Required   
 
			justificationabsence.TraineeId = 0;
 
			justificationabsence.Category_JustificationAbsenceId = 0;
 
			justificationabsence.StartDate = DateTime.Now;
 
			justificationabsence.StartTime = DateTime.Now;
 
			justificationabsence.EndtDate = DateTime.Now;
 
			justificationabsence.EndTime = DateTime.Now;
            //Unique
			var existant_JustificationAbsence = this.CreateOrLouadFirstJustificationAbsence(new UnitOfWork<TrainingISModel>(),GAppContext);
			justificationabsence.Reference = existant_JustificationAbsence.Reference;
 
            return justificationabsence;
        }


		public virtual JustificationAbsence CreateInValideJustificationAbsenceInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            JustificationAbsence justificationabsence = this.CreateOrLouadFirstJustificationAbsence(unitOfWork, GAppContext);
			// Required   
 
			justificationabsence.TraineeId = 0;
 
			justificationabsence.Category_JustificationAbsenceId = 0;
 
			justificationabsence.StartDate = DateTime.Now;
 
			justificationabsence.StartTime = DateTime.Now;
 
			justificationabsence.EndtDate = DateTime.Now;
 
			justificationabsence.EndTime = DateTime.Now;
            //Unique
			var existant_JustificationAbsence = this.CreateOrLouadFirstJustificationAbsence(new UnitOfWork<TrainingISModel>(), GAppContext);
			justificationabsence.Reference = existant_JustificationAbsence.Reference;
            return justificationabsence;
        }
    }

	public partial class JustificationAbsencesControllerTests_Service : BaseJustificationAbsencesControllerTests_Service{}
}
