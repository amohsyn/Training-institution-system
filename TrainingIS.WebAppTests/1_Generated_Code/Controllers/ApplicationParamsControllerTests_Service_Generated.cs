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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Tests.Services 
{
    public class BaseApplicationParamsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseApplicationParamsControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first ApplicationParam instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual ApplicationParam CreateOrLouadFirstApplicationParam(UnitOfWork<TrainingISModel> unitOfWork)
        {
            ApplicationParamBLO applicationparamBLO = new ApplicationParamBLO(unitOfWork);
           
			ApplicationParam entity = null;
            if (applicationparamBLO.FindAll()?.Count > 0)
                entity = applicationparamBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp ApplicationParam for Test
                entity = this.CreateValideApplicationParamInstance();
                applicationparamBLO.Save(entity);
            }
            return entity;
        }

        public virtual ApplicationParam CreateValideApplicationParamInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            ApplicationParam  Valide_ApplicationParam = this._Fixture.Create<ApplicationParam>();
            Valide_ApplicationParam.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_ApplicationParam;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide ApplicationParam can't exist</returns>
        public virtual ApplicationParam CreateInValideApplicationParamInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            ApplicationParam applicationparam = this.CreateValideApplicationParamInstance(unitOfWork);
             
			// Required   
 
			applicationparam.Code = null;
            //Unique
			var existant_ApplicationParam = this.CreateOrLouadFirstApplicationParam(new UnitOfWork<TrainingISModel>());
 
            return applicationparam;
        }


		public virtual ApplicationParam CreateInValideApplicationParamInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            ApplicationParam applicationparam = this.CreateOrLouadFirstApplicationParam(unitOfWork);
			// Required   
 
			applicationparam.Code = null;
            //Unique
			var existant_ApplicationParam = this.CreateOrLouadFirstApplicationParam(new UnitOfWork<TrainingISModel>());
            return applicationparam;
        }
    }

	public partial class ApplicationParamsControllerTests_Service : BaseApplicationParamsControllerTests_Service{}
}
