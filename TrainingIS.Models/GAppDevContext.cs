﻿using GApp.Dev.Generator.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews.FormerModelsViews;

namespace TrainingIS.Models
{
    public class GAppDevContext : IGAppDevContext
    {

        #region Models Types
        public List<Assembly> Get_All_Solution_Assemblies()
        {
            List<Assembly> assemblies = new List<Assembly>();

            // Models assembly
            assemblies.Add(typeof(FormerDetailsView).Assembly);

            return assemblies;
        }
        #endregion
    }
}
