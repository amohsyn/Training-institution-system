using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class TrainingDAO : BaseDAO<Training>{
        
		public TrainingDAO(DbContext context) : base(context)
		{

        }

		public TrainingDAO() : base(null)
		{
			this.Context = new TrainingISModel();
        }
   }
}
