using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Default_Form_Trainee_ModelBLM
    {

        //public override Default_Form_Trainee_Model ConverTo_Default_Form_Trainee_Model(Trainee Trainee)
        //{
        //    Default_Form_Trainee_Model default_Form_Trainee_Model =  base.ConverTo_Default_Form_Trainee_Model(Trainee);
        //    default_Form_Trainee_Model.Photo = Trainee.Photo;
        //    return default_Form_Trainee_Model;
        //}

        //public override Trainee ConverTo_Trainee(Default_Form_Trainee_Model Default_Form_Trainee_Model)
        //{
        //    var trainee = base.ConverTo_Trainee(Default_Form_Trainee_Model);


        //    if (!string.IsNullOrEmpty(Default_Form_Trainee_Model.Photo_Reference))
        //    {
        //        if (trainee.Photo == null) trainee.Photo = new GPicture();
        //        if (trainee.Photo.Reference != Default_Form_Trainee_Model.Photo_Reference)
        //        {
        //            // Save the old reference to be deleted by the save methode 
        //            trainee.Photo.Old_Reference = trainee.Photo.Reference;

        //            GPictureBLO gPictureBLO = new GPictureBLO(this.GAppContext);
        //            trainee.Photo.Reference = Default_Form_Trainee_Model.Photo_Reference;

        //            trainee.Photo.Original_Thumbnail = gPictureBLO.Get_URL_Original_Picture_Path(Default_Form_Trainee_Model.Photo_Reference);
        //            trainee.Photo.Small_Thumbnail = gPictureBLO.Get_URL_Small_Picture_Path(Default_Form_Trainee_Model.Photo_Reference);
        //            trainee.Photo.Medium_Thumbnail = gPictureBLO.Get_URL_Medium_Picture_Path(Default_Form_Trainee_Model.Photo_Reference);
        //            trainee.Photo.Large_Thumbnail = gPictureBLO.Get_URL_Large_Picture_Path(Default_Form_Trainee_Model.Photo_Reference);
        //        }
        //    }



        //    return trainee;
        //}
    }
}
