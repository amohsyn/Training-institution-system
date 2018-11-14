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
using GApp.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.WorkGroupResources;
using TrainingIS.Entities.Resources.Mission_Working_GroupResources;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.Models.WorkGroups
{
    /// <summary>
    /// Riason to Add a Specific Form_Model : 
    /// the Default_WorkGroup_Model not support Foreign Key Witout Foreign Key Id
    /// </summary>
    [EditView(typeof(WorkGroup))]
    public class Edit_WorkGroup_Model : Form_WorkGroup_Model
    {
       

   
     }
}
