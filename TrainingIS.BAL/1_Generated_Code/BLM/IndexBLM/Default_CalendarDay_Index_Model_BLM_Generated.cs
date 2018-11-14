//modelType = Default_CalendarDay_Index_Model

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
	public partial class BaseDefault_CalendarDay_Index_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_CalendarDay_Index_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual CalendarDay ConverTo_CalendarDay(Default_CalendarDay_Index_Model Default_CalendarDay_Index_Model)
        {
			CalendarDay CalendarDay = null;
            if (Default_CalendarDay_Index_Model.Id != 0)
            {
                CalendarDay = new CalendarDayBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_CalendarDay_Index_Model.Id);
            }
            else
            {
                CalendarDay = new CalendarDay();
            } 
			CalendarDay.Date = DefaultDateTime_If_Empty(Default_CalendarDay_Index_Model.Date);
			CalendarDay.DateName = Default_CalendarDay_Index_Model.DateName;
			CalendarDay.DateNameAbbrev = Default_CalendarDay_Index_Model.DateNameAbbrev;
			CalendarDay.DayOfWeek = Default_CalendarDay_Index_Model.DayOfWeek;
			CalendarDay.IsWeekend = Default_CalendarDay_Index_Model.IsWeekend;
			CalendarDay.WeekNumber = Default_CalendarDay_Index_Model.WeekNumber;
			CalendarDay.WeekBeginDate = DefaultDateTime_If_Empty(Default_CalendarDay_Index_Model.WeekBeginDate);
			CalendarDay.WeekEndDate = DefaultDateTime_If_Empty(Default_CalendarDay_Index_Model.WeekEndDate);
			CalendarDay.CalendarMonthName = Default_CalendarDay_Index_Model.CalendarMonthName;
			CalendarDay.CalendarMonthNameAbbrev = Default_CalendarDay_Index_Model.CalendarMonthNameAbbrev;
			CalendarDay.CalendarMonthBegin = DefaultDateTime_If_Empty(Default_CalendarDay_Index_Model.CalendarMonthBegin);
			CalendarDay.CalendarMonthEnd = DefaultDateTime_If_Empty(Default_CalendarDay_Index_Model.CalendarMonthEnd);
			CalendarDay.CalendarMonthNumber = Default_CalendarDay_Index_Model.CalendarMonthNumber;
			CalendarDay.CalendarYear = Default_CalendarDay_Index_Model.CalendarYear;
			CalendarDay.FiscalYear = Default_CalendarDay_Index_Model.FiscalYear;
			CalendarDay.DayOfYear = Default_CalendarDay_Index_Model.DayOfYear;
			CalendarDay.CalendarYearBegin = DefaultDateTime_If_Empty(Default_CalendarDay_Index_Model.CalendarYearBegin);
			CalendarDay.CalendarYearEnd = DefaultDateTime_If_Empty(Default_CalendarDay_Index_Model.CalendarYearEnd);
			CalendarDay.FiscalYearBegin = DefaultDateTime_If_Empty(Default_CalendarDay_Index_Model.FiscalYearBegin);
			CalendarDay.FiscalYearEnd = DefaultDateTime_If_Empty(Default_CalendarDay_Index_Model.FiscalYearEnd);
			CalendarDay.Id = Default_CalendarDay_Index_Model.Id;
            return CalendarDay;
        }
        public virtual Default_CalendarDay_Index_Model ConverTo_Default_CalendarDay_Index_Model(CalendarDay CalendarDay)
        {  
			Default_CalendarDay_Index_Model Default_CalendarDay_Index_Model = new Default_CalendarDay_Index_Model();
			Default_CalendarDay_Index_Model.toStringValue = CalendarDay.ToString();
			Default_CalendarDay_Index_Model.Date = DefaultDateTime_If_Empty(CalendarDay.Date);
			Default_CalendarDay_Index_Model.DateName = CalendarDay.DateName;
			Default_CalendarDay_Index_Model.DateNameAbbrev = CalendarDay.DateNameAbbrev;
			Default_CalendarDay_Index_Model.DayOfWeek = CalendarDay.DayOfWeek;
			Default_CalendarDay_Index_Model.IsWeekend = CalendarDay.IsWeekend;
			Default_CalendarDay_Index_Model.WeekNumber = CalendarDay.WeekNumber;
			Default_CalendarDay_Index_Model.WeekBeginDate = DefaultDateTime_If_Empty(CalendarDay.WeekBeginDate);
			Default_CalendarDay_Index_Model.WeekEndDate = DefaultDateTime_If_Empty(CalendarDay.WeekEndDate);
			Default_CalendarDay_Index_Model.CalendarMonthName = CalendarDay.CalendarMonthName;
			Default_CalendarDay_Index_Model.CalendarMonthNameAbbrev = CalendarDay.CalendarMonthNameAbbrev;
			Default_CalendarDay_Index_Model.CalendarMonthBegin = DefaultDateTime_If_Empty(CalendarDay.CalendarMonthBegin);
			Default_CalendarDay_Index_Model.CalendarMonthEnd = DefaultDateTime_If_Empty(CalendarDay.CalendarMonthEnd);
			Default_CalendarDay_Index_Model.CalendarMonthNumber = CalendarDay.CalendarMonthNumber;
			Default_CalendarDay_Index_Model.CalendarYear = CalendarDay.CalendarYear;
			Default_CalendarDay_Index_Model.FiscalYear = CalendarDay.FiscalYear;
			Default_CalendarDay_Index_Model.DayOfYear = CalendarDay.DayOfYear;
			Default_CalendarDay_Index_Model.CalendarYearBegin = DefaultDateTime_If_Empty(CalendarDay.CalendarYearBegin);
			Default_CalendarDay_Index_Model.CalendarYearEnd = DefaultDateTime_If_Empty(CalendarDay.CalendarYearEnd);
			Default_CalendarDay_Index_Model.FiscalYearBegin = DefaultDateTime_If_Empty(CalendarDay.FiscalYearBegin);
			Default_CalendarDay_Index_Model.FiscalYearEnd = DefaultDateTime_If_Empty(CalendarDay.FiscalYearEnd);
			Default_CalendarDay_Index_Model.Id = CalendarDay.Id;
            return Default_CalendarDay_Index_Model;            
        }

		public virtual Default_CalendarDay_Index_Model CreateNew()
        {
            CalendarDay CalendarDay = new CalendarDayBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_CalendarDay_Index_Model Default_CalendarDay_Index_Model = this.ConverTo_Default_CalendarDay_Index_Model(CalendarDay);
            return Default_CalendarDay_Index_Model;
        } 

		public virtual List<Default_CalendarDay_Index_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            CalendarDayBLO entityBLO = new CalendarDayBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<CalendarDay> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_CalendarDay_Index_Model> ls_models = new List<Default_CalendarDay_Index_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_CalendarDay_Index_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_CalendarDay_Index_ModelBLM : BaseDefault_CalendarDay_Index_Model_BLM
	{
		public Default_CalendarDay_Index_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
