using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class ClassroomCategoryDAO : BaseDAO<ClassroomCategory>{
        
		public ClassroomCategoryDAO(DbContext context) : base(context)
		{

        }

		public ClassroomCategoryDAO() : base(null)
		{
			this.Context = new TrainingISModel();
        }
   }
}
