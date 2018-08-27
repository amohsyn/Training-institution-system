//modelType = Index_Absence_Model

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
using TrainingIS.Models.Absences;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseIndex_Absence_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseIndex_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Absence ConverTo_Absence(Index_Absence_Model Index_Absence_Model)
        {
			Absence Absence = null;
            if (Index_Absence_Model.Id != 0)
            {
                Absence = new AbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Index_Absence_Model.Id);
            }
            else
            {
                Absence = new Absence();
            } 
			Absence.AbsenceDate = DefaultDateTime_If_Empty(Index_Absence_Model.AbsenceDate);
			Absence.Trainee = Index_Absence_Model.Trainee;
			Absence.isHaveAuthorization = Index_Absence_Model.isHaveAuthorization;
			Absence.SeancePlanning = Index_Absence_Model.SeancePlanning;
			Absence.Id = Index_Absence_Model.Id;
            return Absence;
        }
        public virtual Index_Absence_Model ConverTo_Index_Absence_Model(Absence Absence)
        {  
			Index_Absence_Model Index_Absence_Model = new Index_Absence_Model();
			Index_Absence_Model.toStringValue = Absence.ToString();
			Index_Absence_Model.AbsenceDate = DefaultDateTime_If_Empty(Absence.AbsenceDate);
			Index_Absence_Model.Trainee = Absence.Trainee;
			Index_Absence_Model.isHaveAuthorization = Absence.isHaveAuthorization;
			Index_Absence_Model.SeancePlanning = Absence.SeancePlanning;
			Index_Absence_Model.Id = Absence.Id;
            return Index_Absence_Model;            
        }

		public virtual Index_Absence_Model CreateNew()
        {
            Absence Absence = new AbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Index_Absence_Model Index_Absence_Model = this.ConverTo_Index_Absence_Model(Absence);
            return Index_Absence_Model;
        } 
    }

	public partial class Index_Absence_ModelBLM : BaseIndex_Absence_ModelBLM
	{
		public Index_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
