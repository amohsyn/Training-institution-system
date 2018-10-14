using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using AutoFixture;
using TrainingIS.BLL;
using TrainingIS.DAL;
using System.ComponentModel.DataAnnotations;
using GApp.WebApp.Manager.Views;
using GApp.DAL;
using GApp.Entities;
using GApp.Core.Context;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;
using GApp.UnitTest.TestData;
using GApp.UnitTest.TestData.Enums;

namespace TestData
{
    public class BaseEntityPropertyShortcutTestDataFactory : ITestDataFactory<EntityPropertyShortcut>
    {
        private Fixture _Fixture = null;
		protected List<EntityPropertyShortcut> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseEntityPropertyShortcutTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<EntityPropertyShortcut> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<EntityPropertyShortcut> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first EntityPropertyShortcut instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual EntityPropertyShortcut CreateOrLouadFirstEntityPropertyShortcut()
        {
            EntityPropertyShortcutBLO entitypropertyshortcutBLO = new EntityPropertyShortcutBLO(UnitOfWork,GAppContext);
           
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

        public virtual EntityPropertyShortcut CreateValideEntityPropertyShortcutInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
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
        public virtual EntityPropertyShortcut CreateInValideEntityPropertyShortcutInstance()
        {
            EntityPropertyShortcut entitypropertyshortcut = this.CreateValideEntityPropertyShortcutInstance();
             
			// Required   
 
			entitypropertyshortcut.EntityName = null;
 
			entitypropertyshortcut.PropertyName = null;
 
			entitypropertyshortcut.PropertyShortcutName = null;
            //Unique
			var existant_EntityPropertyShortcut = this.CreateOrLouadFirstEntityPropertyShortcut();
			entitypropertyshortcut.Reference = existant_EntityPropertyShortcut.Reference;
 
            return entitypropertyshortcut;
        }


		public virtual EntityPropertyShortcut CreateInValideEntityPropertyShortcutInstance_ForEdit()
        {
            EntityPropertyShortcut entitypropertyshortcut = this.CreateOrLouadFirstEntityPropertyShortcut();
			// Required   
 
			entitypropertyshortcut.EntityName = null;
 
			entitypropertyshortcut.PropertyName = null;
 
			entitypropertyshortcut.PropertyShortcutName = null;
            //Unique
			var existant_EntityPropertyShortcut = this.CreateOrLouadFirstEntityPropertyShortcut();
			entitypropertyshortcut.Reference = existant_EntityPropertyShortcut.Reference;
            return entitypropertyshortcut;
        }
    }

	public partial class EntityPropertyShortcutTestDataFactory : BaseEntityPropertyShortcutTestDataFactory{
	
		public EntityPropertyShortcutTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
