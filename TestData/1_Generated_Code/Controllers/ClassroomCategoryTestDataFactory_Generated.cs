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
    public class BaseClassroomCategoryTestDataFactory : EntityTestData<ClassroomCategory>
    {
        public BaseClassroomCategoryTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<ClassroomCategory> Generate_TestData()
        {
            List<ClassroomCategory> Data = base.Generate_TestData();
            if(Data == null) Data = new List<ClassroomCategory>();
            Data.Add(this.CreateValideClassroomCategoryInstance());
            return Data;
        }
	
		/// <summary>
        /// Find the first ClassroomCategory instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual ClassroomCategory CreateOrLouadFirstClassroomCategory()
        {
            ClassroomCategoryBLO classroomcategoryBLO = new ClassroomCategoryBLO(UnitOfWork,GAppContext);
           
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

        public virtual ClassroomCategory CreateValideClassroomCategoryInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
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
        public virtual ClassroomCategory CreateInValideClassroomCategoryInstance()
        {
            ClassroomCategory classroomcategory = this.CreateValideClassroomCategoryInstance();
             
			// Required   
 
			classroomcategory.Code = null;
            //Unique
			var existant_ClassroomCategory = this.CreateOrLouadFirstClassroomCategory();
			classroomcategory.Code = existant_ClassroomCategory.Code;
			classroomcategory.Reference = existant_ClassroomCategory.Reference;
 
            return classroomcategory;
        }


		public virtual ClassroomCategory CreateInValideClassroomCategoryInstance_ForEdit()
        {
            ClassroomCategory classroomcategory = this.CreateOrLouadFirstClassroomCategory();
			// Required   
 
			classroomcategory.Code = null;
            //Unique
			var existant_ClassroomCategory = this.CreateOrLouadFirstClassroomCategory();
			classroomcategory.Code = existant_ClassroomCategory.Code;
			classroomcategory.Reference = existant_ClassroomCategory.Reference;
            return classroomcategory;
        }
    }

	public partial class ClassroomCategoryTestDataFactory : BaseClassroomCategoryTestDataFactory{
	
		public ClassroomCategoryTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
