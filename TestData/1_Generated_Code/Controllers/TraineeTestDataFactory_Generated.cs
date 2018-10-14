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
    public class BaseTraineeTestDataFactory : ITestDataFactory<Trainee>
    {
        private Fixture _Fixture = null;
		protected List<Trainee> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseTraineeTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<Trainee> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<Trainee> Generate()
        {
            return null;
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
			// Group
			var Group = new GroupTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstGroup();
            Valide_Trainee.Group = null;
            Valide_Trainee.GroupId = Group.Id;
			// Nationality
			var Nationality = new NationalityTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstNationality();
            Valide_Trainee.Nationality = null;
            Valide_Trainee.NationalityId = Nationality.Id;
			// Photo
			//var Photo = new PhotoTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstPhoto();
   //         Valide_Trainee.Photo = null;
   //         Valide_Trainee.PhotoId = Photo.Id;
			// Schoollevel
			var Schoollevel = new SchoollevelTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSchoollevel();
            Valide_Trainee.Schoollevel = null;
            Valide_Trainee.SchoollevelId = Schoollevel.Id;
			// Specialty
			var Specialty = new SpecialtyTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSpecialty();
            Valide_Trainee.Specialty = null;
            Valide_Trainee.SpecialtyId = Specialty.Id;
			// YearStudy
			var YearStudy = new YearStudyTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstYearStudy();
            Valide_Trainee.YearStudy = null;
            Valide_Trainee.YearStudyId = YearStudy.Id;
            // One to Many
            //
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
