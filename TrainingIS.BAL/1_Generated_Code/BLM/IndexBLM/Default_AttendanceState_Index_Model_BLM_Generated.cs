//modelType = Default_AttendanceState_Index_Model

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
	public partial class BaseDefault_AttendanceState_Index_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_AttendanceState_Index_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual AttendanceState ConverTo_AttendanceState(Default_AttendanceState_Index_Model Default_AttendanceState_Index_Model)
        {
			AttendanceState AttendanceState = null;
            if (Default_AttendanceState_Index_Model.Id != 0)
            {
                AttendanceState = new AttendanceStateBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_AttendanceState_Index_Model.Id);
            }
            else
            {
                AttendanceState = new AttendanceState();
            } 
			AttendanceState.Trainee = Default_AttendanceState_Index_Model.Trainee;
			AttendanceState.Valid_Note = Default_AttendanceState_Index_Model.Valid_Note;
			AttendanceState.Invalid_Note = Default_AttendanceState_Index_Model.Invalid_Note;
			AttendanceState.Valid_Sanction = Default_AttendanceState_Index_Model.Valid_Sanction;
			AttendanceState.Invalid_Sanction = Default_AttendanceState_Index_Model.Invalid_Sanction;
			AttendanceState.Id = Default_AttendanceState_Index_Model.Id;
            return AttendanceState;
        }
        public virtual Default_AttendanceState_Index_Model ConverTo_Default_AttendanceState_Index_Model(AttendanceState AttendanceState)
        {  
			Default_AttendanceState_Index_Model Default_AttendanceState_Index_Model = new Default_AttendanceState_Index_Model();
			Default_AttendanceState_Index_Model.toStringValue = AttendanceState.ToString();
			Default_AttendanceState_Index_Model.Trainee = AttendanceState.Trainee;
			Default_AttendanceState_Index_Model.Valid_Note = AttendanceState.Valid_Note;
			Default_AttendanceState_Index_Model.Invalid_Note = AttendanceState.Invalid_Note;
			Default_AttendanceState_Index_Model.Valid_Sanction = AttendanceState.Valid_Sanction;
			Default_AttendanceState_Index_Model.Invalid_Sanction = AttendanceState.Invalid_Sanction;
			Default_AttendanceState_Index_Model.Id = AttendanceState.Id;
            return Default_AttendanceState_Index_Model;            
        }

		public virtual Default_AttendanceState_Index_Model CreateNew()
        {
            AttendanceState AttendanceState = new AttendanceStateBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_AttendanceState_Index_Model Default_AttendanceState_Index_Model = this.ConverTo_Default_AttendanceState_Index_Model(AttendanceState);
            return Default_AttendanceState_Index_Model;
        } 

		public virtual List<Default_AttendanceState_Index_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            AttendanceStateBLO entityBLO = new AttendanceStateBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<AttendanceState> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_AttendanceState_Index_Model> ls_models = new List<Default_AttendanceState_Index_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_AttendanceState_Index_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_AttendanceState_Index_ModelBLM : BaseDefault_AttendanceState_Index_Model_BLM
	{
		public Default_AttendanceState_Index_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
