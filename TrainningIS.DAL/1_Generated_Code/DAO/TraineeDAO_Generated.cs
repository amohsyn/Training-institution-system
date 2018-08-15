using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class TraineeDAO : BaseDAO<Trainee>{
        
		public TraineeDAO(DbContext context) : base(context)
		{

        }

   }
}
