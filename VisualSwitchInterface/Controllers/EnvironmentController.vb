﻿Namespace Controllers

    <Authorize()>
    Public Class EnvironmentController
        Inherits Controller

        Function Index() As ActionResult
            Return View()
        End Function

    End Class
End Namespace