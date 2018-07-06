using TrainingIS.Entities;
using GApp.BLL;
using TrainingIS.DAL;
using System.Data.Entity;
namespace  TrainingIS.BLL
{
	public partial class TrainingTypeBLO : BaseBLO<TrainingType>{
	    
		public TrainingTypeBLO(DbContext context) : base()
        {
            this.entityDAO = new TrainingTypeDAO(context);
        }
		 
		public TrainingTypeBLO() : base()
        {
           this.entityDAO = new TrainingTypeDAO(TrainingISModel.CreateContext());
        }
 
	}
}
