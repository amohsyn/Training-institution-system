using GApp.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class GPictureDAO : BaseDAO<GPicture>{
        
		public GPictureDAO(DbContext context) : base(context)
		{

        }

   }
}
