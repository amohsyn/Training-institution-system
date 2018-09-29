using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class TaskProjectDAO : BaseDAO<TaskProject>{
        
		public TaskProjectDAO(DbContext context) : base(context)
		{

        }

   }
}
