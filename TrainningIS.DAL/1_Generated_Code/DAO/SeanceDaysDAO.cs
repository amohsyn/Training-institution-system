using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class SeanceDaysDAO : BaseDAO<SeanceDay>{
        
		public SeanceDaysDAO(DbContext context) : base(context)
		{

        }

   }
}
