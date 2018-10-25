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
    public class BaseAbsenceTestDataFactory : EntityTestData<Absence>
    {
        public BaseAbsenceTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Absence> Generate_TestData()
        {
            List<Absence> Data = base.Generate_TestData();
            if(Data == null) Data = new List<Absence>();
            Data.Add(this.CreateValideAbsenceInstance());
            return Data;
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
            // One to Many
            //
			Valide_Absence.Sanctions = null;
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
