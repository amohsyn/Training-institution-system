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
    public class ApplicationParamsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public ApplicationParamsControllerTests_Service()
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
        public ApplicationParam CreateOrLouadFirstApplicationParam(UnitOfWork unitOfWork)
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

        public ApplicationParam CreateValideApplicationParamInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
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
        public ApplicationParam CreateInValideApplicationParamInstance(UnitOfWork unitOfWork = null)
        {
            ApplicationParam applicationparam = this.CreateValideApplicationParamInstance(unitOfWork);
             
			// Required   
 
			applicationparam.Code = null;
            //Unique
			var existant_ApplicationParam = this.CreateOrLouadFirstApplicationParam(new UnitOfWork());
            
            return applicationparam;
        }


		  public ApplicationParam CreateInValideApplicationParamInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            ApplicationParam applicationparam = this.CreateOrLouadFirstApplicationParam(unitOfWork);
             
			// Required   
 
			applicationparam.Code = null;
            //Unique
			var existant_ApplicationParam = this.CreateOrLouadFirstApplicationParam(new UnitOfWork());
            
            return applicationparam;
        }
    }
}

