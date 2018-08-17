﻿//modelType = FormerIndexView

using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.DAL;
using GApp.Core.Context;
using TrainingIS.Entities.ModelsViews.FormerModelsViews;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseFormerIndexViewBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseFormerIndexViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Former ConverTo_Former(FormerIndexView FormerIndexView)
        {
			Former Former = null;
            if (FormerIndexView.Id != 0)
            {
                Former = new FormerBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(FormerIndexView.Id);
            }
            else
            {
                Former = new Former();
            } 
			Former.RegistrationNumber = FormerIndexView.RegistrationNumber;
			Former.FirstName = FormerIndexView.FirstName;
			Former.LastName = FormerIndexView.LastName;
			Former.FormerSpecialty = FormerIndexView.FormerSpecialty;
			Former.Email = FormerIndexView.Email;
			Former.Id = FormerIndexView.Id;
            return Former;
        }
        public virtual FormerIndexView ConverTo_FormerIndexView(Former Former)
        {  
			FormerIndexView FormerIndexView = new FormerIndexView();
			FormerIndexView.toStringValue = Former.ToString();
			FormerIndexView.RegistrationNumber = Former.RegistrationNumber;
			FormerIndexView.FormerSpecialty = Former.FormerSpecialty;
			FormerIndexView.FirstName = Former.FirstName;
			FormerIndexView.LastName = Former.LastName;
			FormerIndexView.Email = Former.Email;
			FormerIndexView.Id = Former.Id;
            return FormerIndexView;            
        }

		public virtual FormerIndexView CreateNew()
        {
            Former Former = new FormerBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            FormerIndexView FormerIndexView = this.ConverTo_FormerIndexView(Former);
            return FormerIndexView;
        } 
    }

	public partial class FormerIndexViewBLM : BaseFormerIndexViewBLM
	{
		public FormerIndexViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
