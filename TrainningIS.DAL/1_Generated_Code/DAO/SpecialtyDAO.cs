using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class SpecialtyDAO : BaseDAO<Specialty>{
        
		public SpecialtyDAO(DbContext context) : base(context)
		{

        }

   }
}
