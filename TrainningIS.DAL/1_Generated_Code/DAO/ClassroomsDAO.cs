using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class ClassroomsDAO : BaseDAO<Classroom>{
        
		public ClassroomsDAO(DbContext context) : base(context)
		{

        }

   }
}
