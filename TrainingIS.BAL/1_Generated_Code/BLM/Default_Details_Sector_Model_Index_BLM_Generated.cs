//modelType = Default_Details_Sector_Model

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
	public partial class BaseDefault_Details_Sector_Model_Index_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Sector_Model_Index_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Sector ConverTo_Sector(Default_Details_Sector_Model Default_Details_Sector_Model)
        {
			Sector Sector = null;
            if (Default_Details_Sector_Model.Id != 0)
            {
                Sector = new SectorBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Sector_Model.Id);
            }
            else
            {
                Sector = new Sector();
            } 
			Sector.Code = Default_Details_Sector_Model.Code;
			Sector.Name = Default_Details_Sector_Model.Name;
			Sector.Description = Default_Details_Sector_Model.Description;
			Sector.Id = Default_Details_Sector_Model.Id;
            return Sector;
        }
        public virtual Default_Details_Sector_Model ConverTo_Default_Details_Sector_Model(Sector Sector)
        {  
			Default_Details_Sector_Model Default_Details_Sector_Model = new Default_Details_Sector_Model();
			Default_Details_Sector_Model.toStringValue = Sector.ToString();
			Default_Details_Sector_Model.Code = Sector.Code;
			Default_Details_Sector_Model.Name = Sector.Name;
			Default_Details_Sector_Model.Description = Sector.Description;
			Default_Details_Sector_Model.Id = Sector.Id;
            return Default_Details_Sector_Model;            
        }

		public virtual Default_Details_Sector_Model CreateNew()
        {
            Sector Sector = new SectorBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Sector_Model Default_Details_Sector_Model = this.ConverTo_Default_Details_Sector_Model(Sector);
            return Default_Details_Sector_Model;
        } 

		public virtual List<Default_Details_Sector_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SectorBLO entityBLO = new SectorBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Sector> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Sector_Model> ls_models = new List<Default_Details_Sector_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Sector_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Sector_ModelBLM : BaseDefault_Details_Sector_Model_Index_BLM
	{
		public Default_Details_Sector_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
