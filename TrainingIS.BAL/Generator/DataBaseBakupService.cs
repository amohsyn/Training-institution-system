using System.Data;

namespace TrainingIS.BLL.Services
{
    public partial class DataBaseBakupService
    {
        public void AddDataTablesToDataSet(DataSet dataSet)
        {
			            dataSet.Tables.Add(new ClassroomBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new ClassroomCategoryBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new YearStudyBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new TraineeBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new TrainingYearBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new SpecialtyBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new TrainingBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new ApplicationParamBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new TrainingTypeBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new SchoollevelBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new SeanceTrainingBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new SeancePlanningBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new ModuleTrainingBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new FormerBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new SeanceDayBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new NationalityBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new EntityPropertyShortcutBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new SeanceNumberBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new StateOfAbseceBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new GroupBLO(this._UnitOfWork).Export());
			            dataSet.Tables.Add(new AbsenceBLO(this._UnitOfWork).Export());
			        }
    }
}




