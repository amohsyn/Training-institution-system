using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;

namespace TrainingIS.BLL.Services
{
    public partial class DataBaseBakupService
    {
        UnitOfWork _UnitOfWork;
        public DataBaseBakupService(UnitOfWork UnitOfWork)
        {
            this._UnitOfWork = UnitOfWork;
        }
        public DataSet Export()
        {
            DataSet dataSet = new DataSet();
            this.AddDataTablesToDataSet(dataSet);
            return dataSet;
        }
    }
}
