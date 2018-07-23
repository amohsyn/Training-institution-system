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
using TrainingIS.BLL.Resources;
using TrainingIS.DAL;

namespace TrainingIS.BLL
{
    public partial class TraineeBLO
    {
        public ImportReport Import_1(DataTable dataTable)
        {
            // Chekc Reference colone existance
            string local_reference_name = this.CheckExistanceOfReferenceColumn(dataTable);

            // Creae ImportService instance
            List<string> navigationPropertiesNames = this._UnitOfWork.context.GetForeignKeyNames(this.TypeEntity()).ToList<string>();
            List<string> foreignKeys = this._UnitOfWork.context.GetForeignKeysIds(this.TypeEntity()).ToList<string>();
            ImportService importService = new ImportService(dataTable, typeof(Trainee), navigationPropertiesNames, foreignKeys);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                // Create UnitOfWork by Row
                this.InitUnitOfWork();

                // Read entity reference : the Reference can't be empty
                String entity_reference = dataRow[local_reference_name].ToString();
                int index = dataRow.Table.Rows.IndexOf(dataRow);
                if (string.IsNullOrEmpty(entity_reference))
                {
                    string msg = string.Format(msgBLO.The_reference_of_the_entity_can_not_be_empty, index + 1);
                    importService.Report.AddMessage(msg, MessageTypes.Error, dataRow);
                    continue;
                }

                // Load or Create Entity

                Operation operation;
                Trainee entity = this.Load_Or_CreateEntity(importService, entity_reference);
                if (entity.Id == 0) operation = Operation.Add;
                else operation = Operation.Update;


                // Save or Update Entity
                try
                {
                    // Fill value from DataRow
                    importService.Fill_Value(entity, dataRow, this._UnitOfWork);

                    // Save Entity to database
                    this.Save(entity);

                    if (operation == Operation.Add)
                    {
                        string msg = string.Format(msgBLO.Inserting_the_entity, entity);
                        importService.Report.AddMessage(msg, MessageTypes.Add_Success);
                    }
                    else
                    {
                        string msg = string.Format(msgBLO.Updatring_the_entity, entity);
                        importService.Report.AddMessage(msg, MessageTypes.Update_Success);
                    }
                }
                catch (GApp.DAL.Exceptions.DataBaseEntityValidationException e)
                {
                    string msg = string.Format(" ! erreur à la ligne {0} :", index + 1) + e.Message;
                    if (operation == Operation.Add)
                        importService.Report.AddMessage(msg, MessageTypes.Add_Error, dataRow);
                    else
                        importService.Report.AddMessage(msg, MessageTypes.Update_Error, dataRow);
                }
                catch (GApp.DAL.Exceptions.GAppDataBaseException e)
                {
                    string msg = string.Format(" ! erreur à la ligne {0} :", index + 1) + e.Message;

                    if (operation == Operation.Add)
                        importService.Report.AddMessage(msg, MessageTypes.Add_Error, dataRow);
                    else
                        importService.Report.AddMessage(msg, MessageTypes.Update_Error, dataRow);
                }
            }
            return importService.Report;
        }



        #region Import private function
        private string CheckExistanceOfReferenceColumn(DataTable dataTable)
        {
            string refernce_name = nameof(BaseEntity.Reference);
            string local_reference_name = this.TypeEntity().GetProperty(refernce_name).getLocalName();
            if (!dataTable.Columns.Contains(local_reference_name))
            {
                string msg = msg_BLO.The_reference_column_does_not_exist_in_the_import_excel_file;
                throw new ImportException(msg);
            }
            return local_reference_name;
        }
        private void InitUnitOfWork()
        {
            // UnitofWorkInitialization
            UnitOfWork unitOfWorkImport = this._UnitOfWork.CreateNewUnitOfWork();
            this._UnitOfWork = unitOfWorkImport;
            this.entityDAO = new TraineeDAO(unitOfWorkImport.context);
        }
        private Trainee Load_Or_CreateEntity(ImportService importService, string entity_reference)
        {
            Operation operation;
            Trainee entity = this.FindBaseEntityByReference(entity_reference);
            if (entity == null) // Add new if the entity not exist
            {
                entity = new Trainee();
                operation = Operation.Add;
            }
            else
            {
                operation = Operation.Update;
            }
            return entity;
        }
        #endregion
        //private void Init()
        //{
        //    this.entityDAO = this._UnitOfWork.TraineeDAO;
        //}
    }
}
