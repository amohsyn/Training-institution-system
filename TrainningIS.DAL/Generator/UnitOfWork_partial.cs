﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS.DAL
{
    public partial class UnitOfWork 
    { 
   
        private TrainingDAO _TrainingDAO;
        public TrainingDAO TrainingDAO
        {
            get
            {
                if (this._TrainingDAO == null)
                    this._TrainingDAO = new TrainingDAO(context);
                return _TrainingDAO;
            }
        }

        private TraineeDAO _TraineeDAO;
        public TraineeDAO TraineeDAO
        {
            get
            {
                if (this._TraineeDAO == null)
                    this._TraineeDAO = new TraineeDAO(context);
                return _TraineeDAO;
            }
        }

        private SeanceTrainingDAO _SeanceTrainingDAO;
        public SeanceTrainingDAO SeanceTrainingDAO
        {
            get
            {
                if (this._SeanceTrainingDAO == null)
                    this._SeanceTrainingDAO = new SeanceTrainingDAO(context);
                return _SeanceTrainingDAO;
            }
        }

        private RoleAppDAO _RoleAppDAO;
        public RoleAppDAO RoleAppDAO
        {
            get
            {
                if (this._RoleAppDAO == null)
                    this._RoleAppDAO = new RoleAppDAO(context);
                return _RoleAppDAO;
            }
        }

        private EntityPropertyShortcutDAO _EntityPropertyShortcutDAO;
        public EntityPropertyShortcutDAO EntityPropertyShortcutDAO
        {
            get
            {
                if (this._EntityPropertyShortcutDAO == null)
                    this._EntityPropertyShortcutDAO = new EntityPropertyShortcutDAO(context);
                return _EntityPropertyShortcutDAO;
            }
        }

        private SeanceNumberDAO _SeanceNumberDAO;
        public SeanceNumberDAO SeanceNumberDAO
        {
            get
            {
                if (this._SeanceNumberDAO == null)
                    this._SeanceNumberDAO = new SeanceNumberDAO(context);
                return _SeanceNumberDAO;
            }
        }

        private ModuleTrainingDAO _ModuleTrainingDAO;
        public ModuleTrainingDAO ModuleTrainingDAO
        {
            get
            {
                if (this._ModuleTrainingDAO == null)
                    this._ModuleTrainingDAO = new ModuleTrainingDAO(context);
                return _ModuleTrainingDAO;
            }
        }

        private ClassroomCategoryDAO _ClassroomCategoryDAO;
        public ClassroomCategoryDAO ClassroomCategoryDAO
        {
            get
            {
                if (this._ClassroomCategoryDAO == null)
                    this._ClassroomCategoryDAO = new ClassroomCategoryDAO(context);
                return _ClassroomCategoryDAO;
            }
        }

        private LogWorkDAO _LogWorkDAO;
        public LogWorkDAO LogWorkDAO
        {
            get
            {
                if (this._LogWorkDAO == null)
                    this._LogWorkDAO = new LogWorkDAO(context);
                return _LogWorkDAO;
            }
        }

        private FormerDAO _FormerDAO;
        public FormerDAO FormerDAO
        {
            get
            {
                if (this._FormerDAO == null)
                    this._FormerDAO = new FormerDAO(context);
                return _FormerDAO;
            }
        }

        private AuthrorizationAppDAO _AuthrorizationAppDAO;
        public AuthrorizationAppDAO AuthrorizationAppDAO
        {
            get
            {
                if (this._AuthrorizationAppDAO == null)
                    this._AuthrorizationAppDAO = new AuthrorizationAppDAO(context);
                return _AuthrorizationAppDAO;
            }
        }

        private SeancePlanningDAO _SeancePlanningDAO;
        public SeancePlanningDAO SeancePlanningDAO
        {
            get
            {
                if (this._SeancePlanningDAO == null)
                    this._SeancePlanningDAO = new SeancePlanningDAO(context);
                return _SeancePlanningDAO;
            }
        }

        private SeanceDayDAO _SeanceDayDAO;
        public SeanceDayDAO SeanceDayDAO
        {
            get
            {
                if (this._SeanceDayDAO == null)
                    this._SeanceDayDAO = new SeanceDayDAO(context);
                return _SeanceDayDAO;
            }
        }

        private SchoollevelDAO _SchoollevelDAO;
        public SchoollevelDAO SchoollevelDAO
        {
            get
            {
                if (this._SchoollevelDAO == null)
                    this._SchoollevelDAO = new SchoollevelDAO(context);
                return _SchoollevelDAO;
            }
        }

        private TrainingYearDAO _TrainingYearDAO;
        public TrainingYearDAO TrainingYearDAO
        {
            get
            {
                if (this._TrainingYearDAO == null)
                    this._TrainingYearDAO = new TrainingYearDAO(context);
                return _TrainingYearDAO;
            }
        }

        private StateOfAbseceDAO _StateOfAbseceDAO;
        public StateOfAbseceDAO StateOfAbseceDAO
        {
            get
            {
                if (this._StateOfAbseceDAO == null)
                    this._StateOfAbseceDAO = new StateOfAbseceDAO(context);
                return _StateOfAbseceDAO;
            }
        }

        private ApplicationParamDAO _ApplicationParamDAO;
        public ApplicationParamDAO ApplicationParamDAO
        {
            get
            {
                if (this._ApplicationParamDAO == null)
                    this._ApplicationParamDAO = new ApplicationParamDAO(context);
                return _ApplicationParamDAO;
            }
        }

        private NationalityDAO _NationalityDAO;
        public NationalityDAO NationalityDAO
        {
            get
            {
                if (this._NationalityDAO == null)
                    this._NationalityDAO = new NationalityDAO(context);
                return _NationalityDAO;
            }
        }

        private GroupDAO _GroupDAO;
        public GroupDAO GroupDAO
        {
            get
            {
                if (this._GroupDAO == null)
                    this._GroupDAO = new GroupDAO(context);
                return _GroupDAO;
            }
        }

        private ClassroomDAO _ClassroomDAO;
        public ClassroomDAO ClassroomDAO
        {
            get
            {
                if (this._ClassroomDAO == null)
                    this._ClassroomDAO = new ClassroomDAO(context);
                return _ClassroomDAO;
            }
        }

        private YearStudyDAO _YearStudyDAO;
        public YearStudyDAO YearStudyDAO
        {
            get
            {
                if (this._YearStudyDAO == null)
                    this._YearStudyDAO = new YearStudyDAO(context);
                return _YearStudyDAO;
            }
        }

        private TrainingTypeDAO _TrainingTypeDAO;
        public TrainingTypeDAO TrainingTypeDAO
        {
            get
            {
                if (this._TrainingTypeDAO == null)
                    this._TrainingTypeDAO = new TrainingTypeDAO(context);
                return _TrainingTypeDAO;
            }
        }

        private ControllerAppDAO _ControllerAppDAO;
        public ControllerAppDAO ControllerAppDAO
        {
            get
            {
                if (this._ControllerAppDAO == null)
                    this._ControllerAppDAO = new ControllerAppDAO(context);
                return _ControllerAppDAO;
            }
        }

        private SpecialtyDAO _SpecialtyDAO;
        public SpecialtyDAO SpecialtyDAO
        {
            get
            {
                if (this._SpecialtyDAO == null)
                    this._SpecialtyDAO = new SpecialtyDAO(context);
                return _SpecialtyDAO;
            }
        }

        private ActionControllerAppDAO _ActionControllerAppDAO;
        public ActionControllerAppDAO ActionControllerAppDAO
        {
            get
            {
                if (this._ActionControllerAppDAO == null)
                    this._ActionControllerAppDAO = new ActionControllerAppDAO(context);
                return _ActionControllerAppDAO;
            }
        }

        private AbsenceDAO _AbsenceDAO;
        public AbsenceDAO AbsenceDAO
        {
            get
            {
                if (this._AbsenceDAO == null)
                    this._AbsenceDAO = new AbsenceDAO(context);
                return _AbsenceDAO;
            }
        }

        private ScheduleDAO _ScheduleDAO;
        public ScheduleDAO ScheduleDAO
        {
            get
            {
                if (this._ScheduleDAO == null)
                    this._ScheduleDAO = new ScheduleDAO(context);
                return _ScheduleDAO;
            }
        }

    }
}


