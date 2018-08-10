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
	public partial class BaseFormerIndexViewBLM : ViewModelBLM
    {
        
        public BaseFormerIndexViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Former ConverTo_Former(FormerIndexView FormerIndexView)
        {
			Former Former = null;
            if (FormerIndexView.Id != 0)
            {
                Former = new FormerBLO(this.UnitOfWork).FindBaseEntityByID(FormerIndexView.Id);
            }
            else
            {
                Former = new Former();
            } 
			Former.RegistrationNumber = FormerIndexView.RegistrationNumber;
			Former.FirstName = FormerIndexView.FirstName;
			Former.LastName = FormerIndexView.LastName;
			Former.Cellphone = FormerIndexView.Cellphone;
			Former.Email = FormerIndexView.Email;
			Former.Id = FormerIndexView.Id;
            return Former;
        }
        public virtual FormerIndexView ConverTo_FormerIndexView(Former Former)
        {  
			FormerIndexView FormerIndexView = new FormerIndexView();
			FormerIndexView.toStringValue = Former.ToString();
			FormerIndexView.RegistrationNumber = Former.RegistrationNumber;
			FormerIndexView.FirstName = Former.FirstName;
			FormerIndexView.LastName = Former.LastName;
			FormerIndexView.Cellphone = Former.Cellphone;
			FormerIndexView.Email = Former.Email;
			FormerIndexView.Id = Former.Id;
            return FormerIndexView;            
        }

		public virtual FormerIndexView CreateNew()
        {
            Former Former = new Former();
            FormerIndexView FormerIndexView = this.ConverTo_FormerIndexView(Former);
            return FormerIndexView;
        } 
    }

	public partial class FormerIndexViewBLM : BaseFormerIndexViewBLM
	{
		public FormerIndexViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
