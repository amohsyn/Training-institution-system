using System.Data;
using TrainingIS.Entities.Resources.CalendarDayResources;
using GApp.Entities.Resources.ApplicationParamResources;
using GApp.Entities.Resources.LogWorkResources;
using GApp.Entities.Resources.RoleAppResources;
using GApp.Entities.Resources.ControllerAppResources;
using GApp.Entities.Resources.ActionControllerAppResources;
using GApp.Entities.Resources.AuthrorizationAppResources;
using GApp.Entities.Resources.EntityPropertyShortcutResources;
using TrainingIS.Entities.Resources.ClassroomCategoryResources;
using TrainingIS.Entities.Resources.NationalityResources;
using TrainingIS.Entities.Resources.ProjectResources;
using TrainingIS.Entities.Resources.Category_JustificationAbsenceResources;
using TrainingIS.Entities.Resources.Category_WarningTraineeResources;
using TrainingIS.Entities.Resources.TrainingYearResources;
using TrainingIS.Entities.Resources.TrainingTypeResources;
using TrainingIS.Entities.Resources.SeanceDayResources;
using TrainingIS.Entities.Resources.SeanceNumberResources;
using TrainingIS.Entities.Resources.YearStudyResources;
using TrainingIS.Entities.Resources.SpecialtyResources;
using TrainingIS.Entities.Resources.SchoollevelResources;
using TrainingIS.Entities.Resources.FormerSpecialtyResources;
using TrainingIS.Entities.Resources.TrainingLevelResources;
using TrainingIS.Entities.Resources.MetierResources;
using TrainingIS.Entities.Resources.SectorResources;
using TrainingIS.Entities.Resources.ClassroomResources;
using TrainingIS.Entities.Resources.ModuleTrainingResources;
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.ScheduleResources;
using TrainingIS.Entities.Resources.TaskProjectResources;
using TrainingIS.Entities.Resources.JustificationAbsenceResources;
using TrainingIS.Entities.Resources.WarningTraineeResources;
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.Entities.Resources.TrainingResources;
using TrainingIS.Entities.Resources.SeancePlanningResources;
using TrainingIS.Entities.Resources.SeanceTrainingResources;
using TrainingIS.Entities.Resources.AbsenceResources;
using TrainingIS.Entities.Resources.StateOfAbseceResources;
using System.Collections.Generic;

namespace TrainingIS.BLL.Services
{
    public partial class DataBaseBakupService 
    {
        public void AddDataTablesToDataSet(DataSet dataSet)
        {
            dataSet.Tables.Add(new CalendarDayBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new ApplicationParamBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new LogWorkBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new RoleAppBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new ControllerAppBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new ActionControllerAppBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new AuthrorizationAppBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new EntityPropertyShortcutBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new ClassroomCategoryBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new NationalityBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new ProjectBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new Category_JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new Category_WarningTraineeBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new TrainingYearBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new TrainingTypeBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new SeanceDayBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new SeanceNumberBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new YearStudyBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new SpecialtyBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new SchoollevelBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new FormerSpecialtyBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new TrainingLevelBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new MetierBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new SectorBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new ClassroomBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new FormerBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new GroupBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new ScheduleBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new TaskProjectBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new WarningTraineeBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new TraineeBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new TrainingBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new SeancePlanningBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new AbsenceBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new StateOfAbseceBLO(this.UnitOfWork, this.GAppContext).Export());
        }

		public List<ImportReport> Import(DataSet dataSet)
        {
            List<ImportReport> importReports = new List<ImportReport>();
            foreach (DataTable table in dataSet.Tables)
            {
				if (table.TableName == msg_CalendarDay.PluralName) {
                    importReports.Add(new CalendarDayBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_ApplicationParam.PluralName) {
                    importReports.Add(new ApplicationParamBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_LogWork.PluralName) {
                    importReports.Add(new LogWorkBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_RoleApp.PluralName) {
                    importReports.Add(new RoleAppBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_ControllerApp.PluralName) {
                    importReports.Add(new ControllerAppBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_ActionControllerApp.PluralName) {
                    importReports.Add(new ActionControllerAppBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_AuthrorizationApp.PluralName) {
                    importReports.Add(new AuthrorizationAppBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_EntityPropertyShortcut.PluralName) {
                    importReports.Add(new EntityPropertyShortcutBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_ClassroomCategory.PluralName) {
                    importReports.Add(new ClassroomCategoryBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_Nationality.PluralName) {
                    importReports.Add(new NationalityBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_Project.PluralName) {
                    importReports.Add(new ProjectBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_Category_JustificationAbsence.PluralName) {
                    importReports.Add(new Category_JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_Category_WarningTrainee.PluralName) {
                    importReports.Add(new Category_WarningTraineeBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_TrainingYear.PluralName) {
                    importReports.Add(new TrainingYearBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_TrainingType.PluralName) {
                    importReports.Add(new TrainingTypeBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_SeanceDay.PluralName) {
                    importReports.Add(new SeanceDayBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_SeanceNumber.PluralName) {
                    importReports.Add(new SeanceNumberBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_YearStudy.PluralName) {
                    importReports.Add(new YearStudyBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_Specialty.PluralName) {
                    importReports.Add(new SpecialtyBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_Schoollevel.PluralName) {
                    importReports.Add(new SchoollevelBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_FormerSpecialty.PluralName) {
                    importReports.Add(new FormerSpecialtyBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_TrainingLevel.PluralName) {
                    importReports.Add(new TrainingLevelBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_Metier.PluralName) {
                    importReports.Add(new MetierBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_Sector.PluralName) {
                    importReports.Add(new SectorBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_Classroom.PluralName) {
                    importReports.Add(new ClassroomBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_ModuleTraining.PluralName) {
                    importReports.Add(new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_Former.PluralName) {
                    importReports.Add(new FormerBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_Group.PluralName) {
                    importReports.Add(new GroupBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_Schedule.PluralName) {
                    importReports.Add(new ScheduleBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_TaskProject.PluralName) {
                    importReports.Add(new TaskProjectBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_JustificationAbsence.PluralName) {
                    importReports.Add(new JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_WarningTrainee.PluralName) {
                    importReports.Add(new WarningTraineeBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_Trainee.PluralName) {
                    importReports.Add(new TraineeBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_Training.PluralName) {
                    importReports.Add(new TrainingBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_SeancePlanning.PluralName) {
                    importReports.Add(new SeancePlanningBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_SeanceTraining.PluralName) {
                    importReports.Add(new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_Absence.PluralName) {
                    importReports.Add(new AbsenceBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
				if (table.TableName == msg_StateOfAbsece.PluralName) {
                    importReports.Add(new StateOfAbseceBLO(this.UnitOfWork, this.GAppContext).Import(table));
				}
            }
            return importReports;
        }
    }
}
