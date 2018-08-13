using GApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;

namespace TrainingIS.BLL.ModelsViews
{
    public class BaseModelBLM
    {
        public UnitOfWork<TrainingISModel> UnitOfWork = null;
        public BaseModelBLM(UnitOfWork<TrainingISModel> unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public DateTime DefaultDateTime_If_Empty(DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue) return DateTime.Now;
            return dateTime;
        }
    }
}
