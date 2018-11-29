//modelType = Edit_Former_Model

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
using TrainingIS.Models.FormerModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseEdit_Former_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		public FormerFormViewBLM FormerFormViewBLM {set;get;}
        
		public BaseEdit_Former_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			FormerFormViewBLM = new FormerFormViewBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Former ConverTo_Former(Edit_Former_Model Edit_Former_Model)
        {
            var Former = FormerFormViewBLM.ConverTo_Former(Edit_Former_Model);
            return Former;
        }

		public virtual Edit_Former_Model ConverTo_Edit_Former_Model(Former Former)
        {
            Edit_Former_Model Edit_Former_Model = new Edit_Former_Model();
            FormerFormViewBLM.ConverTo_FormerFormView(Edit_Former_Model, Former);
            return Edit_Former_Model;            
        }

		public virtual Edit_Former_Model CreateNew()
        {
            Former Former = new FormerBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Edit_Former_Model Edit_Former_Model = this.ConverTo_Edit_Former_Model(Former);
            return Edit_Former_Model;
        } 

		public virtual List<Edit_Former_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            FormerBLO entityBLO = new FormerBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Former> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Edit_Former_Model> ls_models = new List<Edit_Former_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Edit_Former_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Edit_Former_ModelBLM : BaseEdit_Former_Model_BLM
	{
		public Edit_Former_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
