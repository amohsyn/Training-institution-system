using TrainingIS.Entities;
using GApp.BLL;
using TrainingIS.DAL;
using System.Data.Entity;
namespace  TrainingIS.BLL
{
	public partial class TrainingYearBLO : BaseBLO<TrainingYear>{
	    
		public TrainingYearBLO(DbContext context) : base()
        {
            this.entityDAO = new TrainingYearDAO(context);
        }
		 
		public TrainingYearBLO() : base()
        {
           this.entityDAO = new TrainingYearDAO(new TrainingISModel());
        }
 
	}
}
