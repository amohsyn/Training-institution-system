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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;
using GApp.UnitTest.TestData;
using GApp.UnitTest.TestData.Enums;

namespace TestData
{
    public class BaseJustificationAbsenceTestDataFactory : EntityTestData<JustificationAbsence>
    {
        public BaseJustificationAbsenceTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<JustificationAbsence> Generate_TestData()
        {
            List<JustificationAbsence> Data = base.Generate_TestData();
            if(Data == null) Data = new List<JustificationAbsence>();
            Data.Add(this.CreateValideJustificationAbsenceInstance());
            return Data;
        }
	
		/// <summary>
        /// Find the first JustificationAbsence instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual JustificationAbsence CreateOrLouadFirstJustificationAbsence()
        {
            JustificationAbsenceBLO justificationabsenceBLO = new JustificationAbsenceBLO(UnitOfWork,GAppContext);
           
			JustificationAbsence entity = null;
            if (justificationabsenceBLO.FindAll()?.Count > 0)
                entity = justificationabsenceBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp JustificationAbsence for Test
                entity = this.CreateValideJustificationAbsenceInstance();
                justificationabsenceBLO.Save(entity);
            }
            return entity;
        }

        public virtual JustificationAbsence CreateValideJustificationAbsenceInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            JustificationAbsence  Valide_JustificationAbsence = this._Fixture.Create<JustificationAbsence>();
            Valide_JustificationAbsence.Id = 0;
            // Many to One 
            //  
            // One to Many
            //
            return Valide_JustificationAbsence;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide JustificationAbsence can't exist</returns>
        public virtual JustificationAbsence CreateInValideJustificationAbsenceInstance()
        {
            JustificationAbsence justificationabsence = this.CreateValideJustificationAbsenceInstance();
             
			// Required   
 
			justificationabsence.TraineeId = 0;
 
			justificationabsence.Category_JustificationAbsenceId = 0;
 
			justificationabsence.StartDate = DateTime.Now;
 
			justificationabsence.EndtDate = DateTime.Now;
            //Unique
			var existant_JustificationAbsence = this.CreateOrLouadFirstJustificationAbsence();
			justificationabsence.Reference = existant_JustificationAbsence.Reference;
 
            return justificationabsence;
        }


		public virtual JustificationAbsence CreateInValideJustificationAbsenceInstance_ForEdit()
        {
            JustificationAbsence justificationabsence = this.CreateOrLouadFirstJustificationAbsence();
			// Required   
 
			justificationabsence.TraineeId = 0;
 
			justificationabsence.Category_JustificationAbsenceId = 0;
 
			justificationabsence.StartDate = DateTime.Now;
 
			justificationabsence.EndtDate = DateTime.Now;
            //Unique
			var existant_JustificationAbsence = this.CreateOrLouadFirstJustificationAbsence();
			justificationabsence.Reference = existant_JustificationAbsence.Reference;
            return justificationabsence;
        }
    }

	public partial class JustificationAbsenceTestDataFactory : BaseJustificationAbsenceTestDataFactory{
	
		public JustificationAbsenceTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
