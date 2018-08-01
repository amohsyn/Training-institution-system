using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class BLO_Manager
    { 
		public BLO_Manager(UnitOfWork UnitOfWork)
        {
            this._UnitOfWork = UnitOfWork;
			BLO_Types[typeof(Classroom)] = typeof(ClassroomBLO);
			BLO_Types[typeof(ClassroomCategory)] = typeof(ClassroomCategoryBLO);
			BLO_Types[typeof(YearStudy)] = typeof(YearStudyBLO);
			BLO_Types[typeof(Trainee)] = typeof(TraineeBLO);
			BLO_Types[typeof(TrainingYear)] = typeof(TrainingYearBLO);
			BLO_Types[typeof(Specialty)] = typeof(SpecialtyBLO);
			BLO_Types[typeof(AppControllerAction)] = typeof(AppControllerActionBLO);
			BLO_Types[typeof(AppController)] = typeof(AppControllerBLO);
			BLO_Types[typeof(Training)] = typeof(TrainingBLO);
			BLO_Types[typeof(AppRole)] = typeof(AppRoleBLO);
			BLO_Types[typeof(TrainingType)] = typeof(TrainingTypeBLO);
			BLO_Types[typeof(Schoollevel)] = typeof(SchoollevelBLO);
			BLO_Types[typeof(SeanceTraining)] = typeof(SeanceTrainingBLO);
			BLO_Types[typeof(SeancePlanning)] = typeof(SeancePlanningBLO);
			BLO_Types[typeof(ModuleTraining)] = typeof(ModuleTrainingBLO);
			BLO_Types[typeof(Former)] = typeof(FormerBLO);
			BLO_Types[typeof(LogWork)] = typeof(LogWorkBLO);
			BLO_Types[typeof(SeanceDay)] = typeof(SeanceDayBLO);
			BLO_Types[typeof(Nationality)] = typeof(NationalityBLO);
			BLO_Types[typeof(EntityPropertyShortcut)] = typeof(EntityPropertyShortcutBLO);
			BLO_Types[typeof(ApplicationParam)] = typeof(ApplicationParamBLO);
			BLO_Types[typeof(SeanceNumber)] = typeof(SeanceNumberBLO);
			BLO_Types[typeof(StateOfAbsece)] = typeof(StateOfAbseceBLO);
			BLO_Types[typeof(Group)] = typeof(GroupBLO);
			BLO_Types[typeof(Absence)] = typeof(AbsenceBLO);
        }
   
        private ClassroomBLO _ClassroomBLO;
        public ClassroomBLO ClassroomBLO
        {
            get
            {
                if (this._ClassroomBLO == null)
                    this._ClassroomBLO = new ClassroomBLO(this._UnitOfWork);
                return _ClassroomBLO;
            }
        }

        private ClassroomCategoryBLO _ClassroomCategoryBLO;
        public ClassroomCategoryBLO ClassroomCategoryBLO
        {
            get
            {
                if (this._ClassroomCategoryBLO == null)
                    this._ClassroomCategoryBLO = new ClassroomCategoryBLO(this._UnitOfWork);
                return _ClassroomCategoryBLO;
            }
        }

        private YearStudyBLO _YearStudyBLO;
        public YearStudyBLO YearStudyBLO
        {
            get
            {
                if (this._YearStudyBLO == null)
                    this._YearStudyBLO = new YearStudyBLO(this._UnitOfWork);
                return _YearStudyBLO;
            }
        }

        private TraineeBLO _TraineeBLO;
        public TraineeBLO TraineeBLO
        {
            get
            {
                if (this._TraineeBLO == null)
                    this._TraineeBLO = new TraineeBLO(this._UnitOfWork);
                return _TraineeBLO;
            }
        }

        private TrainingYearBLO _TrainingYearBLO;
        public TrainingYearBLO TrainingYearBLO
        {
            get
            {
                if (this._TrainingYearBLO == null)
                    this._TrainingYearBLO = new TrainingYearBLO(this._UnitOfWork);
                return _TrainingYearBLO;
            }
        }

        private SpecialtyBLO _SpecialtyBLO;
        public SpecialtyBLO SpecialtyBLO
        {
            get
            {
                if (this._SpecialtyBLO == null)
                    this._SpecialtyBLO = new SpecialtyBLO(this._UnitOfWork);
                return _SpecialtyBLO;
            }
        }

        private AppControllerActionBLO _AppControllerActionBLO;
        public AppControllerActionBLO AppControllerActionBLO
        {
            get
            {
                if (this._AppControllerActionBLO == null)
                    this._AppControllerActionBLO = new AppControllerActionBLO(this._UnitOfWork);
                return _AppControllerActionBLO;
            }
        }

        private AppControllerBLO _AppControllerBLO;
        public AppControllerBLO AppControllerBLO
        {
            get
            {
                if (this._AppControllerBLO == null)
                    this._AppControllerBLO = new AppControllerBLO(this._UnitOfWork);
                return _AppControllerBLO;
            }
        }

        private TrainingBLO _TrainingBLO;
        public TrainingBLO TrainingBLO
        {
            get
            {
                if (this._TrainingBLO == null)
                    this._TrainingBLO = new TrainingBLO(this._UnitOfWork);
                return _TrainingBLO;
            }
        }

        private AppRoleBLO _AppRoleBLO;
        public AppRoleBLO AppRoleBLO
        {
            get
            {
                if (this._AppRoleBLO == null)
                    this._AppRoleBLO = new AppRoleBLO(this._UnitOfWork);
                return _AppRoleBLO;
            }
        }

        private TrainingTypeBLO _TrainingTypeBLO;
        public TrainingTypeBLO TrainingTypeBLO
        {
            get
            {
                if (this._TrainingTypeBLO == null)
                    this._TrainingTypeBLO = new TrainingTypeBLO(this._UnitOfWork);
                return _TrainingTypeBLO;
            }
        }

        private SchoollevelBLO _SchoollevelBLO;
        public SchoollevelBLO SchoollevelBLO
        {
            get
            {
                if (this._SchoollevelBLO == null)
                    this._SchoollevelBLO = new SchoollevelBLO(this._UnitOfWork);
                return _SchoollevelBLO;
            }
        }

        private SeanceTrainingBLO _SeanceTrainingBLO;
        public SeanceTrainingBLO SeanceTrainingBLO
        {
            get
            {
                if (this._SeanceTrainingBLO == null)
                    this._SeanceTrainingBLO = new SeanceTrainingBLO(this._UnitOfWork);
                return _SeanceTrainingBLO;
            }
        }

        private SeancePlanningBLO _SeancePlanningBLO;
        public SeancePlanningBLO SeancePlanningBLO
        {
            get
            {
                if (this._SeancePlanningBLO == null)
                    this._SeancePlanningBLO = new SeancePlanningBLO(this._UnitOfWork);
                return _SeancePlanningBLO;
            }
        }

        private ModuleTrainingBLO _ModuleTrainingBLO;
        public ModuleTrainingBLO ModuleTrainingBLO
        {
            get
            {
                if (this._ModuleTrainingBLO == null)
                    this._ModuleTrainingBLO = new ModuleTrainingBLO(this._UnitOfWork);
                return _ModuleTrainingBLO;
            }
        }

        private FormerBLO _FormerBLO;
        public FormerBLO FormerBLO
        {
            get
            {
                if (this._FormerBLO == null)
                    this._FormerBLO = new FormerBLO(this._UnitOfWork);
                return _FormerBLO;
            }
        }

        private LogWorkBLO _LogWorkBLO;
        public LogWorkBLO LogWorkBLO
        {
            get
            {
                if (this._LogWorkBLO == null)
                    this._LogWorkBLO = new LogWorkBLO(this._UnitOfWork);
                return _LogWorkBLO;
            }
        }

        private SeanceDayBLO _SeanceDayBLO;
        public SeanceDayBLO SeanceDayBLO
        {
            get
            {
                if (this._SeanceDayBLO == null)
                    this._SeanceDayBLO = new SeanceDayBLO(this._UnitOfWork);
                return _SeanceDayBLO;
            }
        }

        private NationalityBLO _NationalityBLO;
        public NationalityBLO NationalityBLO
        {
            get
            {
                if (this._NationalityBLO == null)
                    this._NationalityBLO = new NationalityBLO(this._UnitOfWork);
                return _NationalityBLO;
            }
        }

        private EntityPropertyShortcutBLO _EntityPropertyShortcutBLO;
        public EntityPropertyShortcutBLO EntityPropertyShortcutBLO
        {
            get
            {
                if (this._EntityPropertyShortcutBLO == null)
                    this._EntityPropertyShortcutBLO = new EntityPropertyShortcutBLO(this._UnitOfWork);
                return _EntityPropertyShortcutBLO;
            }
        }

        private ApplicationParamBLO _ApplicationParamBLO;
        public ApplicationParamBLO ApplicationParamBLO
        {
            get
            {
                if (this._ApplicationParamBLO == null)
                    this._ApplicationParamBLO = new ApplicationParamBLO(this._UnitOfWork);
                return _ApplicationParamBLO;
            }
        }

        private SeanceNumberBLO _SeanceNumberBLO;
        public SeanceNumberBLO SeanceNumberBLO
        {
            get
            {
                if (this._SeanceNumberBLO == null)
                    this._SeanceNumberBLO = new SeanceNumberBLO(this._UnitOfWork);
                return _SeanceNumberBLO;
            }
        }

        private StateOfAbseceBLO _StateOfAbseceBLO;
        public StateOfAbseceBLO StateOfAbseceBLO
        {
            get
            {
                if (this._StateOfAbseceBLO == null)
                    this._StateOfAbseceBLO = new StateOfAbseceBLO(this._UnitOfWork);
                return _StateOfAbseceBLO;
            }
        }

        private GroupBLO _GroupBLO;
        public GroupBLO GroupBLO
        {
            get
            {
                if (this._GroupBLO == null)
                    this._GroupBLO = new GroupBLO(this._UnitOfWork);
                return _GroupBLO;
            }
        }

        private AbsenceBLO _AbsenceBLO;
        public AbsenceBLO AbsenceBLO
        {
            get
            {
                if (this._AbsenceBLO == null)
                    this._AbsenceBLO = new AbsenceBLO(this._UnitOfWork);
                return _AbsenceBLO;
            }
        }

    }
}



