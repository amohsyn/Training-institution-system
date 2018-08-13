using GApp.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class ActionControllerAppDAO : BaseDAO<ActionControllerApp>{
        
		public ActionControllerAppDAO(DbContext context) : base(context)
		{

        }

   }
}
