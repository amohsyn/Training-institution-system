using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class ControllerAppDAO : BaseDAO<ControllerApp>{
        
		public ControllerAppDAO(DbContext context) : base(context)
		{

        }

   }
}
