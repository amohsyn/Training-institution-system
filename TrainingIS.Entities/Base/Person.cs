using GApp.Models.DataAnnotations;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.NationalityResources;
using GApp.Entities.Resources.PersonResources;

namespace TrainingIS.Entities.Base
{
 
    public class Person : BaseEntity
    {

        #region Civil Status
        // 
        // CivilStatus
        //
        [Required]
        [Display(Name = "FirstName", GroupName = "CivilStatus", Order =1,    ResourceType = typeof(msg_Person))]
        public string FirstName { set; get; }

        [Required]
        [Display(Name = "LastName", GroupName = "CivilStatus", Order = 2, ResourceType = typeof(msg_Person))]
        public string LastName { set; get; }

        [GAppDataTable(isColumn = false)]
        [Display(Name = "FirstNameArabe", GroupName = "CivilStatus", Order = 3, ResourceType = typeof(msg_Person))]
        public string FirstNameArabe { set; get; }

        [GAppDataTable(isColumn = false)]
        [Display(Name = "LastNameArabe", GroupName = "CivilStatus", Order = 4, ResourceType = typeof(msg_Person))]
        public string LastNameArabe { set; get; }

        [Required]
        [GAppDataTable(isColumn = false)]
        [Display(Name = "Sex", Order = 5, GroupName = "CivilStatus", ResourceType = typeof(msg_Person))]
        public SexEnum Sex { set; get; }

        [GAppDataTable(isColumn = false)]
        [Display(Name = "Birthdate", GroupName = "CivilStatus", Order =6, ResourceType = typeof(msg_Person))]
        public DateTime Birthdate { set; get; }

        // Nationality
        [GAppDataTable(isColumn = false)]
        [Display(Name = "SingularName", Order = 7, ResourceType = typeof(msg_Nationality))]
        public virtual Nationality Nationality { set; get; }
        [Display(Name = "SingularName", Order = 8, ResourceType = typeof(msg_Nationality))]
        public long NationalityId { set; get; }

        [GAppDataTable(isColumn = false)]
        [Display(Name = "BirthPlace", GroupName = "CivilStatus", Order = 9, ResourceType = typeof(msg_Person))]
        public string BirthPlace { set; get; }

        [GAppDataTable(isColumn = false)]
        [Display(Name = "CIN", GroupName = "CivilStatus", Order = 10, ResourceType = typeof(msg_Person))]
        [Unique]
        public string CIN { set; get; }
        #endregion

        #region Photo
        [Display(Name = "Photo", GroupName = "Photo", Order = 1, ResourceType = typeof(msg_Person))]
        [GAppDataTable(AutoGenerateFilter = false, SearchBy = "Photo.Description", OrderBy = "Photo.UpdateDate", PropertyPath = "Photo")]
        public virtual GPicture Photo { set; get; }



        #endregion

        #region ContactInformation
        //
        // ContactInformation
        //
        [GAppDataTable(isColumn = false)]
        [Display(Name = "Cellphone", GroupName = "ContactInformation", Order =20, ResourceType = typeof(msg_Person))]
        public string Cellphone { set; get; }

        private string _Email = null;
        [GAppDataTable(isColumn = false)]
        [Display(Name = "Email", GroupName = "ContactInformation", Order = 21, ResourceType = typeof(msg_Person))]
        [Unique]
        [EmailAddress]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string Email {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _Email = null;
                }
                else
                {
                    _Email = value;
                }
               
            }
            get {
                return _Email;
            }

        }

        [GAppDataTable(isColumn = false)]
        [Display(Name = "Address", GroupName = "ContactInformation", Order = 22, ResourceType = typeof(msg_Person))]
        public string Address { set; get; }

        [GAppDataTable(isColumn = false)]
        [Display(Name = "FaceBook", GroupName = "ContactInformation", Order = 23, ResourceType = typeof(msg_Person))]
        public string FaceBook { set; get; }

        [GAppDataTable(isColumn = false)]
        [Display(Name = "WebSite", GroupName = "ContactInformation", Order = 24, ResourceType = typeof(msg_Person))]
        public string WebSite { set; get; }
        #endregion
    }
}
