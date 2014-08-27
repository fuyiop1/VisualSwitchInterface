Namespace Controllers

    <Authorize()>
    Public Class LiveEventController
        Inherits Controller

        Function Index() As ActionResult
            Return View()
        End Function

    End Class
End Namespace