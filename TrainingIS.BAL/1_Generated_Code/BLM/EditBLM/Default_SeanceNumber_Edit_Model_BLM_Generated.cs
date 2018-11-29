//modelType = Default_SeanceNumber_Edit_Model

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
	public partial class BaseDefault_SeanceNumber_Edit_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		public Default_Form_SeanceNumber_ModelBLM Default_Form_SeanceNumber_ModelBLM {set;get;}
        
		public BaseDefault_SeanceNumber_Edit_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_SeanceNumber_ModelBLM = new Default_Form_SeanceNumber_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual SeanceNumber ConverTo_SeanceNumber(Default_SeanceNumber_Edit_Model Default_SeanceNumber_Edit_Model)
        {
            var SeanceNumber = Default_Form_SeanceNumber_ModelBLM.ConverTo_SeanceNumber(Default_SeanceNumber_Edit_Model);
            return SeanceNumber;
        }

		public virtual Default_SeanceNumber_Edit_Model ConverTo_Default_SeanceNumber_Edit_Model(SeanceNumber SeanceNumber)
        {
            Default_SeanceNumber_Edit_Model Default_SeanceNumber_Edit_Model = new Default_SeanceNumber_Edit_Model();
            Default_Form_SeanceNumber_ModelBLM.ConverTo_Default_Form_SeanceNumber_Model(Default_SeanceNumber_Edit_Model, SeanceNumber);
            return Default_SeanceNumber_Edit_Model;            
        }

		public virtual Default_SeanceNumber_Edit_Model CreateNew()
        {
            SeanceNumber SeanceNumber = new SeanceNumberBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_SeanceNumber_Edit_Model Default_SeanceNumber_Edit_Model = this.ConverTo_Default_SeanceNumber_Edit_Model(SeanceNumber);
            return Default_SeanceNumber_Edit_Model;
        } 

		public virtual List<Default_SeanceNumber_Edit_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SeanceNumberBLO entityBLO = new SeanceNumberBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<SeanceNumber> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_SeanceNumber_Edit_Model> ls_models = new List<Default_SeanceNumber_Edit_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_SeanceNumber_Edit_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_SeanceNumber_Edit_ModelBLM : BaseDefault_SeanceNumber_Edit_Model_BLM
	{
		public Default_SeanceNumber_Edit_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
