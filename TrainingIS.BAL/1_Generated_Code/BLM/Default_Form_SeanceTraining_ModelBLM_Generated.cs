//modelType = Default_Form_SeanceTraining_Model

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
	public partial class BaseDefault_Form_SeanceTraining_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_SeanceTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SeanceTraining ConverTo_SeanceTraining(Default_Form_SeanceTraining_Model Default_Form_SeanceTraining_Model)
        {
			SeanceTraining SeanceTraining = null;
            if (Default_Form_SeanceTraining_Model.Id != 0)
            {
                SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_SeanceTraining_Model.Id);
            }
            else
            {
                SeanceTraining = new SeanceTraining();
            } 
			SeanceTraining.SeanceDate = Default_Form_SeanceTraining_Model.SeanceDate;
			SeanceTraining.SeancePlanningId = Default_Form_SeanceTraining_Model.SeancePlanningId;
			SeanceTraining.SeancePlanning = new SeancePlanningBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_SeanceTraining_Model.SeancePlanningId)) ;
			SeanceTraining.Contained = Default_Form_SeanceTraining_Model.Contained;
			SeanceTraining.FormerValidation = Default_Form_SeanceTraining_Model.FormerValidation;
			// Absences
            AbsenceBLO AbsencesBLO = new AbsenceBLO(this.UnitOfWork,this.GAppContext);
			if (SeanceTraining.Absences != null)
                SeanceTraining.Absences.Clear();
            else
                SeanceTraining.Absences = new List<Absence>();
			if(Default_Form_SeanceTraining_Model.Selected_Absences != null)
			{
				foreach (string Selected_Absence_Id in Default_Form_SeanceTraining_Model.Selected_Absences)
				{
					Int64 Selected_Absence_Id_Int64 = Convert.ToInt64(Selected_Absence_Id);
					Absence Absence =AbsencesBLO.FindBaseEntityByID(Selected_Absence_Id_Int64);
					SeanceTraining.Absences.Add(Absence);
				}
			}
	
			SeanceTraining.Reference = Default_Form_SeanceTraining_Model.Reference;
			SeanceTraining.Id = Default_Form_SeanceTraining_Model.Id;
            return SeanceTraining;
        }
        public virtual Default_Form_SeanceTraining_Model ConverTo_Default_Form_SeanceTraining_Model(SeanceTraining SeanceTraining)
        {  
			Default_Form_SeanceTraining_Model Default_Form_SeanceTraining_Model = new Default_Form_SeanceTraining_Model();
			Default_Form_SeanceTraining_Model.toStringValue = SeanceTraining.ToString();
			Default_Form_SeanceTraining_Model.SeanceDate = ConversionUtil.DefaultValue_if_Null<DateTime>(SeanceTraining.SeanceDate);
			Default_Form_SeanceTraining_Model.SeancePlanningId = SeanceTraining.SeancePlanningId;
			Default_Form_SeanceTraining_Model.Contained = SeanceTraining.Contained;
			Default_Form_SeanceTraining_Model.FormerValidation = SeanceTraining.FormerValidation;

			// Absences
            if (SeanceTraining.Absences != null && SeanceTraining.Absences.Count > 0)
            {
                Default_Form_SeanceTraining_Model.Selected_Absences = SeanceTraining
                                                        .Absences
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_Form_SeanceTraining_Model.Selected_Absences = new List<string>();
            }			
			Default_Form_SeanceTraining_Model.Id = SeanceTraining.Id;
			Default_Form_SeanceTraining_Model.Reference = SeanceTraining.Reference;
            return Default_Form_SeanceTraining_Model;            
        }

		public virtual Default_Form_SeanceTraining_Model CreateNew()
        {
            SeanceTraining SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_SeanceTraining_Model Default_Form_SeanceTraining_Model = this.ConverTo_Default_Form_SeanceTraining_Model(SeanceTraining);
            return Default_Form_SeanceTraining_Model;
        } 

		public virtual List<Default_Form_SeanceTraining_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SeanceTrainingBLO entityBLO = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<SeanceTraining> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_SeanceTraining_Model> ls_models = new List<Default_Form_SeanceTraining_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_SeanceTraining_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_SeanceTraining_ModelBLM : BaseDefault_Form_SeanceTraining_ModelBLM
	{
		public Default_Form_SeanceTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
