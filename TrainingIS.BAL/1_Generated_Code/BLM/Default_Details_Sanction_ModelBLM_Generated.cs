//modelType = Default_Details_Sanction_Model

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
	public partial class BaseDefault_Details_Sanction_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Sanction_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Sanction ConverTo_Sanction(Default_Details_Sanction_Model Default_Details_Sanction_Model)
        {
			Sanction Sanction = null;
            if (Default_Details_Sanction_Model.Id != 0)
            {
                Sanction = new SanctionBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Sanction_Model.Id);
            }
            else
            {
                Sanction = new Sanction();
            } 
			Sanction.SanctionCategory = Default_Details_Sanction_Model.SanctionCategory;
			Sanction.Id = Default_Details_Sanction_Model.Id;
            return Sanction;
        }
        public virtual Default_Details_Sanction_Model ConverTo_Default_Details_Sanction_Model(Sanction Sanction)
        {  
			Default_Details_Sanction_Model Default_Details_Sanction_Model = new Default_Details_Sanction_Model();
			Default_Details_Sanction_Model.toStringValue = Sanction.ToString();
			Default_Details_Sanction_Model.SanctionCategory = Sanction.SanctionCategory;
			Default_Details_Sanction_Model.Id = Sanction.Id;
            return Default_Details_Sanction_Model;            
        }

		public virtual Default_Details_Sanction_Model CreateNew()
        {
            Sanction Sanction = new SanctionBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Sanction_Model Default_Details_Sanction_Model = this.ConverTo_Default_Details_Sanction_Model(Sanction);
            return Default_Details_Sanction_Model;
        } 

		public virtual List<Default_Details_Sanction_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SanctionBLO entityBLO = new SanctionBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Sanction> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Sanction_Model> ls_models = new List<Default_Details_Sanction_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Sanction_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Sanction_ModelBLM : BaseDefault_Details_Sanction_ModelBLM
	{
		public Default_Details_Sanction_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
