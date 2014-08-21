Imports VisualSwitchInterface.Models

Namespace Controllers
    Public Class AccountController
        Inherits Controller

        Function Login() As ActionResult
            Return View()
        End Function

        <HttpPost()>
        Function Login(model As AccountLoginModel) As ActionResult
            If ValidateLogin(model) Then
                Dim ticket = New FormsAuthenticationTicket(1, model.Username, DateTime.Now, DateTime.Now.AddMinutes(20), False, "Admin")

                Dim encTicket = FormsAuthentication.Encrypt(ticket)

                Response.Cookies.Add(New HttpCookie(FormsAuthentication.FormsCookieName, encTicket))

                Return RedirectToAction("Index", "Home")
            End If
            Return View(model)
        End Function

        Private Function ValidateLogin(ByVal model As AccountLoginModel) As Boolean
            Dim result = ModelState.IsValid

            If result Then
                If "admin" <> model.Username Or "admin" <> model.Password Then
                    ModelState.AddModelError("", "Invalid username or password")
                    result = False
                End If
            End If

            Return result
        End Function
    End Class
End Namespace