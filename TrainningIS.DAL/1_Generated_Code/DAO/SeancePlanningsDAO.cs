using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class SeancePlanningsDAO : BaseDAO<SeancePlanning>{
        
		public SeancePlanningsDAO(DbContext context) : base(context)
		{

        }

   }
}
