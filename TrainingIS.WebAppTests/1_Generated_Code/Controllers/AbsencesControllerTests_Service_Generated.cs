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
using TrainingIS.Models.Absences;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Tests.Services 
{
    public class BaseAbsencesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseAbsencesControllerTests_Service()
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
        public virtual Absence CreateOrLouadFirstAbsence(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            AbsenceBLO absenceBLO = new AbsenceBLO(unitOfWork,GAppContext);
           
			Absence entity = null;
            if (absenceBLO.FindAll()?.Count > 0)
                entity = absenceBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Absence for Test
                entity = this.CreateValideAbsenceInstance(unitOfWork,GAppContext);
                absenceBLO.Save(entity);
            }
            return entity;
        }

        public virtual Absence CreateValideAbsenceInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            Absence  Valide_Absence = this._Fixture.Create<Absence>();
            Valide_Absence.Id = 0;
            // Many to One 
            //
			// SeanceTraining
			var SeanceTraining = new SeanceTrainingsControllerTests_Service().CreateOrLouadFirstSeanceTraining(unitOfWork,GAppContext);
            Valide_Absence.SeanceTraining = null;
            Valide_Absence.SeanceTrainingId = SeanceTraining.Id;
			// Trainee
			var Trainee = new TraineesControllerTests_Service().CreateOrLouadFirstTrainee(unitOfWork,GAppContext);
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
        public virtual Absence CreateInValideAbsenceInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Absence absence = this.CreateValideAbsenceInstance(unitOfWork, GAppContext);
             
			// Required   
 
			absence.AbsenceDate = DateTime.Now;
 
			absence.SeanceTrainingId = 0;
 
			absence.TraineeId = 0;
 
			absence.isHaveAuthorization = false;
            //Unique
			var existant_Absence = this.CreateOrLouadFirstAbsence(new UnitOfWork<TrainingISModel>(),GAppContext);
			absence.Reference = existant_Absence.Reference;
 
            return absence;
        }


		public virtual Absence CreateInValideAbsenceInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Absence absence = this.CreateOrLouadFirstAbsence(unitOfWork, GAppContext);
			// Required   
 
			absence.AbsenceDate = DateTime.Now;
 
			absence.SeanceTrainingId = 0;
 
			absence.TraineeId = 0;
 
			absence.isHaveAuthorization = false;
            //Unique
			var existant_Absence = this.CreateOrLouadFirstAbsence(new UnitOfWork<TrainingISModel>(), GAppContext);
			absence.Reference = existant_Absence.Reference;
            return absence;
        }
    }

	public partial class AbsencesControllerTests_Service : BaseAbsencesControllerTests_Service{}
}
