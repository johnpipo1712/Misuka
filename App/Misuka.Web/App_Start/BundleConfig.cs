using System.Web;
using System.Web.Optimization;

namespace Misuka.Web
{
  public class BundleConfig
  {
    // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
    public static void RegisterBundles(BundleCollection bundles)
    {
      bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/Scripts/jquery-{version}.js"));

      bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                  "~/Scripts/jquery-ui-{version}.js"));

      bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                  "~/Scripts/jquery.unobtrusive*",
                  "~/Scripts/jquery.validate*"));

      // Use the development version of Modernizr to develop with and learn from. Then, when you're
      // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
      bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                  "~/Scripts/modernizr-*"));

      bundles.Add(new ScriptBundle("~/bundles/jquery-base").Include("~/Scripts/jquery-1.11.1.js"));

      bundles.Add(new ScriptBundle("~/bundles/jquery-common").Include(
        "~/Scripts/plugins/bootbox/bootbox.js"
        , "~/Scripts/jquery.nicescroll.min.js"
        , "~/Scripts/jquery.unobtrusive-ajax.min.js"
        , "~/Scripts/jquery.validate.js"
        , "~/Scripts/jquery.validate.unobtrusive.min.js"
        , "~/Scripts/jquery.mask.js"
        , "~/Scripts/jquery.tmpl.min.js"
        , "~/Scripts/jqGrid.common.js"
        , "~/Scripts/jqueryFixes.js"
        , "~/Scripts/jquery.jqGrid.src.js"
        , "~/Scripts/utils/common.js"
        , "~/Scripts/buzz/buzz.js"
        , "~/Scripts/buzz/buzz.min.js"
        , "~/Scripts/ckeditor/ckeditor.js"
        , "~/Scripts/magnific-popup/magnific-popup.js"
        , "~/Scripts/plugins/pnotify/pnotify.custom.js"
        , "~/Scripts/plugins/waitMe/waitMe.js"
        , "~/Scripts/bootstrap-datepicker/js/bootstrap-datepicker.js"
         ,"~/Scripts/jquery-ui.js"
        , "~/Scripts/utils/dialog.js"));

      //Styles
      bundles.Add(new StyleBundle("~/Content/css").Include(
        "~/Content/css/bootbox/bootbox.css",
        "~/Content/css/common.css",
        "~/Scripts/plugins/pnotify/pnotify.custom.css",
        "~/Scripts/plugins/waitMe/waitMe.css",
        "~/Content/css/jquery-ui/jquery-ui.css",
        "~/Scripts/plugins/magnific-popup/magnific-popup.css",
        "~/Content/css/jqgrid/jquery-ui-jqgrid.css",
        "~/Content/css/grid-custom.css"));

      bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                  "~/Content/themes/base/jquery.ui.core.css",
                  "~/Content/themes/base/jquery.ui.resizable.css",
                  "~/Content/themes/base/jquery.ui.selectable.css",
                  "~/Content/themes/base/jquery.ui.accordion.css",
                  "~/Content/themes/base/jquery.ui.autocomplete.css",
                  "~/Content/themes/base/jquery.ui.button.css",
                  "~/Content/themes/base/jquery.ui.dialog.css",
                  "~/Content/themes/base/jquery.ui.slider.css",
                  "~/Content/themes/base/jquery.ui.tabs.css",
                  "~/Content/themes/base/jquery.ui.datepicker.css",
                  "~/Content/themes/base/jquery.ui.progressbar.css",
                  "~/Content/themes/base/jquery.ui.theme.css"));
    }
  }
}