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
    public class BaseYearStudyTestDataFactory : EntityTestData<YearStudy>
    {
        public BaseYearStudyTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<YearStudy> Generate_TestData()
        {
            List<YearStudy> Data = base.Generate_TestData();
            if(Data == null) Data = new List<YearStudy>();
            Data.Add(this.CreateValideYearStudyInstance());
            return Data;
        }
	
		/// <summary>
        /// Find the first YearStudy instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual YearStudy CreateOrLouadFirstYearStudy()
        {
            YearStudyBLO yearstudyBLO = new YearStudyBLO(UnitOfWork,GAppContext);
           
			YearStudy entity = null;
            if (yearstudyBLO.FindAll()?.Count > 0)
                entity = yearstudyBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp YearStudy for Test
                entity = this.CreateValideYearStudyInstance();
                yearstudyBLO.Save(entity);
            }
            return entity;
        }

        public virtual YearStudy CreateValideYearStudyInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            YearStudy  Valide_YearStudy = this._Fixture.Create<YearStudy>();
            Valide_YearStudy.Id = 0;
            // Many to One 
            //  
            // One to Many
            //
            return Valide_YearStudy;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide YearStudy can't exist</returns>
        public virtual YearStudy CreateInValideYearStudyInstance()
        {
            YearStudy yearstudy = this.CreateValideYearStudyInstance();
             
			// Required   
 
			yearstudy.Code = null;
 
			yearstudy.Name = null;
            //Unique
			var existant_YearStudy = this.CreateOrLouadFirstYearStudy();
			yearstudy.Code = existant_YearStudy.Code;
			yearstudy.Reference = existant_YearStudy.Reference;
 
            return yearstudy;
        }


		public virtual YearStudy CreateInValideYearStudyInstance_ForEdit()
        {
            YearStudy yearstudy = this.CreateOrLouadFirstYearStudy();
			// Required   
 
			yearstudy.Code = null;
 
			yearstudy.Name = null;
            //Unique
			var existant_YearStudy = this.CreateOrLouadFirstYearStudy();
			yearstudy.Code = existant_YearStudy.Code;
			yearstudy.Reference = existant_YearStudy.Reference;
            return yearstudy;
        }
    }

	public partial class YearStudyTestDataFactory : BaseYearStudyTestDataFactory{
	
		public YearStudyTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
