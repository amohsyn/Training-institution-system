using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class TrainingBLO
    {
        public List<Entities.Group> Get_Groups_Of_Former(Former current_Former)
        {
            // [Bug] add CurrentTraining Year Condition
            List<Entities.Group> Groups = this._UnitOfWork.context.Trainings
                .Where(t => t.Former.Id == current_Former.Id)
                .Select(t => t.Group)
                .Distinct()
                .ToList();

            return Groups;



        }
    }
}
