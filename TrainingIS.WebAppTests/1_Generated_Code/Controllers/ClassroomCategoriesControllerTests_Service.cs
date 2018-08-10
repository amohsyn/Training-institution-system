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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    public class ClassroomCategoriesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public ClassroomCategoriesControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first ClassroomCategory instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public ClassroomCategory CreateOrLouadFirstClassroomCategory(UnitOfWork unitOfWork)
        {
            ClassroomCategoryBLO classroomcategoryBLO = new ClassroomCategoryBLO(unitOfWork);
           
		   ClassroomCategory entity = null;
            if (classroomcategoryBLO.FindAll()?.Count > 0)
                entity = classroomcategoryBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp ClassroomCategory for Test
                entity = this.CreateValideClassroomCategoryInstance();
                classroomcategoryBLO.Save(entity);
            }
            return entity;
        }

        public ClassroomCategory CreateValideClassroomCategoryInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            ClassroomCategory  Valide_ClassroomCategory = this._Fixture.Create<ClassroomCategory>();
            Valide_ClassroomCategory.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_ClassroomCategory;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide ClassroomCategory can't exist</returns>
        public ClassroomCategory CreateInValideClassroomCategoryInstance(UnitOfWork unitOfWork = null)
        {
            ClassroomCategory classroomcategory = this.CreateValideClassroomCategoryInstance(unitOfWork);
             
			// Required   
 
			classroomcategory.Code = null;
            //Unique
			var existant_ClassroomCategory = this.CreateOrLouadFirstClassroomCategory(new UnitOfWork());
			classroomcategory.Code = existant_ClassroomCategory.Code;
            
            return classroomcategory;
        }


		  public ClassroomCategory CreateInValideClassroomCategoryInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            ClassroomCategory classroomcategory = this.CreateOrLouadFirstClassroomCategory(unitOfWork);
             
			// Required   
 
			classroomcategory.Code = null;
            //Unique
			var existant_ClassroomCategory = this.CreateOrLouadFirstClassroomCategory(new UnitOfWork());
			classroomcategory.Code = existant_ClassroomCategory.Code;
            
            return classroomcategory;
        }
    }
}

