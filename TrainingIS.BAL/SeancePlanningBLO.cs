using GApp.DAL;
using GApp.Models.Pages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL.Exceptions;
using TrainingIS.BLL.Services.Import;
using TrainingIS.DAL;
using TrainingIS.Entities;
using TrainingIS.Entities.Resources.SeancePlanningResources;
using static GApp.BLL.Services.MessagesService;

namespace TrainingIS.BLL
{
    public partial class SeancePlanningBLO
    {
        public override ImportReport Import(DataTable dataTable, string FileName = "")
        {
            try
            {
                // Check Existance of Schedule Reference
                string Schedule_Column_Name = typeof(SeancePlanning).GetProperty(nameof(SeancePlanning.Schedule)).getLocalName();
                int Schedule_Columns_Index = dataTable.Columns.IndexOf(Schedule_Column_Name);

                if (dataTable.Rows.Count > 1)
                {
                    string Schedule_Reference = dataTable.Rows[1][Schedule_Column_Name] as string;
                    if (string.IsNullOrEmpty(Schedule_Reference))
                    {
                        // Check existance of : Schedule_Columns_Index start Date
                        string Schedule_Start_Date_LocalName = typeof(Schedule).GetProperty(nameof(Schedule.StartDate)).getLocalName();
                        int Schedule_Start_Date_Columns_Index = dataTable.Columns.IndexOf(Schedule_Start_Date_LocalName);

                        if (Schedule_Start_Date_Columns_Index > 0)
                        {
                            // Read Start_Date_Value
                            DateTime Schedule_Start_Date_Value = Convert.ToDateTime(dataTable.Rows[1][Schedule_Start_Date_Columns_Index]);

                            // Create a new Schedule
                            ScheduleBLO scheduleBLO = new ScheduleBLO(this._UnitOfWork, this.GAppContext);
                            Schedule Schedule = scheduleBLO.Close_And_Start_Schedule(Schedule_Start_Date_Value);

                            // Set the new Schedule in DataTable
                            for (int i = 0; i < dataTable.Rows.Count; i++)
                            {
                                dataTable.Rows[i][Schedule_Column_Name] = Schedule.Reference;
                            }
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                throw new ImportException(exception.Message);
            }

            return base.Import(dataTable, FileName);
        }

        public List<SeancePlanning> GetSeancesPlanning(DateTime date_now, SeanceNumber seanceNumber)
        {
            Schedule Current_Schedule = new ScheduleBLO(this._UnitOfWork, this.GAppContext).GetExistantSchedule(date_now);

            if (Current_Schedule != null)
            {
                SeanceDayBLO seanceDayBLO = new SeanceDayBLO(this._UnitOfWork, this.GAppContext);
                SeanceDay SeanceDay = seanceDayBLO.GetSeanceDay(date_now);

                if (SeanceDay != null)
                {
                    List<SeancePlanning> query = this._UnitOfWork.context.SeancePlannings
                                    .Where(s => s.SeanceDay.Reference == SeanceDay.Reference
                                    && s.SeanceNumber.Reference == seanceNumber.Reference
                                    && s.Schedule.Id == Current_Schedule.Id
                                    )
                                    .Select(s => s).ToList();

                    return query;
                }
            }


            return new List<SeancePlanning>();

        }

        public List<SeancePlanning> GetSeancesPlanning(DateTime seanceDate, Former former)
        {
            Schedule Current_Schedule = new ScheduleBLO(this._UnitOfWork, this.GAppContext).GetExistantSchedule(seanceDate);

            if (Current_Schedule != null)
            {
                SeanceDayBLO seanceDayBLO = new SeanceDayBLO(this._UnitOfWork, this.GAppContext);
                SeanceDay SeanceDay = seanceDayBLO.GetSeanceDay(seanceDate);

                if (SeanceDay != null)
                {
                    List<SeancePlanning> query = this._UnitOfWork.context.SeancePlannings
                                    .Where(
                        s => s.SeanceDay.Reference == SeanceDay.Reference
                        && s.Training.Former.Id == former.Id
                        && s.Schedule.Id == Current_Schedule.Id
                        )
                                    .Select(s => s).ToList();

                    return query;
                }
            }


            return new List<SeancePlanning>();

        }


        public List<ImportReport> Ismontic_Import_Time_Table(DataTable dataTable, string ScheduleReference,string FileName = "")
        {

            // Create ImportService instance
            List<string> foreignKeys = this._UnitOfWork.context.GetForeignKeysIds(this.TypeEntity()).ToList<string>();
            ImportService importService_Ismontic = new ImportService(dataTable, typeof(SeancePlanning), this.GAppContext);

            // Check DataTable Name
            if (!dataTable.TableName.ToUpper().Contains("EMPLOI"))
            {
                string msg = string.Format("Le nom {0} de la feuille Excel  est incorrect, le programme d'import attend une feuille Excel avec le nom 'Emploi' ", dataTable.TableName);
                throw new BLL_Exception(msg);
            }

            // Converto Ismontic_DataTable to Cplus_DataTable
            ExportService exportService = new ExportService(typeof(SeancePlanning));
            DataTable SeancePlanning_DataTable = exportService.CreateDataTable(msg_SeancePlanning.PluralName);


            int _index = -1;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                _index++;



                // UnitofWorkInitialization
                this.InitUnitOfWork();



                // Read Ismontic Data
                string Ismontic_ClassRoom_Reference = dataRow["salle"] as string;
                if (this.Throw_Read_Exception_If_Null(Ismontic_ClassRoom_Reference, "salle", importService_Ismontic, dataRow, _index)) continue;

                string Ismontic_Group_Code = dataRow["groupe"] as string;
                if (this.Throw_Read_Exception_If_Null(Ismontic_Group_Code, "groupe", importService_Ismontic, dataRow, _index)) continue;

                string Former_Complete_Name = dataRow["nom-formateur"] as string;
                if (this.Throw_Read_Exception_If_Null(Former_Complete_Name, "nom-formateur", importService_Ismontic, dataRow, _index)) continue;

                string Ismontic_SeanceNumber_Ismontic_Reference = dataRow["séance"] as string;
                if (this.Throw_Read_Exception_If_Null(Ismontic_SeanceNumber_Ismontic_Reference, "séance", importService_Ismontic, dataRow, _index)) continue;


                string Ismontic_Module_Code = dataRow["module"] as string;
                if (this.Throw_Read_Exception_If_Null(Ismontic_Module_Code, "module", importService_Ismontic, dataRow, _index)) continue;


                string Ismontic_Seance_Day_Reference = dataRow["JOUR"] as string;
                if (this.Throw_Read_Exception_If_Null(Ismontic_Seance_Day_Reference, "JOUR", importService_Ismontic, dataRow, _index)) continue;


                // 
                // Convert to Cplus_Data

                // BLO Instance
                TrainingYearBLO trainingYearBLO = new TrainingYearBLO(this._UnitOfWork, this.GAppContext);
                TrainingLevelBLO trainingLevelBLO = new TrainingLevelBLO(this._UnitOfWork, this.GAppContext);
                SpecialtyBLO specialtyBLO = new SpecialtyBLO(this._UnitOfWork, this.GAppContext);
                GroupBLO groupBLO = new GroupBLO(this._UnitOfWork, this.GAppContext);
                ModuleTrainingBLO moduleTrainingBLO = new ModuleTrainingBLO(this._UnitOfWork, this.GAppContext);
                TrainingBLO trainingBLO = new TrainingBLO(this._UnitOfWork, this.GAppContext);
                SeanceDayBLO seanceDayBLO = new SeanceDayBLO(this._UnitOfWork, this.GAppContext);
                SeanceNumberBLO seanceNumberBLO = new SeanceNumberBLO(this._UnitOfWork, this.GAppContext);
                ClassroomBLO classroomBLO = new ClassroomBLO(this._UnitOfWork, this.GAppContext);
                FormerBLO formerBLO = new FormerBLO(this._UnitOfWork, this.GAppContext);

                // TrainingYear
                string TrainingYear_Reference = trainingYearBLO.getCurrentTrainingYear().Reference;


                // Group
                Group _Group = groupBLO.GetGroup_By_GroupCode_TrainingYearReference(Ismontic_Group_Code, TrainingYear_Reference);
                if (_Group == null)
                {
                    string msg = string.Format("Ligne {0} : Le  group avec le code  '{1}' n'exist pas dans la base de données ", _index + 2, Ismontic_Group_Code);
                    importService_Ismontic.Report.AddMessage(msg, MessageTypes.Error, dataRow);
                    continue;
                }


                // Specialty
                Specialty _Specialty = _Group.Specialty;
                string Specialty_Reference = _Specialty.Reference;


                // YearStady
                string YearStady_Reference = _Group.YearStudy.Reference;

                // Module
                string Module_Reference = string.Format("{0}-{1}-{2}", Specialty_Reference, Ismontic_Module_Code, YearStady_Reference);
                if (moduleTrainingBLO.FindBaseEntityByReference(Module_Reference) == null)
                {
                    string msg = string.Format("Ligne {0} : Le module {1} n'exist pas dans la base de données ", _index + 2, Module_Reference);
                    importService_Ismontic.Report.AddMessage(msg, MessageTypes.Error, dataRow);
                    continue;
                }


                // Schedule 
                string Schedule_Reference = ScheduleReference;

                //Former
                Former _Former = formerBLO.Find_By_Full_Name(Former_Complete_Name);
 
                if (_Former == null)
                {
                    string msg = string.Format("Ligne {0} : Le formateur {1} n'exist pas dans la base de données ", _index + 2, Former_Complete_Name);
                    importService_Ismontic.Report.AddMessage(msg, MessageTypes.Error, dataRow);
                    continue;
                }

                // Trainings
                string Training_Reference = string.Format("{0}-{1}-{2}", Module_Reference, _Former.Reference, _Group.Reference);
                if (trainingBLO.FindBaseEntityByReference(Training_Reference) == null)
                {
                    string msg = string.Format("Ligne {0} : L'affectation [Module {1}, Formateur {2} {3}, Group {4}] avec le référence{5} n'exist pas dans la base de données ",
                        _index + 2,
                        Module_Reference,
                        _Former.FirstName,
                        _Former.LastName,
                        _Group.Code,
                        Training_Reference);
                    importService_Ismontic.Report.AddMessage(msg, MessageTypes.Error, dataRow);
                    continue;
                }

                // SeanceDay
                string SeanceDay_Reference = Ismontic_Seance_Day_Reference;
                if (seanceDayBLO.FindBaseEntityByReference(SeanceDay_Reference) == null)
                {
                    string msg = string.Format("Ligne {0} : Le jour {1} n'exist pas dans la base de données ", _index + 2, SeanceDay_Reference);
                    importService_Ismontic.Report.AddMessage(msg, MessageTypes.Error, dataRow);
                    continue;
                }

                // SeanceNumber
                string SeanceNumber_Reference = Ismontic_SeanceNumber_Ismontic_Reference.Substring(1, Ismontic_SeanceNumber_Ismontic_Reference.Count() - 1);
                if (seanceNumberBLO.FindBaseEntityByReference(SeanceNumber_Reference) == null)
                {
                    string msg = string.Format("Ligne {0} : Le numéro de la séance {1} n'exist pas dans la base de données ", _index + 2, SeanceNumber_Reference);
                    importService_Ismontic.Report.AddMessage(msg, MessageTypes.Error, dataRow);
                    continue;
                }

                // ClassRoom
                string ClassRoom_Reference = Ismontic_ClassRoom_Reference;
                if (classroomBLO.FindBaseEntityByReference(ClassRoom_Reference) == null)
                {
                    string msg = string.Format("Ligne {0} : Le numéro de la séance {1} n'exist pas dans la base de données ", _index + 2, ClassRoom_Reference);
                    importService_Ismontic.Report.AddMessage(msg, MessageTypes.Error, dataRow);
                    continue;
                }

                string Reference = string.Format("{0}-{1}-{2}-{3}", Training_Reference, SeanceDay_Reference , SeanceNumber_Reference,Schedule_Reference);


                // Create Training_DataRow
                DataRow SeancePlanning_DataRow = SeancePlanning_DataTable.NewRow();

                string Schedule_Column_Name = typeof(SeancePlanning).GetProperty(nameof(SeancePlanning.Schedule)).getLocalName();
                SeancePlanning_DataRow[Schedule_Column_Name] = ScheduleReference;

                string Training_Column_Name = typeof(SeancePlanning).GetProperty(nameof(SeancePlanning.Training)).getLocalName();
                SeancePlanning_DataRow[Training_Column_Name] = Training_Reference;

                string SeanceDay_Column_Name = typeof(SeancePlanning).GetProperty(nameof(SeancePlanning.SeanceDay)).getLocalName();
                SeancePlanning_DataRow[SeanceDay_Column_Name] = SeanceDay_Reference;

                string SeanceNumber_Column_Name = typeof(SeancePlanning).GetProperty(nameof(SeancePlanning.SeanceNumber)).getLocalName();
                SeancePlanning_DataRow[SeanceNumber_Column_Name] = SeanceNumber_Reference;

                string Classroom_Column_Name = typeof(SeancePlanning).GetProperty(nameof(SeancePlanning.Classroom)).getLocalName();
                SeancePlanning_DataRow[Classroom_Column_Name] = ClassRoom_Reference;

                string Reference_Column_Name = typeof(SeancePlanning).GetProperty(nameof(SeancePlanning.Reference)).getLocalName();
                SeancePlanning_DataRow[Reference_Column_Name] = Reference;

                SeancePlanning_DataTable.Rows.Add(SeancePlanning_DataRow);
            }

            List<ImportReport> importReports = new List<ImportReport>();

            ImportReport cplus_import = this.Import(SeancePlanning_DataTable, FileName);

            importReports.Add(importService_Ismontic.Report);
            importReports.Add(cplus_import);
            return importReports;
        }

        private bool Throw_Read_Exception_If_Null(string data, string ColumnName, ImportService importService_Ismontic, DataRow dataRow, int Index)
        {
            if (data == null)
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
            this.entityDAO = new SeancePlanningDAO(_UnitOfWork.context);
        }


        public IQueryable<SeanceTraining> Find_All_Planified_SeanceTraining()
        {

           

           Int64 FormerId = 4;

            var Schedule_Query = from schedule in this._UnitOfWork.context.Schedules select schedule;

            var Schedule_And_Days = from schedule in Schedule_Query.ToList()
                                          select new { schedule, Dates = this.EachDay(schedule.StartDate, schedule.EndtDate) };


            List<DateTime> All_Dates = new List<DateTime>();
            foreach (var schedule in Schedule_Query.ToList())
            {
                All_Dates.AddRange(EachDay(schedule.StartDate, schedule.EndtDate));
            }


            // Day_And_Schedule_Query

            var Date_And_Schedule_Query = from date in All_Dates
                                         from schedule_And_Days in Schedule_And_Days
                                         where schedule_And_Days.Dates.Contains(date)

                                         select new { Date = date, Schedule = schedule_And_Days.schedule };

            var Date_And_Schedule = Date_And_Schedule_Query.ToList();




            // All SeanceTraining

            var Date_Schedule_SeancePlanning_Query = from date_And_Schedule in Date_And_Schedule
                                                     from seancePlanning in this._UnitOfWork.context.SeancePlannings
                                                     join schdule in this._UnitOfWork.context.Trainings on seancePlanning.Schedule.Id equals schdule.Id
                                                     join training in this._UnitOfWork.context.Trainings on seancePlanning.Training.Id equals training.Id
                                                     join former in this._UnitOfWork.context.Formers on training.Former.Id equals former.Id
                                                     where former.Id == FormerId
                                                     where date_And_Schedule.Schedule.Id == schdule.Id
                                                     && seancePlanning.SeanceDay.Day == date_And_Schedule.Date.DayOfWeek.ToString()
                                                     select new { date_And_Schedule.Date, seancePlanning };




            var ls = Date_Schedule_SeancePlanning_Query.ToList();



            //foreach (var schedule in Schedule_Query.ToList())
            //{
            //    var Plannings_Query = 
            //    All_Days.AddRange(EachDay(schedule.StartDate, schedule.EndtDate));
            //}


            // Find All Date of one Schedule




            //var SeancePlanning = from seancePlanning in this._UnitOfWork.context.SeancePlannings
            //                     where seancePlanning.Training.Former.Id == FormerId
            //                     select seancePlanning;

            //// Find the planified Day

            //int totale = 0;
            //IQueryable<SeancePlanning> SeancePlanning_Query =   this.entityDAO.Find_WithOut_Pagination(filterRequestParams, searchCreteria, totale);


            //IQueryable <SeanceTraining> SeanceTraining_Query = from seancePlanning in SeancePlanning_Query
            //                                                   where seancePlanning.day


            return null ;
        }

        private IEnumerable<DateTime> EachDay(DateTime StartDate, DateTime EndDate)
        {
            for (var day = StartDate.Date; day.Date <= EndDate.Date; day = day.AddDays(1))
                yield return day;
        }

    }
}
