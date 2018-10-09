//modelType = Default_Details_Trainee_Model

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
	public partial class BaseDefault_Details_Trainee_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Trainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Trainee ConverTo_Trainee(Default_Details_Trainee_Model Default_Details_Trainee_Model)
        {
			Trainee Trainee = null;
            if (Default_Details_Trainee_Model.Id != 0)
            {
                Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Trainee_Model.Id);
            }
            else
            {
                Trainee = new Trainee();
            } 
			Trainee.CNE = Default_Details_Trainee_Model.CNE;
			Trainee.DateRegistration = Default_Details_Trainee_Model.DateRegistration;
			Trainee.isActif = Default_Details_Trainee_Model.isActif;
			Trainee.Schoollevel = Default_Details_Trainee_Model.Schoollevel;
			Trainee.Specialty = Default_Details_Trainee_Model.Specialty;
			Trainee.YearStudy = Default_Details_Trainee_Model.YearStudy;
			Trainee.Group = Default_Details_Trainee_Model.Group;
			Trainee.FirstName = Default_Details_Trainee_Model.FirstName;
			Trainee.LastName = Default_Details_Trainee_Model.LastName;
			Trainee.FirstNameArabe = Default_Details_Trainee_Model.FirstNameArabe;
			Trainee.LastNameArabe = Default_Details_Trainee_Model.LastNameArabe;
			Trainee.Sex = Default_Details_Trainee_Model.Sex;
			Trainee.Birthdate = DefaultDateTime_If_Empty(Default_Details_Trainee_Model.Birthdate);
			Trainee.Nationality = Default_Details_Trainee_Model.Nationality;
			Trainee.BirthPlace = Default_Details_Trainee_Model.BirthPlace;
			Trainee.CIN = Default_Details_Trainee_Model.CIN;
			if (!string.IsNullOrEmpty(Default_Details_Trainee_Model.Photo_Reference))
            {
				if(Default_Details_Trainee_Model.Photo_Reference == "Delete" && Trainee.Photo != null)
                {
                    Trainee.Photo.Old_Reference = Trainee.Photo.Reference;
                    Trainee.Photo.Reference = "Delete";
                }
                else
				{
					if (Trainee.Photo == null) Trainee.Photo = new GPicture();
					if (Trainee.Photo.Reference != Default_Details_Trainee_Model.Photo_Reference)
					{
						// Save the old reference to be deleted by the save methode 
						Trainee.Photo.Old_Reference = Trainee.Photo.Reference;

						GPictureBLO gPictureBLO = new GPictureBLO(this.GAppContext);
						Trainee.Photo.Reference = Default_Details_Trainee_Model.Photo_Reference;
                  
						Trainee.Photo.Original_Thumbnail = gPictureBLO.Get_URL_Original_Picture_Path(Default_Details_Trainee_Model.Photo_Reference);
						Trainee.Photo.Small_Thumbnail = gPictureBLO.Get_URL_Small_Picture_Path(Default_Details_Trainee_Model.Photo_Reference);
						Trainee.Photo.Medium_Thumbnail = gPictureBLO.Get_URL_Medium_Picture_Path(Default_Details_Trainee_Model.Photo_Reference);
						Trainee.Photo.Large_Thumbnail = gPictureBLO.Get_URL_Large_Picture_Path(Default_Details_Trainee_Model.Photo_Reference);
					}
				}

               
            }
					Trainee.Cellphone = Default_Details_Trainee_Model.Cellphone;
			Trainee.Email = Default_Details_Trainee_Model.Email;
			Trainee.Address = Default_Details_Trainee_Model.Address;
			Trainee.FaceBook = Default_Details_Trainee_Model.FaceBook;
			Trainee.WebSite = Default_Details_Trainee_Model.WebSite;
			Trainee.Id = Default_Details_Trainee_Model.Id;
            return Trainee;
        }
        public virtual Default_Details_Trainee_Model ConverTo_Default_Details_Trainee_Model(Trainee Trainee)
        {  
			Default_Details_Trainee_Model Default_Details_Trainee_Model = new Default_Details_Trainee_Model();
			Default_Details_Trainee_Model.toStringValue = Trainee.ToString();
			Default_Details_Trainee_Model.CNE = Trainee.CNE;
			Default_Details_Trainee_Model.DateRegistration = ConversionUtil.DefaultValue_if_Null<DateTime>(Trainee.DateRegistration);
			Default_Details_Trainee_Model.isActif = Trainee.isActif;
			Default_Details_Trainee_Model.Schoollevel = Trainee.Schoollevel;
			Default_Details_Trainee_Model.Specialty = Trainee.Specialty;
			Default_Details_Trainee_Model.YearStudy = Trainee.YearStudy;
			Default_Details_Trainee_Model.Group = Trainee.Group;
			Default_Details_Trainee_Model.FirstName = Trainee.FirstName;
			Default_Details_Trainee_Model.LastName = Trainee.LastName;
			Default_Details_Trainee_Model.FirstNameArabe = Trainee.FirstNameArabe;
			Default_Details_Trainee_Model.LastNameArabe = Trainee.LastNameArabe;
			Default_Details_Trainee_Model.Sex = Trainee.Sex;
			Default_Details_Trainee_Model.Birthdate = DefaultDateTime_If_Empty(Trainee.Birthdate);
			Default_Details_Trainee_Model.Nationality = Trainee.Nationality;
			Default_Details_Trainee_Model.BirthPlace = Trainee.BirthPlace;
			Default_Details_Trainee_Model.CIN = Trainee.CIN;
			Default_Details_Trainee_Model.Photo = Trainee.Photo;
			Default_Details_Trainee_Model.Cellphone = Trainee.Cellphone;
			Default_Details_Trainee_Model.Email = Trainee.Email;
			Default_Details_Trainee_Model.Address = Trainee.Address;
			Default_Details_Trainee_Model.FaceBook = Trainee.FaceBook;
			Default_Details_Trainee_Model.WebSite = Trainee.WebSite;
			Default_Details_Trainee_Model.Id = Trainee.Id;
            return Default_Details_Trainee_Model;            
        }

		public virtual Default_Details_Trainee_Model CreateNew()
        {
            Trainee Trainee = new TraineeBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Trainee_Model Default_Details_Trainee_Model = this.ConverTo_Default_Details_Trainee_Model(Trainee);
            return Default_Details_Trainee_Model;
        } 

		public virtual List<Default_Details_Trainee_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            TraineeBLO entityBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Trainee> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Trainee_Model> ls_models = new List<Default_Details_Trainee_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Trainee_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Trainee_ModelBLM : BaseDefault_Details_Trainee_ModelBLM
	{
		public Default_Details_Trainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
