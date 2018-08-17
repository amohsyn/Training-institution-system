using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class FormerSpecialtyDAO : BaseDAO<FormerSpecialty>{
        
		public FormerSpecialtyDAO(DbContext context) : base(context)
		{

        }

   }
}
