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

        public List<SanctionCategory> Find_By_System_DisciplineCategory(System_DisciplineCategories system_DisciplineCategory)
        {
            var ls_sanctionCategory = this._UnitOfWork.context.SanctionCategories
                     .Where(c => c.DisciplineCategory.System_DisciplineCategy == system_DisciplineCategory)
                     .OrderBy(c => c.WorkflowOrder)
                     .ToList();
                     
            return ls_sanctionCategory;
        }

       

        /// <summary>
        /// Geht the next SanctionCategroy in the WorkFlox order
        /// </summary>
        /// <param name="current_Sanction_Category"></param>
        /// <param name="system_DisciplineCategory"></param>
        /// <returns>return null if the next SanctionCategory not exist</returns>
        public SanctionCategory Get_Next_SanctionCategory(SanctionCategory current_Sanction_Category, System_DisciplineCategories system_DisciplineCategory)
        {
            SanctionCategory sanctionCategory = null;

            if (current_Sanction_Category == null)
            {
                 sanctionCategory = this._UnitOfWork.context.SanctionCategories
                    .Where(c => c.DisciplineCategory.System_DisciplineCategy == system_DisciplineCategory)
                    .OrderBy(c => c.WorkflowOrder)
                    .FirstOrDefault();
            }
            else
            {
                sanctionCategory = this._UnitOfWork.context.SanctionCategories
                   .Where(c => c.DisciplineCategory.System_DisciplineCategy == system_DisciplineCategory)
                   .Where(c => c.WorkflowOrder > current_Sanction_Category.WorkflowOrder)
                   .OrderBy(c => c.WorkflowOrder)
                   .FirstOrDefault();
            }
            return sanctionCategory;
        }
    }
}
