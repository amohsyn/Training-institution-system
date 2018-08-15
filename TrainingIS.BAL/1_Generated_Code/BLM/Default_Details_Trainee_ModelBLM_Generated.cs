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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_Trainee_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Details_Trainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Trainee ConverTo_Trainee(Default_Details_Trainee_Model Default_Details_Trainee_Model)
        {
			Trainee Trainee = null;
            if (Default_Details_Trainee_Model.Id != 0)
            {
                Trainee = new TraineeBLO(this.UnitOfWork).FindBaseEntityByID(Default_Details_Trainee_Model.Id);
            }
            else
            {
                Trainee = new Trainee();
            } 
			Trainee.CNE = Default_Details_Trainee_Model.CNE;
			Trainee.DateRegistration = Default_Details_Trainee_Model.DateRegistration;
			Trainee.isActif = Default_Details_Trainee_Model.isActif;
			Trainee.Schoollevel = Default_Details_Trainee_Model.Schoollevel;
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
            Trainee Trainee = new Trainee();
            Default_Details_Trainee_Model Default_Details_Trainee_Model = this.ConverTo_Default_Details_Trainee_Model(Trainee);
            return Default_Details_Trainee_Model;
        } 
    }

	public partial class Default_Details_Trainee_ModelBLM : BaseDefault_Details_Trainee_ModelBLM
	{
		public Default_Details_Trainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
