//modelType = Create_Absence_Model

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
using TrainingIS.Models.Absences;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseCreate_Absence_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseCreate_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Absence ConverTo_Absence(Create_Absence_Model Create_Absence_Model)
        {
			Absence Absence = null;
            if (Create_Absence_Model.Id != 0)
            {
                Absence = new AbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Create_Absence_Model.Id);
            }
            else
            {
                Absence = new Absence();
            } 
			Absence.TraineeId = Create_Absence_Model.TraineeId;
			Absence.Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Create_Absence_Model.TraineeId)) ;
			Absence.isHaveAuthorization = Create_Absence_Model.isHaveAuthorization;
			Absence.SeanceTrainingId = Create_Absence_Model.SeanceTrainingId;
			Absence.SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Create_Absence_Model.SeanceTrainingId)) ;
			Absence.FormerComment = Create_Absence_Model.FormerComment;
			Absence.TraineeComment = Create_Absence_Model.TraineeComment;
			Absence.SupervisorComment = Create_Absence_Model.SupervisorComment;
			Absence.Id = Create_Absence_Model.Id;
            return Absence;
        }
        public virtual Create_Absence_Model ConverTo_Create_Absence_Model(Absence Absence)
        {  
			Create_Absence_Model Create_Absence_Model = new Create_Absence_Model();
			Create_Absence_Model.toStringValue = Absence.ToString();
			Create_Absence_Model.SeanceTrainingId = Absence.SeanceTrainingId;
			Create_Absence_Model.TraineeId = Absence.TraineeId;
			Create_Absence_Model.isHaveAuthorization = Absence.isHaveAuthorization;
			Create_Absence_Model.FormerComment = Absence.FormerComment;
			Create_Absence_Model.TraineeComment = Absence.TraineeComment;
			Create_Absence_Model.SupervisorComment = Absence.SupervisorComment;
			Create_Absence_Model.Id = Absence.Id;
            return Create_Absence_Model;            
        }

		public virtual Create_Absence_Model CreateNew()
        {
            Absence Absence = new AbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Create_Absence_Model Create_Absence_Model = this.ConverTo_Create_Absence_Model(Absence);
            return Create_Absence_Model;
        } 

		public List<Create_Absence_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            AbsenceBLO entityBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Absence> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Create_Absence_Model> ls_models = new List<Create_Absence_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Create_Absence_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Create_Absence_ModelBLM : BaseCreate_Absence_ModelBLM
	{
		public Create_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
