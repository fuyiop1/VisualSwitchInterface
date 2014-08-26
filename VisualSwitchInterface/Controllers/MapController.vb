Imports VisualSwitchInterface.Models

Namespace Controllers

    <Authorize()>
    Public Class MapController
        Inherits Controller

        Private Shared ReadOnly TempSwitchModels As List(Of SwitchModel) = New List(Of SwitchModel) From
            {
                New SwitchModel With {.Id = 1, .Name = "Switch1", .CoordX = 20, .CoordY = 20},
                New SwitchModel With {.Id = 2, .Name = "Switch2", .CoordX = 300, .CoordY = 200},
                New SwitchModel With {.Id = 3, .Name = "Switch3", .CoordX = 400, .CoordY = 150}
            }

        Function Index() As ActionResult
            Return View()
        End Function

        Function _FloorChanged() As ActionResult
            Dim model = New MapModel
            model.FilePath = "/Content/Images/VisualSwitchInterface/sample-floor.png"
            model.SwitchModels = TempSwitchModels
            model.ReadImage(Server.MapPath(model.FilePath))
            Return PartialView(model)
        End Function

        Function _SwitchClicked(id As Integer) As ActionResult
            Dim model = TempSwitchModels.FirstOrDefault(Function(x) x.Id = id)
            Return PartialView(model)
        End Function

        Function _AddSwitch(coordX As Integer, coordY As Integer) As ActionResult
            Dim model = New SwitchModel With {.CoordX = coordX, .CoordY = coordY}
            Return PartialView(model)
        End Function

        <HttpPost()>
        Function _AddSwitch(model As SwitchModel) As ActionResult
            If Validate(model) Then
                model.Id = TempSwitchModels.Max(Function(x) x.Id) + 1
                TempSwitchModels.Add(model)
                EncapulateSuccess()
            End If
            Return PartialView(model)
        End Function

        Private Sub EncapulateSuccess()
            ViewBag.IsSuccess = True
        End Sub

        Private Function Validate(ByVal model As SwitchModel) As Boolean
            Dim isValid = ModelState.IsValid

            If isValid Then
                Dim modelQueryByName = TempSwitchModels.FirstOrDefault(Function(x) x.Name.Equals(model.Name, StringComparison.OrdinalIgnoreCase))
                If modelQueryByName IsNot Nothing Then
                    If model.Id = 0 Or modelQueryByName.Id <> model.Id Then
                        ModelState.AddModelError("Name", "Switch name already exists.")
                        isValid = False
                    End If
                End If

            End If

            Return isValid
        End Function

    End Class
End Namespace