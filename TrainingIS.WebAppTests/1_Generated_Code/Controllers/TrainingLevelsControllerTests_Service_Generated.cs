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
    public class BaseTrainingLevelsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseTrainingLevelsControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first TrainingLevel instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual TrainingLevel CreateOrLouadFirstTrainingLevel(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            TrainingLevelBLO traininglevelBLO = new TrainingLevelBLO(unitOfWork,GAppContext);
           
			TrainingLevel entity = null;
            if (traininglevelBLO.FindAll()?.Count > 0)
                entity = traininglevelBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp TrainingLevel for Test
                entity = this.CreateValideTrainingLevelInstance(unitOfWork,GAppContext);
                traininglevelBLO.Save(entity);
            }
            return entity;
        }

        public virtual TrainingLevel CreateValideTrainingLevelInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            TrainingLevel  Valide_TrainingLevel = this._Fixture.Create<TrainingLevel>();
            Valide_TrainingLevel.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_TrainingLevel;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide TrainingLevel can't exist</returns>
        public virtual TrainingLevel CreateInValideTrainingLevelInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            TrainingLevel traininglevel = this.CreateValideTrainingLevelInstance(unitOfWork, GAppContext);
             
			// Required   
 
			traininglevel.Code = null;
 
			traininglevel.Name = null;
            //Unique
			var existant_TrainingLevel = this.CreateOrLouadFirstTrainingLevel(new UnitOfWork<TrainingISModel>(),GAppContext);
 
            return traininglevel;
        }


		public virtual TrainingLevel CreateInValideTrainingLevelInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            TrainingLevel traininglevel = this.CreateOrLouadFirstTrainingLevel(unitOfWork, GAppContext);
			// Required   
 
			traininglevel.Code = null;
 
			traininglevel.Name = null;
            //Unique
			var existant_TrainingLevel = this.CreateOrLouadFirstTrainingLevel(new UnitOfWork<TrainingISModel>(), GAppContext);
            return traininglevel;
        }
    }

	public partial class TrainingLevelsControllerTests_Service : BaseTrainingLevelsControllerTests_Service{}
}
