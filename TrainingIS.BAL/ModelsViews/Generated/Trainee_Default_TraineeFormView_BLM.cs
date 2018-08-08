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
			Trainee.CNE = Default_TraineeFormView.CNE;
			Trainee.DateRegistration = Default_TraineeFormView.DateRegistration;
			Trainee.isActif = Default_TraineeFormView.isActif;
			Trainee.SchoollevelId = Default_TraineeFormView.SchoollevelId;
			Trainee.GroupId = Default_TraineeFormView.GroupId;
			Trainee.FirstName = Default_TraineeFormView.FirstName;
			Trainee.LastName = Default_TraineeFormView.LastName;
			Trainee.FirstNameArabe = Default_TraineeFormView.FirstNameArabe;
			Trainee.LastNameArabe = Default_TraineeFormView.LastNameArabe;
			Trainee.Sex = Default_TraineeFormView.Sex;
			Trainee.Birthdate = DefaultDateTime_If_Empty(Default_TraineeFormView.Birthdate);
			Trainee.NationalityId = Default_TraineeFormView.NationalityId;
			Trainee.BirthPlace = Default_TraineeFormView.BirthPlace;
			Trainee.CIN = Default_TraineeFormView.CIN;
			Trainee.Cellphone = Default_TraineeFormView.Cellphone;
			Trainee.Email = Default_TraineeFormView.Email;
			Trainee.Address = Default_TraineeFormView.Address;
			Trainee.FaceBook = Default_TraineeFormView.FaceBook;
			Trainee.WebSite = Default_TraineeFormView.WebSite;
			Trainee.Id = Default_TraineeFormView.Id;
            return Trainee;
        }
        public virtual Default_TraineeFormView ConverTo_Default_TraineeFormView(Trainee Trainee)
        {  
			Default_TraineeFormView Default_TraineeFormView = new Default_TraineeFormView();
			Default_TraineeFormView.toStringValue = Trainee.ToString();
			Default_TraineeFormView.CNE = Trainee.CNE;
			Default_TraineeFormView.DateRegistration = ConversionUtil.DefaultValue_if_Null<DateTime>(Trainee.DateRegistration);
			Default_TraineeFormView.isActif = Trainee.isActif;
			Default_TraineeFormView.SchoollevelId = ConversionUtil.DefaultValue_if_Null<Int64>(Trainee.SchoollevelId);
			Default_TraineeFormView.GroupId = Trainee.GroupId;
			Default_TraineeFormView.FirstName = Trainee.FirstName;
			Default_TraineeFormView.LastName = Trainee.LastName;
			Default_TraineeFormView.FirstNameArabe = Trainee.FirstNameArabe;
			Default_TraineeFormView.LastNameArabe = Trainee.LastNameArabe;
			Default_TraineeFormView.Sex = Trainee.Sex;
			Default_TraineeFormView.Birthdate = DefaultDateTime_If_Empty(Trainee.Birthdate);
			Default_TraineeFormView.NationalityId = Trainee.NationalityId;
			Default_TraineeFormView.BirthPlace = Trainee.BirthPlace;
			Default_TraineeFormView.CIN = Trainee.CIN;
			Default_TraineeFormView.Cellphone = Trainee.Cellphone;
			Default_TraineeFormView.Email = Trainee.Email;
			Default_TraineeFormView.Address = Trainee.Address;
			Default_TraineeFormView.FaceBook = Trainee.FaceBook;
			Default_TraineeFormView.WebSite = Trainee.WebSite;
			Default_TraineeFormView.Id = Trainee.Id;
            return Default_TraineeFormView;            
        }

		public virtual Default_TraineeFormView CreateNew()
        {
            Trainee Trainee = new Trainee();
            Default_TraineeFormView Default_TraineeFormView = this.ConverTo_Default_TraineeFormView(Trainee);
            return Default_TraineeFormView;
        } 
    }

	public partial class Default_TraineeFormViewBLM : BaseDefault_TraineeFormViewBLM
	{
		public Default_TraineeFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
