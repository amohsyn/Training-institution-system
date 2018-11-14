//modelType = Export_Sanction_Model

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
	public partial class BaseExport_Sanction_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseExport_Sanction_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Sanction ConverTo_Sanction(Export_Sanction_Model Export_Sanction_Model)
        {
			Sanction Sanction = null;
            if (Export_Sanction_Model.Id != 0)
            {
                Sanction = new SanctionBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Export_Sanction_Model.Id);
            }
            else
            {
                Sanction = new Sanction();
            } 
			Sanction.Trainee = Export_Sanction_Model.Trainee;
			Sanction.SanctionCategory = Export_Sanction_Model.SanctionCategory;
			Sanction.SanctionState = Export_Sanction_Model.SanctionState;
			Sanction.Meeting = Export_Sanction_Model.Meeting;
			Sanction.Id = Export_Sanction_Model.Id;
            return Sanction;
        }
        public virtual Export_Sanction_Model ConverTo_Export_Sanction_Model(Sanction Sanction)
        {  
			Export_Sanction_Model Export_Sanction_Model = new Export_Sanction_Model();
			Export_Sanction_Model.toStringValue = Sanction.ToString();
			Export_Sanction_Model.Trainee = Sanction.Trainee;
			Export_Sanction_Model.SanctionCategory = Sanction.SanctionCategory;
			Export_Sanction_Model.SanctionState = Sanction.SanctionState;
			Export_Sanction_Model.Meeting = Sanction.Meeting;
			Export_Sanction_Model.Id = Sanction.Id;
            return Export_Sanction_Model;            
        }

		public virtual Export_Sanction_Model CreateNew()
        {
            Sanction Sanction = new SanctionBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Export_Sanction_Model Export_Sanction_Model = this.ConverTo_Export_Sanction_Model(Sanction);
            return Export_Sanction_Model;
        } 

		public virtual List<Export_Sanction_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SanctionBLO entityBLO = new SanctionBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Sanction> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Export_Sanction_Model> ls_models = new List<Export_Sanction_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Export_Sanction_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Export_Sanction_ModelBLM : BaseExport_Sanction_Model_BLM
	{
		public Export_Sanction_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
