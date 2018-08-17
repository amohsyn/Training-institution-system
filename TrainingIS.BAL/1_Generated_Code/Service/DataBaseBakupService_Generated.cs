using System.Data;
using GApp.Entities.Resources.ApplicationParamResources;
using GApp.Entities.Resources.LogWorkResources;
using GApp.Entities.Resources.RoleAppResources;
using GApp.Entities.Resources.ControllerAppResources;
using GApp.Entities.Resources.ActionControllerAppResources;
using GApp.Entities.Resources.AuthrorizationAppResources;
using GApp.Entities.Resources.EntityPropertyShortcutResources;
using TrainingIS.Entities.Resources.ClassroomCategoryResources;
using TrainingIS.Entities.Resources.NationalityResources;
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
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.Entities.Resources.TrainingResources;
using TrainingIS.Entities.Resources.SeancePlanningResources;
using TrainingIS.Entities.Resources.SeanceTrainingResources;
using TrainingIS.Entities.Resources.AbsenceResources;
using TrainingIS.Entities.Resources.StateOfAbseceResources;

namespace TrainingIS.BLL.Services
{
    public partial class DataBaseBakupService 
    {
        public void AddDataTablesToDataSet(DataSet dataSet)
        {
            dataSet.Tables.Add(new ApplicationParamBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new LogWorkBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new RoleAppBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new ControllerAppBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new ActionControllerAppBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new AuthrorizationAppBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new EntityPropertyShortcutBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new ClassroomCategoryBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new NationalityBLO(this.UnitOfWork, this.GAppContext).Export());
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
            dataSet.Tables.Add(new TraineeBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new TrainingBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new SeancePlanningBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new AbsenceBLO(this.UnitOfWork, this.GAppContext).Export());
            dataSet.Tables.Add(new StateOfAbseceBLO(this.UnitOfWork, this.GAppContext).Export());
        }

		public string Import(DataSet dataSet)
        {
            string msg = "";
            foreach (DataTable table in dataSet.Tables)
            {
				if (table.TableName == msg_ApplicationParam.PluralName) {
                    msg += new ApplicationParamBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_LogWork.PluralName) {
                    msg += new LogWorkBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_RoleApp.PluralName) {
                    msg += new RoleAppBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_ControllerApp.PluralName) {
                    msg += new ControllerAppBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_ActionControllerApp.PluralName) {
                    msg += new ActionControllerAppBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_AuthrorizationApp.PluralName) {
                    msg += new AuthrorizationAppBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_EntityPropertyShortcut.PluralName) {
                    msg += new EntityPropertyShortcutBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_ClassroomCategory.PluralName) {
                    msg += new ClassroomCategoryBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_Nationality.PluralName) {
                    msg += new NationalityBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_TrainingYear.PluralName) {
                    msg += new TrainingYearBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_TrainingType.PluralName) {
                    msg += new TrainingTypeBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_SeanceDay.PluralName) {
                    msg += new SeanceDayBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_SeanceNumber.PluralName) {
                    msg += new SeanceNumberBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_YearStudy.PluralName) {
                    msg += new YearStudyBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_Specialty.PluralName) {
                    msg += new SpecialtyBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_Schoollevel.PluralName) {
                    msg += new SchoollevelBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_FormerSpecialty.PluralName) {
                    msg += new FormerSpecialtyBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_TrainingLevel.PluralName) {
                    msg += new TrainingLevelBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_Metier.PluralName) {
                    msg += new MetierBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_Sector.PluralName) {
                    msg += new SectorBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_Classroom.PluralName) {
                    msg += new ClassroomBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_ModuleTraining.PluralName) {
                    msg += new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_Former.PluralName) {
                    msg += new FormerBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_Group.PluralName) {
                    msg += new GroupBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_Schedule.PluralName) {
                    msg += new ScheduleBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_Trainee.PluralName) {
                    msg += new TraineeBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_Training.PluralName) {
                    msg += new TrainingBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_SeancePlanning.PluralName) {
                    msg += new SeancePlanningBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_SeanceTraining.PluralName) {
                    msg += new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_Absence.PluralName) {
                    msg += new AbsenceBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
				if (table.TableName == msg_StateOfAbsece.PluralName) {
                    msg += new StateOfAbseceBLO(this.UnitOfWork, this.GAppContext).Import(table);
				}
            }
            return msg;
        }
    }
}
