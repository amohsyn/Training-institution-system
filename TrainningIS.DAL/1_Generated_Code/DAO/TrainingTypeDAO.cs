using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class TrainingTypeDAO : BaseDAO<TrainingType>{
        
		public TrainingTypeDAO(DbContext context) : base(context)
		{

        }

   }
}
