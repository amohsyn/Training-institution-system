using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.DAL;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_TrainingYear_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Details_TrainingYear_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual TrainingYear ConverTo_TrainingYear(Default_Details_TrainingYear_Model Default_Details_TrainingYear_Model)
        {
			TrainingYear TrainingYear = null;
            if (Default_Details_TrainingYear_Model.Id != 0)
            {
                TrainingYear = new TrainingYearBLO(this.UnitOfWork).FindBaseEntityByID(Default_Details_TrainingYear_Model.Id);
            }
            else
            {
                TrainingYear = new TrainingYear();
            } 
			TrainingYear.Code = Default_Details_TrainingYear_Model.Code;
			TrainingYear.StartDate = Default_Details_TrainingYear_Model.StartDate;
			TrainingYear.EndtDate = Default_Details_TrainingYear_Model.EndtDate;
			TrainingYear.Id = Default_Details_TrainingYear_Model.Id;
            return TrainingYear;
        }
        public virtual Default_Details_TrainingYear_Model ConverTo_Default_Details_TrainingYear_Model(TrainingYear TrainingYear)
        {  
			Default_Details_TrainingYear_Model Default_Details_TrainingYear_Model = new Default_Details_TrainingYear_Model();
			Default_Details_TrainingYear_Model.toStringValue = TrainingYear.ToString();
			Default_Details_TrainingYear_Model.Code = TrainingYear.Code;
			Default_Details_TrainingYear_Model.StartDate = ConversionUtil.DefaultValue_if_Null<DateTime>(TrainingYear.StartDate);
			Default_Details_TrainingYear_Model.EndtDate = ConversionUtil.DefaultValue_if_Null<DateTime>(TrainingYear.EndtDate);
			Default_Details_TrainingYear_Model.Id = TrainingYear.Id;
            return Default_Details_TrainingYear_Model;            
        }

		public virtual Default_Details_TrainingYear_Model CreateNew()
        {
            TrainingYear TrainingYear = new TrainingYear();
            Default_Details_TrainingYear_Model Default_Details_TrainingYear_Model = this.ConverTo_Default_Details_TrainingYear_Model(TrainingYear);
            return Default_Details_TrainingYear_Model;
        } 
    }

	public partial class Default_Details_TrainingYear_ModelBLM : BaseDefault_Details_TrainingYear_ModelBLM
	{
		public Default_Details_TrainingYear_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
