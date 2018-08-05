using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class LogWorkDAO : BaseDAO<LogWork>{
        
		public LogWorkDAO(DbContext context) : base(context)
		{

        }

   }
}
