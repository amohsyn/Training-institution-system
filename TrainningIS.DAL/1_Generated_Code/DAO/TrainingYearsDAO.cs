using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class TrainingYearsDAO : BaseDAO<TrainingYear>{
        
		public TrainingYearsDAO(DbContext context) : base(context)
		{

        }

   }
}
