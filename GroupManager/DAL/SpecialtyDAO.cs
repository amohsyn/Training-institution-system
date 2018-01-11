using App.Entities;
using DAL;
using GApp.DAL;
using System.Data.Entity;

namespace App.DAL
{
    public partial class SpecialtyDAO : BaseDAO<Specialty> , IBaseDAO<Specialty>
    {
        
		public SpecialtyDAO(DbContext context) : base(context)
		{

        }

		public SpecialtyDAO() : base(null)
		{
			this.Context = new ModelContext();
        }
   }
}
