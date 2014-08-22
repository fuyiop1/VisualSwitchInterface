Imports VisualSwitchInterface.Models

Namespace Controllers
    Public Class AccountController
        Inherits Controller
        Const SessionLoginFailCountKey = "SessionLoginFailCountKey"

        Function Login(returnUrl As String) As ActionResult
            Dim model = New AccountLoginModel
            Return View(model)
        End Function

        <HttpPost()>
        Function Login(model As AccountLoginModel, returnUrl As String) As ActionResult
            If ValidateLogin(model) Then
                Dim ticket = New FormsAuthenticationTicket(1, model.Username, DateTime.Now, DateTime.Now.AddMinutes(20), False, "Admin")
                Dim encTicket = FormsAuthentication.Encrypt(ticket)
                Response.Cookies.Add(New HttpCookie(FormsAuthentication.FormsCookieName, encTicket))

                Session.Remove(SessionLoginFailCountKey)

                If String.IsNullOrEmpty(returnUrl) Then
                    Return RedirectToAction("Index", "Home")
                Else
                    Return Redirect(returnUrl)
                End If
            End If
            Return View(model)
        End Function

        Private Function ValidateLogin(ByVal model As AccountLoginModel) As Boolean
            Dim result = ModelState.IsValid

            If result Then
                If "admin" <> model.Username Or "admin" <> model.Password Then
                    ModelState.AddModelError("", "Invalid username or password")
                    result = False

                    Dim sessionLoginFailCount = Convert.ToInt32(Session(SessionLoginFailCountKey))
                    sessionLoginFailCount += 1
                    Session(SessionLoginFailCountKey) = sessionLoginFailCount

                    If sessionLoginFailCount >= 3 Then
                        model.IsLocked = True
                    End If
                End If
            End If

            Return result
        End Function
    End Class
End Namespace