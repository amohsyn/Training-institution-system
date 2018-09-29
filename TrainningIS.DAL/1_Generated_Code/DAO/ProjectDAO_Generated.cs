using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class ProjectDAO : BaseDAO<Project>{
        
		public ProjectDAO(DbContext context) : base(context)
		{

        }

   }
}
