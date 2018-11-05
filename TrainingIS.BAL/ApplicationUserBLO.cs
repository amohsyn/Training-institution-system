using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.BLL;
using GApp.Core.Context;
using GApp.DAL;
using TrainingIS.DAL;
using TrainingIS.Entitie_excludes;

namespace TrainingIS.BLL
{
    public class ApplicationUserBLO : Base_NotDb_BLO
    {
        UserBLO UserBLO;
        //public ApplicationUserBLO(GAppContext GAppContext) : base(GAppContext)
        //{
        //    UserBLO = new UserBLO(thisthis.GAppContext);
        //}
        public ApplicationUserBLO( UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork,GAppContext)
        {
            UserBLO = new UserBLO(UnitOfWork, this.GAppContext);
        }

        public IEnumerable FindAll()
        {
            throw new NotImplementedException();
        }

        internal ApplicationUser FindBaseEntityByID(long v)
        {
            throw new NotImplementedException();
        }
    }
}
