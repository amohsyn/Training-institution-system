using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class FunctionDAO : BaseDAO<Function>{
        
		public FunctionDAO(DbContext context) : base(context)
		{

        }

   }
}
