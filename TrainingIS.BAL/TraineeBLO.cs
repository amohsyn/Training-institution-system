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
using GApp.BLL;

namespace TrainingIS.BLL
{
    public partial class TraineeBLO
    {

        /// <summary>
        /// Import data to dataBase from DataTable
        /// </summary>
        /// <param name="dataTable"></param>
        public void Import_2(DataTable dataTable)
        {

            var Properties = this.TypeEntity().GetProperties();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                String reference = dataRow[nameof(BaseEntity.Reference)].ToString();

                // Add new if the entity not exist
                if (this.FindBaseEntityByReference(reference) == null)
                {
                    Trainee entity = new Trainee();

                    // Fill primitive value from DataRow
                    GApp.Core.Utils.ConversionUtil.FillBeanFieldsByDataRow_PrimitiveValue(entity, dataRow);

                    // Fill none primitive value
                    var navigationPropertiesNames = this.NavigationPropertiesNames();
                    foreach (PropertyInfo propertyInfo in Properties)
                    {
                        if (navigationPropertiesNames.Contains(propertyInfo.Name))
                        {
                            // Generic Algo

                            //// if One to One or OneToMany
                            string navigationMemberReference = dataRow[propertyInfo.Name].ToString();
                            Type navigationMemberType = propertyInfo.PropertyType;

                            //// Create navigationMemberBLO Instance 
                            //Type[] typeArgs = { navigationMemberType };
                            //var navigationMemberBLOGenericType = typeof(BaseBLO<BaseEntity>).MakeGenericType(typeArgs);
                            //var navigationMemberBLO = Activator.CreateInstance(navigationMemberBLOGenericType, this.entityDAO.CurrentContext) as IBaseBLO<BaseEntity>;

                            //var navigatationMemberValue = navigationMemberBLO.FindBaseEntityByReference(navigationMemberReference);
                            //propertyInfo.SetValue(trainee, navigatationMemberValue);

                            // if One to One or OneToMany
                            if (propertyInfo.Name == "Group")
                            {
                                GroupBLO groupBLO = new GroupBLO();
                                var navigatationMemberValue = groupBLO.FindBaseEntityByReference(navigationMemberReference);
                                propertyInfo.SetValue(entity, navigatationMemberValue);
                            }
                            // if ManyToMany
                        }



                    }
                    this.Save(entity);
                }

            }
        }

    }
}
