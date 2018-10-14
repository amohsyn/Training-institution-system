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
    public class BaseClassroomCategoryTestDataFactory : ITestDataFactory<ClassroomCategory>
    {
        private Fixture _Fixture = null;
		protected List<ClassroomCategory> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseClassroomCategoryTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<ClassroomCategory> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<ClassroomCategory> Generate()
        {
            return null;
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
