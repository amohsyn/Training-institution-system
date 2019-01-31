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
        public Group GetGroup_By_GroupCode_TrainingYearReference(string Group_Code, string trainingYear_Reference)
        {
            Group group = this._UnitOfWork.context.Groups
               .Where(g => g.TrainingYear.Reference == trainingYear_Reference)
               .Where(g => g.Code == Group_Code).FirstOrDefault();
            return group;

        }

        public List<Group> Find_By_SpecialtyId(long specialtyId)
        {
            return this._UnitOfWork.context.Groups
               .Where(g => g.SpecialtyId == specialtyId)
               .ToList();
        }
    }
}
