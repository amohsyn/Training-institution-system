//modelType = Default_Details_GPicture_Model

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
using GApp.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_GPicture_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_GPicture_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual GPicture ConverTo_GPicture(Default_Details_GPicture_Model Default_Details_GPicture_Model)
        {
			GPicture GPicture = null;
            if (Default_Details_GPicture_Model.Id != 0)
            {
                GPicture = new GPictureBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_GPicture_Model.Id);
            }
            else
            {
                GPicture = new GPicture();
            } 
			GPicture.Name = Default_Details_GPicture_Model.Name;
			GPicture.Description = Default_Details_GPicture_Model.Description;
			GPicture.Original_Thumbnail = Default_Details_GPicture_Model.Original_Thumbnail;
			GPicture.Large_Thumbnail = Default_Details_GPicture_Model.Large_Thumbnail;
			GPicture.Medium_Thumbnail = Default_Details_GPicture_Model.Medium_Thumbnail;
			GPicture.Small_Thumbnail = Default_Details_GPicture_Model.Small_Thumbnail;
			GPicture.Old_Reference = Default_Details_GPicture_Model.Old_Reference;
			GPicture.Id = Default_Details_GPicture_Model.Id;
            return GPicture;
        }
        public virtual Default_Details_GPicture_Model ConverTo_Default_Details_GPicture_Model(GPicture GPicture)
        {  
			Default_Details_GPicture_Model Default_Details_GPicture_Model = new Default_Details_GPicture_Model();
			Default_Details_GPicture_Model.toStringValue = GPicture.ToString();
			Default_Details_GPicture_Model.Name = GPicture.Name;
			Default_Details_GPicture_Model.Description = GPicture.Description;
			Default_Details_GPicture_Model.Original_Thumbnail = GPicture.Original_Thumbnail;
			Default_Details_GPicture_Model.Large_Thumbnail = GPicture.Large_Thumbnail;
			Default_Details_GPicture_Model.Medium_Thumbnail = GPicture.Medium_Thumbnail;
			Default_Details_GPicture_Model.Small_Thumbnail = GPicture.Small_Thumbnail;
			Default_Details_GPicture_Model.Old_Reference = GPicture.Old_Reference;
			Default_Details_GPicture_Model.Id = GPicture.Id;
            return Default_Details_GPicture_Model;            
        }

		public virtual Default_Details_GPicture_Model CreateNew()
        {
            GPicture GPicture = new GPictureBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_GPicture_Model Default_Details_GPicture_Model = this.ConverTo_Default_Details_GPicture_Model(GPicture);
            return Default_Details_GPicture_Model;
        } 

		public virtual List<Default_Details_GPicture_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            GPictureBLO entityBLO = new GPictureBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<GPicture> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_GPicture_Model> ls_models = new List<Default_Details_GPicture_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_GPicture_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_GPicture_ModelBLM : BaseDefault_Details_GPicture_ModelBLM
	{
		public Default_Details_GPicture_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
