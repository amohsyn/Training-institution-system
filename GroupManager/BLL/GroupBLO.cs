using App.Entities;
using GApp.BLL;
using App.DAL;
using System.Data.Entity;
using DAL;

namespace App.BLL
{
    public partial class GroupBLO :  BaseBLO<Group> , IBaseBLO<Group>
    {

        public GroupBLO(DbContext context) : base()
        {
            this.entityDAO = new GroupDAO(context);
        }

        public GroupBLO() : base()
        {
            this.entityDAO = new GroupDAO(new ModelContext());
        }

    }
}
