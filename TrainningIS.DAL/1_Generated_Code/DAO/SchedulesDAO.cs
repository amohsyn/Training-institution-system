using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class SchedulesDAO : BaseDAO<Schedule>{
        
		public SchedulesDAO(DbContext context) : base(context)
		{

        }

   }
}
