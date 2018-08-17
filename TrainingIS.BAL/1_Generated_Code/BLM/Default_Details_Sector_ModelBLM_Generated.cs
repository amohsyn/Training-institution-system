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

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_Sector_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Sector_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
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
    }

	public partial class Default_Details_Sector_ModelBLM : BaseDefault_Details_Sector_ModelBLM
	{
		public Default_Details_Sector_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
