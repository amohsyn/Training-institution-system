//modelType = Default_Form_CalendarDay_Model

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
	public partial class BaseDefault_Form_CalendarDay_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_CalendarDay_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual CalendarDay ConverTo_CalendarDay(Default_Form_CalendarDay_Model Default_Form_CalendarDay_Model)
        {
			CalendarDay CalendarDay = null;
            if (Default_Form_CalendarDay_Model.Id != 0)
            {
                CalendarDay = new CalendarDayBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_CalendarDay_Model.Id);
            }
            else
            {
                CalendarDay = new CalendarDay();
            } 
			CalendarDay.Date = DefaultDateTime_If_Empty(Default_Form_CalendarDay_Model.Date);
			CalendarDay.DateName = Default_Form_CalendarDay_Model.DateName;
			CalendarDay.DateNameAbbrev = Default_Form_CalendarDay_Model.DateNameAbbrev;
			CalendarDay.DayOfWeek = Default_Form_CalendarDay_Model.DayOfWeek;
			CalendarDay.IsWeekend = Default_Form_CalendarDay_Model.IsWeekend;
			CalendarDay.WeekNumber = Default_Form_CalendarDay_Model.WeekNumber;
			CalendarDay.WeekBeginDate = DefaultDateTime_If_Empty(Default_Form_CalendarDay_Model.WeekBeginDate);
			CalendarDay.WeekEndDate = DefaultDateTime_If_Empty(Default_Form_CalendarDay_Model.WeekEndDate);
			CalendarDay.CalendarMonthName = Default_Form_CalendarDay_Model.CalendarMonthName;
			CalendarDay.CalendarMonthNameAbbrev = Default_Form_CalendarDay_Model.CalendarMonthNameAbbrev;
			CalendarDay.CalendarMonthBegin = DefaultDateTime_If_Empty(Default_Form_CalendarDay_Model.CalendarMonthBegin);
			CalendarDay.CalendarMonthEnd = DefaultDateTime_If_Empty(Default_Form_CalendarDay_Model.CalendarMonthEnd);
			CalendarDay.CalendarMonthNumber = Default_Form_CalendarDay_Model.CalendarMonthNumber;
			CalendarDay.CalendarYear = Default_Form_CalendarDay_Model.CalendarYear;
			CalendarDay.FiscalYear = Default_Form_CalendarDay_Model.FiscalYear;
			CalendarDay.DayOfYear = Default_Form_CalendarDay_Model.DayOfYear;
			CalendarDay.CalendarYearBegin = DefaultDateTime_If_Empty(Default_Form_CalendarDay_Model.CalendarYearBegin);
			CalendarDay.CalendarYearEnd = DefaultDateTime_If_Empty(Default_Form_CalendarDay_Model.CalendarYearEnd);
			CalendarDay.FiscalYearBegin = DefaultDateTime_If_Empty(Default_Form_CalendarDay_Model.FiscalYearBegin);
			CalendarDay.FiscalYearEnd = DefaultDateTime_If_Empty(Default_Form_CalendarDay_Model.FiscalYearEnd);
			CalendarDay.Id = Default_Form_CalendarDay_Model.Id;
            return CalendarDay;
        }
        public virtual Default_Form_CalendarDay_Model ConverTo_Default_Form_CalendarDay_Model(CalendarDay CalendarDay)
        {  
			Default_Form_CalendarDay_Model Default_Form_CalendarDay_Model = new Default_Form_CalendarDay_Model();
			Default_Form_CalendarDay_Model.toStringValue = CalendarDay.ToString();
			Default_Form_CalendarDay_Model.Date = DefaultDateTime_If_Empty(CalendarDay.Date);
			Default_Form_CalendarDay_Model.DateName = CalendarDay.DateName;
			Default_Form_CalendarDay_Model.DateNameAbbrev = CalendarDay.DateNameAbbrev;
			Default_Form_CalendarDay_Model.DayOfWeek = CalendarDay.DayOfWeek;
			Default_Form_CalendarDay_Model.IsWeekend = CalendarDay.IsWeekend;
			Default_Form_CalendarDay_Model.WeekNumber = CalendarDay.WeekNumber;
			Default_Form_CalendarDay_Model.WeekBeginDate = DefaultDateTime_If_Empty(CalendarDay.WeekBeginDate);
			Default_Form_CalendarDay_Model.WeekEndDate = DefaultDateTime_If_Empty(CalendarDay.WeekEndDate);
			Default_Form_CalendarDay_Model.CalendarMonthName = CalendarDay.CalendarMonthName;
			Default_Form_CalendarDay_Model.CalendarMonthNameAbbrev = CalendarDay.CalendarMonthNameAbbrev;
			Default_Form_CalendarDay_Model.CalendarMonthBegin = DefaultDateTime_If_Empty(CalendarDay.CalendarMonthBegin);
			Default_Form_CalendarDay_Model.CalendarMonthEnd = DefaultDateTime_If_Empty(CalendarDay.CalendarMonthEnd);
			Default_Form_CalendarDay_Model.CalendarMonthNumber = CalendarDay.CalendarMonthNumber;
			Default_Form_CalendarDay_Model.CalendarYear = CalendarDay.CalendarYear;
			Default_Form_CalendarDay_Model.FiscalYear = CalendarDay.FiscalYear;
			Default_Form_CalendarDay_Model.DayOfYear = CalendarDay.DayOfYear;
			Default_Form_CalendarDay_Model.CalendarYearBegin = DefaultDateTime_If_Empty(CalendarDay.CalendarYearBegin);
			Default_Form_CalendarDay_Model.CalendarYearEnd = DefaultDateTime_If_Empty(CalendarDay.CalendarYearEnd);
			Default_Form_CalendarDay_Model.FiscalYearBegin = DefaultDateTime_If_Empty(CalendarDay.FiscalYearBegin);
			Default_Form_CalendarDay_Model.FiscalYearEnd = DefaultDateTime_If_Empty(CalendarDay.FiscalYearEnd);
			Default_Form_CalendarDay_Model.Id = CalendarDay.Id;
            return Default_Form_CalendarDay_Model;            
        }

		public virtual Default_Form_CalendarDay_Model CreateNew()
        {
            CalendarDay CalendarDay = new CalendarDayBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_CalendarDay_Model Default_Form_CalendarDay_Model = this.ConverTo_Default_Form_CalendarDay_Model(CalendarDay);
            return Default_Form_CalendarDay_Model;
        } 

		public virtual List<Default_Form_CalendarDay_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            CalendarDayBLO entityBLO = new CalendarDayBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<CalendarDay> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_CalendarDay_Model> ls_models = new List<Default_Form_CalendarDay_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_CalendarDay_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_CalendarDay_ModelBLM : BaseDefault_Form_CalendarDay_ModelBLM
	{
		public Default_Form_CalendarDay_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
