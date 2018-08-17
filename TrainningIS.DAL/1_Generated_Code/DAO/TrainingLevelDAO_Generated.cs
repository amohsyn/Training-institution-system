using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class TrainingLevelDAO : BaseDAO<TrainingLevel>{
        
		public TrainingLevelDAO(DbContext context) : base(context)
		{

        }

   }
}
