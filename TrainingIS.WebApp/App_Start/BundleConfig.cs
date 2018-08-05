using System.Web;
using System.Web.Optimization;

namespace TrainingIS.WebApp
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            // 
            // Page - Core
            //
            // CSS
            bundles.Add(new StyleBundle("~/Content/Core").Include(
                "~/Content/gentelella/vendors/bootstrap/dist/css/bootstrap.min.css",
                "~/Content/gentelella/vendors/font-awesome/css/font-awesome.min.css",
                "~/Content/gentelella/vendors/nprogress/nprogress.css",
                 "~/Content/sweetAlert/sweetalert.css",
                "~/Content/gentelella/css/custom.css",
                 "~/Content/site.css"));
            // Javascript
            bundles.Add(new ScriptBundle("~/bundles/Core").Include(
                "~/Content/gentelella/vendors/jquery/dist/jquery.min.js",
                "~/Content/gentelella/vendors/bootstrap/dist/js/bootstrap.min.js",
                "~/Content/gentelella/vendors/fastclick/lib/fastclick.js",
                "~/Content/gentelella/vendors/nprogress/nprogress.js",
                "~/Content/sweetAlert/sweetalert.min.js",
                "~/Content/gentelella/js/gentelella_trainingis.js",
                "~/Scripts/libs/js.cookie.js"
                 ));


            // 
            // Page - Form 
            //
            bundles.Add(new StyleBundle("~/Content/Form").Include(
                "~/Content/gentelella/vendors/bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.css",
                "~/Content/vendors/select2/css/select2.min.css",
                 "~/Content/vendors/select2-bootstrap/select2-bootstrap.min.css"
                ));

            //  
            //"~/Content/gentelella/vendors/bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js",
            bundles.Add(new ScriptBundle("~/bundles/Form").Include(
               "~/Content/gentelella/vendors/moment/min/moment.min.js",
               "~/Content/gentelella/vendors/bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js",
               "~/Content/vendors/select2/js/select2.min.js",
               "~/Content/shared/manager/form.js"
                 ));

            //
            // Page - Manager.Index
            //
            bundles.Add(new StyleBundle("~/Content/Manager/Index").Include(
                "~/Content/gentelella/vendors/datatables.net-bs/css/dataTables.bootstrap.min.css",
                "~/Content/shared/manager/index.css"

                )
                );
            bundles.Add(new ScriptBundle("~/bundles/Manager/Index").Include(
                "~/Content/gentelella/vendors/datatables.net/js/jquery.dataTables.min.js",
                 "~/Content/shared/manager/index.js"
                 ));

            //
            // Page - Manager.Details
            //
            bundles.Add(new StyleBundle("~/Content/Manager/Details").Include(
                "~/Content/shared/manager/details.css")
                );
             

            //,
            //    "~/Content/gentelella/vendors/datatables.net-buttons-bs/css/buttons.bootstrap.min.css",
            //    "~/Content/gentelella/vendors/datatables.net-fixedheader-bs/css/fixedHeader.bootstrap.min.css",
            //    "~/Content/gentelella/vendors/datatables.net-responsive-bs/css/responsive.bootstrap.min.css",
            //    "~/Content/gentelella/vendors/datatables.net-scroller-bs/css/scroller.bootstrap.min.css"



            //"~/Content/gentelella/vendors/datatables.net-bs/js/dataTables.bootstrap.min.js",
            //      "~/Content/gentelella/vendors/datatables.net-buttons/js/dataTables.buttons.min.js",
            //       "~/Content/gentelella/vendors/datatables.net-buttons-bs/js/buttons.bootstrap.min.js",
            //        "~/Content/gentelella/vendors/datatables.net-buttons/js/buttons.flash.min.js",
            //         "~/Content/gentelella/vendors/datatables.net-buttons/js/buttons.html5.min.js",
            //          "~/Content/gentelella/vendors/datatables.net-buttons/js/buttons.print.min.js",
            //           "~/Content/gentelella/vendors/datatables.net-fixedheader/js/dataTables.fixedHeader.min.js",
            //            "~/Content/gentelella/vendors/datatables.net-keytable/js/dataTables.keyTable.min.js",
            //             "~/Content/gentelella/vendors/datatables.net-responsive/js/dataTables.responsive.min.js",
            //              "~/Content/gentelella/vendors/datatables.net-responsive-bs/js/responsive.bootstrap.js",
            //               "~/Content/gentelella/vendors/datatables.net-scroller/js/dataTables.scroller.min.js",
            //                "~/Content/gentelella/vendors/jszip/dist/jszip.min.js",
            //                 "~/Content/gentelella/vendors/pdfmake/build/pdfmake.min.js",
            //                  "~/Content/gentelella/vendors/pdfmake/build/vfs_fonts.js"






            // 
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


            // Login : Gentella
            bundles.Add(new StyleBundle("~/Content/Login").Include(
                "~/Content/gentelella/vendors/bootstrap/dist/css/bootstrap.min.css",
                "~/Content/gentelella/vendors/font-awesome/css/font-awesome.min.css",
                "~/Content/gentelella/vendors/nprogress/nprogress.css",
                "~/Content/gentelella/vendors/animate.css/animate.min.css",
                "~/Content/gentelella/css/custom.min.css",
                "~/Content/sweetAlert/sweetalert.css",
                "~/Content/site.css"
                ));

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
