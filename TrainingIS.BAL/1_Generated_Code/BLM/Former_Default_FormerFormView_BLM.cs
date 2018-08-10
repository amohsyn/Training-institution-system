using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_FormerFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_FormerFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Former ConverTo_Former(Default_FormerFormView Default_FormerFormView)
        {
			Former Former = null;
            if (Default_FormerFormView.Id != 0)
            {
                Former = new FormerBLO(this.UnitOfWork).FindBaseEntityByID(Default_FormerFormView.Id);
            }
            else
            {
                Former = new Former();
            } 
			Former.RegistrationNumber = Default_FormerFormView.RegistrationNumber;
			Former.Login = Default_FormerFormView.Login;
			Former.Password = Default_FormerFormView.Password;
			Former.FirstName = Default_FormerFormView.FirstName;
			Former.LastName = Default_FormerFormView.LastName;
			Former.FirstNameArabe = Default_FormerFormView.FirstNameArabe;
			Former.LastNameArabe = Default_FormerFormView.LastNameArabe;
			Former.Sex = Default_FormerFormView.Sex;
			Former.Birthdate = DefaultDateTime_If_Empty(Default_FormerFormView.Birthdate);
			Former.NationalityId = Default_FormerFormView.NationalityId;
			Former.BirthPlace = Default_FormerFormView.BirthPlace;
			Former.CIN = Default_FormerFormView.CIN;
			Former.Cellphone = Default_FormerFormView.Cellphone;
			Former.Email = Default_FormerFormView.Email;
			Former.Address = Default_FormerFormView.Address;
			Former.FaceBook = Default_FormerFormView.FaceBook;
			Former.WebSite = Default_FormerFormView.WebSite;
			Former.Id = Default_FormerFormView.Id;
            return Former;
        }
        public virtual Default_FormerFormView ConverTo_Default_FormerFormView(Former Former)
        {  
			Default_FormerFormView Default_FormerFormView = new Default_FormerFormView();
			Default_FormerFormView.toStringValue = Former.ToString();
			Default_FormerFormView.RegistrationNumber = Former.RegistrationNumber;
			Default_FormerFormView.Login = Former.Login;
			Default_FormerFormView.Password = Former.Password;
			Default_FormerFormView.FirstName = Former.FirstName;
			Default_FormerFormView.LastName = Former.LastName;
			Default_FormerFormView.FirstNameArabe = Former.FirstNameArabe;
			Default_FormerFormView.LastNameArabe = Former.LastNameArabe;
			Default_FormerFormView.Sex = Former.Sex;
			Default_FormerFormView.Birthdate = DefaultDateTime_If_Empty(Former.Birthdate);
			Default_FormerFormView.NationalityId = Former.NationalityId;
			Default_FormerFormView.BirthPlace = Former.BirthPlace;
			Default_FormerFormView.CIN = Former.CIN;
			Default_FormerFormView.Cellphone = Former.Cellphone;
			Default_FormerFormView.Email = Former.Email;
			Default_FormerFormView.Address = Former.Address;
			Default_FormerFormView.FaceBook = Former.FaceBook;
			Default_FormerFormView.WebSite = Former.WebSite;
			Default_FormerFormView.Id = Former.Id;
            return Default_FormerFormView;            
        }

		public virtual Default_FormerFormView CreateNew()
        {
            Former Former = new Former();
            Default_FormerFormView Default_FormerFormView = this.ConverTo_Default_FormerFormView(Former);
            return Default_FormerFormView;
        } 
    }

	public partial class Default_FormerFormViewBLM : BaseDefault_FormerFormViewBLM
	{
		public Default_FormerFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
