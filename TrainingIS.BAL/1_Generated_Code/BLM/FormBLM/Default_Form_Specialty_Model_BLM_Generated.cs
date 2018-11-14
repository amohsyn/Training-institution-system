//modelType = Default_Form_Specialty_Model

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
	public partial class BaseDefault_Form_Specialty_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Specialty_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Specialty ConverTo_Specialty(Default_Form_Specialty_Model Default_Form_Specialty_Model)
        {
			Specialty Specialty = null;
            if (Default_Form_Specialty_Model.Id != 0)
            {
                Specialty = new SpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Specialty_Model.Id);
            }
            else
            {
                Specialty = new Specialty();
            } 
			Specialty.SectorId = Default_Form_Specialty_Model.SectorId;
			Specialty.Sector = new SectorBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Specialty_Model.SectorId)) ;
			Specialty.TrainingLevelId = Default_Form_Specialty_Model.TrainingLevelId;
			Specialty.TrainingLevel = new TrainingLevelBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Specialty_Model.TrainingLevelId)) ;
			Specialty.Code = Default_Form_Specialty_Model.Code;
			Specialty.Name = Default_Form_Specialty_Model.Name;
			Specialty.Description = Default_Form_Specialty_Model.Description;
			Specialty.Reference = Default_Form_Specialty_Model.Reference;
			Specialty.Id = Default_Form_Specialty_Model.Id;
            return Specialty;
        }
        public virtual void ConverTo_Default_Form_Specialty_Model(Default_Form_Specialty_Model Default_Form_Specialty_Model, Specialty Specialty)
        {  
			 
			Default_Form_Specialty_Model.toStringValue = Specialty.ToString();
			Default_Form_Specialty_Model.SectorId = Specialty.SectorId;
			Default_Form_Specialty_Model.TrainingLevelId = Specialty.TrainingLevelId;
			Default_Form_Specialty_Model.Code = Specialty.Code;
			Default_Form_Specialty_Model.Name = Specialty.Name;
			Default_Form_Specialty_Model.Description = Specialty.Description;
			Default_Form_Specialty_Model.Id = Specialty.Id;
			Default_Form_Specialty_Model.Reference = Specialty.Reference;
                     
        }

    }

	public partial class Default_Form_Specialty_ModelBLM : BaseDefault_Form_Specialty_Model_BLM
	{
		public Default_Form_Specialty_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
