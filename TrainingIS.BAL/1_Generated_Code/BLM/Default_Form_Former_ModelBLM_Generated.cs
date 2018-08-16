//modelType = Default_Form_Former_Model

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
	public partial class BaseDefault_Form_Former_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Former_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Former ConverTo_Former(Default_Form_Former_Model Default_Form_Former_Model)
        {
			Former Former = null;
            if (Default_Form_Former_Model.Id != 0)
            {
                Former = new FormerBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Former_Model.Id);
            }
            else
            {
                Former = new Former();
            } 
			Former.RegistrationNumber = Default_Form_Former_Model.RegistrationNumber;
			Former.Login = Default_Form_Former_Model.Login;
			Former.Password = Default_Form_Former_Model.Password;
			Former.FirstName = Default_Form_Former_Model.FirstName;
			Former.LastName = Default_Form_Former_Model.LastName;
			Former.FirstNameArabe = Default_Form_Former_Model.FirstNameArabe;
			Former.LastNameArabe = Default_Form_Former_Model.LastNameArabe;
			Former.Sex = Default_Form_Former_Model.Sex;
			Former.Birthdate = DefaultDateTime_If_Empty(Default_Form_Former_Model.Birthdate);
			Former.NationalityId = Default_Form_Former_Model.NationalityId;
			Former.BirthPlace = Default_Form_Former_Model.BirthPlace;
			Former.CIN = Default_Form_Former_Model.CIN;
			Former.Cellphone = Default_Form_Former_Model.Cellphone;
			Former.Email = Default_Form_Former_Model.Email;
			Former.Address = Default_Form_Former_Model.Address;
			Former.FaceBook = Default_Form_Former_Model.FaceBook;
			Former.WebSite = Default_Form_Former_Model.WebSite;
			Former.Id = Default_Form_Former_Model.Id;
            return Former;
        }
        public virtual Default_Form_Former_Model ConverTo_Default_Form_Former_Model(Former Former)
        {  
			Default_Form_Former_Model Default_Form_Former_Model = new Default_Form_Former_Model();
			Default_Form_Former_Model.toStringValue = Former.ToString();
			Default_Form_Former_Model.RegistrationNumber = Former.RegistrationNumber;
			Default_Form_Former_Model.Login = Former.Login;
			Default_Form_Former_Model.Password = Former.Password;
			Default_Form_Former_Model.FirstName = Former.FirstName;
			Default_Form_Former_Model.LastName = Former.LastName;
			Default_Form_Former_Model.FirstNameArabe = Former.FirstNameArabe;
			Default_Form_Former_Model.LastNameArabe = Former.LastNameArabe;
			Default_Form_Former_Model.Sex = Former.Sex;
			Default_Form_Former_Model.Birthdate = DefaultDateTime_If_Empty(Former.Birthdate);
			Default_Form_Former_Model.NationalityId = Former.NationalityId;
			Default_Form_Former_Model.BirthPlace = Former.BirthPlace;
			Default_Form_Former_Model.CIN = Former.CIN;
			Default_Form_Former_Model.Cellphone = Former.Cellphone;
			Default_Form_Former_Model.Email = Former.Email;
			Default_Form_Former_Model.Address = Former.Address;
			Default_Form_Former_Model.FaceBook = Former.FaceBook;
			Default_Form_Former_Model.WebSite = Former.WebSite;
			Default_Form_Former_Model.Id = Former.Id;
            return Default_Form_Former_Model;            
        }

		public virtual Default_Form_Former_Model CreateNew()
        {
            Former Former = new FormerBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Former_Model Default_Form_Former_Model = this.ConverTo_Default_Form_Former_Model(Former);
            return Default_Form_Former_Model;
        } 
    }

	public partial class Default_Form_Former_ModelBLM : BaseDefault_Form_Former_ModelBLM
	{
		public Default_Form_Former_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
