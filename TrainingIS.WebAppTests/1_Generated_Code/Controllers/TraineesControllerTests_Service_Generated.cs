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
    public class TraineesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public TraineesControllerTests_Service()
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
        public Trainee CreateOrLouadFirstTrainee(UnitOfWork<TrainingISModel> unitOfWork)
        {
            TraineeBLO traineeBLO = new TraineeBLO(unitOfWork);
           
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

        public Trainee CreateValideTraineeInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            Trainee  Valide_Trainee = this._Fixture.Create<Trainee>();
            Valide_Trainee.Id = 0;
            // Many to One 
            //
			// Group
			var Group = new GroupsControllerTests_Service().CreateOrLouadFirstGroup(unitOfWork);
            Valide_Trainee.Group = null;
            Valide_Trainee.GroupId = Group.Id;
			// Nationality
			var Nationality = new NationalitiesControllerTests_Service().CreateOrLouadFirstNationality(unitOfWork);
            Valide_Trainee.Nationality = null;
            Valide_Trainee.NationalityId = Nationality.Id;
			// Schoollevel
			var Schoollevel = new SchoollevelsControllerTests_Service().CreateOrLouadFirstSchoollevel(unitOfWork);
            Valide_Trainee.Schoollevel = null;
            Valide_Trainee.SchoollevelId = Schoollevel.Id;
            // One to Many
            //
			Valide_Trainee.StateOfAbseces = null;
            return Valide_Trainee;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Trainee can't exist</returns>
        public Trainee CreateInValideTraineeInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            Trainee trainee = this.CreateValideTraineeInstance(unitOfWork);
             
			// Required   
 
			trainee.CNE = null;
 
			trainee.GroupId = 0;
 
			trainee.FirstName = null;
 
			trainee.LastName = null;
 
			trainee.FirstNameArabe = null;
 
			trainee.LastNameArabe = null;
 
			trainee.Sex = SexEnum.man;
 
			trainee.Birthdate = DateTime.Now;
 
			trainee.NationalityId = 0;
 
			trainee.BirthPlace = null;
 
			trainee.CIN = null;
 
			trainee.Email = null;
            //Unique
			var existant_Trainee = this.CreateOrLouadFirstTrainee(new UnitOfWork<TrainingISModel>());
			trainee.CNE = existant_Trainee.CNE;
			trainee.CIN = existant_Trainee.CIN;
            
            return trainee;
        }


		  public Trainee CreateInValideTraineeInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            Trainee trainee = this.CreateOrLouadFirstTrainee(unitOfWork);
             
			// Required   
 
			trainee.CNE = null;
 
			trainee.GroupId = 0;
 
			trainee.FirstName = null;
 
			trainee.LastName = null;
 
			trainee.FirstNameArabe = null;
 
			trainee.LastNameArabe = null;
 
			trainee.Sex = SexEnum.man;
 
			trainee.Birthdate = DateTime.Now;
 
			trainee.NationalityId = 0;
 
			trainee.BirthPlace = null;
 
			trainee.CIN = null;
 
			trainee.Email = null;
            //Unique
			var existant_Trainee = this.CreateOrLouadFirstTrainee(new UnitOfWork<TrainingISModel>());
			trainee.CNE = existant_Trainee.CNE;
			trainee.CIN = existant_Trainee.CIN;
            
            return trainee;
        }
    }
}

