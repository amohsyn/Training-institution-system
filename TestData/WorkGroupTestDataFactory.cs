using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using AutoFixture;
using TrainingIS.BLL;
using TrainingIS.DAL;
using System.ComponentModel.DataAnnotations;
using GApp.WebApp.Manager.Views;
using GApp.DAL;
using GApp.Entities;
using GApp.Core.Context;
using TrainingIS.Models.WorkGroups;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;
using GApp.UnitTest.TestData;
using GApp.UnitTest.TestData.Enums;
using System.IO;
using System.Data;
using GApp.DAL.ReadExcel;
using ClosedXML.Excel;

namespace TestData
{
    public partial class WorkGroupTestDataFactory
    {
        public override WorkGroup CreateValideWorkGroupInstance()
        {
            if (UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();

            WorkGroup Valide_WorkGroup = this._Fixture.Build<WorkGroup>()
                .Without(p => p.President)
                .Without(p => p.VicePresident)
                 .Without(p => p.Protractor)
                
                .Create();

            Valide_WorkGroup.Id = 0;
            // Many to One 
            //   
            // President_Former
            var President_Former = new FormerTestDataFactory(UnitOfWork, GAppContext).CreateOrLouadFirstFormer();
            Valide_WorkGroup.President_Former = President_Former;

            // President_Trainee
            var President_Trainee = new TraineeTestDataFactory(UnitOfWork, GAppContext).CreateOrLouadFirstTrainee();
            Valide_WorkGroup.President_Trainee = President_Trainee;

            // President_Administrator
            var President_Administrator = new AdministratorTestDataFactory(UnitOfWork, GAppContext).CreateOrLouadFirstAdministrator();
            Valide_WorkGroup.President_Administrator = President_Administrator;

            // VicePresident_Former
            var VicePresident_Former = new FormerTestDataFactory(UnitOfWork, GAppContext).CreateOrLouadFirstFormer();
            Valide_WorkGroup.VicePresident_Former = VicePresident_Former;

            // VicePresident_Trainee
            var VicePresident_Trainee = new TraineeTestDataFactory(UnitOfWork, GAppContext).CreateOrLouadFirstTrainee();
            Valide_WorkGroup.VicePresident_Trainee = VicePresident_Trainee;

            // VicePresident_Administrator
            var VicePresident_Administrator = new AdministratorTestDataFactory(UnitOfWork, GAppContext).CreateOrLouadFirstAdministrator();
            Valide_WorkGroup.VicePresident_Administrator = VicePresident_Administrator;

            // Protractor_Former
            var Protractor_Former = new FormerTestDataFactory(UnitOfWork, GAppContext).CreateOrLouadFirstFormer();
            Valide_WorkGroup.Protractor_Former = Protractor_Former;

            // Protractor_Administrator
            var Protractor_Administrator = new AdministratorTestDataFactory(UnitOfWork, GAppContext).CreateOrLouadFirstAdministrator();
            Valide_WorkGroup.Protractor_Administrator = Protractor_Administrator;

            // Protractor_Trainee
            var Protractor_Trainee = new TraineeTestDataFactory(UnitOfWork, GAppContext).CreateOrLouadFirstTrainee();
            Valide_WorkGroup.Protractor_Trainee = Protractor_Trainee;

            // One to Many
            //
            Valide_WorkGroup.MemebersAdministrators = null;
            Valide_WorkGroup.MemebersFormers = null;
            Valide_WorkGroup.MemebersTrainees = null;
            Valide_WorkGroup.Mission_Working_Groups = null;
            return Valide_WorkGroup;
        }
    }
}
