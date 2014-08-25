Imports System.Web.Optimization

Public Class BundleConfig
    ' For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
    Public Shared Sub RegisterBundles(ByVal bundles As BundleCollection)
        bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
                   "~/Scripts/jquery-{version}.js"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.unobtrusive*",
                    "~/Scripts/jquery.validate*"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryPanzoom").Include(
                    "~/Scripts/jquery.panzoom.js",
                    "~/Scripts/jquery.mousewheel.js"))

        ' Use the development version of Modernizr to develop with and learn from. Then, when you're
        ' ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"))

        bundles.Add(New ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap.js"))

        bundles.Add(New ScriptBundle("~/bundles/VisualSwitchInterface").Include(
                    "~/Scripts/VisualSwitchInterface/global.js"))

        bundles.Add(New StyleBundle("~/Content/css").Include("~/Content/site.css"))

        bundles.Add(New StyleBundle("~/Content/bootstrap").Include(
                    "~/Content/bootstrap.css",
                    "~/Content/bootstrap-theme.css"))

    End Sub
End Class