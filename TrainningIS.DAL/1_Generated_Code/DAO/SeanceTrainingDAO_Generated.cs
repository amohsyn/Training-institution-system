using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class SeanceTrainingDAO : BaseDAO<SeanceTraining>{
        
		public SeanceTrainingDAO(DbContext context) : base(context)
		{

        }

   }
}
