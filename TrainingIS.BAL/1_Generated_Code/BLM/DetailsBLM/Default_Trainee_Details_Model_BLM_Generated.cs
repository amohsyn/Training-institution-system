//modelType = Default_Trainee_Details_Model

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
	public partial class BaseDefault_Trainee_Details_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Trainee_Details_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Trainee ConverTo_Trainee(Default_Trainee_Details_Model Default_Trainee_Details_Model)
        {
			Trainee Trainee = null;
            if (Default_Trainee_Details_Model.Id != 0)
            {
                Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Trainee_Details_Model.Id);
            }
            else
            {
                Trainee = new Trainee();
            } 
			if (!string.IsNullOrEmpty(Default_Trainee_Details_Model.Photo_Reference))
            {
				if(Default_Trainee_Details_Model.Photo_Reference == "Delete" && Trainee.Photo != null)
                {
                    Trainee.Photo.Old_Reference = Trainee.Photo.Reference;
                    Trainee.Photo.Reference = "Delete";
                }
                else
				{
					if (Trainee.Photo == null) Trainee.Photo = new GPicture() { Old_Reference = "Empty" };
					if (Trainee.Photo.Reference != Default_Trainee_Details_Model.Photo_Reference)
					{
						// Save the old reference to be deleted by the save methode 
						if (!string.IsNullOrEmpty(Trainee.Photo.Reference))
                            Trainee.Photo.Old_Reference = Trainee.Photo.Reference;

						 

						GPictureBLO gPictureBLO = new GPictureBLO(this.UnitOfWork, this.GAppContext);
						Trainee.Photo.Reference = Default_Trainee_Details_Model.Photo_Reference;
                  
						Trainee.Photo.Original_Thumbnail = gPictureBLO.Get_URL_Original_Picture_Path(Default_Trainee_Details_Model.Photo_Reference);
						Trainee.Photo.Small_Thumbnail = gPictureBLO.Get_URL_Small_Picture_Path(Default_Trainee_Details_Model.Photo_Reference);
						Trainee.Photo.Medium_Thumbnail = gPictureBLO.Get_URL_Medium_Picture_Path(Default_Trainee_Details_Model.Photo_Reference);
						Trainee.Photo.Large_Thumbnail = gPictureBLO.Get_URL_Large_Picture_Path(Default_Trainee_Details_Model.Photo_Reference);
					}
				}

               
            }
					Trainee.CNE = Default_Trainee_Details_Model.CNE;
			Trainee.DateRegistration = Default_Trainee_Details_Model.DateRegistration;
			Trainee.isActif = Default_Trainee_Details_Model.isActif;
			Trainee.Schoollevel = Default_Trainee_Details_Model.Schoollevel;
			Trainee.Specialty = Default_Trainee_Details_Model.Specialty;
			Trainee.YearStudy = Default_Trainee_Details_Model.YearStudy;
			Trainee.Group = Default_Trainee_Details_Model.Group;
			Trainee.FirstName = Default_Trainee_Details_Model.FirstName;
			Trainee.LastName = Default_Trainee_Details_Model.LastName;
			Trainee.FirstNameArabe = Default_Trainee_Details_Model.FirstNameArabe;
			Trainee.LastNameArabe = Default_Trainee_Details_Model.LastNameArabe;
			Trainee.Sex = Default_Trainee_Details_Model.Sex;
			Trainee.Birthdate = DefaultDateTime_If_Empty(Default_Trainee_Details_Model.Birthdate);
			Trainee.Nationality = Default_Trainee_Details_Model.Nationality;
			Trainee.BirthPlace = Default_Trainee_Details_Model.BirthPlace;
			Trainee.CIN = Default_Trainee_Details_Model.CIN;
			Trainee.Cellphone = Default_Trainee_Details_Model.Cellphone;
			Trainee.Email = Default_Trainee_Details_Model.Email;
			Trainee.Address = Default_Trainee_Details_Model.Address;
			Trainee.FaceBook = Default_Trainee_Details_Model.FaceBook;
			Trainee.WebSite = Default_Trainee_Details_Model.WebSite;
			Trainee.Id = Default_Trainee_Details_Model.Id;
            return Trainee;
        }
        public virtual Default_Trainee_Details_Model ConverTo_Default_Trainee_Details_Model(Trainee Trainee)
        {  
			Default_Trainee_Details_Model Default_Trainee_Details_Model = new Default_Trainee_Details_Model();
			Default_Trainee_Details_Model.toStringValue = Trainee.ToString();
			Default_Trainee_Details_Model.Photo = Trainee.Photo;
			Default_Trainee_Details_Model.CNE = Trainee.CNE;
			Default_Trainee_Details_Model.DateRegistration = ConversionUtil.DefaultValue_if_Null<DateTime>(Trainee.DateRegistration);
			Default_Trainee_Details_Model.isActif = Trainee.isActif;
			Default_Trainee_Details_Model.Schoollevel = Trainee.Schoollevel;
			Default_Trainee_Details_Model.Specialty = Trainee.Specialty;
			Default_Trainee_Details_Model.YearStudy = Trainee.YearStudy;
			Default_Trainee_Details_Model.Group = Trainee.Group;
			Default_Trainee_Details_Model.FirstName = Trainee.FirstName;
			Default_Trainee_Details_Model.LastName = Trainee.LastName;
			Default_Trainee_Details_Model.FirstNameArabe = Trainee.FirstNameArabe;
			Default_Trainee_Details_Model.LastNameArabe = Trainee.LastNameArabe;
			Default_Trainee_Details_Model.Sex = Trainee.Sex;
			Default_Trainee_Details_Model.Birthdate = DefaultDateTime_If_Empty(Trainee.Birthdate);
			Default_Trainee_Details_Model.Nationality = Trainee.Nationality;
			Default_Trainee_Details_Model.BirthPlace = Trainee.BirthPlace;
			Default_Trainee_Details_Model.CIN = Trainee.CIN;
			Default_Trainee_Details_Model.Cellphone = Trainee.Cellphone;
			Default_Trainee_Details_Model.Email = Trainee.Email;
			Default_Trainee_Details_Model.Address = Trainee.Address;
			Default_Trainee_Details_Model.FaceBook = Trainee.FaceBook;
			Default_Trainee_Details_Model.WebSite = Trainee.WebSite;
			Default_Trainee_Details_Model.Id = Trainee.Id;
            return Default_Trainee_Details_Model;            
        }

		public virtual Default_Trainee_Details_Model CreateNew()
        {
            Trainee Trainee = new TraineeBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Trainee_Details_Model Default_Trainee_Details_Model = this.ConverTo_Default_Trainee_Details_Model(Trainee);
            return Default_Trainee_Details_Model;
        } 

		public virtual List<Default_Trainee_Details_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            TraineeBLO entityBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Trainee> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Trainee_Details_Model> ls_models = new List<Default_Trainee_Details_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Trainee_Details_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Trainee_Details_ModelBLM : BaseDefault_Trainee_Details_Model_BLM
	{
		public Default_Trainee_Details_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
