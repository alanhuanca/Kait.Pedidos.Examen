using System.Web;
using System.Web.Optimization;

namespace Kait.Pedidos
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios.  De esta manera estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/plugins/dataPickerStyles").Include(
                      "~/Content/plugins/datapicker/datepicker3.css"));

            bundles.Add(new ScriptBundle("~/plugins/dataPicker").Include(
                      "~/Scripts/plugins/datapicker/bootstrap-datepicker.js"));
             
            bundles.Add(new StyleBundle("~/plugins/sweetAlertStyles").Include(
                      "~/Content/plugins/sweetalert/sweetalert.css"));
             
            bundles.Add(new ScriptBundle("~/plugins/sweetAlert").Include(
                      "~/Scripts/plugins/sweetalert/sweetalert.min.js"));
             
            bundles.Add(new StyleBundle("~/Content/plugins/dataTables/dataTablesStyles").Include(
                      "~/Content/plugins/dataTables/datatables.min.css"));
             
            bundles.Add(new ScriptBundle("~/plugins/dataTables").Include(
                      "~/Scripts/plugins/dataTables/datatables.min.js",
                      "~/Scripts/plugins/dataTables/dataTables.bootstrap4.min.js"));
        }
    }
}
