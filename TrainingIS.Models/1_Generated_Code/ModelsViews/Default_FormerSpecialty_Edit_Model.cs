﻿using GApp.Core.Entities.ModelsViews;
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
using GApp.Entities.Resources.AppResources; 
using GApp.Entities.Resources.BaseEntity; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(FormerSpecialty))]
    public class Default_FormerSpecialty_Edit_Model : Default_Form_FormerSpecialty_Model
    {

    }
}    
