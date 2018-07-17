﻿using GApp.Entities;
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
using TrainingIS.Entities.Resources.TraineeResources;

namespace TrainingIS.BLL
{
    public partial class TraineeBLO
    {

        ///// <summary>
        ///// Import data to dataBase from DataTable
        ///// </summary>
        ///// <param name="dataTable"></param>
        //public string Import_3(DataTable dataTable)
        //{
        //    string msg = "";
        //    int number_of_saved = 0;
        //    var Properties = this.TypeEntity().GetProperties();
        //    foreach (DataRow dataRow in dataTable.Rows)
        //    {
        //        String reference = dataRow[nameof(BaseEntity.Reference)].ToString();

        //        if (string.IsNullOrEmpty(reference))
        //        {
        //            int index = dataTable.Rows.IndexOf(dataRow);
        //            msg += " * " + string.Format(msgBLO.The_reference_of_the_entity_can_not_be_empty, index + 1) + "<br>";
        //            continue;
        //        }

        //        // Add new if the entity not exist
        //        Trainee entity = this.FindBaseEntityByReference(reference);
        //        if (entity == null)
        //        {
        //            entity = new Trainee();

        //            // Fill primitive value from DataRow
        //            GApp.Core.Utils.ConversionUtil.FillBeanFieldsByDataRow_PrimitiveValue(entity, dataRow);

        //            // Fill none primitive value
        //            var navigationPropertiesNames = this.NavigationPropertiesNames();
        //            foreach (PropertyInfo propertyInfo in Properties)
        //            {
        //                if (navigationPropertiesNames.Contains(propertyInfo.Name))
        //                {
        //                    // Generic Algo

        //                    //// if One to One or OneToMany
        //                    string navigationMemberReference = dataRow[propertyInfo.Name].ToString();
        //                    Type navigationMemberType = propertyInfo.PropertyType;

        //                    // if One to One or OneToMany
        //                    //if (propertyInfo.Name == "Group")
        //                    //{
        //                    //    GroupBLO groupBLO = new GroupBLO();
        //                    //    var navigatationMemberValue = groupBLO.FindBaseEntityByReference(navigationMemberReference);
        //                    //    propertyInfo.SetValue(entity, navigatationMemberValue);
        //                    //}


        //                    DAL.TrainingISModel trainingISModel = this._UnitOfWork.context;
        //                    var navigationProperty_set = trainingISModel.Set(propertyInfo.PropertyType);
        //                    var vlaue = navigationProperty_set.Local.OfType<BaseEntity>().Where(e => e.Reference == navigationMemberReference).FirstOrDefault();
        //                    propertyInfo.SetValue(entity, vlaue);
        //                    // if ManyToMany
        //                }



        //            }

        //            this.Save(entity);
        //            number_of_saved++;
        //            msg += " + " + string.Format(msgBLO.Inserting_the_entity, entity) + "<br>";

        //        }
        //        else
        //        {
        //            msg += " - " + string.Format(msgBLO.the_entity_already_exists, entity) + "<br>";
        //        }

        //    }

        //    msg += "<hr>";
        //    msg += string.Format(msgBLO.In_total_there_is_the_insertion_of, number_of_saved) + " " + msg_Trainee.PluralName;
        //    return msg;
        //}

        ///// <summary>
        ///// Import data to dataBase from DataTable
        ///// </summary>
        ///// <param name="dataTable"></param>
        //public void Import_2(DataTable dataTable)
        //{

        //    var Properties = this.TypeEntity().GetProperties();
        //    foreach (DataRow dataRow in dataTable.Rows)
        //    {
        //        String reference = dataRow[nameof(BaseEntity.Reference)].ToString();

        //        // Add new if the entity not exist
        //        if (this.FindBaseEntityByReference(reference) == null)
        //        {
        //            Trainee entity = new Trainee();

        //            // Fill primitive value from DataRow
        //            GApp.Core.Utils.ConversionUtil.FillBeanFieldsByDataRow_PrimitiveValue(entity, dataRow);

        //            // Fill none primitive value
        //            var navigationPropertiesNames = this.NavigationPropertiesNames();
        //            foreach (PropertyInfo propertyInfo in Properties)
        //            {
        //                if (navigationPropertiesNames.Contains(propertyInfo.Name))
        //                {

        //                    // get One-to-Many member value



        //                    // Generic Algo

        //                    //// if One to One or OneToMany
        //                    // string navigationMemberReference = dataRow[propertyInfo.Name].ToString();
        //                    // Type navigationMemberType = propertyInfo.PropertyType;
        //                    // //// Create navigationMemberBLO Instance 
        //                    // Type[] typeArgs = { navigationMemberType };
        //                    // var navigationMemberBLOGenericType = typeof(BaseBLO<BaseEntity>).MakeGenericType(typeArgs);
        //                    //// var navigationMemberBLO = Activator.CreateInstance(navigationMemberBLOGenericType, this.entityDAO.CurrentContext) as IBaseBLO<BaseEntity>;
        //                    //// var navigatationMemberValue = navigationMemberBLO.FindBaseEntityByReference(navigationMemberReference);
        //                    // propertyInfo.SetValue(entity, navigatationMemberValue);


        //                    // Static algo

        //                    // if One to One or OneToMany
        //                    ////if (propertyInfo.Name == "Group")
        //                    ////{
        //                    ////    GroupBLO groupBLO = new GroupBLO();
        //                    ////    var navigatationMemberValue = groupBLO.FindBaseEntityByReference(navigationMemberReference);
        //                    ////    propertyInfo.SetValue(entity, navigatationMemberValue);
        //                    ////}
        //                    // if ManyToMany

        //                    DAL.TrainingISModel trainingISModel =  this._UnitOfWork.context;
        //                    var navigationProperty_set = trainingISModel.Set(propertyInfo.PropertyType);
        //                    var vlaue = navigationProperty_set.Local.OfType<BaseEntity>().Where(e => e.Reference == "").FirstOrDefault();
        //                    propertyInfo.SetValue(entity, vlaue);
        //                }



        //            }
        //            this.Save(entity);
        //        }

        //    }
        //}

    }
}
