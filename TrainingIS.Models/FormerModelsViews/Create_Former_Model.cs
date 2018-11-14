using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.Models.FormerModelsViews
{
    [CreateView(typeof(Former))]
    public class Create_Former_Model : FormerFormView
    {
    }
}
