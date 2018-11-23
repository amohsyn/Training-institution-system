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
using TrainingIS.Entities.enums;
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.Entities.Resources.SanctionCategoryResources;
using TrainingIS.Entities.Resources.SanctionResources;
using TrainingIS.Entities.Resources.MeetingResources;
using GApp.Entities.Resources.BaseEntity;


namespace TrainingIS.Entities.ModelsViews
{
    [EditView(typeof(Sanction))]
    public class Sanction_Edit_Model : Form_Sanction_Model
    {

    }
}
