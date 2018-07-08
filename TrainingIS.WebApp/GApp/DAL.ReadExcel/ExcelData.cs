using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;


namespace GApp.DAL.ReadExcel
{
    public class ExcelData
    {


        // Plug and chug, it works
        string _path;

        public ExcelData(string path)
        {
            _path = path;
        }

        public IExcelDataReader getExcelReader()
        {
            // ExcelDataReader works with the binary Excel file, so it needs a FileStream
            // to get started. This is how we avoid dependencies on ACE or Interop:
            FileStream stream = File.Open(_path, FileMode.Open, FileAccess.Read);

            // We return the interface, so that
            IExcelDataReader reader = null;
            try
            {
                if (_path.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                if (_path.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                return reader;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<string> getWorksheetNames()
        {
            var reader = this.getExcelReader();

           

            var workbook = reader.AsDataSet();
            //using System.Data
            var sheets = from DataTable sheet in workbook.Tables select sheet.TableName;
            return sheets;
        }

        public IEnumerable<DataRow> getData()
        {
            var reader = this.getExcelReader();
            var workSheet = reader.AsDataSet().Tables[0];
            var rows = from DataRow a in workSheet.Rows select a;
            return rows;
        }

        public DataTable getFirstTable()
        {
            var reader = this.getExcelReader();

            //// reader.IsFirstRowAsColumnNames
            var conf = new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true
                }
            };

            var workSheet = reader.AsDataSet(conf).Tables[0];
            reader.Close();
            return workSheet;
        }

        public List<string> GetColums(DataTable dataTable)
        {
           // DataTable dataTable = this.getFirstTable();

            List<string> columns = new List<string>();
            foreach (var item in dataTable.Columns)
            {
                DataColumn dataColumn = (DataColumn)item;
                columns.Add(dataColumn.ColumnName);
            }
            
            return columns;
        }
    }
}