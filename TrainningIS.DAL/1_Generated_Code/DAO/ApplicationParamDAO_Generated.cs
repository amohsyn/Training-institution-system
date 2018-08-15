using GApp.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class ApplicationParamDAO : BaseDAO<ApplicationParam>{
        
		public ApplicationParamDAO(DbContext context) : base(context)
		{

        }

   }
}
