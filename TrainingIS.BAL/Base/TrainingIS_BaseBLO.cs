using GApp.BLL;
using GApp.Core.Context;
using GApp.DAL;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;

namespace TrainingIS.BLL.Base
{
    public class TrainingIS_BaseBLO<T> : BaseBLO<T> where T : BaseEntity , new()
    {
        protected UnitOfWork<TrainingISModel> _UnitOfWork = null;

        public TrainingIS_BaseBLO(IBaseDAO<T> entityDAO, GAppContext GAppContext) : base(entityDAO, GAppContext)
        {
          
        }

        public virtual List<T> Convert_DataTable_to_List(DataTable dataTable)
        {
            List<T> Data = new List<T>();
            ImportService importService = new ImportService(dataTable, typeof(T), this.GAppContext);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                T trainingYear = new T();
                importService.Fill_Value(trainingYear, dataRow, this._UnitOfWork);
                Data.Add(trainingYear);
            }
            return Data;
        }
    }
}
