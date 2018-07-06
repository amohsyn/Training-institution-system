using TrainingIS.Entities;
using GApp.BLL;
using TrainingIS.DAL;
using System.Data.Entity;
namespace  TrainingIS.BLL
{
	public partial class FormerBLO : BaseBLO<Former>{
	    
		public FormerBLO(DbContext context) : base()
        {
            this.entityDAO = new FormerDAO(context);
        }
		 
		public FormerBLO() : base()
        {
           this.entityDAO = new FormerDAO(TrainingISModel.CreateContext());
        }
 
	}
}
