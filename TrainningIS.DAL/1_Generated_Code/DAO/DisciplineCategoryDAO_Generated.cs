using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class DisciplineCategoryDAO : BaseDAO<DisciplineCategory>{
        
		public DisciplineCategoryDAO(DbContext context) : base(context)
		{

        }

   }
}
