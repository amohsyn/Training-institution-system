using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class FormerBLO
    {
        /// <summary>
        /// After Save we create the user account for the forer if not yet exist
        /// </summary>
        /// <param name="former"></param>
        /// <returns></returns>
        public override int Save(Former former)
        {
            int return_value = base.Save(former);

            // Create User if not yet created

            // By default we create a user for the former that have email
            // login : email
            // password : matricule
            if (!string.IsNullOrEmpty(former.Email) && !string.IsNullOrEmpty(former.RegistrationNumber))
            {
                UserBLO userBLO = new UserBLO();
                ApplicationUser user = userBLO.FindByLogin(former.Email);
                if(user == null)
                {
                    user = new ApplicationUser();
                    user.UserName = former.Email;
                    user.PhoneNumber = former.Cellphone;
                    userBLO.CreateUser(user, former.RegistrationNumber, RoleBLO.Former_ROLE);
                }
            }
            return return_value;
        }

        //public DataTable Export()
        //{
        //    DataTable formerDataTable = new DataTable("Formateurs");

        //    var Properties = typeof(Former).GetProperties();
        //    foreach (PropertyInfo item in Properties)
        //    {
        //        DataColumn column = new DataColumn();
        //        column.ColumnName = item.Name;
        //        formerDataTable.Columns.Add(column);
        //    }

        //    var formers = this.FindAll();

        //    foreach (var former in formers)
        //    {
        //        DataRow dataRow = formerDataTable.NewRow();
        //        foreach (PropertyInfo item in Properties)
        //        {
        //            dataRow[item.Name] = item.GetValue(former);
        //        }
        //        formerDataTable.Rows.Add(dataRow);
        //    }

        //    return formerDataTable;


        //}

        //public void Import(DataTable dataTable)
        //{
        //    var Properties = typeof(Former).GetProperties();


        //    foreach (DataRow dataRow in dataTable.Rows)
        //    {
        //        String reference = dataRow[nameof(BaseEntity.Reference)].ToString();

        //        // Add if not exist
        //        if(this.FindBaseEntityByReference(reference) == null)
        //        {
        //            Former former = new Former();

        //            // Fill Primitive value
        //            GApp.Core.Utils.ConversionUtil.FillBeanFieldsByDataRow_PrimitiveValue(former, dataRow);

        //            // Fill non Primitive value
        //            foreach (PropertyInfo propertyInfo in Properties)
        //            {
        //                // if One to One 
        //                // if OneToMany
        //                // if ManyToMany

        //                // propertyInfo.SetValue(former, dataRow[propertyInfo.Name]);
        //            }
        //            this.Save(former);
        //        }


        //    }
        //}
    }
}
