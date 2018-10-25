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
    public class BaseSanctionCategoryTestDataFactory : EntityTestData<SanctionCategory>
    {
        public BaseSanctionCategoryTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<SanctionCategory> Generate_TestData()
        {
            List<SanctionCategory> Data = base.Generate_TestData();
            if(Data == null) Data = new List<SanctionCategory>();
            Data.Add(this.CreateValideSanctionCategoryInstance());
            return Data;
        }
	
		/// <summary>
        /// Find the first SanctionCategory instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual SanctionCategory CreateOrLouadFirstSanctionCategory()
        {
            SanctionCategoryBLO sanctioncategoryBLO = new SanctionCategoryBLO(UnitOfWork,GAppContext);
           
			SanctionCategory entity = null;
            if (sanctioncategoryBLO.FindAll()?.Count > 0)
                entity = sanctioncategoryBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp SanctionCategory for Test
                entity = this.CreateValideSanctionCategoryInstance();
                sanctioncategoryBLO.Save(entity);
            }
            return entity;
        }

        public virtual SanctionCategory CreateValideSanctionCategoryInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            SanctionCategory  Valide_SanctionCategory = this._Fixture.Create<SanctionCategory>();
            Valide_SanctionCategory.Id = 0;
            // Many to One 
            //   
			// DisciplineCategory
			var DisciplineCategory = new DisciplineCategoryTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstDisciplineCategory();
            Valide_SanctionCategory.DisciplineCategory = DisciplineCategory;
						 Valide_SanctionCategory.DisciplineCategoryId = DisciplineCategory.Id;
			           
            // One to Many
            //
            return Valide_SanctionCategory;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide SanctionCategory can't exist</returns>
        public virtual SanctionCategory CreateInValideSanctionCategoryInstance()
        {
            SanctionCategory sanctioncategory = this.CreateValideSanctionCategoryInstance();
             
			// Required   
 
			sanctioncategory.DisciplineCategoryId = 0;
 
			sanctioncategory.Name = null;
 
			sanctioncategory.Code = null;
            //Unique
			var existant_SanctionCategory = this.CreateOrLouadFirstSanctionCategory();
			sanctioncategory.Reference = existant_SanctionCategory.Reference;
 
            return sanctioncategory;
        }


		public virtual SanctionCategory CreateInValideSanctionCategoryInstance_ForEdit()
        {
            SanctionCategory sanctioncategory = this.CreateOrLouadFirstSanctionCategory();
			// Required   
 
			sanctioncategory.DisciplineCategoryId = 0;
 
			sanctioncategory.Name = null;
 
			sanctioncategory.Code = null;
            //Unique
			var existant_SanctionCategory = this.CreateOrLouadFirstSanctionCategory();
			sanctioncategory.Reference = existant_SanctionCategory.Reference;
            return sanctioncategory;
        }
    }

	public partial class SanctionCategoryTestDataFactory : BaseSanctionCategoryTestDataFactory{
	
		public SanctionCategoryTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
