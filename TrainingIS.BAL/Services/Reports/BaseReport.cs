using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;

namespace TrainingIS.BLL.Reports
{
    public class BaseReport
    {
        protected TrainingISModel context;
        public BaseReport()
        {
            context = new TrainingISModel();
        }

    }
}
