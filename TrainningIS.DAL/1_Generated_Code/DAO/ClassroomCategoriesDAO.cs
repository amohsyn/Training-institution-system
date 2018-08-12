using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class ClassroomCategoriesDAO : BaseDAO<ClassroomCategory>{
        
		public ClassroomCategoriesDAO(DbContext context) : base(context)
		{

        }

   }
}
