' Note: For instructions on enabling IIS6 or IIS7 classic mode, 
' visit http://go.microsoft.com/?LinkId=9394802
Imports System.Web.Http
Imports System.Web.Optimization

Public Class MvcApplication
    Inherits HttpApplication

    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()

        WebApiConfig.Register(GlobalConfiguration.Configuration)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub

    Sub Application_AcquireRequestState()
        ProcessCultureInfo()
    End Sub

    Sub ProcessCultureInfo()
        Dim culture As Globalization.CultureInfo = Nothing
        If Request.UserLanguages IsNot Nothing Then
            Try
                Dim cultureString = Request.UserLanguages.First()
                culture = Globalization.CultureInfo.CreateSpecificCulture(cultureString)
            Catch ex As Exception
            End Try
        End If

        If culture IsNot Nothing Then
            Threading.Thread.CurrentThread.CurrentUICulture = culture
            Threading.Thread.CurrentThread.CurrentCulture = culture
        End If

    End Sub
End Class
