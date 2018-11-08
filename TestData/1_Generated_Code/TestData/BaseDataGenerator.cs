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
using GApp.Entities;
using System.IO;
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
        public virtual void Insert_Test_Data()
        {
        			
			CalendarDayTestDataFactory CalendarDay_TestData = new CalendarDayTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			CalendarDay_TestData.Insert_Test_Data();

        			
			ApplicationParamTestDataFactory ApplicationParam_TestData = new ApplicationParamTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			ApplicationParam_TestData.Insert_Test_Data();

        			
			LogWorkTestDataFactory LogWork_TestData = new LogWorkTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			LogWork_TestData.Insert_Test_Data();

        			
			RoleAppTestDataFactory RoleApp_TestData = new RoleAppTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			RoleApp_TestData.Insert_Test_Data();

        			
			ControllerAppTestDataFactory ControllerApp_TestData = new ControllerAppTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			ControllerApp_TestData.Insert_Test_Data();

        			
			ActionControllerAppTestDataFactory ActionControllerApp_TestData = new ActionControllerAppTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			ActionControllerApp_TestData.Insert_Test_Data();

        			
			AuthrorizationAppTestDataFactory AuthrorizationApp_TestData = new AuthrorizationAppTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			AuthrorizationApp_TestData.Insert_Test_Data();

        			
			EntityPropertyShortcutTestDataFactory EntityPropertyShortcut_TestData = new EntityPropertyShortcutTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			EntityPropertyShortcut_TestData.Insert_Test_Data();

        			
			ClassroomCategoryTestDataFactory ClassroomCategory_TestData = new ClassroomCategoryTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			ClassroomCategory_TestData.Insert_Test_Data();

        			
			NationalityTestDataFactory Nationality_TestData = new NationalityTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Nationality_TestData.Insert_Test_Data();

        			
			GPictureTestDataFactory GPicture_TestData = new GPictureTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			GPicture_TestData.Insert_Test_Data();

        			
			ProjectTestDataFactory Project_TestData = new ProjectTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Project_TestData.Insert_Test_Data();

        			
			Category_JustificationAbsenceTestDataFactory Category_JustificationAbsence_TestData = new Category_JustificationAbsenceTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Category_JustificationAbsence_TestData.Insert_Test_Data();

        			
			Category_WarningTraineeTestDataFactory Category_WarningTrainee_TestData = new Category_WarningTraineeTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Category_WarningTrainee_TestData.Insert_Test_Data();

        			
			TrainingYearTestDataFactory TrainingYear_TestData = new TrainingYearTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			TrainingYear_TestData.Insert_Test_Data();

        			
			TrainingTypeTestDataFactory TrainingType_TestData = new TrainingTypeTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			TrainingType_TestData.Insert_Test_Data();

        			
			SeanceDayTestDataFactory SeanceDay_TestData = new SeanceDayTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			SeanceDay_TestData.Insert_Test_Data();

        			
			SeanceNumberTestDataFactory SeanceNumber_TestData = new SeanceNumberTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			SeanceNumber_TestData.Insert_Test_Data();

        			
			YearStudyTestDataFactory YearStudy_TestData = new YearStudyTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			YearStudy_TestData.Insert_Test_Data();

        			
			SchoollevelTestDataFactory Schoollevel_TestData = new SchoollevelTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Schoollevel_TestData.Insert_Test_Data();

        			
			FormerSpecialtyTestDataFactory FormerSpecialty_TestData = new FormerSpecialtyTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			FormerSpecialty_TestData.Insert_Test_Data();

        			
			TrainingLevelTestDataFactory TrainingLevel_TestData = new TrainingLevelTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			TrainingLevel_TestData.Insert_Test_Data();

        			
			MetierTestDataFactory Metier_TestData = new MetierTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Metier_TestData.Insert_Test_Data();

        			
			SectorTestDataFactory Sector_TestData = new SectorTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Sector_TestData.Insert_Test_Data();

        			
			FunctionTestDataFactory Function_TestData = new FunctionTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Function_TestData.Insert_Test_Data();

        			
			Mission_Working_GroupTestDataFactory Mission_Working_Group_TestData = new Mission_Working_GroupTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Mission_Working_Group_TestData.Insert_Test_Data();

        			
			DisciplineCategoryTestDataFactory DisciplineCategory_TestData = new DisciplineCategoryTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			DisciplineCategory_TestData.Insert_Test_Data();

        			
			ClassroomTestDataFactory Classroom_TestData = new ClassroomTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Classroom_TestData.Insert_Test_Data();

        			
			SpecialtyTestDataFactory Specialty_TestData = new SpecialtyTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Specialty_TestData.Insert_Test_Data();

        			
			ModuleTrainingTestDataFactory ModuleTraining_TestData = new ModuleTrainingTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			ModuleTraining_TestData.Insert_Test_Data();

        			
			FormerTestDataFactory Former_TestData = new FormerTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Former_TestData.Insert_Test_Data();

        			
			AdministratorTestDataFactory Administrator_TestData = new AdministratorTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Administrator_TestData.Insert_Test_Data();

        			
			GroupTestDataFactory Group_TestData = new GroupTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Group_TestData.Insert_Test_Data();

        			
			ScheduleTestDataFactory Schedule_TestData = new ScheduleTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Schedule_TestData.Insert_Test_Data();

        			
			TaskProjectTestDataFactory TaskProject_TestData = new TaskProjectTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			TaskProject_TestData.Insert_Test_Data();

        			
			JustificationAbsenceTestDataFactory JustificationAbsence_TestData = new JustificationAbsenceTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			JustificationAbsence_TestData.Insert_Test_Data();

        			
			WarningTraineeTestDataFactory WarningTrainee_TestData = new WarningTraineeTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			WarningTrainee_TestData.Insert_Test_Data();

        			
			SanctionCategoryTestDataFactory SanctionCategory_TestData = new SanctionCategoryTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			SanctionCategory_TestData.Insert_Test_Data();

        			
			TraineeTestDataFactory Trainee_TestData = new TraineeTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Trainee_TestData.Insert_Test_Data();

        			
			TrainingTestDataFactory Training_TestData = new TrainingTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Training_TestData.Insert_Test_Data();

        			
			WorkGroupTestDataFactory WorkGroup_TestData = new WorkGroupTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			WorkGroup_TestData.Insert_Test_Data();

        			
			AttendanceStateTestDataFactory AttendanceState_TestData = new AttendanceStateTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			AttendanceState_TestData.Insert_Test_Data();

        			
			SeancePlanningTestDataFactory SeancePlanning_TestData = new SeancePlanningTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			SeancePlanning_TestData.Insert_Test_Data();

        			
			MeetingTestDataFactory Meeting_TestData = new MeetingTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Meeting_TestData.Insert_Test_Data();

        			
			SeanceTrainingTestDataFactory SeanceTraining_TestData = new SeanceTrainingTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			SeanceTraining_TestData.Insert_Test_Data();

        			
			AbsenceTestDataFactory Absence_TestData = new AbsenceTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Absence_TestData.Insert_Test_Data();

        			
			StateOfAbseceTestDataFactory StateOfAbsece_TestData = new StateOfAbseceTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			StateOfAbsece_TestData.Insert_Test_Data();

        			
			SanctionTestDataFactory Sanction_TestData = new SanctionTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Sanction_TestData.Insert_Test_Data();

        
        }

		public virtual void Update_Test_Data()
        {
        			
			CalendarDayTestDataFactory CalendarDay_TestData = new CalendarDayTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			CalendarDay_TestData.Update_Test_Data();

        			
			ApplicationParamTestDataFactory ApplicationParam_TestData = new ApplicationParamTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			ApplicationParam_TestData.Update_Test_Data();

        			
			LogWorkTestDataFactory LogWork_TestData = new LogWorkTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			LogWork_TestData.Update_Test_Data();

        			
			RoleAppTestDataFactory RoleApp_TestData = new RoleAppTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			RoleApp_TestData.Update_Test_Data();

        			
			ControllerAppTestDataFactory ControllerApp_TestData = new ControllerAppTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			ControllerApp_TestData.Update_Test_Data();

        			
			ActionControllerAppTestDataFactory ActionControllerApp_TestData = new ActionControllerAppTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			ActionControllerApp_TestData.Update_Test_Data();

        			
			AuthrorizationAppTestDataFactory AuthrorizationApp_TestData = new AuthrorizationAppTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			AuthrorizationApp_TestData.Update_Test_Data();

        			
			EntityPropertyShortcutTestDataFactory EntityPropertyShortcut_TestData = new EntityPropertyShortcutTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			EntityPropertyShortcut_TestData.Update_Test_Data();

        			
			ClassroomCategoryTestDataFactory ClassroomCategory_TestData = new ClassroomCategoryTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			ClassroomCategory_TestData.Update_Test_Data();

        			
			NationalityTestDataFactory Nationality_TestData = new NationalityTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Nationality_TestData.Update_Test_Data();

        			
			GPictureTestDataFactory GPicture_TestData = new GPictureTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			GPicture_TestData.Update_Test_Data();

        			
			ProjectTestDataFactory Project_TestData = new ProjectTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Project_TestData.Update_Test_Data();

        			
			Category_JustificationAbsenceTestDataFactory Category_JustificationAbsence_TestData = new Category_JustificationAbsenceTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Category_JustificationAbsence_TestData.Update_Test_Data();

        			
			Category_WarningTraineeTestDataFactory Category_WarningTrainee_TestData = new Category_WarningTraineeTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Category_WarningTrainee_TestData.Update_Test_Data();

        			
			TrainingYearTestDataFactory TrainingYear_TestData = new TrainingYearTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			TrainingYear_TestData.Update_Test_Data();

        			
			TrainingTypeTestDataFactory TrainingType_TestData = new TrainingTypeTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			TrainingType_TestData.Update_Test_Data();

        			
			SeanceDayTestDataFactory SeanceDay_TestData = new SeanceDayTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			SeanceDay_TestData.Update_Test_Data();

        			
			SeanceNumberTestDataFactory SeanceNumber_TestData = new SeanceNumberTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			SeanceNumber_TestData.Update_Test_Data();

        			
			YearStudyTestDataFactory YearStudy_TestData = new YearStudyTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			YearStudy_TestData.Update_Test_Data();

        			
			SchoollevelTestDataFactory Schoollevel_TestData = new SchoollevelTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Schoollevel_TestData.Update_Test_Data();

        			
			FormerSpecialtyTestDataFactory FormerSpecialty_TestData = new FormerSpecialtyTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			FormerSpecialty_TestData.Update_Test_Data();

        			
			TrainingLevelTestDataFactory TrainingLevel_TestData = new TrainingLevelTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			TrainingLevel_TestData.Update_Test_Data();

        			
			MetierTestDataFactory Metier_TestData = new MetierTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Metier_TestData.Update_Test_Data();

        			
			SectorTestDataFactory Sector_TestData = new SectorTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Sector_TestData.Update_Test_Data();

        			
			FunctionTestDataFactory Function_TestData = new FunctionTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Function_TestData.Update_Test_Data();

        			
			Mission_Working_GroupTestDataFactory Mission_Working_Group_TestData = new Mission_Working_GroupTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Mission_Working_Group_TestData.Update_Test_Data();

        			
			DisciplineCategoryTestDataFactory DisciplineCategory_TestData = new DisciplineCategoryTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			DisciplineCategory_TestData.Update_Test_Data();

        			
			ClassroomTestDataFactory Classroom_TestData = new ClassroomTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Classroom_TestData.Update_Test_Data();

        			
			SpecialtyTestDataFactory Specialty_TestData = new SpecialtyTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Specialty_TestData.Update_Test_Data();

        			
			ModuleTrainingTestDataFactory ModuleTraining_TestData = new ModuleTrainingTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			ModuleTraining_TestData.Update_Test_Data();

        			
			FormerTestDataFactory Former_TestData = new FormerTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Former_TestData.Update_Test_Data();

        			
			AdministratorTestDataFactory Administrator_TestData = new AdministratorTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Administrator_TestData.Update_Test_Data();

        			
			GroupTestDataFactory Group_TestData = new GroupTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Group_TestData.Update_Test_Data();

        			
			ScheduleTestDataFactory Schedule_TestData = new ScheduleTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Schedule_TestData.Update_Test_Data();

        			
			TaskProjectTestDataFactory TaskProject_TestData = new TaskProjectTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			TaskProject_TestData.Update_Test_Data();

        			
			JustificationAbsenceTestDataFactory JustificationAbsence_TestData = new JustificationAbsenceTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			JustificationAbsence_TestData.Update_Test_Data();

        			
			WarningTraineeTestDataFactory WarningTrainee_TestData = new WarningTraineeTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			WarningTrainee_TestData.Update_Test_Data();

        			
			SanctionCategoryTestDataFactory SanctionCategory_TestData = new SanctionCategoryTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			SanctionCategory_TestData.Update_Test_Data();

        			
			TraineeTestDataFactory Trainee_TestData = new TraineeTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Trainee_TestData.Update_Test_Data();

        			
			TrainingTestDataFactory Training_TestData = new TrainingTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Training_TestData.Update_Test_Data();

        			
			WorkGroupTestDataFactory WorkGroup_TestData = new WorkGroupTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			WorkGroup_TestData.Update_Test_Data();

        			
			AttendanceStateTestDataFactory AttendanceState_TestData = new AttendanceStateTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			AttendanceState_TestData.Update_Test_Data();

        			
			SeancePlanningTestDataFactory SeancePlanning_TestData = new SeancePlanningTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			SeancePlanning_TestData.Update_Test_Data();

        			
			MeetingTestDataFactory Meeting_TestData = new MeetingTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Meeting_TestData.Update_Test_Data();

        			
			SeanceTrainingTestDataFactory SeanceTraining_TestData = new SeanceTrainingTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			SeanceTraining_TestData.Update_Test_Data();

        			
			AbsenceTestDataFactory Absence_TestData = new AbsenceTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Absence_TestData.Update_Test_Data();

        			
			StateOfAbseceTestDataFactory StateOfAbsece_TestData = new StateOfAbseceTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			StateOfAbsece_TestData.Update_Test_Data();

        			
			SanctionTestDataFactory Sanction_TestData = new SanctionTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
			Sanction_TestData.Update_Test_Data();

        
        }

		public virtual Dictionary<Type, string> Get_TestData_Files()
        {
            Dictionary<Type, string> Data = new Dictionary<Type, string>();
	
			CalendarDayTestDataFactory CalendarDay_TestData = new CalendarDayTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(CalendarDay_TestData.Get_Data_File_Name())){
                Data.Add(typeof(CalendarDay), CalendarDay_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(CalendarDay), "");
            }
	
			ApplicationParamTestDataFactory ApplicationParam_TestData = new ApplicationParamTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(ApplicationParam_TestData.Get_Data_File_Name())){
                Data.Add(typeof(ApplicationParam), ApplicationParam_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(ApplicationParam), "");
            }
	
			LogWorkTestDataFactory LogWork_TestData = new LogWorkTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(LogWork_TestData.Get_Data_File_Name())){
                Data.Add(typeof(LogWork), LogWork_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(LogWork), "");
            }
	
			RoleAppTestDataFactory RoleApp_TestData = new RoleAppTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(RoleApp_TestData.Get_Data_File_Name())){
                Data.Add(typeof(RoleApp), RoleApp_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(RoleApp), "");
            }
	
			ControllerAppTestDataFactory ControllerApp_TestData = new ControllerAppTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(ControllerApp_TestData.Get_Data_File_Name())){
                Data.Add(typeof(ControllerApp), ControllerApp_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(ControllerApp), "");
            }
	
			ActionControllerAppTestDataFactory ActionControllerApp_TestData = new ActionControllerAppTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(ActionControllerApp_TestData.Get_Data_File_Name())){
                Data.Add(typeof(ActionControllerApp), ActionControllerApp_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(ActionControllerApp), "");
            }
	
			AuthrorizationAppTestDataFactory AuthrorizationApp_TestData = new AuthrorizationAppTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(AuthrorizationApp_TestData.Get_Data_File_Name())){
                Data.Add(typeof(AuthrorizationApp), AuthrorizationApp_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(AuthrorizationApp), "");
            }
	
			EntityPropertyShortcutTestDataFactory EntityPropertyShortcut_TestData = new EntityPropertyShortcutTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(EntityPropertyShortcut_TestData.Get_Data_File_Name())){
                Data.Add(typeof(EntityPropertyShortcut), EntityPropertyShortcut_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(EntityPropertyShortcut), "");
            }
	
			ClassroomCategoryTestDataFactory ClassroomCategory_TestData = new ClassroomCategoryTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(ClassroomCategory_TestData.Get_Data_File_Name())){
                Data.Add(typeof(ClassroomCategory), ClassroomCategory_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(ClassroomCategory), "");
            }
	
			NationalityTestDataFactory Nationality_TestData = new NationalityTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Nationality_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Nationality), Nationality_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Nationality), "");
            }
	
			GPictureTestDataFactory GPicture_TestData = new GPictureTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(GPicture_TestData.Get_Data_File_Name())){
                Data.Add(typeof(GPicture), GPicture_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(GPicture), "");
            }
	
			ProjectTestDataFactory Project_TestData = new ProjectTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Project_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Project), Project_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Project), "");
            }
	
			Category_JustificationAbsenceTestDataFactory Category_JustificationAbsence_TestData = new Category_JustificationAbsenceTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Category_JustificationAbsence_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Category_JustificationAbsence), Category_JustificationAbsence_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Category_JustificationAbsence), "");
            }
	
			Category_WarningTraineeTestDataFactory Category_WarningTrainee_TestData = new Category_WarningTraineeTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Category_WarningTrainee_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Category_WarningTrainee), Category_WarningTrainee_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Category_WarningTrainee), "");
            }
	
			TrainingYearTestDataFactory TrainingYear_TestData = new TrainingYearTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(TrainingYear_TestData.Get_Data_File_Name())){
                Data.Add(typeof(TrainingYear), TrainingYear_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(TrainingYear), "");
            }
	
			TrainingTypeTestDataFactory TrainingType_TestData = new TrainingTypeTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(TrainingType_TestData.Get_Data_File_Name())){
                Data.Add(typeof(TrainingType), TrainingType_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(TrainingType), "");
            }
	
			SeanceDayTestDataFactory SeanceDay_TestData = new SeanceDayTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(SeanceDay_TestData.Get_Data_File_Name())){
                Data.Add(typeof(SeanceDay), SeanceDay_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(SeanceDay), "");
            }
	
			SeanceNumberTestDataFactory SeanceNumber_TestData = new SeanceNumberTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(SeanceNumber_TestData.Get_Data_File_Name())){
                Data.Add(typeof(SeanceNumber), SeanceNumber_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(SeanceNumber), "");
            }
	
			YearStudyTestDataFactory YearStudy_TestData = new YearStudyTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(YearStudy_TestData.Get_Data_File_Name())){
                Data.Add(typeof(YearStudy), YearStudy_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(YearStudy), "");
            }
	
			SchoollevelTestDataFactory Schoollevel_TestData = new SchoollevelTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Schoollevel_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Schoollevel), Schoollevel_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Schoollevel), "");
            }
	
			FormerSpecialtyTestDataFactory FormerSpecialty_TestData = new FormerSpecialtyTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(FormerSpecialty_TestData.Get_Data_File_Name())){
                Data.Add(typeof(FormerSpecialty), FormerSpecialty_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(FormerSpecialty), "");
            }
	
			TrainingLevelTestDataFactory TrainingLevel_TestData = new TrainingLevelTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(TrainingLevel_TestData.Get_Data_File_Name())){
                Data.Add(typeof(TrainingLevel), TrainingLevel_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(TrainingLevel), "");
            }
	
			MetierTestDataFactory Metier_TestData = new MetierTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Metier_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Metier), Metier_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Metier), "");
            }
	
			SectorTestDataFactory Sector_TestData = new SectorTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Sector_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Sector), Sector_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Sector), "");
            }
	
			FunctionTestDataFactory Function_TestData = new FunctionTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Function_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Function), Function_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Function), "");
            }
	
			Mission_Working_GroupTestDataFactory Mission_Working_Group_TestData = new Mission_Working_GroupTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Mission_Working_Group_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Mission_Working_Group), Mission_Working_Group_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Mission_Working_Group), "");
            }
	
			DisciplineCategoryTestDataFactory DisciplineCategory_TestData = new DisciplineCategoryTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(DisciplineCategory_TestData.Get_Data_File_Name())){
                Data.Add(typeof(DisciplineCategory), DisciplineCategory_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(DisciplineCategory), "");
            }
	
			ClassroomTestDataFactory Classroom_TestData = new ClassroomTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Classroom_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Classroom), Classroom_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Classroom), "");
            }
	
			SpecialtyTestDataFactory Specialty_TestData = new SpecialtyTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Specialty_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Specialty), Specialty_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Specialty), "");
            }
	
			ModuleTrainingTestDataFactory ModuleTraining_TestData = new ModuleTrainingTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(ModuleTraining_TestData.Get_Data_File_Name())){
                Data.Add(typeof(ModuleTraining), ModuleTraining_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(ModuleTraining), "");
            }
	
			FormerTestDataFactory Former_TestData = new FormerTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Former_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Former), Former_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Former), "");
            }
	
			AdministratorTestDataFactory Administrator_TestData = new AdministratorTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Administrator_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Administrator), Administrator_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Administrator), "");
            }
	
			GroupTestDataFactory Group_TestData = new GroupTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Group_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Group), Group_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Group), "");
            }
	
			ScheduleTestDataFactory Schedule_TestData = new ScheduleTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Schedule_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Schedule), Schedule_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Schedule), "");
            }
	
			TaskProjectTestDataFactory TaskProject_TestData = new TaskProjectTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(TaskProject_TestData.Get_Data_File_Name())){
                Data.Add(typeof(TaskProject), TaskProject_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(TaskProject), "");
            }
	
			JustificationAbsenceTestDataFactory JustificationAbsence_TestData = new JustificationAbsenceTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(JustificationAbsence_TestData.Get_Data_File_Name())){
                Data.Add(typeof(JustificationAbsence), JustificationAbsence_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(JustificationAbsence), "");
            }
	
			WarningTraineeTestDataFactory WarningTrainee_TestData = new WarningTraineeTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(WarningTrainee_TestData.Get_Data_File_Name())){
                Data.Add(typeof(WarningTrainee), WarningTrainee_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(WarningTrainee), "");
            }
	
			SanctionCategoryTestDataFactory SanctionCategory_TestData = new SanctionCategoryTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(SanctionCategory_TestData.Get_Data_File_Name())){
                Data.Add(typeof(SanctionCategory), SanctionCategory_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(SanctionCategory), "");
            }
	
			TraineeTestDataFactory Trainee_TestData = new TraineeTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Trainee_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Trainee), Trainee_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Trainee), "");
            }
	
			TrainingTestDataFactory Training_TestData = new TrainingTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Training_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Training), Training_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Training), "");
            }
	
			WorkGroupTestDataFactory WorkGroup_TestData = new WorkGroupTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(WorkGroup_TestData.Get_Data_File_Name())){
                Data.Add(typeof(WorkGroup), WorkGroup_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(WorkGroup), "");
            }
	
			AttendanceStateTestDataFactory AttendanceState_TestData = new AttendanceStateTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(AttendanceState_TestData.Get_Data_File_Name())){
                Data.Add(typeof(AttendanceState), AttendanceState_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(AttendanceState), "");
            }
	
			SeancePlanningTestDataFactory SeancePlanning_TestData = new SeancePlanningTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(SeancePlanning_TestData.Get_Data_File_Name())){
                Data.Add(typeof(SeancePlanning), SeancePlanning_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(SeancePlanning), "");
            }
	
			MeetingTestDataFactory Meeting_TestData = new MeetingTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Meeting_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Meeting), Meeting_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Meeting), "");
            }
	
			SeanceTrainingTestDataFactory SeanceTraining_TestData = new SeanceTrainingTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(SeanceTraining_TestData.Get_Data_File_Name())){
                Data.Add(typeof(SeanceTraining), SeanceTraining_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(SeanceTraining), "");
            }
	
			AbsenceTestDataFactory Absence_TestData = new AbsenceTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Absence_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Absence), Absence_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Absence), "");
            }
	
			StateOfAbseceTestDataFactory StateOfAbsece_TestData = new StateOfAbseceTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(StateOfAbsece_TestData.Get_Data_File_Name())){
                Data.Add(typeof(StateOfAbsece), StateOfAbsece_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(StateOfAbsece), "");
            }
	
			SanctionTestDataFactory Sanction_TestData = new SanctionTestDataFactory(new UnitOfWork<TrainingISModel>(), GAppContext);
            if (File.Exists(Sanction_TestData.Get_Data_File_Name())){
                Data.Add(typeof(Sanction), Sanction_TestData.Get_Data_File_Name());
            }
            else{
                Data.Add(typeof(Sanction), "");
            }
            return Data;
        }

		public virtual Dictionary<Type, Type> Get_TestData_Types()
        {
            Dictionary<Type, Type> Data = new Dictionary<Type, Type>();
	
            Data.Add( typeof(CalendarDay) , typeof(CalendarDayTestDataFactory));
	
            Data.Add( typeof(ApplicationParam) , typeof(ApplicationParamTestDataFactory));
	
            Data.Add( typeof(LogWork) , typeof(LogWorkTestDataFactory));
	
            Data.Add( typeof(RoleApp) , typeof(RoleAppTestDataFactory));
	
            Data.Add( typeof(ControllerApp) , typeof(ControllerAppTestDataFactory));
	
            Data.Add( typeof(ActionControllerApp) , typeof(ActionControllerAppTestDataFactory));
	
            Data.Add( typeof(AuthrorizationApp) , typeof(AuthrorizationAppTestDataFactory));
	
            Data.Add( typeof(EntityPropertyShortcut) , typeof(EntityPropertyShortcutTestDataFactory));
	
            Data.Add( typeof(ClassroomCategory) , typeof(ClassroomCategoryTestDataFactory));
	
            Data.Add( typeof(Nationality) , typeof(NationalityTestDataFactory));
	
            Data.Add( typeof(GPicture) , typeof(GPictureTestDataFactory));
	
            Data.Add( typeof(Project) , typeof(ProjectTestDataFactory));
	
            Data.Add( typeof(Category_JustificationAbsence) , typeof(Category_JustificationAbsenceTestDataFactory));
	
            Data.Add( typeof(Category_WarningTrainee) , typeof(Category_WarningTraineeTestDataFactory));
	
            Data.Add( typeof(TrainingYear) , typeof(TrainingYearTestDataFactory));
	
            Data.Add( typeof(TrainingType) , typeof(TrainingTypeTestDataFactory));
	
            Data.Add( typeof(SeanceDay) , typeof(SeanceDayTestDataFactory));
	
            Data.Add( typeof(SeanceNumber) , typeof(SeanceNumberTestDataFactory));
	
            Data.Add( typeof(YearStudy) , typeof(YearStudyTestDataFactory));
	
            Data.Add( typeof(Schoollevel) , typeof(SchoollevelTestDataFactory));
	
            Data.Add( typeof(FormerSpecialty) , typeof(FormerSpecialtyTestDataFactory));
	
            Data.Add( typeof(TrainingLevel) , typeof(TrainingLevelTestDataFactory));
	
            Data.Add( typeof(Metier) , typeof(MetierTestDataFactory));
	
            Data.Add( typeof(Sector) , typeof(SectorTestDataFactory));
	
            Data.Add( typeof(Function) , typeof(FunctionTestDataFactory));
	
            Data.Add( typeof(Mission_Working_Group) , typeof(Mission_Working_GroupTestDataFactory));
	
            Data.Add( typeof(DisciplineCategory) , typeof(DisciplineCategoryTestDataFactory));
	
            Data.Add( typeof(Classroom) , typeof(ClassroomTestDataFactory));
	
            Data.Add( typeof(Specialty) , typeof(SpecialtyTestDataFactory));
	
            Data.Add( typeof(ModuleTraining) , typeof(ModuleTrainingTestDataFactory));
	
            Data.Add( typeof(Former) , typeof(FormerTestDataFactory));
	
            Data.Add( typeof(Administrator) , typeof(AdministratorTestDataFactory));
	
            Data.Add( typeof(Group) , typeof(GroupTestDataFactory));
	
            Data.Add( typeof(Schedule) , typeof(ScheduleTestDataFactory));
	
            Data.Add( typeof(TaskProject) , typeof(TaskProjectTestDataFactory));
	
            Data.Add( typeof(JustificationAbsence) , typeof(JustificationAbsenceTestDataFactory));
	
            Data.Add( typeof(WarningTrainee) , typeof(WarningTraineeTestDataFactory));
	
            Data.Add( typeof(SanctionCategory) , typeof(SanctionCategoryTestDataFactory));
	
            Data.Add( typeof(Trainee) , typeof(TraineeTestDataFactory));
	
            Data.Add( typeof(Training) , typeof(TrainingTestDataFactory));
	
            Data.Add( typeof(WorkGroup) , typeof(WorkGroupTestDataFactory));
	
            Data.Add( typeof(AttendanceState) , typeof(AttendanceStateTestDataFactory));
	
            Data.Add( typeof(SeancePlanning) , typeof(SeancePlanningTestDataFactory));
	
            Data.Add( typeof(Meeting) , typeof(MeetingTestDataFactory));
	
            Data.Add( typeof(SeanceTraining) , typeof(SeanceTrainingTestDataFactory));
	
            Data.Add( typeof(Absence) , typeof(AbsenceTestDataFactory));
	
            Data.Add( typeof(StateOfAbsece) , typeof(StateOfAbseceTestDataFactory));
	
            Data.Add( typeof(Sanction) , typeof(SanctionTestDataFactory));
            return Data;
        }


    }
}
