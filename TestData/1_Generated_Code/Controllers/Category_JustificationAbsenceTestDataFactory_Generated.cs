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
    public class BaseCategory_JustificationAbsenceTestDataFactory : ITestDataFactory<Category_JustificationAbsence>
    {
        private Fixture _Fixture = null;
		protected List<Category_JustificationAbsence> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseCategory_JustificationAbsenceTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<Category_JustificationAbsence> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<Category_JustificationAbsence> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first Category_JustificationAbsence instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Category_JustificationAbsence CreateOrLouadFirstCategory_JustificationAbsence()
        {
            Category_JustificationAbsenceBLO category_justificationabsenceBLO = new Category_JustificationAbsenceBLO(UnitOfWork,GAppContext);
           
			Category_JustificationAbsence entity = null;
            if (category_justificationabsenceBLO.FindAll()?.Count > 0)
                entity = category_justificationabsenceBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Category_JustificationAbsence for Test
                entity = this.CreateValideCategory_JustificationAbsenceInstance();
                category_justificationabsenceBLO.Save(entity);
            }
            return entity;
        }

        public virtual Category_JustificationAbsence CreateValideCategory_JustificationAbsenceInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Category_JustificationAbsence  Valide_Category_JustificationAbsence = this._Fixture.Create<Category_JustificationAbsence>();
            Valide_Category_JustificationAbsence.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_Category_JustificationAbsence;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Category_JustificationAbsence can't exist</returns>
        public virtual Category_JustificationAbsence CreateInValideCategory_JustificationAbsenceInstance()
        {
            Category_JustificationAbsence category_justificationabsence = this.CreateValideCategory_JustificationAbsenceInstance();
             
			// Required   
 
			category_justificationabsence.Name = null;
            //Unique
			var existant_Category_JustificationAbsence = this.CreateOrLouadFirstCategory_JustificationAbsence();
			category_justificationabsence.Reference = existant_Category_JustificationAbsence.Reference;
 
            return category_justificationabsence;
        }


		public virtual Category_JustificationAbsence CreateInValideCategory_JustificationAbsenceInstance_ForEdit()
        {
            Category_JustificationAbsence category_justificationabsence = this.CreateOrLouadFirstCategory_JustificationAbsence();
			// Required   
 
			category_justificationabsence.Name = null;
            //Unique
			var existant_Category_JustificationAbsence = this.CreateOrLouadFirstCategory_JustificationAbsence();
			category_justificationabsence.Reference = existant_Category_JustificationAbsence.Reference;
            return category_justificationabsence;
        }
    }

	public partial class Category_JustificationAbsenceTestDataFactory : BaseCategory_JustificationAbsenceTestDataFactory{
	
		public Category_JustificationAbsenceTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
