using GApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using TrainingIS.BLL.Exceptions;
using TrainingIS.BLL.Services.Import;
using TrainingIS.DAL;
using TrainingIS.Entities;
using TrainingIS.Entities.Resources.TrainingResources;
using static GApp.BLL.Services.MessagesService;

namespace TrainingIS.BLL
{
    public partial class TrainingBLO
    {
        public List<Entities.Group> Get_Groups_Of_Former(Former current_Former)
        {
            TrainingYear trainingYear = new TrainingYearBLO(this._UnitOfWork, this.GAppContext).getCurrentTrainingYear();
            // [Bug] add CurrentTraining Year Condition
            List<Entities.Group> Groups = this._UnitOfWork.context.Trainings
                .Where(t => t.Former.Id == current_Former.Id)
                .Where(t => t.TrainingYearId == trainingYear.Id)
                .Select(t => t.Group)
                .Distinct()
                .ToList();

            return Groups;



        }
        public List<ModuleTraining> Get_ModuleTraining_Of_Former(Former current_former)
        {
            TrainingYear trainingYear = new TrainingYearBLO(this._UnitOfWork, this.GAppContext).getCurrentTrainingYear();

            // [Bug] add CurrentTraining Year Condition
            List<ModuleTraining> ModuleTrainings = this._UnitOfWork.context.Trainings
                .Where(t => t.Former.Id == current_former.Id)
                .Where(t=> t.TrainingYearId == trainingYear.Id)
                .Select(t => t.ModuleTraining)
                .Distinct()
                .ToList();

            return ModuleTrainings;
        }

