using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.Entities;

namespace TestData
{
    public partial class FormerTestDataFactory
    {
        public override Former CreateValideFormerInstance()
        {
            var Former = base.CreateValideFormerInstance();
            Former.Email = "Former_Create_Valide_Former@gapp.com";
            Former.Login = "Former@123456";
            Former.Photo = null;
            return Former;
        }

        protected override List<Former> Generate_TestData()
        {
            FormerSpecialtyBLO formerSpecialtyBLO = new FormerSpecialtyBLO(this.UnitOfWork, this.GAppContext);
            FormerBLO formerBLO = new FormerBLO(this.UnitOfWork, this.GAppContext);
            List<Former> Data = new List<Former>();
            var formerSpecialties = formerSpecialtyBLO.FindAll();
            int former_number = 0;
            foreach (FormerSpecialty formerSpecialty in formerSpecialties)
            {
                List<float> ls_WeeklyHourlyMass = new List<float> { 26, 17.5F, 30 };
                foreach (var WeeklyHourlyMass in ls_WeeklyHourlyMass)
                {

                    foreach (var Sex in Enum.GetValues(typeof(SexEnum)))
                    {
                        foreach (var Nationality in this.UnitOfWork.context.Nationalities)
                        {
                            former_number++;
                            Former former = formerBLO.CreateInstance();
                            former.FormerSpecialty = formerSpecialty;
                            former.WeeklyHourlyMass = WeeklyHourlyMass;
                            former.RegistrationNumber = string.Format("Matricule_Former_{0}", former_number);
                            former.CreateUserAccount = true;
                            former.Login = string.Format("Formateur{0}@gapp.com", former_number);
                            former.Password = "Formateur@123456";
                            former.FirstName = string.Format("Nom_Former_{0}", former_number);
                            former.LastName = string.Format("Prenom_Former_{0}", former_number);
                            former.FirstNameArabe = string.Format("Nom_Former_ar_{0}", former_number);
                            former.LastNameArabe = string.Format("Prenom_Former_ar_{0}", former_number);
                            former.Sex = (SexEnum)Sex;
                            former.Nationality = Nationality;
                            former.BirthPlace = "Vide";
                            former.CIN = string.Format("CIN_Former_{0}", former_number);
                            former.Email = string.Format("Formateur{0}@gapp.com", former_number);
                            former.Reference = former.RegistrationNumber;
                            former.Ordre = former_number;
                            Data.Add(former);
                        }
                       

                    }

                   

                }
              


            }

            return Data;
        }
    }
}
