using System.Web;
using System.Web.Optimization;

namespace ActorLibrary
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/soundmanager").Include(
                      "~/Scripts/soundmanager/script/soundmanager2.js",
                      "~/Scripts/360-player/script/360player.js",
                      "~/Scripts/360-player/script/berniecode-animator.js",
                      "~/Scripts/Soundmanager.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/mediaelementplayer").Include(
                      "~/Scripts/mediaelement/mediaelement-and-player.js"

                      ));

            bundles.Add(new ScriptBundle("~/bundles/fileupload").Include(
                      "~/Scripts/jQuery-file-upload/js/jquery.fileupload.js",
                      "~/Scripts/jQuery-file-upload/js/jquery.fileupload-audio.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                      "~/Scripts/Scripts/knockout.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Scripts/mediaelement/mediaelementplayer.css",
                      "~/Scripts/jQuery-file-upload/css/jquery.fileupload.css",
                      "~/Content/site.css"
                      ));
        }
    }
}
