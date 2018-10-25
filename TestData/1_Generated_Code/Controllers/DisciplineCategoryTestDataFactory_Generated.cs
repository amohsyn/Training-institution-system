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
    public class BaseDisciplineCategoryTestDataFactory : EntityTestData<DisciplineCategory>
    {
        public BaseDisciplineCategoryTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<DisciplineCategory> Generate_TestData()
        {
            List<DisciplineCategory> Data = base.Generate_TestData();
            if(Data == null) Data = new List<DisciplineCategory>();
            Data.Add(this.CreateValideDisciplineCategoryInstance());
            return Data;
        }
	
		/// <summary>
        /// Find the first DisciplineCategory instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual DisciplineCategory CreateOrLouadFirstDisciplineCategory()
        {
            DisciplineCategoryBLO disciplinecategoryBLO = new DisciplineCategoryBLO(UnitOfWork,GAppContext);
           
			DisciplineCategory entity = null;
            if (disciplinecategoryBLO.FindAll()?.Count > 0)
                entity = disciplinecategoryBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp DisciplineCategory for Test
                entity = this.CreateValideDisciplineCategoryInstance();
                disciplinecategoryBLO.Save(entity);
            }
            return entity;
        }

        public virtual DisciplineCategory CreateValideDisciplineCategoryInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            DisciplineCategory  Valide_DisciplineCategory = this._Fixture.Create<DisciplineCategory>();
            Valide_DisciplineCategory.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_DisciplineCategory;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide DisciplineCategory can't exist</returns>
        public virtual DisciplineCategory CreateInValideDisciplineCategoryInstance()
        {
            DisciplineCategory disciplinecategory = this.CreateValideDisciplineCategoryInstance();
             
			// Required   
 
			disciplinecategory.Code = null;
 
			disciplinecategory.Name = null;
            //Unique
			var existant_DisciplineCategory = this.CreateOrLouadFirstDisciplineCategory();
			disciplinecategory.Reference = existant_DisciplineCategory.Reference;
 
            return disciplinecategory;
        }


		public virtual DisciplineCategory CreateInValideDisciplineCategoryInstance_ForEdit()
        {
            DisciplineCategory disciplinecategory = this.CreateOrLouadFirstDisciplineCategory();
			// Required   
 
			disciplinecategory.Code = null;
 
			disciplinecategory.Name = null;
            //Unique
			var existant_DisciplineCategory = this.CreateOrLouadFirstDisciplineCategory();
			disciplinecategory.Reference = existant_DisciplineCategory.Reference;
            return disciplinecategory;
        }
    }

	public partial class DisciplineCategoryTestDataFactory : BaseDisciplineCategoryTestDataFactory{
	
		public DisciplineCategoryTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
