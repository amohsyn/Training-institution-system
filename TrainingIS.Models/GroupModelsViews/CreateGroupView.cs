using GApp.Core.Entities.ModelsViews;
using GApp.Entities.Resources.AppResources;
using GApp.Models;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.SpecialtyResources;
using TrainingIS.Entities.Resources.TrainingTypeResources;
using TrainingIS.Entities.Resources.TrainingYearResources;
using TrainingIS.Entities.Resources.YearStudyResources;

namespace TrainingIS.Entities.ModelsViews.GroupModelsViews
{
 
    [CreateView(typeof(Group))]
    public class CreateGroupView : Form_Group_Model
    {
       

    }
}
