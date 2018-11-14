//modelType = Default_Form_Sector_Model

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
	public partial class BaseDefault_Form_Sector_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Sector_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Sector ConverTo_Sector(Default_Form_Sector_Model Default_Form_Sector_Model)
        {
			Sector Sector = null;
            if (Default_Form_Sector_Model.Id != 0)
            {
                Sector = new SectorBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Sector_Model.Id);
            }
            else
            {
                Sector = new Sector();
            } 
			Sector.Code = Default_Form_Sector_Model.Code;
			Sector.Name = Default_Form_Sector_Model.Name;
			Sector.Description = Default_Form_Sector_Model.Description;
			Sector.Reference = Default_Form_Sector_Model.Reference;
			Sector.Id = Default_Form_Sector_Model.Id;
            return Sector;
        }
        public virtual void ConverTo_Default_Form_Sector_Model(Default_Form_Sector_Model Default_Form_Sector_Model, Sector Sector)
        {  
			 
			Default_Form_Sector_Model.toStringValue = Sector.ToString();
			Default_Form_Sector_Model.Code = Sector.Code;
			Default_Form_Sector_Model.Name = Sector.Name;
			Default_Form_Sector_Model.Description = Sector.Description;
			Default_Form_Sector_Model.Id = Sector.Id;
			Default_Form_Sector_Model.Reference = Sector.Reference;
                     
        }

    }

	public partial class Default_Form_Sector_ModelBLM : BaseDefault_Form_Sector_Model_BLM
	{
		public Default_Form_Sector_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
