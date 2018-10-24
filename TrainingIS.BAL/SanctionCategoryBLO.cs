using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.enums;

namespace TrainingIS.BLL
{
    public partial class SanctionCategoryBLO
    {
        public List<SanctionCategory> Find_By_DecisionAuthority(DecisionsAuthorities decisionAuthority)
        {
            var SanctionCategories = this._UnitOfWork.context
                .SanctionCategories.Where(s => s.DecisionAuthority == decisionAuthority)
                .ToList();

            return SanctionCategories;
        }
    }
}
