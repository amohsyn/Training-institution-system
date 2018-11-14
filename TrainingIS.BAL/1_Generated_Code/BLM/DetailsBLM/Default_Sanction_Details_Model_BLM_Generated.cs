//modelType = Default_Sanction_Details_Model

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
	public partial class BaseDefault_Sanction_Details_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Sanction_Details_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Sanction ConverTo_Sanction(Default_Sanction_Details_Model Default_Sanction_Details_Model)
        {
			Sanction Sanction = null;
            if (Default_Sanction_Details_Model.Id != 0)
            {
                Sanction = new SanctionBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Sanction_Details_Model.Id);
            }
            else
            {
                Sanction = new Sanction();
            } 
			Sanction.Trainee = Default_Sanction_Details_Model.Trainee;
			Sanction.SanctionCategory = Default_Sanction_Details_Model.SanctionCategory;
			Sanction.SanctionState = Default_Sanction_Details_Model.SanctionState;
			Sanction.Meeting = Default_Sanction_Details_Model.Meeting;
			Sanction.Id = Default_Sanction_Details_Model.Id;
            return Sanction;
        }
        public virtual Default_Sanction_Details_Model ConverTo_Default_Sanction_Details_Model(Sanction Sanction)
        {  
			Default_Sanction_Details_Model Default_Sanction_Details_Model = new Default_Sanction_Details_Model();
			Default_Sanction_Details_Model.toStringValue = Sanction.ToString();
			Default_Sanction_Details_Model.Trainee = Sanction.Trainee;
			Default_Sanction_Details_Model.SanctionCategory = Sanction.SanctionCategory;
			Default_Sanction_Details_Model.SanctionState = Sanction.SanctionState;
			Default_Sanction_Details_Model.Meeting = Sanction.Meeting;
			Default_Sanction_Details_Model.Id = Sanction.Id;
            return Default_Sanction_Details_Model;            
        }

		public virtual Default_Sanction_Details_Model CreateNew()
        {
            Sanction Sanction = new SanctionBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Sanction_Details_Model Default_Sanction_Details_Model = this.ConverTo_Default_Sanction_Details_Model(Sanction);
            return Default_Sanction_Details_Model;
        } 

		public virtual List<Default_Sanction_Details_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SanctionBLO entityBLO = new SanctionBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Sanction> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Sanction_Details_Model> ls_models = new List<Default_Sanction_Details_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Sanction_Details_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Sanction_Details_ModelBLM : BaseDefault_Sanction_Details_Model_BLM
	{
		public Default_Sanction_Details_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
