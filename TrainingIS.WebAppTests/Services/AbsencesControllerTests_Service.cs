using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.DAL;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TrainingIS.WebApp.Tests.Services
 
{
    public partial class AbsencesControllerTests_Service
    {
        public override Absence CreateValideAbsenceInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            var absence = base.CreateValideAbsenceInstance(unitOfWork);
          
            return absence;
        }
    }
}
