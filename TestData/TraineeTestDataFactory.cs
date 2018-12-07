using GApp.Core.Context;
using GApp.DAL;
using GApp.UnitTest.TestData;
using GApp.UnitTest.TestData.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TestData
{
    public partial class TraineeTestDataFactory  
    {

        protected override List<Trainee> Generate_TestData()
        {
            var Data = new List<Trainee>();
            TraineeBLO traineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
            Nationality nationality = this.UnitOfWork.context.Nationalities.FirstOrDefault();
            var trainingYear = this.UnitOfWork.context.TrainingYears.First();
            var Groups = this.UnitOfWork
                .context.Set<Group>()
                .Where(g=>g.TrainingYear.Id == trainingYear.Id)
                .ToList();

            int Nbr_Trainne_Per_Group = 28;
            int Trainee_Number = 0;
            foreach (var group in Groups)
            {
                for (int i = 0; i < Nbr_Trainne_Per_Group; i++)
                {
                    Trainee_Number++;

                     
                    Trainee trainee = traineeBLO.CreateInstance();
                    trainee.FirstName = string.Format("Nom_{0}", Trainee_Number);
                    trainee.LastName = string.Format("Prenom_{0}",Trainee_Number);

                    trainee.FirstNameArabe = string.Format("Nom_ar_{0}", Trainee_Number);
                    trainee.LastNameArabe = string.Format("Prenom_ar_{0}", Trainee_Number);

                    trainee.CIN = "CIN" + Trainee_Number;
                    trainee.CNE = "CNE" + Trainee_Number;
                    trainee.Group = group;
                    trainee.Reference = trainee.CalculateReference();
                    trainee.Specialty = group.Specialty;
                    trainee.YearStudy = group.YearStudy;
                    trainee.Birthdate = DateTime.Now;
                    trainee.Nationality = nationality;
                    trainee.Ordre = i + 1;
                    if (i> 14)
                    {
                        trainee.Sex = SexEnum.woman;
                    }
                    {
                        trainee.Sex = SexEnum.man;
                    }

                    // IsActifEnum
                    if (i> 25)
                    {
                        trainee.isActif = IsActifEnum.No;
                    }
                    else
                    {
                        trainee.isActif = IsActifEnum.Yes;
                    }


                    Data.Add(trainee);



                }
            }
            return Data;
        }

        public override Trainee CreateValideTraineeInstance()
        {
            Trainee trainee =  base.CreateValideTraineeInstance();
            trainee.Email = "ValideTrainee@gapp.com";
            return trainee;
        }

        public override Trainee Create_CRUD_Trainee_Test_Instance()
        {
            Trainee trainee = base.Create_CRUD_Trainee_Test_Instance();
            trainee.Email = "CRUD_Trainee@gapp.com";
            return trainee;
        }


    }
}
