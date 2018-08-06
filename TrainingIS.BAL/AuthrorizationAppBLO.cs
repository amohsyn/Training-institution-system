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
        public IEnumerable<AuthrorizationAppFormView> FindAll(RoleApp rolleApp)
        {
            var Query = from authrization in this._UnitOfWork.context.AuthrorizationApps
                        where authrization.RoleApp.Id == rolleApp.Id
                        select authrization;
            var result = Query.ToList();
            return result;
        }
    }
}
