//modelType = Default_Classroom_Edit_Model

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
	public partial class BaseDefault_Classroom_Edit_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		private Default_Form_Classroom_ModelBLM Default_Form_Classroom_ModelBLM {set;get;}
        
		public BaseDefault_Classroom_Edit_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_Classroom_ModelBLM = new Default_Form_Classroom_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Classroom ConverTo_Classroom(Default_Classroom_Edit_Model Default_Classroom_Edit_Model)
        {
            var Classroom = Default_Form_Classroom_ModelBLM.ConverTo_Classroom(Default_Classroom_Edit_Model);
            return Classroom;
        }

		public virtual Default_Classroom_Edit_Model ConverTo_Default_Classroom_Edit_Model(Classroom Classroom)
        {
            Default_Classroom_Edit_Model Default_Classroom_Edit_Model = new Default_Classroom_Edit_Model();
            Default_Form_Classroom_ModelBLM.ConverTo_Default_Form_Classroom_Model(Default_Classroom_Edit_Model, Classroom);
            return Default_Classroom_Edit_Model;            
        }

		public virtual Default_Classroom_Edit_Model CreateNew()
        {
            Classroom Classroom = new ClassroomBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Classroom_Edit_Model Default_Classroom_Edit_Model = this.ConverTo_Default_Classroom_Edit_Model(Classroom);
            return Default_Classroom_Edit_Model;
        } 

		public virtual List<Default_Classroom_Edit_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ClassroomBLO entityBLO = new ClassroomBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Classroom> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Classroom_Edit_Model> ls_models = new List<Default_Classroom_Edit_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Classroom_Edit_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Classroom_Edit_ModelBLM : BaseDefault_Classroom_Edit_Model_BLM
	{
		public Default_Classroom_Edit_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
