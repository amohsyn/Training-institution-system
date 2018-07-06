using TrainingIS.Entities;
using GApp.BLL;
using TrainingIS.DAL;
using System.Data.Entity;
namespace  TrainingIS.BLL
{
	public partial class ClassroomCategoryBLO : BaseBLO<ClassroomCategory>{
	    
		public ClassroomCategoryBLO(DbContext context) : base()
        {
            this.entityDAO = new ClassroomCategoryDAO(context);
        }
		 
		public ClassroomCategoryBLO() : base()
        {
           this.entityDAO = new ClassroomCategoryDAO(TrainingISModel.CreateContext());
        }
 
	}
}
