using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class StateOfAbseceDAO : BaseDAO<StateOfAbsece>{
        
		public StateOfAbseceDAO(DbContext context) : base(context)
		{

        }

   }
}
