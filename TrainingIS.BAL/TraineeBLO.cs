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

        private void InitUnitOfWork()
        {
            // UnitofWorkInitialization
            UnitOfWork unitOfWorkImport = this._UnitOfWork.CreateNewUnitOfWork();
            this._UnitOfWork = unitOfWorkImport;
            this.entityDAO = new TraineeDAO(unitOfWorkImport.context);
        }

        public ImportReport Import_1(DataTable dataTable)
        {
            // Chekc Reference colone existance
            string refernce_name = nameof(BaseEntity.Reference);
            string local_reference_name = this.TypeEntity().GetProperty(refernce_name).getLocalName();
            if (!dataTable.Columns.Contains(local_reference_name))
            {
                string msg = msg_BLO.The_reference_column_does_not_exist_in_the_import_excel_file;
                throw new ImportException(msg);
            }

            // Init ImportService
            List<string>  navigationPropertiesNames = this._UnitOfWork.context.GetForeignKeyNames(this.TypeEntity()).ToList<string>();
            List<string> foreignKeys = this._UnitOfWork.context.GetForeignKeysIds(this.TypeEntity()).ToList<string>();
            ImportService importService = new ImportService(dataTable,typeof(Trainee), navigationPropertiesNames, foreignKeys);
            int number_of_saved = 0;
            int number_of_updated = 0;

            Operation operation;
            var Properties = this.TypeEntity().GetProperties();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                // Create UnitOfWork by Row
                this.InitUnitOfWork();

                String reference = dataRow[local_reference_name].ToString();

                #region Create or Louad Trainee Instance
                int index = dataTable.Rows.IndexOf(dataRow);
                // the Reference can't be empty
                if (string.IsNullOrEmpty(reference))
                {
                    string msg = string.Format(msgBLO.The_reference_of_the_entity_can_not_be_empty, index + 1);
                    importService.Report.AddMessage(msg, MessageTypes.Error);
                    continue;
                }
                // Add new if the entity not exist
                Trainee entity = this.FindBaseEntityByReference(reference);
                if (entity == null)
                {
                    entity = new Trainee();
                    operation = Operation.Add;
                }
                else
                {
                    operation = Operation.Update;
                }
                #endregion


               

                // Save or Update Entity
                try
                {
                    importService.Fill_Value(entity, dataRow, this._UnitOfWork);

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
                        string msg = string.Format(msgBLO.Updatring_the_entity, entity);
                        importService.Report.AddMessage(msg, MessageTypes.Update_Success);
                    }
                }
                catch (GApp.DAL.Exceptions.DataBaseEntityValidationException e)
                {
                    string msg = string.Format(" ! erreur à la ligne {0} :", index + 1) + e.Message;
                    importService.Report.AddMessage(msg, MessageTypes.Error);
                    importService.Report.Add_DataRows_WithDataBaseErros(dataRow);
                }
                catch (GApp.DAL.Exceptions.GAppDataBaseException e)
                {
                    string msg = string.Format(" ! erreur à la ligne {0} :", index + 1) + e.Message;

                    importService.Report.AddMessage(msg, MessageTypes.Error);
                    importService.Report.Add_DataRows_WithDataBaseErros(dataRow);
                }
                catch (Exception e)
                {
                    string msg = string.Format(" ! erreur à la ligne {0} :", index + 1) + e.Message;
                    importService.Report.AddMessage(msg, MessageTypes.Error);
                    importService.Report.Add_DataRows_WithDataBaseErros(dataRow);
                    //  throw new ImportException(msg);

                }

                // Init Context after each insert
                // this._UnitOfWork.Reset();
                //this.Init();
                
            }

            // Resume
            string resume_msg = string.Format(msgBLO.In_total_there_is_the_insertion_of, number_of_saved) + " " + msg_Trainee.PluralName;
            importService.Report.AddMessage(resume_msg, MessageTypes.Resume_Info);
            resume_msg = string.Format(msgBLO.In_total_there_is_the_update_of, number_of_updated) + " " + msg_Trainee.PluralName;
            importService.Report.AddMessage(resume_msg, MessageTypes.Resume_Info);

            return importService.Report;
        }

        private void Init()
        {
            this.entityDAO = this._UnitOfWork.TraineeDAO;
        }
    }
}
