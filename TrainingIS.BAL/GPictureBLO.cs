using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GApp.Core.Context;
using ImageResizer;
using ImageResizer.Plugins.Faces;

namespace TrainingIS.BLL
{
    public class GPictureBLO : Base_NotDb_BLO
    {
        public static string Upload_Dicrectory = "Upload";
        public static string Upload_Tmp_Dicrectory = "Upload_Tmp";

        public static int ORIGINAL_With = 1500;
        public static int LARGE_With = 1000;
        public static int MEDIUM_With = 350;
        public static int SMALL_With = 150;

        public static string ORIGINAL = "Original";
        public static string LARGE = "Large";
        public static string MEDIUM = "Medium";
        public static string SMALL = "Small";

        public GPictureBLO(GAppContext GAppContext) : base(GAppContext)
        {
        }

        /// <summary>
        /// Save Tmp GPiture
        /// </summary>
        /// <param name="photo"></param>
        public void Move_To_Uplpad_Directory(string Photo_Reference)
        {
            // Move tmp Picture
            string SourceDirectory = string.Format("{0}{1}/{2}",
                this.GAppContext.Server_Path,
                Upload_Tmp_Dicrectory,
                Photo_Reference
                );

            string DistinationDirectory = string.Format("{0}{1}/{2}",
               this.GAppContext.Server_Path,
               Upload_Dicrectory,
               Photo_Reference
               );
            Directory.Move(SourceDirectory, DistinationDirectory);
        }

        public void Delete(string Photo_Reference)
        {
            string Tmp_Picture_Directory = string.Format("{0}{1}/{2}", this.GAppContext.Server_Path, Upload_Dicrectory, Photo_Reference);
            Directory.Delete(Tmp_Picture_Directory, true);
        }

        public string Save_Tmp(HttpPostedFile GPictureFile)
        {
            // Create GPicture Reference
            string _GPicture_Reference  = Guid.NewGuid().ToString();

            this.Create_Temp_Upload_Directory_If_Not_Exist(_GPicture_Reference);

            // Save Orginal file 
            // GPictureFile.SaveAs(this.Get_Original_Picture_Path(_GPicture_Reference));

            // GPictureFile.InputStream


            //Bitmap bitmap = new Bitmap(GPictureFile.InputStream);
           
            //FacesPlugin facesPlugin = new FacesPlugin();
            //NameValueCollection nameValueCollection = new NameValueCollection();
            
            //var f = facesPlugin.GetFacesFromImage(bitmap, nameValueCollection);
            

           


            var setting_Original_file = new ResizeSettings { MaxWidth = ORIGINAL_With, Quality = 100, Format = "png" };
            ImageBuilder.Current.Build(
                GPictureFile,
                this.Get_Original_Picture_Path(_GPicture_Reference),
                setting_Original_file);

            // Save Larg file
            var setting_large_file = new ResizeSettings { MaxWidth = LARGE_With, Quality = 100, Format = "png"};
            ImageBuilder.Current.Build(
                this.Get_Original_Picture_Path(_GPicture_Reference),
                this.Get_Large_Picture_Path(_GPicture_Reference),
                setting_large_file);

            // Save Medium file
            var setting_Medium_file = new ResizeSettings { MaxWidth = MEDIUM_With, Quality = 100, Format = "png" };
            ImageBuilder.Current.Build(
                this.Get_Original_Picture_Path(_GPicture_Reference),
                this.Get_Medium_Picture_Path(_GPicture_Reference),
                setting_Medium_file);

            // Save Small file
            var setting_Small_file = new ResizeSettings { MaxWidth = SMALL_With, Quality = 100, Format = "png" };
            ImageBuilder.Current.Build(
                this.Get_Original_Picture_Path(_GPicture_Reference),
                this.Get_Small_Picture_Path(_GPicture_Reference),
                setting_Small_file);

            return _GPicture_Reference;
        }

        public void Create_Temp_Upload_Directory_If_Not_Exist(string Photo_Reference)
        {

            string Temp_Upload_path = string.Format("{0}{1}", this.GAppContext.Server_Path, Upload_Tmp_Dicrectory);
            if (!Directory.Exists(Temp_Upload_path))
                Directory.CreateDirectory(Temp_Upload_path);

            string Temp_File_path = Temp_Upload_path + "/" + Photo_Reference;
            if (!Directory.Exists(Temp_File_path))
                Directory.CreateDirectory(Temp_File_path);

        }

      

        public void Create_Upload_Directory_If_Not_Exist(string Photo_Reference)
        {
            string Upload_path = string.Format("{0}{1}", this.GAppContext.Server_Path, Upload_Dicrectory);
            if (!Directory.Exists(Upload_path))
                Directory.CreateDirectory(Upload_path);

            string File_path = Upload_path + "/" + Photo_Reference;
            if (!Directory.Exists(File_path))
                Directory.CreateDirectory(File_path);

        }

        public string Get_Original_Picture_Path(string Photo_Reference)
        {
            string Tmp_Picture_Directory = string.Format("{0}{1}", this.GAppContext.Server_Path, Upload_Tmp_Dicrectory);
            return string.Format("{0}/{1}/{2}.png", Tmp_Picture_Directory, Photo_Reference, ORIGINAL);
        }
        public string Get_Large_Picture_Path(string Photo_Reference)
        {
            string Tmp_Picture_Directory = string.Format("{0}{1}", this.GAppContext.Server_Path, Upload_Tmp_Dicrectory);
            return string.Format("{0}/{1}/{2}.png", Tmp_Picture_Directory, Photo_Reference, LARGE);
        }
        public string Get_Medium_Picture_Path(string Photo_Reference)
        {
            string Tmp_Picture_Directory = string.Format("{0}{1}", this.GAppContext.Server_Path, Upload_Tmp_Dicrectory);
            return string.Format("{0}/{1}/{2}.png", Tmp_Picture_Directory, Photo_Reference, MEDIUM);
        }
        public string Get_Small_Picture_Path(string Photo_Reference)
        {
            string Tmp_Picture_Directory = string.Format("{0}{1}", this.GAppContext.Server_Path, Upload_Tmp_Dicrectory);
            return string.Format("{0}/{1}/{2}.png", Tmp_Picture_Directory, Photo_Reference,SMALL);
        }


        public string Get_URL_Original_Picture_Path(string Photo_Reference)
        {
         
            return string.Format("{0}/{1}/{2}.png", Upload_Dicrectory, Photo_Reference, ORIGINAL);
        }
        public string Get_URL_Large_Picture_Path(string Photo_Reference)
        {
           
            return string.Format("{0}/{1}/{2}.png", Upload_Dicrectory, Photo_Reference, LARGE);
        }
        public string Get_URL_Medium_Picture_Path(string Photo_Reference)
        {
            
            return string.Format("{0}/{1}/{2}.png", Upload_Dicrectory, Photo_Reference, MEDIUM);
        }
        public string Get_URL_Small_Picture_Path(string Photo_Reference)
        {
           
            return string.Format("{0}/{1}/{2}.png", Upload_Dicrectory, Photo_Reference, SMALL);
        }



    }
}
