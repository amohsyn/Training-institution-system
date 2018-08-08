  

   
 
 
  
    
  
  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.WebApp.Controllers;

namespace TrainingIS.WebApp.Manager.Controller
{
    public partial class Controllers_MetaData
    {
 
        private void Add_Default_Controllers()
        {
            ControllersTypes.Add(typeof(ApplicationParamsController));
            ControllersTypes.Add(typeof(LogWorksController));
            ControllersTypes.Add(typeof(RoleAppsController));
            ControllersTypes.Add(typeof(ControllerAppsController));
            ControllersTypes.Add(typeof(ActionControllerAppsController));
            ControllersTypes.Add(typeof(AuthrorizationAppsController));
            ControllersTypes.Add(typeof(EntityPropertyShortcutsController));
            ControllersTypes.Add(typeof(ClassroomCategoriesController));
            ControllersTypes.Add(typeof(NationalitiesController));
            ControllersTypes.Add(typeof(TrainingYearsController));
            ControllersTypes.Add(typeof(TrainingTypesController));
            ControllersTypes.Add(typeof(SeanceDaysController));
            ControllersTypes.Add(typeof(SeanceNumbersController));
            ControllersTypes.Add(typeof(YearStudiesController));
            ControllersTypes.Add(typeof(SpecialtiesController));
            ControllersTypes.Add(typeof(SchoollevelsController));
            ControllersTypes.Add(typeof(ClassroomsController));
            ControllersTypes.Add(typeof(ModuleTrainingsController));
            ControllersTypes.Add(typeof(FormersController));
            ControllersTypes.Add(typeof(GroupsController));
            ControllersTypes.Add(typeof(SchedulesController));
            ControllersTypes.Add(typeof(TraineesController));
            ControllersTypes.Add(typeof(TrainingsController));
            ControllersTypes.Add(typeof(SeancePlanningsController));
            ControllersTypes.Add(typeof(SeanceTrainingsController));
            ControllersTypes.Add(typeof(AbsencesController));
            ControllersTypes.Add(typeof(StateOfAbsecesController));
     
        }
    }
}
