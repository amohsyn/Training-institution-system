using GApp.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class LogWorkDAO : BaseDAO<LogWork>{
        
		public LogWorkDAO(DbContext context) : base(context)
		{

        }

   }
}
