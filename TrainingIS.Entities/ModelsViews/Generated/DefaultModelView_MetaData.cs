

   
 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS.Entities.ModelsViews.Generated
{
    public class DefaultModelView_MetaData
    {
        private List<Type> _List_Default_ModelsViewsTypes;
        public List<Type> List_Default_ModelsViewsTypes {
            get
            {
                if(_List_Default_ModelsViewsTypes == null)
                {
                    foreach (var key in this.ModelsViewsTypes.Keys)
                    {
                        _List_Default_ModelsViewsTypes.AddRange(this.ModelsViewsTypes[key]);
                    }
                }
                return _List_Default_ModelsViewsTypes;
                   
            }
        }
        public Dictionary<Type, List<Type>> ModelsViewsTypes { set; get; }
        public  DefaultModelView_MetaData()
        {
            ModelsViewsTypes = new Dictionary<Type, List<Type>>();
			ModelsViewsTypes[typeof(ApplicationParam)] = new List<Type>();
			ModelsViewsTypes[typeof(ApplicationParam)].Add(typeof(Default_ApplicationParamDetailsView));
			ModelsViewsTypes[typeof(ApplicationParam)].Add(typeof(Default_ApplicationParamFormView));
  
			ModelsViewsTypes[typeof(LogWork)] = new List<Type>();
			ModelsViewsTypes[typeof(LogWork)].Add(typeof(Default_LogWorkDetailsView));
			ModelsViewsTypes[typeof(LogWork)].Add(typeof(Default_LogWorkFormView));
  
			ModelsViewsTypes[typeof(AppRole)] = new List<Type>();
			ModelsViewsTypes[typeof(AppRole)].Add(typeof(Default_AppRoleDetailsView));
			ModelsViewsTypes[typeof(AppRole)].Add(typeof(Default_AppRoleFormView));
  
			ModelsViewsTypes[typeof(AppController)] = new List<Type>();
			ModelsViewsTypes[typeof(AppController)].Add(typeof(Default_AppControllerDetailsView));
			ModelsViewsTypes[typeof(AppController)].Add(typeof(Default_AppControllerFormView));
  
			ModelsViewsTypes[typeof(AppControllerAction)] = new List<Type>();
			ModelsViewsTypes[typeof(AppControllerAction)].Add(typeof(Default_AppControllerActionDetailsView));
			ModelsViewsTypes[typeof(AppControllerAction)].Add(typeof(Default_AppControllerActionFormView));
  
			ModelsViewsTypes[typeof(EntityPropertyShortcut)] = new List<Type>();
			ModelsViewsTypes[typeof(EntityPropertyShortcut)].Add(typeof(Default_EntityPropertyShortcutDetailsView));
			ModelsViewsTypes[typeof(EntityPropertyShortcut)].Add(typeof(Default_EntityPropertyShortcutFormView));
  
			ModelsViewsTypes[typeof(ClassroomCategory)] = new List<Type>();
			ModelsViewsTypes[typeof(ClassroomCategory)].Add(typeof(Default_ClassroomCategoryDetailsView));
			ModelsViewsTypes[typeof(ClassroomCategory)].Add(typeof(Default_ClassroomCategoryFormView));
  
			ModelsViewsTypes[typeof(Nationality)] = new List<Type>();
			ModelsViewsTypes[typeof(Nationality)].Add(typeof(Default_NationalityDetailsView));
			ModelsViewsTypes[typeof(Nationality)].Add(typeof(Default_NationalityFormView));
  
			ModelsViewsTypes[typeof(TrainingYear)] = new List<Type>();
			ModelsViewsTypes[typeof(TrainingYear)].Add(typeof(Default_TrainingYearDetailsView));
			ModelsViewsTypes[typeof(TrainingYear)].Add(typeof(Default_TrainingYearFormView));
  
			ModelsViewsTypes[typeof(TrainingType)] = new List<Type>();
			ModelsViewsTypes[typeof(TrainingType)].Add(typeof(Default_TrainingTypeDetailsView));
			ModelsViewsTypes[typeof(TrainingType)].Add(typeof(Default_TrainingTypeFormView));
  
			ModelsViewsTypes[typeof(SeanceDay)] = new List<Type>();
			ModelsViewsTypes[typeof(SeanceDay)].Add(typeof(Default_SeanceDayDetailsView));
			ModelsViewsTypes[typeof(SeanceDay)].Add(typeof(Default_SeanceDayFormView));
  
			ModelsViewsTypes[typeof(SeanceNumber)] = new List<Type>();
			ModelsViewsTypes[typeof(SeanceNumber)].Add(typeof(Default_SeanceNumberDetailsView));
			ModelsViewsTypes[typeof(SeanceNumber)].Add(typeof(Default_SeanceNumberFormView));
  
			ModelsViewsTypes[typeof(YearStudy)] = new List<Type>();
			ModelsViewsTypes[typeof(YearStudy)].Add(typeof(Default_YearStudyDetailsView));
			ModelsViewsTypes[typeof(YearStudy)].Add(typeof(Default_YearStudyFormView));
  
			ModelsViewsTypes[typeof(Specialty)] = new List<Type>();
			ModelsViewsTypes[typeof(Specialty)].Add(typeof(Default_SpecialtyDetailsView));
			ModelsViewsTypes[typeof(Specialty)].Add(typeof(Default_SpecialtyFormView));
  
			ModelsViewsTypes[typeof(Schoollevel)] = new List<Type>();
			ModelsViewsTypes[typeof(Schoollevel)].Add(typeof(Default_SchoollevelDetailsView));
			ModelsViewsTypes[typeof(Schoollevel)].Add(typeof(Default_SchoollevelFormView));
  
			ModelsViewsTypes[typeof(Classroom)] = new List<Type>();
			ModelsViewsTypes[typeof(Classroom)].Add(typeof(Default_ClassroomDetailsView));
			ModelsViewsTypes[typeof(Classroom)].Add(typeof(Default_ClassroomFormView));
  
			ModelsViewsTypes[typeof(ModuleTraining)] = new List<Type>();
			ModelsViewsTypes[typeof(ModuleTraining)].Add(typeof(Default_ModuleTrainingDetailsView));
			ModelsViewsTypes[typeof(ModuleTraining)].Add(typeof(Default_ModuleTrainingFormView));
  
			ModelsViewsTypes[typeof(Former)] = new List<Type>();
			ModelsViewsTypes[typeof(Former)].Add(typeof(Default_FormerDetailsView));
			ModelsViewsTypes[typeof(Former)].Add(typeof(Default_FormerFormView));
  
			ModelsViewsTypes[typeof(Group)] = new List<Type>();
			ModelsViewsTypes[typeof(Group)].Add(typeof(Default_GroupDetailsView));
			ModelsViewsTypes[typeof(Group)].Add(typeof(Default_GroupFormView));
  
			ModelsViewsTypes[typeof(Trainee)] = new List<Type>();
			ModelsViewsTypes[typeof(Trainee)].Add(typeof(Default_TraineeDetailsView));
			ModelsViewsTypes[typeof(Trainee)].Add(typeof(Default_TraineeFormView));
  
			ModelsViewsTypes[typeof(Training)] = new List<Type>();
			ModelsViewsTypes[typeof(Training)].Add(typeof(Default_TrainingDetailsView));
			ModelsViewsTypes[typeof(Training)].Add(typeof(Default_TrainingFormView));
  
			ModelsViewsTypes[typeof(SeancePlanning)] = new List<Type>();
			ModelsViewsTypes[typeof(SeancePlanning)].Add(typeof(Default_SeancePlanningDetailsView));
			ModelsViewsTypes[typeof(SeancePlanning)].Add(typeof(Default_SeancePlanningFormView));
  
			ModelsViewsTypes[typeof(SeanceTraining)] = new List<Type>();
			ModelsViewsTypes[typeof(SeanceTraining)].Add(typeof(Default_SeanceTrainingDetailsView));
			ModelsViewsTypes[typeof(SeanceTraining)].Add(typeof(Default_SeanceTrainingFormView));
  
			ModelsViewsTypes[typeof(Absence)] = new List<Type>();
			ModelsViewsTypes[typeof(Absence)].Add(typeof(Default_AbsenceDetailsView));
			ModelsViewsTypes[typeof(Absence)].Add(typeof(Default_AbsenceFormView));
  
			ModelsViewsTypes[typeof(StateOfAbsece)] = new List<Type>();
			ModelsViewsTypes[typeof(StateOfAbsece)].Add(typeof(Default_StateOfAbseceDetailsView));
			ModelsViewsTypes[typeof(StateOfAbsece)].Add(typeof(Default_StateOfAbseceFormView));
  
        }
    }
}


