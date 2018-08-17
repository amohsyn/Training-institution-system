using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class SectorDAO : BaseDAO<Sector>{
        
		public SectorDAO(DbContext context) : base(context)
		{

        }

   }
}
