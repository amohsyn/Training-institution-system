using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class LogWorksDAO : BaseDAO<LogWork>{
        
		public LogWorksDAO(DbContext context) : base(context)
		{

        }

   }
}
