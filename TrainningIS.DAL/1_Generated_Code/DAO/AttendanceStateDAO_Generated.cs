using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class AttendanceStateDAO : BaseDAO<AttendanceState>{
        
		public AttendanceStateDAO(DbContext context) : base(context)
		{

        }

   }
}
