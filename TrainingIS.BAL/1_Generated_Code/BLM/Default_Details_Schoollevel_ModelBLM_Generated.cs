//modelType = Default_Details_Schoollevel_Model

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
	public partial class BaseDefault_Details_Schoollevel_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Schoollevel_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Schoollevel ConverTo_Schoollevel(Default_Details_Schoollevel_Model Default_Details_Schoollevel_Model)
        {
			Schoollevel Schoollevel = null;
            if (Default_Details_Schoollevel_Model.Id != 0)
            {
                Schoollevel = new SchoollevelBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Schoollevel_Model.Id);
            }
            else
            {
                Schoollevel = new Schoollevel();
            } 
			Schoollevel.Code = Default_Details_Schoollevel_Model.Code;
			Schoollevel.Name = Default_Details_Schoollevel_Model.Name;
			Schoollevel.Description = Default_Details_Schoollevel_Model.Description;
			Schoollevel.Id = Default_Details_Schoollevel_Model.Id;
            return Schoollevel;
        }
        public virtual Default_Details_Schoollevel_Model ConverTo_Default_Details_Schoollevel_Model(Schoollevel Schoollevel)
        {  
			Default_Details_Schoollevel_Model Default_Details_Schoollevel_Model = new Default_Details_Schoollevel_Model();
			Default_Details_Schoollevel_Model.toStringValue = Schoollevel.ToString();
			Default_Details_Schoollevel_Model.Code = Schoollevel.Code;
			Default_Details_Schoollevel_Model.Name = Schoollevel.Name;
			Default_Details_Schoollevel_Model.Description = Schoollevel.Description;
			Default_Details_Schoollevel_Model.Id = Schoollevel.Id;
            return Default_Details_Schoollevel_Model;            
        }

		public virtual Default_Details_Schoollevel_Model CreateNew()
        {
            Schoollevel Schoollevel = new SchoollevelBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Schoollevel_Model Default_Details_Schoollevel_Model = this.ConverTo_Default_Details_Schoollevel_Model(Schoollevel);
            return Default_Details_Schoollevel_Model;
        } 

		public virtual List<Default_Details_Schoollevel_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SchoollevelBLO entityBLO = new SchoollevelBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Schoollevel> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Schoollevel_Model> ls_models = new List<Default_Details_Schoollevel_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Schoollevel_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Schoollevel_ModelBLM : BaseDefault_Details_Schoollevel_ModelBLM
	{
		public Default_Details_Schoollevel_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
