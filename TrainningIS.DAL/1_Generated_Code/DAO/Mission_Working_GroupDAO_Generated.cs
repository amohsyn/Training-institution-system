using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class Mission_Working_GroupDAO : BaseDAO<Mission_Working_Group>{
        
		public Mission_Working_GroupDAO(DbContext context) : base(context)
		{

        }

   }
}
