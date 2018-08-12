using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class ApplicationParamsDAO : BaseDAO<ApplicationParam>{
        
		public ApplicationParamsDAO(DbContext context) : base(context)
		{

        }

   }
}
