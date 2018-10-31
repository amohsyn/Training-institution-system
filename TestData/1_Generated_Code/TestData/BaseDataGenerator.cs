using GApp.Core.Context;
using GApp.DAL;
using GApp.UnitTest.TestData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestData;
using TrainingIS.DAL;
using TrainingIS.Entities;
using System.Collections.Generic;

namespace TestDataGenerator.TestData
{
    public class BaseDataGenerator
    {
        UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        GAppContext GAppContext { set; get; }

        public BaseDataGenerator()
        {
            UnitOfWork = new UnitOfWork<TrainingISModel>();
            GAppContext = new GAppContext("Root");
        }
        public virtual void Insert_Or_Update_Test_Data()
        {
        			
			CalendarDayTestDataFactory CalendarDay_TestData = new CalendarDayTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			CalendarDay_TestData.Insert_Or_Update_Test_Data();

        			
			ApplicationParamTestDataFactory ApplicationParam_TestData = new ApplicationParamTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			ApplicationParam_TestData.Insert_Or_Update_Test_Data();

        			
			LogWorkTestDataFactory LogWork_TestData = new LogWorkTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			LogWork_TestData.Insert_Or_Update_Test_Data();

        			
			RoleAppTestDataFactory RoleApp_TestData = new RoleAppTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			RoleApp_TestData.Insert_Or_Update_Test_Data();

        			
			ControllerAppTestDataFactory ControllerApp_TestData = new ControllerAppTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			ControllerApp_TestData.Insert_Or_Update_Test_Data();

        			
			ActionControllerAppTestDataFactory ActionControllerApp_TestData = new ActionControllerAppTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			ActionControllerApp_TestData.Insert_Or_Update_Test_Data();

        			
			AuthrorizationAppTestDataFactory AuthrorizationApp_TestData = new AuthrorizationAppTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			AuthrorizationApp_TestData.Insert_Or_Update_Test_Data();

        			
			EntityPropertyShortcutTestDataFactory EntityPropertyShortcut_TestData = new EntityPropertyShortcutTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			EntityPropertyShortcut_TestData.Insert_Or_Update_Test_Data();

        			
			ClassroomCategoryTestDataFactory ClassroomCategory_TestData = new ClassroomCategoryTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			ClassroomCategory_TestData.Insert_Or_Update_Test_Data();

        			
			NationalityTestDataFactory Nationality_TestData = new NationalityTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Nationality_TestData.Insert_Or_Update_Test_Data();

        			
			GPictureTestDataFactory GPicture_TestData = new GPictureTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			GPicture_TestData.Insert_Or_Update_Test_Data();

        			
			ProjectTestDataFactory Project_TestData = new ProjectTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Project_TestData.Insert_Or_Update_Test_Data();

        			
			Category_JustificationAbsenceTestDataFactory Category_JustificationAbsence_TestData = new Category_JustificationAbsenceTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Category_JustificationAbsence_TestData.Insert_Or_Update_Test_Data();

        			
			Category_WarningTraineeTestDataFactory Category_WarningTrainee_TestData = new Category_WarningTraineeTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Category_WarningTrainee_TestData.Insert_Or_Update_Test_Data();

        			
			TrainingYearTestDataFactory TrainingYear_TestData = new TrainingYearTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			TrainingYear_TestData.Insert_Or_Update_Test_Data();

        			
			TrainingTypeTestDataFactory TrainingType_TestData = new TrainingTypeTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			TrainingType_TestData.Insert_Or_Update_Test_Data();

        			
			SeanceDayTestDataFactory SeanceDay_TestData = new SeanceDayTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			SeanceDay_TestData.Insert_Or_Update_Test_Data();

        			
			SeanceNumberTestDataFactory SeanceNumber_TestData = new SeanceNumberTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			SeanceNumber_TestData.Insert_Or_Update_Test_Data();

        			
			YearStudyTestDataFactory YearStudy_TestData = new YearStudyTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			YearStudy_TestData.Insert_Or_Update_Test_Data();

        			
			SpecialtyTestDataFactory Specialty_TestData = new SpecialtyTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Specialty_TestData.Insert_Or_Update_Test_Data();

        			
			SchoollevelTestDataFactory Schoollevel_TestData = new SchoollevelTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Schoollevel_TestData.Insert_Or_Update_Test_Data();

        			
			FormerSpecialtyTestDataFactory FormerSpecialty_TestData = new FormerSpecialtyTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			FormerSpecialty_TestData.Insert_Or_Update_Test_Data();

        			
			TrainingLevelTestDataFactory TrainingLevel_TestData = new TrainingLevelTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			TrainingLevel_TestData.Insert_Or_Update_Test_Data();

        			
			MetierTestDataFactory Metier_TestData = new MetierTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Metier_TestData.Insert_Or_Update_Test_Data();

        			
			SectorTestDataFactory Sector_TestData = new SectorTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Sector_TestData.Insert_Or_Update_Test_Data();

        			
			FunctionTestDataFactory Function_TestData = new FunctionTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Function_TestData.Insert_Or_Update_Test_Data();

        			
			Mission_Working_GroupTestDataFactory Mission_Working_Group_TestData = new Mission_Working_GroupTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Mission_Working_Group_TestData.Insert_Or_Update_Test_Data();

        			
			DisciplineCategoryTestDataFactory DisciplineCategory_TestData = new DisciplineCategoryTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			DisciplineCategory_TestData.Insert_Or_Update_Test_Data();

        			
			ClassroomTestDataFactory Classroom_TestData = new ClassroomTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Classroom_TestData.Insert_Or_Update_Test_Data();

        			
			ModuleTrainingTestDataFactory ModuleTraining_TestData = new ModuleTrainingTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			ModuleTraining_TestData.Insert_Or_Update_Test_Data();

        			
			FormerTestDataFactory Former_TestData = new FormerTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Former_TestData.Insert_Or_Update_Test_Data();

        			
			AdministratorTestDataFactory Administrator_TestData = new AdministratorTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Administrator_TestData.Insert_Or_Update_Test_Data();

        			
			GroupTestDataFactory Group_TestData = new GroupTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Group_TestData.Insert_Or_Update_Test_Data();

        			
			ScheduleTestDataFactory Schedule_TestData = new ScheduleTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Schedule_TestData.Insert_Or_Update_Test_Data();

        			
			TaskProjectTestDataFactory TaskProject_TestData = new TaskProjectTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			TaskProject_TestData.Insert_Or_Update_Test_Data();

        			
			JustificationAbsenceTestDataFactory JustificationAbsence_TestData = new JustificationAbsenceTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			JustificationAbsence_TestData.Insert_Or_Update_Test_Data();

        			
			WarningTraineeTestDataFactory WarningTrainee_TestData = new WarningTraineeTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			WarningTrainee_TestData.Insert_Or_Update_Test_Data();

        			
			SanctionCategoryTestDataFactory SanctionCategory_TestData = new SanctionCategoryTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			SanctionCategory_TestData.Insert_Or_Update_Test_Data();

        			
			TraineeTestDataFactory Trainee_TestData = new TraineeTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Trainee_TestData.Insert_Or_Update_Test_Data();

        			
			TrainingTestDataFactory Training_TestData = new TrainingTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Training_TestData.Insert_Or_Update_Test_Data();

        			
			WorkGroupTestDataFactory WorkGroup_TestData = new WorkGroupTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			WorkGroup_TestData.Insert_Or_Update_Test_Data();

        			
			SeancePlanningTestDataFactory SeancePlanning_TestData = new SeancePlanningTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			SeancePlanning_TestData.Insert_Or_Update_Test_Data();

        			
			MeetingTestDataFactory Meeting_TestData = new MeetingTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Meeting_TestData.Insert_Or_Update_Test_Data();

        			
			SeanceTrainingTestDataFactory SeanceTraining_TestData = new SeanceTrainingTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			SeanceTraining_TestData.Insert_Or_Update_Test_Data();

        			
			AbsenceTestDataFactory Absence_TestData = new AbsenceTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Absence_TestData.Insert_Or_Update_Test_Data();

        			
			StateOfAbseceTestDataFactory StateOfAbsece_TestData = new StateOfAbseceTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			StateOfAbsece_TestData.Insert_Or_Update_Test_Data();

        			
			SanctionTestDataFactory Sanction_TestData = new SanctionTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Sanction_TestData.Insert_Or_Update_Test_Data();

        
        }
    }
}
