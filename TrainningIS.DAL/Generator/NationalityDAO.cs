using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class NationalityDAO : BaseDAO<Nationality>{
        
		public NationalityDAO(DbContext context) : base(context)
		{

        }

   }
}
