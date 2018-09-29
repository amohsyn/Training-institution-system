using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class TaskProjectBLO
    {
        public override int Save(TaskProject item)
        {
            // if insert 
            if (item.Id == 0)
            {
                var UserBLO = new UserBLO(this.GAppContext).FindByLogin(this.GAppContext.Current_User_Name);
                item.Owner = UserBLO;
            }
            return base.Save(item);
        }
    }
}
