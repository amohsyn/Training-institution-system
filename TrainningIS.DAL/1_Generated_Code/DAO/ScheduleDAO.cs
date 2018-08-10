using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class ScheduleDAO : BaseDAO<Schedule>{
        
		public ScheduleDAO(DbContext context) : base(context)
		{

        }

   }
}
