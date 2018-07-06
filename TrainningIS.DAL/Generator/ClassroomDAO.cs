using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class ClassroomDAO : BaseDAO<Classroom>{
        
		public ClassroomDAO(DbContext context) : base(context)
		{

        }

		public ClassroomDAO() : base(null)
		{
			this.Context = new TrainingISModel();
        }
   }
}
