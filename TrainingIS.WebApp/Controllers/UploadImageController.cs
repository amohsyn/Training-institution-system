using ImageResizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TrainingIS.BLL;
using TrainingIS.Entities;
using TrainingIS.WebApp.Helpers;

namespace TrainingIS.WebApp.Controllers
{
    public class UploadImageController : TrainingIS_BaseController
    {

        string nameFile = "GPicture";

        /// <summary>
        /// Upload Temp File
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadFile()
        {
            string _GPicture_Reference = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var _GPictureFile = System.Web.HttpContext.Current.Request.Files["GPictureFile"];
                if (_GPictureFile.ContentLength > 0)
                {
                    GPictureBLO gpictureBLO = new GPictureBLO(this.GAppContext);
                    _GPicture_Reference = gpictureBLO.Save_Tmp(_GPictureFile);
                }
            }
            return Json(Convert.ToString(_GPicture_Reference), JsonRequestBehavior.AllowGet);
        }
 
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SaveFile()
        {
            var _comPath = "";
            string _imgname = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var site = Server.MapPath(pic.FileName);
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);

                    _imgname = Guid.NewGuid().ToString(); //Nama akan ke random dengan fungsi guid

                    _comPath = Server.MapPath("/Upload/" + nameFile) + _imgname + _ext;
                    _imgname = nameFile + _imgname + _ext;

                    ViewBag.Msg = _comPath;
                    var path = _comPath;

                    // Saving Image in Original Mode
                    pic.SaveAs(path);

                    // resizing image
                    MemoryStream ms = new MemoryStream();
                    WebImage img = new WebImage(_comPath);


                    img.Save(_comPath);

                }
            }
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }
    }
}