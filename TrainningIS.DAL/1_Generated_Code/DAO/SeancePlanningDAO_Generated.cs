using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class SeancePlanningDAO : BaseDAO<SeancePlanning>{
        
		public SeancePlanningDAO(DbContext context) : base(context)
		{

        }

   }
}
