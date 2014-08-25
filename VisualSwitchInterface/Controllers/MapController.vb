Imports VisualSwitchInterface.Models

Namespace Controllers

    <Authorize()>
    Public Class MapController
        Inherits Controller

        Private Shared ReadOnly TempSwitchModels As List(Of SwitchModel) = New List(Of SwitchModel) From
            {
                New SwitchModel With {.Id = 1, .Name = "Switch1", .CoordX = 0.2, .CoordY = 0.2},
                New SwitchModel With {.Id = 2, .Name = "Switch2", .CoordX = 0.3, .CoordY = 0.6},
                New SwitchModel With {.Id = 3, .Name = "Switch3", .CoordX = 0.4, .CoordY = 0.4}
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

    End Class
End Namespace