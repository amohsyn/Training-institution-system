﻿//modelType = Default_Form_Classroom_Model

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
	public partial class BaseDefault_Form_Classroom_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Classroom_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Classroom ConverTo_Classroom(Default_Form_Classroom_Model Default_Form_Classroom_Model)
        {
			Classroom Classroom = null;
            if (Default_Form_Classroom_Model.Id != 0)
            {
                Classroom = new ClassroomBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Classroom_Model.Id);
            }
            else
            {
                Classroom = new Classroom();
            } 
			Classroom.Code = Default_Form_Classroom_Model.Code;
			Classroom.Name = Default_Form_Classroom_Model.Name;
			Classroom.ClassroomCategoryId = Default_Form_Classroom_Model.ClassroomCategoryId;
			Classroom.ClassroomCategory = new ClassroomCategoryBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Classroom_Model.ClassroomCategoryId)) ;
			Classroom.Description = Default_Form_Classroom_Model.Description;
			Classroom.Id = Default_Form_Classroom_Model.Id;
            return Classroom;
        }
        public virtual Default_Form_Classroom_Model ConverTo_Default_Form_Classroom_Model(Classroom Classroom)
        {  
			Default_Form_Classroom_Model Default_Form_Classroom_Model = new Default_Form_Classroom_Model();
			Default_Form_Classroom_Model.toStringValue = Classroom.ToString();
			Default_Form_Classroom_Model.Code = Classroom.Code;
			Default_Form_Classroom_Model.Name = Classroom.Name;
			Default_Form_Classroom_Model.ClassroomCategoryId = Classroom.ClassroomCategoryId;
			Default_Form_Classroom_Model.Description = Classroom.Description;
			Default_Form_Classroom_Model.Id = Classroom.Id;
            return Default_Form_Classroom_Model;            
        }

		public virtual Default_Form_Classroom_Model CreateNew()
        {
            Classroom Classroom = new ClassroomBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Classroom_Model Default_Form_Classroom_Model = this.ConverTo_Default_Form_Classroom_Model(Classroom);
            return Default_Form_Classroom_Model;
        } 

		public virtual List<Default_Form_Classroom_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ClassroomBLO entityBLO = new ClassroomBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Classroom> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_Classroom_Model> ls_models = new List<Default_Form_Classroom_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_Classroom_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_Classroom_ModelBLM : BaseDefault_Form_Classroom_ModelBLM
	{
		public Default_Form_Classroom_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
