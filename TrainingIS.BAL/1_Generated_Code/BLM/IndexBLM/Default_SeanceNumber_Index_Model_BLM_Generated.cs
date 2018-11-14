//modelType = Default_SeanceNumber_Index_Model

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
	public partial class BaseDefault_SeanceNumber_Index_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_SeanceNumber_Index_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SeanceNumber ConverTo_SeanceNumber(Default_SeanceNumber_Index_Model Default_SeanceNumber_Index_Model)
        {
			SeanceNumber SeanceNumber = null;
            if (Default_SeanceNumber_Index_Model.Id != 0)
            {
                SeanceNumber = new SeanceNumberBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_SeanceNumber_Index_Model.Id);
            }
            else
            {
                SeanceNumber = new SeanceNumber();
            } 
			SeanceNumber.Code = Default_SeanceNumber_Index_Model.Code;
			SeanceNumber.StartTime = DefaultDateTime_If_Empty(Default_SeanceNumber_Index_Model.StartTime);
			SeanceNumber.EndTime = DefaultDateTime_If_Empty(Default_SeanceNumber_Index_Model.EndTime);
			SeanceNumber.Description = Default_SeanceNumber_Index_Model.Description;
			SeanceNumber.Id = Default_SeanceNumber_Index_Model.Id;
            return SeanceNumber;
        }
        public virtual Default_SeanceNumber_Index_Model ConverTo_Default_SeanceNumber_Index_Model(SeanceNumber SeanceNumber)
        {  
			Default_SeanceNumber_Index_Model Default_SeanceNumber_Index_Model = new Default_SeanceNumber_Index_Model();
			Default_SeanceNumber_Index_Model.toStringValue = SeanceNumber.ToString();
			Default_SeanceNumber_Index_Model.Code = SeanceNumber.Code;
			Default_SeanceNumber_Index_Model.StartTime = DefaultDateTime_If_Empty(SeanceNumber.StartTime);
			Default_SeanceNumber_Index_Model.EndTime = DefaultDateTime_If_Empty(SeanceNumber.EndTime);
			Default_SeanceNumber_Index_Model.Description = SeanceNumber.Description;
			Default_SeanceNumber_Index_Model.Id = SeanceNumber.Id;
            return Default_SeanceNumber_Index_Model;            
        }

		public virtual Default_SeanceNumber_Index_Model CreateNew()
        {
            SeanceNumber SeanceNumber = new SeanceNumberBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_SeanceNumber_Index_Model Default_SeanceNumber_Index_Model = this.ConverTo_Default_SeanceNumber_Index_Model(SeanceNumber);
            return Default_SeanceNumber_Index_Model;
        } 

		public virtual List<Default_SeanceNumber_Index_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SeanceNumberBLO entityBLO = new SeanceNumberBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<SeanceNumber> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_SeanceNumber_Index_Model> ls_models = new List<Default_SeanceNumber_Index_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_SeanceNumber_Index_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_SeanceNumber_Index_ModelBLM : BaseDefault_SeanceNumber_Index_Model_BLM
	{
		public Default_SeanceNumber_Index_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
