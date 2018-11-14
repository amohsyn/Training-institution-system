//modelType = Edit_Meeting_Model

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
	public partial class BaseEdit_Meeting_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		private Form_Meeting_ModelBLM Form_Meeting_ModelBLM {set;get;}
        
		public BaseEdit_Meeting_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Form_Meeting_ModelBLM = new Form_Meeting_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Meeting ConverTo_Meeting(Edit_Meeting_Model Edit_Meeting_Model)
        {
            var Meeting = Form_Meeting_ModelBLM.ConverTo_Meeting(Edit_Meeting_Model);
            return Meeting;
        }

		public virtual Edit_Meeting_Model ConverTo_Edit_Meeting_Model(Meeting Meeting)
        {
            Edit_Meeting_Model Edit_Meeting_Model = new Edit_Meeting_Model();
            Form_Meeting_ModelBLM.ConverTo_Form_Meeting_Model(Edit_Meeting_Model, Meeting);
            return Edit_Meeting_Model;            
        }

		public virtual Edit_Meeting_Model CreateNew()
        {
            Meeting Meeting = new MeetingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Edit_Meeting_Model Edit_Meeting_Model = this.ConverTo_Edit_Meeting_Model(Meeting);
            return Edit_Meeting_Model;
        } 

		public virtual List<Edit_Meeting_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            MeetingBLO entityBLO = new MeetingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Meeting> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Edit_Meeting_Model> ls_models = new List<Edit_Meeting_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Edit_Meeting_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Edit_Meeting_ModelBLM : BaseEdit_Meeting_Model_BLM
	{
		public Edit_Meeting_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
