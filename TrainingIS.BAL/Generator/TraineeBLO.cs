using TrainingIS.Entities;
using GApp.BLL;
using TrainingIS.DAL;
using System.Data.Entity;
namespace  TrainingIS.BLL
{
	public partial class TraineeBLO : BaseBLO<Trainee>{
	    
		public TraineeBLO(DbContext context) : base()
        {
            this.entityDAO = new TraineeDAO(context);
        }
		 
		public TraineeBLO() : base()
        {
           this.entityDAO = new TraineeDAO(TrainingISModel.CreateContext());
        }
 
	}
}
