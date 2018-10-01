//modelType = Default_Form_WarningTrainee_Model

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
	public partial class BaseDefault_Form_WarningTrainee_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_WarningTrainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual WarningTrainee ConverTo_WarningTrainee(Default_Form_WarningTrainee_Model Default_Form_WarningTrainee_Model)
        {
			WarningTrainee WarningTrainee = null;
            if (Default_Form_WarningTrainee_Model.Id != 0)
            {
                WarningTrainee = new WarningTraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_WarningTrainee_Model.Id);
            }
            else
            {
                WarningTrainee = new WarningTrainee();
            } 
			WarningTrainee.TraineeId = Default_Form_WarningTrainee_Model.TraineeId;
			WarningTrainee.Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_WarningTrainee_Model.TraineeId)) ;
			WarningTrainee.WarningDate = DefaultDateTime_If_Empty(Default_Form_WarningTrainee_Model.WarningDate);
			WarningTrainee.Category_WarningTraineeId = Default_Form_WarningTrainee_Model.Category_WarningTraineeId;
			WarningTrainee.Category_WarningTrainee = new Category_WarningTraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_WarningTrainee_Model.Category_WarningTraineeId)) ;
			WarningTrainee.Description = Default_Form_WarningTrainee_Model.Description;
			WarningTrainee.Id = Default_Form_WarningTrainee_Model.Id;
            return WarningTrainee;
        }
        public virtual Default_Form_WarningTrainee_Model ConverTo_Default_Form_WarningTrainee_Model(WarningTrainee WarningTrainee)
        {  
			Default_Form_WarningTrainee_Model Default_Form_WarningTrainee_Model = new Default_Form_WarningTrainee_Model();
			Default_Form_WarningTrainee_Model.toStringValue = WarningTrainee.ToString();
			Default_Form_WarningTrainee_Model.TraineeId = WarningTrainee.TraineeId;
			Default_Form_WarningTrainee_Model.WarningDate = DefaultDateTime_If_Empty(WarningTrainee.WarningDate);
			Default_Form_WarningTrainee_Model.Category_WarningTraineeId = WarningTrainee.Category_WarningTraineeId;
			Default_Form_WarningTrainee_Model.Description = WarningTrainee.Description;
			Default_Form_WarningTrainee_Model.Id = WarningTrainee.Id;
            return Default_Form_WarningTrainee_Model;            
        }

		public virtual Default_Form_WarningTrainee_Model CreateNew()
        {
            WarningTrainee WarningTrainee = new WarningTraineeBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_WarningTrainee_Model Default_Form_WarningTrainee_Model = this.ConverTo_Default_Form_WarningTrainee_Model(WarningTrainee);
            return Default_Form_WarningTrainee_Model;
        } 

		public List<Default_Form_WarningTrainee_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            WarningTraineeBLO entityBLO = new WarningTraineeBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<WarningTrainee> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_WarningTrainee_Model> ls_models = new List<Default_Form_WarningTrainee_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_WarningTrainee_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_WarningTrainee_ModelBLM : BaseDefault_Form_WarningTrainee_ModelBLM
	{
		public Default_Form_WarningTrainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
