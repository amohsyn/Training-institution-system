//modelType = Create_Training_Model

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
using TrainingIS.Entities.ModelsViews.Trainings;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseCreate_Training_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		public Form_Training_ModelBLM Form_Training_ModelBLM {set;get;}
        
		public BaseCreate_Training_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Form_Training_ModelBLM = new Form_Training_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Training ConverTo_Training(Create_Training_Model Create_Training_Model)
        {
            var Training = Form_Training_ModelBLM.ConverTo_Training(Create_Training_Model);
            return Training;
        }

		public virtual Create_Training_Model ConverTo_Create_Training_Model(Training Training)
        {
            Create_Training_Model Create_Training_Model = new Create_Training_Model();
            Form_Training_ModelBLM.ConverTo_Form_Training_Model(Create_Training_Model, Training);
            return Create_Training_Model;            
        }

		public virtual Create_Training_Model CreateNew()
        {
            Training Training = new TrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Create_Training_Model Create_Training_Model = this.ConverTo_Create_Training_Model(Training);
            return Create_Training_Model;
        } 

		public virtual List<Create_Training_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            TrainingBLO entityBLO = new TrainingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Training> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Create_Training_Model> ls_models = new List<Create_Training_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Create_Training_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Create_Training_ModelBLM : BaseCreate_Training_Model_BLM
	{
		public Create_Training_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
