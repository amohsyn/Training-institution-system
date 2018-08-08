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
	public partial class BaseDefault_TraineeDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_TraineeDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Trainee ConverTo_Trainee(Default_TraineeDetailsView Default_TraineeDetailsView)
        {
			Trainee Trainee = null;
            if (Default_TraineeDetailsView.Id != 0)
            {
                Trainee = new TraineeBLO(this.UnitOfWork).FindBaseEntityByID(Default_TraineeDetailsView.Id);
            }
            else
            {
                Trainee = new Trainee();
            } 
			Trainee.CNE = Default_TraineeDetailsView.CNE;
			Trainee.DateRegistration = Default_TraineeDetailsView.DateRegistration;
			Trainee.isActif = Default_TraineeDetailsView.isActif;
			Trainee.Schoollevel = Default_TraineeDetailsView.Schoollevel;
			Trainee.Group = Default_TraineeDetailsView.Group;
			Trainee.FirstName = Default_TraineeDetailsView.FirstName;
			Trainee.LastName = Default_TraineeDetailsView.LastName;
			Trainee.FirstNameArabe = Default_TraineeDetailsView.FirstNameArabe;
			Trainee.LastNameArabe = Default_TraineeDetailsView.LastNameArabe;
			Trainee.Sex = Default_TraineeDetailsView.Sex;
			Trainee.Birthdate = DefaultDateTime_If_Empty(Default_TraineeDetailsView.Birthdate);
			Trainee.Nationality = Default_TraineeDetailsView.Nationality;
			Trainee.BirthPlace = Default_TraineeDetailsView.BirthPlace;
			Trainee.CIN = Default_TraineeDetailsView.CIN;
			Trainee.Cellphone = Default_TraineeDetailsView.Cellphone;
			Trainee.Email = Default_TraineeDetailsView.Email;
			Trainee.Address = Default_TraineeDetailsView.Address;
			Trainee.FaceBook = Default_TraineeDetailsView.FaceBook;
			Trainee.WebSite = Default_TraineeDetailsView.WebSite;
			Trainee.Id = Default_TraineeDetailsView.Id;
            return Trainee;
        }
        public virtual Default_TraineeDetailsView ConverTo_Default_TraineeDetailsView(Trainee Trainee)
        {  
			Default_TraineeDetailsView Default_TraineeDetailsView = new Default_TraineeDetailsView();
			Default_TraineeDetailsView.toStringValue = Trainee.ToString();
			Default_TraineeDetailsView.CNE = Trainee.CNE;
			Default_TraineeDetailsView.DateRegistration = ConversionUtil.DefaultValue_if_Null<DateTime>(Trainee.DateRegistration);
			Default_TraineeDetailsView.isActif = Trainee.isActif;
			Default_TraineeDetailsView.Schoollevel = Trainee.Schoollevel;
			Default_TraineeDetailsView.Group = Trainee.Group;
			Default_TraineeDetailsView.FirstName = Trainee.FirstName;
			Default_TraineeDetailsView.LastName = Trainee.LastName;
			Default_TraineeDetailsView.FirstNameArabe = Trainee.FirstNameArabe;
			Default_TraineeDetailsView.LastNameArabe = Trainee.LastNameArabe;
			Default_TraineeDetailsView.Sex = Trainee.Sex;
			Default_TraineeDetailsView.Birthdate = DefaultDateTime_If_Empty(Trainee.Birthdate);
			Default_TraineeDetailsView.Nationality = Trainee.Nationality;
			Default_TraineeDetailsView.BirthPlace = Trainee.BirthPlace;
			Default_TraineeDetailsView.CIN = Trainee.CIN;
			Default_TraineeDetailsView.Cellphone = Trainee.Cellphone;
			Default_TraineeDetailsView.Email = Trainee.Email;
			Default_TraineeDetailsView.Address = Trainee.Address;
			Default_TraineeDetailsView.FaceBook = Trainee.FaceBook;
			Default_TraineeDetailsView.WebSite = Trainee.WebSite;
			Default_TraineeDetailsView.Id = Trainee.Id;
            return Default_TraineeDetailsView;            
        }

		public virtual Default_TraineeDetailsView CreateNew()
        {
            Trainee Trainee = new Trainee();
            Default_TraineeDetailsView Default_TraineeDetailsView = this.ConverTo_Default_TraineeDetailsView(Trainee);
            return Default_TraineeDetailsView;
        } 
    }

	public partial class Default_TraineeDetailsViewBLM : BaseDefault_TraineeDetailsViewBLM
	{
		public Default_TraineeDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
