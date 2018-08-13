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
	public partial class BaseDefault_Details_SeanceDay_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Details_SeanceDay_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual SeanceDay ConverTo_SeanceDay(Default_Details_SeanceDay_Model Default_Details_SeanceDay_Model)
        {
			SeanceDay SeanceDay = null;
            if (Default_Details_SeanceDay_Model.Id != 0)
            {
                SeanceDay = new SeanceDayBLO(this.UnitOfWork).FindBaseEntityByID(Default_Details_SeanceDay_Model.Id);
            }
            else
            {
                SeanceDay = new SeanceDay();
            } 
			SeanceDay.Name = Default_Details_SeanceDay_Model.Name;
			SeanceDay.Code = Default_Details_SeanceDay_Model.Code;
			SeanceDay.Description = Default_Details_SeanceDay_Model.Description;
			SeanceDay.Id = Default_Details_SeanceDay_Model.Id;
            return SeanceDay;
        }
        public virtual Default_Details_SeanceDay_Model ConverTo_Default_Details_SeanceDay_Model(SeanceDay SeanceDay)
        {  
			Default_Details_SeanceDay_Model Default_Details_SeanceDay_Model = new Default_Details_SeanceDay_Model();
			Default_Details_SeanceDay_Model.toStringValue = SeanceDay.ToString();
			Default_Details_SeanceDay_Model.Name = SeanceDay.Name;
			Default_Details_SeanceDay_Model.Code = SeanceDay.Code;
			Default_Details_SeanceDay_Model.Description = SeanceDay.Description;
			Default_Details_SeanceDay_Model.Id = SeanceDay.Id;
            return Default_Details_SeanceDay_Model;            
        }

		public virtual Default_Details_SeanceDay_Model CreateNew()
        {
            SeanceDay SeanceDay = new SeanceDay();
            Default_Details_SeanceDay_Model Default_Details_SeanceDay_Model = this.ConverTo_Default_Details_SeanceDay_Model(SeanceDay);
            return Default_Details_SeanceDay_Model;
        } 
    }

	public partial class Default_Details_SeanceDay_ModelBLM : BaseDefault_Details_SeanceDay_ModelBLM
	{
		public Default_Details_SeanceDay_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
