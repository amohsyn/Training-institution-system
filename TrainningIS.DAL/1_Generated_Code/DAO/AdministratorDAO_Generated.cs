using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class AdministratorDAO : BaseDAO<Administrator>{
        
		public AdministratorDAO(DbContext context) : base(context)
		{

        }

   }
}
