using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;

namespace TrainingIS.BLL
{
    public  partial class TraineeBLO
    {

        public List<string> getForeignKeyMembers(Type typeEntity)
        {
            EntityType TraineeEntityType = DAL.TrainingISModel.CreateContext().getEntityType(typeEntity);
            var NavigationMember = TraineeEntityType.NavigationProperties.Select(p => p.Name).ToList<string>();
            List<string> ForeignKeys = new List<string>();
            for (int i = 0; i < NavigationMember.Count(); i++)
            {
                ForeignKeys.Add( NavigationMember[i] + "Id");
            }
            return ForeignKeys;
        }

        public DataTable Export()
        {
            DataTable formerDataTable = new DataTable("Trainee");


            var foreignKeys = getForeignKeyMembers(typeof(Trainee));

            //var ls = .GetForeignKeyNames(typeof(Trainee));
            var Properties = typeof(Trainee).GetProperties();
            foreach (PropertyInfo item in Properties)
            {
                if (!foreignKeys.Contains(item.Name))
                {
                    DataColumn column = new DataColumn();
                    column.ColumnName = item.Name;
                    formerDataTable.Columns.Add(column);
                }
               
            }

            var formers = this.FindAll();

            foreach (var former in formers)
            {
                DataRow dataRow = formerDataTable.NewRow();
                foreach (PropertyInfo item in Properties)
                {
                    if (!foreignKeys.Contains(item.Name))
                    {
                        dataRow[item.Name] = item.GetValue(former);
                    }
                        
                }
                formerDataTable.Rows.Add(dataRow);
            }

            return formerDataTable;


        }

        public void Import(DataTable dataTable)
        {
            var Properties = typeof(Former).GetProperties();


            foreach (DataRow dataRow in dataTable.Rows)
            {
                String reference = dataRow[nameof(BaseEntity.Reference)].ToString();

                // Add if not exist
                if (this.FindBaseEntityByReference(reference) == null)
                {
                    Trainee trainee = new Trainee();

                    // Fill Primitive value
                    GApp.Core.Utils.ConversionUtil.FillBeanFieldsByDataRow_PrimitiveValue(trainee, dataRow);

                    // Fill non Primitive value
                    foreach (PropertyInfo propertyInfo in Properties)
                    {
                        // if One to One 
                        // if OneToMany
                        // if ManyToMany

                        // propertyInfo.SetValue(former, dataRow[propertyInfo.Name]);
                    }
                    this.Save(trainee);
                }


            }
        }
    }
}
