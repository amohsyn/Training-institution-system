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
using TrainingIS.Entities.Resources.TraineeResources;
using static TrainingIS.BLL.MessagesService;

namespace TrainingIS.BLL
{
    public partial class TraineeBLO
    {

        public override DataTable Export()
        {
            ImportService importService = new ImportService(this.TypeEntity(), this._UnitOfWork.context);
            var entities = this.FindAll();
            DataTable entityDataTable = new DataTable("Entities");

            var foreignKeys = this._UnitOfWork.context.GetForeignKeysIds(typeof(Trainee));
            var Keys =  this._UnitOfWork.context.GetKeyNames(typeof(Trainee));

            var navigationPropertiesNames = this.NavigationPropertiesNames();

            // Create DataColumn Names
            var Properties = typeof(Trainee).GetProperties();
            foreach (PropertyInfo item in Properties)
            {
                string local_name_of_property = importService.getLocalNameOfProperty(item);

                // d'ont show foreignKeys members
                if (!foreignKeys.Contains(item.Name) && !Keys.Contains(item.Name))
                {
                    DataColumn column = new DataColumn();
                    column.ColumnName = local_name_of_property;
                    entityDataTable.Columns.Add(column);
                }

            }

            foreach (var entity in entities)
            {
                DataRow dataRow = entityDataTable.NewRow();
                foreach (PropertyInfo item in Properties)
                {
                    if (!foreignKeys.Contains(item.Name) && !Keys.Contains(item.Name))
                    {
                        string local_name_of_property = importService.getLocalNameOfProperty(item);

                        if (navigationPropertiesNames.Contains(item.Name))
                        {
                            // OneToOne or ManyToOne
                            var value = item.GetValue(entity) as BaseEntity;
                            if (value != null)
                                dataRow[local_name_of_property] = value.Reference;
                        }
                        else
                        {
                            dataRow[local_name_of_property] = item.GetValue(entity);
                        }

                    }
                }
                entityDataTable.Rows.Add(dataRow);
            }
            return entityDataTable;
        }


        public override string Import(DataTable dataTable)
        {
            ImportService importService = new ImportService(typeof(Trainee), this._UnitOfWork.context);
            int number_of_saved = 0;
            int number_of_updated = 0;

            Operation operation;
            var Properties = this.TypeEntity().GetProperties();
            foreach (DataRow dataRow in dataTable.Rows)
            {

                String reference = dataRow[nameof(BaseEntity.Reference)].ToString();

                #region Create or Louad Trainee Instance
                int index = dataTable.Rows.IndexOf(dataRow);
                // the Reference can't be empty
                if (string.IsNullOrEmpty(reference)){
                      string msg = string.Format(msgBLO.The_reference_of_the_entity_can_not_be_empty, index + 1);
                    importService.Report.AddMessage(msg, MessageTypes.Error);
                    continue;
                }
                // Add new if the entity not exist
                Trainee entity = this.FindBaseEntityByReference(reference);
                if (entity == null){
                    entity = new Trainee();
                    operation = Operation.Add;
                }else{
                    operation = Operation.Update;
                }
                #endregion


                importService.Fill_Value(entity, dataRow);
                     
                // Save or Update Entity
                try
                {
                    this.Save(entity);
                    if (operation == Operation.Add)
                    {
                        number_of_saved++;
                        string msg = string.Format(msgBLO.Inserting_the_entity, entity);
                        importService.Report.AddMessage(msg, MessageTypes.Add_Success);

                    }
                    else
                    {
                        number_of_updated++;
                        string msg =   string.Format(msgBLO.Updatring_the_entity, entity);
                        importService.Report.AddMessage(msg, MessageTypes.Update_Success);
                    }
                }
                catch (Exception e)
                {
                    string msg = string.Format(" ! erreur à la ligne {0} :", index + 1) + e.Message ;
                    importService.Report.AddMessage(msg, MessageTypes.Error);
                    throw new ImportLineException(msg);

                }
            }

            // Resume
            string resume_msg = string.Format(msgBLO.In_total_there_is_the_insertion_of, number_of_saved) + " " + msg_Trainee.PluralName;
            importService.Report.AddMessage(resume_msg, MessageTypes.Resume_Info);
            resume_msg = string.Format(msgBLO.In_total_there_is_the_update_of, number_of_updated) + " " + msg_Trainee.PluralName;
            importService.Report.AddMessage(resume_msg, MessageTypes.Resume_Info);

            return importService.Report.getReport();
        }


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
