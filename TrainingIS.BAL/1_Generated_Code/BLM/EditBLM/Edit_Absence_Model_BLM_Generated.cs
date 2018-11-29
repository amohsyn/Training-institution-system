//modelType = Edit_Absence_Model

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
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseEdit_Absence_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		public Form_Absence_ModelBLM Form_Absence_ModelBLM {set;get;}
        
		public BaseEdit_Absence_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Form_Absence_ModelBLM = new Form_Absence_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Absence ConverTo_Absence(Edit_Absence_Model Edit_Absence_Model)
        {
            var Absence = Form_Absence_ModelBLM.ConverTo_Absence(Edit_Absence_Model);
            return Absence;
        }

		public virtual Edit_Absence_Model ConverTo_Edit_Absence_Model(Absence Absence)
        {
            Edit_Absence_Model Edit_Absence_Model = new Edit_Absence_Model();
            Form_Absence_ModelBLM.ConverTo_Form_Absence_Model(Edit_Absence_Model, Absence);
            return Edit_Absence_Model;            
        }

		public virtual Edit_Absence_Model CreateNew()
        {
            Absence Absence = new AbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Edit_Absence_Model Edit_Absence_Model = this.ConverTo_Edit_Absence_Model(Absence);
            return Edit_Absence_Model;
        } 

		public virtual List<Edit_Absence_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            AbsenceBLO entityBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Absence> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Edit_Absence_Model> ls_models = new List<Edit_Absence_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Edit_Absence_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Edit_Absence_ModelBLM : BaseEdit_Absence_Model_BLM
	{
		public Edit_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
