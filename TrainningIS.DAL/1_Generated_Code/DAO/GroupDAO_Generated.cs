using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class GroupDAO : BaseDAO<Group>{
        
		public GroupDAO(DbContext context) : base(context)
		{

        }

   }
}
