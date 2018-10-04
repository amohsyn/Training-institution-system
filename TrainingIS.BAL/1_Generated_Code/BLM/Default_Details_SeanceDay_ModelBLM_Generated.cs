//modelType = Default_Details_SeanceDay_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_SeanceDay_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_SeanceDay_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SeanceDay ConverTo_SeanceDay(Default_Details_SeanceDay_Model Default_Details_SeanceDay_Model)
        {
			SeanceDay SeanceDay = null;
            if (Default_Details_SeanceDay_Model.Id != 0)
            {
                SeanceDay = new SeanceDayBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_SeanceDay_Model.Id);
            }
            else
            {
                SeanceDay = new SeanceDay();
            } 
			SeanceDay.Name = Default_Details_SeanceDay_Model.Name;
			SeanceDay.Code = Default_Details_SeanceDay_Model.Code;
			SeanceDay.Day = Default_Details_SeanceDay_Model.Day;
			SeanceDay.Description = Default_Details_SeanceDay_Model.Description;
			SeanceDay.Id = Default_Details_SeanceDay_Model.Id;
            return SeanceDay;
        }
        public virtual Default_Details_SeanceDay_Model ConverTo_Default_Details_SeanceDay_Model(SeanceDay SeanceDay)
        {  
			Default_Details_SeanceDay_Model Default_Details_SeanceDay_Model = new Default_Details_SeanceDay_Model();
			Default_Details_SeanceDay_Model.toStringValue = SeanceDay.ToString();
			Default_Details_SeanceDay_Model.Name = SeanceDay.Name;
			Default_Details_SeanceDay_Model.Code = SeanceDay.Code;
			Default_Details_SeanceDay_Model.Day = SeanceDay.Day;
			Default_Details_SeanceDay_Model.Description = SeanceDay.Description;
			Default_Details_SeanceDay_Model.Id = SeanceDay.Id;
            return Default_Details_SeanceDay_Model;            
        }

		public virtual Default_Details_SeanceDay_Model CreateNew()
        {
            SeanceDay SeanceDay = new SeanceDayBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_SeanceDay_Model Default_Details_SeanceDay_Model = this.ConverTo_Default_Details_SeanceDay_Model(SeanceDay);
            return Default_Details_SeanceDay_Model;
        } 

		public virtual List<Default_Details_SeanceDay_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SeanceDayBLO entityBLO = new SeanceDayBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<SeanceDay> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_SeanceDay_Model> ls_models = new List<Default_Details_SeanceDay_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_SeanceDay_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_SeanceDay_ModelBLM : BaseDefault_Details_SeanceDay_ModelBLM
	{
		public Default_Details_SeanceDay_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
