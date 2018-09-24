//modelType = Details_Absence_Model

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

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDetails_Absence_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDetails_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Absence ConverTo_Absence(Details_Absence_Model Details_Absence_Model)
        {
			Absence Absence = null;
            if (Details_Absence_Model.Id != 0)
            {
                Absence = new AbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Details_Absence_Model.Id);
            }
            else
            {
                Absence = new Absence();
            } 
			Absence.Trainee = Details_Absence_Model.Trainee;
			Absence.isHaveAuthorization = Details_Absence_Model.isHaveAuthorization;
			Absence.SeanceTraining = Details_Absence_Model.SeanceTraining;
			Absence.FormerComment = Details_Absence_Model.FormerComment;
			Absence.TraineeComment = Details_Absence_Model.TraineeComment;
			Absence.SupervisorComment = Details_Absence_Model.SupervisorComment;
			Absence.Id = Details_Absence_Model.Id;
            return Absence;
        }
        public virtual Details_Absence_Model ConverTo_Details_Absence_Model(Absence Absence)
        {  
			Details_Absence_Model Details_Absence_Model = new Details_Absence_Model();
			Details_Absence_Model.toStringValue = Absence.ToString();
			Details_Absence_Model.SeanceTraining = Absence.SeanceTraining;
			Details_Absence_Model.Trainee = Absence.Trainee;
			Details_Absence_Model.isHaveAuthorization = Absence.isHaveAuthorization;
			Details_Absence_Model.FormerComment = Absence.FormerComment;
			Details_Absence_Model.TraineeComment = Absence.TraineeComment;
			Details_Absence_Model.SupervisorComment = Absence.SupervisorComment;
			Details_Absence_Model.Id = Absence.Id;
            return Details_Absence_Model;            
        }

		public virtual Details_Absence_Model CreateNew()
        {
            Absence Absence = new AbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Details_Absence_Model Details_Absence_Model = this.ConverTo_Details_Absence_Model(Absence);
            return Details_Absence_Model;
        } 

        public List<Details_Absence_Model> Find(string OrderBy, string FilterBy,  string SearchBy, List<string> SearchCreteria, int? CurrentPage, int? PageSize, out int totalRecords)
        {
            AbsenceBLO entityBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Absence> Query_Entity = entityBLO
                .Find_as_Queryable(OrderBy, FilterBy, SearchBy, SearchCreteria, CurrentPage, PageSize, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Details_Absence_Model> ls_models = new List<Details_Absence_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Details_Absence_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Details_Absence_ModelBLM : BaseDetails_Absence_ModelBLM
	{
		public Details_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
