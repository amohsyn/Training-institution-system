using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
using GApp.Core.Utils;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_TraineeFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_TraineeFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Trainee ConverTo_Trainee(Default_TraineeFormView Default_TraineeFormView)
        {
			Trainee Trainee = null;
            if (Default_TraineeFormView.Id != 0)
            {
                Trainee = new TraineeBLO(this.UnitOfWork).FindBaseEntityByID(Default_TraineeFormView.Id);
            }
            else
            {
                Trainee = new Trainee();
            } 
			Trainee.Cellphone = Default_TraineeFormView.Cellphone;
			Trainee.TutorCellPhone = Default_TraineeFormView.TutorCellPhone;
			Trainee.Email = Default_TraineeFormView.Email;
			Trainee.Address = Default_TraineeFormView.Address;
			Trainee.FaceBook = Default_TraineeFormView.FaceBook;
			Trainee.WebSite = Default_TraineeFormView.WebSite;
			Trainee.CNE = Default_TraineeFormView.CNE;
			Trainee.isActif = Default_TraineeFormView.isActif;
			Trainee.DateRegistration = Default_TraineeFormView.DateRegistration;
			Trainee.NationalityId = Default_TraineeFormView.NationalityId;
			Trainee.SchoollevelId = Default_TraineeFormView.SchoollevelId;
			Trainee.GroupId = Default_TraineeFormView.GroupId;
			Trainee.FirstName = Default_TraineeFormView.FirstName;
			Trainee.LastName = Default_TraineeFormView.LastName;
			Trainee.FirstNameArabe = Default_TraineeFormView.FirstNameArabe;
			Trainee.LastNameArabe = Default_TraineeFormView.LastNameArabe;
			Trainee.Birthdate = Default_TraineeFormView.Birthdate;
			Trainee.BirthPlace = Default_TraineeFormView.BirthPlace;
			Trainee.Sex = Default_TraineeFormView.Sex;
			Trainee.CIN = Default_TraineeFormView.CIN;
			Trainee.Id = Default_TraineeFormView.Id;
            return Trainee;
        }
        public virtual Default_TraineeFormView ConverTo_Default_TraineeFormView(Trainee Trainee)
        {  
			Default_TraineeFormView Default_TraineeFormView = new Default_TraineeFormView();
			Default_TraineeFormView.toStringValue = Trainee.ToString();
			Default_TraineeFormView.Cellphone = Trainee.Cellphone;
			Default_TraineeFormView.TutorCellPhone = Trainee.TutorCellPhone;
			Default_TraineeFormView.Email = Trainee.Email;
			Default_TraineeFormView.Address = Trainee.Address;
			Default_TraineeFormView.FaceBook = Trainee.FaceBook;
			Default_TraineeFormView.WebSite = Trainee.WebSite;
			Default_TraineeFormView.CNE = Trainee.CNE;
			Default_TraineeFormView.isActif = Trainee.isActif;
			Default_TraineeFormView.DateRegistration = ConversionUtil.DefaultValue_if_Null<DateTime>(Trainee.DateRegistration);
			Default_TraineeFormView.NationalityId = Trainee.NationalityId;
			Default_TraineeFormView.SchoollevelId = ConversionUtil.DefaultValue_if_Null<Int64>(Trainee.SchoollevelId);
			Default_TraineeFormView.GroupId = Trainee.GroupId;
			Default_TraineeFormView.FirstName = Trainee.FirstName;
			Default_TraineeFormView.LastName = Trainee.LastName;
			Default_TraineeFormView.FirstNameArabe = Trainee.FirstNameArabe;
			Default_TraineeFormView.LastNameArabe = Trainee.LastNameArabe;
			Default_TraineeFormView.Birthdate = Trainee.Birthdate;
			Default_TraineeFormView.BirthPlace = Trainee.BirthPlace;
			Default_TraineeFormView.Sex = Trainee.Sex;
			Default_TraineeFormView.CIN = Trainee.CIN;
			Default_TraineeFormView.Id = Trainee.Id;
            return Default_TraineeFormView;            
        }
    }

	public partial class Default_TraineeFormViewBLM : BaseDefault_TraineeFormViewBLM
	{
		public Default_TraineeFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
