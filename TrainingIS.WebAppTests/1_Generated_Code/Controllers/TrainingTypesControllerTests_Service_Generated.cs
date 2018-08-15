﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

namespace TrainingIS.WebApp.Controllers.Tests
{
    public class TrainingTypesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public TrainingTypesControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first TrainingType instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public TrainingType CreateOrLouadFirstTrainingType(UnitOfWork<TrainingISModel> unitOfWork)
        {
            TrainingTypeBLO trainingtypeBLO = new TrainingTypeBLO(unitOfWork);
           
		   TrainingType entity = null;
            if (trainingtypeBLO.FindAll()?.Count > 0)
                entity = trainingtypeBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp TrainingType for Test
                entity = this.CreateValideTrainingTypeInstance();
                trainingtypeBLO.Save(entity);
            }
            return entity;
        }

        public TrainingType CreateValideTrainingTypeInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            TrainingType  Valide_TrainingType = this._Fixture.Create<TrainingType>();
            Valide_TrainingType.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_TrainingType;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide TrainingType can't exist</returns>
        public TrainingType CreateInValideTrainingTypeInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            TrainingType trainingtype = this.CreateValideTrainingTypeInstance(unitOfWork);
             
			// Required   
 
			trainingtype.Code = null;
 
			trainingtype.Name = null;
            //Unique
			var existant_TrainingType = this.CreateOrLouadFirstTrainingType(new UnitOfWork<TrainingISModel>());
			trainingtype.Code = existant_TrainingType.Code;
            
            return trainingtype;
        }


		  public TrainingType CreateInValideTrainingTypeInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            TrainingType trainingtype = this.CreateOrLouadFirstTrainingType(unitOfWork);
             
			// Required   
 
			trainingtype.Code = null;
 
			trainingtype.Name = null;
            //Unique
			var existant_TrainingType = this.CreateOrLouadFirstTrainingType(new UnitOfWork<TrainingISModel>());
			trainingtype.Code = existant_TrainingType.Code;
            
            return trainingtype;
        }
    }
}
