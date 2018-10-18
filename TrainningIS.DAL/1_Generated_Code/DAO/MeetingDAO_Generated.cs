using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class MeetingDAO : BaseDAO<Meeting>{
        
		public MeetingDAO(DbContext context) : base(context)
		{

        }

   }
}
