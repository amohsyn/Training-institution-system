using App.Entities;
using GApp.BLL;
using App.DAL;
using System.Data.Entity;
using DAL;

namespace App.BLL
{
    public partial class SpecialtyBLO : BaseBLO<Specialty>, IBaseBLO<Specialty>
    {

        public SpecialtyBLO(DbContext context) : base()
        {
            this.entityDAO = new SpecialtyDAO(context);
        }

        public SpecialtyBLO() : base()
        {
            this.entityDAO = new SpecialtyDAO(new ModelContext());
        }

    }
}
