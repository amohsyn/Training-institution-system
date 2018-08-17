//modelType = Default_Details_Former_Model

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

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_Former_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Former_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Former ConverTo_Former(Default_Details_Former_Model Default_Details_Former_Model)
        {
			Former Former = null;
            if (Default_Details_Former_Model.Id != 0)
            {
                Former = new FormerBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Former_Model.Id);
            }
            else
            {
                Former = new Former();
            } 
			Former.RegistrationNumber = Default_Details_Former_Model.RegistrationNumber;
			Former.CreateUserAccount = Default_Details_Former_Model.CreateUserAccount;
			Former.Login = Default_Details_Former_Model.Login;
			Former.Password = Default_Details_Former_Model.Password;
			Former.FirstName = Default_Details_Former_Model.FirstName;
			Former.LastName = Default_Details_Former_Model.LastName;
			Former.FirstNameArabe = Default_Details_Former_Model.FirstNameArabe;
			Former.LastNameArabe = Default_Details_Former_Model.LastNameArabe;
			Former.Sex = Default_Details_Former_Model.Sex;
			Former.Birthdate = DefaultDateTime_If_Empty(Default_Details_Former_Model.Birthdate);
			Former.Nationality = Default_Details_Former_Model.Nationality;
			Former.BirthPlace = Default_Details_Former_Model.BirthPlace;
			Former.CIN = Default_Details_Former_Model.CIN;
			Former.Cellphone = Default_Details_Former_Model.Cellphone;
			Former.Email = Default_Details_Former_Model.Email;
			Former.Address = Default_Details_Former_Model.Address;
			Former.FaceBook = Default_Details_Former_Model.FaceBook;
			Former.WebSite = Default_Details_Former_Model.WebSite;
			Former.Id = Default_Details_Former_Model.Id;
            return Former;
        }
        public virtual Default_Details_Former_Model ConverTo_Default_Details_Former_Model(Former Former)
        {  
			Default_Details_Former_Model Default_Details_Former_Model = new Default_Details_Former_Model();
			Default_Details_Former_Model.toStringValue = Former.ToString();
			Default_Details_Former_Model.RegistrationNumber = Former.RegistrationNumber;
			Default_Details_Former_Model.CreateUserAccount = Former.CreateUserAccount;
			Default_Details_Former_Model.Login = Former.Login;
			Default_Details_Former_Model.Password = Former.Password;
			Default_Details_Former_Model.FirstName = Former.FirstName;
			Default_Details_Former_Model.LastName = Former.LastName;
			Default_Details_Former_Model.FirstNameArabe = Former.FirstNameArabe;
			Default_Details_Former_Model.LastNameArabe = Former.LastNameArabe;
			Default_Details_Former_Model.Sex = Former.Sex;
			Default_Details_Former_Model.Birthdate = DefaultDateTime_If_Empty(Former.Birthdate);
			Default_Details_Former_Model.Nationality = Former.Nationality;
			Default_Details_Former_Model.BirthPlace = Former.BirthPlace;
			Default_Details_Former_Model.CIN = Former.CIN;
			Default_Details_Former_Model.Cellphone = Former.Cellphone;
			Default_Details_Former_Model.Email = Former.Email;
			Default_Details_Former_Model.Address = Former.Address;
			Default_Details_Former_Model.FaceBook = Former.FaceBook;
			Default_Details_Former_Model.WebSite = Former.WebSite;
			Default_Details_Former_Model.Id = Former.Id;
            return Default_Details_Former_Model;            
        }

		public virtual Default_Details_Former_Model CreateNew()
        {
            Former Former = new FormerBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Former_Model Default_Details_Former_Model = this.ConverTo_Default_Details_Former_Model(Former);
            return Default_Details_Former_Model;
        } 
    }

	public partial class Default_Details_Former_ModelBLM : BaseDefault_Details_Former_ModelBLM
	{
		public Default_Details_Former_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
