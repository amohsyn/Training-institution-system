﻿//modelType = Edit_SeanceTraining_Model

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
using TrainingIS.Models.SeanceTrainings;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseEdit_SeanceTraining_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		public Form_SeanceTraining_ModelBLM Form_SeanceTraining_ModelBLM {set;get;}
        
		public BaseEdit_SeanceTraining_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Form_SeanceTraining_ModelBLM = new Form_SeanceTraining_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual SeanceTraining ConverTo_SeanceTraining(Edit_SeanceTraining_Model Edit_SeanceTraining_Model)
        {
            var SeanceTraining = Form_SeanceTraining_ModelBLM.ConverTo_SeanceTraining(Edit_SeanceTraining_Model);
            return SeanceTraining;
        }

		public virtual Edit_SeanceTraining_Model ConverTo_Edit_SeanceTraining_Model(SeanceTraining SeanceTraining)
        {
            Edit_SeanceTraining_Model Edit_SeanceTraining_Model = new Edit_SeanceTraining_Model();
            Form_SeanceTraining_ModelBLM.ConverTo_Form_SeanceTraining_Model(Edit_SeanceTraining_Model, SeanceTraining);
            return Edit_SeanceTraining_Model;            
        }

		public virtual Edit_SeanceTraining_Model CreateNew()
        {
            SeanceTraining SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Edit_SeanceTraining_Model Edit_SeanceTraining_Model = this.ConverTo_Edit_SeanceTraining_Model(SeanceTraining);
            return Edit_SeanceTraining_Model;
        } 

		public virtual List<Edit_SeanceTraining_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SeanceTrainingBLO entityBLO = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<SeanceTraining> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Edit_SeanceTraining_Model> ls_models = new List<Edit_SeanceTraining_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Edit_SeanceTraining_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Edit_SeanceTraining_ModelBLM : BaseEdit_SeanceTraining_Model_BLM
	{
		public Edit_SeanceTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
