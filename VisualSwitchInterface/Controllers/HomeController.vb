Namespace Controllers
    Public Class HomeController
        Inherits Controller

        Function Index() As ActionResult
            Return RedirectToAction("Index", "Map")
        End Function

    End Class
End Namespace