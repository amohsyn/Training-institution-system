using GApp.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class ControllerAppsDAO : BaseDAO<ControllerApp>{
        
		public ControllerAppsDAO(DbContext context) : base(context)
		{

        }

   }
}
