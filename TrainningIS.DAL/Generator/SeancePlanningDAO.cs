using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class SeancePlanningDAO : BaseDAO<SeancePlanning>{
        
		public SeancePlanningDAO(DbContext context) : base(context)
		{

        }

   }
}
