using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class SchoollevelDAO : BaseDAO<Schoollevel>{
        
		public SchoollevelDAO(DbContext context) : base(context)
		{

        }

   }
}
