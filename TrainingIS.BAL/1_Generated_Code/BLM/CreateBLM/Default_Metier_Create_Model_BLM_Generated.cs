//modelType = Default_Metier_Create_Model

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
	public partial class BaseDefault_Metier_Create_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		private Default_Form_Metier_ModelBLM Default_Form_Metier_ModelBLM {set;get;}
        
		public BaseDefault_Metier_Create_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_Metier_ModelBLM = new Default_Form_Metier_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Metier ConverTo_Metier(Default_Metier_Create_Model Default_Metier_Create_Model)
        {
            var Metier = Default_Form_Metier_ModelBLM.ConverTo_Metier(Default_Metier_Create_Model);
            return Metier;
        }

		public virtual Default_Metier_Create_Model ConverTo_Default_Metier_Create_Model(Metier Metier)
        {
            Default_Metier_Create_Model Default_Metier_Create_Model = new Default_Metier_Create_Model();
            Default_Form_Metier_ModelBLM.ConverTo_Default_Form_Metier_Model(Default_Metier_Create_Model, Metier);
            return Default_Metier_Create_Model;            
        }

		public virtual Default_Metier_Create_Model CreateNew()
        {
            Metier Metier = new MetierBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Metier_Create_Model Default_Metier_Create_Model = this.ConverTo_Default_Metier_Create_Model(Metier);
            return Default_Metier_Create_Model;
        } 

		public virtual List<Default_Metier_Create_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            MetierBLO entityBLO = new MetierBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Metier> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Metier_Create_Model> ls_models = new List<Default_Metier_Create_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Metier_Create_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Metier_Create_ModelBLM : BaseDefault_Metier_Create_Model_BLM
	{
		public Default_Metier_Create_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
