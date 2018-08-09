using GApp.BLL;
using GApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    /// <summary>
    /// BLO - Manager , Get BLO by Type
    /// </summary>
    public partial class BLO_Manager : IDisposable
    {
        public UnitOfWork _UnitOfWork = null;
        public Dictionary<Type, Type> BLO_Types = new Dictionary<Type, Type>();
       

        public Type getBLOType(Type EntityType)
        {
            if (this.BLO_Types.ContainsKey(EntityType))
            {
                return this.BLO_Types[EntityType];
            }
            return null;
        }

        public void Save()
        {
            this._UnitOfWork.context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._UnitOfWork.context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
