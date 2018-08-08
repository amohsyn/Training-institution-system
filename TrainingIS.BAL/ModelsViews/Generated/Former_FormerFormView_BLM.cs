using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews.FormerModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseFormerFormViewBLM : ViewModelBLM
    {
        
        public BaseFormerFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Former ConverTo_Former(FormerFormView FormerFormView)
        {
			Former Former = null;
            if (FormerFormView.Id != 0)
            {
                Former = new FormerBLO(this.UnitOfWork).FindBaseEntityByID(FormerFormView.Id);
            }
            else
            {
                Former = new Former();
            } 
			Former.RegistrationNumber = FormerFormView.RegistrationNumber;
			Former.FirstName = FormerFormView.FirstName;
			Former.LastName = FormerFormView.LastName;
			Former.FirstNameArabe = FormerFormView.FirstNameArabe;
			Former.LastNameArabe = FormerFormView.LastNameArabe;
			Former.NationalityId = FormerFormView.NationalityId;
			Former.Sex = FormerFormView.Sex;
			Former.Birthdate = DefaultDateTime_If_Empty(FormerFormView.Birthdate);
			Former.BirthPlace = FormerFormView.BirthPlace;
			Former.CIN = FormerFormView.CIN;
			Former.Cellphone = FormerFormView.Cellphone;
			Former.Email = FormerFormView.Email;
			Former.Address = FormerFormView.Address;
			Former.Id = FormerFormView.Id;
            return Former;
        }
        public virtual FormerFormView ConverTo_FormerFormView(Former Former)
        {  
			FormerFormView FormerFormView = new FormerFormView();
			FormerFormView.toStringValue = Former.ToString();
			FormerFormView.RegistrationNumber = Former.RegistrationNumber;
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
            Former Former = new Former();
            FormerFormView FormerFormView = this.ConverTo_FormerFormView(Former);
            return FormerFormView;
        } 
    }

	public partial class FormerFormViewBLM : BaseFormerFormViewBLM
	{
		public FormerFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
