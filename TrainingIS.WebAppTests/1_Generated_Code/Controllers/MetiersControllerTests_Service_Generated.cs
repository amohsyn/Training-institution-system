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
    public class BaseMetiersControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseMetiersControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first Metier instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Metier CreateOrLouadFirstMetier(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            MetierBLO metierBLO = new MetierBLO(unitOfWork,GAppContext);
           
			Metier entity = null;
            if (metierBLO.FindAll()?.Count > 0)
                entity = metierBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Metier for Test
                entity = this.CreateValideMetierInstance(unitOfWork,GAppContext);
                metierBLO.Save(entity);
            }
            return entity;
        }

        public virtual Metier CreateValideMetierInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            Metier  Valide_Metier = this._Fixture.Create<Metier>();
            Valide_Metier.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_Metier;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Metier can't exist</returns>
        public virtual Metier CreateInValideMetierInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Metier metier = this.CreateValideMetierInstance(unitOfWork, GAppContext);
             
			// Required   
 
			metier.Code = null;
 
			metier.Name = null;
            //Unique
			var existant_Metier = this.CreateOrLouadFirstMetier(new UnitOfWork<TrainingISModel>(),GAppContext);
			metier.Reference = existant_Metier.Reference;
 
            return metier;
        }


		public virtual Metier CreateInValideMetierInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Metier metier = this.CreateOrLouadFirstMetier(unitOfWork, GAppContext);
			// Required   
 
			metier.Code = null;
 
			metier.Name = null;
            //Unique
			var existant_Metier = this.CreateOrLouadFirstMetier(new UnitOfWork<TrainingISModel>(), GAppContext);
			metier.Reference = existant_Metier.Reference;
            return metier;
        }
    }

	public partial class MetiersControllerTests_Service : BaseMetiersControllerTests_Service{}
}
