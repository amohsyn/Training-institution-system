//modelType = Default_Details_JustificationAbsence_Model

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
	public partial class BaseDefault_Details_JustificationAbsence_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_JustificationAbsence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual JustificationAbsence ConverTo_JustificationAbsence(Default_Details_JustificationAbsence_Model Default_Details_JustificationAbsence_Model)
        {
			JustificationAbsence JustificationAbsence = null;
            if (Default_Details_JustificationAbsence_Model.Id != 0)
            {
                JustificationAbsence = new JustificationAbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_JustificationAbsence_Model.Id);
            }
            else
            {
                JustificationAbsence = new JustificationAbsence();
            } 
			JustificationAbsence.Trainee = Default_Details_JustificationAbsence_Model.Trainee;
			JustificationAbsence.Category_JustificationAbsence = Default_Details_JustificationAbsence_Model.Category_JustificationAbsence;
			JustificationAbsence.StartDate = DefaultDateTime_If_Empty(Default_Details_JustificationAbsence_Model.StartDate);
			JustificationAbsence.EndtDate = DefaultDateTime_If_Empty(Default_Details_JustificationAbsence_Model.EndtDate);
			JustificationAbsence.Description = Default_Details_JustificationAbsence_Model.Description;
			JustificationAbsence.Id = Default_Details_JustificationAbsence_Model.Id;
            return JustificationAbsence;
        }
        public virtual Default_Details_JustificationAbsence_Model ConverTo_Default_Details_JustificationAbsence_Model(JustificationAbsence JustificationAbsence)
        {  
			Default_Details_JustificationAbsence_Model Default_Details_JustificationAbsence_Model = new Default_Details_JustificationAbsence_Model();
			Default_Details_JustificationAbsence_Model.toStringValue = JustificationAbsence.ToString();
			Default_Details_JustificationAbsence_Model.Trainee = JustificationAbsence.Trainee;
			Default_Details_JustificationAbsence_Model.Category_JustificationAbsence = JustificationAbsence.Category_JustificationAbsence;
			Default_Details_JustificationAbsence_Model.StartDate = DefaultDateTime_If_Empty(JustificationAbsence.StartDate);
			Default_Details_JustificationAbsence_Model.EndtDate = DefaultDateTime_If_Empty(JustificationAbsence.EndtDate);
			Default_Details_JustificationAbsence_Model.Description = JustificationAbsence.Description;
			Default_Details_JustificationAbsence_Model.Id = JustificationAbsence.Id;
            return Default_Details_JustificationAbsence_Model;            
        }

		public virtual Default_Details_JustificationAbsence_Model CreateNew()
        {
            JustificationAbsence JustificationAbsence = new JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_JustificationAbsence_Model Default_Details_JustificationAbsence_Model = this.ConverTo_Default_Details_JustificationAbsence_Model(JustificationAbsence);
            return Default_Details_JustificationAbsence_Model;
        } 

		public List<Default_Details_JustificationAbsence_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            JustificationAbsenceBLO entityBLO = new JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<JustificationAbsence> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_JustificationAbsence_Model> ls_models = new List<Default_Details_JustificationAbsence_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_JustificationAbsence_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_JustificationAbsence_ModelBLM : BaseDefault_Details_JustificationAbsence_ModelBLM
	{
		public Default_Details_JustificationAbsence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
