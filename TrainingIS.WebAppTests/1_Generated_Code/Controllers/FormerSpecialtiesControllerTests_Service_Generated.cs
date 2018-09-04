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
    public class BaseFormerSpecialtiesControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseFormerSpecialtiesControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first FormerSpecialty instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual FormerSpecialty CreateOrLouadFirstFormerSpecialty(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            FormerSpecialtyBLO formerspecialtyBLO = new FormerSpecialtyBLO(unitOfWork,GAppContext);
           
			FormerSpecialty entity = null;
            if (formerspecialtyBLO.FindAll()?.Count > 0)
                entity = formerspecialtyBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp FormerSpecialty for Test
                entity = this.CreateValideFormerSpecialtyInstance(unitOfWork,GAppContext);
                formerspecialtyBLO.Save(entity);
            }
            return entity;
        }

        public virtual FormerSpecialty CreateValideFormerSpecialtyInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            FormerSpecialty  Valide_FormerSpecialty = this._Fixture.Create<FormerSpecialty>();
            Valide_FormerSpecialty.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_FormerSpecialty;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide FormerSpecialty can't exist</returns>
        public virtual FormerSpecialty CreateInValideFormerSpecialtyInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            FormerSpecialty formerspecialty = this.CreateValideFormerSpecialtyInstance(unitOfWork, GAppContext);
             
			// Required   
 
			formerspecialty.Code = null;
 
			formerspecialty.Name = null;
            //Unique
			var existant_FormerSpecialty = this.CreateOrLouadFirstFormerSpecialty(new UnitOfWork<TrainingISModel>(),GAppContext);
			formerspecialty.Reference = existant_FormerSpecialty.Reference;
 
            return formerspecialty;
        }


		public virtual FormerSpecialty CreateInValideFormerSpecialtyInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            FormerSpecialty formerspecialty = this.CreateOrLouadFirstFormerSpecialty(unitOfWork, GAppContext);
			// Required   
 
			formerspecialty.Code = null;
 
			formerspecialty.Name = null;
            //Unique
			var existant_FormerSpecialty = this.CreateOrLouadFirstFormerSpecialty(new UnitOfWork<TrainingISModel>(), GAppContext);
			formerspecialty.Reference = existant_FormerSpecialty.Reference;
            return formerspecialty;
        }
    }

	public partial class FormerSpecialtiesControllerTests_Service : BaseFormerSpecialtiesControllerTests_Service{}
}
