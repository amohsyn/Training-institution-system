using TrainingIS.Entities;
using GApp.BLL;
using TrainingIS.DAL;
using System.Data.Entity;
namespace  TrainingIS.BLL
{
	public partial class SpecialtyBLO : BaseBLO<Specialty>{
	    
		public SpecialtyBLO(DbContext context) : base()
        {
            this.entityDAO = new SpecialtyDAO(context);
        }
		 
		public SpecialtyBLO() : base()
        {
           this.entityDAO = new SpecialtyDAO(TrainingISModel.CreateContext());
        }
 
	}
}
