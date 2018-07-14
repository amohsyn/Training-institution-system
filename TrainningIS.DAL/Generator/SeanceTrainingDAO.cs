using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class SeanceTrainingDAO : BaseDAO<SeanceTraining>{
        
		public SeanceTrainingDAO(DbContext context) : base(context)
		{

        }

		public SeanceTrainingDAO() : base(null)
		{
			this.Context = new TrainingISModel();
        }
   }
}
