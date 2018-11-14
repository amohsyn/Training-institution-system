//modelType = Default_Schoollevel_Details_Model

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
	public partial class BaseDefault_Schoollevel_Details_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Schoollevel_Details_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Schoollevel ConverTo_Schoollevel(Default_Schoollevel_Details_Model Default_Schoollevel_Details_Model)
        {
			Schoollevel Schoollevel = null;
            if (Default_Schoollevel_Details_Model.Id != 0)
            {
                Schoollevel = new SchoollevelBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Schoollevel_Details_Model.Id);
            }
            else
            {
                Schoollevel = new Schoollevel();
            } 
			Schoollevel.Code = Default_Schoollevel_Details_Model.Code;
			Schoollevel.Name = Default_Schoollevel_Details_Model.Name;
			Schoollevel.Description = Default_Schoollevel_Details_Model.Description;
			Schoollevel.Id = Default_Schoollevel_Details_Model.Id;
            return Schoollevel;
        }
        public virtual Default_Schoollevel_Details_Model ConverTo_Default_Schoollevel_Details_Model(Schoollevel Schoollevel)
        {  
			Default_Schoollevel_Details_Model Default_Schoollevel_Details_Model = new Default_Schoollevel_Details_Model();
			Default_Schoollevel_Details_Model.toStringValue = Schoollevel.ToString();
			Default_Schoollevel_Details_Model.Code = Schoollevel.Code;
			Default_Schoollevel_Details_Model.Name = Schoollevel.Name;
			Default_Schoollevel_Details_Model.Description = Schoollevel.Description;
			Default_Schoollevel_Details_Model.Id = Schoollevel.Id;
            return Default_Schoollevel_Details_Model;            
        }

		public virtual Default_Schoollevel_Details_Model CreateNew()
        {
            Schoollevel Schoollevel = new SchoollevelBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Schoollevel_Details_Model Default_Schoollevel_Details_Model = this.ConverTo_Default_Schoollevel_Details_Model(Schoollevel);
            return Default_Schoollevel_Details_Model;
        } 

		public virtual List<Default_Schoollevel_Details_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SchoollevelBLO entityBLO = new SchoollevelBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Schoollevel> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Schoollevel_Details_Model> ls_models = new List<Default_Schoollevel_Details_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Schoollevel_Details_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Schoollevel_Details_ModelBLM : BaseDefault_Schoollevel_Details_Model_BLM
	{
		public Default_Schoollevel_Details_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
