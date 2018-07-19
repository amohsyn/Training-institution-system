using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class ApplicationParamDAO : BaseDAO<ApplicationParam>{
        
		public ApplicationParamDAO(DbContext context) : base(context)
		{

        }

		public ApplicationParamDAO() : base(null)
		{
			this.Context = new TrainingISModel();
        }
   }
}
