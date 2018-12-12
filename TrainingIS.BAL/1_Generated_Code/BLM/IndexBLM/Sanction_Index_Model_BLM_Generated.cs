//modelType = Sanction_Index_Model

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
	public partial class BaseSanction_Index_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseSanction_Index_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Sanction ConverTo_Sanction(Sanction_Index_Model Sanction_Index_Model)
        {
			Sanction Sanction = null;
            if (Sanction_Index_Model.Id != 0)
            {
                Sanction = new SanctionBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Sanction_Index_Model.Id);
            }
            else
            {
                Sanction = new Sanction();
            } 
			Sanction.Trainee = Sanction_Index_Model.Trainee;
			Sanction.SanctionCategory = Sanction_Index_Model.SanctionCategory;
			Sanction.SanctionState = Sanction_Index_Model.SanctionState;
			Sanction.Meeting = Sanction_Index_Model.Meeting;
			Sanction.Id = Sanction_Index_Model.Id;
            return Sanction;
        }
        public virtual Sanction_Index_Model ConverTo_Sanction_Index_Model(Sanction Sanction)
        {  
			Sanction_Index_Model Sanction_Index_Model = new Sanction_Index_Model();
			Sanction_Index_Model.toStringValue = Sanction.ToString();
			Sanction_Index_Model.Trainee = Sanction.Trainee;
			Sanction_Index_Model.SanctionCategory = Sanction.SanctionCategory;
			Sanction_Index_Model.SanctionState = Sanction.SanctionState;
			Sanction_Index_Model.Meeting = Sanction.Meeting;
			Sanction_Index_Model.Id = Sanction.Id;
            return Sanction_Index_Model;            
        }

		public virtual Sanction_Index_Model CreateNew()
        {
            Sanction Sanction = new SanctionBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Sanction_Index_Model Sanction_Index_Model = this.ConverTo_Sanction_Index_Model(Sanction);
            return Sanction_Index_Model;
        } 

		public virtual List<Sanction_Index_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SanctionBLO entityBLO = new SanctionBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Sanction> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Sanction_Index_Model> ls_models = new List<Sanction_Index_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Sanction_Index_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Sanction_Index_ModelBLM : BaseSanction_Index_Model_BLM
	{
		public Sanction_Index_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
