﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingIS.Entities;

namespace TrainingIS.WebApp.ViewModels
{
    // Generated by Manager v 0.2.0
    public class BaseAbsenceViewModel
    {
        public Absence entity { get; set; }

        public BaseAbsenceViewModel(Absence entity)
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
    public partial class AbsenceViewModel : BaseAbsenceViewModel {
        public AbsenceViewModel(Absence entity):base(entity) {}
    };
}