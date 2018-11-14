//modelType = Default_Sanction_Create_Model

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
	public partial class BaseDefault_Sanction_Create_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		private Default_Form_Sanction_ModelBLM Default_Form_Sanction_ModelBLM {set;get;}
        
		public BaseDefault_Sanction_Create_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_Sanction_ModelBLM = new Default_Form_Sanction_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Sanction ConverTo_Sanction(Default_Sanction_Create_Model Default_Sanction_Create_Model)
        {
            var Sanction = Default_Form_Sanction_ModelBLM.ConverTo_Sanction(Default_Sanction_Create_Model);
            return Sanction;
        }

		public virtual Default_Sanction_Create_Model ConverTo_Default_Sanction_Create_Model(Sanction Sanction)
        {
            Default_Sanction_Create_Model Default_Sanction_Create_Model = new Default_Sanction_Create_Model();
            Default_Form_Sanction_ModelBLM.ConverTo_Default_Form_Sanction_Model(Default_Sanction_Create_Model, Sanction);
            return Default_Sanction_Create_Model;            
        }

		public virtual Default_Sanction_Create_Model CreateNew()
        {
            Sanction Sanction = new SanctionBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Sanction_Create_Model Default_Sanction_Create_Model = this.ConverTo_Default_Sanction_Create_Model(Sanction);
            return Default_Sanction_Create_Model;
        } 

		public virtual List<Default_Sanction_Create_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SanctionBLO entityBLO = new SanctionBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Sanction> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Sanction_Create_Model> ls_models = new List<Default_Sanction_Create_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Sanction_Create_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Sanction_Create_ModelBLM : BaseDefault_Sanction_Create_Model_BLM
	{
		public Default_Sanction_Create_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
