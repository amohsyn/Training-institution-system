﻿//modelType = Default_Form_Trainee_Model

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
	public partial class BaseDefault_Form_Trainee_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Trainee_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Trainee ConverTo_Trainee(Default_Form_Trainee_Model Default_Form_Trainee_Model)
        {
			Trainee Trainee = null;
            if (Default_Form_Trainee_Model.Id != 0)
            {
                Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Trainee_Model.Id);
            }
            else
            {
                Trainee = new Trainee();
            } 
			if (!string.IsNullOrEmpty(Default_Form_Trainee_Model.Photo_Reference))
            {
				if(Default_Form_Trainee_Model.Photo_Reference == "Delete" && Trainee.Photo != null)
                {
                    Trainee.Photo.Old_Reference = Trainee.Photo.Reference;
                    Trainee.Photo.Reference = "Delete";
                }
                else
				{
					if (Trainee.Photo == null) Trainee.Photo = new GPicture() { Old_Reference = "Empty" };
					if (Trainee.Photo.Reference != Default_Form_Trainee_Model.Photo_Reference)
					{
						// Save the old reference to be deleted by the save methode 
						if (!string.IsNullOrEmpty(Trainee.Photo.Reference))
                            Trainee.Photo.Old_Reference = Trainee.Photo.Reference;

						 

						GPictureBLO gPictureBLO = new GPictureBLO(this.UnitOfWork, this.GAppContext);
						Trainee.Photo.Reference = Default_Form_Trainee_Model.Photo_Reference;
                  
						Trainee.Photo.Original_Thumbnail = gPictureBLO.Get_URL_Original_Picture_Path(Default_Form_Trainee_Model.Photo_Reference);
						Trainee.Photo.Small_Thumbnail = gPictureBLO.Get_URL_Small_Picture_Path(Default_Form_Trainee_Model.Photo_Reference);
						Trainee.Photo.Medium_Thumbnail = gPictureBLO.Get_URL_Medium_Picture_Path(Default_Form_Trainee_Model.Photo_Reference);
						Trainee.Photo.Large_Thumbnail = gPictureBLO.Get_URL_Large_Picture_Path(Default_Form_Trainee_Model.Photo_Reference);
					}
				}

               
            }
					Trainee.CNE = Default_Form_Trainee_Model.CNE;
			Trainee.DateRegistration = Default_Form_Trainee_Model.DateRegistration;
			Trainee.isActif = Default_Form_Trainee_Model.isActif;
			Trainee.SchoollevelId = Default_Form_Trainee_Model.SchoollevelId;
			Trainee.Schoollevel = new SchoollevelBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Trainee_Model.SchoollevelId)) ;
			Trainee.SpecialtyId = Default_Form_Trainee_Model.SpecialtyId;
			Trainee.Specialty = new SpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Trainee_Model.SpecialtyId)) ;
			Trainee.YearStudyId = Default_Form_Trainee_Model.YearStudyId;
			Trainee.YearStudy = new YearStudyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Trainee_Model.YearStudyId)) ;
			Trainee.GroupId = Default_Form_Trainee_Model.GroupId;
			Trainee.Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Trainee_Model.GroupId)) ;
			Trainee.FirstName = Default_Form_Trainee_Model.FirstName;
			Trainee.LastName = Default_Form_Trainee_Model.LastName;
			Trainee.FirstNameArabe = Default_Form_Trainee_Model.FirstNameArabe;
			Trainee.LastNameArabe = Default_Form_Trainee_Model.LastNameArabe;
			Trainee.Sex = Default_Form_Trainee_Model.Sex;
			Trainee.Birthdate = DefaultDateTime_If_Empty(Default_Form_Trainee_Model.Birthdate);
			Trainee.NationalityId = Default_Form_Trainee_Model.NationalityId;
			Trainee.Nationality = new NationalityBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Trainee_Model.NationalityId)) ;
			Trainee.BirthPlace = Default_Form_Trainee_Model.BirthPlace;
			Trainee.CIN = Default_Form_Trainee_Model.CIN;
			Trainee.Cellphone = Default_Form_Trainee_Model.Cellphone;
			Trainee.Email = Default_Form_Trainee_Model.Email;
			Trainee.Address = Default_Form_Trainee_Model.Address;
			Trainee.FaceBook = Default_Form_Trainee_Model.FaceBook;
			Trainee.WebSite = Default_Form_Trainee_Model.WebSite;
			Trainee.Reference = Default_Form_Trainee_Model.Reference;
			Trainee.Id = Default_Form_Trainee_Model.Id;
            return Trainee;
        }
        public virtual void ConverTo_Default_Form_Trainee_Model(Default_Form_Trainee_Model Default_Form_Trainee_Model, Trainee Trainee)
        {  
			 
			Default_Form_Trainee_Model.toStringValue = Trainee.ToString();
			Default_Form_Trainee_Model.Photo = Trainee.Photo;
			Default_Form_Trainee_Model.CNE = Trainee.CNE;
			Default_Form_Trainee_Model.DateRegistration = ConversionUtil.DefaultValue_if_Null<DateTime>(Trainee.DateRegistration);
			Default_Form_Trainee_Model.isActif = Trainee.isActif;
			Default_Form_Trainee_Model.SchoollevelId = ConversionUtil.DefaultValue_if_Null<Int64>(Trainee.SchoollevelId);
			Default_Form_Trainee_Model.SpecialtyId = Trainee.SpecialtyId;
			Default_Form_Trainee_Model.YearStudyId = Trainee.YearStudyId;
			Default_Form_Trainee_Model.GroupId = Trainee.GroupId;
			Default_Form_Trainee_Model.FirstName = Trainee.FirstName;
			Default_Form_Trainee_Model.LastName = Trainee.LastName;
			Default_Form_Trainee_Model.FirstNameArabe = Trainee.FirstNameArabe;
			Default_Form_Trainee_Model.LastNameArabe = Trainee.LastNameArabe;
			Default_Form_Trainee_Model.Sex = Trainee.Sex;
			Default_Form_Trainee_Model.Birthdate = DefaultDateTime_If_Empty(Trainee.Birthdate);
			Default_Form_Trainee_Model.NationalityId = Trainee.NationalityId;
			Default_Form_Trainee_Model.BirthPlace = Trainee.BirthPlace;
			Default_Form_Trainee_Model.CIN = Trainee.CIN;
			Default_Form_Trainee_Model.Cellphone = Trainee.Cellphone;
			Default_Form_Trainee_Model.Email = Trainee.Email;
			Default_Form_Trainee_Model.Address = Trainee.Address;
			Default_Form_Trainee_Model.FaceBook = Trainee.FaceBook;
			Default_Form_Trainee_Model.WebSite = Trainee.WebSite;
			Default_Form_Trainee_Model.Id = Trainee.Id;
			Default_Form_Trainee_Model.Reference = Trainee.Reference;
                     
        }

    }

	public partial class Default_Form_Trainee_ModelBLM : BaseDefault_Form_Trainee_Model_BLM
	{
		public Default_Form_Trainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
