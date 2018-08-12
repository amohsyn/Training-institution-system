using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class ModuleTrainingsDAO : BaseDAO<ModuleTraining>{
        
		public ModuleTrainingsDAO(DbContext context) : base(context)
		{

        }

   }
}
