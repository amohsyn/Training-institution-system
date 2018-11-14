using GApp.Core.Entities.ModelsViews;
using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Models.DataAnnotations;
using GApp.Models;
using GApp.Entities;
using TrainingIS.Entities.enums;
using System.ComponentModel.DataAnnotations;

using GApp.Entities.Resources.BaseEntity;  
 
namespace TrainingIS.Entities.ModelsViews
{
    [FormView(typeof(GPicture))]
    public class Default_Form_GPicture_Model : BaseModel
    {
		public String Name  {set; get;}  
   
		public String Description  {set; get;}  
   
		public String Original_Thumbnail  {set; get;}  
   
		public String Large_Thumbnail  {set; get;}  
   
		public String Medium_Thumbnail  {set; get;}  
   
		public String Small_Thumbnail  {set; get;}  
   
		public String Old_Reference  {set; get;}  
   
		[Unique]
		[Display(Name = "Reference", Order = 0, ResourceType = typeof(msg_BaseEntity))]
		[GAppDataTable(PropertyPath = "Reference", FilterBy = "Reference", SearchBy = "Reference", OrderBy = "Reference",  AutoGenerateFilter = false,isColumn = false )]
		public String Reference  {set; get;}  
   
    }
}    
