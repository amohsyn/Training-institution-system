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

namespace TrainingIS.WebApp.Controllers.Tests
{
    public class AbsencesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public AbsencesControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first Absence instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public Absence CreateOrLouadFirstAbsence(UnitOfWork<TrainingISModel> unitOfWork)
        {
            AbsenceBLO absenceBLO = new AbsenceBLO(unitOfWork);
           
		   Absence entity = null;
            if (absenceBLO.FindAll()?.Count > 0)
                entity = absenceBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp Absence for Test
                entity = this.CreateValideAbsenceInstance();
                absenceBLO.Save(entity);
            }
            return entity;
        }

        public Absence CreateValideAbsenceInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            Absence  Valide_Absence = this._Fixture.Create<Absence>();
            Valide_Absence.Id = 0;
            // Many to One 
            //
			// SeanceTraining
			var SeanceTraining = new SeanceTrainingsControllerTests_Service().CreateOrLouadFirstSeanceTraining(unitOfWork);
            Valide_Absence.SeanceTraining = null;
            Valide_Absence.SeanceTrainingId = SeanceTraining.Id;
			// Trainee
			var Trainee = new TraineesControllerTests_Service().CreateOrLouadFirstTrainee(unitOfWork);
            Valide_Absence.Trainee = null;
            Valide_Absence.TraineeId = Trainee.Id;
            // One to Many
            //
            return Valide_Absence;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Absence can't exist</returns>
        public Absence CreateInValideAbsenceInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            Absence absence = this.CreateValideAbsenceInstance(unitOfWork);
             
			// Required   
 
			absence.TraineeId = 0;
 
			absence.isHaveAuthorization = false;
 
			absence.SeanceTrainingId = 0;
            //Unique
			var existant_Absence = this.CreateOrLouadFirstAbsence(new UnitOfWork<TrainingISModel>());
            
            return absence;
        }


		  public Absence CreateInValideAbsenceInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            Absence absence = this.CreateOrLouadFirstAbsence(unitOfWork);
             
			// Required   
 
			absence.TraineeId = 0;
 
			absence.isHaveAuthorization = false;
 
			absence.SeanceTrainingId = 0;
            //Unique
			var existant_Absence = this.CreateOrLouadFirstAbsence(new UnitOfWork<TrainingISModel>());
            
            return absence;
        }
    }
}

