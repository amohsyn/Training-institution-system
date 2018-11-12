using GApp.BLL;
using GApp.Core.Context;
using GApp.DAL;
using GApp.DAL.Excel;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL.Services.Import;
using TrainingIS.DAL;

namespace TrainingIS.BLL.Base
{
    public class TrainingIS_BaseBLO<T> : BaseBLO<T> where T : BaseEntity , new()
    {
        public BaseExcelDAO<T> ExcelDAO = new BaseExcelDAO<T>();
        public string PluralName { set; get; }
        protected UnitOfWork<TrainingISModel> _UnitOfWork = null;

        public TrainingIS_BaseBLO(IBaseDAO<T> entityDAO, GAppContext GAppContext) : base(entityDAO, GAppContext)
        {
          
        }

        #region Convert Data
        /// <summary>
        /// Export all data to DataTable
        /// </summary>
        /// <returns>DataTable contain all data in database</returns>
        public virtual DataTable Convert_to_DataTable(List<T> Data)
        {
            ExportService exportService = new ExportService(typeof(T));
            DataTable entityDataTable = exportService.CreateDataTable(this.PluralName);
            exportService.Fill(entityDataTable, Data.ToList<object>());
            return entityDataTable;
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
        #endregion

    }
}
