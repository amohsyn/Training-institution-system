using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_FormerDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_FormerDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Former ConverTo_Former(Default_FormerDetailsView Default_FormerDetailsView)
        {
			Former Former = new Former();
			Former.FirstName = Default_FormerDetailsView.FirstName;
			Former.LastName = Default_FormerDetailsView.LastName;
			Former.Sex = Default_FormerDetailsView.Sex;
			Former.CIN = Default_FormerDetailsView.CIN;
			Former.Cellphone = Default_FormerDetailsView.Cellphone;
			Former.Email = Default_FormerDetailsView.Email;
			Former.Address = Default_FormerDetailsView.Address;
			Former.FaceBook = Default_FormerDetailsView.FaceBook;
			Former.WebSite = Default_FormerDetailsView.WebSite;
			Former.RegistrationNumber = Default_FormerDetailsView.RegistrationNumber;
			Former.Id = Default_FormerDetailsView.Id;
            return Former;

        }
        public virtual Default_FormerDetailsView ConverTo_Default_FormerDetailsView(Former Former)
        {
            Default_FormerDetailsView Default_FormerDetailsView = new Default_FormerDetailsView();
			Default_FormerDetailsView.FirstName = Former.FirstName;
			Default_FormerDetailsView.LastName = Former.LastName;
			Default_FormerDetailsView.Sex = Former.Sex;
			Default_FormerDetailsView.CIN = Former.CIN;
			Default_FormerDetailsView.Cellphone = Former.Cellphone;
			Default_FormerDetailsView.Email = Former.Email;
			Default_FormerDetailsView.Address = Former.Address;
			Default_FormerDetailsView.FaceBook = Former.FaceBook;
			Default_FormerDetailsView.WebSite = Former.WebSite;
			Default_FormerDetailsView.RegistrationNumber = Former.RegistrationNumber;
			Default_FormerDetailsView.Id = Former.Id;
            return Default_FormerDetailsView;            
        }
    }

	public partial class Default_FormerDetailsViewBLM : BaseDefault_FormerDetailsViewBLM
	{
		public Default_FormerDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
