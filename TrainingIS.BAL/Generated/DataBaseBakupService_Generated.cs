using System.Data;
using TrainingIS.Entities.Resources.TrainingResources;
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.Entities.Resources.SeanceTrainingResources;
using TrainingIS.Entities.Resources.RoleAppResources;
using TrainingIS.Entities.Resources.EntityPropertyShortcutResources;
using TrainingIS.Entities.Resources.SeanceNumberResources;
using TrainingIS.Entities.Resources.ModuleTrainingResources;
using TrainingIS.Entities.Resources.ClassroomCategoryResources;
using TrainingIS.Entities.Resources.LogWorkResources;
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Entities.Resources.AuthrorizationAppResources;
using TrainingIS.Entities.Resources.SeancePlanningResources;
using TrainingIS.Entities.Resources.SeanceDayResources;
using TrainingIS.Entities.Resources.SchoollevelResources;
using TrainingIS.Entities.Resources.TrainingYearResources;
using TrainingIS.Entities.Resources.StateOfAbseceResources;
using TrainingIS.Entities.Resources.ApplicationParamResources;
using TrainingIS.Entities.Resources.NationalityResources;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.ClassroomResources;
using TrainingIS.Entities.Resources.YearStudyResources;
using TrainingIS.Entities.Resources.TrainingTypeResources;
using TrainingIS.Entities.Resources.ControllerAppResources;
using TrainingIS.Entities.Resources.SpecialtyResources;
using TrainingIS.Entities.Resources.ActionControllerAppResources;
using TrainingIS.Entities.Resources.AbsenceResources;
using TrainingIS.Entities.Resources.ScheduleResources;

namespace TrainingIS.BLL.Services
{
    public partial class DataBaseBakupService 
    {
        public void AddDataTablesToDataSet(DataSet dataSet)
        {
            dataSet.Tables.Add(new TrainingBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new TraineeBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new SeanceTrainingBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new RoleAppBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new EntityPropertyShortcutBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new SeanceNumberBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new ModuleTrainingBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new ClassroomCategoryBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new LogWorkBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new FormerBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new AuthrorizationAppBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new SeancePlanningBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new SeanceDayBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new SchoollevelBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new TrainingYearBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new StateOfAbseceBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new ApplicationParamBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new NationalityBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new GroupBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new ClassroomBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new YearStudyBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new TrainingTypeBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new ControllerAppBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new SpecialtyBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new ActionControllerAppBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new AbsenceBLO(this._UnitOfWork).Export());
            dataSet.Tables.Add(new ScheduleBLO(this._UnitOfWork).Export());
        }

		public string Import(DataSet dataSet)
        {
            string msg = "";
            foreach (DataTable table in dataSet.Tables)
            {
				if (table.TableName == msg_Training.PluralName) {
                    msg += new TrainingBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_Trainee.PluralName) {
                    msg += new TraineeBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_SeanceTraining.PluralName) {
                    msg += new SeanceTrainingBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_RoleApp.PluralName) {
                    msg += new RoleAppBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_EntityPropertyShortcut.PluralName) {
                    msg += new EntityPropertyShortcutBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_SeanceNumber.PluralName) {
                    msg += new SeanceNumberBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_ModuleTraining.PluralName) {
                    msg += new ModuleTrainingBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_ClassroomCategory.PluralName) {
                    msg += new ClassroomCategoryBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_LogWork.PluralName) {
                    msg += new LogWorkBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_Former.PluralName) {
                    msg += new FormerBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_AuthrorizationApp.PluralName) {
                    msg += new AuthrorizationAppBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_SeancePlanning.PluralName) {
                    msg += new SeancePlanningBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_SeanceDay.PluralName) {
                    msg += new SeanceDayBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_Schoollevel.PluralName) {
                    msg += new SchoollevelBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_TrainingYear.PluralName) {
                    msg += new TrainingYearBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_StateOfAbsece.PluralName) {
                    msg += new StateOfAbseceBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_ApplicationParam.PluralName) {
                    msg += new ApplicationParamBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_Nationality.PluralName) {
                    msg += new NationalityBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_Group.PluralName) {
                    msg += new GroupBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_Classroom.PluralName) {
                    msg += new ClassroomBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_YearStudy.PluralName) {
                    msg += new YearStudyBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_TrainingType.PluralName) {
                    msg += new TrainingTypeBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_ControllerApp.PluralName) {
                    msg += new ControllerAppBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_Specialty.PluralName) {
                    msg += new SpecialtyBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_ActionControllerApp.PluralName) {
                    msg += new ActionControllerAppBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_Absence.PluralName) {
                    msg += new AbsenceBLO(this._UnitOfWork).Import(table);
				}
				if (table.TableName == msg_Schedule.PluralName) {
                    msg += new ScheduleBLO(this._UnitOfWork).Import(table);
				}
            }
            return msg;
        }
    }
}
