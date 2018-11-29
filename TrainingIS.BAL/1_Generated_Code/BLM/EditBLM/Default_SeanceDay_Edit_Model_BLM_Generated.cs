//modelType = Default_SeanceDay_Edit_Model

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
	public partial class BaseDefault_SeanceDay_Edit_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		public Default_Form_SeanceDay_ModelBLM Default_Form_SeanceDay_ModelBLM {set;get;}
        
		public BaseDefault_SeanceDay_Edit_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_SeanceDay_ModelBLM = new Default_Form_SeanceDay_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual SeanceDay ConverTo_SeanceDay(Default_SeanceDay_Edit_Model Default_SeanceDay_Edit_Model)
        {
            var SeanceDay = Default_Form_SeanceDay_ModelBLM.ConverTo_SeanceDay(Default_SeanceDay_Edit_Model);
            return SeanceDay;
        }

		public virtual Default_SeanceDay_Edit_Model ConverTo_Default_SeanceDay_Edit_Model(SeanceDay SeanceDay)
        {
            Default_SeanceDay_Edit_Model Default_SeanceDay_Edit_Model = new Default_SeanceDay_Edit_Model();
            Default_Form_SeanceDay_ModelBLM.ConverTo_Default_Form_SeanceDay_Model(Default_SeanceDay_Edit_Model, SeanceDay);
            return Default_SeanceDay_Edit_Model;            
        }

		public virtual Default_SeanceDay_Edit_Model CreateNew()
        {
            SeanceDay SeanceDay = new SeanceDayBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_SeanceDay_Edit_Model Default_SeanceDay_Edit_Model = this.ConverTo_Default_SeanceDay_Edit_Model(SeanceDay);
            return Default_SeanceDay_Edit_Model;
        } 

		public virtual List<Default_SeanceDay_Edit_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SeanceDayBLO entityBLO = new SeanceDayBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<SeanceDay> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_SeanceDay_Edit_Model> ls_models = new List<Default_SeanceDay_Edit_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_SeanceDay_Edit_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_SeanceDay_Edit_ModelBLM : BaseDefault_SeanceDay_Edit_Model_BLM
	{
		public Default_SeanceDay_Edit_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
