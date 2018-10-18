using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class SanctionCategoryDAO : BaseDAO<SanctionCategory>{
        
		public SanctionCategoryDAO(DbContext context) : base(context)
		{

        }

   }
}
