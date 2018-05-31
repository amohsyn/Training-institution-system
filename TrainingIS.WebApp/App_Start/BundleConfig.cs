using System.Web;
using System.Web.Optimization;

namespace TrainingIS.WebApp
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
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
                      "~/Content/gentelella_vendors/bootstrap/dist/css/bootstrap.min.css",
                      "~/Content/site.css"));

            // Admin Panel : Gentella
            bundles.Add(new StyleBundle("~/Content/gentelella").Include(
                "~/Content/gentelella_vendors/bootstrap/dist/css/bootstrap.min.css",
                "~/Content/gentelella_vendors/font-awesome/css/font-awesome.min.css",
                "~/Content/gentelella_vendors/nprogress/nprogress.css",
                "~/Content/gentelella_vendors/iCheck/skins/flat/green.css",
                "~/Content/gentelella_vendors/google-code-prettify/dist/prettify.min.css",
                "~/Content/gentelella_vendors/select2/dist/css/select2.min.css",
                "~/Content/gentelella_vendors/switchery/dist/switchery.min.css",
                "~/Content/gentelella_vendors/starrr/dist/starrr.css",
                "~/Content/gentelella_vendors/bootstrap-daterangepicker/daterangepicker.css",
                "~/Content/gentelella/css/custom.css",
                 "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/gentelella").Include(
                "~/Content/gentelella_vendors/jquery/dist/jquery.min.js",
                "~/Content/gentelella_vendors/bootstrap/dist/js/bootstrap.min.js",
                "~/Content/gentelella_vendors/fastclick/lib/fastclick.js",
                "~/Content/gentelella_vendors/nprogress/nprogress.js",
                "~/Content/gentelella_vendors/bootstrap-progressbar/bootstrap-progressbar.min.js",
                "~/Content/gentelella_vendors/iCheck/icheck.min.js",
                "~/Content/gentelella_vendors/moment/min/moment.min.js",
                "~/Content/gentelella_vendors/bootstrap-daterangepicker/daterangepicker.js",
                "~/Content/gentelella_vendors/bootstrap-wysiwyg/js/bootstrap-wysiwyg.min.js",
                "~/Content/gentelella_vendors/jquery.hotkeys/jquery.hotkeys.js",
                "~/Content/gentelella_vendors/google-code-prettify/src/prettify.js",
                "~/Content/gentelella_vendors/jquery.tagsinput/src/jquery.tagsinput.js",
                "~/Content/gentelella_vendors/switchery/dist/switchery.min.js",
                "~/Content/gentelella_vendors/select2/dist/js/select2.full.min.js",
                "~/Content/gentelella_vendors/parsleyjs/dist/parsley.min.js",
                "~/Content/gentelella_vendors/autosize/dist/autosize.min.js",
                "~/Content/gentelella_vendors/devbridge-autocomplete/dist/jquery.autocomplete.min.js",
                "~/Content/gentelella_vendors/starrr/dist/starrr.js",
                "~/Content/gentelella/js/custom.min.js"
                 ));


            // Login : Gentella
            bundles.Add(new StyleBundle("~/Content/Login").Include(
                "~/Content/gentelella_vendors/bootstrap/dist/css/bootstrap.min.css",
                "~/Content/gentelella_vendors/font-awesome/css/font-awesome.min.css",
                "~/Content/gentelella_vendors/nprogress/nprogress.css",
                "~/Content/gentelella_vendors/animate.css/animate.min.css",
                "~/Content/gentelella/css/custom.min.css",
                "~/Content/site.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Login").Include(
                "~/Content/gentelella_vendors/jquery/dist/jquery.min.js"
                 ));



        }
    }
}
