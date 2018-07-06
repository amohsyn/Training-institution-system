using TrainingIS.Entities;
using GApp.BLL;
using TrainingIS.DAL;
using System.Data.Entity;
namespace  TrainingIS.BLL
{
	public partial class GroupBLO : BaseBLO<Group>{
	    
		public GroupBLO(DbContext context) : base()
        {
            this.entityDAO = new GroupDAO(context);
        }
		 
		public GroupBLO() : base()
        {
           this.entityDAO = new GroupDAO(TrainingISModel.CreateContext());
        }
 
	}
}
