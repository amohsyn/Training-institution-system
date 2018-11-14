//modelType = Edit_Training_Model

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
	public partial class BaseEdit_Training_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		private Form_Training_ModelBLM Form_Training_ModelBLM {set;get;}
        
		public BaseEdit_Training_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Form_Training_ModelBLM = new Form_Training_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Training ConverTo_Training(Edit_Training_Model Edit_Training_Model)
        {
            var Training = Form_Training_ModelBLM.ConverTo_Training(Edit_Training_Model);
            return Training;
        }

		public virtual Edit_Training_Model ConverTo_Edit_Training_Model(Training Training)
        {
            Edit_Training_Model Edit_Training_Model = new Edit_Training_Model();
            Form_Training_ModelBLM.ConverTo_Form_Training_Model(Edit_Training_Model, Training);
            return Edit_Training_Model;            
        }

		public virtual Edit_Training_Model CreateNew()
        {
            Training Training = new TrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Edit_Training_Model Edit_Training_Model = this.ConverTo_Edit_Training_Model(Training);
            return Edit_Training_Model;
        } 

		public virtual List<Edit_Training_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            TrainingBLO entityBLO = new TrainingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Training> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Edit_Training_Model> ls_models = new List<Edit_Training_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Edit_Training_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Edit_Training_ModelBLM : BaseEdit_Training_Model_BLM
	{
		public Edit_Training_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
