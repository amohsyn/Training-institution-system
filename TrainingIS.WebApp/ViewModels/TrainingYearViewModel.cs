﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingIS.Entities;

namespace TrainingIS.WebApp.ViewModels
{
    // Generated by Manager v 0.2.0
    public class BaseTrainingYearViewModel
    {
        public TrainingYear entity { get; set; }

        public BaseTrainingYearViewModel(TrainingYear entity)
        {
            this.entity = entity;
        }

        //public int Age
        //{
        //    get
        //    {
        //        int age = DateTime.Now.Year - this.trainee.Birthdate.Year;
        //        if (this.trainee.Birthdate > DateTime.Now.AddYears(-age))
        //        {
        //            age--;
        //        }
        //        return age;
        //    }
        //}
    }
    public partial class TrainingYearViewModel : BaseTrainingYearViewModel {
        public TrainingYearViewModel(TrainingYear entity):base(entity) {}
    };
}
