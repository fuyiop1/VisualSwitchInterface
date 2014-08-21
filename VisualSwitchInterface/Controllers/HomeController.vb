Namespace Controllers

    <Authorize()>
    Public Class HomeController
        Inherits Controller

        Function Index() As ActionResult
            Return View()
        End Function

        Function _MapClicked() As ActionResult
            Return PartialView()
        End Function

    End Class
End Namespace