using System.Web;
using System.Web.Optimization;

namespace Aloe
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                         , "~/Scripts/jquery.js"
                        , "~/Scripts/Scripts.js"
                         , "~/Scripts/jquery.smartmenus.js"
                          , "~/Scripts/jquery.smartmenus.bootstrap.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/InputMask").Include(
                      "~/Scripts/inputmask.js*"
                      ));

            


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                       "~/Scripts/bootstrap-datepicker.js",
                        //"~/Scripts/bootstrap-modal-popover.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/bootstrap.css" 
                      ,"~/Content/site.css"
                         ,"~/Content/datepicker.css"
                             , "~/Content/deal.css"
                                    , "~/Content/jquery.smartmenus.bootstrap.css"
                                         , "~/Content/sb-admin.css"
                                                , "~/Content/style.css"
                                                

                                                 
                      ));

            bundles.Add(new StyleBundle("~/Content/MainDesign").Include(
                               "~/Content/bootstrap.css"
                                 , "~/Content/site.css"
                                    , "~/Content/datepicker.css"
                                        , "~/Content/deal.css"
                                               , "~/Content/jquery.smartmenus.bootstrap.css"
                                                    , "~/Content/sb-admin.css"
                                                           , "~/Content/style.css"
                                                           ));



            //** Graph Charts
            bundles.Add(new StyleBundle("~/bundles/Graph").Include(
                "~/Scripts/Graph/jquery-ui.min.js"
                , "~/Scripts/Graph/polygonalGraphWidget.js",
                "~/Scripts/Graph/easypiechart.js"
                 , "~/Scripts/Comparison_Graph.js"
                , "~/Scripts/Graph/CircleReadings.js"
                 , "~/Scripts/Graph/Productivity.js" 
               
               ));



        }
    }
}
