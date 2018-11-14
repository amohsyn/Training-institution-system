//modelType = Default_Former_Export_Model

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
	public partial class BaseDefault_Former_Export_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Former_Export_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Former ConverTo_Former(Default_Former_Export_Model Default_Former_Export_Model)
        {
			Former Former = null;
            if (Default_Former_Export_Model.Id != 0)
            {
                Former = new FormerBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Former_Export_Model.Id);
            }
            else
            {
                Former = new Former();
            } 
			if (!string.IsNullOrEmpty(Default_Former_Export_Model.Photo_Reference))
            {
				if(Default_Former_Export_Model.Photo_Reference == "Delete" && Former.Photo != null)
                {
                    Former.Photo.Old_Reference = Former.Photo.Reference;
                    Former.Photo.Reference = "Delete";
                }
                else
				{
					if (Former.Photo == null) Former.Photo = new GPicture() { Old_Reference = "Empty" };
					if (Former.Photo.Reference != Default_Former_Export_Model.Photo_Reference)
					{
						// Save the old reference to be deleted by the save methode 
						if (!string.IsNullOrEmpty(Former.Photo.Reference))
                            Former.Photo.Old_Reference = Former.Photo.Reference;

						 

						GPictureBLO gPictureBLO = new GPictureBLO(this.UnitOfWork, this.GAppContext);
						Former.Photo.Reference = Default_Former_Export_Model.Photo_Reference;
                  
						Former.Photo.Original_Thumbnail = gPictureBLO.Get_URL_Original_Picture_Path(Default_Former_Export_Model.Photo_Reference);
						Former.Photo.Small_Thumbnail = gPictureBLO.Get_URL_Small_Picture_Path(Default_Former_Export_Model.Photo_Reference);
						Former.Photo.Medium_Thumbnail = gPictureBLO.Get_URL_Medium_Picture_Path(Default_Former_Export_Model.Photo_Reference);
						Former.Photo.Large_Thumbnail = gPictureBLO.Get_URL_Large_Picture_Path(Default_Former_Export_Model.Photo_Reference);
					}
				}

               
            }
					Former.FormerSpecialty = Default_Former_Export_Model.FormerSpecialty;
			Former.WeeklyHourlyMass = Default_Former_Export_Model.WeeklyHourlyMass;
			Former.RegistrationNumber = Default_Former_Export_Model.RegistrationNumber;
			Former.CreateUserAccount = Default_Former_Export_Model.CreateUserAccount;
			Former.Login = Default_Former_Export_Model.Login;
			Former.Password = Default_Former_Export_Model.Password;
			Former.FirstName = Default_Former_Export_Model.FirstName;
			Former.LastName = Default_Former_Export_Model.LastName;
			Former.FirstNameArabe = Default_Former_Export_Model.FirstNameArabe;
			Former.LastNameArabe = Default_Former_Export_Model.LastNameArabe;
			Former.Sex = Default_Former_Export_Model.Sex;
			Former.Birthdate = DefaultDateTime_If_Empty(Default_Former_Export_Model.Birthdate);
			Former.Nationality = Default_Former_Export_Model.Nationality;
			Former.BirthPlace = Default_Former_Export_Model.BirthPlace;
			Former.CIN = Default_Former_Export_Model.CIN;
			Former.Cellphone = Default_Former_Export_Model.Cellphone;
			Former.Email = Default_Former_Export_Model.Email;
			Former.Address = Default_Former_Export_Model.Address;
			Former.FaceBook = Default_Former_Export_Model.FaceBook;
			Former.WebSite = Default_Former_Export_Model.WebSite;
			Former.Id = Default_Former_Export_Model.Id;
            return Former;
        }
        public virtual Default_Former_Export_Model ConverTo_Default_Former_Export_Model(Former Former)
        {  
			Default_Former_Export_Model Default_Former_Export_Model = new Default_Former_Export_Model();
			Default_Former_Export_Model.toStringValue = Former.ToString();
			Default_Former_Export_Model.Photo = Former.Photo;
			Default_Former_Export_Model.FormerSpecialty = Former.FormerSpecialty;
			Default_Former_Export_Model.WeeklyHourlyMass = Former.WeeklyHourlyMass;
			Default_Former_Export_Model.RegistrationNumber = Former.RegistrationNumber;
			Default_Former_Export_Model.CreateUserAccount = Former.CreateUserAccount;
			Default_Former_Export_Model.Login = Former.Login;
			Default_Former_Export_Model.Password = Former.Password;
			Default_Former_Export_Model.FirstName = Former.FirstName;
			Default_Former_Export_Model.LastName = Former.LastName;
			Default_Former_Export_Model.FirstNameArabe = Former.FirstNameArabe;
			Default_Former_Export_Model.LastNameArabe = Former.LastNameArabe;
			Default_Former_Export_Model.Sex = Former.Sex;
			Default_Former_Export_Model.Birthdate = DefaultDateTime_If_Empty(Former.Birthdate);
			Default_Former_Export_Model.Nationality = Former.Nationality;
			Default_Former_Export_Model.BirthPlace = Former.BirthPlace;
			Default_Former_Export_Model.CIN = Former.CIN;
			Default_Former_Export_Model.Cellphone = Former.Cellphone;
			Default_Former_Export_Model.Email = Former.Email;
			Default_Former_Export_Model.Address = Former.Address;
			Default_Former_Export_Model.FaceBook = Former.FaceBook;
			Default_Former_Export_Model.WebSite = Former.WebSite;
			Default_Former_Export_Model.Id = Former.Id;
            return Default_Former_Export_Model;            
        }

		public virtual Default_Former_Export_Model CreateNew()
        {
            Former Former = new FormerBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Former_Export_Model Default_Former_Export_Model = this.ConverTo_Default_Former_Export_Model(Former);
            return Default_Former_Export_Model;
        } 

		public virtual List<Default_Former_Export_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            FormerBLO entityBLO = new FormerBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Former> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Former_Export_Model> ls_models = new List<Default_Former_Export_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Former_Export_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Former_Export_ModelBLM : BaseDefault_Former_Export_Model_BLM
	{
		public Default_Former_Export_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
