using System.Web;
using System.Web.Optimization;
using TrainingIS.WebApp.Services;

namespace TrainingIS.WebApp
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // 
            // All Pages  - Core 
            bundles.Add(new StyleBundle("~/Content/Core")
                .Include("~/Content/gentelella/vendors/bootstrap/dist/css/bootstrap.min.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/gentelella/vendors/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/gentelella/vendors/nprogress/nprogress.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/gentelella/vendors/google-code-prettify/dist/prettify.min.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/sweetAlert/sweetalert.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/gentelella/vendors/datatables.net-bs/css/dataTables.bootstrap.min.css", new CssRewriteUrlTransformWrapper())

                .Include("~/Content/gentelella/vendors/bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/vendors/select2/css/select2.min.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/vendors/select2-bootstrap/select2-bootstrap.min.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/gentelella/css/custom.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/GApp.WebApp/css/WebApp.Core.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/GApp.WebApp/css/WebApp.Components.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/site.css", new CssRewriteUrlTransformWrapper()));

            bundles.Add(new ScriptBundle("~/bundles/Core").Include(
                "~/Content/gentelella/vendors/jquery/dist/jquery.min.js",
                "~/Content/gentelella/vendors/bootstrap/js/tooltip.js",
                "~/Content/gentelella/vendors/bootstrap/dist/js/bootstrap.min.js",
                "~/Content/gentelella/vendors/fastclick/lib/fastclick.js",
                "~/Content/gentelella/vendors/nprogress/nprogress.js",
                "~/Content/gentelella/vendors/bootstrap-wysiwyg/js/bootstrap-wysiwyg.min.js",
                "~/Content/gentelella/vendors/jquery.hotkeys/jquery.hotkeys.js",
                "~/Content/gentelella/vendors/google-code-prettify/dist/prettify.min.js",
                "~/Content/sweetAlert/sweetalert.min.js",
                "~/Content/gentelella/vendors/datatables.net/js/jquery.dataTables.min.js",
                "~/Content/gentelella/vendors/datatables.net-bs/js/dataTables.bootstrap.js",
                "~/Content/gentelella/vendors/datatables.net-responsive/js/dataTables.responsive.js",
                "~/Content/gentelella/vendors/moment/min/moment.min.js",
                "~/Content/gentelella/vendors/bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js",
                "~/Content/vendors/select2/js/select2.min.js",
                "~/Content/gentelella/js/gentelella_trainingis.js",
                "~/Scripts/libs/js.cookie.js",
                 "~/Content/GApp.WebApp/js/WebApp.Core.js",
                 "~/Content/GApp.WebApp/js/WebApp.Components.js"
                 ));

            // Page - Index
            //
            bundles.Add(new StyleBundle("~/Content/Manager/Index")
                 .Include("~/Content/GApp.WebApp/css/WebApp.Index.css", new CssRewriteUrlTransformWrapper())
                );
            bundles.Add(new ScriptBundle("~/bundles/Manager/Index").Include(
                 "~/Content/GApp.WebApp/js/WebApp.Index.js"
            ));

            // 
            // Page - Form ( Create, Edit)
            bundles.Add(new StyleBundle("~/Content/Form")
              .Include("~/Content/GApp.WebApp/css/WebApp.Form.css", new CssRewriteUrlTransformWrapper())
                );
            bundles.Add(new ScriptBundle("~/bundles/Form").Include(
                "~/Content/GApp.WebApp/js/WebApp.Form.js"
                 ));

            // Page - Details
            //
            bundles.Add(new StyleBundle("~/Content/Manager/Details")
                .Include("~/Content/GApp.WebApp/css/WebApp.Details.css", new CssRewriteUrlTransformWrapper())
                );


            //
            // Statistic
            //
            bundles.Add(new ScriptBundle("~/bundles/Statistic").Include(
               "~/Content/gentelella/vendors/Chart.js/dist/Chart.min.js", new CssRewriteUrlTransformWrapper()
           ));


            // Absences
            //
            bundles.Add(new ScriptBundle("~/bundles/Absences/Create_Absences").Include(
                "~/Content/views/absences/js/Create_Absences.js", new CssRewriteUrlTransformWrapper()
            ));
            bundles.Add(new StyleBundle("~/Content/Absences/Create_Absences")
               .Include("~/Content/views/absences/css/Create_Absences.css", new CssRewriteUrlTransformWrapper())
               );

            // Admin Panel : gentelella
            //
            bundles.Add(new StyleBundle("~/Content/gentelella").Include(
                "~/Content/gentelella/vendors/bootstrap/dist/css/bootstrap.min.css",
                "~/Content/gentelella/vendors/font-awesome/css/font-awesome.min.css",
                "~/Content/gentelella/vendors/nprogress/nprogress.css",
                "~/Content/gentelella/vendors/iCheck/skins/flat/green.css",
                "~/Content/gentelella/vendors/google-code-prettify/dist/prettify.min.css",
                "~/Content/gentelella/vendors/select2/dist/css/select2.min.css",
                "~/Content/gentelella/vendors/switchery/dist/switchery.min.css",
                "~/Content/gentelella/vendors/starrr/dist/starrr.css",
                "~/Content/gentelella/vendors/bootstrap-daterangepicker/daterangepicker.css",
                "~/Content/gentelella/css/custom.css",
                 "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/gentelella").Include(
                "~/Content/gentelella/vendors/jquery/dist/jquery.min.js",
                "~/Content/gentelella/vendors/bootstrap/dist/js/bootstrap.min.js",
                "~/Content/gentelella/vendors/fastclick/lib/fastclick.js",
                "~/Content/gentelella/vendors/nprogress/nprogress.js",
                "~/Content/gentelella/vendors/bootstrap-progressbar/bootstrap-progressbar.min.js",
                "~/Content/gentelella/vendors/iCheck/icheck.min.js",
                "~/Content/gentelella/vendors/moment/min/moment.min.js",
                "~/Content/gentelella/vendors/bootstrap-daterangepicker/daterangepicker.js",
                "~/Content/gentelella/vendors/bootstrap-wysiwyg/js/bootstrap-wysiwyg.min.js",
                "~/Content/gentelella/vendors/jquery.hotkeys/jquery.hotkeys.js",
                "~/Content/gentelella/vendors/google-code-prettify/src/prettify.js",
                "~/Content/gentelella/vendors/jquery.tagsinput/src/jquery.tagsinput.js",
                "~/Content/gentelella/vendors/switchery/dist/switchery.min.js",
                "~/Content/gentelella/vendors/select2/dist/js/select2.full.min.js",
                "~/Content/gentelella/vendors/parsleyjs/dist/parsley.min.js",
                "~/Content/gentelella/vendors/autosize/dist/autosize.min.js",
                "~/Content/gentelella/vendors/devbridge-autocomplete/dist/jquery.autocomplete.min.js",
                "~/Content/gentelella/vendors/starrr/dist/starrr.js",
                "~/Content/gentelella/js/custom.js"
                 ));


            // Page - Login : Gentella
            bundles.Add(new StyleBundle("~/Content/Login")
                .Include("~/Content/gentelella/vendors/bootstrap/dist/css/bootstrap.min.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/gentelella/vendors/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransformWrapper())
               .Include("~/Content/gentelella/vendors/nprogress/nprogress.css", new CssRewriteUrlTransformWrapper())
               .Include("~/Content/gentelella/vendors/animate.css/animate.min.css", new CssRewriteUrlTransformWrapper())
               .Include("~/Content/gentelella/css/custom.min.css", new CssRewriteUrlTransformWrapper())
               .Include("~/Content/sweetAlert/sweetalert.css", new CssRewriteUrlTransformWrapper())
               .Include("~/Content/site.css", new CssRewriteUrlTransformWrapper())
                );
            bundles.Add(new ScriptBundle("~/bundles/Login").Include(
                "~/Content/gentelella/vendors/jquery/dist/jquery.min.js",
                 "~/Content/sweetAlert/sweetalert.min.js"
                 ));


          




            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération à l'adresse https://modernizr.com pour sélectionner uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/gentelella/vendors/bootstrap/dist/css/bootstrap.min.css",
                      "~/Content/site.css"));

        }
    }
}
