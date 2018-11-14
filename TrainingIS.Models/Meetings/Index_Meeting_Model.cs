using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.Models.Meetings
{
    [IndexView(typeof(Meeting))]
    public class Index_Meeting_Model : Details_Meeting_Model
    {
    }
}
