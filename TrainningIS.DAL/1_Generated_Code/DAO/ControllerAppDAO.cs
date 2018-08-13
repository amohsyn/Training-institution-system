using GApp.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class ControllerAppDAO : BaseDAO<ControllerApp>{
        
		public ControllerAppDAO(DbContext context) : base(context)
		{

        }

   }
}
