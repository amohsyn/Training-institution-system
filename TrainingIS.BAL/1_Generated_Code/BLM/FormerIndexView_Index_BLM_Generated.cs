//modelType = FormerIndexView

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
	public partial class BaseFormerIndexView_Index_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseFormerIndexView_Index_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Former ConverTo_Former(FormerIndexView FormerIndexView)
        {
			Former Former = null;
            if (FormerIndexView.Id != 0)
            {
                Former = new FormerBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(FormerIndexView.Id);
            }
            else
            {
                Former = new Former();
            } 
			Former.RegistrationNumber = FormerIndexView.RegistrationNumber;
			Former.FirstName = FormerIndexView.FirstName;
			Former.LastName = FormerIndexView.LastName;
			Former.FormerSpecialty = FormerIndexView.FormerSpecialty;
			Former.Email = FormerIndexView.Email;
			Former.Id = FormerIndexView.Id;
            return Former;
        }
        public virtual FormerIndexView ConverTo_FormerIndexView(Former Former)
        {  
			FormerIndexView FormerIndexView = new FormerIndexView();
			FormerIndexView.toStringValue = Former.ToString();
			FormerIndexView.FormerSpecialty = Former.FormerSpecialty;
			FormerIndexView.RegistrationNumber = Former.RegistrationNumber;
			FormerIndexView.FirstName = Former.FirstName;
			FormerIndexView.LastName = Former.LastName;
			FormerIndexView.Email = Former.Email;
			FormerIndexView.Id = Former.Id;
            return FormerIndexView;            
        }

		public virtual FormerIndexView CreateNew()
        {
            Former Former = new FormerBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            FormerIndexView FormerIndexView = this.ConverTo_FormerIndexView(Former);
            return FormerIndexView;
        } 

		public virtual List<FormerIndexView> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            FormerBLO entityBLO = new FormerBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Former> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<FormerIndexView> ls_models = new List<FormerIndexView>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_FormerIndexView(entity));
            }
            return ls_models;
        }


    }

	public partial class FormerIndexViewBLM : BaseFormerIndexView_Index_BLM
	{
		public FormerIndexViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
