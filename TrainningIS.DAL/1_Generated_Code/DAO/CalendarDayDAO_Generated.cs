using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class CalendarDayDAO : BaseDAO<CalendarDay>{
        
		public CalendarDayDAO(DbContext context) : base(context)
		{

        }

   }
}
