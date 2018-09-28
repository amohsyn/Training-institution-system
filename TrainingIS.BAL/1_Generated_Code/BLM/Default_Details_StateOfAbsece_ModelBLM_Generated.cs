//modelType = Default_Details_StateOfAbsece_Model

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
	public partial class BaseDefault_Details_StateOfAbsece_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_StateOfAbsece_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual StateOfAbsece ConverTo_StateOfAbsece(Default_Details_StateOfAbsece_Model Default_Details_StateOfAbsece_Model)
        {
			StateOfAbsece StateOfAbsece = null;
            if (Default_Details_StateOfAbsece_Model.Id != 0)
            {
                StateOfAbsece = new StateOfAbseceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_StateOfAbsece_Model.Id);
            }
            else
            {
                StateOfAbsece = new StateOfAbsece();
            } 
			StateOfAbsece.Name = Default_Details_StateOfAbsece_Model.Name;
			StateOfAbsece.Category = Default_Details_StateOfAbsece_Model.Category;
			StateOfAbsece.Value = Default_Details_StateOfAbsece_Model.Value;
			StateOfAbsece.Trainee = Default_Details_StateOfAbsece_Model.Trainee;
			StateOfAbsece.Id = Default_Details_StateOfAbsece_Model.Id;
            return StateOfAbsece;
        }
        public virtual Default_Details_StateOfAbsece_Model ConverTo_Default_Details_StateOfAbsece_Model(StateOfAbsece StateOfAbsece)
        {  
			Default_Details_StateOfAbsece_Model Default_Details_StateOfAbsece_Model = new Default_Details_StateOfAbsece_Model();
			Default_Details_StateOfAbsece_Model.toStringValue = StateOfAbsece.ToString();
			Default_Details_StateOfAbsece_Model.Name = StateOfAbsece.Name;
			Default_Details_StateOfAbsece_Model.Category = StateOfAbsece.Category;
			Default_Details_StateOfAbsece_Model.Value = StateOfAbsece.Value;
			Default_Details_StateOfAbsece_Model.Trainee = StateOfAbsece.Trainee;
			Default_Details_StateOfAbsece_Model.Id = StateOfAbsece.Id;
            return Default_Details_StateOfAbsece_Model;            
        }

		public virtual Default_Details_StateOfAbsece_Model CreateNew()
        {
            StateOfAbsece StateOfAbsece = new StateOfAbseceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_StateOfAbsece_Model Default_Details_StateOfAbsece_Model = this.ConverTo_Default_Details_StateOfAbsece_Model(StateOfAbsece);
            return Default_Details_StateOfAbsece_Model;
        } 

		public List<Default_Details_StateOfAbsece_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            StateOfAbseceBLO entityBLO = new StateOfAbseceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<StateOfAbsece> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_StateOfAbsece_Model> ls_models = new List<Default_Details_StateOfAbsece_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_StateOfAbsece_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_StateOfAbsece_ModelBLM : BaseDefault_Details_StateOfAbsece_ModelBLM
	{
		public Default_Details_StateOfAbsece_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
