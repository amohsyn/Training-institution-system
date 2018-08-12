using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class StateOfAbsecesDAO : BaseDAO<StateOfAbsece>{
        
		public StateOfAbsecesDAO(DbContext context) : base(context)
		{

        }

   }
}
