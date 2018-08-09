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
    public class LogWorksControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public LogWorksControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first LogWork instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public LogWork CreateOrLouadFirstLogWork(UnitOfWork unitOfWork)
        {
            LogWorkBLO logworkBLO = new LogWorkBLO(unitOfWork);
           
		   LogWork entity = null;
            if (logworkBLO.FindAll()?.Count > 0)
                entity = logworkBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp LogWork for Test
                entity = this.CreateValideLogWorkInstance();
                logworkBLO.Save(entity);
            }
            return entity;
        }

        public LogWork CreateValideLogWorkInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            LogWork  Valide_LogWork = this._Fixture.Create<LogWork>();
            Valide_LogWork.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_LogWork;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide LogWork can't exist</returns>
        public LogWork CreateInValideLogWorkInstance(UnitOfWork unitOfWork = null)
        {
            LogWork logwork = this.CreateValideLogWorkInstance(unitOfWork);
             
			// Required   
 
			logwork.UserId = null;
 
			logwork.OperationWorkType = OperationWorkTypes.Import;
            //Unique
			var existant_LogWork = this.CreateOrLouadFirstLogWork(new UnitOfWork());
            
            return logwork;
        }


		  public LogWork CreateInValideLogWorkInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            LogWork logwork = this.CreateOrLouadFirstLogWork(unitOfWork);
             
			// Required   
 
			logwork.UserId = null;
 
			logwork.OperationWorkType = OperationWorkTypes.Import;
            //Unique
			var existant_LogWork = this.CreateOrLouadFirstLogWork(new UnitOfWork());
            
            return logwork;
        }
    }
}

