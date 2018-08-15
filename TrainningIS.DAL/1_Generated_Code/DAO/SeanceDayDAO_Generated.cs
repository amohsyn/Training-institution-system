using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class SeanceDayDAO : BaseDAO<SeanceDay>{
        
		public SeanceDayDAO(DbContext context) : base(context)
		{

        }

   }
}
