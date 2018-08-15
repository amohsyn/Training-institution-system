using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class TrainingDAO : BaseDAO<Training>{
        
		public TrainingDAO(DbContext context) : base(context)
		{

        }

   }
}
