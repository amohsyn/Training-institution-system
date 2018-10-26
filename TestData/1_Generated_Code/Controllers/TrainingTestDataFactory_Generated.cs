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
    public class BaseTrainingTestDataFactory : EntityTestData<Training>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new TrainingBLO(UnitOfWork, GAppContext);
        }

        public BaseTrainingTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Training> Generate_TestData()
        {
            List<Training> Data = base.Generate_TestData();
            if(Data == null) Data = new List<Training>();
			Training Training = this.CreateValideTrainingInstance();
            Training.Reference = "ValideTrainingInstance";
            Data.Add(Training);
            return Data;
        }
	
		/// <summary>
        /// Find the first Training instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Training CreateOrLouadFirstTraining()
        {
            TrainingBLO trainingBLO = new TrainingBLO(UnitOfWork,GAppContext);
           
			Training entity = null;
            if (trainingBLO.FindAll()?.Count > 0)
                entity = trainingBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Training for Test
                entity = this.CreateValideTrainingInstance();
                trainingBLO.Save(entity);
            }
            return entity;
        }

        public virtual Training CreateValideTrainingInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Training  Valide_Training = this._Fixture.Create<Training>();
            Valide_Training.Id = 0;
            // Many to One 
            //   
			// TrainingYear
			var TrainingYear = new TrainingYearTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainingYear();
            Valide_Training.TrainingYear = TrainingYear;
						 Valide_Training.TrainingYearId = TrainingYear.Id;
			           
			// ModuleTraining
			var ModuleTraining = new ModuleTrainingTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstModuleTraining();
            Valide_Training.ModuleTraining = ModuleTraining;
						 Valide_Training.ModuleTrainingId = ModuleTraining.Id;
			           
			// Former
			var Former = new FormerTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstFormer();
            Valide_Training.Former = Former;
						 Valide_Training.FormerId = Former.Id;
			           
			// Group
			var Group = new GroupTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstGroup();
            Valide_Training.Group = Group;
						 Valide_Training.GroupId = Group.Id;
			           
            // One to Many
            //
            return Valide_Training;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Training can't exist</returns>
        public virtual Training CreateInValideTrainingInstance()
        {
            Training training = this.CreateValideTrainingInstance();
             
			// Required   
 
			training.TrainingYearId = 0;
 
			training.ModuleTrainingId = 0;
 
			training.FormerId = 0;
 
			training.GroupId = 0;
            //Unique
			var existant_Training = this.CreateOrLouadFirstTraining();
			training.Reference = existant_Training.Reference;
 
            return training;
        }


		public virtual Training CreateInValideTrainingInstance_ForEdit()
        {
            Training training = this.CreateOrLouadFirstTraining();
			// Required   
 
			training.TrainingYearId = 0;
 
			training.ModuleTrainingId = 0;
 
			training.FormerId = 0;
 
			training.GroupId = 0;
            //Unique
			var existant_Training = this.CreateOrLouadFirstTraining();
			training.Reference = existant_Training.Reference;
            return training;
        }
    }

	public partial class TrainingTestDataFactory : BaseTrainingTestDataFactory{
	
		public TrainingTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
