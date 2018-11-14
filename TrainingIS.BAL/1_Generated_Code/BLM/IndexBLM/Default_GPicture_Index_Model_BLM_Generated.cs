//modelType = Default_GPicture_Index_Model

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
	public partial class BaseDefault_GPicture_Index_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_GPicture_Index_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual GPicture ConverTo_GPicture(Default_GPicture_Index_Model Default_GPicture_Index_Model)
        {
			GPicture GPicture = null;
            if (Default_GPicture_Index_Model.Id != 0)
            {
                GPicture = new GPictureBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_GPicture_Index_Model.Id);
            }
            else
            {
                GPicture = new GPicture();
            } 
			GPicture.Name = Default_GPicture_Index_Model.Name;
			GPicture.Description = Default_GPicture_Index_Model.Description;
			GPicture.Original_Thumbnail = Default_GPicture_Index_Model.Original_Thumbnail;
			GPicture.Large_Thumbnail = Default_GPicture_Index_Model.Large_Thumbnail;
			GPicture.Medium_Thumbnail = Default_GPicture_Index_Model.Medium_Thumbnail;
			GPicture.Small_Thumbnail = Default_GPicture_Index_Model.Small_Thumbnail;
			GPicture.Old_Reference = Default_GPicture_Index_Model.Old_Reference;
			GPicture.Id = Default_GPicture_Index_Model.Id;
            return GPicture;
        }
        public virtual Default_GPicture_Index_Model ConverTo_Default_GPicture_Index_Model(GPicture GPicture)
        {  
			Default_GPicture_Index_Model Default_GPicture_Index_Model = new Default_GPicture_Index_Model();
			Default_GPicture_Index_Model.toStringValue = GPicture.ToString();
			Default_GPicture_Index_Model.Name = GPicture.Name;
			Default_GPicture_Index_Model.Description = GPicture.Description;
			Default_GPicture_Index_Model.Original_Thumbnail = GPicture.Original_Thumbnail;
			Default_GPicture_Index_Model.Large_Thumbnail = GPicture.Large_Thumbnail;
			Default_GPicture_Index_Model.Medium_Thumbnail = GPicture.Medium_Thumbnail;
			Default_GPicture_Index_Model.Small_Thumbnail = GPicture.Small_Thumbnail;
			Default_GPicture_Index_Model.Old_Reference = GPicture.Old_Reference;
			Default_GPicture_Index_Model.Id = GPicture.Id;
            return Default_GPicture_Index_Model;            
        }

		public virtual Default_GPicture_Index_Model CreateNew()
        {
            GPicture GPicture = new GPictureBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_GPicture_Index_Model Default_GPicture_Index_Model = this.ConverTo_Default_GPicture_Index_Model(GPicture);
            return Default_GPicture_Index_Model;
        } 

		public virtual List<Default_GPicture_Index_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            GPictureBLO entityBLO = new GPictureBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<GPicture> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_GPicture_Index_Model> ls_models = new List<Default_GPicture_Index_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_GPicture_Index_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_GPicture_Index_ModelBLM : BaseDefault_GPicture_Index_Model_BLM
	{
		public Default_GPicture_Index_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
