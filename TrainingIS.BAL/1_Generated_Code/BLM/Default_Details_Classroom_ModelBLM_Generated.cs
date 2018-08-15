//modelType = Default_Details_Classroom_Model

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
	public partial class BaseDefault_Details_Classroom_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Details_Classroom_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Classroom ConverTo_Classroom(Default_Details_Classroom_Model Default_Details_Classroom_Model)
        {
			Classroom Classroom = null;
            if (Default_Details_Classroom_Model.Id != 0)
            {
                Classroom = new ClassroomBLO(this.UnitOfWork).FindBaseEntityByID(Default_Details_Classroom_Model.Id);
            }
            else
            {
                Classroom = new Classroom();
            } 
			Classroom.Code = Default_Details_Classroom_Model.Code;
			Classroom.Name = Default_Details_Classroom_Model.Name;
			Classroom.ClassroomCategory = Default_Details_Classroom_Model.ClassroomCategory;
			Classroom.Description = Default_Details_Classroom_Model.Description;
			Classroom.Id = Default_Details_Classroom_Model.Id;
            return Classroom;
        }
        public virtual Default_Details_Classroom_Model ConverTo_Default_Details_Classroom_Model(Classroom Classroom)
        {  
			Default_Details_Classroom_Model Default_Details_Classroom_Model = new Default_Details_Classroom_Model();
			Default_Details_Classroom_Model.toStringValue = Classroom.ToString();
			Default_Details_Classroom_Model.Code = Classroom.Code;
			Default_Details_Classroom_Model.Name = Classroom.Name;
			Default_Details_Classroom_Model.ClassroomCategory = Classroom.ClassroomCategory;
			Default_Details_Classroom_Model.Description = Classroom.Description;
			Default_Details_Classroom_Model.Id = Classroom.Id;
            return Default_Details_Classroom_Model;            
        }

		public virtual Default_Details_Classroom_Model CreateNew()
        {
            Classroom Classroom = new Classroom();
            Default_Details_Classroom_Model Default_Details_Classroom_Model = this.ConverTo_Default_Details_Classroom_Model(Classroom);
            return Default_Details_Classroom_Model;
        } 
    }

	public partial class Default_Details_Classroom_ModelBLM : BaseDefault_Details_Classroom_ModelBLM
	{
		public Default_Details_Classroom_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
