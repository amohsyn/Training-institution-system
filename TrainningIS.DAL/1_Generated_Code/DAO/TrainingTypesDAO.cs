using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class TrainingTypesDAO : BaseDAO<TrainingType>{
        
		public TrainingTypesDAO(DbContext context) : base(context)
		{

        }

   }
}
