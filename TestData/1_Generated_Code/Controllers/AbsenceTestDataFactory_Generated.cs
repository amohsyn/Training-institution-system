using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using AutoFixture;
using TrainingIS.BLL;
using TrainingIS.DAL;
using System.ComponentModel.DataAnnotations;
using GApp.WebApp.Manager.Views;
using GApp.DAL;
using GApp.Entities;
using GApp.Core.Context;
using TrainingIS.Models.Absences;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;
using GApp.UnitTest.TestData;
using GApp.UnitTest.TestData.Enums;

namespace TestData
{
    public class BaseAbsenceTestDataFactory : ITestDataFactory<Absence>
    {
        private Fixture _Fixture = null;
		protected List<Absence> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseAbsenceTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<Absence> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<Absence> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first Absence instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Absence CreateOrLouadFirstAbsence()
        {
            AbsenceBLO absenceBLO = new AbsenceBLO(UnitOfWork,GAppContext);
           
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

        public virtual Absence CreateValideAbsenceInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Absence  Valide_Absence = this._Fixture.Create<Absence>();
            Valide_Absence.Id = 0;
            // Many to One 
            //
			// JustificationAbsence
			var JustificationAbsence = new JustificationAbsenceTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstJustificationAbsence();
            Valide_Absence.JustificationAbsence = null;
            //Valide_Absence.JustificationAbsenceId = JustificationAbsence.Id;
			// SeanceTraining
			var SeanceTraining = new SeanceTrainingTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSeanceTraining();
            Valide_Absence.SeanceTraining = null;
            Valide_Absence.SeanceTrainingId = SeanceTraining.Id;
			// Trainee
			var Trainee = new TraineeTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainee();
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
        public virtual Absence CreateInValideAbsenceInstance()
        {
            Absence absence = this.CreateValideAbsenceInstance();
             
			// Required   
 
			absence.AbsenceDate = DateTime.Now;
 
			absence.SeanceTrainingId = 0;
 
			absence.TraineeId = 0;
 
			absence.isHaveAuthorization = false;
            //Unique
			var existant_Absence = this.CreateOrLouadFirstAbsence();
			absence.Reference = existant_Absence.Reference;
 
            return absence;
        }


		public virtual Absence CreateInValideAbsenceInstance_ForEdit()
        {
            Absence absence = this.CreateOrLouadFirstAbsence();
			// Required   
 
			absence.AbsenceDate = DateTime.Now;
 
			absence.SeanceTrainingId = 0;
 
			absence.TraineeId = 0;
 
			absence.isHaveAuthorization = false;
            //Unique
			var existant_Absence = this.CreateOrLouadFirstAbsence();
			absence.Reference = existant_Absence.Reference;
            return absence;
        }
    }

	public partial class AbsenceTestDataFactory : BaseAbsenceTestDataFactory{
	
		public AbsenceTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
