using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class TrainingYearDAO : BaseDAO<TrainingYear>{
        
		public TrainingYearDAO(DbContext context) : base(context)
		{

        }

   }
}
