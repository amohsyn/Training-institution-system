//modelType = Create_Former_Model

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
	public partial class BaseCreate_Former_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		private FormerFormViewBLM FormerFormViewBLM {set;get;}
        
		public BaseCreate_Former_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			FormerFormViewBLM = new FormerFormViewBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Former ConverTo_Former(Create_Former_Model Create_Former_Model)
        {
            var Former = FormerFormViewBLM.ConverTo_Former(Create_Former_Model);
            return Former;
        }

		public virtual Create_Former_Model ConverTo_Create_Former_Model(Former Former)
        {
            Create_Former_Model Create_Former_Model = new Create_Former_Model();
            FormerFormViewBLM.ConverTo_FormerFormView(Create_Former_Model, Former);
            return Create_Former_Model;            
        }

		public virtual Create_Former_Model CreateNew()
        {
            Former Former = new FormerBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Create_Former_Model Create_Former_Model = this.ConverTo_Create_Former_Model(Former);
            return Create_Former_Model;
        } 

		public virtual List<Create_Former_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            FormerBLO entityBLO = new FormerBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Former> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Create_Former_Model> ls_models = new List<Create_Former_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Create_Former_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Create_Former_ModelBLM : BaseCreate_Former_Model_BLM
	{
		public Create_Former_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
