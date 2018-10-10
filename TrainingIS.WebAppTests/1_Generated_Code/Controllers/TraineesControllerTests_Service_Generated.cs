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
using TrainingIS.Models.Trainees;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Tests.Services 
{
    public class BaseTraineesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseTraineesControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first Trainee instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Trainee CreateOrLouadFirstTrainee(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            TraineeBLO traineeBLO = new TraineeBLO(unitOfWork,GAppContext);
           
			Trainee entity = null;
            if (traineeBLO.FindAll()?.Count > 0)
                entity = traineeBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Trainee for Test
                entity = this.CreateValideTraineeInstance(unitOfWork,GAppContext);
                traineeBLO.Save(entity);
            }
            return entity;
        }

        public virtual Trainee CreateValideTraineeInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            Trainee  Valide_Trainee = this._Fixture.Create<Trainee>();
            Valide_Trainee.Id = 0;
            // Many to One 
            //
			// Group
			var Group = new GroupsControllerTests_Service().CreateOrLouadFirstGroup(unitOfWork,GAppContext);
            Valide_Trainee.Group = null;
            Valide_Trainee.GroupId = Group.Id;
			// Nationality
			var Nationality = new NationalitiesControllerTests_Service().CreateOrLouadFirstNationality(unitOfWork,GAppContext);
            Valide_Trainee.Nationality = null;
            Valide_Trainee.NationalityId = Nationality.Id;
			// Photo
			//var Photo = new PhotosControllerTests_Service().CreateOrLouadFirstPhoto(unitOfWork,GAppContext);
   //         Valide_Trainee.Photo = null;
   //         Valide_Trainee.PhotoId = Photo.Id;
			// Schoollevel
			var Schoollevel = new SchoollevelsControllerTests_Service().CreateOrLouadFirstSchoollevel(unitOfWork,GAppContext);
            Valide_Trainee.Schoollevel = null;
            Valide_Trainee.SchoollevelId = Schoollevel.Id;
			// Specialty
			var Specialty = new SpecialtiesControllerTests_Service().CreateOrLouadFirstSpecialty(unitOfWork,GAppContext);
            Valide_Trainee.Specialty = null;
            Valide_Trainee.SpecialtyId = Specialty.Id;
			// YearStudy
			var YearStudy = new YearStudiesControllerTests_Service().CreateOrLouadFirstYearStudy(unitOfWork,GAppContext);
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
        public virtual Trainee CreateInValideTraineeInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Trainee trainee = this.CreateValideTraineeInstance(unitOfWork, GAppContext);
             
			// Required   
 
			trainee.CNE = null;
 
			trainee.GroupId = 0;
 
			trainee.FirstName = null;
 
			trainee.LastName = null;
 
			trainee.Sex = SexEnum.man;
            //Unique
			var existant_Trainee = this.CreateOrLouadFirstTrainee(new UnitOfWork<TrainingISModel>(),GAppContext);
			trainee.CNE = existant_Trainee.CNE;
			trainee.CIN = existant_Trainee.CIN;
			trainee.Email = existant_Trainee.Email;
			trainee.Reference = existant_Trainee.Reference;
 
            return trainee;
        }


		public virtual Trainee CreateInValideTraineeInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Trainee trainee = this.CreateOrLouadFirstTrainee(unitOfWork, GAppContext);
			// Required   
 
			trainee.CNE = null;
 
			trainee.GroupId = 0;
 
			trainee.FirstName = null;
 
			trainee.LastName = null;
 
			trainee.Sex = SexEnum.man;
            //Unique
			var existant_Trainee = this.CreateOrLouadFirstTrainee(new UnitOfWork<TrainingISModel>(), GAppContext);
			trainee.CNE = existant_Trainee.CNE;
			trainee.CIN = existant_Trainee.CIN;
			trainee.Email = existant_Trainee.Email;
			trainee.Reference = existant_Trainee.Reference;
            return trainee;
        }
    }

	public partial class TraineesControllerTests_Service : BaseTraineesControllerTests_Service{}
}
