using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class SeanceDayDAO : BaseDAO<SeanceDay>{
        
		public SeanceDayDAO(DbContext context) : base(context)
		{

        }

		public SeanceDayDAO() : base(null)
		{
			this.Context = new TrainingISModel();
        }
   }
}
