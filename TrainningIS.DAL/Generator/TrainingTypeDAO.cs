using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class TrainingTypeDAO : BaseDAO<TrainingType>{
        
		public TrainingTypeDAO(DbContext context) : base(context)
		{

        }

   }
}
