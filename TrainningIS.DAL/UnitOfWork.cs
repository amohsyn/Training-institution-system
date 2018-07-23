using GApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.DAL
{
    public partial class UnitOfWork : IDisposable
    {
        private TrainingYear _CurrentTrainingYear;
        public TrainingYear CurrentTrainingYear
        {
            set
            {
                _CurrentTrainingYear = value;
            }
            get
            {
                if(this._CurrentTrainingYear == null)
                {
                    string msg = "You have to add a year of training in UnitOfWork object";
                    throw new GAppException(msg);
                }
                return _CurrentTrainingYear;
            }
        }

        public readonly  TrainingISModel context = null;

        public UnitOfWork()
        {
            context = new TrainingISModel();

        }
        public UnitOfWork CreateNewUnitOfWork()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.CurrentTrainingYear = this.CurrentTrainingYear;
            return unitOfWork;
        }


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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
