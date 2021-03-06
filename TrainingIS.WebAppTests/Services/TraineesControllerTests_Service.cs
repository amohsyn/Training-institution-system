﻿using System;
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
    public partial class TraineesControllerTests_Service
    {
        public override Trainee CreateValideTraineeInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Trainee trainee = base.CreateValideTraineeInstance(unitOfWork, GAppContext);
            trainee.Email = "trainee@gapp.com";
            return trainee;
        }
    }
}
