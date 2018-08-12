using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class AbsencesDAO : BaseDAO<Absence>{
        
		public AbsencesDAO(DbContext context) : base(context)
		{

        }

   }
}
