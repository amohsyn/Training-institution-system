using GApp.Core.Entities.ModelsViews;
using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Models.DataAnnotations;
using GApp.Models;
using GApp.Entities;
using TrainingIS.Entities.enums;
using System.ComponentModel.DataAnnotations;

using GApp.Entities.Resources.BaseEntity;  
 
namespace TrainingIS.Entities.ModelsViews
{
    [FormView(typeof(CalendarDay))]
    public class Default_Form_CalendarDay_Model : BaseModel
    {
		public DateTime Date  {set; get;}  
   
		public String DateName  {set; get;}  
   
		public String DateNameAbbrev  {set; get;}  
   
		public Int32 DayOfWeek  {set; get;}  
   
		public Boolean IsWeekend  {set; get;}  
   
		public Int32 WeekNumber  {set; get;}  
   
		public DateTime WeekBeginDate  {set; get;}  
   
		public DateTime WeekEndDate  {set; get;}  
   
		public String CalendarMonthName  {set; get;}  
   
		public String CalendarMonthNameAbbrev  {set; get;}  
   
		public DateTime CalendarMonthBegin  {set; get;}  
   
		public DateTime CalendarMonthEnd  {set; get;}  
   
		public Int32 CalendarMonthNumber  {set; get;}  
   
		public Int32 CalendarYear  {set; get;}  
   
		public Int32 FiscalYear  {set; get;}  
   
		public Int32 DayOfYear  {set; get;}  
   
		public DateTime CalendarYearBegin  {set; get;}  
   
		public DateTime CalendarYearEnd  {set; get;}  
   
		public DateTime FiscalYearBegin  {set; get;}  
   
		public DateTime FiscalYearEnd  {set; get;}  
   
		[Unique]
		[Display(Name = "Reference", Order = 0, ResourceType = typeof(msg_BaseEntity))]
		[GAppDataTable(PropertyPath = "Reference", FilterBy = "Reference", SearchBy = "Reference", OrderBy = "Reference",  AutoGenerateFilter = false,isColumn = false )]
		public String Reference  {set; get;}  
   
    }
}    
