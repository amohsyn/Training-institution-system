using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Core.Context;

namespace TrainingIS.BLL
{
    public class UnitOfWorkBLO : Base_NotDb_BLO
    {
        public static string UnitOfWork_Key = "UnitOfWork";

        public UnitOfWorkBLO(GAppContext GAppContext) : base(GAppContext)
        {
        }

        public void ThrowException_If_UnitOfWork_Not_In_GAppContext_Session()
        {
            // Find UnitOfWork from GAppContext
            if (!this.GAppContext.Session.ContainsKey(UnitOfWorkBLO.UnitOfWork_Key))
            {
                string msg_ex = string.Format("The GAppContext Session does not have '{0}' key ", UnitOfWorkBLO.UnitOfWork_Key);
                throw new ArgumentException(msg_ex, nameof(GAppContext));
            }
        }
    }
}
