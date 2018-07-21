using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TrainingIS.BLL.Reports
{
    public class NumberOfAbsencesPerTrainee : BaseReport
    {
        List<DataColumn> _Columns = null; 
        public  NumberOfAbsencesPerTrainee():base()
        {
            
        }

        public List<DataColumn> getColums()
        {
            if(_Columns == null)
            {
                _Columns = new List<DataColumn>();
                // Columns
                _Columns.Add(new DataColumn("Nom", typeof(string)));
                //columns.Add(new DataColumn("Prénom", typeof(string)));
                //columns.Add(new DataColumn("Nombre d'absence", typeof(Int32)));
                //foreach (var item in context.Modules)
                //{
                //    string frm = "Nombre d'absence par dans le module {0}";
                //    columns.Add(new DataColumn(string.Format(frm, item.Code), typeof(Int32)) );
                //}
            }
            return _Columns;
        }


        public DataTable getDataTable()
        {
            // Data
            var Query = from g in context.Groups select new { Nom = g.Code };
            var list_data = Query.ToList();

            // Convert list to DataTable
            //
            // Add colums
            DataTable dataTable = new DataTable("Absence");
            foreach (var item in this.getColums())
            {
                dataTable.Columns.Add(item);
            }
            // Insert Data
            foreach (var data in list_data)
            {
                int index = 0;
                foreach (PropertyInfo propertyInfo in data.GetType().GetProperties())
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[index] = propertyInfo.GetValue(data);
                    index++;
                    dataTable.Rows.Add(dataRow);
                }

            }
           
            return dataTable;
        }

        
    }
}
