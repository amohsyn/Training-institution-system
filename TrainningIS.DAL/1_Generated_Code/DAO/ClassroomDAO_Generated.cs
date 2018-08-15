using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class ClassroomDAO : BaseDAO<Classroom>{
        
		public ClassroomDAO(DbContext context) : base(context)
		{

        }

   }
}
