using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class TraineesDAO : BaseDAO<Trainee>{
        
		public TraineesDAO(DbContext context) : base(context)
		{

        }

   }
}
