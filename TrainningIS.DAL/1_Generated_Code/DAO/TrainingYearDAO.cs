using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class TrainingYearDAO : BaseDAO<TrainingYear>{
        
		public TrainingYearDAO(DbContext context) : base(context)
		{

        }

   }
}
