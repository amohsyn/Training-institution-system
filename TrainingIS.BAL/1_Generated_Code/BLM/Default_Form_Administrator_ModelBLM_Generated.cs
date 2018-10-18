//modelType = Default_Form_Administrator_Model

using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.DAL;
using GApp.Core.Context;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_Administrator_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Administrator_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Administrator ConverTo_Administrator(Default_Form_Administrator_Model Default_Form_Administrator_Model)
        {
			Administrator Administrator = null;
            if (Default_Form_Administrator_Model.Id != 0)
            {
                Administrator = new AdministratorBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Administrator_Model.Id);
            }
            else
            {
                Administrator = new Administrator();
            } 
			Administrator.RegistrationNumber = Default_Form_Administrator_Model.RegistrationNumber;
			Administrator.CreateUserAccount = Default_Form_Administrator_Model.CreateUserAccount;
			Administrator.Login = Default_Form_Administrator_Model.Login;
			Administrator.Password = Default_Form_Administrator_Model.Password;
			Administrator.FirstName = Default_Form_Administrator_Model.FirstName;
			Administrator.LastName = Default_Form_Administrator_Model.LastName;
			Administrator.FirstNameArabe = Default_Form_Administrator_Model.FirstNameArabe;
			Administrator.LastNameArabe = Default_Form_Administrator_Model.LastNameArabe;
			Administrator.Sex = Default_Form_Administrator_Model.Sex;
			Administrator.Birthdate = DefaultDateTime_If_Empty(Default_Form_Administrator_Model.Birthdate);
			Administrator.NationalityId = Default_Form_Administrator_Model.NationalityId;
			Administrator.Nationality = new NationalityBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Administrator_Model.NationalityId)) ;
			Administrator.BirthPlace = Default_Form_Administrator_Model.BirthPlace;
			Administrator.CIN = Default_Form_Administrator_Model.CIN;
			if (!string.IsNullOrEmpty(Default_Form_Administrator_Model.Photo_Reference))
            {
				if(Default_Form_Administrator_Model.Photo_Reference == "Delete" && Administrator.Photo != null)
                {
                    Administrator.Photo.Old_Reference = Administrator.Photo.Reference;
                    Administrator.Photo.Reference = "Delete";
                }
                else
				{
					if (Administrator.Photo == null) Administrator.Photo = new GPicture() { Old_Reference = "Empty" };
					if (Administrator.Photo.Reference != Default_Form_Administrator_Model.Photo_Reference)
					{
						// Save the old reference to be deleted by the save methode 
						if (!string.IsNullOrEmpty(Administrator.Photo.Reference))
                            Administrator.Photo.Old_Reference = Administrator.Photo.Reference;

						 

						GPictureBLO gPictureBLO = new GPictureBLO(this.GAppContext);
						Administrator.Photo.Reference = Default_Form_Administrator_Model.Photo_Reference;
                  
						Administrator.Photo.Original_Thumbnail = gPictureBLO.Get_URL_Original_Picture_Path(Default_Form_Administrator_Model.Photo_Reference);
						Administrator.Photo.Small_Thumbnail = gPictureBLO.Get_URL_Small_Picture_Path(Default_Form_Administrator_Model.Photo_Reference);
						Administrator.Photo.Medium_Thumbnail = gPictureBLO.Get_URL_Medium_Picture_Path(Default_Form_Administrator_Model.Photo_Reference);
						Administrator.Photo.Large_Thumbnail = gPictureBLO.Get_URL_Large_Picture_Path(Default_Form_Administrator_Model.Photo_Reference);
					}
				}

               
            }
					Administrator.Cellphone = Default_Form_Administrator_Model.Cellphone;
			Administrator.Email = Default_Form_Administrator_Model.Email;
			Administrator.Address = Default_Form_Administrator_Model.Address;
			Administrator.FaceBook = Default_Form_Administrator_Model.FaceBook;
			Administrator.WebSite = Default_Form_Administrator_Model.WebSite;
			Administrator.Id = Default_Form_Administrator_Model.Id;
            return Administrator;
        }
        public virtual Default_Form_Administrator_Model ConverTo_Default_Form_Administrator_Model(Administrator Administrator)
        {  
			Default_Form_Administrator_Model Default_Form_Administrator_Model = new Default_Form_Administrator_Model();
			Default_Form_Administrator_Model.toStringValue = Administrator.ToString();
			Default_Form_Administrator_Model.RegistrationNumber = Administrator.RegistrationNumber;
			Default_Form_Administrator_Model.CreateUserAccount = Administrator.CreateUserAccount;
			Default_Form_Administrator_Model.Login = Administrator.Login;
			Default_Form_Administrator_Model.Password = Administrator.Password;
			Default_Form_Administrator_Model.FirstName = Administrator.FirstName;
			Default_Form_Administrator_Model.LastName = Administrator.LastName;
			Default_Form_Administrator_Model.FirstNameArabe = Administrator.FirstNameArabe;
			Default_Form_Administrator_Model.LastNameArabe = Administrator.LastNameArabe;
			Default_Form_Administrator_Model.Sex = Administrator.Sex;
			Default_Form_Administrator_Model.Birthdate = DefaultDateTime_If_Empty(Administrator.Birthdate);
			Default_Form_Administrator_Model.NationalityId = Administrator.NationalityId;
			Default_Form_Administrator_Model.BirthPlace = Administrator.BirthPlace;
			Default_Form_Administrator_Model.CIN = Administrator.CIN;
			Default_Form_Administrator_Model.Photo = Administrator.Photo;
			Default_Form_Administrator_Model.Cellphone = Administrator.Cellphone;
			Default_Form_Administrator_Model.Email = Administrator.Email;
			Default_Form_Administrator_Model.Address = Administrator.Address;
			Default_Form_Administrator_Model.FaceBook = Administrator.FaceBook;
			Default_Form_Administrator_Model.WebSite = Administrator.WebSite;
			Default_Form_Administrator_Model.Id = Administrator.Id;
            return Default_Form_Administrator_Model;            
        }

		public virtual Default_Form_Administrator_Model CreateNew()
        {
            Administrator Administrator = new AdministratorBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Administrator_Model Default_Form_Administrator_Model = this.ConverTo_Default_Form_Administrator_Model(Administrator);
            return Default_Form_Administrator_Model;
        } 

		public virtual List<Default_Form_Administrator_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            AdministratorBLO entityBLO = new AdministratorBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Administrator> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_Administrator_Model> ls_models = new List<Default_Form_Administrator_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_Administrator_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_Administrator_ModelBLM : BaseDefault_Form_Administrator_ModelBLM
	{
		public Default_Form_Administrator_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
