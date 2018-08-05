using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class StateOfAbseceDAO : BaseDAO<StateOfAbsece>{
        
		public StateOfAbseceDAO(DbContext context) : base(context)
		{

        }

   }
}
