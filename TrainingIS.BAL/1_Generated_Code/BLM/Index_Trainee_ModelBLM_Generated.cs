//modelType = Index_Trainee_Model

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
using TrainingIS.Models.Trainees;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseIndex_Trainee_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseIndex_Trainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Trainee ConverTo_Trainee(Index_Trainee_Model Index_Trainee_Model)
        {
			Trainee Trainee = null;
            if (Index_Trainee_Model.Id != 0)
            {
                Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Index_Trainee_Model.Id);
            }
            else
            {
                Trainee = new Trainee();
            } 
			if (!string.IsNullOrEmpty(Index_Trainee_Model.Photo_Reference))
            {
				if(Index_Trainee_Model.Photo_Reference == "Delete" && Trainee.Photo != null)
                {
                    Trainee.Photo.Old_Reference = Trainee.Photo.Reference;
                    Trainee.Photo.Reference = "Delete";
                }
                else
				{
					if (Trainee.Photo == null) Trainee.Photo = new GPicture() { Old_Reference = "Empty" };
					if (Trainee.Photo.Reference != Index_Trainee_Model.Photo_Reference)
					{
						// Save the old reference to be deleted by the save methode 
						if (!string.IsNullOrEmpty(Trainee.Photo.Reference))
                            Trainee.Photo.Old_Reference = Trainee.Photo.Reference;

						 

						GPictureBLO gPictureBLO = new GPictureBLO(this.UnitOfWork, this.GAppContext);
						Trainee.Photo.Reference = Index_Trainee_Model.Photo_Reference;
                  
						Trainee.Photo.Original_Thumbnail = gPictureBLO.Get_URL_Original_Picture_Path(Index_Trainee_Model.Photo_Reference);
						Trainee.Photo.Small_Thumbnail = gPictureBLO.Get_URL_Small_Picture_Path(Index_Trainee_Model.Photo_Reference);
						Trainee.Photo.Medium_Thumbnail = gPictureBLO.Get_URL_Medium_Picture_Path(Index_Trainee_Model.Photo_Reference);
						Trainee.Photo.Large_Thumbnail = gPictureBLO.Get_URL_Large_Picture_Path(Index_Trainee_Model.Photo_Reference);
					}
				}

               
            }
					Trainee.CNE = Index_Trainee_Model.CNE;
			Trainee.Group = Index_Trainee_Model.Group;
			Trainee.FirstName = Index_Trainee_Model.FirstName;
			Trainee.LastName = Index_Trainee_Model.LastName;
			Trainee.Id = Index_Trainee_Model.Id;
            return Trainee;
        }
        public virtual Index_Trainee_Model ConverTo_Index_Trainee_Model(Trainee Trainee)
        {  
			Index_Trainee_Model Index_Trainee_Model = new Index_Trainee_Model();
			Index_Trainee_Model.toStringValue = Trainee.ToString();
			Index_Trainee_Model.CNE = Trainee.CNE;
			Index_Trainee_Model.Group = Trainee.Group;
			Index_Trainee_Model.FirstName = Trainee.FirstName;
			Index_Trainee_Model.LastName = Trainee.LastName;
			Index_Trainee_Model.Photo = Trainee.Photo;
			Index_Trainee_Model.Id = Trainee.Id;
            return Index_Trainee_Model;            
        }

		public virtual Index_Trainee_Model CreateNew()
        {
            Trainee Trainee = new TraineeBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Index_Trainee_Model Index_Trainee_Model = this.ConverTo_Index_Trainee_Model(Trainee);
            return Index_Trainee_Model;
        } 

		public virtual List<Index_Trainee_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            TraineeBLO entityBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Trainee> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Index_Trainee_Model> ls_models = new List<Index_Trainee_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Index_Trainee_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Index_Trainee_ModelBLM : BaseIndex_Trainee_ModelBLM
	{
		public Index_Trainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
