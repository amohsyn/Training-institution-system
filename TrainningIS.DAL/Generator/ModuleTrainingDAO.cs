using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class ModuleTrainingDAO : BaseDAO<ModuleTraining>{
        
		public ModuleTrainingDAO(DbContext context) : base(context)
		{

        }

   }
}
