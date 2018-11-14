﻿//modelType = Default_ModuleTraining_Export_Model

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
	public partial class BaseDefault_ModuleTraining_Export_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_ModuleTraining_Export_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ModuleTraining ConverTo_ModuleTraining(Default_ModuleTraining_Export_Model Default_ModuleTraining_Export_Model)
        {
			ModuleTraining ModuleTraining = null;
            if (Default_ModuleTraining_Export_Model.Id != 0)
            {
                ModuleTraining = new ModuleTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_ModuleTraining_Export_Model.Id);
            }
            else
            {
                ModuleTraining = new ModuleTraining();
            } 
			ModuleTraining.Specialty = Default_ModuleTraining_Export_Model.Specialty;
			ModuleTraining.Metier = Default_ModuleTraining_Export_Model.Metier;
			ModuleTraining.YearStudy = Default_ModuleTraining_Export_Model.YearStudy;
			ModuleTraining.Name = Default_ModuleTraining_Export_Model.Name;
			ModuleTraining.Code = Default_ModuleTraining_Export_Model.Code;
			ModuleTraining.HourlyMass = Default_ModuleTraining_Export_Model.HourlyMass;
			ModuleTraining.Hourly_Mass_To_Teach = Default_ModuleTraining_Export_Model.Hourly_Mass_To_Teach;
			ModuleTraining.Description = Default_ModuleTraining_Export_Model.Description;
			ModuleTraining.Id = Default_ModuleTraining_Export_Model.Id;
            return ModuleTraining;
        }
        public virtual Default_ModuleTraining_Export_Model ConverTo_Default_ModuleTraining_Export_Model(ModuleTraining ModuleTraining)
        {  
			Default_ModuleTraining_Export_Model Default_ModuleTraining_Export_Model = new Default_ModuleTraining_Export_Model();
			Default_ModuleTraining_Export_Model.toStringValue = ModuleTraining.ToString();
			Default_ModuleTraining_Export_Model.Specialty = ModuleTraining.Specialty;
			Default_ModuleTraining_Export_Model.Metier = ModuleTraining.Metier;
			Default_ModuleTraining_Export_Model.YearStudy = ModuleTraining.YearStudy;
			Default_ModuleTraining_Export_Model.Name = ModuleTraining.Name;
			Default_ModuleTraining_Export_Model.Code = ModuleTraining.Code;
			Default_ModuleTraining_Export_Model.HourlyMass = ModuleTraining.HourlyMass;
			Default_ModuleTraining_Export_Model.Hourly_Mass_To_Teach = ModuleTraining.Hourly_Mass_To_Teach;
			Default_ModuleTraining_Export_Model.Description = ModuleTraining.Description;
			Default_ModuleTraining_Export_Model.Id = ModuleTraining.Id;
            return Default_ModuleTraining_Export_Model;            
        }

		public virtual Default_ModuleTraining_Export_Model CreateNew()
        {
            ModuleTraining ModuleTraining = new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_ModuleTraining_Export_Model Default_ModuleTraining_Export_Model = this.ConverTo_Default_ModuleTraining_Export_Model(ModuleTraining);
            return Default_ModuleTraining_Export_Model;
        } 

		public virtual List<Default_ModuleTraining_Export_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ModuleTrainingBLO entityBLO = new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ModuleTraining> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_ModuleTraining_Export_Model> ls_models = new List<Default_ModuleTraining_Export_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_ModuleTraining_Export_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_ModuleTraining_Export_ModelBLM : BaseDefault_ModuleTraining_Export_Model_BLM
	{
		public Default_ModuleTraining_Export_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
