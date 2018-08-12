using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class TrainingsDAO : BaseDAO<Training>{
        
		public TrainingsDAO(DbContext context) : base(context)
		{

        }

   }
}
