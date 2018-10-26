//modelType = FormerFormView

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
using TrainingIS.Entities.ModelsViews.FormerModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseFormerFormViewBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseFormerFormViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Former ConverTo_Former(FormerFormView FormerFormView)
        {
			Former Former = null;
            if (FormerFormView.Id != 0)
            {
                Former = new FormerBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(FormerFormView.Id);
            }
            else
            {
                Former = new Former();
            } 
			Former.RegistrationNumber = FormerFormView.RegistrationNumber;
			Former.FirstName = FormerFormView.FirstName;
			Former.LastName = FormerFormView.LastName;
			if (!string.IsNullOrEmpty(FormerFormView.Photo_Reference))
            {
				if(FormerFormView.Photo_Reference == "Delete" && Former.Photo != null)
                {
                    Former.Photo.Old_Reference = Former.Photo.Reference;
                    Former.Photo.Reference = "Delete";
                }
                else
				{
					if (Former.Photo == null) Former.Photo = new GPicture() { Old_Reference = "Empty" };
					if (Former.Photo.Reference != FormerFormView.Photo_Reference)
					{
						// Save the old reference to be deleted by the save methode 
						if (!string.IsNullOrEmpty(Former.Photo.Reference))
                            Former.Photo.Old_Reference = Former.Photo.Reference;

						 

						GPictureBLO gPictureBLO = new GPictureBLO(this.UnitOfWork, this.GAppContext);
						Former.Photo.Reference = FormerFormView.Photo_Reference;
                  
						Former.Photo.Original_Thumbnail = gPictureBLO.Get_URL_Original_Picture_Path(FormerFormView.Photo_Reference);
						Former.Photo.Small_Thumbnail = gPictureBLO.Get_URL_Small_Picture_Path(FormerFormView.Photo_Reference);
						Former.Photo.Medium_Thumbnail = gPictureBLO.Get_URL_Medium_Picture_Path(FormerFormView.Photo_Reference);
						Former.Photo.Large_Thumbnail = gPictureBLO.Get_URL_Large_Picture_Path(FormerFormView.Photo_Reference);
					}
				}

               
            }
					Former.FormerSpecialtyId = FormerFormView.FormerSpecialtyId;
			Former.FormerSpecialty = new FormerSpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(FormerFormView.FormerSpecialtyId)) ;
			Former.WeeklyHourlyMass = FormerFormView.WeeklyHourlyMass;
			Former.FirstNameArabe = FormerFormView.FirstNameArabe;
			Former.LastNameArabe = FormerFormView.LastNameArabe;
			Former.NationalityId = FormerFormView.NationalityId;
			Former.Nationality = new NationalityBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(FormerFormView.NationalityId)) ;
			Former.Sex = FormerFormView.Sex;
			Former.Birthdate = DefaultDateTime_If_Empty(FormerFormView.Birthdate);
			Former.BirthPlace = FormerFormView.BirthPlace;
			Former.CIN = FormerFormView.CIN;
			Former.Cellphone = FormerFormView.Cellphone;
			Former.Email = FormerFormView.Email;
			Former.Address = FormerFormView.Address;
			Former.CreateUserAccount = FormerFormView.CreateUserAccount;
			Former.Login = FormerFormView.Login;
			Former.Password = FormerFormView.Password;
			Former.Id = FormerFormView.Id;
            return Former;
        }
        public virtual FormerFormView ConverTo_FormerFormView(Former Former)
        {  
			FormerFormView FormerFormView = new FormerFormView();
			FormerFormView.toStringValue = Former.ToString();
			FormerFormView.Photo = Former.Photo;
			FormerFormView.FormerSpecialtyId = Former.FormerSpecialtyId;
			FormerFormView.WeeklyHourlyMass = Former.WeeklyHourlyMass;
			FormerFormView.RegistrationNumber = Former.RegistrationNumber;
			FormerFormView.CreateUserAccount = Former.CreateUserAccount;
			FormerFormView.Login = Former.Login;
			FormerFormView.Password = Former.Password;
			FormerFormView.FirstName = Former.FirstName;
			FormerFormView.LastName = Former.LastName;
			FormerFormView.FirstNameArabe = Former.FirstNameArabe;
			FormerFormView.LastNameArabe = Former.LastNameArabe;
			FormerFormView.Sex = Former.Sex;
			FormerFormView.Birthdate = DefaultDateTime_If_Empty(Former.Birthdate);
			FormerFormView.NationalityId = Former.NationalityId;
			FormerFormView.BirthPlace = Former.BirthPlace;
			FormerFormView.CIN = Former.CIN;
			FormerFormView.Cellphone = Former.Cellphone;
			FormerFormView.Email = Former.Email;
			FormerFormView.Address = Former.Address;
			FormerFormView.Id = Former.Id;
            return FormerFormView;            
        }

		public virtual FormerFormView CreateNew()
        {
            Former Former = new FormerBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            FormerFormView FormerFormView = this.ConverTo_FormerFormView(Former);
            return FormerFormView;
        } 

		public virtual List<FormerFormView> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            FormerBLO entityBLO = new FormerBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Former> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<FormerFormView> ls_models = new List<FormerFormView>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_FormerFormView(entity));
            }
            return ls_models;
        }


    }

	public partial class FormerFormViewBLM : BaseFormerFormViewBLM
	{
		public FormerFormViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
