using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrainingIS.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Former,Supervisor")]
    public  partial class SeanceTrainingsController
    {

    }
}