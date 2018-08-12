using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class SchoollevelsDAO : BaseDAO<Schoollevel>{
        
		public SchoollevelsDAO(DbContext context) : base(context)
		{

        }

   }
}
