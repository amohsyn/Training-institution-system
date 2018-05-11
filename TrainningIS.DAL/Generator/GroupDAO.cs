using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainningIS.DAL.DAL
{
    public partial class GroupDAO : BaseDAO<Group>{
        
		public GroupDAO(DbContext context) : base(context)
		{

        }

		public GroupDAO() : base(null)
		{
			this.Context = new TrainingISModel();
        }
   }
}
