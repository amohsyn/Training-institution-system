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
    public class BaseTrainingTypesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseTrainingTypesControllerTests_Service()
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
        public virtual TrainingType CreateOrLouadFirstTrainingType(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            TrainingTypeBLO trainingtypeBLO = new TrainingTypeBLO(unitOfWork,GAppContext);
           
			TrainingType entity = null;
            if (trainingtypeBLO.FindAll()?.Count > 0)
                entity = trainingtypeBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp TrainingType for Test
                entity = this.CreateValideTrainingTypeInstance(unitOfWork,GAppContext);
                trainingtypeBLO.Save(entity);
            }
            return entity;
        }

        public virtual TrainingType CreateValideTrainingTypeInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
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
        public virtual TrainingType CreateInValideTrainingTypeInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            TrainingType trainingtype = this.CreateValideTrainingTypeInstance(unitOfWork, GAppContext);
             
			// Required   
 
			trainingtype.Code = null;
 
			trainingtype.Name = null;
            //Unique
			var existant_TrainingType = this.CreateOrLouadFirstTrainingType(new UnitOfWork<TrainingISModel>(),GAppContext);
			trainingtype.Code = existant_TrainingType.Code;
 
            return trainingtype;
        }


		public virtual TrainingType CreateInValideTrainingTypeInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            TrainingType trainingtype = this.CreateOrLouadFirstTrainingType(unitOfWork, GAppContext);
			// Required   
 
			trainingtype.Code = null;
 
			trainingtype.Name = null;
            //Unique
			var existant_TrainingType = this.CreateOrLouadFirstTrainingType(new UnitOfWork<TrainingISModel>(), GAppContext);
			trainingtype.Code = existant_TrainingType.Code;
            return trainingtype;
        }
    }

	public partial class TrainingTypesControllerTests_Service : BaseTrainingTypesControllerTests_Service{}
}
