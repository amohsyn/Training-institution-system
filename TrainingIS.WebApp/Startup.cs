using System;
using GApp.Core.Context;
using GApp.DAL;
using GApp.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.Entitie_excludes;
using TrainingIS.Entities;
using TrainingIS.WebApp.Models;

[assembly: OwinStartupAttribute(typeof(TrainingIS.WebApp.Startup))]
namespace TrainingIS.WebApp
{
    public partial class Startup
    {
        
        public void Configuration(IAppBuilder app)
        {
        
            ConfigureAuth(app);
  

        }

      


 
    }
}
