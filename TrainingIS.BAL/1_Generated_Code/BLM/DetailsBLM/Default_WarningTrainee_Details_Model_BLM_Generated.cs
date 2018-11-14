//modelType = Default_WarningTrainee_Details_Model

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
	public partial class BaseDefault_WarningTrainee_Details_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_WarningTrainee_Details_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual WarningTrainee ConverTo_WarningTrainee(Default_WarningTrainee_Details_Model Default_WarningTrainee_Details_Model)
        {
			WarningTrainee WarningTrainee = null;
            if (Default_WarningTrainee_Details_Model.Id != 0)
            {
                WarningTrainee = new WarningTraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_WarningTrainee_Details_Model.Id);
            }
            else
            {
                WarningTrainee = new WarningTrainee();
            } 
			WarningTrainee.Trainee = Default_WarningTrainee_Details_Model.Trainee;
			WarningTrainee.WarningDate = DefaultDateTime_If_Empty(Default_WarningTrainee_Details_Model.WarningDate);
			WarningTrainee.Category_WarningTrainee = Default_WarningTrainee_Details_Model.Category_WarningTrainee;
			WarningTrainee.Description = Default_WarningTrainee_Details_Model.Description;
			WarningTrainee.Id = Default_WarningTrainee_Details_Model.Id;
            return WarningTrainee;
        }
        public virtual Default_WarningTrainee_Details_Model ConverTo_Default_WarningTrainee_Details_Model(WarningTrainee WarningTrainee)
        {  
			Default_WarningTrainee_Details_Model Default_WarningTrainee_Details_Model = new Default_WarningTrainee_Details_Model();
			Default_WarningTrainee_Details_Model.toStringValue = WarningTrainee.ToString();
			Default_WarningTrainee_Details_Model.Trainee = WarningTrainee.Trainee;
			Default_WarningTrainee_Details_Model.WarningDate = DefaultDateTime_If_Empty(WarningTrainee.WarningDate);
			Default_WarningTrainee_Details_Model.Category_WarningTrainee = WarningTrainee.Category_WarningTrainee;
			Default_WarningTrainee_Details_Model.Description = WarningTrainee.Description;
			Default_WarningTrainee_Details_Model.Id = WarningTrainee.Id;
            return Default_WarningTrainee_Details_Model;            
        }

		public virtual Default_WarningTrainee_Details_Model CreateNew()
        {
            WarningTrainee WarningTrainee = new WarningTraineeBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_WarningTrainee_Details_Model Default_WarningTrainee_Details_Model = this.ConverTo_Default_WarningTrainee_Details_Model(WarningTrainee);
            return Default_WarningTrainee_Details_Model;
        } 

		public virtual List<Default_WarningTrainee_Details_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            WarningTraineeBLO entityBLO = new WarningTraineeBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<WarningTrainee> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_WarningTrainee_Details_Model> ls_models = new List<Default_WarningTrainee_Details_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_WarningTrainee_Details_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_WarningTrainee_Details_ModelBLM : BaseDefault_WarningTrainee_Details_Model_BLM
	{
		public Default_WarningTrainee_Details_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
