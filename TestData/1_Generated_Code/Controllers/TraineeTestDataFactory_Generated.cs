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
using TrainingIS.Models.Trainees;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;
using GApp.UnitTest.TestData;
using GApp.UnitTest.TestData.Enums;

namespace TestData
{
    public class BaseTraineeTestDataFactory : EntityTestData<Trainee>
    {
        public BaseTraineeTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Trainee> Generate_TestData()
        {
            List<Trainee> Data = base.Generate_TestData();
            if(Data == null) Data = new List<Trainee>();
            Data.Add(this.CreateValideTraineeInstance());
            return Data;
        }
	
		/// <summary>
        /// Find the first Trainee instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Trainee CreateOrLouadFirstTrainee()
        {
            TraineeBLO traineeBLO = new TraineeBLO(UnitOfWork,GAppContext);
           
			Trainee entity = null;
            if (traineeBLO.FindAll()?.Count > 0)
                entity = traineeBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Trainee for Test
                entity = this.CreateValideTraineeInstance();
                traineeBLO.Save(entity);
            }
            return entity;
        }

        public virtual Trainee CreateValideTraineeInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Trainee  Valide_Trainee = this._Fixture.Create<Trainee>();
            Valide_Trainee.Id = 0;
            // Many to One 
            //   
			// Schoollevel
			var Schoollevel = new SchoollevelTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSchoollevel();
            Valide_Trainee.Schoollevel = Schoollevel;
						 Valide_Trainee.SchoollevelId = Schoollevel.Id;
			           
			// Specialty
			var Specialty = new SpecialtyTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSpecialty();
            Valide_Trainee.Specialty = Specialty;
						 Valide_Trainee.SpecialtyId = Specialty.Id;
			           
			// YearStudy
			var YearStudy = new YearStudyTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstYearStudy();
            Valide_Trainee.YearStudy = YearStudy;
						 Valide_Trainee.YearStudyId = YearStudy.Id;
			           
			// Group
			var Group = new GroupTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstGroup();
            Valide_Trainee.Group = Group;
						 Valide_Trainee.GroupId = Group.Id;
			           
			// Nationality
			var Nationality = new NationalityTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstNationality();
            Valide_Trainee.Nationality = Nationality;
						 Valide_Trainee.NationalityId = Nationality.Id;
			           
			// Photo
			var Photo = new GPictureTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstGPicture();
            Valide_Trainee.Photo = Photo;
			           
            // One to Many
            //
			Valide_Trainee.Member_To_WorkGroups = null;
			Valide_Trainee.StateOfAbseces = null;
            return Valide_Trainee;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Trainee can't exist</returns>
        public virtual Trainee CreateInValideTraineeInstance()
        {
            Trainee trainee = this.CreateValideTraineeInstance();
             
			// Required   
 
			trainee.CNE = null;
 
			trainee.GroupId = 0;
 
			trainee.FirstName = null;
 
			trainee.LastName = null;
 
			trainee.Sex = SexEnum.man;
            //Unique
			var existant_Trainee = this.CreateOrLouadFirstTrainee();
			trainee.CNE = existant_Trainee.CNE;
			trainee.CIN = existant_Trainee.CIN;
			trainee.Email = existant_Trainee.Email;
			trainee.Reference = existant_Trainee.Reference;
 
            return trainee;
        }


		public virtual Trainee CreateInValideTraineeInstance_ForEdit()
        {
            Trainee trainee = this.CreateOrLouadFirstTrainee();
			// Required   
 
			trainee.CNE = null;
 
			trainee.GroupId = 0;
 
			trainee.FirstName = null;
 
			trainee.LastName = null;
 
			trainee.Sex = SexEnum.man;
            //Unique
			var existant_Trainee = this.CreateOrLouadFirstTrainee();
			trainee.CNE = existant_Trainee.CNE;
			trainee.CIN = existant_Trainee.CIN;
			trainee.Email = existant_Trainee.Email;
			trainee.Reference = existant_Trainee.Reference;
            return trainee;
        }
    }

	public partial class TraineeTestDataFactory : BaseTraineeTestDataFactory{
	
		public TraineeTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
