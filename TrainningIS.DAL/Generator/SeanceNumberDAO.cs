using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class SeanceNumberDAO : BaseDAO<SeanceNumber>{
        
		public SeanceNumberDAO(DbContext context) : base(context)
		{

        }

		public SeanceNumberDAO() : base(null)
		{
			this.Context = new TrainingISModel();
        }
   }
}
