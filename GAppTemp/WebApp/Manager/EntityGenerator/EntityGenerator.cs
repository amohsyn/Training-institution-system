﻿using GApp.Core.Entities.ModelsViews;
using GApp.Core.MetaDatas.Attributes;
using GApp.Entities;
using GApp.Exceptions;
using GApp.WebApp.Manager.Views;
using GApp.WebApp.Manager.Views.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;

namespace GApp.WebApp.Manager.Generator
{
    [Obsolete("Use ModelView_CodeGenerator")]
    public partial class EntityGeneratorWork<T> where T : DbContext, new()
    {
        public Type EntityType { set; get; }

        public EntityGeneratorWork(Type EntityType)
        {
            this.EntityType = EntityType;
            this.InitRelationShip();

            this.InitEntityMetaData();
            this.InitModelView();
        }

        public List<Type> getAllEntities()
        {
            T context = new T();
            return context.GetAllTypesInContextOrder();
        }

      
    }
}