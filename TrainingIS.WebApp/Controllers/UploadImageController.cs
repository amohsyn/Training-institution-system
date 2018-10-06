using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TrainingIS.WebApp.Helpers;

namespace TrainingIS.WebApp.Controllers
{
    public class UploadImageController : TrainingIS_BaseController
    {
        //
        // GET: /UploadImage/
        #region Initialize
        FilesHelper filesHelper;
        string nameFile = "fix_judul_resep_";
        #endregion
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadFile()
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

                    _imgname = Guid.NewGuid().ToString();
                    this.Create_Upload_Directory_If_Not_Exist();
                    _comPath = Server.MapPath("/Upload/Temp/DELETE_") + _imgname + _ext;
                    System.IO.File.Delete(_comPath);
                    _imgname = "DELETE_" + _imgname + _ext;

                    ViewBag.Msg = _comPath;
                    var path = _comPath;

                    // Saving Image in Original Mode
                    pic.SaveAs(path);

                    // resizing image
                    MemoryStream ms = new MemoryStream();
                    WebImage img = new WebImage(_comPath);
                    img.Crop(10, 10, 10, 10);

                    img.Save(_comPath);

                }
            }

            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }

        private void Create_Upload_Directory_If_Not_Exist()
        {
            string Upload_path = Server.MapPath("/Upload");
            if (!Directory.Exists(Upload_path))
                Directory.CreateDirectory(Upload_path);
            string Temp_Upload_path = Server.MapPath("/Upload/Temp");
            if (!Directory.Exists(Temp_Upload_path))
                Directory.CreateDirectory(Temp_Upload_path);

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