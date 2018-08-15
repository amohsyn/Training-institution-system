using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class ScheduleDAO : BaseDAO<Schedule>{
        
		public ScheduleDAO(DbContext context) : base(context)
		{

        }

   }
}
