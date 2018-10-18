using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class SanctionDAO : BaseDAO<Sanction>{
        
		public SanctionDAO(DbContext context) : base(context)
		{

        }

   }
}
