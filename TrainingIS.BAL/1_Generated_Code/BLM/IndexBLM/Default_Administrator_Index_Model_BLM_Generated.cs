//modelType = Default_Administrator_Index_Model

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
	public partial class BaseDefault_Administrator_Index_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Administrator_Index_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Administrator ConverTo_Administrator(Default_Administrator_Index_Model Default_Administrator_Index_Model)
        {
			Administrator Administrator = null;
            if (Default_Administrator_Index_Model.Id != 0)
            {
                Administrator = new AdministratorBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Administrator_Index_Model.Id);
            }
            else
            {
                Administrator = new Administrator();
            } 
			if (!string.IsNullOrEmpty(Default_Administrator_Index_Model.Photo_Reference))
            {
				if(Default_Administrator_Index_Model.Photo_Reference == "Delete" && Administrator.Photo != null)
                {
                    Administrator.Photo.Old_Reference = Administrator.Photo.Reference;
                    Administrator.Photo.Reference = "Delete";
                }
                else
				{
					if (Administrator.Photo == null) Administrator.Photo = new GPicture() { Old_Reference = "Empty" };
					if (Administrator.Photo.Reference != Default_Administrator_Index_Model.Photo_Reference)
					{
						// Save the old reference to be deleted by the save methode 
						if (!string.IsNullOrEmpty(Administrator.Photo.Reference))
                            Administrator.Photo.Old_Reference = Administrator.Photo.Reference;

						 

						GPictureBLO gPictureBLO = new GPictureBLO(this.UnitOfWork, this.GAppContext);
						Administrator.Photo.Reference = Default_Administrator_Index_Model.Photo_Reference;
                  
						Administrator.Photo.Original_Thumbnail = gPictureBLO.Get_URL_Original_Picture_Path(Default_Administrator_Index_Model.Photo_Reference);
						Administrator.Photo.Small_Thumbnail = gPictureBLO.Get_URL_Small_Picture_Path(Default_Administrator_Index_Model.Photo_Reference);
						Administrator.Photo.Medium_Thumbnail = gPictureBLO.Get_URL_Medium_Picture_Path(Default_Administrator_Index_Model.Photo_Reference);
						Administrator.Photo.Large_Thumbnail = gPictureBLO.Get_URL_Large_Picture_Path(Default_Administrator_Index_Model.Photo_Reference);
					}
				}

               
            }
					Administrator.RegistrationNumber = Default_Administrator_Index_Model.RegistrationNumber;
			Administrator.CreateUserAccount = Default_Administrator_Index_Model.CreateUserAccount;
			Administrator.Login = Default_Administrator_Index_Model.Login;
			Administrator.Password = Default_Administrator_Index_Model.Password;
			Administrator.FirstName = Default_Administrator_Index_Model.FirstName;
			Administrator.LastName = Default_Administrator_Index_Model.LastName;
			Administrator.FirstNameArabe = Default_Administrator_Index_Model.FirstNameArabe;
			Administrator.LastNameArabe = Default_Administrator_Index_Model.LastNameArabe;
			Administrator.Sex = Default_Administrator_Index_Model.Sex;
			Administrator.Birthdate = DefaultDateTime_If_Empty(Default_Administrator_Index_Model.Birthdate);
			Administrator.Nationality = Default_Administrator_Index_Model.Nationality;
			Administrator.BirthPlace = Default_Administrator_Index_Model.BirthPlace;
			Administrator.CIN = Default_Administrator_Index_Model.CIN;
			Administrator.Cellphone = Default_Administrator_Index_Model.Cellphone;
			Administrator.Email = Default_Administrator_Index_Model.Email;
			Administrator.Address = Default_Administrator_Index_Model.Address;
			Administrator.FaceBook = Default_Administrator_Index_Model.FaceBook;
			Administrator.WebSite = Default_Administrator_Index_Model.WebSite;
			Administrator.Id = Default_Administrator_Index_Model.Id;
            return Administrator;
        }
        public virtual Default_Administrator_Index_Model ConverTo_Default_Administrator_Index_Model(Administrator Administrator)
        {  
			Default_Administrator_Index_Model Default_Administrator_Index_Model = new Default_Administrator_Index_Model();
			Default_Administrator_Index_Model.toStringValue = Administrator.ToString();
			Default_Administrator_Index_Model.Photo = Administrator.Photo;
			Default_Administrator_Index_Model.RegistrationNumber = Administrator.RegistrationNumber;
			Default_Administrator_Index_Model.CreateUserAccount = Administrator.CreateUserAccount;
			Default_Administrator_Index_Model.Login = Administrator.Login;
			Default_Administrator_Index_Model.Password = Administrator.Password;
			Default_Administrator_Index_Model.FirstName = Administrator.FirstName;
			Default_Administrator_Index_Model.LastName = Administrator.LastName;
			Default_Administrator_Index_Model.FirstNameArabe = Administrator.FirstNameArabe;
			Default_Administrator_Index_Model.LastNameArabe = Administrator.LastNameArabe;
			Default_Administrator_Index_Model.Sex = Administrator.Sex;
			Default_Administrator_Index_Model.Birthdate = DefaultDateTime_If_Empty(Administrator.Birthdate);
			Default_Administrator_Index_Model.Nationality = Administrator.Nationality;
			Default_Administrator_Index_Model.BirthPlace = Administrator.BirthPlace;
			Default_Administrator_Index_Model.CIN = Administrator.CIN;
			Default_Administrator_Index_Model.Cellphone = Administrator.Cellphone;
			Default_Administrator_Index_Model.Email = Administrator.Email;
			Default_Administrator_Index_Model.Address = Administrator.Address;
			Default_Administrator_Index_Model.FaceBook = Administrator.FaceBook;
			Default_Administrator_Index_Model.WebSite = Administrator.WebSite;
			Default_Administrator_Index_Model.Id = Administrator.Id;
            return Default_Administrator_Index_Model;            
        }

		public virtual Default_Administrator_Index_Model CreateNew()
        {
            Administrator Administrator = new AdministratorBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Administrator_Index_Model Default_Administrator_Index_Model = this.ConverTo_Default_Administrator_Index_Model(Administrator);
            return Default_Administrator_Index_Model;
        } 

		public virtual List<Default_Administrator_Index_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            AdministratorBLO entityBLO = new AdministratorBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Administrator> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Administrator_Index_Model> ls_models = new List<Default_Administrator_Index_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Administrator_Index_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Administrator_Index_ModelBLM : BaseDefault_Administrator_Index_Model_BLM
	{
		public Default_Administrator_Index_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
