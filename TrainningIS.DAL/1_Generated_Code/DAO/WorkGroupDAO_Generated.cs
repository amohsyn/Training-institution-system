using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class WorkGroupDAO : BaseDAO<WorkGroup>{
        
		public WorkGroupDAO(DbContext context) : base(context)
		{

        }

   }
}
