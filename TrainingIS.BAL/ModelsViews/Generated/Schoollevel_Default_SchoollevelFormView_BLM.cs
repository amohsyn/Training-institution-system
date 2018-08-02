using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
using GApp.Core.Utils;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_SchoollevelFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_SchoollevelFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Schoollevel ConverTo_Schoollevel(Default_SchoollevelFormView Default_SchoollevelFormView)
        {
			Schoollevel Schoollevel = null;
            if (Default_SchoollevelFormView.Id != 0)
            {
                Schoollevel = new SchoollevelBLO(this.UnitOfWork).FindBaseEntityByID(Default_SchoollevelFormView.Id);
            }
            else
            {
                Schoollevel = new Schoollevel();
            }
			Schoollevel.Code = Default_SchoollevelFormView.Code;
			Schoollevel.Name = Default_SchoollevelFormView.Name;
			Schoollevel.Description = Default_SchoollevelFormView.Description;
			Schoollevel.Id = Default_SchoollevelFormView.Id;
            return Schoollevel;
        }
        public virtual Default_SchoollevelFormView ConverTo_Default_SchoollevelFormView(Schoollevel Schoollevel)
        {  
            Default_SchoollevelFormView Default_SchoollevelFormView = new Default_SchoollevelFormView();
			Default_SchoollevelFormView.Code = Schoollevel.Code;
			Default_SchoollevelFormView.Name = Schoollevel.Name;
			Default_SchoollevelFormView.Description = Schoollevel.Description;
			Default_SchoollevelFormView.Id = Schoollevel.Id;
            return Default_SchoollevelFormView;            
        }
    }

	public partial class Default_SchoollevelFormViewBLM : BaseDefault_SchoollevelFormViewBLM
	{
		public Default_SchoollevelFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
