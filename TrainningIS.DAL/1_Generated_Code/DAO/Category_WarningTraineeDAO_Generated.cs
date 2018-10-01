using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class Category_WarningTraineeDAO : BaseDAO<Category_WarningTrainee>{
        
		public Category_WarningTraineeDAO(DbContext context) : base(context)
		{

        }

   }
}
