using GApp.Core.Context;
using GApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;

namespace TrainingIS.BLL
{
    public abstract class Base_NotDb_BLO
    {
        protected GAppContext GAppContext;
        protected UnitOfWork<TrainingISModel> UnitOfWork;

        public Base_NotDb_BLO(GAppContext GAppContext)
        {
            this.GAppContext = GAppContext;

            // 
            // Find UnitOfWork from GAppContext
            // can not call :  new UnitOfWorkBLO(this.GAppContext).ThrowException_If_UnitOfWork_Not_In_GAppContext_Session();
            // because of stackOverFlow
            if (!this.GAppContext.Session.ContainsKey(UnitOfWorkBLO.UnitOfWork_Key))
            {
                string msg_ex = string.Format("The GAppContext Session does not have '{0}' key ", UnitOfWorkBLO.UnitOfWork_Key);
                throw new ArgumentException(msg_ex, nameof(GAppContext));
            }
            this.UnitOfWork = this.GAppContext.Session[UnitOfWorkBLO.UnitOfWork_Key] as UnitOfWork<TrainingISModel>;

        }
    }
}
