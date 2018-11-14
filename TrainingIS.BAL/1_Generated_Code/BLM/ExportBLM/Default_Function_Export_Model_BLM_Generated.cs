//modelType = Default_Function_Export_Model

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
	public partial class BaseDefault_Function_Export_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Function_Export_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Function ConverTo_Function(Default_Function_Export_Model Default_Function_Export_Model)
        {
			Function Function = null;
            if (Default_Function_Export_Model.Id != 0)
            {
                Function = new FunctionBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Function_Export_Model.Id);
            }
            else
            {
                Function = new Function();
            } 
			Function.Code = Default_Function_Export_Model.Code;
			Function.Name = Default_Function_Export_Model.Name;
			Function.Description = Default_Function_Export_Model.Description;
			Function.Id = Default_Function_Export_Model.Id;
            return Function;
        }
        public virtual Default_Function_Export_Model ConverTo_Default_Function_Export_Model(Function Function)
        {  
			Default_Function_Export_Model Default_Function_Export_Model = new Default_Function_Export_Model();
			Default_Function_Export_Model.toStringValue = Function.ToString();
			Default_Function_Export_Model.Code = Function.Code;
			Default_Function_Export_Model.Name = Function.Name;
			Default_Function_Export_Model.Description = Function.Description;
			Default_Function_Export_Model.Id = Function.Id;
            return Default_Function_Export_Model;            
        }

		public virtual Default_Function_Export_Model CreateNew()
        {
            Function Function = new FunctionBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Function_Export_Model Default_Function_Export_Model = this.ConverTo_Default_Function_Export_Model(Function);
            return Default_Function_Export_Model;
        } 

		public virtual List<Default_Function_Export_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            FunctionBLO entityBLO = new FunctionBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Function> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Function_Export_Model> ls_models = new List<Default_Function_Export_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Function_Export_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Function_Export_ModelBLM : BaseDefault_Function_Export_Model_BLM
	{
		public Default_Function_Export_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
