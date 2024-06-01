using System.Web;
using System.Web.Optimization;

namespace CapaPresentacionPancho
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new Bundle("~/bundles/complementos").Include(
                              "~/Scripts/fontawesome/all.min.js",
                              "~/Scripts/DataTables/jquery.dataTables.js",
                              "~/Scripts/DataTables/dataTables.responsive.js",
                              "~/Scripts/loadingoverlay/loadingoverlay.min.js",
                              "~/Scripts/jquery.validate.js",
                              "~/Scripts/sweetalert.min.js",
                              "~/Scripts/jquery-ui.js",
                              "~/Scripts/scripts.js"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.bundle.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/site.css",
                        "~/Content/DataTables/css/jquery.dataTables.css",
                        "~/Content/sweetalert.css",
                        "~/Content/jquery-ui.css",
                        "~/Content/DataTables/css/responsive.dataTables.css"));
        }
    }
}
