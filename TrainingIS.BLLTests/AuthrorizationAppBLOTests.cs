using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Entities;
using TrainingIS.WebApp.Controllers;
using System.Transactions;
using GApp.DAL.Exceptions;
using GApp.UnitTest.DataAnnotations;

namespace TrainingIS.BLL.Tests
{
    [TestClass()]
    [CleanTestDB]
    public class AuthrorizationAppBLOTests : Base_BLO_Tests
    {


        [TestMethod()]
        public void Update_to_Existante_Authorization_Exception_Test()
        {
            this.UnitOfWork = new GApp.DAL.UnitOfWork<DAL.TrainingISModel>();
            AuthrorizationAppBLO authrorizationAppBLO = new AuthrorizationAppBLO(this.UnitOfWork, this.GAppContext);


            //  Create Authorisation = Former-Trainees
            AuthrorizationApp authrorization_Former_Trainee = authrorizationAppBLO.CreateInstance();
            authrorization_Former_Trainee.RoleApp = new RoleAppBLO(this.UnitOfWork, this.GAppContext).FindBaseEntityByReference(RoleBLO.Former_ROLE);
            authrorization_Former_Trainee.ControllerApp = new ControllerAppBLO(this.UnitOfWork, this.GAppContext)
                .FindBaseEntityByReference(nameof(TraineesController).RemoveFromEnd("Controller"));
            authrorization_Former_Trainee.isAllAction = true;
            authrorizationAppBLO.Save(authrorization_Former_Trainee);


            // Create Authorisation Former-Absences
            AuthrorizationApp authrorization_Formet_Absence = authrorizationAppBLO.CreateInstance();
            authrorization_Formet_Absence.RoleApp = new RoleAppBLO(this.UnitOfWork, this.GAppContext).FindBaseEntityByReference(RoleBLO.Former_ROLE);
            authrorization_Formet_Absence.ControllerApp = new ControllerAppBLO(this.UnitOfWork, this.GAppContext)
                .FindBaseEntityByReference(nameof(AbsencesController).RemoveFromEnd("Controller"));
            authrorization_Formet_Absence.isAllAction = true;
            authrorizationAppBLO.Save(authrorization_Formet_Absence);


            try
            {
                // Update to Existante Authorisation = Former-Trainees
                authrorization_Formet_Absence.ControllerApp = new ControllerAppBLO(this.UnitOfWork, this.GAppContext)
                    .FindBaseEntityByReference(nameof(TraineesController).RemoveFromEnd("Controller"));
                authrorization_Formet_Absence.isAllAction = true;
                authrorizationAppBLO.Save(authrorization_Formet_Absence);

                // must throw Exception
                throw new AssertFailedException("Update to Existante Authorization ");
            }
            catch (GAppDbUniqueException ex)
            {


            }


        }


        [TestMethod()]
        public void Save_With_Update_Reference_Test()
        {
            this.UnitOfWork = new GApp.DAL.UnitOfWork<DAL.TrainingISModel>();
            AuthrorizationAppBLO authrorizationAppBLO = new AuthrorizationAppBLO(this.UnitOfWork, this.GAppContext);

            // Create Authorisation
            AuthrorizationApp authrorizationApp = authrorizationAppBLO.CreateInstance();
            authrorizationApp.RoleApp = new RoleAppBLO(this.UnitOfWork, this.GAppContext).FindBaseEntityByReference(RoleBLO.Former_ROLE);
            authrorizationApp.ControllerApp = new ControllerAppBLO(this.UnitOfWork, this.GAppContext)
                .FindBaseEntityByReference(nameof(SeanceTrainingsController).RemoveFromEnd("Controller"));
            authrorizationApp.isAllAction = true;
            authrorizationAppBLO.Save(authrorizationApp);
            string isAllAction_reference = authrorizationApp.Reference;

            // Update Authorisation
            authrorizationApp.isAllAction = false;
            authrorizationApp.ControllerApp = new ControllerAppBLO(this.UnitOfWork, this.GAppContext)
                 .FindBaseEntityByReference(nameof(GroupsController).RemoveFromEnd("Controller"));
            string index_action_reference = string.Format("{0}-{1}", authrorizationApp.ControllerApp.Code, "Index");
            ActionControllerApp actionControllerApp = new ActionControllerAppBLO(this.UnitOfWork, this.GAppContext).FindBaseEntityByReference(index_action_reference);
            authrorizationApp.ActionControllerApps.Add(actionControllerApp);
            authrorizationAppBLO.Save(authrorizationApp);
            string Index_Action_reference = authrorizationApp.Reference;

            Assert.AreNotEqual(isAllAction_reference, Index_Action_reference);
            Assert.AreEqual(Index_Action_reference, authrorizationApp.CalculateReference());
        }


        [TestMethod()]
        public void Save_Existante_Authorization_Exception_Test()
        {
            this.UnitOfWork = new GApp.DAL.UnitOfWork<DAL.TrainingISModel>();
            AuthrorizationAppBLO authrorizationAppBLO = new AuthrorizationAppBLO(this.UnitOfWork, this.GAppContext);


            try
            {
                // Create Authorisation
                AuthrorizationApp authrorizationApp = authrorizationAppBLO.CreateInstance();
                authrorizationApp.RoleApp = new RoleAppBLO(this.UnitOfWork, this.GAppContext).FindBaseEntityByReference(RoleBLO.Former_ROLE);
                authrorizationApp.ControllerApp = new ControllerAppBLO(this.UnitOfWork, this.GAppContext)
                    .FindBaseEntityByReference(nameof(SeanceTrainingsController).RemoveFromEnd("Controller"));
                authrorizationApp.isAllAction = true;
                authrorizationAppBLO.Save(authrorizationApp);

                // Create Authorisation
                authrorizationApp = authrorizationAppBLO.CreateInstance();
                authrorizationApp.RoleApp = new RoleAppBLO(this.UnitOfWork, this.GAppContext).FindBaseEntityByReference(RoleBLO.Former_ROLE);
                authrorizationApp.ControllerApp = new ControllerAppBLO(this.UnitOfWork, this.GAppContext)
                    .FindBaseEntityByReference(nameof(SeanceTrainingsController).RemoveFromEnd("Controller"));
                authrorizationApp.isAllAction = true;
                authrorizationAppBLO.Save(authrorizationApp);

                // must throw Exception
                Assert.IsFalse(true);
            }
            catch (GAppDbUniqueException ex)
            {


            }



        }


    }
}