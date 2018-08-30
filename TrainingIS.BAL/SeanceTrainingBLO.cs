using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class SeanceTrainingBLO
    {

        public override int Save(SeanceTraining item)
        {
            return base.Save(item);
        }
        public override int Delete(SeanceTraining item)
        {
            return base.Delete(item);
        }

        /// <summary>
        ///  Find All SeanceTraining according to User Role
        ///  For the former it return its seanceTrainig
        ///  For the PedagogicalDirector its return All SeanceTraining
        /// </summary>
        /// <returns></returns>
        public override List<SeanceTraining> FindAll()
        {
            List<SeanceTraining> SeanceTrainings;
            UserBLO userBLO = new UserBLO(this.GAppContext);
            if (userBLO.Is_Current_User_Has_Role(RoleBLO.Former_ROLE))
            {
                Former former = new FormerBLO(this._UnitOfWork, this.GAppContext).Get_Current_Former() as Former;
                if (former == null) throw new ArgumentNullException(nameof(Former));

                SeanceTrainings = (from s in this._UnitOfWork.context.SeanceTrainings
                                   where s.SeancePlanning.Training.Former.Id == former.Id
                                   select s).ToList();
            }
            else
            {
                SeanceTrainings = (from s in this._UnitOfWork.context.SeanceTrainings

                            select s).ToList();
            }

            return SeanceTrainings;
        }
    }
}
