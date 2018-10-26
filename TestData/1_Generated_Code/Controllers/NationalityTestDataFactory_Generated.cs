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
    public class BaseNationalityTestDataFactory : EntityTestData<Nationality>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new NationalityBLO(UnitOfWork, GAppContext);
        }

        public BaseNationalityTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Nationality> Generate_TestData()
        {
            List<Nationality> Data = base.Generate_TestData();
            if(Data == null) Data = new List<Nationality>();
			Nationality Nationality = this.CreateValideNationalityInstance();
            Nationality.Reference = "ValideNationalityInstance";
            Data.Add(Nationality);
            return Data;
        }
	
		/// <summary>
        /// Find the first Nationality instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Nationality CreateOrLouadFirstNationality()
        {
            NationalityBLO nationalityBLO = new NationalityBLO(UnitOfWork,GAppContext);
           
			Nationality entity = null;
            if (nationalityBLO.FindAll()?.Count > 0)
                entity = nationalityBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Nationality for Test
                entity = this.CreateValideNationalityInstance();
                nationalityBLO.Save(entity);
            }
            return entity;
        }

        public virtual Nationality CreateValideNationalityInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Nationality  Valide_Nationality = this._Fixture.Create<Nationality>();
            Valide_Nationality.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_Nationality;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Nationality can't exist</returns>
        public virtual Nationality CreateInValideNationalityInstance()
        {
            Nationality nationality = this.CreateValideNationalityInstance();
             
			// Required   
 
			nationality.Code = null;
 
			nationality.Name = null;
            //Unique
			var existant_Nationality = this.CreateOrLouadFirstNationality();
			nationality.Code = existant_Nationality.Code;
			nationality.Reference = existant_Nationality.Reference;
 
            return nationality;
        }


		public virtual Nationality CreateInValideNationalityInstance_ForEdit()
        {
            Nationality nationality = this.CreateOrLouadFirstNationality();
			// Required   
 
			nationality.Code = null;
 
			nationality.Name = null;
            //Unique
			var existant_Nationality = this.CreateOrLouadFirstNationality();
			nationality.Code = existant_Nationality.Code;
			nationality.Reference = existant_Nationality.Reference;
            return nationality;
        }
    }

	public partial class NationalityTestDataFactory : BaseNationalityTestDataFactory{
	
		public NationalityTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
