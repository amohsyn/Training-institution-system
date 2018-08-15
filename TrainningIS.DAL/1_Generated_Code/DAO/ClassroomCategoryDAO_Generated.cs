using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class ClassroomCategoryDAO : BaseDAO<ClassroomCategory>{
        
		public ClassroomCategoryDAO(DbContext context) : base(context)
		{

        }

   }
}
