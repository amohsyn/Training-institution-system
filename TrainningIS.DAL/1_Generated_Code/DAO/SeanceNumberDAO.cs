using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class SeanceNumberDAO : BaseDAO<SeanceNumber>{
        
		public SeanceNumberDAO(DbContext context) : base(context)
		{

        }

   }
}
