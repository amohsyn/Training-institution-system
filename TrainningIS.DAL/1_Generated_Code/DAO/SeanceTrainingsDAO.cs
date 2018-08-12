using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class SeanceTrainingsDAO : BaseDAO<SeanceTraining>{
        
		public SeanceTrainingsDAO(DbContext context) : base(context)
		{

        }

   }
}
