using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_SchoollevelDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_SchoollevelDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Schoollevel ConverTo_Schoollevel(Default_SchoollevelDetailsView Default_SchoollevelDetailsView)
        {
			Schoollevel Schoollevel = new Schoollevel();
			Schoollevel.Code = Default_SchoollevelDetailsView.Code;
			Schoollevel.Name = Default_SchoollevelDetailsView.Name;
			Schoollevel.Description = Default_SchoollevelDetailsView.Description;
			Schoollevel.Id = Default_SchoollevelDetailsView.Id;
            return Schoollevel;

        }
        public virtual Default_SchoollevelDetailsView ConverTo_Default_SchoollevelDetailsView(Schoollevel Schoollevel)
        {
            Default_SchoollevelDetailsView Default_SchoollevelDetailsView = new Default_SchoollevelDetailsView();
			Default_SchoollevelDetailsView.Code = Schoollevel.Code;
			Default_SchoollevelDetailsView.Name = Schoollevel.Name;
			Default_SchoollevelDetailsView.Description = Schoollevel.Description;
			Default_SchoollevelDetailsView.Id = Schoollevel.Id;
            return Default_SchoollevelDetailsView;            
        }
    }

	public partial class Default_SchoollevelDetailsViewBLM : BaseDefault_SchoollevelDetailsViewBLM
	{
		public Default_SchoollevelDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
