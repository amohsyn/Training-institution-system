using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.Entities;

namespace Default_Data
{
    public class Specialtys
    {
        public void InsertDefaultData()
        {

            SpecialtyBLO specialtyBLO = new SpecialtyBLO();

            Specialty specialty = null;

            specialty = specialtyBLO.FindBaseEntityByReference("TDI");
            if (specialty == null)
            {
                specialty = new Specialty();
                specialty.Reference = "TDI";
                specialty.Code = "TDI";
                specialty.Name = "Techniques de Développement Informatique";
                specialtyBLO.Save(specialty);
            }

            specialty = specialtyBLO.FindBaseEntityByReference("TRI");
            if (specialty == null)
            {
                specialty = new Specialty();
                specialty.Reference = "TRI";
                specialty.Code = "TRI";
                specialty.Name = "Techniques des Réseaux Informatiques";
                specialtyBLO.Save(specialty);
            }
            specialty = specialtyBLO.FindBaseEntityByReference("TDM");
            if (specialty == null)
            {
                specialty = new Specialty();
                specialty.Reference = "TDM";
                specialty.Code = "TDM";
                specialty.Name = "Techniques de Développement Multimédia";
                specialtyBLO.Save(specialty);
            }

        }
    }
}
