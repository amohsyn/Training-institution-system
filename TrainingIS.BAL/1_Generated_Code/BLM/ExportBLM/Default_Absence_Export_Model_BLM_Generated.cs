//modelType = Default_Absence_Export_Model

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
	public partial class BaseDefault_Absence_Export_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Absence_Export_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Absence ConverTo_Absence(Default_Absence_Export_Model Default_Absence_Export_Model)
        {
			Absence Absence = null;
            if (Default_Absence_Export_Model.Id != 0)
            {
                Absence = new AbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Absence_Export_Model.Id);
            }
            else
            {
                Absence = new Absence();
            } 
			Absence.AbsenceDate = DefaultDateTime_If_Empty(Default_Absence_Export_Model.AbsenceDate);
			Absence.SeanceTraining = Default_Absence_Export_Model.SeanceTraining;
			Absence.Trainee = Default_Absence_Export_Model.Trainee;
			Absence.FormerComment = Default_Absence_Export_Model.FormerComment;
			Absence.TraineeComment = Default_Absence_Export_Model.TraineeComment;
			Absence.SupervisorComment = Default_Absence_Export_Model.SupervisorComment;
			Absence.isHaveAuthorization = Default_Absence_Export_Model.isHaveAuthorization;
			Absence.Valide = Default_Absence_Export_Model.Valide;
			Absence.AbsenceState = Default_Absence_Export_Model.AbsenceState;
			Absence.Id = Default_Absence_Export_Model.Id;
            return Absence;
        }
        public virtual Default_Absence_Export_Model ConverTo_Default_Absence_Export_Model(Absence Absence)
        {  
			Default_Absence_Export_Model Default_Absence_Export_Model = new Default_Absence_Export_Model();
			Default_Absence_Export_Model.toStringValue = Absence.ToString();
			Default_Absence_Export_Model.AbsenceDate = DefaultDateTime_If_Empty(Absence.AbsenceDate);
			Default_Absence_Export_Model.SeanceTraining = Absence.SeanceTraining;
			Default_Absence_Export_Model.Trainee = Absence.Trainee;
			Default_Absence_Export_Model.FormerComment = Absence.FormerComment;
			Default_Absence_Export_Model.TraineeComment = Absence.TraineeComment;
			Default_Absence_Export_Model.SupervisorComment = Absence.SupervisorComment;
			Default_Absence_Export_Model.isHaveAuthorization = Absence.isHaveAuthorization;
			Default_Absence_Export_Model.Valide = Absence.Valide;
			Default_Absence_Export_Model.AbsenceState = Absence.AbsenceState;
			Default_Absence_Export_Model.Id = Absence.Id;
            return Default_Absence_Export_Model;            
        }

		public virtual Default_Absence_Export_Model CreateNew()
        {
            Absence Absence = new AbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Absence_Export_Model Default_Absence_Export_Model = this.ConverTo_Default_Absence_Export_Model(Absence);
            return Default_Absence_Export_Model;
        } 

		public virtual List<Default_Absence_Export_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            AbsenceBLO entityBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Absence> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Absence_Export_Model> ls_models = new List<Default_Absence_Export_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Absence_Export_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Absence_Export_ModelBLM : BaseDefault_Absence_Export_Model_BLM
	{
		public Default_Absence_Export_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
