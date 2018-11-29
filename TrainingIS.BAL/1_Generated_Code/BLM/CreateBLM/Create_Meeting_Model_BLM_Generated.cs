//modelType = Create_Meeting_Model

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
	public partial class BaseCreate_Meeting_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		public Form_Meeting_ModelBLM Form_Meeting_ModelBLM {set;get;}
        
		public BaseCreate_Meeting_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Form_Meeting_ModelBLM = new Form_Meeting_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Meeting ConverTo_Meeting(Create_Meeting_Model Create_Meeting_Model)
        {
            var Meeting = Form_Meeting_ModelBLM.ConverTo_Meeting(Create_Meeting_Model);
            return Meeting;
        }

		public virtual Create_Meeting_Model ConverTo_Create_Meeting_Model(Meeting Meeting)
        {
            Create_Meeting_Model Create_Meeting_Model = new Create_Meeting_Model();
            Form_Meeting_ModelBLM.ConverTo_Form_Meeting_Model(Create_Meeting_Model, Meeting);
            return Create_Meeting_Model;            
        }

		public virtual Create_Meeting_Model CreateNew()
        {
            Meeting Meeting = new MeetingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Create_Meeting_Model Create_Meeting_Model = this.ConverTo_Create_Meeting_Model(Meeting);
            return Create_Meeting_Model;
        } 

		public virtual List<Create_Meeting_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            MeetingBLO entityBLO = new MeetingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Meeting> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Create_Meeting_Model> ls_models = new List<Create_Meeting_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Create_Meeting_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Create_Meeting_ModelBLM : BaseCreate_Meeting_Model_BLM
	{
		public Create_Meeting_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
