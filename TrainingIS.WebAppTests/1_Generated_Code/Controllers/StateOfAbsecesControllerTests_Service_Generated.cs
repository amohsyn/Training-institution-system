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
    public class BaseStateOfAbsecesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseStateOfAbsecesControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first StateOfAbsece instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual StateOfAbsece CreateOrLouadFirstStateOfAbsece(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            StateOfAbseceBLO stateofabseceBLO = new StateOfAbseceBLO(unitOfWork,GAppContext);
           
			StateOfAbsece entity = null;
            if (stateofabseceBLO.FindAll()?.Count > 0)
                entity = stateofabseceBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp StateOfAbsece for Test
                entity = this.CreateValideStateOfAbseceInstance(unitOfWork,GAppContext);
                stateofabseceBLO.Save(entity);
            }
            return entity;
        }

        public virtual StateOfAbsece CreateValideStateOfAbseceInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            StateOfAbsece  Valide_StateOfAbsece = this._Fixture.Create<StateOfAbsece>();
            Valide_StateOfAbsece.Id = 0;
            // Many to One 
            //
			// Trainee
			var Trainee = new TraineesControllerTests_Service().CreateOrLouadFirstTrainee(unitOfWork,GAppContext);
            Valide_StateOfAbsece.Trainee = null;
            Valide_StateOfAbsece.TraineeId = Trainee.Id;
            // One to Many
            //
            return Valide_StateOfAbsece;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide StateOfAbsece can't exist</returns>
        public virtual StateOfAbsece CreateInValideStateOfAbseceInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            StateOfAbsece stateofabsece = this.CreateValideStateOfAbseceInstance(unitOfWork, GAppContext);
             
			// Required   
 
			stateofabsece.Name = null;
 
			stateofabsece.Category = StateOfAbseceCategories.TrainingYear;
 
			stateofabsece.Value = 0;
 
			stateofabsece.TraineeId = 0;
            //Unique
			var existant_StateOfAbsece = this.CreateOrLouadFirstStateOfAbsece(new UnitOfWork<TrainingISModel>(),GAppContext);
 
            return stateofabsece;
        }


		public virtual StateOfAbsece CreateInValideStateOfAbseceInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            StateOfAbsece stateofabsece = this.CreateOrLouadFirstStateOfAbsece(unitOfWork, GAppContext);
			// Required   
 
			stateofabsece.Name = null;
 
			stateofabsece.Category = StateOfAbseceCategories.TrainingYear;
 
			stateofabsece.Value = 0;
 
			stateofabsece.TraineeId = 0;
            //Unique
			var existant_StateOfAbsece = this.CreateOrLouadFirstStateOfAbsece(new UnitOfWork<TrainingISModel>(), GAppContext);
            return stateofabsece;
        }
    }

	public partial class StateOfAbsecesControllerTests_Service : BaseStateOfAbsecesControllerTests_Service{}
}
