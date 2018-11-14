//modelType = Default_StateOfAbsece_Details_Model

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
	public partial class BaseDefault_StateOfAbsece_Details_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_StateOfAbsece_Details_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual StateOfAbsece ConverTo_StateOfAbsece(Default_StateOfAbsece_Details_Model Default_StateOfAbsece_Details_Model)
        {
			StateOfAbsece StateOfAbsece = null;
            if (Default_StateOfAbsece_Details_Model.Id != 0)
            {
                StateOfAbsece = new StateOfAbseceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_StateOfAbsece_Details_Model.Id);
            }
            else
            {
                StateOfAbsece = new StateOfAbsece();
            } 
			StateOfAbsece.Name = Default_StateOfAbsece_Details_Model.Name;
			StateOfAbsece.Category = Default_StateOfAbsece_Details_Model.Category;
			StateOfAbsece.Value = Default_StateOfAbsece_Details_Model.Value;
			StateOfAbsece.Trainee = Default_StateOfAbsece_Details_Model.Trainee;
			StateOfAbsece.Id = Default_StateOfAbsece_Details_Model.Id;
            return StateOfAbsece;
        }
        public virtual Default_StateOfAbsece_Details_Model ConverTo_Default_StateOfAbsece_Details_Model(StateOfAbsece StateOfAbsece)
        {  
			Default_StateOfAbsece_Details_Model Default_StateOfAbsece_Details_Model = new Default_StateOfAbsece_Details_Model();
			Default_StateOfAbsece_Details_Model.toStringValue = StateOfAbsece.ToString();
			Default_StateOfAbsece_Details_Model.Name = StateOfAbsece.Name;
			Default_StateOfAbsece_Details_Model.Category = StateOfAbsece.Category;
			Default_StateOfAbsece_Details_Model.Value = StateOfAbsece.Value;
			Default_StateOfAbsece_Details_Model.Trainee = StateOfAbsece.Trainee;
			Default_StateOfAbsece_Details_Model.Id = StateOfAbsece.Id;
            return Default_StateOfAbsece_Details_Model;            
        }

		public virtual Default_StateOfAbsece_Details_Model CreateNew()
        {
            StateOfAbsece StateOfAbsece = new StateOfAbseceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_StateOfAbsece_Details_Model Default_StateOfAbsece_Details_Model = this.ConverTo_Default_StateOfAbsece_Details_Model(StateOfAbsece);
            return Default_StateOfAbsece_Details_Model;
        } 

		public virtual List<Default_StateOfAbsece_Details_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            StateOfAbseceBLO entityBLO = new StateOfAbseceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<StateOfAbsece> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_StateOfAbsece_Details_Model> ls_models = new List<Default_StateOfAbsece_Details_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_StateOfAbsece_Details_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_StateOfAbsece_Details_ModelBLM : BaseDefault_StateOfAbsece_Details_Model_BLM
	{
		public Default_StateOfAbsece_Details_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
