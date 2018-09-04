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
    public class BaseEntityPropertyShortcutsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseEntityPropertyShortcutsControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first EntityPropertyShortcut instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual EntityPropertyShortcut CreateOrLouadFirstEntityPropertyShortcut(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            EntityPropertyShortcutBLO entitypropertyshortcutBLO = new EntityPropertyShortcutBLO(unitOfWork,GAppContext);
           
			EntityPropertyShortcut entity = null;
            if (entitypropertyshortcutBLO.FindAll()?.Count > 0)
                entity = entitypropertyshortcutBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp EntityPropertyShortcut for Test
                entity = this.CreateValideEntityPropertyShortcutInstance(unitOfWork,GAppContext);
                entitypropertyshortcutBLO.Save(entity);
            }
            return entity;
        }

        public virtual EntityPropertyShortcut CreateValideEntityPropertyShortcutInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            EntityPropertyShortcut  Valide_EntityPropertyShortcut = this._Fixture.Create<EntityPropertyShortcut>();
            Valide_EntityPropertyShortcut.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_EntityPropertyShortcut;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide EntityPropertyShortcut can't exist</returns>
        public virtual EntityPropertyShortcut CreateInValideEntityPropertyShortcutInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            EntityPropertyShortcut entitypropertyshortcut = this.CreateValideEntityPropertyShortcutInstance(unitOfWork, GAppContext);
             
			// Required   
 
			entitypropertyshortcut.EntityName = null;
 
			entitypropertyshortcut.PropertyName = null;
 
			entitypropertyshortcut.PropertyShortcutName = null;
            //Unique
			var existant_EntityPropertyShortcut = this.CreateOrLouadFirstEntityPropertyShortcut(new UnitOfWork<TrainingISModel>(),GAppContext);
			entitypropertyshortcut.Reference = existant_EntityPropertyShortcut.Reference;
 
            return entitypropertyshortcut;
        }


		public virtual EntityPropertyShortcut CreateInValideEntityPropertyShortcutInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            EntityPropertyShortcut entitypropertyshortcut = this.CreateOrLouadFirstEntityPropertyShortcut(unitOfWork, GAppContext);
			// Required   
 
			entitypropertyshortcut.EntityName = null;
 
			entitypropertyshortcut.PropertyName = null;
 
			entitypropertyshortcut.PropertyShortcutName = null;
            //Unique
			var existant_EntityPropertyShortcut = this.CreateOrLouadFirstEntityPropertyShortcut(new UnitOfWork<TrainingISModel>(), GAppContext);
			entitypropertyshortcut.Reference = existant_EntityPropertyShortcut.Reference;
            return entitypropertyshortcut;
        }
    }

	public partial class EntityPropertyShortcutsControllerTests_Service : BaseEntityPropertyShortcutsControllerTests_Service{}
}
