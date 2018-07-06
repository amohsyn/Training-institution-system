using TrainingIS.Entities;
using GApp.BLL;
using TrainingIS.DAL;
using System.Data.Entity;
namespace  TrainingIS.BLL
{
	public partial class ClassroomBLO : BaseBLO<Classroom>{
	    
		public ClassroomBLO(DbContext context) : base()
        {
            this.entityDAO = new ClassroomDAO(context);
        }
		 
		public ClassroomBLO() : base()
        {
           this.entityDAO = new ClassroomDAO(TrainingISModel.CreateContext());
        }
 
	}
}
