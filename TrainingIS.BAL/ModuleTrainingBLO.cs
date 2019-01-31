using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class ModuleTrainingBLO
    {
        public List<ModuleTraining> Find_By_SpecialtyId(long SpecialtyId)
        {
            var modules =  this._UnitOfWork.context.ModuleTrainings
                .Where(m => m.SpecialtyId == SpecialtyId)
                .ToList();
            return modules;

        }
    }
}
