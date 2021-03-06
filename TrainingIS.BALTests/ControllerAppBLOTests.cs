﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.WebApp.Controllers;
using TrainingIS.Entities;
using TrainingIS.DAL;
using System.Reflection;
using System.Web.Mvc;
using System.Data.Entity;
using GApp.DAL;
using GApp.Entities;
using GApp.Core.Context;

namespace TrainingIS.BLL.Tests
{
    [TestClass()]
    public class ControllerAppBLOTests
    {
        [TestMethod()]
        public void Update_ControllerAppsTest()
        {
            GAppContext gAppContext = new GAppContext(RoleBLO.Root_ROLE);
            UnitOfWork<TrainingISModel> unitOfWork = new UnitOfWork<TrainingISModel>();
            ControllerAppBLO controllerAppBLO = new ControllerAppBLO(unitOfWork, gAppContext);

            var controller_type = typeof(ApplicationParamsController);
            string code = controller_type.Name.RemoveFromEnd("Controller");
            ControllerApp controllerApp = controllerAppBLO.FindBaseEntityByReference(code);
            if (controllerApp == null)
            {
                controllerApp = new ControllerApp();
                controllerApp.Code = code;
                controllerApp.Name = code;
                controllerAppBLO.Save(controllerApp);
            }

            EntityState state = unitOfWork.context.Entry(controllerApp).State ;

            Assert.AreEqual(state, EntityState.Unchanged);
        }
    }
}