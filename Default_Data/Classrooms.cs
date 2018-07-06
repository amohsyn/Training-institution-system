using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.Entities;

namespace Default_Data
{
    public class Classrooms
    {
        public void InsertDefaultData()
        {
            // Insert classroomCategory
            ClassroomCategoryBLO classroomCategoryBLO = new ClassroomCategoryBLO();

            ClassroomCategory classroomCategoryCours = classroomCategoryBLO.FindBaseEntityByReference("Cours");
            if (classroomCategoryCours == null)
            {
                classroomCategoryCours = new ClassroomCategory();
                classroomCategoryCours.Reference = "Cours";
                classroomCategoryCours.Code = "Cours";
                classroomCategoryCours.Name = "Salle de cours";
                classroomCategoryBLO.Save(classroomCategoryCours);
            }

            ClassroomCategory classroomCategoryTP = classroomCategoryBLO.FindBaseEntityByReference("TP");
            if (classroomCategoryTP == null)
            {
                classroomCategoryTP = new ClassroomCategory();
                classroomCategoryTP.Reference = "TP";
                classroomCategoryTP.Code = "TP";
                classroomCategoryTP.Name = "Salle de TP";
                classroomCategoryBLO.Save(classroomCategoryTP);
            }

            // Insert ClassRomm
            ClassroomBLO classroomBLO = new ClassroomBLO();

            for (int i = 1; i <= 6; i++)
            {
                Classroom classroomTP = classroomBLO.FindBaseEntityByReference("TP" + i);
                if (classroomTP == null)
                {
                    classroomTP = new Classroom();
                    classroomTP.Reference = "TP" + i;
                    classroomTP.Code = "TP" + i;
                    classroomTP.Name = "TP" + i;
                    classroomTP.ClassroomCategory = classroomCategoryTP;
                    classroomBLO.Save(classroomTP);
                }
            }

            for (int i = 1; i <= 5; i++)
            {
                Classroom classroomCours = classroomBLO.FindBaseEntityByReference("Cours" + i);
                if (classroomCours == null)
                {
                    classroomCours = new Classroom();
                    classroomCours.Reference = "Cours" + i;
                    classroomCours.Code = "Cours" + i;
                    classroomCours.Name = "Cours" + i;
                    classroomCours.ClassroomCategory = classroomCategoryCours;
                    classroomBLO.Save(classroomCours);
                }
            }




        }
    }
}
