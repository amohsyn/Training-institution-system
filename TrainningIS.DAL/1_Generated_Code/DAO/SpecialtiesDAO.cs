using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class SpecialtiesDAO : BaseDAO<Specialty>{
        
		public SpecialtiesDAO(DbContext context) : base(context)
		{

        }

   }
}
