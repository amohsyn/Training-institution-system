using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class ModuleTrainingDAO : BaseDAO<ModuleTraining>{
        
		public ModuleTrainingDAO(DbContext context) : base(context)
		{

        }

   }
}
