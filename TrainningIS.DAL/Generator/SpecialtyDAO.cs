using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainningIS.DAL.DAL
{
    public partial class SpecialtyDAO : BaseDAO<Specialty>{
        
		public SpecialtyDAO(DbContext context) : base(context)
		{

        }

		public SpecialtyDAO() : base(null)
		{
			this.Context = new TrainingISModel();
        }
   }
}
