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
	public partial class BaseDefault_FormerDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_FormerDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Former ConverTo_Former(Default_FormerDetailsView Default_FormerDetailsView)
        {
			Former Former = null;
            if (Default_FormerDetailsView.Id != 0)
            {
                Former = new FormerBLO(this.UnitOfWork).FindBaseEntityByID(Default_FormerDetailsView.Id);
            }
            else
            {
                Former = new Former();
            } 
			Former.RegistrationNumber = Default_FormerDetailsView.RegistrationNumber;
			Former.Login = Default_FormerDetailsView.Login;
			Former.Password = Default_FormerDetailsView.Password;
			Former.FirstName = Default_FormerDetailsView.FirstName;
			Former.LastName = Default_FormerDetailsView.LastName;
			Former.FirstNameArabe = Default_FormerDetailsView.FirstNameArabe;
			Former.LastNameArabe = Default_FormerDetailsView.LastNameArabe;
			Former.Sex = Default_FormerDetailsView.Sex;
			Former.Birthdate = DefaultDateTime_If_Empty(Default_FormerDetailsView.Birthdate);
			Former.Nationality = Default_FormerDetailsView.Nationality;
			Former.BirthPlace = Default_FormerDetailsView.BirthPlace;
			Former.CIN = Default_FormerDetailsView.CIN;
			Former.Cellphone = Default_FormerDetailsView.Cellphone;
			Former.Email = Default_FormerDetailsView.Email;
			Former.Address = Default_FormerDetailsView.Address;
			Former.FaceBook = Default_FormerDetailsView.FaceBook;
			Former.WebSite = Default_FormerDetailsView.WebSite;
			Former.Id = Default_FormerDetailsView.Id;
            return Former;
        }
        public virtual Default_FormerDetailsView ConverTo_Default_FormerDetailsView(Former Former)
        {  
			Default_FormerDetailsView Default_FormerDetailsView = new Default_FormerDetailsView();
			Default_FormerDetailsView.toStringValue = Former.ToString();
			Default_FormerDetailsView.RegistrationNumber = Former.RegistrationNumber;
			Default_FormerDetailsView.Login = Former.Login;
			Default_FormerDetailsView.Password = Former.Password;
			Default_FormerDetailsView.FirstName = Former.FirstName;
			Default_FormerDetailsView.LastName = Former.LastName;
			Default_FormerDetailsView.FirstNameArabe = Former.FirstNameArabe;
			Default_FormerDetailsView.LastNameArabe = Former.LastNameArabe;
			Default_FormerDetailsView.Sex = Former.Sex;
			Default_FormerDetailsView.Birthdate = DefaultDateTime_If_Empty(Former.Birthdate);
			Default_FormerDetailsView.Nationality = Former.Nationality;
			Default_FormerDetailsView.BirthPlace = Former.BirthPlace;
			Default_FormerDetailsView.CIN = Former.CIN;
			Default_FormerDetailsView.Cellphone = Former.Cellphone;
			Default_FormerDetailsView.Email = Former.Email;
			Default_FormerDetailsView.Address = Former.Address;
			Default_FormerDetailsView.FaceBook = Former.FaceBook;
			Default_FormerDetailsView.WebSite = Former.WebSite;
			Default_FormerDetailsView.Id = Former.Id;
            return Default_FormerDetailsView;            
        }

		public virtual Default_FormerDetailsView CreateNew()
        {
            Former Former = new Former();
            Default_FormerDetailsView Default_FormerDetailsView = this.ConverTo_Default_FormerDetailsView(Former);
            return Default_FormerDetailsView;
        } 
    }

	public partial class Default_FormerDetailsViewBLM : BaseDefault_FormerDetailsViewBLM
	{
		public Default_FormerDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
