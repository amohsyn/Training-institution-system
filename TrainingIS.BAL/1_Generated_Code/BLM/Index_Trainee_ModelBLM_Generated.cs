//modelType = Index_Trainee_Model

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
using TrainingIS.Models.Trainees;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseIndex_Trainee_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseIndex_Trainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Trainee ConverTo_Trainee(Index_Trainee_Model Index_Trainee_Model)
        {
			Trainee Trainee = null;
            if (Index_Trainee_Model.Id != 0)
            {
                Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Index_Trainee_Model.Id);
            }
            else
            {
                Trainee = new Trainee();
            } 
			Trainee.CNE = Index_Trainee_Model.CNE;
			Trainee.Group = Index_Trainee_Model.Group;
			Trainee.FirstName = Index_Trainee_Model.FirstName;
			Trainee.LastName = Index_Trainee_Model.LastName;
			Trainee.Id = Index_Trainee_Model.Id;
            return Trainee;
        }
        public virtual Index_Trainee_Model ConverTo_Index_Trainee_Model(Trainee Trainee)
        {  
			Index_Trainee_Model Index_Trainee_Model = new Index_Trainee_Model();
			Index_Trainee_Model.toStringValue = Trainee.ToString();
			Index_Trainee_Model.CNE = Trainee.CNE;
			Index_Trainee_Model.Group = Trainee.Group;
			Index_Trainee_Model.FirstName = Trainee.FirstName;
			Index_Trainee_Model.LastName = Trainee.LastName;
			Index_Trainee_Model.Id = Trainee.Id;
            return Index_Trainee_Model;            
        }

		public virtual Index_Trainee_Model CreateNew()
        {
            Trainee Trainee = new TraineeBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Index_Trainee_Model Index_Trainee_Model = this.ConverTo_Index_Trainee_Model(Trainee);
            return Index_Trainee_Model;
        } 

        public List<Index_Trainee_Model> Find(string OrderBy, string FilterBy,  string SearchBy, List<string> SearchCreteria, int? CurrentPage, int? PageSize, out int totalRecords)
        {
            TraineeBLO entityBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Trainee> Query_Entity = entityBLO
                .Find_as_Queryable(OrderBy, FilterBy, SearchBy, SearchCreteria, CurrentPage, PageSize, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Index_Trainee_Model> ls_models = new List<Index_Trainee_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Index_Trainee_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Index_Trainee_ModelBLM : BaseIndex_Trainee_ModelBLM
	{
		public Index_Trainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
