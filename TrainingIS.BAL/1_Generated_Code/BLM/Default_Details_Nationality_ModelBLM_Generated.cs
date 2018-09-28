//modelType = Default_Details_Nationality_Model

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
	public partial class BaseDefault_Details_Nationality_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Nationality_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Nationality ConverTo_Nationality(Default_Details_Nationality_Model Default_Details_Nationality_Model)
        {
			Nationality Nationality = null;
            if (Default_Details_Nationality_Model.Id != 0)
            {
                Nationality = new NationalityBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Nationality_Model.Id);
            }
            else
            {
                Nationality = new Nationality();
            } 
			Nationality.Code = Default_Details_Nationality_Model.Code;
			Nationality.Name = Default_Details_Nationality_Model.Name;
			Nationality.Description = Default_Details_Nationality_Model.Description;
			Nationality.Id = Default_Details_Nationality_Model.Id;
            return Nationality;
        }
        public virtual Default_Details_Nationality_Model ConverTo_Default_Details_Nationality_Model(Nationality Nationality)
        {  
			Default_Details_Nationality_Model Default_Details_Nationality_Model = new Default_Details_Nationality_Model();
			Default_Details_Nationality_Model.toStringValue = Nationality.ToString();
			Default_Details_Nationality_Model.Code = Nationality.Code;
			Default_Details_Nationality_Model.Name = Nationality.Name;
			Default_Details_Nationality_Model.Description = Nationality.Description;
			Default_Details_Nationality_Model.Id = Nationality.Id;
            return Default_Details_Nationality_Model;            
        }

		public virtual Default_Details_Nationality_Model CreateNew()
        {
            Nationality Nationality = new NationalityBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Nationality_Model Default_Details_Nationality_Model = this.ConverTo_Default_Details_Nationality_Model(Nationality);
            return Default_Details_Nationality_Model;
        } 

		public List<Default_Details_Nationality_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            NationalityBLO entityBLO = new NationalityBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Nationality> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Nationality_Model> ls_models = new List<Default_Details_Nationality_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Nationality_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Nationality_ModelBLM : BaseDefault_Details_Nationality_ModelBLM
	{
		public Default_Details_Nationality_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
