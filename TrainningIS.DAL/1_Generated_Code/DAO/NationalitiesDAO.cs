using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class NationalitiesDAO : BaseDAO<Nationality>{
        
		public NationalitiesDAO(DbContext context) : base(context)
		{

        }

   }
}
