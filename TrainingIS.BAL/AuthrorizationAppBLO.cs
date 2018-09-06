using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class AuthrorizationAppBLO
    {
 
        public IEnumerable<AuthrorizationApp> FindAll(RoleApp rolleApp)
        {
            var Query = from authrization in this._UnitOfWork.context.AuthrorizationApps
                        where authrization.RoleApp.Id == rolleApp.Id
                        select authrization;
            var result = Query.ToList();
            return result;
        }

        public override int Save(AuthrorizationApp item)
        {
            this.is_Update_Reference = true;
            return base.Save(item);
        }
    }
}
