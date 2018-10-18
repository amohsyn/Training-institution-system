  
 

 

 
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

						 

						GPictureBLO gPictureBLO = new GPictureBLO(this.GAppContext);
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
//modelType = Default_Details_Trainee_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_Trainee_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Trainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Trainee ConverTo_Trainee(Default_Details_Trainee_Model Default_Details_Trainee_Model)
        {
			Trainee Trainee = null;
            if (Default_Details_Trainee_Model.Id != 0)
            {
                Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Trainee_Model.Id);
            }
            else
            {
                Trainee = new Trainee();
            } 
			Trainee.CNE = Default_Details_Trainee_Model.CNE;
			Trainee.DateRegistration = Default_Details_Trainee_Model.DateRegistration;
			Trainee.isActif = Default_Details_Trainee_Model.isActif;
			Trainee.Schoollevel = Default_Details_Trainee_Model.Schoollevel;
			Trainee.Specialty = Default_Details_Trainee_Model.Specialty;
			Trainee.YearStudy = Default_Details_Trainee_Model.YearStudy;
			Trainee.Group = Default_Details_Trainee_Model.Group;
			Trainee.FirstName = Default_Details_Trainee_Model.FirstName;
			Trainee.LastName = Default_Details_Trainee_Model.LastName;
			Trainee.FirstNameArabe = Default_Details_Trainee_Model.FirstNameArabe;
			Trainee.LastNameArabe = Default_Details_Trainee_Model.LastNameArabe;
			Trainee.Sex = Default_Details_Trainee_Model.Sex;
			Trainee.Birthdate = DefaultDateTime_If_Empty(Default_Details_Trainee_Model.Birthdate);
			Trainee.Nationality = Default_Details_Trainee_Model.Nationality;
			Trainee.BirthPlace = Default_Details_Trainee_Model.BirthPlace;
			Trainee.CIN = Default_Details_Trainee_Model.CIN;
			if (!string.IsNullOrEmpty(Default_Details_Trainee_Model.Photo_Reference))
            {
				if(Default_Details_Trainee_Model.Photo_Reference == "Delete" && Trainee.Photo != null)
                {
                    Trainee.Photo.Old_Reference = Trainee.Photo.Reference;
                    Trainee.Photo.Reference = "Delete";
                }
                else
				{
					if (Trainee.Photo == null) Trainee.Photo = new GPicture() { Old_Reference = "Empty" };
					if (Trainee.Photo.Reference != Default_Details_Trainee_Model.Photo_Reference)
					{
						// Save the old reference to be deleted by the save methode 
						if (!string.IsNullOrEmpty(Trainee.Photo.Reference))
                            Trainee.Photo.Old_Reference = Trainee.Photo.Reference;

						 

						GPictureBLO gPictureBLO = new GPictureBLO(this.GAppContext);
						Trainee.Photo.Reference = Default_Details_Trainee_Model.Photo_Reference;
                  
						Trainee.Photo.Original_Thumbnail = gPictureBLO.Get_URL_Original_Picture_Path(Default_Details_Trainee_Model.Photo_Reference);
						Trainee.Photo.Small_Thumbnail = gPictureBLO.Get_URL_Small_Picture_Path(Default_Details_Trainee_Model.Photo_Reference);
						Trainee.Photo.Medium_Thumbnail = gPictureBLO.Get_URL_Medium_Picture_Path(Default_Details_Trainee_Model.Photo_Reference);
						Trainee.Photo.Large_Thumbnail = gPictureBLO.Get_URL_Large_Picture_Path(Default_Details_Trainee_Model.Photo_Reference);
					}
				}

               
            }
					Trainee.Cellphone = Default_Details_Trainee_Model.Cellphone;
			Trainee.Email = Default_Details_Trainee_Model.Email;
			Trainee.Address = Default_Details_Trainee_Model.Address;
			Trainee.FaceBook = Default_Details_Trainee_Model.FaceBook;
			Trainee.WebSite = Default_Details_Trainee_Model.WebSite;
			Trainee.Id = Default_Details_Trainee_Model.Id;
            return Trainee;
        }
        public virtual Default_Details_Trainee_Model ConverTo_Default_Details_Trainee_Model(Trainee Trainee)
        {  
			Default_Details_Trainee_Model Default_Details_Trainee_Model = new Default_Details_Trainee_Model();
			Default_Details_Trainee_Model.toStringValue = Trainee.ToString();
			Default_Details_Trainee_Model.CNE = Trainee.CNE;
			Default_Details_Trainee_Model.DateRegistration = ConversionUtil.DefaultValue_if_Null<DateTime>(Trainee.DateRegistration);
			Default_Details_Trainee_Model.isActif = Trainee.isActif;
			Default_Details_Trainee_Model.Schoollevel = Trainee.Schoollevel;
			Default_Details_Trainee_Model.Specialty = Trainee.Specialty;
			Default_Details_Trainee_Model.YearStudy = Trainee.YearStudy;
			Default_Details_Trainee_Model.Group = Trainee.Group;
			Default_Details_Trainee_Model.FirstName = Trainee.FirstName;
			Default_Details_Trainee_Model.LastName = Trainee.LastName;
			Default_Details_Trainee_Model.FirstNameArabe = Trainee.FirstNameArabe;
			Default_Details_Trainee_Model.LastNameArabe = Trainee.LastNameArabe;
			Default_Details_Trainee_Model.Sex = Trainee.Sex;
			Default_Details_Trainee_Model.Birthdate = DefaultDateTime_If_Empty(Trainee.Birthdate);
			Default_Details_Trainee_Model.Nationality = Trainee.Nationality;
			Default_Details_Trainee_Model.BirthPlace = Trainee.BirthPlace;
			Default_Details_Trainee_Model.CIN = Trainee.CIN;
			Default_Details_Trainee_Model.Photo = Trainee.Photo;
			Default_Details_Trainee_Model.Cellphone = Trainee.Cellphone;
			Default_Details_Trainee_Model.Email = Trainee.Email;
			Default_Details_Trainee_Model.Address = Trainee.Address;
			Default_Details_Trainee_Model.FaceBook = Trainee.FaceBook;
			Default_Details_Trainee_Model.WebSite = Trainee.WebSite;
			Default_Details_Trainee_Model.Id = Trainee.Id;
            return Default_Details_Trainee_Model;            
        }

		public virtual Default_Details_Trainee_Model CreateNew()
        {
            Trainee Trainee = new TraineeBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Trainee_Model Default_Details_Trainee_Model = this.ConverTo_Default_Details_Trainee_Model(Trainee);
            return Default_Details_Trainee_Model;
        } 

		public virtual List<Default_Details_Trainee_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            TraineeBLO entityBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Trainee> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Trainee_Model> ls_models = new List<Default_Details_Trainee_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Trainee_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Trainee_ModelBLM : BaseDefault_Details_Trainee_ModelBLM
	{
		public Default_Details_Trainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_Trainee_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_Trainee_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Trainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Trainee ConverTo_Trainee(Default_Form_Trainee_Model Default_Form_Trainee_Model)
        {
			Trainee Trainee = null;
            if (Default_Form_Trainee_Model.Id != 0)
            {
                Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Trainee_Model.Id);
            }
            else
            {
                Trainee = new Trainee();
            } 
			Trainee.CNE = Default_Form_Trainee_Model.CNE;
			Trainee.DateRegistration = Default_Form_Trainee_Model.DateRegistration;
			Trainee.isActif = Default_Form_Trainee_Model.isActif;
			Trainee.SchoollevelId = Default_Form_Trainee_Model.SchoollevelId;
			Trainee.Schoollevel = new SchoollevelBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Trainee_Model.SchoollevelId)) ;
			Trainee.SpecialtyId = Default_Form_Trainee_Model.SpecialtyId;
			Trainee.Specialty = new SpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Trainee_Model.SpecialtyId)) ;
			Trainee.YearStudyId = Default_Form_Trainee_Model.YearStudyId;
			Trainee.YearStudy = new YearStudyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Trainee_Model.YearStudyId)) ;
			Trainee.GroupId = Default_Form_Trainee_Model.GroupId;
			Trainee.Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Trainee_Model.GroupId)) ;
			Trainee.FirstName = Default_Form_Trainee_Model.FirstName;
			Trainee.LastName = Default_Form_Trainee_Model.LastName;
			Trainee.FirstNameArabe = Default_Form_Trainee_Model.FirstNameArabe;
			Trainee.LastNameArabe = Default_Form_Trainee_Model.LastNameArabe;
			Trainee.Sex = Default_Form_Trainee_Model.Sex;
			Trainee.Birthdate = DefaultDateTime_If_Empty(Default_Form_Trainee_Model.Birthdate);
			Trainee.NationalityId = Default_Form_Trainee_Model.NationalityId;
			Trainee.Nationality = new NationalityBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Trainee_Model.NationalityId)) ;
			Trainee.BirthPlace = Default_Form_Trainee_Model.BirthPlace;
			Trainee.CIN = Default_Form_Trainee_Model.CIN;
			if (!string.IsNullOrEmpty(Default_Form_Trainee_Model.Photo_Reference))
            {
				if(Default_Form_Trainee_Model.Photo_Reference == "Delete" && Trainee.Photo != null)
                {
                    Trainee.Photo.Old_Reference = Trainee.Photo.Reference;
                    Trainee.Photo.Reference = "Delete";
                }
                else
				{
					if (Trainee.Photo == null) Trainee.Photo = new GPicture() { Old_Reference = "Empty" };
					if (Trainee.Photo.Reference != Default_Form_Trainee_Model.Photo_Reference)
					{
						// Save the old reference to be deleted by the save methode 
						if (!string.IsNullOrEmpty(Trainee.Photo.Reference))
                            Trainee.Photo.Old_Reference = Trainee.Photo.Reference;

						 

						GPictureBLO gPictureBLO = new GPictureBLO(this.GAppContext);
						Trainee.Photo.Reference = Default_Form_Trainee_Model.Photo_Reference;
                  
						Trainee.Photo.Original_Thumbnail = gPictureBLO.Get_URL_Original_Picture_Path(Default_Form_Trainee_Model.Photo_Reference);
						Trainee.Photo.Small_Thumbnail = gPictureBLO.Get_URL_Small_Picture_Path(Default_Form_Trainee_Model.Photo_Reference);
						Trainee.Photo.Medium_Thumbnail = gPictureBLO.Get_URL_Medium_Picture_Path(Default_Form_Trainee_Model.Photo_Reference);
						Trainee.Photo.Large_Thumbnail = gPictureBLO.Get_URL_Large_Picture_Path(Default_Form_Trainee_Model.Photo_Reference);
					}
				}

               
            }
					Trainee.Cellphone = Default_Form_Trainee_Model.Cellphone;
			Trainee.Email = Default_Form_Trainee_Model.Email;
			Trainee.Address = Default_Form_Trainee_Model.Address;
			Trainee.FaceBook = Default_Form_Trainee_Model.FaceBook;
			Trainee.WebSite = Default_Form_Trainee_Model.WebSite;
			Trainee.Id = Default_Form_Trainee_Model.Id;
            return Trainee;
        }
        public virtual Default_Form_Trainee_Model ConverTo_Default_Form_Trainee_Model(Trainee Trainee)
        {  
			Default_Form_Trainee_Model Default_Form_Trainee_Model = new Default_Form_Trainee_Model();
			Default_Form_Trainee_Model.toStringValue = Trainee.ToString();
			Default_Form_Trainee_Model.CNE = Trainee.CNE;
			Default_Form_Trainee_Model.DateRegistration = ConversionUtil.DefaultValue_if_Null<DateTime>(Trainee.DateRegistration);
			Default_Form_Trainee_Model.isActif = Trainee.isActif;
			Default_Form_Trainee_Model.SchoollevelId = ConversionUtil.DefaultValue_if_Null<Int64>(Trainee.SchoollevelId);
			Default_Form_Trainee_Model.SpecialtyId = Trainee.SpecialtyId;
			Default_Form_Trainee_Model.YearStudyId = Trainee.YearStudyId;
			Default_Form_Trainee_Model.GroupId = Trainee.GroupId;
			Default_Form_Trainee_Model.FirstName = Trainee.FirstName;
			Default_Form_Trainee_Model.LastName = Trainee.LastName;
			Default_Form_Trainee_Model.FirstNameArabe = Trainee.FirstNameArabe;
			Default_Form_Trainee_Model.LastNameArabe = Trainee.LastNameArabe;
			Default_Form_Trainee_Model.Sex = Trainee.Sex;
			Default_Form_Trainee_Model.Birthdate = DefaultDateTime_If_Empty(Trainee.Birthdate);
			Default_Form_Trainee_Model.NationalityId = Trainee.NationalityId;
			Default_Form_Trainee_Model.BirthPlace = Trainee.BirthPlace;
			Default_Form_Trainee_Model.CIN = Trainee.CIN;
			Default_Form_Trainee_Model.Photo = Trainee.Photo;
			Default_Form_Trainee_Model.Cellphone = Trainee.Cellphone;
			Default_Form_Trainee_Model.Email = Trainee.Email;
			Default_Form_Trainee_Model.Address = Trainee.Address;
			Default_Form_Trainee_Model.FaceBook = Trainee.FaceBook;
			Default_Form_Trainee_Model.WebSite = Trainee.WebSite;
			Default_Form_Trainee_Model.Id = Trainee.Id;
            return Default_Form_Trainee_Model;            
        }

		public virtual Default_Form_Trainee_Model CreateNew()
        {
            Trainee Trainee = new TraineeBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Trainee_Model Default_Form_Trainee_Model = this.ConverTo_Default_Form_Trainee_Model(Trainee);
            return Default_Form_Trainee_Model;
        } 

		public virtual List<Default_Form_Trainee_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            TraineeBLO entityBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Trainee> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_Trainee_Model> ls_models = new List<Default_Form_Trainee_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_Trainee_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_Trainee_ModelBLM : BaseDefault_Form_Trainee_ModelBLM
	{
		public Default_Form_Trainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Create_SeanceTraining_Model

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
using TrainingIS.Models.SeanceTrainings;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseCreate_SeanceTraining_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseCreate_SeanceTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SeanceTraining ConverTo_SeanceTraining(Create_SeanceTraining_Model Create_SeanceTraining_Model)
        {
			SeanceTraining SeanceTraining = null;
            if (Create_SeanceTraining_Model.Id != 0)
            {
                SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Create_SeanceTraining_Model.Id);
            }
            else
            {
                SeanceTraining = new SeanceTraining();
            } 
			SeanceTraining.SeanceDate = Create_SeanceTraining_Model.SeanceDate;
			SeanceTraining.SeancePlanningId = Create_SeanceTraining_Model.SeancePlanningId;
			SeanceTraining.SeancePlanning = new SeancePlanningBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Create_SeanceTraining_Model.SeancePlanningId)) ;
			SeanceTraining.Contained = Create_SeanceTraining_Model.Contained;
			SeanceTraining.Id = Create_SeanceTraining_Model.Id;
            return SeanceTraining;
        }
        public virtual Create_SeanceTraining_Model ConverTo_Create_SeanceTraining_Model(SeanceTraining SeanceTraining)
        {  
			Create_SeanceTraining_Model Create_SeanceTraining_Model = new Create_SeanceTraining_Model();
			Create_SeanceTraining_Model.toStringValue = SeanceTraining.ToString();
			Create_SeanceTraining_Model.SeanceDate = ConversionUtil.DefaultValue_if_Null<DateTime>(SeanceTraining.SeanceDate);
			Create_SeanceTraining_Model.SeancePlanningId = SeanceTraining.SeancePlanningId;
			Create_SeanceTraining_Model.Contained = SeanceTraining.Contained;
			Create_SeanceTraining_Model.Id = SeanceTraining.Id;
            return Create_SeanceTraining_Model;            
        }

		public virtual Create_SeanceTraining_Model CreateNew()
        {
            SeanceTraining SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Create_SeanceTraining_Model Create_SeanceTraining_Model = this.ConverTo_Create_SeanceTraining_Model(SeanceTraining);
            return Create_SeanceTraining_Model;
        } 

		public virtual List<Create_SeanceTraining_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SeanceTrainingBLO entityBLO = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<SeanceTraining> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Create_SeanceTraining_Model> ls_models = new List<Create_SeanceTraining_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Create_SeanceTraining_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Create_SeanceTraining_ModelBLM : BaseCreate_SeanceTraining_ModelBLM
	{
		public Create_SeanceTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Index_SeanceTraining_Model

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
using TrainingIS.Models.SeanceTrainings;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseIndex_SeanceTraining_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseIndex_SeanceTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SeanceTraining ConverTo_SeanceTraining(Index_SeanceTraining_Model Index_SeanceTraining_Model)
        {
			SeanceTraining SeanceTraining = null;
            if (Index_SeanceTraining_Model.Id != 0)
            {
                SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Index_SeanceTraining_Model.Id);
            }
            else
            {
                SeanceTraining = new SeanceTraining();
            } 
			SeanceTraining.SeanceDate = Index_SeanceTraining_Model.SeanceDate;
			SeanceTraining.Contained = Index_SeanceTraining_Model.Contained;
			SeanceTraining.FormerValidation = Index_SeanceTraining_Model.FormerValidation;
			SeanceTraining.Absences = Index_SeanceTraining_Model.Absences;
			SeanceTraining.Id = Index_SeanceTraining_Model.Id;
            return SeanceTraining;
        }
        public virtual Index_SeanceTraining_Model ConverTo_Index_SeanceTraining_Model(SeanceTraining SeanceTraining)
        {  
			Index_SeanceTraining_Model Index_SeanceTraining_Model = new Index_SeanceTraining_Model();
			Index_SeanceTraining_Model.toStringValue = SeanceTraining.ToString();
			Index_SeanceTraining_Model.SeanceDate = ConversionUtil.DefaultValue_if_Null<DateTime>(SeanceTraining.SeanceDate);
			Index_SeanceTraining_Model.Contained = SeanceTraining.Contained;
			Index_SeanceTraining_Model.FormerValidation = SeanceTraining.FormerValidation;
			Index_SeanceTraining_Model.Absences = SeanceTraining.Absences;
			Index_SeanceTraining_Model.Id = SeanceTraining.Id;
            return Index_SeanceTraining_Model;            
        }

		public virtual Index_SeanceTraining_Model CreateNew()
        {
            SeanceTraining SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Index_SeanceTraining_Model Index_SeanceTraining_Model = this.ConverTo_Index_SeanceTraining_Model(SeanceTraining);
            return Index_SeanceTraining_Model;
        } 

		public virtual List<Index_SeanceTraining_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SeanceTrainingBLO entityBLO = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<SeanceTraining> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Index_SeanceTraining_Model> ls_models = new List<Index_SeanceTraining_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Index_SeanceTraining_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Index_SeanceTraining_ModelBLM : BaseIndex_SeanceTraining_ModelBLM
	{
		public Index_SeanceTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_SeanceTraining_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_SeanceTraining_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_SeanceTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SeanceTraining ConverTo_SeanceTraining(Default_Details_SeanceTraining_Model Default_Details_SeanceTraining_Model)
        {
			SeanceTraining SeanceTraining = null;
            if (Default_Details_SeanceTraining_Model.Id != 0)
            {
                SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_SeanceTraining_Model.Id);
            }
            else
            {
                SeanceTraining = new SeanceTraining();
            } 
			SeanceTraining.SeanceDate = Default_Details_SeanceTraining_Model.SeanceDate;
			SeanceTraining.SeancePlanning = Default_Details_SeanceTraining_Model.SeancePlanning;
			SeanceTraining.Contained = Default_Details_SeanceTraining_Model.Contained;
			SeanceTraining.FormerValidation = Default_Details_SeanceTraining_Model.FormerValidation;
			SeanceTraining.Absences = Default_Details_SeanceTraining_Model.Absences;
			SeanceTraining.Id = Default_Details_SeanceTraining_Model.Id;
            return SeanceTraining;
        }
        public virtual Default_Details_SeanceTraining_Model ConverTo_Default_Details_SeanceTraining_Model(SeanceTraining SeanceTraining)
        {  
			Default_Details_SeanceTraining_Model Default_Details_SeanceTraining_Model = new Default_Details_SeanceTraining_Model();
			Default_Details_SeanceTraining_Model.toStringValue = SeanceTraining.ToString();
			Default_Details_SeanceTraining_Model.SeanceDate = ConversionUtil.DefaultValue_if_Null<DateTime>(SeanceTraining.SeanceDate);
			Default_Details_SeanceTraining_Model.SeancePlanning = SeanceTraining.SeancePlanning;
			Default_Details_SeanceTraining_Model.Contained = SeanceTraining.Contained;
			Default_Details_SeanceTraining_Model.FormerValidation = SeanceTraining.FormerValidation;
			Default_Details_SeanceTraining_Model.Absences = SeanceTraining.Absences;
			Default_Details_SeanceTraining_Model.Id = SeanceTraining.Id;
            return Default_Details_SeanceTraining_Model;            
        }

		public virtual Default_Details_SeanceTraining_Model CreateNew()
        {
            SeanceTraining SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_SeanceTraining_Model Default_Details_SeanceTraining_Model = this.ConverTo_Default_Details_SeanceTraining_Model(SeanceTraining);
            return Default_Details_SeanceTraining_Model;
        } 

		public virtual List<Default_Details_SeanceTraining_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SeanceTrainingBLO entityBLO = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<SeanceTraining> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_SeanceTraining_Model> ls_models = new List<Default_Details_SeanceTraining_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_SeanceTraining_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_SeanceTraining_ModelBLM : BaseDefault_Details_SeanceTraining_ModelBLM
	{
		public Default_Details_SeanceTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_SeanceTraining_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_SeanceTraining_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_SeanceTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SeanceTraining ConverTo_SeanceTraining(Default_Form_SeanceTraining_Model Default_Form_SeanceTraining_Model)
        {
			SeanceTraining SeanceTraining = null;
            if (Default_Form_SeanceTraining_Model.Id != 0)
            {
                SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_SeanceTraining_Model.Id);
            }
            else
            {
                SeanceTraining = new SeanceTraining();
            } 
			SeanceTraining.SeanceDate = Default_Form_SeanceTraining_Model.SeanceDate;
			SeanceTraining.SeancePlanningId = Default_Form_SeanceTraining_Model.SeancePlanningId;
			SeanceTraining.SeancePlanning = new SeancePlanningBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_SeanceTraining_Model.SeancePlanningId)) ;
			SeanceTraining.Contained = Default_Form_SeanceTraining_Model.Contained;
			SeanceTraining.FormerValidation = Default_Form_SeanceTraining_Model.FormerValidation;
			// Absence
            AbsenceBLO AbsenceBLO = new AbsenceBLO(this.UnitOfWork,this.GAppContext);

			if (SeanceTraining.Absences != null)
                SeanceTraining.Absences.Clear();
            else
                SeanceTraining.Absences = new List<Absence>();

			if(Default_Form_SeanceTraining_Model.Selected_Absences != null)
			{
				foreach (string Selected_Absence_Id in Default_Form_SeanceTraining_Model.Selected_Absences)
				{
					Int64 Selected_Absence_Id_Int64 = Convert.ToInt64(Selected_Absence_Id);
					Absence Absence =AbsenceBLO.FindBaseEntityByID(Selected_Absence_Id_Int64);
					SeanceTraining.Absences.Add(Absence);
				}
			}
	
			SeanceTraining.Id = Default_Form_SeanceTraining_Model.Id;
            return SeanceTraining;
        }
        public virtual Default_Form_SeanceTraining_Model ConverTo_Default_Form_SeanceTraining_Model(SeanceTraining SeanceTraining)
        {  
			Default_Form_SeanceTraining_Model Default_Form_SeanceTraining_Model = new Default_Form_SeanceTraining_Model();
			Default_Form_SeanceTraining_Model.toStringValue = SeanceTraining.ToString();
			Default_Form_SeanceTraining_Model.SeanceDate = ConversionUtil.DefaultValue_if_Null<DateTime>(SeanceTraining.SeanceDate);
			Default_Form_SeanceTraining_Model.SeancePlanningId = SeanceTraining.SeancePlanningId;
			Default_Form_SeanceTraining_Model.Contained = SeanceTraining.Contained;
			Default_Form_SeanceTraining_Model.FormerValidation = SeanceTraining.FormerValidation;

			// Absence
            if (SeanceTraining.Absences != null && SeanceTraining.Absences.Count > 0)
            {
                Default_Form_SeanceTraining_Model.Selected_Absences = SeanceTraining
                                                        .Absences
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_Form_SeanceTraining_Model.Selected_Absences = new List<string>();
            }			
			Default_Form_SeanceTraining_Model.Id = SeanceTraining.Id;
            return Default_Form_SeanceTraining_Model;            
        }

		public virtual Default_Form_SeanceTraining_Model CreateNew()
        {
            SeanceTraining SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_SeanceTraining_Model Default_Form_SeanceTraining_Model = this.ConverTo_Default_Form_SeanceTraining_Model(SeanceTraining);
            return Default_Form_SeanceTraining_Model;
        } 

		public virtual List<Default_Form_SeanceTraining_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SeanceTrainingBLO entityBLO = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<SeanceTraining> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_SeanceTraining_Model> ls_models = new List<Default_Form_SeanceTraining_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_SeanceTraining_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_SeanceTraining_ModelBLM : BaseDefault_Form_SeanceTraining_ModelBLM
	{
		public Default_Form_SeanceTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Index_ModuleTraining_Model

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
using TrainingIS.Models.ModuleTrainings;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseIndex_ModuleTraining_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseIndex_ModuleTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ModuleTraining ConverTo_ModuleTraining(Index_ModuleTraining_Model Index_ModuleTraining_Model)
        {
			ModuleTraining ModuleTraining = null;
            if (Index_ModuleTraining_Model.Id != 0)
            {
                ModuleTraining = new ModuleTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Index_ModuleTraining_Model.Id);
            }
            else
            {
                ModuleTraining = new ModuleTraining();
            } 
			ModuleTraining.Specialty = Index_ModuleTraining_Model.Specialty;
			ModuleTraining.Metier = Index_ModuleTraining_Model.Metier;
			ModuleTraining.YearStudy = Index_ModuleTraining_Model.YearStudy;
			ModuleTraining.Name = Index_ModuleTraining_Model.Name;
			ModuleTraining.Code = Index_ModuleTraining_Model.Code;
			ModuleTraining.HourlyMass = Index_ModuleTraining_Model.HourlyMass;
			ModuleTraining.Id = Index_ModuleTraining_Model.Id;
            return ModuleTraining;
        }
        public virtual Index_ModuleTraining_Model ConverTo_Index_ModuleTraining_Model(ModuleTraining ModuleTraining)
        {  
			Index_ModuleTraining_Model Index_ModuleTraining_Model = new Index_ModuleTraining_Model();
			Index_ModuleTraining_Model.toStringValue = ModuleTraining.ToString();
			Index_ModuleTraining_Model.Specialty = ModuleTraining.Specialty;
			Index_ModuleTraining_Model.Metier = ModuleTraining.Metier;
			Index_ModuleTraining_Model.YearStudy = ModuleTraining.YearStudy;
			Index_ModuleTraining_Model.Name = ModuleTraining.Name;
			Index_ModuleTraining_Model.Code = ModuleTraining.Code;
			Index_ModuleTraining_Model.HourlyMass = ModuleTraining.HourlyMass;
			Index_ModuleTraining_Model.Id = ModuleTraining.Id;
            return Index_ModuleTraining_Model;            
        }

		public virtual Index_ModuleTraining_Model CreateNew()
        {
            ModuleTraining ModuleTraining = new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Index_ModuleTraining_Model Index_ModuleTraining_Model = this.ConverTo_Index_ModuleTraining_Model(ModuleTraining);
            return Index_ModuleTraining_Model;
        } 

		public virtual List<Index_ModuleTraining_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ModuleTrainingBLO entityBLO = new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ModuleTraining> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Index_ModuleTraining_Model> ls_models = new List<Index_ModuleTraining_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Index_ModuleTraining_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Index_ModuleTraining_ModelBLM : BaseIndex_ModuleTraining_ModelBLM
	{
		public Index_ModuleTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_ModuleTraining_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_ModuleTraining_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_ModuleTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ModuleTraining ConverTo_ModuleTraining(Default_Details_ModuleTraining_Model Default_Details_ModuleTraining_Model)
        {
			ModuleTraining ModuleTraining = null;
            if (Default_Details_ModuleTraining_Model.Id != 0)
            {
                ModuleTraining = new ModuleTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_ModuleTraining_Model.Id);
            }
            else
            {
                ModuleTraining = new ModuleTraining();
            } 
			ModuleTraining.Specialty = Default_Details_ModuleTraining_Model.Specialty;
			ModuleTraining.Metier = Default_Details_ModuleTraining_Model.Metier;
			ModuleTraining.YearStudy = Default_Details_ModuleTraining_Model.YearStudy;
			ModuleTraining.Name = Default_Details_ModuleTraining_Model.Name;
			ModuleTraining.Code = Default_Details_ModuleTraining_Model.Code;
			ModuleTraining.HourlyMass = Default_Details_ModuleTraining_Model.HourlyMass;
			ModuleTraining.Hourly_Mass_To_Teach = Default_Details_ModuleTraining_Model.Hourly_Mass_To_Teach;
			ModuleTraining.Description = Default_Details_ModuleTraining_Model.Description;
			ModuleTraining.Id = Default_Details_ModuleTraining_Model.Id;
            return ModuleTraining;
        }
        public virtual Default_Details_ModuleTraining_Model ConverTo_Default_Details_ModuleTraining_Model(ModuleTraining ModuleTraining)
        {  
			Default_Details_ModuleTraining_Model Default_Details_ModuleTraining_Model = new Default_Details_ModuleTraining_Model();
			Default_Details_ModuleTraining_Model.toStringValue = ModuleTraining.ToString();
			Default_Details_ModuleTraining_Model.Specialty = ModuleTraining.Specialty;
			Default_Details_ModuleTraining_Model.Metier = ModuleTraining.Metier;
			Default_Details_ModuleTraining_Model.YearStudy = ModuleTraining.YearStudy;
			Default_Details_ModuleTraining_Model.Name = ModuleTraining.Name;
			Default_Details_ModuleTraining_Model.Code = ModuleTraining.Code;
			Default_Details_ModuleTraining_Model.HourlyMass = ModuleTraining.HourlyMass;
			Default_Details_ModuleTraining_Model.Hourly_Mass_To_Teach = ModuleTraining.Hourly_Mass_To_Teach;
			Default_Details_ModuleTraining_Model.Description = ModuleTraining.Description;
			Default_Details_ModuleTraining_Model.Id = ModuleTraining.Id;
            return Default_Details_ModuleTraining_Model;            
        }

		public virtual Default_Details_ModuleTraining_Model CreateNew()
        {
            ModuleTraining ModuleTraining = new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_ModuleTraining_Model Default_Details_ModuleTraining_Model = this.ConverTo_Default_Details_ModuleTraining_Model(ModuleTraining);
            return Default_Details_ModuleTraining_Model;
        } 

		public virtual List<Default_Details_ModuleTraining_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ModuleTrainingBLO entityBLO = new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ModuleTraining> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_ModuleTraining_Model> ls_models = new List<Default_Details_ModuleTraining_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_ModuleTraining_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_ModuleTraining_ModelBLM : BaseDefault_Details_ModuleTraining_ModelBLM
	{
		public Default_Details_ModuleTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_ModuleTraining_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_ModuleTraining_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_ModuleTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ModuleTraining ConverTo_ModuleTraining(Default_Form_ModuleTraining_Model Default_Form_ModuleTraining_Model)
        {
			ModuleTraining ModuleTraining = null;
            if (Default_Form_ModuleTraining_Model.Id != 0)
            {
                ModuleTraining = new ModuleTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_ModuleTraining_Model.Id);
            }
            else
            {
                ModuleTraining = new ModuleTraining();
            } 
			ModuleTraining.SpecialtyId = Default_Form_ModuleTraining_Model.SpecialtyId;
			ModuleTraining.Specialty = new SpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_ModuleTraining_Model.SpecialtyId)) ;
			ModuleTraining.MetierId = Default_Form_ModuleTraining_Model.MetierId;
			ModuleTraining.Metier = new MetierBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_ModuleTraining_Model.MetierId)) ;
			ModuleTraining.YearStudyId = Default_Form_ModuleTraining_Model.YearStudyId;
			ModuleTraining.YearStudy = new YearStudyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_ModuleTraining_Model.YearStudyId)) ;
			ModuleTraining.Name = Default_Form_ModuleTraining_Model.Name;
			ModuleTraining.Code = Default_Form_ModuleTraining_Model.Code;
			ModuleTraining.HourlyMass = Default_Form_ModuleTraining_Model.HourlyMass;
			ModuleTraining.Hourly_Mass_To_Teach = Default_Form_ModuleTraining_Model.Hourly_Mass_To_Teach;
			ModuleTraining.Description = Default_Form_ModuleTraining_Model.Description;
			ModuleTraining.Id = Default_Form_ModuleTraining_Model.Id;
            return ModuleTraining;
        }
        public virtual Default_Form_ModuleTraining_Model ConverTo_Default_Form_ModuleTraining_Model(ModuleTraining ModuleTraining)
        {  
			Default_Form_ModuleTraining_Model Default_Form_ModuleTraining_Model = new Default_Form_ModuleTraining_Model();
			Default_Form_ModuleTraining_Model.toStringValue = ModuleTraining.ToString();
			Default_Form_ModuleTraining_Model.SpecialtyId = ModuleTraining.SpecialtyId;
			Default_Form_ModuleTraining_Model.MetierId = ModuleTraining.MetierId;
			Default_Form_ModuleTraining_Model.YearStudyId = ModuleTraining.YearStudyId;
			Default_Form_ModuleTraining_Model.Name = ModuleTraining.Name;
			Default_Form_ModuleTraining_Model.Code = ModuleTraining.Code;
			Default_Form_ModuleTraining_Model.HourlyMass = ModuleTraining.HourlyMass;
			Default_Form_ModuleTraining_Model.Hourly_Mass_To_Teach = ModuleTraining.Hourly_Mass_To_Teach;
			Default_Form_ModuleTraining_Model.Description = ModuleTraining.Description;
			Default_Form_ModuleTraining_Model.Id = ModuleTraining.Id;
            return Default_Form_ModuleTraining_Model;            
        }

		public virtual Default_Form_ModuleTraining_Model CreateNew()
        {
            ModuleTraining ModuleTraining = new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_ModuleTraining_Model Default_Form_ModuleTraining_Model = this.ConverTo_Default_Form_ModuleTraining_Model(ModuleTraining);
            return Default_Form_ModuleTraining_Model;
        } 

		public virtual List<Default_Form_ModuleTraining_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ModuleTrainingBLO entityBLO = new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ModuleTraining> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_ModuleTraining_Model> ls_models = new List<Default_Form_ModuleTraining_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_ModuleTraining_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_ModuleTraining_ModelBLM : BaseDefault_Form_ModuleTraining_ModelBLM
	{
		public Default_Form_ModuleTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Create_Absence_Model

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
using TrainingIS.Models.Absences;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseCreate_Absence_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseCreate_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Absence ConverTo_Absence(Create_Absence_Model Create_Absence_Model)
        {
			Absence Absence = null;
            if (Create_Absence_Model.Id != 0)
            {
                Absence = new AbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Create_Absence_Model.Id);
            }
            else
            {
                Absence = new Absence();
            } 
			Absence.TraineeId = Create_Absence_Model.TraineeId;
			Absence.Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Create_Absence_Model.TraineeId)) ;
			Absence.isHaveAuthorization = Create_Absence_Model.isHaveAuthorization;
			Absence.SeanceTrainingId = Create_Absence_Model.SeanceTrainingId;
			Absence.SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Create_Absence_Model.SeanceTrainingId)) ;
			Absence.FormerComment = Create_Absence_Model.FormerComment;
			Absence.TraineeComment = Create_Absence_Model.TraineeComment;
			Absence.SupervisorComment = Create_Absence_Model.SupervisorComment;
			Absence.JustificationAbsence = Create_Absence_Model.JustificationAbsence;
			Absence.Id = Create_Absence_Model.Id;
            return Absence;
        }
        public virtual Create_Absence_Model ConverTo_Create_Absence_Model(Absence Absence)
        {  
			Create_Absence_Model Create_Absence_Model = new Create_Absence_Model();
			Create_Absence_Model.toStringValue = Absence.ToString();
			Create_Absence_Model.SeanceTrainingId = Absence.SeanceTrainingId;
			Create_Absence_Model.TraineeId = Absence.TraineeId;
			Create_Absence_Model.isHaveAuthorization = Absence.isHaveAuthorization;
			Create_Absence_Model.FormerComment = Absence.FormerComment;
			Create_Absence_Model.TraineeComment = Absence.TraineeComment;
			Create_Absence_Model.SupervisorComment = Absence.SupervisorComment;
			Create_Absence_Model.JustificationAbsence = Absence.JustificationAbsence;
			Create_Absence_Model.Id = Absence.Id;
            return Create_Absence_Model;            
        }

		public virtual Create_Absence_Model CreateNew()
        {
            Absence Absence = new AbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Create_Absence_Model Create_Absence_Model = this.ConverTo_Create_Absence_Model(Absence);
            return Create_Absence_Model;
        } 

		public virtual List<Create_Absence_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            AbsenceBLO entityBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Absence> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Create_Absence_Model> ls_models = new List<Create_Absence_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Create_Absence_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Create_Absence_ModelBLM : BaseCreate_Absence_ModelBLM
	{
		public Create_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Details_Absence_Model

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
using TrainingIS.Models.Absences;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDetails_Absence_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDetails_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Absence ConverTo_Absence(Details_Absence_Model Details_Absence_Model)
        {
			Absence Absence = null;
            if (Details_Absence_Model.Id != 0)
            {
                Absence = new AbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Details_Absence_Model.Id);
            }
            else
            {
                Absence = new Absence();
            } 
			Absence.Trainee = Details_Absence_Model.Trainee;
			Absence.isHaveAuthorization = Details_Absence_Model.isHaveAuthorization;
			Absence.SeanceTraining = Details_Absence_Model.SeanceTraining;
			Absence.FormerComment = Details_Absence_Model.FormerComment;
			Absence.TraineeComment = Details_Absence_Model.TraineeComment;
			Absence.SupervisorComment = Details_Absence_Model.SupervisorComment;
			Absence.Id = Details_Absence_Model.Id;
            return Absence;
        }
        public virtual Details_Absence_Model ConverTo_Details_Absence_Model(Absence Absence)
        {  
			Details_Absence_Model Details_Absence_Model = new Details_Absence_Model();
			Details_Absence_Model.toStringValue = Absence.ToString();
			Details_Absence_Model.SeanceTraining = Absence.SeanceTraining;
			Details_Absence_Model.Trainee = Absence.Trainee;
			Details_Absence_Model.isHaveAuthorization = Absence.isHaveAuthorization;
			Details_Absence_Model.FormerComment = Absence.FormerComment;
			Details_Absence_Model.TraineeComment = Absence.TraineeComment;
			Details_Absence_Model.SupervisorComment = Absence.SupervisorComment;
			Details_Absence_Model.Id = Absence.Id;
            return Details_Absence_Model;            
        }

		public virtual Details_Absence_Model CreateNew()
        {
            Absence Absence = new AbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Details_Absence_Model Details_Absence_Model = this.ConverTo_Details_Absence_Model(Absence);
            return Details_Absence_Model;
        } 

		public virtual List<Details_Absence_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            AbsenceBLO entityBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Absence> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Details_Absence_Model> ls_models = new List<Details_Absence_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Details_Absence_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Details_Absence_ModelBLM : BaseDetails_Absence_ModelBLM
	{
		public Details_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Edit_Absence_Model

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
using TrainingIS.Models.Absences;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseEdit_Absence_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseEdit_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Absence ConverTo_Absence(Edit_Absence_Model Edit_Absence_Model)
        {
			Absence Absence = null;
            if (Edit_Absence_Model.Id != 0)
            {
                Absence = new AbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Edit_Absence_Model.Id);
            }
            else
            {
                Absence = new Absence();
            } 
			Absence.TraineeId = Edit_Absence_Model.TraineeId;
			Absence.Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Edit_Absence_Model.TraineeId)) ;
			Absence.isHaveAuthorization = Edit_Absence_Model.isHaveAuthorization;
			Absence.SeanceTraining = Edit_Absence_Model.SeanceTraining;
			Absence.SeanceTrainingId = Edit_Absence_Model.SeanceTrainingId;
			Absence.SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Edit_Absence_Model.SeanceTrainingId)) ;
			Absence.FormerComment = Edit_Absence_Model.FormerComment;
			Absence.TraineeComment = Edit_Absence_Model.TraineeComment;
			Absence.SupervisorComment = Edit_Absence_Model.SupervisorComment;
			Absence.JustificationAbsence = Edit_Absence_Model.JustificationAbsence;
			Absence.Id = Edit_Absence_Model.Id;
            return Absence;
        }
        public virtual Edit_Absence_Model ConverTo_Edit_Absence_Model(Absence Absence)
        {  
			Edit_Absence_Model Edit_Absence_Model = new Edit_Absence_Model();
			Edit_Absence_Model.toStringValue = Absence.ToString();
			Edit_Absence_Model.SeanceTraining = Absence.SeanceTraining;
			Edit_Absence_Model.SeanceTrainingId = Absence.SeanceTrainingId;
			Edit_Absence_Model.TraineeId = Absence.TraineeId;
			Edit_Absence_Model.isHaveAuthorization = Absence.isHaveAuthorization;
			Edit_Absence_Model.FormerComment = Absence.FormerComment;
			Edit_Absence_Model.TraineeComment = Absence.TraineeComment;
			Edit_Absence_Model.SupervisorComment = Absence.SupervisorComment;
			Edit_Absence_Model.JustificationAbsence = Absence.JustificationAbsence;
			Edit_Absence_Model.Id = Absence.Id;
            return Edit_Absence_Model;            
        }

		public virtual Edit_Absence_Model CreateNew()
        {
            Absence Absence = new AbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Edit_Absence_Model Edit_Absence_Model = this.ConverTo_Edit_Absence_Model(Absence);
            return Edit_Absence_Model;
        } 

		public virtual List<Edit_Absence_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            AbsenceBLO entityBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Absence> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Edit_Absence_Model> ls_models = new List<Edit_Absence_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Edit_Absence_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Edit_Absence_ModelBLM : BaseEdit_Absence_ModelBLM
	{
		public Edit_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Index_Absence_Model

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
using TrainingIS.Models.Absences;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseIndex_Absence_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseIndex_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Absence ConverTo_Absence(Index_Absence_Model Index_Absence_Model)
        {
			Absence Absence = null;
            if (Index_Absence_Model.Id != 0)
            {
                Absence = new AbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Index_Absence_Model.Id);
            }
            else
            {
                Absence = new Absence();
            } 
			Absence.AbsenceDate = DefaultDateTime_If_Empty(Index_Absence_Model.AbsenceDate);
			Absence.Trainee = Index_Absence_Model.Trainee;
			Absence.isHaveAuthorization = Index_Absence_Model.isHaveAuthorization;
			Absence.SeanceTraining = Index_Absence_Model.SeanceTraining;
			Absence.Valide = Index_Absence_Model.Valide;
			Absence.Id = Index_Absence_Model.Id;
            return Absence;
        }
        public virtual Index_Absence_Model ConverTo_Index_Absence_Model(Absence Absence)
        {  
			Index_Absence_Model Index_Absence_Model = new Index_Absence_Model();
			Index_Absence_Model.toStringValue = Absence.ToString();
			Index_Absence_Model.AbsenceDate = DefaultDateTime_If_Empty(Absence.AbsenceDate);
			Index_Absence_Model.SeanceTraining = Absence.SeanceTraining;
			Index_Absence_Model.Trainee = Absence.Trainee;
			Index_Absence_Model.isHaveAuthorization = Absence.isHaveAuthorization;
			Index_Absence_Model.Valide = Absence.Valide;
			Index_Absence_Model.Id = Absence.Id;
            return Index_Absence_Model;            
        }

		public virtual Index_Absence_Model CreateNew()
        {
            Absence Absence = new AbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Index_Absence_Model Index_Absence_Model = this.ConverTo_Index_Absence_Model(Absence);
            return Index_Absence_Model;
        } 

		public virtual List<Index_Absence_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            AbsenceBLO entityBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Absence> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Index_Absence_Model> ls_models = new List<Index_Absence_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Index_Absence_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Index_Absence_ModelBLM : BaseIndex_Absence_ModelBLM
	{
		public Index_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_Absence_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_Absence_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Absence ConverTo_Absence(Default_Details_Absence_Model Default_Details_Absence_Model)
        {
			Absence Absence = null;
            if (Default_Details_Absence_Model.Id != 0)
            {
                Absence = new AbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Absence_Model.Id);
            }
            else
            {
                Absence = new Absence();
            } 
			Absence.AbsenceDate = DefaultDateTime_If_Empty(Default_Details_Absence_Model.AbsenceDate);
			Absence.SeanceTraining = Default_Details_Absence_Model.SeanceTraining;
			Absence.Trainee = Default_Details_Absence_Model.Trainee;
			Absence.isHaveAuthorization = Default_Details_Absence_Model.isHaveAuthorization;
			Absence.FormerComment = Default_Details_Absence_Model.FormerComment;
			Absence.TraineeComment = Default_Details_Absence_Model.TraineeComment;
			Absence.SupervisorComment = Default_Details_Absence_Model.SupervisorComment;
			Absence.Valide = Default_Details_Absence_Model.Valide;
			Absence.Id = Default_Details_Absence_Model.Id;
            return Absence;
        }
        public virtual Default_Details_Absence_Model ConverTo_Default_Details_Absence_Model(Absence Absence)
        {  
			Default_Details_Absence_Model Default_Details_Absence_Model = new Default_Details_Absence_Model();
			Default_Details_Absence_Model.toStringValue = Absence.ToString();
			Default_Details_Absence_Model.AbsenceDate = DefaultDateTime_If_Empty(Absence.AbsenceDate);
			Default_Details_Absence_Model.SeanceTraining = Absence.SeanceTraining;
			Default_Details_Absence_Model.Trainee = Absence.Trainee;
			Default_Details_Absence_Model.isHaveAuthorization = Absence.isHaveAuthorization;
			Default_Details_Absence_Model.FormerComment = Absence.FormerComment;
			Default_Details_Absence_Model.TraineeComment = Absence.TraineeComment;
			Default_Details_Absence_Model.SupervisorComment = Absence.SupervisorComment;
			Default_Details_Absence_Model.Valide = Absence.Valide;
			Default_Details_Absence_Model.Id = Absence.Id;
            return Default_Details_Absence_Model;            
        }

		public virtual Default_Details_Absence_Model CreateNew()
        {
            Absence Absence = new AbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Absence_Model Default_Details_Absence_Model = this.ConverTo_Default_Details_Absence_Model(Absence);
            return Default_Details_Absence_Model;
        } 

		public virtual List<Default_Details_Absence_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            AbsenceBLO entityBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Absence> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Absence_Model> ls_models = new List<Default_Details_Absence_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Absence_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Absence_ModelBLM : BaseDefault_Details_Absence_ModelBLM
	{
		public Default_Details_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_Absence_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_Absence_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Absence ConverTo_Absence(Default_Form_Absence_Model Default_Form_Absence_Model)
        {
			Absence Absence = null;
            if (Default_Form_Absence_Model.Id != 0)
            {
                Absence = new AbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Absence_Model.Id);
            }
            else
            {
                Absence = new Absence();
            } 
			Absence.AbsenceDate = DefaultDateTime_If_Empty(Default_Form_Absence_Model.AbsenceDate);
			Absence.SeanceTrainingId = Default_Form_Absence_Model.SeanceTrainingId;
			Absence.SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Absence_Model.SeanceTrainingId)) ;
			Absence.TraineeId = Default_Form_Absence_Model.TraineeId;
			Absence.Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Absence_Model.TraineeId)) ;
			Absence.isHaveAuthorization = Default_Form_Absence_Model.isHaveAuthorization;
			Absence.FormerComment = Default_Form_Absence_Model.FormerComment;
			Absence.TraineeComment = Default_Form_Absence_Model.TraineeComment;
			Absence.SupervisorComment = Default_Form_Absence_Model.SupervisorComment;
			Absence.Valide = Default_Form_Absence_Model.Valide;
			Absence.Id = Default_Form_Absence_Model.Id;
            return Absence;
        }
        public virtual Default_Form_Absence_Model ConverTo_Default_Form_Absence_Model(Absence Absence)
        {  
			Default_Form_Absence_Model Default_Form_Absence_Model = new Default_Form_Absence_Model();
			Default_Form_Absence_Model.toStringValue = Absence.ToString();
			Default_Form_Absence_Model.AbsenceDate = DefaultDateTime_If_Empty(Absence.AbsenceDate);
			Default_Form_Absence_Model.SeanceTrainingId = Absence.SeanceTrainingId;
			Default_Form_Absence_Model.TraineeId = Absence.TraineeId;
			Default_Form_Absence_Model.isHaveAuthorization = Absence.isHaveAuthorization;
			Default_Form_Absence_Model.FormerComment = Absence.FormerComment;
			Default_Form_Absence_Model.TraineeComment = Absence.TraineeComment;
			Default_Form_Absence_Model.SupervisorComment = Absence.SupervisorComment;
			Default_Form_Absence_Model.Valide = Absence.Valide;
			Default_Form_Absence_Model.Id = Absence.Id;
            return Default_Form_Absence_Model;            
        }

		public virtual Default_Form_Absence_Model CreateNew()
        {
            Absence Absence = new AbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Absence_Model Default_Form_Absence_Model = this.ConverTo_Default_Form_Absence_Model(Absence);
            return Default_Form_Absence_Model;
        } 

		public virtual List<Default_Form_Absence_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            AbsenceBLO entityBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Absence> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_Absence_Model> ls_models = new List<Default_Form_Absence_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_Absence_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_Absence_ModelBLM : BaseDefault_Form_Absence_ModelBLM
	{
		public Default_Form_Absence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_ActionControllerApp_Model

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
using TrainingIS.Entities.ModelsViews;
using GApp.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_ActionControllerApp_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_ActionControllerApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ActionControllerApp ConverTo_ActionControllerApp(Default_Details_ActionControllerApp_Model Default_Details_ActionControllerApp_Model)
        {
			ActionControllerApp ActionControllerApp = null;
            if (Default_Details_ActionControllerApp_Model.Id != 0)
            {
                ActionControllerApp = new ActionControllerAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_ActionControllerApp_Model.Id);
            }
            else
            {
                ActionControllerApp = new ActionControllerApp();
            } 
			ActionControllerApp.Code = Default_Details_ActionControllerApp_Model.Code;
			ActionControllerApp.Name = Default_Details_ActionControllerApp_Model.Name;
			ActionControllerApp.Description = Default_Details_ActionControllerApp_Model.Description;
			ActionControllerApp.ControllerApp = Default_Details_ActionControllerApp_Model.ControllerApp;
			ActionControllerApp.Id = Default_Details_ActionControllerApp_Model.Id;
            return ActionControllerApp;
        }
        public virtual Default_Details_ActionControllerApp_Model ConverTo_Default_Details_ActionControllerApp_Model(ActionControllerApp ActionControllerApp)
        {  
			Default_Details_ActionControllerApp_Model Default_Details_ActionControllerApp_Model = new Default_Details_ActionControllerApp_Model();
			Default_Details_ActionControllerApp_Model.toStringValue = ActionControllerApp.ToString();
			Default_Details_ActionControllerApp_Model.Code = ActionControllerApp.Code;
			Default_Details_ActionControllerApp_Model.Name = ActionControllerApp.Name;
			Default_Details_ActionControllerApp_Model.Description = ActionControllerApp.Description;
			Default_Details_ActionControllerApp_Model.ControllerApp = ActionControllerApp.ControllerApp;
			Default_Details_ActionControllerApp_Model.Id = ActionControllerApp.Id;
            return Default_Details_ActionControllerApp_Model;            
        }

		public virtual Default_Details_ActionControllerApp_Model CreateNew()
        {
            ActionControllerApp ActionControllerApp = new ActionControllerAppBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_ActionControllerApp_Model Default_Details_ActionControllerApp_Model = this.ConverTo_Default_Details_ActionControllerApp_Model(ActionControllerApp);
            return Default_Details_ActionControllerApp_Model;
        } 

		public virtual List<Default_Details_ActionControllerApp_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ActionControllerAppBLO entityBLO = new ActionControllerAppBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ActionControllerApp> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_ActionControllerApp_Model> ls_models = new List<Default_Details_ActionControllerApp_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_ActionControllerApp_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_ActionControllerApp_ModelBLM : BaseDefault_Details_ActionControllerApp_ModelBLM
	{
		public Default_Details_ActionControllerApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_ActionControllerApp_Model

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
using TrainingIS.Entities.ModelsViews;
using GApp.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_ActionControllerApp_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_ActionControllerApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ActionControllerApp ConverTo_ActionControllerApp(Default_Form_ActionControllerApp_Model Default_Form_ActionControllerApp_Model)
        {
			ActionControllerApp ActionControllerApp = null;
            if (Default_Form_ActionControllerApp_Model.Id != 0)
            {
                ActionControllerApp = new ActionControllerAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_ActionControllerApp_Model.Id);
            }
            else
            {
                ActionControllerApp = new ActionControllerApp();
            } 
			ActionControllerApp.Code = Default_Form_ActionControllerApp_Model.Code;
			ActionControllerApp.Name = Default_Form_ActionControllerApp_Model.Name;
			ActionControllerApp.Description = Default_Form_ActionControllerApp_Model.Description;
			ActionControllerApp.ControllerAppId = Default_Form_ActionControllerApp_Model.ControllerAppId;
			ActionControllerApp.ControllerApp = new ControllerAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_ActionControllerApp_Model.ControllerAppId)) ;
			ActionControllerApp.Id = Default_Form_ActionControllerApp_Model.Id;
            return ActionControllerApp;
        }
        public virtual Default_Form_ActionControllerApp_Model ConverTo_Default_Form_ActionControllerApp_Model(ActionControllerApp ActionControllerApp)
        {  
			Default_Form_ActionControllerApp_Model Default_Form_ActionControllerApp_Model = new Default_Form_ActionControllerApp_Model();
			Default_Form_ActionControllerApp_Model.toStringValue = ActionControllerApp.ToString();
			Default_Form_ActionControllerApp_Model.Code = ActionControllerApp.Code;
			Default_Form_ActionControllerApp_Model.Name = ActionControllerApp.Name;
			Default_Form_ActionControllerApp_Model.Description = ActionControllerApp.Description;
			Default_Form_ActionControllerApp_Model.ControllerAppId = ActionControllerApp.ControllerAppId;
			Default_Form_ActionControllerApp_Model.Id = ActionControllerApp.Id;
            return Default_Form_ActionControllerApp_Model;            
        }

		public virtual Default_Form_ActionControllerApp_Model CreateNew()
        {
            ActionControllerApp ActionControllerApp = new ActionControllerAppBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_ActionControllerApp_Model Default_Form_ActionControllerApp_Model = this.ConverTo_Default_Form_ActionControllerApp_Model(ActionControllerApp);
            return Default_Form_ActionControllerApp_Model;
        } 

		public virtual List<Default_Form_ActionControllerApp_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ActionControllerAppBLO entityBLO = new ActionControllerAppBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ActionControllerApp> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_ActionControllerApp_Model> ls_models = new List<Default_Form_ActionControllerApp_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_ActionControllerApp_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_ActionControllerApp_ModelBLM : BaseDefault_Form_ActionControllerApp_ModelBLM
	{
		public Default_Form_ActionControllerApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_Administrator_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_Administrator_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Administrator_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Administrator ConverTo_Administrator(Default_Details_Administrator_Model Default_Details_Administrator_Model)
        {
			Administrator Administrator = null;
            if (Default_Details_Administrator_Model.Id != 0)
            {
                Administrator = new AdministratorBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Administrator_Model.Id);
            }
            else
            {
                Administrator = new Administrator();
            } 
			Administrator.RegistrationNumber = Default_Details_Administrator_Model.RegistrationNumber;
			Administrator.CreateUserAccount = Default_Details_Administrator_Model.CreateUserAccount;
			Administrator.Login = Default_Details_Administrator_Model.Login;
			Administrator.Password = Default_Details_Administrator_Model.Password;
			Administrator.FirstName = Default_Details_Administrator_Model.FirstName;
			Administrator.LastName = Default_Details_Administrator_Model.LastName;
			Administrator.FirstNameArabe = Default_Details_Administrator_Model.FirstNameArabe;
			Administrator.LastNameArabe = Default_Details_Administrator_Model.LastNameArabe;
			Administrator.Sex = Default_Details_Administrator_Model.Sex;
			Administrator.Birthdate = DefaultDateTime_If_Empty(Default_Details_Administrator_Model.Birthdate);
			Administrator.Nationality = Default_Details_Administrator_Model.Nationality;
			Administrator.BirthPlace = Default_Details_Administrator_Model.BirthPlace;
			Administrator.CIN = Default_Details_Administrator_Model.CIN;
			if (!string.IsNullOrEmpty(Default_Details_Administrator_Model.Photo_Reference))
            {
				if(Default_Details_Administrator_Model.Photo_Reference == "Delete" && Administrator.Photo != null)
                {
                    Administrator.Photo.Old_Reference = Administrator.Photo.Reference;
                    Administrator.Photo.Reference = "Delete";
                }
                else
				{
					if (Administrator.Photo == null) Administrator.Photo = new GPicture() { Old_Reference = "Empty" };
					if (Administrator.Photo.Reference != Default_Details_Administrator_Model.Photo_Reference)
					{
						// Save the old reference to be deleted by the save methode 
						if (!string.IsNullOrEmpty(Administrator.Photo.Reference))
                            Administrator.Photo.Old_Reference = Administrator.Photo.Reference;

						 

						GPictureBLO gPictureBLO = new GPictureBLO(this.GAppContext);
						Administrator.Photo.Reference = Default_Details_Administrator_Model.Photo_Reference;
                  
						Administrator.Photo.Original_Thumbnail = gPictureBLO.Get_URL_Original_Picture_Path(Default_Details_Administrator_Model.Photo_Reference);
						Administrator.Photo.Small_Thumbnail = gPictureBLO.Get_URL_Small_Picture_Path(Default_Details_Administrator_Model.Photo_Reference);
						Administrator.Photo.Medium_Thumbnail = gPictureBLO.Get_URL_Medium_Picture_Path(Default_Details_Administrator_Model.Photo_Reference);
						Administrator.Photo.Large_Thumbnail = gPictureBLO.Get_URL_Large_Picture_Path(Default_Details_Administrator_Model.Photo_Reference);
					}
				}

               
            }
					Administrator.Cellphone = Default_Details_Administrator_Model.Cellphone;
			Administrator.Email = Default_Details_Administrator_Model.Email;
			Administrator.Address = Default_Details_Administrator_Model.Address;
			Administrator.FaceBook = Default_Details_Administrator_Model.FaceBook;
			Administrator.WebSite = Default_Details_Administrator_Model.WebSite;
			Administrator.Id = Default_Details_Administrator_Model.Id;
            return Administrator;
        }
        public virtual Default_Details_Administrator_Model ConverTo_Default_Details_Administrator_Model(Administrator Administrator)
        {  
			Default_Details_Administrator_Model Default_Details_Administrator_Model = new Default_Details_Administrator_Model();
			Default_Details_Administrator_Model.toStringValue = Administrator.ToString();
			Default_Details_Administrator_Model.RegistrationNumber = Administrator.RegistrationNumber;
			Default_Details_Administrator_Model.CreateUserAccount = Administrator.CreateUserAccount;
			Default_Details_Administrator_Model.Login = Administrator.Login;
			Default_Details_Administrator_Model.Password = Administrator.Password;
			Default_Details_Administrator_Model.FirstName = Administrator.FirstName;
			Default_Details_Administrator_Model.LastName = Administrator.LastName;
			Default_Details_Administrator_Model.FirstNameArabe = Administrator.FirstNameArabe;
			Default_Details_Administrator_Model.LastNameArabe = Administrator.LastNameArabe;
			Default_Details_Administrator_Model.Sex = Administrator.Sex;
			Default_Details_Administrator_Model.Birthdate = DefaultDateTime_If_Empty(Administrator.Birthdate);
			Default_Details_Administrator_Model.Nationality = Administrator.Nationality;
			Default_Details_Administrator_Model.BirthPlace = Administrator.BirthPlace;
			Default_Details_Administrator_Model.CIN = Administrator.CIN;
			Default_Details_Administrator_Model.Photo = Administrator.Photo;
			Default_Details_Administrator_Model.Cellphone = Administrator.Cellphone;
			Default_Details_Administrator_Model.Email = Administrator.Email;
			Default_Details_Administrator_Model.Address = Administrator.Address;
			Default_Details_Administrator_Model.FaceBook = Administrator.FaceBook;
			Default_Details_Administrator_Model.WebSite = Administrator.WebSite;
			Default_Details_Administrator_Model.Id = Administrator.Id;
            return Default_Details_Administrator_Model;            
        }

		public virtual Default_Details_Administrator_Model CreateNew()
        {
            Administrator Administrator = new AdministratorBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Administrator_Model Default_Details_Administrator_Model = this.ConverTo_Default_Details_Administrator_Model(Administrator);
            return Default_Details_Administrator_Model;
        } 

		public virtual List<Default_Details_Administrator_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            AdministratorBLO entityBLO = new AdministratorBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Administrator> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Administrator_Model> ls_models = new List<Default_Details_Administrator_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Administrator_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Administrator_ModelBLM : BaseDefault_Details_Administrator_ModelBLM
	{
		public Default_Details_Administrator_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_Administrator_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_Administrator_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Administrator_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Administrator ConverTo_Administrator(Default_Form_Administrator_Model Default_Form_Administrator_Model)
        {
			Administrator Administrator = null;
            if (Default_Form_Administrator_Model.Id != 0)
            {
                Administrator = new AdministratorBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Administrator_Model.Id);
            }
            else
            {
                Administrator = new Administrator();
            } 
			Administrator.RegistrationNumber = Default_Form_Administrator_Model.RegistrationNumber;
			Administrator.CreateUserAccount = Default_Form_Administrator_Model.CreateUserAccount;
			Administrator.Login = Default_Form_Administrator_Model.Login;
			Administrator.Password = Default_Form_Administrator_Model.Password;
			Administrator.FirstName = Default_Form_Administrator_Model.FirstName;
			Administrator.LastName = Default_Form_Administrator_Model.LastName;
			Administrator.FirstNameArabe = Default_Form_Administrator_Model.FirstNameArabe;
			Administrator.LastNameArabe = Default_Form_Administrator_Model.LastNameArabe;
			Administrator.Sex = Default_Form_Administrator_Model.Sex;
			Administrator.Birthdate = DefaultDateTime_If_Empty(Default_Form_Administrator_Model.Birthdate);
			Administrator.NationalityId = Default_Form_Administrator_Model.NationalityId;
			Administrator.Nationality = new NationalityBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Administrator_Model.NationalityId)) ;
			Administrator.BirthPlace = Default_Form_Administrator_Model.BirthPlace;
			Administrator.CIN = Default_Form_Administrator_Model.CIN;
			if (!string.IsNullOrEmpty(Default_Form_Administrator_Model.Photo_Reference))
            {
				if(Default_Form_Administrator_Model.Photo_Reference == "Delete" && Administrator.Photo != null)
                {
                    Administrator.Photo.Old_Reference = Administrator.Photo.Reference;
                    Administrator.Photo.Reference = "Delete";
                }
                else
				{
					if (Administrator.Photo == null) Administrator.Photo = new GPicture() { Old_Reference = "Empty" };
					if (Administrator.Photo.Reference != Default_Form_Administrator_Model.Photo_Reference)
					{
						// Save the old reference to be deleted by the save methode 
						if (!string.IsNullOrEmpty(Administrator.Photo.Reference))
                            Administrator.Photo.Old_Reference = Administrator.Photo.Reference;

						 

						GPictureBLO gPictureBLO = new GPictureBLO(this.GAppContext);
						Administrator.Photo.Reference = Default_Form_Administrator_Model.Photo_Reference;
                  
						Administrator.Photo.Original_Thumbnail = gPictureBLO.Get_URL_Original_Picture_Path(Default_Form_Administrator_Model.Photo_Reference);
						Administrator.Photo.Small_Thumbnail = gPictureBLO.Get_URL_Small_Picture_Path(Default_Form_Administrator_Model.Photo_Reference);
						Administrator.Photo.Medium_Thumbnail = gPictureBLO.Get_URL_Medium_Picture_Path(Default_Form_Administrator_Model.Photo_Reference);
						Administrator.Photo.Large_Thumbnail = gPictureBLO.Get_URL_Large_Picture_Path(Default_Form_Administrator_Model.Photo_Reference);
					}
				}

               
            }
					Administrator.Cellphone = Default_Form_Administrator_Model.Cellphone;
			Administrator.Email = Default_Form_Administrator_Model.Email;
			Administrator.Address = Default_Form_Administrator_Model.Address;
			Administrator.FaceBook = Default_Form_Administrator_Model.FaceBook;
			Administrator.WebSite = Default_Form_Administrator_Model.WebSite;
			Administrator.Id = Default_Form_Administrator_Model.Id;
            return Administrator;
        }
        public virtual Default_Form_Administrator_Model ConverTo_Default_Form_Administrator_Model(Administrator Administrator)
        {  
			Default_Form_Administrator_Model Default_Form_Administrator_Model = new Default_Form_Administrator_Model();
			Default_Form_Administrator_Model.toStringValue = Administrator.ToString();
			Default_Form_Administrator_Model.RegistrationNumber = Administrator.RegistrationNumber;
			Default_Form_Administrator_Model.CreateUserAccount = Administrator.CreateUserAccount;
			Default_Form_Administrator_Model.Login = Administrator.Login;
			Default_Form_Administrator_Model.Password = Administrator.Password;
			Default_Form_Administrator_Model.FirstName = Administrator.FirstName;
			Default_Form_Administrator_Model.LastName = Administrator.LastName;
			Default_Form_Administrator_Model.FirstNameArabe = Administrator.FirstNameArabe;
			Default_Form_Administrator_Model.LastNameArabe = Administrator.LastNameArabe;
			Default_Form_Administrator_Model.Sex = Administrator.Sex;
			Default_Form_Administrator_Model.Birthdate = DefaultDateTime_If_Empty(Administrator.Birthdate);
			Default_Form_Administrator_Model.NationalityId = Administrator.NationalityId;
			Default_Form_Administrator_Model.BirthPlace = Administrator.BirthPlace;
			Default_Form_Administrator_Model.CIN = Administrator.CIN;
			Default_Form_Administrator_Model.Photo = Administrator.Photo;
			Default_Form_Administrator_Model.Cellphone = Administrator.Cellphone;
			Default_Form_Administrator_Model.Email = Administrator.Email;
			Default_Form_Administrator_Model.Address = Administrator.Address;
			Default_Form_Administrator_Model.FaceBook = Administrator.FaceBook;
			Default_Form_Administrator_Model.WebSite = Administrator.WebSite;
			Default_Form_Administrator_Model.Id = Administrator.Id;
            return Default_Form_Administrator_Model;            
        }

		public virtual Default_Form_Administrator_Model CreateNew()
        {
            Administrator Administrator = new AdministratorBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Administrator_Model Default_Form_Administrator_Model = this.ConverTo_Default_Form_Administrator_Model(Administrator);
            return Default_Form_Administrator_Model;
        } 

		public virtual List<Default_Form_Administrator_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            AdministratorBLO entityBLO = new AdministratorBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Administrator> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_Administrator_Model> ls_models = new List<Default_Form_Administrator_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_Administrator_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_Administrator_ModelBLM : BaseDefault_Form_Administrator_ModelBLM
	{
		public Default_Form_Administrator_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_ApplicationParam_Model

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
using TrainingIS.Entities.ModelsViews;
using GApp.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_ApplicationParam_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_ApplicationParam_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ApplicationParam ConverTo_ApplicationParam(Default_Details_ApplicationParam_Model Default_Details_ApplicationParam_Model)
        {
			ApplicationParam ApplicationParam = null;
            if (Default_Details_ApplicationParam_Model.Id != 0)
            {
                ApplicationParam = new ApplicationParamBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_ApplicationParam_Model.Id);
            }
            else
            {
                ApplicationParam = new ApplicationParam();
            } 
			ApplicationParam.Code = Default_Details_ApplicationParam_Model.Code;
			ApplicationParam.Name = Default_Details_ApplicationParam_Model.Name;
			ApplicationParam.Value = Default_Details_ApplicationParam_Model.Value;
			ApplicationParam.Description = Default_Details_ApplicationParam_Model.Description;
			ApplicationParam.Id = Default_Details_ApplicationParam_Model.Id;
            return ApplicationParam;
        }
        public virtual Default_Details_ApplicationParam_Model ConverTo_Default_Details_ApplicationParam_Model(ApplicationParam ApplicationParam)
        {  
			Default_Details_ApplicationParam_Model Default_Details_ApplicationParam_Model = new Default_Details_ApplicationParam_Model();
			Default_Details_ApplicationParam_Model.toStringValue = ApplicationParam.ToString();
			Default_Details_ApplicationParam_Model.Code = ApplicationParam.Code;
			Default_Details_ApplicationParam_Model.Name = ApplicationParam.Name;
			Default_Details_ApplicationParam_Model.Value = ApplicationParam.Value;
			Default_Details_ApplicationParam_Model.Description = ApplicationParam.Description;
			Default_Details_ApplicationParam_Model.Id = ApplicationParam.Id;
            return Default_Details_ApplicationParam_Model;            
        }

		public virtual Default_Details_ApplicationParam_Model CreateNew()
        {
            ApplicationParam ApplicationParam = new ApplicationParamBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_ApplicationParam_Model Default_Details_ApplicationParam_Model = this.ConverTo_Default_Details_ApplicationParam_Model(ApplicationParam);
            return Default_Details_ApplicationParam_Model;
        } 

		public virtual List<Default_Details_ApplicationParam_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ApplicationParamBLO entityBLO = new ApplicationParamBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ApplicationParam> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_ApplicationParam_Model> ls_models = new List<Default_Details_ApplicationParam_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_ApplicationParam_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_ApplicationParam_ModelBLM : BaseDefault_Details_ApplicationParam_ModelBLM
	{
		public Default_Details_ApplicationParam_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_ApplicationParam_Model

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
using TrainingIS.Entities.ModelsViews;
using GApp.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_ApplicationParam_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_ApplicationParam_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ApplicationParam ConverTo_ApplicationParam(Default_Form_ApplicationParam_Model Default_Form_ApplicationParam_Model)
        {
			ApplicationParam ApplicationParam = null;
            if (Default_Form_ApplicationParam_Model.Id != 0)
            {
                ApplicationParam = new ApplicationParamBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_ApplicationParam_Model.Id);
            }
            else
            {
                ApplicationParam = new ApplicationParam();
            } 
			ApplicationParam.Code = Default_Form_ApplicationParam_Model.Code;
			ApplicationParam.Name = Default_Form_ApplicationParam_Model.Name;
			ApplicationParam.Value = Default_Form_ApplicationParam_Model.Value;
			ApplicationParam.Description = Default_Form_ApplicationParam_Model.Description;
			ApplicationParam.Id = Default_Form_ApplicationParam_Model.Id;
            return ApplicationParam;
        }
        public virtual Default_Form_ApplicationParam_Model ConverTo_Default_Form_ApplicationParam_Model(ApplicationParam ApplicationParam)
        {  
			Default_Form_ApplicationParam_Model Default_Form_ApplicationParam_Model = new Default_Form_ApplicationParam_Model();
			Default_Form_ApplicationParam_Model.toStringValue = ApplicationParam.ToString();
			Default_Form_ApplicationParam_Model.Code = ApplicationParam.Code;
			Default_Form_ApplicationParam_Model.Name = ApplicationParam.Name;
			Default_Form_ApplicationParam_Model.Value = ApplicationParam.Value;
			Default_Form_ApplicationParam_Model.Description = ApplicationParam.Description;
			Default_Form_ApplicationParam_Model.Id = ApplicationParam.Id;
            return Default_Form_ApplicationParam_Model;            
        }

		public virtual Default_Form_ApplicationParam_Model CreateNew()
        {
            ApplicationParam ApplicationParam = new ApplicationParamBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_ApplicationParam_Model Default_Form_ApplicationParam_Model = this.ConverTo_Default_Form_ApplicationParam_Model(ApplicationParam);
            return Default_Form_ApplicationParam_Model;
        } 

		public virtual List<Default_Form_ApplicationParam_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ApplicationParamBLO entityBLO = new ApplicationParamBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ApplicationParam> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_ApplicationParam_Model> ls_models = new List<Default_Form_ApplicationParam_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_ApplicationParam_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_ApplicationParam_ModelBLM : BaseDefault_Form_ApplicationParam_ModelBLM
	{
		public Default_Form_ApplicationParam_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_AuthrorizationApp_Model

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
using TrainingIS.Entities.ModelsViews;
using GApp.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_AuthrorizationApp_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_AuthrorizationApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual AuthrorizationApp ConverTo_AuthrorizationApp(Default_Details_AuthrorizationApp_Model Default_Details_AuthrorizationApp_Model)
        {
			AuthrorizationApp AuthrorizationApp = null;
            if (Default_Details_AuthrorizationApp_Model.Id != 0)
            {
                AuthrorizationApp = new AuthrorizationAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_AuthrorizationApp_Model.Id);
            }
            else
            {
                AuthrorizationApp = new AuthrorizationApp();
            } 
			AuthrorizationApp.RoleApp = Default_Details_AuthrorizationApp_Model.RoleApp;
			AuthrorizationApp.ControllerApp = Default_Details_AuthrorizationApp_Model.ControllerApp;
			AuthrorizationApp.isAllAction = Default_Details_AuthrorizationApp_Model.isAllAction;
			AuthrorizationApp.ActionControllerApps = Default_Details_AuthrorizationApp_Model.ActionControllerApps;
			AuthrorizationApp.Id = Default_Details_AuthrorizationApp_Model.Id;
            return AuthrorizationApp;
        }
        public virtual Default_Details_AuthrorizationApp_Model ConverTo_Default_Details_AuthrorizationApp_Model(AuthrorizationApp AuthrorizationApp)
        {  
			Default_Details_AuthrorizationApp_Model Default_Details_AuthrorizationApp_Model = new Default_Details_AuthrorizationApp_Model();
			Default_Details_AuthrorizationApp_Model.toStringValue = AuthrorizationApp.ToString();
			Default_Details_AuthrorizationApp_Model.RoleApp = AuthrorizationApp.RoleApp;
			Default_Details_AuthrorizationApp_Model.ControllerApp = AuthrorizationApp.ControllerApp;
			Default_Details_AuthrorizationApp_Model.isAllAction = AuthrorizationApp.isAllAction;
			Default_Details_AuthrorizationApp_Model.ActionControllerApps = AuthrorizationApp.ActionControllerApps;
			Default_Details_AuthrorizationApp_Model.Id = AuthrorizationApp.Id;
            return Default_Details_AuthrorizationApp_Model;            
        }

		public virtual Default_Details_AuthrorizationApp_Model CreateNew()
        {
            AuthrorizationApp AuthrorizationApp = new AuthrorizationAppBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_AuthrorizationApp_Model Default_Details_AuthrorizationApp_Model = this.ConverTo_Default_Details_AuthrorizationApp_Model(AuthrorizationApp);
            return Default_Details_AuthrorizationApp_Model;
        } 

		public virtual List<Default_Details_AuthrorizationApp_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            AuthrorizationAppBLO entityBLO = new AuthrorizationAppBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<AuthrorizationApp> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_AuthrorizationApp_Model> ls_models = new List<Default_Details_AuthrorizationApp_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_AuthrorizationApp_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_AuthrorizationApp_ModelBLM : BaseDefault_Details_AuthrorizationApp_ModelBLM
	{
		public Default_Details_AuthrorizationApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_AuthrorizationApp_Model

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
using TrainingIS.Entities.ModelsViews;
using GApp.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_AuthrorizationApp_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_AuthrorizationApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual AuthrorizationApp ConverTo_AuthrorizationApp(Default_Form_AuthrorizationApp_Model Default_Form_AuthrorizationApp_Model)
        {
			AuthrorizationApp AuthrorizationApp = null;
            if (Default_Form_AuthrorizationApp_Model.Id != 0)
            {
                AuthrorizationApp = new AuthrorizationAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_AuthrorizationApp_Model.Id);
            }
            else
            {
                AuthrorizationApp = new AuthrorizationApp();
            } 
			AuthrorizationApp.RoleAppId = Default_Form_AuthrorizationApp_Model.RoleAppId;
			AuthrorizationApp.RoleApp = new RoleAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_AuthrorizationApp_Model.RoleAppId)) ;
			AuthrorizationApp.ControllerAppId = Default_Form_AuthrorizationApp_Model.ControllerAppId;
			AuthrorizationApp.ControllerApp = new ControllerAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_AuthrorizationApp_Model.ControllerAppId)) ;
			AuthrorizationApp.isAllAction = Default_Form_AuthrorizationApp_Model.isAllAction;
			// ActionControllerApp
            ActionControllerAppBLO ActionControllerAppBLO = new ActionControllerAppBLO(this.UnitOfWork,this.GAppContext);

			if (AuthrorizationApp.ActionControllerApps != null)
                AuthrorizationApp.ActionControllerApps.Clear();
            else
                AuthrorizationApp.ActionControllerApps = new List<ActionControllerApp>();

			if(Default_Form_AuthrorizationApp_Model.Selected_ActionControllerApps != null)
			{
				foreach (string Selected_ActionControllerApp_Id in Default_Form_AuthrorizationApp_Model.Selected_ActionControllerApps)
				{
					Int64 Selected_ActionControllerApp_Id_Int64 = Convert.ToInt64(Selected_ActionControllerApp_Id);
					ActionControllerApp ActionControllerApp =ActionControllerAppBLO.FindBaseEntityByID(Selected_ActionControllerApp_Id_Int64);
					AuthrorizationApp.ActionControllerApps.Add(ActionControllerApp);
				}
			}
	
			AuthrorizationApp.Id = Default_Form_AuthrorizationApp_Model.Id;
            return AuthrorizationApp;
        }
        public virtual Default_Form_AuthrorizationApp_Model ConverTo_Default_Form_AuthrorizationApp_Model(AuthrorizationApp AuthrorizationApp)
        {  
			Default_Form_AuthrorizationApp_Model Default_Form_AuthrorizationApp_Model = new Default_Form_AuthrorizationApp_Model();
			Default_Form_AuthrorizationApp_Model.toStringValue = AuthrorizationApp.ToString();
			Default_Form_AuthrorizationApp_Model.RoleAppId = AuthrorizationApp.RoleAppId;
			Default_Form_AuthrorizationApp_Model.ControllerAppId = AuthrorizationApp.ControllerAppId;
			Default_Form_AuthrorizationApp_Model.isAllAction = AuthrorizationApp.isAllAction;

			// ActionControllerApp
            if (AuthrorizationApp.ActionControllerApps != null && AuthrorizationApp.ActionControllerApps.Count > 0)
            {
                Default_Form_AuthrorizationApp_Model.Selected_ActionControllerApps = AuthrorizationApp
                                                        .ActionControllerApps
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_Form_AuthrorizationApp_Model.Selected_ActionControllerApps = new List<string>();
            }			
			Default_Form_AuthrorizationApp_Model.Id = AuthrorizationApp.Id;
            return Default_Form_AuthrorizationApp_Model;            
        }

		public virtual Default_Form_AuthrorizationApp_Model CreateNew()
        {
            AuthrorizationApp AuthrorizationApp = new AuthrorizationAppBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_AuthrorizationApp_Model Default_Form_AuthrorizationApp_Model = this.ConverTo_Default_Form_AuthrorizationApp_Model(AuthrorizationApp);
            return Default_Form_AuthrorizationApp_Model;
        } 

		public virtual List<Default_Form_AuthrorizationApp_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            AuthrorizationAppBLO entityBLO = new AuthrorizationAppBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<AuthrorizationApp> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_AuthrorizationApp_Model> ls_models = new List<Default_Form_AuthrorizationApp_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_AuthrorizationApp_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_AuthrorizationApp_ModelBLM : BaseDefault_Form_AuthrorizationApp_ModelBLM
	{
		public Default_Form_AuthrorizationApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_CalendarDay_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_CalendarDay_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_CalendarDay_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual CalendarDay ConverTo_CalendarDay(Default_Details_CalendarDay_Model Default_Details_CalendarDay_Model)
        {
			CalendarDay CalendarDay = null;
            if (Default_Details_CalendarDay_Model.Id != 0)
            {
                CalendarDay = new CalendarDayBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_CalendarDay_Model.Id);
            }
            else
            {
                CalendarDay = new CalendarDay();
            } 
			CalendarDay.Date = DefaultDateTime_If_Empty(Default_Details_CalendarDay_Model.Date);
			CalendarDay.DateName = Default_Details_CalendarDay_Model.DateName;
			CalendarDay.DateNameAbbrev = Default_Details_CalendarDay_Model.DateNameAbbrev;
			CalendarDay.DayOfWeek = Default_Details_CalendarDay_Model.DayOfWeek;
			CalendarDay.IsWeekend = Default_Details_CalendarDay_Model.IsWeekend;
			CalendarDay.WeekNumber = Default_Details_CalendarDay_Model.WeekNumber;
			CalendarDay.WeekBeginDate = DefaultDateTime_If_Empty(Default_Details_CalendarDay_Model.WeekBeginDate);
			CalendarDay.WeekEndDate = DefaultDateTime_If_Empty(Default_Details_CalendarDay_Model.WeekEndDate);
			CalendarDay.CalendarMonthName = Default_Details_CalendarDay_Model.CalendarMonthName;
			CalendarDay.CalendarMonthNameAbbrev = Default_Details_CalendarDay_Model.CalendarMonthNameAbbrev;
			CalendarDay.CalendarMonthBegin = DefaultDateTime_If_Empty(Default_Details_CalendarDay_Model.CalendarMonthBegin);
			CalendarDay.CalendarMonthEnd = DefaultDateTime_If_Empty(Default_Details_CalendarDay_Model.CalendarMonthEnd);
			CalendarDay.CalendarMonthNumber = Default_Details_CalendarDay_Model.CalendarMonthNumber;
			CalendarDay.CalendarYear = Default_Details_CalendarDay_Model.CalendarYear;
			CalendarDay.FiscalYear = Default_Details_CalendarDay_Model.FiscalYear;
			CalendarDay.DayOfYear = Default_Details_CalendarDay_Model.DayOfYear;
			CalendarDay.CalendarYearBegin = DefaultDateTime_If_Empty(Default_Details_CalendarDay_Model.CalendarYearBegin);
			CalendarDay.CalendarYearEnd = DefaultDateTime_If_Empty(Default_Details_CalendarDay_Model.CalendarYearEnd);
			CalendarDay.FiscalYearBegin = DefaultDateTime_If_Empty(Default_Details_CalendarDay_Model.FiscalYearBegin);
			CalendarDay.FiscalYearEnd = DefaultDateTime_If_Empty(Default_Details_CalendarDay_Model.FiscalYearEnd);
			CalendarDay.Id = Default_Details_CalendarDay_Model.Id;
            return CalendarDay;
        }
        public virtual Default_Details_CalendarDay_Model ConverTo_Default_Details_CalendarDay_Model(CalendarDay CalendarDay)
        {  
			Default_Details_CalendarDay_Model Default_Details_CalendarDay_Model = new Default_Details_CalendarDay_Model();
			Default_Details_CalendarDay_Model.toStringValue = CalendarDay.ToString();
			Default_Details_CalendarDay_Model.Date = DefaultDateTime_If_Empty(CalendarDay.Date);
			Default_Details_CalendarDay_Model.DateName = CalendarDay.DateName;
			Default_Details_CalendarDay_Model.DateNameAbbrev = CalendarDay.DateNameAbbrev;
			Default_Details_CalendarDay_Model.DayOfWeek = CalendarDay.DayOfWeek;
			Default_Details_CalendarDay_Model.IsWeekend = CalendarDay.IsWeekend;
			Default_Details_CalendarDay_Model.WeekNumber = CalendarDay.WeekNumber;
			Default_Details_CalendarDay_Model.WeekBeginDate = DefaultDateTime_If_Empty(CalendarDay.WeekBeginDate);
			Default_Details_CalendarDay_Model.WeekEndDate = DefaultDateTime_If_Empty(CalendarDay.WeekEndDate);
			Default_Details_CalendarDay_Model.CalendarMonthName = CalendarDay.CalendarMonthName;
			Default_Details_CalendarDay_Model.CalendarMonthNameAbbrev = CalendarDay.CalendarMonthNameAbbrev;
			Default_Details_CalendarDay_Model.CalendarMonthBegin = DefaultDateTime_If_Empty(CalendarDay.CalendarMonthBegin);
			Default_Details_CalendarDay_Model.CalendarMonthEnd = DefaultDateTime_If_Empty(CalendarDay.CalendarMonthEnd);
			Default_Details_CalendarDay_Model.CalendarMonthNumber = CalendarDay.CalendarMonthNumber;
			Default_Details_CalendarDay_Model.CalendarYear = CalendarDay.CalendarYear;
			Default_Details_CalendarDay_Model.FiscalYear = CalendarDay.FiscalYear;
			Default_Details_CalendarDay_Model.DayOfYear = CalendarDay.DayOfYear;
			Default_Details_CalendarDay_Model.CalendarYearBegin = DefaultDateTime_If_Empty(CalendarDay.CalendarYearBegin);
			Default_Details_CalendarDay_Model.CalendarYearEnd = DefaultDateTime_If_Empty(CalendarDay.CalendarYearEnd);
			Default_Details_CalendarDay_Model.FiscalYearBegin = DefaultDateTime_If_Empty(CalendarDay.FiscalYearBegin);
			Default_Details_CalendarDay_Model.FiscalYearEnd = DefaultDateTime_If_Empty(CalendarDay.FiscalYearEnd);
			Default_Details_CalendarDay_Model.Id = CalendarDay.Id;
            return Default_Details_CalendarDay_Model;            
        }

		public virtual Default_Details_CalendarDay_Model CreateNew()
        {
            CalendarDay CalendarDay = new CalendarDayBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_CalendarDay_Model Default_Details_CalendarDay_Model = this.ConverTo_Default_Details_CalendarDay_Model(CalendarDay);
            return Default_Details_CalendarDay_Model;
        } 

		public virtual List<Default_Details_CalendarDay_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            CalendarDayBLO entityBLO = new CalendarDayBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<CalendarDay> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_CalendarDay_Model> ls_models = new List<Default_Details_CalendarDay_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_CalendarDay_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_CalendarDay_ModelBLM : BaseDefault_Details_CalendarDay_ModelBLM
	{
		public Default_Details_CalendarDay_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_CalendarDay_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_CalendarDay_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_CalendarDay_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual CalendarDay ConverTo_CalendarDay(Default_Form_CalendarDay_Model Default_Form_CalendarDay_Model)
        {
			CalendarDay CalendarDay = null;
            if (Default_Form_CalendarDay_Model.Id != 0)
            {
                CalendarDay = new CalendarDayBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_CalendarDay_Model.Id);
            }
            else
            {
                CalendarDay = new CalendarDay();
            } 
			CalendarDay.Date = DefaultDateTime_If_Empty(Default_Form_CalendarDay_Model.Date);
			CalendarDay.DateName = Default_Form_CalendarDay_Model.DateName;
			CalendarDay.DateNameAbbrev = Default_Form_CalendarDay_Model.DateNameAbbrev;
			CalendarDay.DayOfWeek = Default_Form_CalendarDay_Model.DayOfWeek;
			CalendarDay.IsWeekend = Default_Form_CalendarDay_Model.IsWeekend;
			CalendarDay.WeekNumber = Default_Form_CalendarDay_Model.WeekNumber;
			CalendarDay.WeekBeginDate = DefaultDateTime_If_Empty(Default_Form_CalendarDay_Model.WeekBeginDate);
			CalendarDay.WeekEndDate = DefaultDateTime_If_Empty(Default_Form_CalendarDay_Model.WeekEndDate);
			CalendarDay.CalendarMonthName = Default_Form_CalendarDay_Model.CalendarMonthName;
			CalendarDay.CalendarMonthNameAbbrev = Default_Form_CalendarDay_Model.CalendarMonthNameAbbrev;
			CalendarDay.CalendarMonthBegin = DefaultDateTime_If_Empty(Default_Form_CalendarDay_Model.CalendarMonthBegin);
			CalendarDay.CalendarMonthEnd = DefaultDateTime_If_Empty(Default_Form_CalendarDay_Model.CalendarMonthEnd);
			CalendarDay.CalendarMonthNumber = Default_Form_CalendarDay_Model.CalendarMonthNumber;
			CalendarDay.CalendarYear = Default_Form_CalendarDay_Model.CalendarYear;
			CalendarDay.FiscalYear = Default_Form_CalendarDay_Model.FiscalYear;
			CalendarDay.DayOfYear = Default_Form_CalendarDay_Model.DayOfYear;
			CalendarDay.CalendarYearBegin = DefaultDateTime_If_Empty(Default_Form_CalendarDay_Model.CalendarYearBegin);
			CalendarDay.CalendarYearEnd = DefaultDateTime_If_Empty(Default_Form_CalendarDay_Model.CalendarYearEnd);
			CalendarDay.FiscalYearBegin = DefaultDateTime_If_Empty(Default_Form_CalendarDay_Model.FiscalYearBegin);
			CalendarDay.FiscalYearEnd = DefaultDateTime_If_Empty(Default_Form_CalendarDay_Model.FiscalYearEnd);
			CalendarDay.Id = Default_Form_CalendarDay_Model.Id;
            return CalendarDay;
        }
        public virtual Default_Form_CalendarDay_Model ConverTo_Default_Form_CalendarDay_Model(CalendarDay CalendarDay)
        {  
			Default_Form_CalendarDay_Model Default_Form_CalendarDay_Model = new Default_Form_CalendarDay_Model();
			Default_Form_CalendarDay_Model.toStringValue = CalendarDay.ToString();
			Default_Form_CalendarDay_Model.Date = DefaultDateTime_If_Empty(CalendarDay.Date);
			Default_Form_CalendarDay_Model.DateName = CalendarDay.DateName;
			Default_Form_CalendarDay_Model.DateNameAbbrev = CalendarDay.DateNameAbbrev;
			Default_Form_CalendarDay_Model.DayOfWeek = CalendarDay.DayOfWeek;
			Default_Form_CalendarDay_Model.IsWeekend = CalendarDay.IsWeekend;
			Default_Form_CalendarDay_Model.WeekNumber = CalendarDay.WeekNumber;
			Default_Form_CalendarDay_Model.WeekBeginDate = DefaultDateTime_If_Empty(CalendarDay.WeekBeginDate);
			Default_Form_CalendarDay_Model.WeekEndDate = DefaultDateTime_If_Empty(CalendarDay.WeekEndDate);
			Default_Form_CalendarDay_Model.CalendarMonthName = CalendarDay.CalendarMonthName;
			Default_Form_CalendarDay_Model.CalendarMonthNameAbbrev = CalendarDay.CalendarMonthNameAbbrev;
			Default_Form_CalendarDay_Model.CalendarMonthBegin = DefaultDateTime_If_Empty(CalendarDay.CalendarMonthBegin);
			Default_Form_CalendarDay_Model.CalendarMonthEnd = DefaultDateTime_If_Empty(CalendarDay.CalendarMonthEnd);
			Default_Form_CalendarDay_Model.CalendarMonthNumber = CalendarDay.CalendarMonthNumber;
			Default_Form_CalendarDay_Model.CalendarYear = CalendarDay.CalendarYear;
			Default_Form_CalendarDay_Model.FiscalYear = CalendarDay.FiscalYear;
			Default_Form_CalendarDay_Model.DayOfYear = CalendarDay.DayOfYear;
			Default_Form_CalendarDay_Model.CalendarYearBegin = DefaultDateTime_If_Empty(CalendarDay.CalendarYearBegin);
			Default_Form_CalendarDay_Model.CalendarYearEnd = DefaultDateTime_If_Empty(CalendarDay.CalendarYearEnd);
			Default_Form_CalendarDay_Model.FiscalYearBegin = DefaultDateTime_If_Empty(CalendarDay.FiscalYearBegin);
			Default_Form_CalendarDay_Model.FiscalYearEnd = DefaultDateTime_If_Empty(CalendarDay.FiscalYearEnd);
			Default_Form_CalendarDay_Model.Id = CalendarDay.Id;
            return Default_Form_CalendarDay_Model;            
        }

		public virtual Default_Form_CalendarDay_Model CreateNew()
        {
            CalendarDay CalendarDay = new CalendarDayBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_CalendarDay_Model Default_Form_CalendarDay_Model = this.ConverTo_Default_Form_CalendarDay_Model(CalendarDay);
            return Default_Form_CalendarDay_Model;
        } 

		public virtual List<Default_Form_CalendarDay_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            CalendarDayBLO entityBLO = new CalendarDayBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<CalendarDay> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_CalendarDay_Model> ls_models = new List<Default_Form_CalendarDay_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_CalendarDay_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_CalendarDay_ModelBLM : BaseDefault_Form_CalendarDay_ModelBLM
	{
		public Default_Form_CalendarDay_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_Category_JustificationAbsence_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_Category_JustificationAbsence_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Category_JustificationAbsence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Category_JustificationAbsence ConverTo_Category_JustificationAbsence(Default_Details_Category_JustificationAbsence_Model Default_Details_Category_JustificationAbsence_Model)
        {
			Category_JustificationAbsence Category_JustificationAbsence = null;
            if (Default_Details_Category_JustificationAbsence_Model.Id != 0)
            {
                Category_JustificationAbsence = new Category_JustificationAbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Category_JustificationAbsence_Model.Id);
            }
            else
            {
                Category_JustificationAbsence = new Category_JustificationAbsence();
            } 
			Category_JustificationAbsence.Name = Default_Details_Category_JustificationAbsence_Model.Name;
			Category_JustificationAbsence.Description = Default_Details_Category_JustificationAbsence_Model.Description;
			Category_JustificationAbsence.Id = Default_Details_Category_JustificationAbsence_Model.Id;
            return Category_JustificationAbsence;
        }
        public virtual Default_Details_Category_JustificationAbsence_Model ConverTo_Default_Details_Category_JustificationAbsence_Model(Category_JustificationAbsence Category_JustificationAbsence)
        {  
			Default_Details_Category_JustificationAbsence_Model Default_Details_Category_JustificationAbsence_Model = new Default_Details_Category_JustificationAbsence_Model();
			Default_Details_Category_JustificationAbsence_Model.toStringValue = Category_JustificationAbsence.ToString();
			Default_Details_Category_JustificationAbsence_Model.Name = Category_JustificationAbsence.Name;
			Default_Details_Category_JustificationAbsence_Model.Description = Category_JustificationAbsence.Description;
			Default_Details_Category_JustificationAbsence_Model.Id = Category_JustificationAbsence.Id;
            return Default_Details_Category_JustificationAbsence_Model;            
        }

		public virtual Default_Details_Category_JustificationAbsence_Model CreateNew()
        {
            Category_JustificationAbsence Category_JustificationAbsence = new Category_JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Category_JustificationAbsence_Model Default_Details_Category_JustificationAbsence_Model = this.ConverTo_Default_Details_Category_JustificationAbsence_Model(Category_JustificationAbsence);
            return Default_Details_Category_JustificationAbsence_Model;
        } 

		public virtual List<Default_Details_Category_JustificationAbsence_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            Category_JustificationAbsenceBLO entityBLO = new Category_JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Category_JustificationAbsence> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Category_JustificationAbsence_Model> ls_models = new List<Default_Details_Category_JustificationAbsence_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Category_JustificationAbsence_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Category_JustificationAbsence_ModelBLM : BaseDefault_Details_Category_JustificationAbsence_ModelBLM
	{
		public Default_Details_Category_JustificationAbsence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_Category_JustificationAbsence_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_Category_JustificationAbsence_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Category_JustificationAbsence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Category_JustificationAbsence ConverTo_Category_JustificationAbsence(Default_Form_Category_JustificationAbsence_Model Default_Form_Category_JustificationAbsence_Model)
        {
			Category_JustificationAbsence Category_JustificationAbsence = null;
            if (Default_Form_Category_JustificationAbsence_Model.Id != 0)
            {
                Category_JustificationAbsence = new Category_JustificationAbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Category_JustificationAbsence_Model.Id);
            }
            else
            {
                Category_JustificationAbsence = new Category_JustificationAbsence();
            } 
			Category_JustificationAbsence.Name = Default_Form_Category_JustificationAbsence_Model.Name;
			Category_JustificationAbsence.Description = Default_Form_Category_JustificationAbsence_Model.Description;
			Category_JustificationAbsence.Id = Default_Form_Category_JustificationAbsence_Model.Id;
            return Category_JustificationAbsence;
        }
        public virtual Default_Form_Category_JustificationAbsence_Model ConverTo_Default_Form_Category_JustificationAbsence_Model(Category_JustificationAbsence Category_JustificationAbsence)
        {  
			Default_Form_Category_JustificationAbsence_Model Default_Form_Category_JustificationAbsence_Model = new Default_Form_Category_JustificationAbsence_Model();
			Default_Form_Category_JustificationAbsence_Model.toStringValue = Category_JustificationAbsence.ToString();
			Default_Form_Category_JustificationAbsence_Model.Name = Category_JustificationAbsence.Name;
			Default_Form_Category_JustificationAbsence_Model.Description = Category_JustificationAbsence.Description;
			Default_Form_Category_JustificationAbsence_Model.Id = Category_JustificationAbsence.Id;
            return Default_Form_Category_JustificationAbsence_Model;            
        }

		public virtual Default_Form_Category_JustificationAbsence_Model CreateNew()
        {
            Category_JustificationAbsence Category_JustificationAbsence = new Category_JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Category_JustificationAbsence_Model Default_Form_Category_JustificationAbsence_Model = this.ConverTo_Default_Form_Category_JustificationAbsence_Model(Category_JustificationAbsence);
            return Default_Form_Category_JustificationAbsence_Model;
        } 

		public virtual List<Default_Form_Category_JustificationAbsence_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            Category_JustificationAbsenceBLO entityBLO = new Category_JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Category_JustificationAbsence> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_Category_JustificationAbsence_Model> ls_models = new List<Default_Form_Category_JustificationAbsence_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_Category_JustificationAbsence_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_Category_JustificationAbsence_ModelBLM : BaseDefault_Form_Category_JustificationAbsence_ModelBLM
	{
		public Default_Form_Category_JustificationAbsence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_Category_WarningTrainee_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_Category_WarningTrainee_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Category_WarningTrainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Category_WarningTrainee ConverTo_Category_WarningTrainee(Default_Details_Category_WarningTrainee_Model Default_Details_Category_WarningTrainee_Model)
        {
			Category_WarningTrainee Category_WarningTrainee = null;
            if (Default_Details_Category_WarningTrainee_Model.Id != 0)
            {
                Category_WarningTrainee = new Category_WarningTraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Category_WarningTrainee_Model.Id);
            }
            else
            {
                Category_WarningTrainee = new Category_WarningTrainee();
            } 
			Category_WarningTrainee.Name = Default_Details_Category_WarningTrainee_Model.Name;
			Category_WarningTrainee.Description = Default_Details_Category_WarningTrainee_Model.Description;
			Category_WarningTrainee.Id = Default_Details_Category_WarningTrainee_Model.Id;
            return Category_WarningTrainee;
        }
        public virtual Default_Details_Category_WarningTrainee_Model ConverTo_Default_Details_Category_WarningTrainee_Model(Category_WarningTrainee Category_WarningTrainee)
        {  
			Default_Details_Category_WarningTrainee_Model Default_Details_Category_WarningTrainee_Model = new Default_Details_Category_WarningTrainee_Model();
			Default_Details_Category_WarningTrainee_Model.toStringValue = Category_WarningTrainee.ToString();
			Default_Details_Category_WarningTrainee_Model.Name = Category_WarningTrainee.Name;
			Default_Details_Category_WarningTrainee_Model.Description = Category_WarningTrainee.Description;
			Default_Details_Category_WarningTrainee_Model.Id = Category_WarningTrainee.Id;
            return Default_Details_Category_WarningTrainee_Model;            
        }

		public virtual Default_Details_Category_WarningTrainee_Model CreateNew()
        {
            Category_WarningTrainee Category_WarningTrainee = new Category_WarningTraineeBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Category_WarningTrainee_Model Default_Details_Category_WarningTrainee_Model = this.ConverTo_Default_Details_Category_WarningTrainee_Model(Category_WarningTrainee);
            return Default_Details_Category_WarningTrainee_Model;
        } 

		public virtual List<Default_Details_Category_WarningTrainee_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            Category_WarningTraineeBLO entityBLO = new Category_WarningTraineeBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Category_WarningTrainee> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Category_WarningTrainee_Model> ls_models = new List<Default_Details_Category_WarningTrainee_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Category_WarningTrainee_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Category_WarningTrainee_ModelBLM : BaseDefault_Details_Category_WarningTrainee_ModelBLM
	{
		public Default_Details_Category_WarningTrainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_Category_WarningTrainee_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_Category_WarningTrainee_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Category_WarningTrainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Category_WarningTrainee ConverTo_Category_WarningTrainee(Default_Form_Category_WarningTrainee_Model Default_Form_Category_WarningTrainee_Model)
        {
			Category_WarningTrainee Category_WarningTrainee = null;
            if (Default_Form_Category_WarningTrainee_Model.Id != 0)
            {
                Category_WarningTrainee = new Category_WarningTraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Category_WarningTrainee_Model.Id);
            }
            else
            {
                Category_WarningTrainee = new Category_WarningTrainee();
            } 
			Category_WarningTrainee.Name = Default_Form_Category_WarningTrainee_Model.Name;
			Category_WarningTrainee.Description = Default_Form_Category_WarningTrainee_Model.Description;
			Category_WarningTrainee.Id = Default_Form_Category_WarningTrainee_Model.Id;
            return Category_WarningTrainee;
        }
        public virtual Default_Form_Category_WarningTrainee_Model ConverTo_Default_Form_Category_WarningTrainee_Model(Category_WarningTrainee Category_WarningTrainee)
        {  
			Default_Form_Category_WarningTrainee_Model Default_Form_Category_WarningTrainee_Model = new Default_Form_Category_WarningTrainee_Model();
			Default_Form_Category_WarningTrainee_Model.toStringValue = Category_WarningTrainee.ToString();
			Default_Form_Category_WarningTrainee_Model.Name = Category_WarningTrainee.Name;
			Default_Form_Category_WarningTrainee_Model.Description = Category_WarningTrainee.Description;
			Default_Form_Category_WarningTrainee_Model.Id = Category_WarningTrainee.Id;
            return Default_Form_Category_WarningTrainee_Model;            
        }

		public virtual Default_Form_Category_WarningTrainee_Model CreateNew()
        {
            Category_WarningTrainee Category_WarningTrainee = new Category_WarningTraineeBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Category_WarningTrainee_Model Default_Form_Category_WarningTrainee_Model = this.ConverTo_Default_Form_Category_WarningTrainee_Model(Category_WarningTrainee);
            return Default_Form_Category_WarningTrainee_Model;
        } 

		public virtual List<Default_Form_Category_WarningTrainee_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            Category_WarningTraineeBLO entityBLO = new Category_WarningTraineeBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Category_WarningTrainee> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_Category_WarningTrainee_Model> ls_models = new List<Default_Form_Category_WarningTrainee_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_Category_WarningTrainee_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_Category_WarningTrainee_ModelBLM : BaseDefault_Form_Category_WarningTrainee_ModelBLM
	{
		public Default_Form_Category_WarningTrainee_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_ClassroomCategory_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_ClassroomCategory_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_ClassroomCategory_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ClassroomCategory ConverTo_ClassroomCategory(Default_Details_ClassroomCategory_Model Default_Details_ClassroomCategory_Model)
        {
			ClassroomCategory ClassroomCategory = null;
            if (Default_Details_ClassroomCategory_Model.Id != 0)
            {
                ClassroomCategory = new ClassroomCategoryBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_ClassroomCategory_Model.Id);
            }
            else
            {
                ClassroomCategory = new ClassroomCategory();
            } 
			ClassroomCategory.Code = Default_Details_ClassroomCategory_Model.Code;
			ClassroomCategory.Name = Default_Details_ClassroomCategory_Model.Name;
			ClassroomCategory.Description = Default_Details_ClassroomCategory_Model.Description;
			ClassroomCategory.Id = Default_Details_ClassroomCategory_Model.Id;
            return ClassroomCategory;
        }
        public virtual Default_Details_ClassroomCategory_Model ConverTo_Default_Details_ClassroomCategory_Model(ClassroomCategory ClassroomCategory)
        {  
			Default_Details_ClassroomCategory_Model Default_Details_ClassroomCategory_Model = new Default_Details_ClassroomCategory_Model();
			Default_Details_ClassroomCategory_Model.toStringValue = ClassroomCategory.ToString();
			Default_Details_ClassroomCategory_Model.Code = ClassroomCategory.Code;
			Default_Details_ClassroomCategory_Model.Name = ClassroomCategory.Name;
			Default_Details_ClassroomCategory_Model.Description = ClassroomCategory.Description;
			Default_Details_ClassroomCategory_Model.Id = ClassroomCategory.Id;
            return Default_Details_ClassroomCategory_Model;            
        }

		public virtual Default_Details_ClassroomCategory_Model CreateNew()
        {
            ClassroomCategory ClassroomCategory = new ClassroomCategoryBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_ClassroomCategory_Model Default_Details_ClassroomCategory_Model = this.ConverTo_Default_Details_ClassroomCategory_Model(ClassroomCategory);
            return Default_Details_ClassroomCategory_Model;
        } 

		public virtual List<Default_Details_ClassroomCategory_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ClassroomCategoryBLO entityBLO = new ClassroomCategoryBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ClassroomCategory> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_ClassroomCategory_Model> ls_models = new List<Default_Details_ClassroomCategory_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_ClassroomCategory_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_ClassroomCategory_ModelBLM : BaseDefault_Details_ClassroomCategory_ModelBLM
	{
		public Default_Details_ClassroomCategory_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_ClassroomCategory_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_ClassroomCategory_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_ClassroomCategory_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ClassroomCategory ConverTo_ClassroomCategory(Default_Form_ClassroomCategory_Model Default_Form_ClassroomCategory_Model)
        {
			ClassroomCategory ClassroomCategory = null;
            if (Default_Form_ClassroomCategory_Model.Id != 0)
            {
                ClassroomCategory = new ClassroomCategoryBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_ClassroomCategory_Model.Id);
            }
            else
            {
                ClassroomCategory = new ClassroomCategory();
            } 
			ClassroomCategory.Code = Default_Form_ClassroomCategory_Model.Code;
			ClassroomCategory.Name = Default_Form_ClassroomCategory_Model.Name;
			ClassroomCategory.Description = Default_Form_ClassroomCategory_Model.Description;
			ClassroomCategory.Id = Default_Form_ClassroomCategory_Model.Id;
            return ClassroomCategory;
        }
        public virtual Default_Form_ClassroomCategory_Model ConverTo_Default_Form_ClassroomCategory_Model(ClassroomCategory ClassroomCategory)
        {  
			Default_Form_ClassroomCategory_Model Default_Form_ClassroomCategory_Model = new Default_Form_ClassroomCategory_Model();
			Default_Form_ClassroomCategory_Model.toStringValue = ClassroomCategory.ToString();
			Default_Form_ClassroomCategory_Model.Code = ClassroomCategory.Code;
			Default_Form_ClassroomCategory_Model.Name = ClassroomCategory.Name;
			Default_Form_ClassroomCategory_Model.Description = ClassroomCategory.Description;
			Default_Form_ClassroomCategory_Model.Id = ClassroomCategory.Id;
            return Default_Form_ClassroomCategory_Model;            
        }

		public virtual Default_Form_ClassroomCategory_Model CreateNew()
        {
            ClassroomCategory ClassroomCategory = new ClassroomCategoryBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_ClassroomCategory_Model Default_Form_ClassroomCategory_Model = this.ConverTo_Default_Form_ClassroomCategory_Model(ClassroomCategory);
            return Default_Form_ClassroomCategory_Model;
        } 

		public virtual List<Default_Form_ClassroomCategory_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ClassroomCategoryBLO entityBLO = new ClassroomCategoryBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ClassroomCategory> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_ClassroomCategory_Model> ls_models = new List<Default_Form_ClassroomCategory_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_ClassroomCategory_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_ClassroomCategory_ModelBLM : BaseDefault_Form_ClassroomCategory_ModelBLM
	{
		public Default_Form_ClassroomCategory_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_Classroom_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_Classroom_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Classroom_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Classroom ConverTo_Classroom(Default_Details_Classroom_Model Default_Details_Classroom_Model)
        {
			Classroom Classroom = null;
            if (Default_Details_Classroom_Model.Id != 0)
            {
                Classroom = new ClassroomBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Classroom_Model.Id);
            }
            else
            {
                Classroom = new Classroom();
            } 
			Classroom.Code = Default_Details_Classroom_Model.Code;
			Classroom.Name = Default_Details_Classroom_Model.Name;
			Classroom.ClassroomCategory = Default_Details_Classroom_Model.ClassroomCategory;
			Classroom.Description = Default_Details_Classroom_Model.Description;
			Classroom.Id = Default_Details_Classroom_Model.Id;
            return Classroom;
        }
        public virtual Default_Details_Classroom_Model ConverTo_Default_Details_Classroom_Model(Classroom Classroom)
        {  
			Default_Details_Classroom_Model Default_Details_Classroom_Model = new Default_Details_Classroom_Model();
			Default_Details_Classroom_Model.toStringValue = Classroom.ToString();
			Default_Details_Classroom_Model.Code = Classroom.Code;
			Default_Details_Classroom_Model.Name = Classroom.Name;
			Default_Details_Classroom_Model.ClassroomCategory = Classroom.ClassroomCategory;
			Default_Details_Classroom_Model.Description = Classroom.Description;
			Default_Details_Classroom_Model.Id = Classroom.Id;
            return Default_Details_Classroom_Model;            
        }

		public virtual Default_Details_Classroom_Model CreateNew()
        {
            Classroom Classroom = new ClassroomBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Classroom_Model Default_Details_Classroom_Model = this.ConverTo_Default_Details_Classroom_Model(Classroom);
            return Default_Details_Classroom_Model;
        } 

		public virtual List<Default_Details_Classroom_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ClassroomBLO entityBLO = new ClassroomBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Classroom> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Classroom_Model> ls_models = new List<Default_Details_Classroom_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Classroom_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Classroom_ModelBLM : BaseDefault_Details_Classroom_ModelBLM
	{
		public Default_Details_Classroom_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_Classroom_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_Classroom_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Classroom_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Classroom ConverTo_Classroom(Default_Form_Classroom_Model Default_Form_Classroom_Model)
        {
			Classroom Classroom = null;
            if (Default_Form_Classroom_Model.Id != 0)
            {
                Classroom = new ClassroomBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Classroom_Model.Id);
            }
            else
            {
                Classroom = new Classroom();
            } 
			Classroom.Code = Default_Form_Classroom_Model.Code;
			Classroom.Name = Default_Form_Classroom_Model.Name;
			Classroom.ClassroomCategoryId = Default_Form_Classroom_Model.ClassroomCategoryId;
			Classroom.ClassroomCategory = new ClassroomCategoryBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Classroom_Model.ClassroomCategoryId)) ;
			Classroom.Description = Default_Form_Classroom_Model.Description;
			Classroom.Id = Default_Form_Classroom_Model.Id;
            return Classroom;
        }
        public virtual Default_Form_Classroom_Model ConverTo_Default_Form_Classroom_Model(Classroom Classroom)
        {  
			Default_Form_Classroom_Model Default_Form_Classroom_Model = new Default_Form_Classroom_Model();
			Default_Form_Classroom_Model.toStringValue = Classroom.ToString();
			Default_Form_Classroom_Model.Code = Classroom.Code;
			Default_Form_Classroom_Model.Name = Classroom.Name;
			Default_Form_Classroom_Model.ClassroomCategoryId = Classroom.ClassroomCategoryId;
			Default_Form_Classroom_Model.Description = Classroom.Description;
			Default_Form_Classroom_Model.Id = Classroom.Id;
            return Default_Form_Classroom_Model;            
        }

		public virtual Default_Form_Classroom_Model CreateNew()
        {
            Classroom Classroom = new ClassroomBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Classroom_Model Default_Form_Classroom_Model = this.ConverTo_Default_Form_Classroom_Model(Classroom);
            return Default_Form_Classroom_Model;
        } 

		public virtual List<Default_Form_Classroom_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ClassroomBLO entityBLO = new ClassroomBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Classroom> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_Classroom_Model> ls_models = new List<Default_Form_Classroom_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_Classroom_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_Classroom_ModelBLM : BaseDefault_Form_Classroom_ModelBLM
	{
		public Default_Form_Classroom_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_ControllerApp_Model

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
using TrainingIS.Entities.ModelsViews;
using GApp.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_ControllerApp_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_ControllerApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ControllerApp ConverTo_ControllerApp(Default_Details_ControllerApp_Model Default_Details_ControllerApp_Model)
        {
			ControllerApp ControllerApp = null;
            if (Default_Details_ControllerApp_Model.Id != 0)
            {
                ControllerApp = new ControllerAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_ControllerApp_Model.Id);
            }
            else
            {
                ControllerApp = new ControllerApp();
            } 
			ControllerApp.Code = Default_Details_ControllerApp_Model.Code;
			ControllerApp.Name = Default_Details_ControllerApp_Model.Name;
			ControllerApp.Description = Default_Details_ControllerApp_Model.Description;
			ControllerApp.Id = Default_Details_ControllerApp_Model.Id;
            return ControllerApp;
        }
        public virtual Default_Details_ControllerApp_Model ConverTo_Default_Details_ControllerApp_Model(ControllerApp ControllerApp)
        {  
			Default_Details_ControllerApp_Model Default_Details_ControllerApp_Model = new Default_Details_ControllerApp_Model();
			Default_Details_ControllerApp_Model.toStringValue = ControllerApp.ToString();
			Default_Details_ControllerApp_Model.Code = ControllerApp.Code;
			Default_Details_ControllerApp_Model.Name = ControllerApp.Name;
			Default_Details_ControllerApp_Model.Description = ControllerApp.Description;
			Default_Details_ControllerApp_Model.Id = ControllerApp.Id;
            return Default_Details_ControllerApp_Model;            
        }

		public virtual Default_Details_ControllerApp_Model CreateNew()
        {
            ControllerApp ControllerApp = new ControllerAppBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_ControllerApp_Model Default_Details_ControllerApp_Model = this.ConverTo_Default_Details_ControllerApp_Model(ControllerApp);
            return Default_Details_ControllerApp_Model;
        } 

		public virtual List<Default_Details_ControllerApp_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ControllerAppBLO entityBLO = new ControllerAppBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ControllerApp> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_ControllerApp_Model> ls_models = new List<Default_Details_ControllerApp_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_ControllerApp_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_ControllerApp_ModelBLM : BaseDefault_Details_ControllerApp_ModelBLM
	{
		public Default_Details_ControllerApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_ControllerApp_Model

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
using TrainingIS.Entities.ModelsViews;
using GApp.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_ControllerApp_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_ControllerApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ControllerApp ConverTo_ControllerApp(Default_Form_ControllerApp_Model Default_Form_ControllerApp_Model)
        {
			ControllerApp ControllerApp = null;
            if (Default_Form_ControllerApp_Model.Id != 0)
            {
                ControllerApp = new ControllerAppBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_ControllerApp_Model.Id);
            }
            else
            {
                ControllerApp = new ControllerApp();
            } 
			ControllerApp.Code = Default_Form_ControllerApp_Model.Code;
			ControllerApp.Name = Default_Form_ControllerApp_Model.Name;
			ControllerApp.Description = Default_Form_ControllerApp_Model.Description;
			ControllerApp.Id = Default_Form_ControllerApp_Model.Id;
            return ControllerApp;
        }
        public virtual Default_Form_ControllerApp_Model ConverTo_Default_Form_ControllerApp_Model(ControllerApp ControllerApp)
        {  
			Default_Form_ControllerApp_Model Default_Form_ControllerApp_Model = new Default_Form_ControllerApp_Model();
			Default_Form_ControllerApp_Model.toStringValue = ControllerApp.ToString();
			Default_Form_ControllerApp_Model.Code = ControllerApp.Code;
			Default_Form_ControllerApp_Model.Name = ControllerApp.Name;
			Default_Form_ControllerApp_Model.Description = ControllerApp.Description;
			Default_Form_ControllerApp_Model.Id = ControllerApp.Id;
            return Default_Form_ControllerApp_Model;            
        }

		public virtual Default_Form_ControllerApp_Model CreateNew()
        {
            ControllerApp ControllerApp = new ControllerAppBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_ControllerApp_Model Default_Form_ControllerApp_Model = this.ConverTo_Default_Form_ControllerApp_Model(ControllerApp);
            return Default_Form_ControllerApp_Model;
        } 

		public virtual List<Default_Form_ControllerApp_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            ControllerAppBLO entityBLO = new ControllerAppBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<ControllerApp> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_ControllerApp_Model> ls_models = new List<Default_Form_ControllerApp_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_ControllerApp_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_ControllerApp_ModelBLM : BaseDefault_Form_ControllerApp_ModelBLM
	{
		public Default_Form_ControllerApp_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_EntityPropertyShortcut_Model

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
using TrainingIS.Entities.ModelsViews;
using GApp.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_EntityPropertyShortcut_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_EntityPropertyShortcut_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual EntityPropertyShortcut ConverTo_EntityPropertyShortcut(Default_Details_EntityPropertyShortcut_Model Default_Details_EntityPropertyShortcut_Model)
        {
			EntityPropertyShortcut EntityPropertyShortcut = null;
            if (Default_Details_EntityPropertyShortcut_Model.Id != 0)
            {
                EntityPropertyShortcut = new EntityPropertyShortcutBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_EntityPropertyShortcut_Model.Id);
            }
            else
            {
                EntityPropertyShortcut = new EntityPropertyShortcut();
            } 
			EntityPropertyShortcut.EntityName = Default_Details_EntityPropertyShortcut_Model.EntityName;
			EntityPropertyShortcut.PropertyName = Default_Details_EntityPropertyShortcut_Model.PropertyName;
			EntityPropertyShortcut.PropertyShortcutName = Default_Details_EntityPropertyShortcut_Model.PropertyShortcutName;
			EntityPropertyShortcut.Description = Default_Details_EntityPropertyShortcut_Model.Description;
			EntityPropertyShortcut.Id = Default_Details_EntityPropertyShortcut_Model.Id;
            return EntityPropertyShortcut;
        }
        public virtual Default_Details_EntityPropertyShortcut_Model ConverTo_Default_Details_EntityPropertyShortcut_Model(EntityPropertyShortcut EntityPropertyShortcut)
        {  
			Default_Details_EntityPropertyShortcut_Model Default_Details_EntityPropertyShortcut_Model = new Default_Details_EntityPropertyShortcut_Model();
			Default_Details_EntityPropertyShortcut_Model.toStringValue = EntityPropertyShortcut.ToString();
			Default_Details_EntityPropertyShortcut_Model.EntityName = EntityPropertyShortcut.EntityName;
			Default_Details_EntityPropertyShortcut_Model.PropertyName = EntityPropertyShortcut.PropertyName;
			Default_Details_EntityPropertyShortcut_Model.PropertyShortcutName = EntityPropertyShortcut.PropertyShortcutName;
			Default_Details_EntityPropertyShortcut_Model.Description = EntityPropertyShortcut.Description;
			Default_Details_EntityPropertyShortcut_Model.Id = EntityPropertyShortcut.Id;
            return Default_Details_EntityPropertyShortcut_Model;            
        }

		public virtual Default_Details_EntityPropertyShortcut_Model CreateNew()
        {
            EntityPropertyShortcut EntityPropertyShortcut = new EntityPropertyShortcutBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_EntityPropertyShortcut_Model Default_Details_EntityPropertyShortcut_Model = this.ConverTo_Default_Details_EntityPropertyShortcut_Model(EntityPropertyShortcut);
            return Default_Details_EntityPropertyShortcut_Model;
        } 

		public virtual List<Default_Details_EntityPropertyShortcut_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            EntityPropertyShortcutBLO entityBLO = new EntityPropertyShortcutBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<EntityPropertyShortcut> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_EntityPropertyShortcut_Model> ls_models = new List<Default_Details_EntityPropertyShortcut_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_EntityPropertyShortcut_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_EntityPropertyShortcut_ModelBLM : BaseDefault_Details_EntityPropertyShortcut_ModelBLM
	{
		public Default_Details_EntityPropertyShortcut_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_EntityPropertyShortcut_Model

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
using TrainingIS.Entities.ModelsViews;
using GApp.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_EntityPropertyShortcut_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_EntityPropertyShortcut_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual EntityPropertyShortcut ConverTo_EntityPropertyShortcut(Default_Form_EntityPropertyShortcut_Model Default_Form_EntityPropertyShortcut_Model)
        {
			EntityPropertyShortcut EntityPropertyShortcut = null;
            if (Default_Form_EntityPropertyShortcut_Model.Id != 0)
            {
                EntityPropertyShortcut = new EntityPropertyShortcutBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_EntityPropertyShortcut_Model.Id);
            }
            else
            {
                EntityPropertyShortcut = new EntityPropertyShortcut();
            } 
			EntityPropertyShortcut.EntityName = Default_Form_EntityPropertyShortcut_Model.EntityName;
			EntityPropertyShortcut.PropertyName = Default_Form_EntityPropertyShortcut_Model.PropertyName;
			EntityPropertyShortcut.PropertyShortcutName = Default_Form_EntityPropertyShortcut_Model.PropertyShortcutName;
			EntityPropertyShortcut.Description = Default_Form_EntityPropertyShortcut_Model.Description;
			EntityPropertyShortcut.Id = Default_Form_EntityPropertyShortcut_Model.Id;
            return EntityPropertyShortcut;
        }
        public virtual Default_Form_EntityPropertyShortcut_Model ConverTo_Default_Form_EntityPropertyShortcut_Model(EntityPropertyShortcut EntityPropertyShortcut)
        {  
			Default_Form_EntityPropertyShortcut_Model Default_Form_EntityPropertyShortcut_Model = new Default_Form_EntityPropertyShortcut_Model();
			Default_Form_EntityPropertyShortcut_Model.toStringValue = EntityPropertyShortcut.ToString();
			Default_Form_EntityPropertyShortcut_Model.EntityName = EntityPropertyShortcut.EntityName;
			Default_Form_EntityPropertyShortcut_Model.PropertyName = EntityPropertyShortcut.PropertyName;
			Default_Form_EntityPropertyShortcut_Model.PropertyShortcutName = EntityPropertyShortcut.PropertyShortcutName;
			Default_Form_EntityPropertyShortcut_Model.Description = EntityPropertyShortcut.Description;
			Default_Form_EntityPropertyShortcut_Model.Id = EntityPropertyShortcut.Id;
            return Default_Form_EntityPropertyShortcut_Model;            
        }

		public virtual Default_Form_EntityPropertyShortcut_Model CreateNew()
        {
            EntityPropertyShortcut EntityPropertyShortcut = new EntityPropertyShortcutBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_EntityPropertyShortcut_Model Default_Form_EntityPropertyShortcut_Model = this.ConverTo_Default_Form_EntityPropertyShortcut_Model(EntityPropertyShortcut);
            return Default_Form_EntityPropertyShortcut_Model;
        } 

		public virtual List<Default_Form_EntityPropertyShortcut_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            EntityPropertyShortcutBLO entityBLO = new EntityPropertyShortcutBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<EntityPropertyShortcut> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_EntityPropertyShortcut_Model> ls_models = new List<Default_Form_EntityPropertyShortcut_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_EntityPropertyShortcut_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_EntityPropertyShortcut_ModelBLM : BaseDefault_Form_EntityPropertyShortcut_ModelBLM
	{
		public Default_Form_EntityPropertyShortcut_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_FormerSpecialty_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_FormerSpecialty_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_FormerSpecialty_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual FormerSpecialty ConverTo_FormerSpecialty(Default_Details_FormerSpecialty_Model Default_Details_FormerSpecialty_Model)
        {
			FormerSpecialty FormerSpecialty = null;
            if (Default_Details_FormerSpecialty_Model.Id != 0)
            {
                FormerSpecialty = new FormerSpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_FormerSpecialty_Model.Id);
            }
            else
            {
                FormerSpecialty = new FormerSpecialty();
            } 
			FormerSpecialty.Code = Default_Details_FormerSpecialty_Model.Code;
			FormerSpecialty.Name = Default_Details_FormerSpecialty_Model.Name;
			FormerSpecialty.Description = Default_Details_FormerSpecialty_Model.Description;
			FormerSpecialty.Id = Default_Details_FormerSpecialty_Model.Id;
            return FormerSpecialty;
        }
        public virtual Default_Details_FormerSpecialty_Model ConverTo_Default_Details_FormerSpecialty_Model(FormerSpecialty FormerSpecialty)
        {  
			Default_Details_FormerSpecialty_Model Default_Details_FormerSpecialty_Model = new Default_Details_FormerSpecialty_Model();
			Default_Details_FormerSpecialty_Model.toStringValue = FormerSpecialty.ToString();
			Default_Details_FormerSpecialty_Model.Code = FormerSpecialty.Code;
			Default_Details_FormerSpecialty_Model.Name = FormerSpecialty.Name;
			Default_Details_FormerSpecialty_Model.Description = FormerSpecialty.Description;
			Default_Details_FormerSpecialty_Model.Id = FormerSpecialty.Id;
            return Default_Details_FormerSpecialty_Model;            
        }

		public virtual Default_Details_FormerSpecialty_Model CreateNew()
        {
            FormerSpecialty FormerSpecialty = new FormerSpecialtyBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_FormerSpecialty_Model Default_Details_FormerSpecialty_Model = this.ConverTo_Default_Details_FormerSpecialty_Model(FormerSpecialty);
            return Default_Details_FormerSpecialty_Model;
        } 

		public virtual List<Default_Details_FormerSpecialty_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            FormerSpecialtyBLO entityBLO = new FormerSpecialtyBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<FormerSpecialty> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_FormerSpecialty_Model> ls_models = new List<Default_Details_FormerSpecialty_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_FormerSpecialty_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_FormerSpecialty_ModelBLM : BaseDefault_Details_FormerSpecialty_ModelBLM
	{
		public Default_Details_FormerSpecialty_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_FormerSpecialty_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_FormerSpecialty_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_FormerSpecialty_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual FormerSpecialty ConverTo_FormerSpecialty(Default_Form_FormerSpecialty_Model Default_Form_FormerSpecialty_Model)
        {
			FormerSpecialty FormerSpecialty = null;
            if (Default_Form_FormerSpecialty_Model.Id != 0)
            {
                FormerSpecialty = new FormerSpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_FormerSpecialty_Model.Id);
            }
            else
            {
                FormerSpecialty = new FormerSpecialty();
            } 
			FormerSpecialty.Code = Default_Form_FormerSpecialty_Model.Code;
			FormerSpecialty.Name = Default_Form_FormerSpecialty_Model.Name;
			FormerSpecialty.Description = Default_Form_FormerSpecialty_Model.Description;
			FormerSpecialty.Id = Default_Form_FormerSpecialty_Model.Id;
            return FormerSpecialty;
        }
        public virtual Default_Form_FormerSpecialty_Model ConverTo_Default_Form_FormerSpecialty_Model(FormerSpecialty FormerSpecialty)
        {  
			Default_Form_FormerSpecialty_Model Default_Form_FormerSpecialty_Model = new Default_Form_FormerSpecialty_Model();
			Default_Form_FormerSpecialty_Model.toStringValue = FormerSpecialty.ToString();
			Default_Form_FormerSpecialty_Model.Code = FormerSpecialty.Code;
			Default_Form_FormerSpecialty_Model.Name = FormerSpecialty.Name;
			Default_Form_FormerSpecialty_Model.Description = FormerSpecialty.Description;
			Default_Form_FormerSpecialty_Model.Id = FormerSpecialty.Id;
            return Default_Form_FormerSpecialty_Model;            
        }

		public virtual Default_Form_FormerSpecialty_Model CreateNew()
        {
            FormerSpecialty FormerSpecialty = new FormerSpecialtyBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_FormerSpecialty_Model Default_Form_FormerSpecialty_Model = this.ConverTo_Default_Form_FormerSpecialty_Model(FormerSpecialty);
            return Default_Form_FormerSpecialty_Model;
        } 

		public virtual List<Default_Form_FormerSpecialty_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            FormerSpecialtyBLO entityBLO = new FormerSpecialtyBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<FormerSpecialty> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_FormerSpecialty_Model> ls_models = new List<Default_Form_FormerSpecialty_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_FormerSpecialty_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_FormerSpecialty_ModelBLM : BaseDefault_Form_FormerSpecialty_ModelBLM
	{
		public Default_Form_FormerSpecialty_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_Former_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_Former_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Former_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Former ConverTo_Former(Default_Details_Former_Model Default_Details_Former_Model)
        {
			Former Former = null;
            if (Default_Details_Former_Model.Id != 0)
            {
                Former = new FormerBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Former_Model.Id);
            }
            else
            {
                Former = new Former();
            } 
			Former.FormerSpecialty = Default_Details_Former_Model.FormerSpecialty;
			Former.WeeklyHourlyMass = Default_Details_Former_Model.WeeklyHourlyMass;
			Former.RegistrationNumber = Default_Details_Former_Model.RegistrationNumber;
			Former.CreateUserAccount = Default_Details_Former_Model.CreateUserAccount;
			Former.Login = Default_Details_Former_Model.Login;
			Former.Password = Default_Details_Former_Model.Password;
			Former.FirstName = Default_Details_Former_Model.FirstName;
			Former.LastName = Default_Details_Former_Model.LastName;
			Former.FirstNameArabe = Default_Details_Former_Model.FirstNameArabe;
			Former.LastNameArabe = Default_Details_Former_Model.LastNameArabe;
			Former.Sex = Default_Details_Former_Model.Sex;
			Former.Birthdate = DefaultDateTime_If_Empty(Default_Details_Former_Model.Birthdate);
			Former.Nationality = Default_Details_Former_Model.Nationality;
			Former.BirthPlace = Default_Details_Former_Model.BirthPlace;
			Former.CIN = Default_Details_Former_Model.CIN;
			if (!string.IsNullOrEmpty(Default_Details_Former_Model.Photo_Reference))
            {
				if(Default_Details_Former_Model.Photo_Reference == "Delete" && Former.Photo != null)
                {
                    Former.Photo.Old_Reference = Former.Photo.Reference;
                    Former.Photo.Reference = "Delete";
                }
                else
				{
					if (Former.Photo == null) Former.Photo = new GPicture() { Old_Reference = "Empty" };
					if (Former.Photo.Reference != Default_Details_Former_Model.Photo_Reference)
					{
						// Save the old reference to be deleted by the save methode 
						if (!string.IsNullOrEmpty(Former.Photo.Reference))
                            Former.Photo.Old_Reference = Former.Photo.Reference;

						 

						GPictureBLO gPictureBLO = new GPictureBLO(this.GAppContext);
						Former.Photo.Reference = Default_Details_Former_Model.Photo_Reference;
                  
						Former.Photo.Original_Thumbnail = gPictureBLO.Get_URL_Original_Picture_Path(Default_Details_Former_Model.Photo_Reference);
						Former.Photo.Small_Thumbnail = gPictureBLO.Get_URL_Small_Picture_Path(Default_Details_Former_Model.Photo_Reference);
						Former.Photo.Medium_Thumbnail = gPictureBLO.Get_URL_Medium_Picture_Path(Default_Details_Former_Model.Photo_Reference);
						Former.Photo.Large_Thumbnail = gPictureBLO.Get_URL_Large_Picture_Path(Default_Details_Former_Model.Photo_Reference);
					}
				}

               
            }
					Former.Cellphone = Default_Details_Former_Model.Cellphone;
			Former.Email = Default_Details_Former_Model.Email;
			Former.Address = Default_Details_Former_Model.Address;
			Former.FaceBook = Default_Details_Former_Model.FaceBook;
			Former.WebSite = Default_Details_Former_Model.WebSite;
			Former.Id = Default_Details_Former_Model.Id;
            return Former;
        }
        public virtual Default_Details_Former_Model ConverTo_Default_Details_Former_Model(Former Former)
        {  
			Default_Details_Former_Model Default_Details_Former_Model = new Default_Details_Former_Model();
			Default_Details_Former_Model.toStringValue = Former.ToString();
			Default_Details_Former_Model.FormerSpecialty = Former.FormerSpecialty;
			Default_Details_Former_Model.WeeklyHourlyMass = Former.WeeklyHourlyMass;
			Default_Details_Former_Model.RegistrationNumber = Former.RegistrationNumber;
			Default_Details_Former_Model.CreateUserAccount = Former.CreateUserAccount;
			Default_Details_Former_Model.Login = Former.Login;
			Default_Details_Former_Model.Password = Former.Password;
			Default_Details_Former_Model.FirstName = Former.FirstName;
			Default_Details_Former_Model.LastName = Former.LastName;
			Default_Details_Former_Model.FirstNameArabe = Former.FirstNameArabe;
			Default_Details_Former_Model.LastNameArabe = Former.LastNameArabe;
			Default_Details_Former_Model.Sex = Former.Sex;
			Default_Details_Former_Model.Birthdate = DefaultDateTime_If_Empty(Former.Birthdate);
			Default_Details_Former_Model.Nationality = Former.Nationality;
			Default_Details_Former_Model.BirthPlace = Former.BirthPlace;
			Default_Details_Former_Model.CIN = Former.CIN;
			Default_Details_Former_Model.Photo = Former.Photo;
			Default_Details_Former_Model.Cellphone = Former.Cellphone;
			Default_Details_Former_Model.Email = Former.Email;
			Default_Details_Former_Model.Address = Former.Address;
			Default_Details_Former_Model.FaceBook = Former.FaceBook;
			Default_Details_Former_Model.WebSite = Former.WebSite;
			Default_Details_Former_Model.Id = Former.Id;
            return Default_Details_Former_Model;            
        }

		public virtual Default_Details_Former_Model CreateNew()
        {
            Former Former = new FormerBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Former_Model Default_Details_Former_Model = this.ConverTo_Default_Details_Former_Model(Former);
            return Default_Details_Former_Model;
        } 

		public virtual List<Default_Details_Former_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            FormerBLO entityBLO = new FormerBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Former> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Former_Model> ls_models = new List<Default_Details_Former_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Former_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Former_ModelBLM : BaseDefault_Details_Former_ModelBLM
	{
		public Default_Details_Former_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_Former_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_Former_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Former_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Former ConverTo_Former(Default_Form_Former_Model Default_Form_Former_Model)
        {
			Former Former = null;
            if (Default_Form_Former_Model.Id != 0)
            {
                Former = new FormerBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Former_Model.Id);
            }
            else
            {
                Former = new Former();
            } 
			Former.FormerSpecialtyId = Default_Form_Former_Model.FormerSpecialtyId;
			Former.FormerSpecialty = new FormerSpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Former_Model.FormerSpecialtyId)) ;
			Former.WeeklyHourlyMass = Default_Form_Former_Model.WeeklyHourlyMass;
			Former.RegistrationNumber = Default_Form_Former_Model.RegistrationNumber;
			Former.CreateUserAccount = Default_Form_Former_Model.CreateUserAccount;
			Former.Login = Default_Form_Former_Model.Login;
			Former.Password = Default_Form_Former_Model.Password;
			Former.FirstName = Default_Form_Former_Model.FirstName;
			Former.LastName = Default_Form_Former_Model.LastName;
			Former.FirstNameArabe = Default_Form_Former_Model.FirstNameArabe;
			Former.LastNameArabe = Default_Form_Former_Model.LastNameArabe;
			Former.Sex = Default_Form_Former_Model.Sex;
			Former.Birthdate = DefaultDateTime_If_Empty(Default_Form_Former_Model.Birthdate);
			Former.NationalityId = Default_Form_Former_Model.NationalityId;
			Former.Nationality = new NationalityBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Former_Model.NationalityId)) ;
			Former.BirthPlace = Default_Form_Former_Model.BirthPlace;
			Former.CIN = Default_Form_Former_Model.CIN;
			if (!string.IsNullOrEmpty(Default_Form_Former_Model.Photo_Reference))
            {
				if(Default_Form_Former_Model.Photo_Reference == "Delete" && Former.Photo != null)
                {
                    Former.Photo.Old_Reference = Former.Photo.Reference;
                    Former.Photo.Reference = "Delete";
                }
                else
				{
					if (Former.Photo == null) Former.Photo = new GPicture() { Old_Reference = "Empty" };
					if (Former.Photo.Reference != Default_Form_Former_Model.Photo_Reference)
					{
						// Save the old reference to be deleted by the save methode 
						if (!string.IsNullOrEmpty(Former.Photo.Reference))
                            Former.Photo.Old_Reference = Former.Photo.Reference;

						 

						GPictureBLO gPictureBLO = new GPictureBLO(this.GAppContext);
						Former.Photo.Reference = Default_Form_Former_Model.Photo_Reference;
                  
						Former.Photo.Original_Thumbnail = gPictureBLO.Get_URL_Original_Picture_Path(Default_Form_Former_Model.Photo_Reference);
						Former.Photo.Small_Thumbnail = gPictureBLO.Get_URL_Small_Picture_Path(Default_Form_Former_Model.Photo_Reference);
						Former.Photo.Medium_Thumbnail = gPictureBLO.Get_URL_Medium_Picture_Path(Default_Form_Former_Model.Photo_Reference);
						Former.Photo.Large_Thumbnail = gPictureBLO.Get_URL_Large_Picture_Path(Default_Form_Former_Model.Photo_Reference);
					}
				}

               
            }
					Former.Cellphone = Default_Form_Former_Model.Cellphone;
			Former.Email = Default_Form_Former_Model.Email;
			Former.Address = Default_Form_Former_Model.Address;
			Former.FaceBook = Default_Form_Former_Model.FaceBook;
			Former.WebSite = Default_Form_Former_Model.WebSite;
			Former.Id = Default_Form_Former_Model.Id;
            return Former;
        }
        public virtual Default_Form_Former_Model ConverTo_Default_Form_Former_Model(Former Former)
        {  
			Default_Form_Former_Model Default_Form_Former_Model = new Default_Form_Former_Model();
			Default_Form_Former_Model.toStringValue = Former.ToString();
			Default_Form_Former_Model.FormerSpecialtyId = Former.FormerSpecialtyId;
			Default_Form_Former_Model.WeeklyHourlyMass = Former.WeeklyHourlyMass;
			Default_Form_Former_Model.RegistrationNumber = Former.RegistrationNumber;
			Default_Form_Former_Model.CreateUserAccount = Former.CreateUserAccount;
			Default_Form_Former_Model.Login = Former.Login;
			Default_Form_Former_Model.Password = Former.Password;
			Default_Form_Former_Model.FirstName = Former.FirstName;
			Default_Form_Former_Model.LastName = Former.LastName;
			Default_Form_Former_Model.FirstNameArabe = Former.FirstNameArabe;
			Default_Form_Former_Model.LastNameArabe = Former.LastNameArabe;
			Default_Form_Former_Model.Sex = Former.Sex;
			Default_Form_Former_Model.Birthdate = DefaultDateTime_If_Empty(Former.Birthdate);
			Default_Form_Former_Model.NationalityId = Former.NationalityId;
			Default_Form_Former_Model.BirthPlace = Former.BirthPlace;
			Default_Form_Former_Model.CIN = Former.CIN;
			Default_Form_Former_Model.Photo = Former.Photo;
			Default_Form_Former_Model.Cellphone = Former.Cellphone;
			Default_Form_Former_Model.Email = Former.Email;
			Default_Form_Former_Model.Address = Former.Address;
			Default_Form_Former_Model.FaceBook = Former.FaceBook;
			Default_Form_Former_Model.WebSite = Former.WebSite;
			Default_Form_Former_Model.Id = Former.Id;
            return Default_Form_Former_Model;            
        }

		public virtual Default_Form_Former_Model CreateNew()
        {
            Former Former = new FormerBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Former_Model Default_Form_Former_Model = this.ConverTo_Default_Form_Former_Model(Former);
            return Default_Form_Former_Model;
        } 

		public virtual List<Default_Form_Former_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            FormerBLO entityBLO = new FormerBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Former> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_Former_Model> ls_models = new List<Default_Form_Former_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_Former_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_Former_ModelBLM : BaseDefault_Form_Former_ModelBLM
	{
		public Default_Form_Former_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = FormerDetailsView

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
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseFormerDetailsViewBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseFormerDetailsViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Former ConverTo_Former(FormerDetailsView FormerDetailsView)
        {
			Former Former = null;
            if (FormerDetailsView.Id != 0)
            {
                Former = new FormerBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(FormerDetailsView.Id);
            }
            else
            {
                Former = new Former();
            } 
			Former.RegistrationNumber = FormerDetailsView.RegistrationNumber;
			Former.FirstName = FormerDetailsView.FirstName;
			Former.LastName = FormerDetailsView.LastName;
			Former.FormerSpecialty = FormerDetailsView.FormerSpecialty;
			Former.WeeklyHourlyMass = FormerDetailsView.WeeklyHourlyMass;
			Former.FirstNameArabe = FormerDetailsView.FirstNameArabe;
			Former.LastNameArabe = FormerDetailsView.LastNameArabe;
			Former.Sex = FormerDetailsView.Sex;
			Former.Nationality = FormerDetailsView.Nationality;
			Former.Birthdate = DefaultDateTime_If_Empty(FormerDetailsView.Birthdate);
			Former.BirthPlace = FormerDetailsView.BirthPlace;
			Former.CIN = FormerDetailsView.CIN;
			Former.Cellphone = FormerDetailsView.Cellphone;
			Former.Email = FormerDetailsView.Email;
			Former.Address = FormerDetailsView.Address;
			Former.Id = FormerDetailsView.Id;
            return Former;
        }
        public virtual FormerDetailsView ConverTo_FormerDetailsView(Former Former)
        {  
			FormerDetailsView FormerDetailsView = new FormerDetailsView();
			FormerDetailsView.toStringValue = Former.ToString();
			FormerDetailsView.FormerSpecialty = Former.FormerSpecialty;
			FormerDetailsView.WeeklyHourlyMass = Former.WeeklyHourlyMass;
			FormerDetailsView.RegistrationNumber = Former.RegistrationNumber;
			FormerDetailsView.FirstName = Former.FirstName;
			FormerDetailsView.LastName = Former.LastName;
			FormerDetailsView.FirstNameArabe = Former.FirstNameArabe;
			FormerDetailsView.LastNameArabe = Former.LastNameArabe;
			FormerDetailsView.Sex = Former.Sex;
			FormerDetailsView.Birthdate = DefaultDateTime_If_Empty(Former.Birthdate);
			FormerDetailsView.Nationality = Former.Nationality;
			FormerDetailsView.BirthPlace = Former.BirthPlace;
			FormerDetailsView.CIN = Former.CIN;
			FormerDetailsView.Cellphone = Former.Cellphone;
			FormerDetailsView.Email = Former.Email;
			FormerDetailsView.Address = Former.Address;
			FormerDetailsView.Id = Former.Id;
            return FormerDetailsView;            
        }

		public virtual FormerDetailsView CreateNew()
        {
            Former Former = new FormerBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            FormerDetailsView FormerDetailsView = this.ConverTo_FormerDetailsView(Former);
            return FormerDetailsView;
        } 

		public virtual List<FormerDetailsView> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            FormerBLO entityBLO = new FormerBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Former> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<FormerDetailsView> ls_models = new List<FormerDetailsView>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_FormerDetailsView(entity));
            }
            return ls_models;
        }


    }

	public partial class FormerDetailsViewBLM : BaseFormerDetailsViewBLM
	{
		public FormerDetailsViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = FormerFormView

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
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseFormerFormViewBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseFormerFormViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Former ConverTo_Former(FormerFormView FormerFormView)
        {
			Former Former = null;
            if (FormerFormView.Id != 0)
            {
                Former = new FormerBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(FormerFormView.Id);
            }
            else
            {
                Former = new Former();
            } 
			Former.RegistrationNumber = FormerFormView.RegistrationNumber;
			Former.FirstName = FormerFormView.FirstName;
			Former.LastName = FormerFormView.LastName;
			if (!string.IsNullOrEmpty(FormerFormView.Photo_Reference))
            {
				if(FormerFormView.Photo_Reference == "Delete" && Former.Photo != null)
                {
                    Former.Photo.Old_Reference = Former.Photo.Reference;
                    Former.Photo.Reference = "Delete";
                }
                else
				{
					if (Former.Photo == null) Former.Photo = new GPicture() { Old_Reference = "Empty" };
					if (Former.Photo.Reference != FormerFormView.Photo_Reference)
					{
						// Save the old reference to be deleted by the save methode 
						if (!string.IsNullOrEmpty(Former.Photo.Reference))
                            Former.Photo.Old_Reference = Former.Photo.Reference;

						 

						GPictureBLO gPictureBLO = new GPictureBLO(this.GAppContext);
						Former.Photo.Reference = FormerFormView.Photo_Reference;
                  
						Former.Photo.Original_Thumbnail = gPictureBLO.Get_URL_Original_Picture_Path(FormerFormView.Photo_Reference);
						Former.Photo.Small_Thumbnail = gPictureBLO.Get_URL_Small_Picture_Path(FormerFormView.Photo_Reference);
						Former.Photo.Medium_Thumbnail = gPictureBLO.Get_URL_Medium_Picture_Path(FormerFormView.Photo_Reference);
						Former.Photo.Large_Thumbnail = gPictureBLO.Get_URL_Large_Picture_Path(FormerFormView.Photo_Reference);
					}
				}

               
            }
					Former.FormerSpecialtyId = FormerFormView.FormerSpecialtyId;
			Former.FormerSpecialty = new FormerSpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(FormerFormView.FormerSpecialtyId)) ;
			Former.WeeklyHourlyMass = FormerFormView.WeeklyHourlyMass;
			Former.FirstNameArabe = FormerFormView.FirstNameArabe;
			Former.LastNameArabe = FormerFormView.LastNameArabe;
			Former.NationalityId = FormerFormView.NationalityId;
			Former.Nationality = new NationalityBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(FormerFormView.NationalityId)) ;
			Former.Sex = FormerFormView.Sex;
			Former.Birthdate = DefaultDateTime_If_Empty(FormerFormView.Birthdate);
			Former.BirthPlace = FormerFormView.BirthPlace;
			Former.CIN = FormerFormView.CIN;
			Former.Cellphone = FormerFormView.Cellphone;
			Former.Email = FormerFormView.Email;
			Former.Address = FormerFormView.Address;
			Former.CreateUserAccount = FormerFormView.CreateUserAccount;
			Former.Login = FormerFormView.Login;
			Former.Password = FormerFormView.Password;
			Former.Id = FormerFormView.Id;
            return Former;
        }
        public virtual FormerFormView ConverTo_FormerFormView(Former Former)
        {  
			FormerFormView FormerFormView = new FormerFormView();
			FormerFormView.toStringValue = Former.ToString();
			FormerFormView.FormerSpecialtyId = Former.FormerSpecialtyId;
			FormerFormView.WeeklyHourlyMass = Former.WeeklyHourlyMass;
			FormerFormView.RegistrationNumber = Former.RegistrationNumber;
			FormerFormView.CreateUserAccount = Former.CreateUserAccount;
			FormerFormView.Login = Former.Login;
			FormerFormView.Password = Former.Password;
			FormerFormView.FirstName = Former.FirstName;
			FormerFormView.LastName = Former.LastName;
			FormerFormView.FirstNameArabe = Former.FirstNameArabe;
			FormerFormView.LastNameArabe = Former.LastNameArabe;
			FormerFormView.Sex = Former.Sex;
			FormerFormView.Birthdate = DefaultDateTime_If_Empty(Former.Birthdate);
			FormerFormView.NationalityId = Former.NationalityId;
			FormerFormView.BirthPlace = Former.BirthPlace;
			FormerFormView.CIN = Former.CIN;
			FormerFormView.Photo = Former.Photo;
			FormerFormView.Cellphone = Former.Cellphone;
			FormerFormView.Email = Former.Email;
			FormerFormView.Address = Former.Address;
			FormerFormView.Id = Former.Id;
            return FormerFormView;            
        }

		public virtual FormerFormView CreateNew()
        {
            Former Former = new FormerBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            FormerFormView FormerFormView = this.ConverTo_FormerFormView(Former);
            return FormerFormView;
        } 

		public virtual List<FormerFormView> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            FormerBLO entityBLO = new FormerBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Former> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<FormerFormView> ls_models = new List<FormerFormView>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_FormerFormView(entity));
            }
            return ls_models;
        }


    }

	public partial class FormerFormViewBLM : BaseFormerFormViewBLM
	{
		public FormerFormViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = FormerIndexView

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
using GApp.Models.Pages;

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
			FormerIndexView.FormerSpecialty = Former.FormerSpecialty;
			FormerIndexView.RegistrationNumber = Former.RegistrationNumber;
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

		public virtual List<FormerIndexView> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            FormerBLO entityBLO = new FormerBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Former> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<FormerIndexView> ls_models = new List<FormerIndexView>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_FormerIndexView(entity));
            }
            return ls_models;
        }


    }

	public partial class FormerIndexViewBLM : BaseFormerIndexViewBLM
	{
		public FormerIndexViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_Function_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_Function_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Function_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Function ConverTo_Function(Default_Details_Function_Model Default_Details_Function_Model)
        {
			Function Function = null;
            if (Default_Details_Function_Model.Id != 0)
            {
                Function = new FunctionBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Function_Model.Id);
            }
            else
            {
                Function = new Function();
            } 
			Function.Code = Default_Details_Function_Model.Code;
			Function.Name = Default_Details_Function_Model.Name;
			Function.Description = Default_Details_Function_Model.Description;
			Function.Id = Default_Details_Function_Model.Id;
            return Function;
        }
        public virtual Default_Details_Function_Model ConverTo_Default_Details_Function_Model(Function Function)
        {  
			Default_Details_Function_Model Default_Details_Function_Model = new Default_Details_Function_Model();
			Default_Details_Function_Model.toStringValue = Function.ToString();
			Default_Details_Function_Model.Code = Function.Code;
			Default_Details_Function_Model.Name = Function.Name;
			Default_Details_Function_Model.Description = Function.Description;
			Default_Details_Function_Model.Id = Function.Id;
            return Default_Details_Function_Model;            
        }

		public virtual Default_Details_Function_Model CreateNew()
        {
            Function Function = new FunctionBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Function_Model Default_Details_Function_Model = this.ConverTo_Default_Details_Function_Model(Function);
            return Default_Details_Function_Model;
        } 

		public virtual List<Default_Details_Function_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            FunctionBLO entityBLO = new FunctionBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Function> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Function_Model> ls_models = new List<Default_Details_Function_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Function_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Function_ModelBLM : BaseDefault_Details_Function_ModelBLM
	{
		public Default_Details_Function_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_Function_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_Function_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Function_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Function ConverTo_Function(Default_Form_Function_Model Default_Form_Function_Model)
        {
			Function Function = null;
            if (Default_Form_Function_Model.Id != 0)
            {
                Function = new FunctionBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Function_Model.Id);
            }
            else
            {
                Function = new Function();
            } 
			Function.Code = Default_Form_Function_Model.Code;
			Function.Name = Default_Form_Function_Model.Name;
			Function.Description = Default_Form_Function_Model.Description;
			Function.Id = Default_Form_Function_Model.Id;
            return Function;
        }
        public virtual Default_Form_Function_Model ConverTo_Default_Form_Function_Model(Function Function)
        {  
			Default_Form_Function_Model Default_Form_Function_Model = new Default_Form_Function_Model();
			Default_Form_Function_Model.toStringValue = Function.ToString();
			Default_Form_Function_Model.Code = Function.Code;
			Default_Form_Function_Model.Name = Function.Name;
			Default_Form_Function_Model.Description = Function.Description;
			Default_Form_Function_Model.Id = Function.Id;
            return Default_Form_Function_Model;            
        }

		public virtual Default_Form_Function_Model CreateNew()
        {
            Function Function = new FunctionBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Function_Model Default_Form_Function_Model = this.ConverTo_Default_Form_Function_Model(Function);
            return Default_Form_Function_Model;
        } 

		public virtual List<Default_Form_Function_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            FunctionBLO entityBLO = new FunctionBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Function> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_Function_Model> ls_models = new List<Default_Form_Function_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_Function_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_Function_ModelBLM : BaseDefault_Form_Function_ModelBLM
	{
		public Default_Form_Function_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_Group_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_Group_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Group_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Group ConverTo_Group(Default_Details_Group_Model Default_Details_Group_Model)
        {
			Group Group = null;
            if (Default_Details_Group_Model.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Group_Model.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.TrainingType = Default_Details_Group_Model.TrainingType;
			Group.TrainingYear = Default_Details_Group_Model.TrainingYear;
			Group.Specialty = Default_Details_Group_Model.Specialty;
			Group.YearStudy = Default_Details_Group_Model.YearStudy;
			Group.Code = Default_Details_Group_Model.Code;
			Group.Description = Default_Details_Group_Model.Description;
			Group.Trainees = Default_Details_Group_Model.Trainees;
			Group.Id = Default_Details_Group_Model.Id;
            return Group;
        }
        public virtual Default_Details_Group_Model ConverTo_Default_Details_Group_Model(Group Group)
        {  
			Default_Details_Group_Model Default_Details_Group_Model = new Default_Details_Group_Model();
			Default_Details_Group_Model.toStringValue = Group.ToString();
			Default_Details_Group_Model.TrainingType = Group.TrainingType;
			Default_Details_Group_Model.TrainingYear = Group.TrainingYear;
			Default_Details_Group_Model.Specialty = Group.Specialty;
			Default_Details_Group_Model.YearStudy = Group.YearStudy;
			Default_Details_Group_Model.Code = Group.Code;
			Default_Details_Group_Model.Description = Group.Description;
			Default_Details_Group_Model.Trainees = Group.Trainees;
			Default_Details_Group_Model.Id = Group.Id;
            return Default_Details_Group_Model;            
        }

		public virtual Default_Details_Group_Model CreateNew()
        {
            Group Group = new GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Group_Model Default_Details_Group_Model = this.ConverTo_Default_Details_Group_Model(Group);
            return Default_Details_Group_Model;
        } 

		public virtual List<Default_Details_Group_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            GroupBLO entityBLO = new GroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Group> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Group_Model> ls_models = new List<Default_Details_Group_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Group_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Group_ModelBLM : BaseDefault_Details_Group_ModelBLM
	{
		public Default_Details_Group_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_Group_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_Group_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Group_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Group ConverTo_Group(Default_Form_Group_Model Default_Form_Group_Model)
        {
			Group Group = null;
            if (Default_Form_Group_Model.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Group_Model.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.TrainingTypeId = Default_Form_Group_Model.TrainingTypeId;
			Group.TrainingType = new TrainingTypeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Group_Model.TrainingTypeId)) ;
			Group.TrainingYearId = Default_Form_Group_Model.TrainingYearId;
			Group.TrainingYear = new TrainingYearBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Group_Model.TrainingYearId)) ;
			Group.SpecialtyId = Default_Form_Group_Model.SpecialtyId;
			Group.Specialty = new SpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Group_Model.SpecialtyId)) ;
			Group.YearStudyId = Default_Form_Group_Model.YearStudyId;
			Group.YearStudy = new YearStudyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Group_Model.YearStudyId)) ;
			Group.Code = Default_Form_Group_Model.Code;
			Group.Description = Default_Form_Group_Model.Description;
			// Trainee
            TraineeBLO TraineeBLO = new TraineeBLO(this.UnitOfWork,this.GAppContext);

			if (Group.Trainees != null)
                Group.Trainees.Clear();
            else
                Group.Trainees = new List<Trainee>();

			if(Default_Form_Group_Model.Selected_Trainees != null)
			{
				foreach (string Selected_Trainee_Id in Default_Form_Group_Model.Selected_Trainees)
				{
					Int64 Selected_Trainee_Id_Int64 = Convert.ToInt64(Selected_Trainee_Id);
					Trainee Trainee =TraineeBLO.FindBaseEntityByID(Selected_Trainee_Id_Int64);
					Group.Trainees.Add(Trainee);
				}
			}
	
			Group.Id = Default_Form_Group_Model.Id;
            return Group;
        }
        public virtual Default_Form_Group_Model ConverTo_Default_Form_Group_Model(Group Group)
        {  
			Default_Form_Group_Model Default_Form_Group_Model = new Default_Form_Group_Model();
			Default_Form_Group_Model.toStringValue = Group.ToString();
			Default_Form_Group_Model.TrainingTypeId = Group.TrainingTypeId;
			Default_Form_Group_Model.TrainingYearId = Group.TrainingYearId;
			Default_Form_Group_Model.SpecialtyId = Group.SpecialtyId;
			Default_Form_Group_Model.YearStudyId = Group.YearStudyId;
			Default_Form_Group_Model.Code = Group.Code;
			Default_Form_Group_Model.Description = Group.Description;

			// Trainee
            if (Group.Trainees != null && Group.Trainees.Count > 0)
            {
                Default_Form_Group_Model.Selected_Trainees = Group
                                                        .Trainees
                                                        .Select(entity => entity.Id.ToString())
                                                        .ToList<string>();
            }  
            else
            {
                Default_Form_Group_Model.Selected_Trainees = new List<string>();
            }			
			Default_Form_Group_Model.Id = Group.Id;
            return Default_Form_Group_Model;            
        }

		public virtual Default_Form_Group_Model CreateNew()
        {
            Group Group = new GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Group_Model Default_Form_Group_Model = this.ConverTo_Default_Form_Group_Model(Group);
            return Default_Form_Group_Model;
        } 

		public virtual List<Default_Form_Group_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            GroupBLO entityBLO = new GroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Group> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_Group_Model> ls_models = new List<Default_Form_Group_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_Group_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_Group_ModelBLM : BaseDefault_Form_Group_ModelBLM
	{
		public Default_Form_Group_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = TrainingFormView

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
using TrainingIS.Entities.ModelsViews.Trainings;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseTrainingFormViewBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseTrainingFormViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Group ConverTo_Group(TrainingFormView TrainingFormView)
        {
			Group Group = null;
            if (TrainingFormView.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(TrainingFormView.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.TrainingYearId = TrainingFormView.TrainingYearId;
			Group.TrainingYear = new TrainingYearBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(TrainingFormView.TrainingYearId)) ;
			Group.SpecialtyId = TrainingFormView.SpecialtyId;
			Group.Specialty = new SpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(TrainingFormView.SpecialtyId)) ;
			Group.Code = TrainingFormView.Code;
			Group.Description = TrainingFormView.Description;
			Group.Id = TrainingFormView.Id;
            return Group;
        }
        public virtual TrainingFormView ConverTo_TrainingFormView(Group Group)
        {  
			TrainingFormView TrainingFormView = new TrainingFormView();
			TrainingFormView.toStringValue = Group.ToString();
			TrainingFormView.TrainingYearId = Group.TrainingYearId;
			TrainingFormView.SpecialtyId = Group.SpecialtyId;
			TrainingFormView.Code = Group.Code;
			TrainingFormView.Description = Group.Description;
			TrainingFormView.Id = Group.Id;
            return TrainingFormView;            
        }

		public virtual TrainingFormView CreateNew()
        {
            Group Group = new GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            TrainingFormView TrainingFormView = this.ConverTo_TrainingFormView(Group);
            return TrainingFormView;
        } 

		public virtual List<TrainingFormView> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            GroupBLO entityBLO = new GroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Group> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<TrainingFormView> ls_models = new List<TrainingFormView>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_TrainingFormView(entity));
            }
            return ls_models;
        }


    }

	public partial class TrainingFormViewBLM : BaseTrainingFormViewBLM
	{
		public TrainingFormViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = CreateGroupView

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
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseCreateGroupViewBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseCreateGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Group ConverTo_Group(CreateGroupView CreateGroupView)
        {
			Group Group = null;
            if (CreateGroupView.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(CreateGroupView.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.TrainingYear = CreateGroupView.TrainingYear;
			Group.TrainingYearId = CreateGroupView.TrainingYearId;
			Group.TrainingYear = new TrainingYearBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(CreateGroupView.TrainingYearId)) ;
			Group.Specialty = CreateGroupView.Specialty;
			Group.SpecialtyId = CreateGroupView.SpecialtyId;
			Group.Specialty = new SpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(CreateGroupView.SpecialtyId)) ;
			Group.TrainingType = CreateGroupView.TrainingType;
			Group.TrainingTypeId = CreateGroupView.TrainingTypeId;
			Group.TrainingType = new TrainingTypeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(CreateGroupView.TrainingTypeId)) ;
			Group.YearStudy = CreateGroupView.YearStudy;
			Group.YearStudyId = CreateGroupView.YearStudyId;
			Group.YearStudy = new YearStudyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(CreateGroupView.YearStudyId)) ;
			Group.Code = CreateGroupView.Code;
			Group.Id = CreateGroupView.Id;
            return Group;
        }
        public virtual CreateGroupView ConverTo_CreateGroupView(Group Group)
        {  
			CreateGroupView CreateGroupView = new CreateGroupView();
			CreateGroupView.toStringValue = Group.ToString();
			CreateGroupView.TrainingType = Group.TrainingType;
			CreateGroupView.TrainingTypeId = Group.TrainingTypeId;
			CreateGroupView.TrainingYear = Group.TrainingYear;
			CreateGroupView.TrainingYearId = Group.TrainingYearId;
			CreateGroupView.Specialty = Group.Specialty;
			CreateGroupView.SpecialtyId = Group.SpecialtyId;
			CreateGroupView.YearStudy = Group.YearStudy;
			CreateGroupView.YearStudyId = Group.YearStudyId;
			CreateGroupView.Code = Group.Code;
			CreateGroupView.Id = Group.Id;
            return CreateGroupView;            
        }

		public virtual CreateGroupView CreateNew()
        {
            Group Group = new GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            CreateGroupView CreateGroupView = this.ConverTo_CreateGroupView(Group);
            return CreateGroupView;
        } 

		public virtual List<CreateGroupView> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            GroupBLO entityBLO = new GroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Group> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<CreateGroupView> ls_models = new List<CreateGroupView>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_CreateGroupView(entity));
            }
            return ls_models;
        }


    }

	public partial class CreateGroupViewBLM : BaseCreateGroupViewBLM
	{
		public CreateGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = DetailsGroupView

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
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDetailsGroupViewBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDetailsGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Group ConverTo_Group(DetailsGroupView DetailsGroupView)
        {
			Group Group = null;
            if (DetailsGroupView.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(DetailsGroupView.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.Code = DetailsGroupView.Code;
			Group.YearStudy = DetailsGroupView.YearStudy;
			Group.Specialty = DetailsGroupView.Specialty;
			Group.TrainingType = DetailsGroupView.TrainingType;
			Group.Id = DetailsGroupView.Id;
            return Group;
        }
        public virtual DetailsGroupView ConverTo_DetailsGroupView(Group Group)
        {  
			DetailsGroupView DetailsGroupView = new DetailsGroupView();
			DetailsGroupView.toStringValue = Group.ToString();
			DetailsGroupView.TrainingType = Group.TrainingType;
			DetailsGroupView.Specialty = Group.Specialty;
			DetailsGroupView.YearStudy = Group.YearStudy;
			DetailsGroupView.Code = Group.Code;
			DetailsGroupView.Id = Group.Id;
            return DetailsGroupView;            
        }

		public virtual DetailsGroupView CreateNew()
        {
            Group Group = new GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            DetailsGroupView DetailsGroupView = this.ConverTo_DetailsGroupView(Group);
            return DetailsGroupView;
        } 

		public virtual List<DetailsGroupView> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            GroupBLO entityBLO = new GroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Group> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<DetailsGroupView> ls_models = new List<DetailsGroupView>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_DetailsGroupView(entity));
            }
            return ls_models;
        }


    }

	public partial class DetailsGroupViewBLM : BaseDetailsGroupViewBLM
	{
		public DetailsGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = EditGroupView

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
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseEditGroupViewBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseEditGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Group ConverTo_Group(EditGroupView EditGroupView)
        {
			Group Group = null;
            if (EditGroupView.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(EditGroupView.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.TrainingYear = EditGroupView.TrainingYear;
			Group.TrainingYearId = EditGroupView.TrainingYearId;
			Group.TrainingYear = new TrainingYearBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(EditGroupView.TrainingYearId)) ;
			Group.Specialty = EditGroupView.Specialty;
			Group.SpecialtyId = EditGroupView.SpecialtyId;
			Group.Specialty = new SpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(EditGroupView.SpecialtyId)) ;
			Group.TrainingType = EditGroupView.TrainingType;
			Group.TrainingTypeId = EditGroupView.TrainingTypeId;
			Group.TrainingType = new TrainingTypeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(EditGroupView.TrainingTypeId)) ;
			Group.YearStudy = EditGroupView.YearStudy;
			Group.YearStudyId = EditGroupView.YearStudyId;
			Group.YearStudy = new YearStudyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(EditGroupView.YearStudyId)) ;
			Group.Code = EditGroupView.Code;
			Group.Id = EditGroupView.Id;
            return Group;
        }
        public virtual EditGroupView ConverTo_EditGroupView(Group Group)
        {  
			EditGroupView EditGroupView = new EditGroupView();
			EditGroupView.toStringValue = Group.ToString();
			EditGroupView.TrainingType = Group.TrainingType;
			EditGroupView.TrainingTypeId = Group.TrainingTypeId;
			EditGroupView.TrainingYear = Group.TrainingYear;
			EditGroupView.TrainingYearId = Group.TrainingYearId;
			EditGroupView.Specialty = Group.Specialty;
			EditGroupView.SpecialtyId = Group.SpecialtyId;
			EditGroupView.YearStudy = Group.YearStudy;
			EditGroupView.YearStudyId = Group.YearStudyId;
			EditGroupView.Code = Group.Code;
			EditGroupView.Id = Group.Id;
            return EditGroupView;            
        }

		public virtual EditGroupView CreateNew()
        {
            Group Group = new GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            EditGroupView EditGroupView = this.ConverTo_EditGroupView(Group);
            return EditGroupView;
        } 

		public virtual List<EditGroupView> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            GroupBLO entityBLO = new GroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Group> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<EditGroupView> ls_models = new List<EditGroupView>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_EditGroupView(entity));
            }
            return ls_models;
        }


    }

	public partial class EditGroupViewBLM : BaseEditGroupViewBLM
	{
		public EditGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = IndexGroupView

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
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseIndexGroupViewBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseIndexGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Group ConverTo_Group(IndexGroupView IndexGroupView)
        {
			Group Group = null;
            if (IndexGroupView.Id != 0)
            {
                Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(IndexGroupView.Id);
            }
            else
            {
                Group = new Group();
            } 
			Group.Code = IndexGroupView.Code;
			Group.YearStudy = IndexGroupView.YearStudy;
			Group.Specialty = IndexGroupView.Specialty;
			Group.TrainingType = IndexGroupView.TrainingType;
			Group.Id = IndexGroupView.Id;
            return Group;
        }
        public virtual IndexGroupView ConverTo_IndexGroupView(Group Group)
        {  
			IndexGroupView IndexGroupView = new IndexGroupView();
			IndexGroupView.toStringValue = Group.ToString();
			IndexGroupView.TrainingType = Group.TrainingType;
			IndexGroupView.Specialty = Group.Specialty;
			IndexGroupView.YearStudy = Group.YearStudy;
			IndexGroupView.Code = Group.Code;
			IndexGroupView.Id = Group.Id;
            return IndexGroupView;            
        }

		public virtual IndexGroupView CreateNew()
        {
            Group Group = new GroupBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            IndexGroupView IndexGroupView = this.ConverTo_IndexGroupView(Group);
            return IndexGroupView;
        } 

		public virtual List<IndexGroupView> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            GroupBLO entityBLO = new GroupBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Group> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<IndexGroupView> ls_models = new List<IndexGroupView>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_IndexGroupView(entity));
            }
            return ls_models;
        }


    }

	public partial class IndexGroupViewBLM : BaseIndexGroupViewBLM
	{
		public IndexGroupViewBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_JustificationAbsence_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_JustificationAbsence_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_JustificationAbsence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual JustificationAbsence ConverTo_JustificationAbsence(Default_Details_JustificationAbsence_Model Default_Details_JustificationAbsence_Model)
        {
			JustificationAbsence JustificationAbsence = null;
            if (Default_Details_JustificationAbsence_Model.Id != 0)
            {
                JustificationAbsence = new JustificationAbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_JustificationAbsence_Model.Id);
            }
            else
            {
                JustificationAbsence = new JustificationAbsence();
            } 
			JustificationAbsence.Trainee = Default_Details_JustificationAbsence_Model.Trainee;
			JustificationAbsence.Category_JustificationAbsence = Default_Details_JustificationAbsence_Model.Category_JustificationAbsence;
			JustificationAbsence.StartDate = DefaultDateTime_If_Empty(Default_Details_JustificationAbsence_Model.StartDate);
			JustificationAbsence.EndtDate = DefaultDateTime_If_Empty(Default_Details_JustificationAbsence_Model.EndtDate);
			JustificationAbsence.Description = Default_Details_JustificationAbsence_Model.Description;
			JustificationAbsence.Id = Default_Details_JustificationAbsence_Model.Id;
            return JustificationAbsence;
        }
        public virtual Default_Details_JustificationAbsence_Model ConverTo_Default_Details_JustificationAbsence_Model(JustificationAbsence JustificationAbsence)
        {  
			Default_Details_JustificationAbsence_Model Default_Details_JustificationAbsence_Model = new Default_Details_JustificationAbsence_Model();
			Default_Details_JustificationAbsence_Model.toStringValue = JustificationAbsence.ToString();
			Default_Details_JustificationAbsence_Model.Trainee = JustificationAbsence.Trainee;
			Default_Details_JustificationAbsence_Model.Category_JustificationAbsence = JustificationAbsence.Category_JustificationAbsence;
			Default_Details_JustificationAbsence_Model.StartDate = DefaultDateTime_If_Empty(JustificationAbsence.StartDate);
			Default_Details_JustificationAbsence_Model.EndtDate = DefaultDateTime_If_Empty(JustificationAbsence.EndtDate);
			Default_Details_JustificationAbsence_Model.Description = JustificationAbsence.Description;
			Default_Details_JustificationAbsence_Model.Id = JustificationAbsence.Id;
            return Default_Details_JustificationAbsence_Model;            
        }

		public virtual Default_Details_JustificationAbsence_Model CreateNew()
        {
            JustificationAbsence JustificationAbsence = new JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_JustificationAbsence_Model Default_Details_JustificationAbsence_Model = this.ConverTo_Default_Details_JustificationAbsence_Model(JustificationAbsence);
            return Default_Details_JustificationAbsence_Model;
        } 

		public virtual List<Default_Details_JustificationAbsence_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            JustificationAbsenceBLO entityBLO = new JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<JustificationAbsence> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_JustificationAbsence_Model> ls_models = new List<Default_Details_JustificationAbsence_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_JustificationAbsence_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_JustificationAbsence_ModelBLM : BaseDefault_Details_JustificationAbsence_ModelBLM
	{
		public Default_Details_JustificationAbsence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_JustificationAbsence_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_JustificationAbsence_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_JustificationAbsence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual JustificationAbsence ConverTo_JustificationAbsence(Default_Form_JustificationAbsence_Model Default_Form_JustificationAbsence_Model)
        {
			JustificationAbsence JustificationAbsence = null;
            if (Default_Form_JustificationAbsence_Model.Id != 0)
            {
                JustificationAbsence = new JustificationAbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_JustificationAbsence_Model.Id);
            }
            else
            {
                JustificationAbsence = new JustificationAbsence();
            } 
			JustificationAbsence.TraineeId = Default_Form_JustificationAbsence_Model.TraineeId;
			JustificationAbsence.Trainee = new TraineeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_JustificationAbsence_Model.TraineeId)) ;
			JustificationAbsence.Category_JustificationAbsenceId = Default_Form_JustificationAbsence_Model.Category_JustificationAbsenceId;
			JustificationAbsence.Category_JustificationAbsence = new Category_JustificationAbsenceBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_JustificationAbsence_Model.Category_JustificationAbsenceId)) ;
			JustificationAbsence.StartDate = DefaultDateTime_If_Empty(Default_Form_JustificationAbsence_Model.StartDate);
			JustificationAbsence.EndtDate = DefaultDateTime_If_Empty(Default_Form_JustificationAbsence_Model.EndtDate);
			JustificationAbsence.Description = Default_Form_JustificationAbsence_Model.Description;
			JustificationAbsence.Id = Default_Form_JustificationAbsence_Model.Id;
            return JustificationAbsence;
        }
        public virtual Default_Form_JustificationAbsence_Model ConverTo_Default_Form_JustificationAbsence_Model(JustificationAbsence JustificationAbsence)
        {  
			Default_Form_JustificationAbsence_Model Default_Form_JustificationAbsence_Model = new Default_Form_JustificationAbsence_Model();
			Default_Form_JustificationAbsence_Model.toStringValue = JustificationAbsence.ToString();
			Default_Form_JustificationAbsence_Model.TraineeId = JustificationAbsence.TraineeId;
			Default_Form_JustificationAbsence_Model.Category_JustificationAbsenceId = JustificationAbsence.Category_JustificationAbsenceId;
			Default_Form_JustificationAbsence_Model.StartDate = DefaultDateTime_If_Empty(JustificationAbsence.StartDate);
			Default_Form_JustificationAbsence_Model.EndtDate = DefaultDateTime_If_Empty(JustificationAbsence.EndtDate);
			Default_Form_JustificationAbsence_Model.Description = JustificationAbsence.Description;
			Default_Form_JustificationAbsence_Model.Id = JustificationAbsence.Id;
            return Default_Form_JustificationAbsence_Model;            
        }

		public virtual Default_Form_JustificationAbsence_Model CreateNew()
        {
            JustificationAbsence JustificationAbsence = new JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_JustificationAbsence_Model Default_Form_JustificationAbsence_Model = this.ConverTo_Default_Form_JustificationAbsence_Model(JustificationAbsence);
            return Default_Form_JustificationAbsence_Model;
        } 

		public virtual List<Default_Form_JustificationAbsence_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            JustificationAbsenceBLO entityBLO = new JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<JustificationAbsence> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_JustificationAbsence_Model> ls_models = new List<Default_Form_JustificationAbsence_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_JustificationAbsence_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_JustificationAbsence_ModelBLM : BaseDefault_Form_JustificationAbsence_ModelBLM
	{
		public Default_Form_JustificationAbsence_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_LogWork_Model

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
using TrainingIS.Entities.ModelsViews;
using GApp.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_LogWork_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_LogWork_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual LogWork ConverTo_LogWork(Default_Details_LogWork_Model Default_Details_LogWork_Model)
        {
			LogWork LogWork = null;
            if (Default_Details_LogWork_Model.Id != 0)
            {
                LogWork = new LogWorkBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_LogWork_Model.Id);
            }
            else
            {
                LogWork = new LogWork();
            } 
			LogWork.UserId = Default_Details_LogWork_Model.UserId;
			LogWork.OperationWorkType = Default_Details_LogWork_Model.OperationWorkType;
			LogWork.OperationReference = Default_Details_LogWork_Model.OperationReference;
			LogWork.EntityType = Default_Details_LogWork_Model.EntityType;
			LogWork.Description = Default_Details_LogWork_Model.Description;
			LogWork.Id = Default_Details_LogWork_Model.Id;
            return LogWork;
        }
        public virtual Default_Details_LogWork_Model ConverTo_Default_Details_LogWork_Model(LogWork LogWork)
        {  
			Default_Details_LogWork_Model Default_Details_LogWork_Model = new Default_Details_LogWork_Model();
			Default_Details_LogWork_Model.toStringValue = LogWork.ToString();
			Default_Details_LogWork_Model.UserId = LogWork.UserId;
			Default_Details_LogWork_Model.OperationWorkType = LogWork.OperationWorkType;
			Default_Details_LogWork_Model.OperationReference = LogWork.OperationReference;
			Default_Details_LogWork_Model.EntityType = LogWork.EntityType;
			Default_Details_LogWork_Model.Description = LogWork.Description;
			Default_Details_LogWork_Model.Id = LogWork.Id;
            return Default_Details_LogWork_Model;            
        }

		public virtual Default_Details_LogWork_Model CreateNew()
        {
            LogWork LogWork = new LogWorkBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_LogWork_Model Default_Details_LogWork_Model = this.ConverTo_Default_Details_LogWork_Model(LogWork);
            return Default_Details_LogWork_Model;
        } 

		public virtual List<Default_Details_LogWork_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            LogWorkBLO entityBLO = new LogWorkBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<LogWork> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_LogWork_Model> ls_models = new List<Default_Details_LogWork_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_LogWork_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_LogWork_ModelBLM : BaseDefault_Details_LogWork_ModelBLM
	{
		public Default_Details_LogWork_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_LogWork_Model

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
using TrainingIS.Entities.ModelsViews;
using GApp.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_LogWork_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_LogWork_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual LogWork ConverTo_LogWork(Default_Form_LogWork_Model Default_Form_LogWork_Model)
        {
			LogWork LogWork = null;
            if (Default_Form_LogWork_Model.Id != 0)
            {
                LogWork = new LogWorkBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_LogWork_Model.Id);
            }
            else
            {
                LogWork = new LogWork();
            } 
			LogWork.UserId = Default_Form_LogWork_Model.UserId;
			LogWork.OperationWorkType = Default_Form_LogWork_Model.OperationWorkType;
			LogWork.OperationReference = Default_Form_LogWork_Model.OperationReference;
			LogWork.EntityType = Default_Form_LogWork_Model.EntityType;
			LogWork.Description = Default_Form_LogWork_Model.Description;
			LogWork.Id = Default_Form_LogWork_Model.Id;
            return LogWork;
        }
        public virtual Default_Form_LogWork_Model ConverTo_Default_Form_LogWork_Model(LogWork LogWork)
        {  
			Default_Form_LogWork_Model Default_Form_LogWork_Model = new Default_Form_LogWork_Model();
			Default_Form_LogWork_Model.toStringValue = LogWork.ToString();
			Default_Form_LogWork_Model.UserId = LogWork.UserId;
			Default_Form_LogWork_Model.OperationWorkType = LogWork.OperationWorkType;
			Default_Form_LogWork_Model.OperationReference = LogWork.OperationReference;
			Default_Form_LogWork_Model.EntityType = LogWork.EntityType;
			Default_Form_LogWork_Model.Description = LogWork.Description;
			Default_Form_LogWork_Model.Id = LogWork.Id;
            return Default_Form_LogWork_Model;            
        }

		public virtual Default_Form_LogWork_Model CreateNew()
        {
            LogWork LogWork = new LogWorkBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_LogWork_Model Default_Form_LogWork_Model = this.ConverTo_Default_Form_LogWork_Model(LogWork);
            return Default_Form_LogWork_Model;
        } 

		public virtual List<Default_Form_LogWork_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            LogWorkBLO entityBLO = new LogWorkBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<LogWork> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_LogWork_Model> ls_models = new List<Default_Form_LogWork_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_LogWork_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_LogWork_ModelBLM : BaseDefault_Form_LogWork_ModelBLM
	{
		public Default_Form_LogWork_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Details_Meeting_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_Meeting_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_Meeting_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Meeting ConverTo_Meeting(Default_Details_Meeting_Model Default_Details_Meeting_Model)
        {
			Meeting Meeting = null;
            if (Default_Details_Meeting_Model.Id != 0)
            {
                Meeting = new MeetingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_Meeting_Model.Id);
            }
            else
            {
                Meeting = new Meeting();
            } 
			Meeting.MeetingDate = DefaultDateTime_If_Empty(Default_Details_Meeting_Model.MeetingDate);
			Meeting.WorkGroup = Default_Details_Meeting_Model.WorkGroup;
			Meeting.Mission_Working_Group = Default_Details_Meeting_Model.Mission_Working_Group;
			Meeting.Formers = Default_Details_Meeting_Model.Formers;
			Meeting.Administrators = Default_Details_Meeting_Model.Administrators;
			Meeting.Trainees = Default_Details_Meeting_Model.Trainees;
			Meeting.Description = Default_Details_Meeting_Model.Description;
			Meeting.Id = Default_Details_Meeting_Model.Id;
            return Meeting;
        }
        public virtual Default_Details_Meeting_Model ConverTo_Default_Details_Meeting_Model(Meeting Meeting)
        {  
			Default_Details_Meeting_Model Default_Details_Meeting_Model = new Default_Details_Meeting_Model();
			Default_Details_Meeting_Model.toStringValue = Meeting.ToString();
			Default_Details_Meeting_Model.MeetingDate = DefaultDateTime_If_Empty(Meeting.MeetingDate);
			Default_Details_Meeting_Model.WorkGroup = Meeting.WorkGroup;
			Default_Details_Meeting_Model.Mission_Working_Group = Meeting.Mission_Working_Group;
			Default_Details_Meeting_Model.Formers = Meeting.Formers;
			Default_Details_Meeting_Model.Administrators = Meeting.Administrators;
			Default_Details_Meeting_Model.Trainees = Meeting.Trainees;
			Default_Details_Meeting_Model.Description = Meeting.Description;
			Default_Details_Meeting_Model.Id = Meeting.Id;
            return Default_Details_Meeting_Model;            
        }

		public virtual Default_Details_Meeting_Model CreateNew()
        {
            Meeting Meeting = new MeetingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_Meeting_Model Default_Details_Meeting_Model = this.ConverTo_Default_Details_Meeting_Model(Meeting);
            return Default_Details_Meeting_Model;
        } 

		public virtual List<Default_Details_Meeting_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            MeetingBLO entityBLO = new MeetingBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Meeting> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_Meeting_Model> ls_models = new List<Default_Details_Meeting_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_Meeting_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_Meeting_ModelBLM : BaseDefault_Details_Meeting_ModelBLM
	{
		public Default_Details_Meeting_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
//modelType = Default_Form_Meeting_Model

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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_Meeting_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Meeting_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Meeting ConverTo_Meeting(Default_Form_Meeting_Model Default_Form_Meeting_Model)
        {
			Meeting Meeting = null;
            if (Default_Form_Meeting_Model.Id != 0)
            {
                Meeting = new MeetingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Meeting_Model.Id);
            }
            else
            {
                Meeting = new Meeting();
            } 
			Meeting.MeetingDate = DefaultDateTime_If_Empty(Default_Form_Meeting_Model.MeetingDate);
			Meeting.WorkGroupId = Default_Form_Meeting_Model.WorkGroupId;
			Meeting.WorkGroup = new WorkGroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Meeting_Model.WorkGroupId)) ;
			Meeting.Mission_Working_GroupId = Default_Form_Meeting_Model.Mission_Working_GroupId;
			Meeting.Mission_Working_Group = new Mission_Working_GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Meeting_Model.Mission_Working_GroupId)) ;
