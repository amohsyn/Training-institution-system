using GApp.Models.DataAnnotations;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.ClassroomCategoryResources;
using TrainingIS.Entities.Base;
namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class Classroom : BaseEntity
    {
        public override string ToString()
        {
            return this.Code;
        }

        public override string CalculateReference()
        {
            string reference = string.Format("{0}", this.Code);
            return reference;
        }

        [Unique]
        [Required]
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

     
        [Display(Name = "Name", ResourceType = typeof(msg_app))]
        public string Name { get; set; }

        
        [Display(Name = "SingularName", ResourceType = typeof(msg_ClassroomCategory))]
        public virtual ClassroomCategory ClassroomCategory { get; set; }

        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_ClassroomCategory))]
        public long ClassroomCategoryId { get; set; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }
    }
}
