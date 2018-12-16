using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.Resources.Category_JustificationAbsenceResources;

namespace TrainingIS.BLL
{
    public partial class Category_JustificationAbsenceBLO
    {
        public static string Absence_Sanction_Justification = "Absence_Sanction_Justification";

        public Category_JustificationAbsence Get_Absence_Sanction_Justification()
        {
            string Reference_of_Absence_Sanction_Justification = "Absence_Sanction_Justification";
            Category_JustificationAbsence category = this.FindBaseEntityByReference(Reference_of_Absence_Sanction_Justification);
            if(category == null)
            {
                category = this.CreateInstance();
                category.Reference = Reference_of_Absence_Sanction_Justification;
                category.Name = msg_Category_JustificationAbsence.Attendance_Sanction;
                this.Save(category);
            }
            return category;
        }
    }
}
