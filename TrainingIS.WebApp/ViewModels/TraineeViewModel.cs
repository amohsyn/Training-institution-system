using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingIS.Entities;

namespace TrainingIS.WebApp.ViewModels
{
    public class TraineeViewModel
    {
        public Trainee entity { get; set; }

        public TraineeViewModel(Trainee entity)
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
}