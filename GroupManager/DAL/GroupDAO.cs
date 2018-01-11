using App.Entities;
using DAL;
using GApp.DAL;
using System.Data.Entity;

namespace App.DAL
{
    public partial class GroupDAO : BaseDAO<Group> , IBaseDAO<Group>
    {
        
		public GroupDAO(DbContext context) : base(context)
		{

        }

		public GroupDAO() : base(null)
		{
			this.Context = new ModelContext();
        }
   }
}
