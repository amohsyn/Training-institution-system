using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TestData
{
    public partial class SanctionTestDataFactory
    {
        public override Sanction CreateValideSanctionInstance()
        {

            var sanction = base.CreateValideSanctionInstance();
            sanction.JustificationAbsence = null;
            sanction.JustificationAbsenceId = 0;
            return sanction;
        }
    }
}
