using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Core.Context;
using GApp.DAL;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TrainingIS.WebApp.Tests.Services
{
    public partial class FormersControllerTests_Service
    {
        public override Former CreateValideFormerInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            var former =  base.CreateValideFormerInstance(unitOfWork, GAppContext);
            former.Email = "former_madani@gapp.com";
            return former;
        }
    }
}
