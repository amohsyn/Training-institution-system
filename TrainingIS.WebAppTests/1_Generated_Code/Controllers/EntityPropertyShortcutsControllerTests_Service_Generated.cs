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

namespace TrainingIS.WebApp.Controllers.Tests
{
    public class EntityPropertyShortcutsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public EntityPropertyShortcutsControllerTests_Service()
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
        public EntityPropertyShortcut CreateOrLouadFirstEntityPropertyShortcut(UnitOfWork<TrainingISModel> unitOfWork)
        {
            EntityPropertyShortcutBLO entitypropertyshortcutBLO = new EntityPropertyShortcutBLO(unitOfWork);
           
		   EntityPropertyShortcut entity = null;
            if (entitypropertyshortcutBLO.FindAll()?.Count > 0)
                entity = entitypropertyshortcutBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp EntityPropertyShortcut for Test
                entity = this.CreateValideEntityPropertyShortcutInstance();
                entitypropertyshortcutBLO.Save(entity);
            }
            return entity;
        }

        public EntityPropertyShortcut CreateValideEntityPropertyShortcutInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
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
        public EntityPropertyShortcut CreateInValideEntityPropertyShortcutInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            EntityPropertyShortcut entitypropertyshortcut = this.CreateValideEntityPropertyShortcutInstance(unitOfWork);
             
			// Required   
 
			entitypropertyshortcut.EntityName = null;
 
			entitypropertyshortcut.PropertyName = null;
 
			entitypropertyshortcut.PropertyShortcutName = null;
            //Unique
			var existant_EntityPropertyShortcut = this.CreateOrLouadFirstEntityPropertyShortcut(new UnitOfWork<TrainingISModel>());
            
            return entitypropertyshortcut;
        }


		  public EntityPropertyShortcut CreateInValideEntityPropertyShortcutInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            EntityPropertyShortcut entitypropertyshortcut = this.CreateOrLouadFirstEntityPropertyShortcut(unitOfWork);
             
			// Required   
 
			entitypropertyshortcut.EntityName = null;
 
			entitypropertyshortcut.PropertyName = null;
 
			entitypropertyshortcut.PropertyShortcutName = null;
            //Unique
			var existant_EntityPropertyShortcut = this.CreateOrLouadFirstEntityPropertyShortcut(new UnitOfWork<TrainingISModel>());
            
            return entitypropertyshortcut;
        }
    }
}

