using GApp.BLL;
using GApp.Core.Context;
using GApp.DAL;
using GApp.DAL.Excel;
using GApp.Entities;
using GApp.Models.Pages;
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


        #region Filter Manager
        /// <summary>
        /// Save or Load the Filter, Order and Pagination user parameter from DataBase for the current Controller.
        /// </summary>
        /// <param name="filterRequestParams">The FilterRequestParams object empty or not</param>
        /// <param name="Controller_Reference">Controller_Reference Identifier</param>
        /// <returns>Finded FilterRequestParams if the params is empty</returns>
        public virtual FilterRequestParams Save_OR_Load_filterRequestParams_State(FilterRequestParams filterRequestParams, string Controller_Reference)
        {
            if (filterRequestParams == null)
                filterRequestParams = new FilterRequestParams();

            var applicationParamBLO = new ApplicationParamBLO(this._UnitOfWork, this.GAppContext);
          
            string current_User = applicationParamBLO.GAppContext.Current_User_Name;

            if (filterRequestParams.IsEmpty())
            {
                filterRequestParams = applicationParamBLO.Read_FilterRequestParams_State(current_User, Controller_Reference);
            }
            else
            {
                applicationParamBLO.Save_FilterRequestParams_State(filterRequestParams, current_User, Controller_Reference);
            }

            return filterRequestParams;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void Delete_filterRequestParams_State(string Controller_Reference)
        {
            var applicationParamBLO = new ApplicationParamBLO(this._UnitOfWork, this.GAppContext);
            string current_User = applicationParamBLO.GAppContext.Current_User_Name;
            applicationParamBLO.Delete_FilterRequestParams_State(current_User, Controller_Reference);
        }
        #endregion

        #region Import & Export
        /// <summary>
        /// Export all data to DataTable
        /// </summary>
        /// <returns>DataTable contain all data in database</returns>
        public virtual DataTable Import_File_Example()
        {
            ExportService exportService = new ExportService(this.TypeEntity());
            DataTable entityDataTable = exportService.CreateDataTable(this.PluralName);
            exportService.Fill(entityDataTable, this.FindAll().ToList<object>());
            return entityDataTable;
        }
        #endregion

    }
}
