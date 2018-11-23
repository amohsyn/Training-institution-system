//modelType = Sanction_Create_Model

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
	public partial class BaseSanction_Create_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		private Form_Sanction_ModelBLM Form_Sanction_ModelBLM {set;get;}
        
		public BaseSanction_Create_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Form_Sanction_ModelBLM = new Form_Sanction_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Sanction ConverTo_Sanction(Sanction_Create_Model Sanction_Create_Model)
        {
            var Sanction = Form_Sanction_ModelBLM.ConverTo_Sanction(Sanction_Create_Model);
            return Sanction;
        }

		public virtual Sanction_Create_Model ConverTo_Sanction_Create_Model(Sanction Sanction)
        {
            Sanction_Create_Model Sanction_Create_Model = new Sanction_Create_Model();
            Form_Sanction_ModelBLM.ConverTo_Form_Sanction_Model(Sanction_Create_Model, Sanction);
            return Sanction_Create_Model;            
        }

		public virtual Sanction_Create_Model CreateNew()
        {
            Sanction Sanction = new SanctionBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Sanction_Create_Model Sanction_Create_Model = this.ConverTo_Sanction_Create_Model(Sanction);
            return Sanction_Create_Model;
        } 

		public virtual List<Sanction_Create_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SanctionBLO entityBLO = new SanctionBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Sanction> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Sanction_Create_Model> ls_models = new List<Sanction_Create_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Sanction_Create_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Sanction_Create_ModelBLM : BaseSanction_Create_Model_BLM
	{
		public Sanction_Create_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
