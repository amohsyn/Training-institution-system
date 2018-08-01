using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;

namespace TrainingIS.BLL.ModelsViews
{
    public class ViewModelBLM
    {
        public UnitOfWork UnitOfWork = new UnitOfWork();
        public ViewModelBLM(UnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }
    }
}
