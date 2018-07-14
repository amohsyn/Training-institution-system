using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class AbsenceDAO : BaseDAO<Absence>{
        
		public AbsenceDAO(DbContext context) : base(context)
		{

        }

		public AbsenceDAO() : base(null)
		{
			this.Context = new TrainingISModel();
        }
   }
}
