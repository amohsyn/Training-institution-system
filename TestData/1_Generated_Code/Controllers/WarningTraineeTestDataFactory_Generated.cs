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
    public class BaseWarningTraineeTestDataFactory : EntityTestData<WarningTrainee>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new WarningTraineeBLO(UnitOfWork, GAppContext);
        }

        public BaseWarningTraineeTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<WarningTrainee> Generate_TestData()
        {
            List<WarningTrainee> Data = base.Generate_TestData();
            if(Data == null) Data = new List<WarningTrainee>();
			WarningTrainee WarningTrainee = this.CreateValideWarningTraineeInstance();
            WarningTrainee.Reference = "ValideWarningTraineeInstance";
            Data.Add(WarningTrainee);
            return Data;
        }
	
		/// <summary>
        /// Find the first WarningTrainee instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual WarningTrainee CreateOrLouadFirstWarningTrainee()
        {
            WarningTraineeBLO warningtraineeBLO = new WarningTraineeBLO(UnitOfWork,GAppContext);
           
			WarningTrainee entity = null;
            if (warningtraineeBLO.FindAll()?.Count > 0)
                entity = warningtraineeBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp WarningTrainee for Test
                entity = this.CreateValideWarningTraineeInstance();
                warningtraineeBLO.Save(entity);
            }
            return entity;
        }

        public virtual WarningTrainee CreateValideWarningTraineeInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            WarningTrainee  Valide_WarningTrainee = this._Fixture.Create<WarningTrainee>();
            Valide_WarningTrainee.Id = 0;
            // Many to One 
            //   
			// Trainee
			var Trainee = new TraineeTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainee();
            Valide_WarningTrainee.Trainee = Trainee;
						 Valide_WarningTrainee.TraineeId = Trainee.Id;
			           
			// Category_WarningTrainee
			var Category_WarningTrainee = new Category_WarningTraineeTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstCategory_WarningTrainee();
            Valide_WarningTrainee.Category_WarningTrainee = Category_WarningTrainee;
						 Valide_WarningTrainee.Category_WarningTraineeId = Category_WarningTrainee.Id;
			           
            // One to Many
            //
            return Valide_WarningTrainee;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide WarningTrainee can't exist</returns>
        public virtual WarningTrainee CreateInValideWarningTraineeInstance()
        {
            WarningTrainee warningtrainee = this.CreateValideWarningTraineeInstance();
             
			// Required   
 
			warningtrainee.TraineeId = 0;
 
			warningtrainee.WarningDate = DateTime.Now;
 
			warningtrainee.Category_WarningTraineeId = 0;
            //Unique
			var existant_WarningTrainee = this.CreateOrLouadFirstWarningTrainee();
			warningtrainee.Reference = existant_WarningTrainee.Reference;
 
            return warningtrainee;
        }


		public virtual WarningTrainee CreateInValideWarningTraineeInstance_ForEdit()
        {
            WarningTrainee warningtrainee = this.CreateOrLouadFirstWarningTrainee();
			// Required   
 
			warningtrainee.TraineeId = 0;
 
			warningtrainee.WarningDate = DateTime.Now;
 
			warningtrainee.Category_WarningTraineeId = 0;
            //Unique
			var existant_WarningTrainee = this.CreateOrLouadFirstWarningTrainee();
			warningtrainee.Reference = existant_WarningTrainee.Reference;
            return warningtrainee;
        }
    }

	public partial class WarningTraineeTestDataFactory : BaseWarningTraineeTestDataFactory{
	
		public WarningTraineeTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
