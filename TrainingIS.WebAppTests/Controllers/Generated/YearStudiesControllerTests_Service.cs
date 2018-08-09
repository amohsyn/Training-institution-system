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
    public class YearStudiesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public YearStudiesControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first YearStudy instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public YearStudy CreateOrLouadFirstYearStudy(UnitOfWork unitOfWork)
        {
            YearStudyBLO yearstudyBLO = new YearStudyBLO(unitOfWork);
           
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

        public YearStudy CreateValideYearStudyInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
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
        public YearStudy CreateInValideYearStudyInstance(UnitOfWork unitOfWork = null)
        {
            YearStudy yearstudy = this.CreateValideYearStudyInstance(unitOfWork);
             
			// Required   
 
			yearstudy.Code = null;
 
			yearstudy.Name = null;
            //Unique
			var existant_YearStudy = this.CreateOrLouadFirstYearStudy(new UnitOfWork());
			yearstudy.Code = existant_YearStudy.Code;
            
            return yearstudy;
        }


		  public YearStudy CreateInValideYearStudyInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            YearStudy yearstudy = this.CreateOrLouadFirstYearStudy(unitOfWork);
             
			// Required   
 
			yearstudy.Code = null;
 
			yearstudy.Name = null;
            //Unique
			var existant_YearStudy = this.CreateOrLouadFirstYearStudy(new UnitOfWork());
			yearstudy.Code = existant_YearStudy.Code;
            
            return yearstudy;
        }
    }
}

