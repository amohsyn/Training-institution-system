using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class YearStudyDAO : BaseDAO<YearStudy>{
        
		public YearStudyDAO(DbContext context) : base(context)
		{

        }

		public YearStudyDAO() : base(null)
		{
			this.Context = new TrainingISModel();
        }
   }
}
