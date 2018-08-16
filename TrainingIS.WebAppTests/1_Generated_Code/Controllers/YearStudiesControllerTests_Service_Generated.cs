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
    public class BaseYearStudiesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseYearStudiesControllerTests_Service()
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
        public virtual YearStudy CreateOrLouadFirstYearStudy(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            YearStudyBLO yearstudyBLO = new YearStudyBLO(unitOfWork,GAppContext);
           
			YearStudy entity = null;
            if (yearstudyBLO.FindAll()?.Count > 0)
                entity = yearstudyBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp YearStudy for Test
                entity = this.CreateValideYearStudyInstance(unitOfWork,GAppContext);
                yearstudyBLO.Save(entity);
            }
            return entity;
        }

        public virtual YearStudy CreateValideYearStudyInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
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
        public virtual YearStudy CreateInValideYearStudyInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            YearStudy yearstudy = this.CreateValideYearStudyInstance(unitOfWork, GAppContext);
             
			// Required   
 
			yearstudy.Code = null;
 
			yearstudy.Name = null;
            //Unique
			var existant_YearStudy = this.CreateOrLouadFirstYearStudy(new UnitOfWork<TrainingISModel>(),GAppContext);
			yearstudy.Code = existant_YearStudy.Code;
 
            return yearstudy;
        }


		public virtual YearStudy CreateInValideYearStudyInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            YearStudy yearstudy = this.CreateOrLouadFirstYearStudy(unitOfWork, GAppContext);
			// Required   
 
			yearstudy.Code = null;
 
			yearstudy.Name = null;
            //Unique
			var existant_YearStudy = this.CreateOrLouadFirstYearStudy(new UnitOfWork<TrainingISModel>(), GAppContext);
			yearstudy.Code = existant_YearStudy.Code;
            return yearstudy;
        }
    }

	public partial class YearStudiesControllerTests_Service : BaseYearStudiesControllerTests_Service{}
}
