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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Tests.Services 
{
    public class BaseClassroomCategoriesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseClassroomCategoriesControllerTests_Service()
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
        public virtual ClassroomCategory CreateOrLouadFirstClassroomCategory(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            ClassroomCategoryBLO classroomcategoryBLO = new ClassroomCategoryBLO(unitOfWork,GAppContext);
           
			ClassroomCategory entity = null;
            if (classroomcategoryBLO.FindAll()?.Count > 0)
                entity = classroomcategoryBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp ClassroomCategory for Test
                entity = this.CreateValideClassroomCategoryInstance(unitOfWork,GAppContext);
                classroomcategoryBLO.Save(entity);
            }
            return entity;
        }

        public virtual ClassroomCategory CreateValideClassroomCategoryInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
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
        public virtual ClassroomCategory CreateInValideClassroomCategoryInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            ClassroomCategory classroomcategory = this.CreateValideClassroomCategoryInstance(unitOfWork, GAppContext);
             
			// Required   
 
			classroomcategory.Code = null;
            //Unique
			var existant_ClassroomCategory = this.CreateOrLouadFirstClassroomCategory(new UnitOfWork<TrainingISModel>(),GAppContext);
			classroomcategory.Code = existant_ClassroomCategory.Code;
			classroomcategory.Reference = existant_ClassroomCategory.Reference;
 
            return classroomcategory;
        }


		public virtual ClassroomCategory CreateInValideClassroomCategoryInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            ClassroomCategory classroomcategory = this.CreateOrLouadFirstClassroomCategory(unitOfWork, GAppContext);
			// Required   
 
			classroomcategory.Code = null;
            //Unique
			var existant_ClassroomCategory = this.CreateOrLouadFirstClassroomCategory(new UnitOfWork<TrainingISModel>(), GAppContext);
			classroomcategory.Code = existant_ClassroomCategory.Code;
			classroomcategory.Reference = existant_ClassroomCategory.Reference;
            return classroomcategory;
        }
    }

	public partial class ClassroomCategoriesControllerTests_Service : BaseClassroomCategoriesControllerTests_Service{}
}
