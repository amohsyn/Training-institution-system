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
    public partial class FormersControllerTests_Service
    {
        public override Former CreateValideFormerInstance(UnitOfWork<TrainingISModel> unitOfWork = null)
        {
            var former =  base.CreateValideFormerInstance(unitOfWork);
            former.Email = "former_madani@gapp.com";
            return former;
        }
    }
}
