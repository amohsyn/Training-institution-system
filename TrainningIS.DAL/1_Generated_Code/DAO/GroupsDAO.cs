using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class GroupsDAO : BaseDAO<Group>{
        
		public GroupsDAO(DbContext context) : base(context)
		{

        }

   }
}
