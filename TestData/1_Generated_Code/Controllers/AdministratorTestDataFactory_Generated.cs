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
    public class BaseAdministratorTestDataFactory : EntityTestData<Administrator>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new AdministratorBLO(UnitOfWork, GAppContext);
        }

        public BaseAdministratorTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Administrator> Generate_TestData()
        {
            List<Administrator> Data = base.Generate_TestData();
            if(Data == null) Data = new List<Administrator>();
			Administrator Administrator = this.CreateValideAdministratorInstance();
            Administrator.Reference = "ValideAdministratorInstance";
            Data.Add(Administrator);
            return Data;
        }
	
		/// <summary>
        /// Find the first Administrator instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Administrator CreateOrLouadFirstAdministrator()
        {
            AdministratorBLO administratorBLO = new AdministratorBLO(UnitOfWork,GAppContext);
           
			Administrator entity = null;
            if (administratorBLO.FindAll()?.Count > 0)
                entity = administratorBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Administrator for Test
                entity = this.CreateValideAdministratorInstance();
                administratorBLO.Save(entity);
            }
            return entity;
        }

        public virtual Administrator CreateValideAdministratorInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Administrator  Valide_Administrator = this._Fixture.Create<Administrator>();
            Valide_Administrator.Id = 0;
            // Many to One 
            //   
			// Photo
			var Photo = new GPictureTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstGPicture();
            Valide_Administrator.Photo = Photo;
			           
			// Nationality
			var Nationality = new NationalityTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstNationality();
            Valide_Administrator.Nationality = Nationality;
						 Valide_Administrator.NationalityId = Nationality.Id;
			           
            // One to Many
            //
			Valide_Administrator.Member_To_WorkGroups = null;
            return Valide_Administrator;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Administrator can't exist</returns>
        public virtual Administrator CreateInValideAdministratorInstance()
        {
            Administrator administrator = this.CreateValideAdministratorInstance();
             
			// Required   
 
			administrator.RegistrationNumber = null;
 
			administrator.Login = null;
 
			administrator.Password = null;
 
			administrator.FirstName = null;
 
			administrator.LastName = null;
 
			administrator.Sex = SexEnum.man;
            //Unique
			var existant_Administrator = this.CreateOrLouadFirstAdministrator();
			administrator.RegistrationNumber = existant_Administrator.RegistrationNumber;
			administrator.CIN = existant_Administrator.CIN;
			administrator.Email = existant_Administrator.Email;
			administrator.Reference = existant_Administrator.Reference;
 
            return administrator;
        }


		public virtual Administrator CreateInValideAdministratorInstance_ForEdit()
        {
            Administrator administrator = this.CreateOrLouadFirstAdministrator();
			// Required   
 
			administrator.RegistrationNumber = null;
 
			administrator.Login = null;
 
			administrator.Password = null;
 
			administrator.FirstName = null;
 
			administrator.LastName = null;
 
			administrator.Sex = SexEnum.man;
            //Unique
			var existant_Administrator = this.CreateOrLouadFirstAdministrator();
			administrator.RegistrationNumber = existant_Administrator.RegistrationNumber;
			administrator.CIN = existant_Administrator.CIN;
			administrator.Email = existant_Administrator.Email;
			administrator.Reference = existant_Administrator.Reference;
            return administrator;
        }
    }

	public partial class AdministratorTestDataFactory : BaseAdministratorTestDataFactory{
	
		public AdministratorTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
