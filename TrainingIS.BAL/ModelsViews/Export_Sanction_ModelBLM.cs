using GApp.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.enums;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Export_Sanction_ModelBLM
    {
        public override Export_Sanction_Model ConverTo_Export_Sanction_Model(Sanction Sanction)
        {
            AbsenceBLO absenceBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);

            var export_sanction = base.ConverTo_Export_Sanction_Model(Sanction);
            export_sanction.CEF = Sanction.Trainee.CIN;
            export_sanction.FirstName = Sanction.Trainee.FirstName;
            export_sanction.LastName = Sanction.Trainee.LastName;
            export_sanction.Group_Code = Sanction.Trainee.Group.Code;
            export_sanction.Specialty_Code = Sanction.Trainee.Specialty.Code;
            export_sanction.SanctionCategory_Code = Sanction.SanctionCategory.Name;
            export_sanction.Meeting_Code = Sanction.Meeting?.ToString();
            export_sanction.SanctionState_Code = GAppEnumLocalization.GetLocalValue(typeof(SanctionStates), Sanction.SanctionState.ToString());
            export_sanction.Absences_Count = absenceBLO.Count_NotJustified_Absences(Sanction.Trainee.Id);
            export_sanction.Valide_Note = Sanction.Trainee.AttendanceState.Valid_Note;
            export_sanction.InValide_Note = Sanction.Trainee.AttendanceState.Invalid_Note;






            return export_sanction;
        }
    }

}
