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
    public class BaseFormerSpecialtyTestDataFactory : ITestDataFactory<FormerSpecialty>
    {
        private Fixture _Fixture = null;
		protected List<FormerSpecialty> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseFormerSpecialtyTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<FormerSpecialty> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<FormerSpecialty> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first FormerSpecialty instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual FormerSpecialty CreateOrLouadFirstFormerSpecialty()
        {
            FormerSpecialtyBLO formerspecialtyBLO = new FormerSpecialtyBLO(UnitOfWork,GAppContext);
           
			FormerSpecialty entity = null;
            if (formerspecialtyBLO.FindAll()?.Count > 0)
                entity = formerspecialtyBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp FormerSpecialty for Test
                entity = this.CreateValideFormerSpecialtyInstance();
                formerspecialtyBLO.Save(entity);
            }
            return entity;
        }

        public virtual FormerSpecialty CreateValideFormerSpecialtyInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
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
        public virtual FormerSpecialty CreateInValideFormerSpecialtyInstance()
        {
            FormerSpecialty formerspecialty = this.CreateValideFormerSpecialtyInstance();
             
			// Required   
 
			formerspecialty.Code = null;
 
			formerspecialty.Name = null;
            //Unique
			var existant_FormerSpecialty = this.CreateOrLouadFirstFormerSpecialty();
			formerspecialty.Reference = existant_FormerSpecialty.Reference;
 
            return formerspecialty;
        }


		public virtual FormerSpecialty CreateInValideFormerSpecialtyInstance_ForEdit()
        {
            FormerSpecialty formerspecialty = this.CreateOrLouadFirstFormerSpecialty();
			// Required   
 
			formerspecialty.Code = null;
 
			formerspecialty.Name = null;
            //Unique
			var existant_FormerSpecialty = this.CreateOrLouadFirstFormerSpecialty();
			formerspecialty.Reference = existant_FormerSpecialty.Reference;
            return formerspecialty;
        }
    }

	public partial class FormerSpecialtyTestDataFactory : BaseFormerSpecialtyTestDataFactory{
	
		public FormerSpecialtyTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
