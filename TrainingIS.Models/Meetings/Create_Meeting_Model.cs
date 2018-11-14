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
using TrainingIS.Entities.Resources.MeetingResources;
using GApp.Entities.Resources.AppResources;
using TrainingIS.Models.Meetings;

namespace TrainingIS.Entities.ModelsViews
{
    [CreateView(typeof(Meeting))]
    public class Create_Meeting_Model : Form_Meeting_Model
    {
       



    }
}
