using GApp.Core.Entities.ModelsViews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Models.DataAnnotations;
using GApp.Models;
using GApp.Entities;
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.Entities.Resources.AbsenceResources;
using TrainingIS.Entities.Resources.SeanceTrainingResources;
using TrainingIS.Entities;
using TrainingIS.Entities.Resources.SeancePlanningResources;
using System.ComponentModel;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.Models.Absences
{
    [EditView(typeof(Absence))]
    public class Edit_Absence_Model : Form_Absence_Model
    {
       
    }
}
