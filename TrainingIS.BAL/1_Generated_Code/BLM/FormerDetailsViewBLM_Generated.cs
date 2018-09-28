//modelType = FormerDetailsView

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
using TrainingIS.Entities.ModelsViews.FormerModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseFormerDetailsViewBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseFormerDetailsViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Former ConverTo_Former(FormerDetailsView FormerDetailsView)
        {
			Former Former = null;
            if (FormerDetailsView.Id != 0)
            {
                Former = new FormerBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(FormerDetailsView.Id);
            }
            else
            {
                Former = new Former();
            } 
			Former.RegistrationNumber = FormerDetailsView.RegistrationNumber;
			Former.FirstName = FormerDetailsView.FirstName;
			Former.LastName = FormerDetailsView.LastName;
			Former.FormerSpecialty = FormerDetailsView.FormerSpecialty;
			Former.WeeklyHourlyMass = FormerDetailsView.WeeklyHourlyMass;
			Former.FirstNameArabe = FormerDetailsView.FirstNameArabe;
			Former.LastNameArabe = FormerDetailsView.LastNameArabe;
			Former.Sex = FormerDetailsView.Sex;
			Former.Nationality = FormerDetailsView.Nationality;
			Former.Birthdate = DefaultDateTime_If_Empty(FormerDetailsView.Birthdate);
			Former.BirthPlace = FormerDetailsView.BirthPlace;
			Former.CIN = FormerDetailsView.CIN;
			Former.Cellphone = FormerDetailsView.Cellphone;
			Former.Email = FormerDetailsView.Email;
			Former.Address = FormerDetailsView.Address;
			Former.Id = FormerDetailsView.Id;
            return Former;
        }
        public virtual FormerDetailsView ConverTo_FormerDetailsView(Former Former)
        {  
			FormerDetailsView FormerDetailsView = new FormerDetailsView();
			FormerDetailsView.toStringValue = Former.ToString();
			FormerDetailsView.RegistrationNumber = Former.RegistrationNumber;
			FormerDetailsView.FormerSpecialty = Former.FormerSpecialty;
			FormerDetailsView.WeeklyHourlyMass = Former.WeeklyHourlyMass;
			FormerDetailsView.FirstName = Former.FirstName;
			FormerDetailsView.LastName = Former.LastName;
			FormerDetailsView.FirstNameArabe = Former.FirstNameArabe;
			FormerDetailsView.LastNameArabe = Former.LastNameArabe;
			FormerDetailsView.Sex = Former.Sex;
			FormerDetailsView.Birthdate = DefaultDateTime_If_Empty(Former.Birthdate);
			FormerDetailsView.Nationality = Former.Nationality;
			FormerDetailsView.BirthPlace = Former.BirthPlace;
			FormerDetailsView.CIN = Former.CIN;
			FormerDetailsView.Cellphone = Former.Cellphone;
			FormerDetailsView.Email = Former.Email;
			FormerDetailsView.Address = Former.Address;
			FormerDetailsView.Id = Former.Id;
            return FormerDetailsView;            
        }

		public virtual FormerDetailsView CreateNew()
        {
            Former Former = new FormerBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            FormerDetailsView FormerDetailsView = this.ConverTo_FormerDetailsView(Former);
            return FormerDetailsView;
        } 

		public List<FormerDetailsView> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            FormerBLO entityBLO = new FormerBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Former> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<FormerDetailsView> ls_models = new List<FormerDetailsView>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_FormerDetailsView(entity));
            }
            return ls_models;
        }


    }

	public partial class FormerDetailsViewBLM : BaseFormerDetailsViewBLM
	{
		public FormerDetailsViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
