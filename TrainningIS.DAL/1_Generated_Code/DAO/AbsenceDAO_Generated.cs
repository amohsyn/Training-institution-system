using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class AbsenceDAO : BaseDAO<Absence>{
        
		public AbsenceDAO(DbContext context) : base(context)
		{

        }

   }
}