        public List<ImportReport> Ismontic_Import(DataTable dataTable, string FileName = "")
        {

            // Create ImportService instance
            List<string> foreignKeys = this._UnitOfWork.context.GetForeignKeysIds(this.TypeEntity()).ToList<string>();
            ImportService importService_Ismontic = new ImportService(dataTable, typeof(Training), this.GAppContext);

            // Check DataTable Name
            if (dataTable.TableName != "MATRICE")
            {
                string msg = string.Format("Le nom {0} de la feuille Excel  est incorrect, le programme d'import attend une feuille Excel avec le nom 'MATRICE' ", dataTable.TableName);
                throw new BLL_Exception(msg);
            }

            // Converto Ismontic_DataTable to Cplus_DataTable
            ExportService exportService = new ExportService(typeof(Training));
            DataTable Trainings_DataTable = exportService.CreateDataTable(msg_Training.PluralName);


            int _index = -1;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                _index++;



                // UnitofWorkInitialization
                this.InitUnitOfWork();

                // BLO Instance
                GroupBLO groupBLO = new GroupBLO(this._UnitOfWork, this.GAppContext);
                ModuleTrainingBLO moduleTrainingBLO = new ModuleTrainingBLO(this._UnitOfWork, this.GAppContext);


                // Read Ismontic Data
                string Ismontic_Specialty_Reference = dataRow["CodeFiliere"] as string;
                if(this.Throw_Read_Exception_If_Null(Ismontic_Specialty_Reference, "CodeFiliere",importService_Ismontic,dataRow, _index)) continue;

                string Ismontic_YearOfTraining_Number = dataRow["ANNEE"].ToString();
                if (string.IsNullOrEmpty(Ismontic_YearOfTraining_Number)) Ismontic_YearOfTraining_Number = null;
                if (this.Throw_Read_Exception_If_Null(Ismontic_YearOfTraining_Number, "ANNEE", importService_Ismontic, dataRow, _index)) continue;

                string Ismontic_Group_Code = dataRow["GROUPE"] as string;
                if (this.Throw_Read_Exception_If_Null(Ismontic_Group_Code, "GROUPE", importService_Ismontic, dataRow, _index)) continue;

                string Ismontic_Module_Code = dataRow["N° Module"] as string;
                if (this.Throw_Read_Exception_If_Null(Ismontic_Module_Code, "N° Module", importService_Ismontic, dataRow, _index)) continue;

                try
                {
                    string data = dataRow["MH A ENSEIGNER"].ToString();
                    if (string.IsNullOrEmpty(data)) data = null;
                    if (this.Throw_Read_Exception_If_Null(Ismontic_Module_Code, "N° Module", importService_Ismontic, dataRow, _index)) continue;

                    if (!string.IsNullOrEmpty(data))
                    {
                        float Ismontic_Trainings_HourMass_To_Learn = float.Parse(dataRow["MH A ENSEIGNER"].ToString());
                    }
                    
                }
                catch (Exception)
                {
                    string msg = string.Format("Ligne {0} : Impossible de lire le nombre {1} ", _index + 2, "MH A ENSEIGNER");
                    importService_Ismontic.Report.AddMessage(msg, MessageTypes.Error, dataRow);
                    continue;

                }
               

                string Former_Complete_Name = dataRow["FORMATEUR"] as string;
                if (this.Throw_Read_Exception_If_Null(Former_Complete_Name, "FORMATEUR", importService_Ismontic, dataRow, _index)) continue;

                // 
                // Convert to Cplus_Data

                // Training Year
                string TrainingYear_Reference = new TrainingYearBLO(this._UnitOfWork, this.GAppContext).getCurrentTrainingYear().Reference;

                // Module
                string Module_Reference = string.Format("{0}-{1}-{2}", Ismontic_Specialty_Reference, Ismontic_Module_Code, Ismontic_YearOfTraining_Number);
                if (moduleTrainingBLO.FindBaseEntityByReference(Module_Reference) == null)
                {
                    string msg = string.Format("Ligne {0} : Le module {1} n'exist pas dans la base de données ", _index + 2, Module_Reference);
                    importService_Ismontic.Report.AddMessage(msg, MessageTypes.Error, dataRow);
                    continue;
                }

                //Former
                string Former_Complete_Name_Replace = Former_Complete_Name.Replace("-", "").Replace(" ", "");
                Former Former = (from f in this._UnitOfWork.context.Formers
                                 where (f.FirstName.Replace("-", "").Replace(" ", "") + f.LastName.Replace("-", "").Replace(" ", "")) == Former_Complete_Name_Replace
                                 select f)
                                 .FirstOrDefault();

                if (Former == null)
                {
                    string msg = string.Format("Ligne {0} : Le formateur {1} n'exist pas dans la base de données ", _index + 2, Former_Complete_Name);
                    importService_Ismontic.Report.AddMessage(msg, MessageTypes.Error, dataRow);
                    continue;
                }

                // Group
                string Group_Reference = string.Format("{0}-{1}_{2}", Ismontic_Group_Code, Ismontic_Specialty_Reference, TrainingYear_Reference);
                if (groupBLO.FindBaseEntityByReference(Group_Reference) == null)
                {
                    string msg = string.Format("Ligne {0} : Le group {1} n'exist pas dans la base de données ", _index + 2, Group_Reference);
                    importService_Ismontic.Report.AddMessage(msg, MessageTypes.Error, dataRow);
                    continue;
                }

                // Code
                string Code = string.Format("{0}-{1}-{2} {3}", Ismontic_Group_Code, Ismontic_Module_Code, Former.FirstName, Former.LastName);
                string Reference = string.Format("{0}-{1}-{2}", Module_Reference, Former.Reference, Group_Reference);

                // Create Training_DataRow
                DataRow Training_DataRow = Trainings_DataTable.NewRow();

                string TrainingYear_Column_Name = typeof(Training).GetProperty(nameof(Training.TrainingYear)).getLocalName();
                Training_DataRow[TrainingYear_Column_Name] = TrainingYear_Reference;

                string Module_Column_Name = typeof(Training).GetProperty(nameof(Training.ModuleTraining)).getLocalName();
                Training_DataRow[Module_Column_Name] = Module_Reference;

                string Former_Column_Name = typeof(Training).GetProperty(nameof(Training.Former)).getLocalName();
                Training_DataRow[Former_Column_Name] = Former.Reference;

                string Group_Column_Name = typeof(Training).GetProperty(nameof(Training.Group)).getLocalName();
                Training_DataRow[Group_Column_Name] = Group_Reference;

                string Code_Column_Name = typeof(Training).GetProperty(nameof(Training.Code)).getLocalName();
                Training_DataRow[Code_Column_Name] = Code;

                string Reference_Column_Name = typeof(Training).GetProperty(nameof(Training.Reference)).getLocalName();
                Training_DataRow[Reference_Column_Name] = Reference;

                Trainings_DataTable.Rows.Add(Training_DataRow);
            }

            List<ImportReport> importReports = new List<ImportReport>();

            ImportReport cplus_import = this.Import(Trainings_DataTable, FileName);

            importReports.Add(importService_Ismontic.Report);
            importReports.Add(cplus_import);
            return importReports;
        }

        private bool Throw_Read_Exception_If_Null(string data, string ColumnName,ImportService importService_Ismontic,DataRow dataRow,int Index)
        {
            if(data == null)
            {
                string msg = string.Format("Ligne {0} : Impossible de lire l'information {1} ", Index + 2, ColumnName);
                importService_Ismontic.Report.AddMessage(msg, MessageTypes.Error, dataRow);
                return true;
            }
            return false;
        }

        private void InitUnitOfWork()
        {
            // UnitofWorkInitialization
            this._UnitOfWork = new UnitOfWork<TrainingISModel>();
            this.entityDAO = new TrainingDAO(_UnitOfWork.context);
        }
    }
}
