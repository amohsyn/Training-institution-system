using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class NationalityDAO : BaseDAO<Nationality>{
        
		public NationalityDAO(DbContext context) : base(context)
		{

        }

   }
}
