using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class GroupBLO
    {
        //public TrainingYear trainingYear = null;

        //public override void Initialize()
        //{
        //    base.Initialize();
        //    this.trainingYear = new TrainingYearBLO(this._UnitOfWork).getCurrentTrainingYear();

        //}

        public override int Save(Group item)
        {
            // Role - Reference = Code + "-" + TrainingYear.Reference
            // must find the TrainingYear from DataBase by Id
            // bacause in the Update Case the item.TrainingYear and item.TrainingYyearId is different

            TrainingYear trainingYear = new TrainingYearBLO(this._UnitOfWork).FindBaseEntityByID(item.TrainingYearId);
            item.Reference = item.Code + "-" + trainingYear.Reference;
            return base.Save(item);
        }


    }
}
