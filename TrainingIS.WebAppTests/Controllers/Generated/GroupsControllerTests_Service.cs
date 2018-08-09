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
using TrainingIS.WebApp.Helpers.AlertMessages;
using GApp.WebApp.Tests;
using GApp.WebApp.Manager.Views;
using TrainingIS.WebApp.Tests.TestUtilities;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    public class GroupsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public GroupsControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first Group instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public Group CreateOrLouadFirstGroup(UnitOfWork unitOfWork)
        {
            GroupBLO groupBLO = new GroupBLO(unitOfWork);
           
		   Group entity = null;
            if (groupBLO.FindAll()?.Count > 0)
                entity = groupBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp Group for Test
                entity = this.CreateValideGroupInstance();
                groupBLO.Save(entity);
            }
            return entity;
        }

        public Group CreateValideGroupInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            Group  Valide_Group = this._Fixture.Create<Group>();
            Valide_Group.Id = 0;
            // Many to One 
            //
			// Specialty
			var Specialty = new SpecialtiesControllerTests_Service().CreateOrLouadFirstSpecialty(unitOfWork);
            Valide_Group.Specialty = null;
            Valide_Group.SpecialtyId = Specialty.Id;
			// TrainingType
			var TrainingType = new TrainingTypesControllerTests_Service().CreateOrLouadFirstTrainingType(unitOfWork);
            Valide_Group.TrainingType = null;
            Valide_Group.TrainingTypeId = TrainingType.Id;
			// TrainingYear
			var TrainingYear = new TrainingYearsControllerTests_Service().CreateOrLouadFirstTrainingYear(unitOfWork);
            Valide_Group.TrainingYear = null;
            Valide_Group.TrainingYearId = TrainingYear.Id;
			// YearStudy
			var YearStudy = new YearStudiesControllerTests_Service().CreateOrLouadFirstYearStudy(unitOfWork);
            Valide_Group.YearStudy = null;
            Valide_Group.YearStudyId = YearStudy.Id;
            // One to Many
            //
            return Valide_Group;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Group can't exist</returns>
        public Group CreateInValideGroupInstance(UnitOfWork unitOfWork = null)
        {
            Group group = this.CreateValideGroupInstance(unitOfWork);
             
			// Required   
 
			group.TrainingTypeId = 0;
 
			group.TrainingYearId = 0;
 
			group.SpecialtyId = 0;
 
			group.YearStudyId = 0;
 
			group.Code = null;
            //Unique
			var existant_Group = this.CreateOrLouadFirstGroup(new UnitOfWork());
            
            return group;
        }


		  public Group CreateInValideGroupInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            Group group = this.CreateOrLouadFirstGroup(unitOfWork);
             
			// Required   
 
			group.TrainingTypeId = 0;
 
			group.TrainingYearId = 0;
 
			group.SpecialtyId = 0;
 
			group.YearStudyId = 0;
 
			group.Code = null;
            //Unique
			var existant_Group = this.CreateOrLouadFirstGroup(new UnitOfWork());
            
            return group;
        }
    }
}

