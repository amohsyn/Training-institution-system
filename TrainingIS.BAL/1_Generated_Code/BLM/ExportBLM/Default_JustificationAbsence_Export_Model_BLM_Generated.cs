//modelType = Default_JustificationAbsence_Export_Model

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
	public partial class BaseDefault_JustificationAbsence_Export_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_JustificationAbsence_Export_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual JustificationAbsence ConverTo_JustificationAbsence(Default_JustificationAbsence_Export_Model Default_JustificationAbsence_Export_Model)
        {
			JustificationAbsence JustificationAbsence = null;
            if (Default_JustificationAbsence_Export_Model.Id != 0)
            {
                JustificationAbsence = new JustificationAbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_JustificationAbsence_Export_Model.Id);
            }
            else
            {
                JustificationAbsence = new JustificationAbsence();
            } 
			JustificationAbsence.Trainee = Default_JustificationAbsence_Export_Model.Trainee;
			JustificationAbsence.Category_JustificationAbsence = Default_JustificationAbsence_Export_Model.Category_JustificationAbsence;
			JustificationAbsence.StartDate = DefaultDateTime_If_Empty(Default_JustificationAbsence_Export_Model.StartDate);
			JustificationAbsence.EndtDate = DefaultDateTime_If_Empty(Default_JustificationAbsence_Export_Model.EndtDate);
			JustificationAbsence.Description = Default_JustificationAbsence_Export_Model.Description;
			JustificationAbsence.Id = Default_JustificationAbsence_Export_Model.Id;
            return JustificationAbsence;
        }
        public virtual Default_JustificationAbsence_Export_Model ConverTo_Default_JustificationAbsence_Export_Model(JustificationAbsence JustificationAbsence)
        {  
			Default_JustificationAbsence_Export_Model Default_JustificationAbsence_Export_Model = new Default_JustificationAbsence_Export_Model();
			Default_JustificationAbsence_Export_Model.toStringValue = JustificationAbsence.ToString();
			Default_JustificationAbsence_Export_Model.Trainee = JustificationAbsence.Trainee;
			Default_JustificationAbsence_Export_Model.Category_JustificationAbsence = JustificationAbsence.Category_JustificationAbsence;
			Default_JustificationAbsence_Export_Model.StartDate = DefaultDateTime_If_Empty(JustificationAbsence.StartDate);
			Default_JustificationAbsence_Export_Model.EndtDate = DefaultDateTime_If_Empty(JustificationAbsence.EndtDate);
			Default_JustificationAbsence_Export_Model.Description = JustificationAbsence.Description;
			Default_JustificationAbsence_Export_Model.Id = JustificationAbsence.Id;
            return Default_JustificationAbsence_Export_Model;            
        }

		public virtual Default_JustificationAbsence_Export_Model CreateNew()
        {
            JustificationAbsence JustificationAbsence = new JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_JustificationAbsence_Export_Model Default_JustificationAbsence_Export_Model = this.ConverTo_Default_JustificationAbsence_Export_Model(JustificationAbsence);
            return Default_JustificationAbsence_Export_Model;
        } 

		public virtual List<Default_JustificationAbsence_Export_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            JustificationAbsenceBLO entityBLO = new JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<JustificationAbsence> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_JustificationAbsence_Export_Model> ls_models = new List<Default_JustificationAbsence_Export_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_JustificationAbsence_Export_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_JustificationAbsence_Export_ModelBLM : BaseDefault_JustificationAbsence_Export_Model_BLM
	{
		public Default_JustificationAbsence_Export_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
