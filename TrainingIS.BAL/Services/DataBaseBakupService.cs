using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TrainingIS.DAL;
using TrainingIS.Entities.Resources.TraineeResources;
using GApp.DAL;

namespace TrainingIS.BLL.Services
{
    public partial class DataBaseBakupService
    {
        UnitOfWork<TrainingISModel> _UnitOfWork;
        public DataBaseBakupService(UnitOfWork<TrainingISModel> UnitOfWork)
        {
            this._UnitOfWork = UnitOfWork;
        }
        public DataSet Export()
        {
            DataSet dataSet = new DataSet();
            this.AddDataTablesToDataSet(dataSet);

            var Types = this._UnitOfWork.context.GetAllTypesInContextOrder();
            this.SortTablesByTypes(dataSet, Types);
         
            return dataSet;
        }

        private void SortTablesByTypes(DataSet dataSet, List<Type> typesOrder)
        {

              
            int order_c = 0;
            var q = from type in typesOrder

                    let order = ++order_c
                    select new {
                        EntityType = type,

                        EntityName = EntityMetaDataConfiguratrion
                    .CreateConfigEntity(type)
                    .entityMetataData.PluralName,

                        Order = order,
                        dataTable = new DataTable()
                     };

            var Entities_Order = q.ToList();


            // Colone DataTables
            List<DataTable> dataTableCollection = dataSet.Tables.Cast<DataTable>().ToList<DataTable>();
            dataSet.Tables.Clear();


            foreach (var entity in Entities_Order)
            {
                DataTable dataTable = dataTableCollection.Where(t => t.TableName == entity.EntityName).FirstOrDefault();
                if (dataTable != null)
                    dataSet.Tables.Add(dataTable);
            }
        }
    }
}
