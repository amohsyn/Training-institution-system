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
using TrainingIS.Entities.ModelsViews.FormerModelsViews;
using TrainingIS.BLL.ModelsViews;
using GApp.UnitTest.TestData;
using GApp.UnitTest.TestData.Enums;

namespace TestData
{
    public class BaseFormerTestDataFactory : ITestDataFactory<Former>
    {
        private Fixture _Fixture = null;
		protected List<Former> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseFormerTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<Former> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<Former> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first Former instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Former CreateOrLouadFirstFormer()
        {
            FormerBLO formerBLO = new FormerBLO(UnitOfWork,GAppContext);
           
			Former entity = null;
            if (formerBLO.FindAll()?.Count > 0)
                entity = formerBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Former for Test
                entity = this.CreateValideFormerInstance();
                formerBLO.Save(entity);
            }
            return entity;
        }

        public virtual Former CreateValideFormerInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Former  Valide_Former = this._Fixture.Create<Former>();
            Valide_Former.Id = 0;
            // Many to One 
            //
			// FormerSpecialty
			var FormerSpecialty = new FormerSpecialtyTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstFormerSpecialty();
            Valide_Former.FormerSpecialty = null;
            Valide_Former.FormerSpecialtyId = FormerSpecialty.Id;
			// Nationality
			var Nationality = new NationalityTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstNationality();
            Valide_Former.Nationality = null;
            Valide_Former.NationalityId = Nationality.Id;
			// Photo
			//var Photo = new PhotoTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstPhoto();
   //         Valide_Former.Photo = null;
   //         Valide_Former.PhotoId = Photo.Id;
            // One to Many
            //
            return Valide_Former;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Former can't exist</returns>
        public virtual Former CreateInValideFormerInstance()
        {
            Former former = this.CreateValideFormerInstance();
             
			// Required   
 
			former.RegistrationNumber = null;
 
			former.FormerSpecialtyId = 0;
 
			former.Login = null;
 
			former.Password = null;
 
			former.FirstName = null;
 
			former.LastName = null;
 
			former.Sex = SexEnum.man;
            //Unique
			var existant_Former = this.CreateOrLouadFirstFormer();
			former.RegistrationNumber = existant_Former.RegistrationNumber;
			former.CIN = existant_Former.CIN;
			former.Email = existant_Former.Email;
			former.Reference = existant_Former.Reference;
 
            return former;
        }


		public virtual Former CreateInValideFormerInstance_ForEdit()
        {
            Former former = this.CreateOrLouadFirstFormer();
			// Required   
 
			former.RegistrationNumber = null;
 
			former.FormerSpecialtyId = 0;
 
			former.Login = null;
 
			former.Password = null;
 
			former.FirstName = null;
 
			former.LastName = null;
 
			former.Sex = SexEnum.man;
            //Unique
			var existant_Former = this.CreateOrLouadFirstFormer();
			former.RegistrationNumber = existant_Former.RegistrationNumber;
			former.CIN = existant_Former.CIN;
			former.Email = existant_Former.Email;
			former.Reference = existant_Former.Reference;
            return former;
        }
    }

	public partial class FormerTestDataFactory : BaseFormerTestDataFactory{
	
		public FormerTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
