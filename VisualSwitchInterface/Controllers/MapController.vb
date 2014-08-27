Imports System.IO
Imports Newtonsoft.Json
Imports VisualSwitchInterface.Models

Namespace Controllers

    <Authorize()>
    Public Class MapController
        Inherits Controller

        Function Index() As ActionResult
            Return View(GetAllMaps())
        End Function

        Function ViewMap(id As Integer) As ActionResult
            Dim model = GetMap(id)

            If model Is Nothing Then
                If id <> 1 Then
                    Return RedirectToAction("ViewMap", New With {.id = 1})
                Else
                    model = New MapModel
                    model.Name = "sample-floor"
                    model.FilePath = "/Content/Images/VisualSwitchInterface/sample-floor.png"
                    SaveMap(model)
                End If
            End If

            model.ReadImage(Server.MapPath(model.FilePath))
            ViewBag.otherMaps = GetAllMaps().Where(Function(x) x.Id <> model.Id).ToList()
            Return View(model)
        End Function

        Function _SwitchClicked(id As Integer) As ActionResult
            Dim model = GetSwitch(id)
            Return PartialView(model)
        End Function

        Function _AddSwitch(id As Integer, coordX As Integer, coordY As Integer) As ActionResult
            Dim model = New SwitchModel With {.MapId = id, .CoordX = coordX, .CoordY = coordY}
            Return PartialView(model)
        End Function

        <HttpPost()>
        Function _AddSwitch(model As SwitchModel) As ActionResult
            If Validate(model) Then
                SaveSwitch(model)
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
                Dim map = GetMap(model.MapId)
                Dim modelQueryByName = map.SwitchModels.FirstOrDefault(Function(x) x.Name.Equals(model.Name, StringComparison.OrdinalIgnoreCase))
                If modelQueryByName IsNot Nothing Then
                    If model.Id = 0 Or modelQueryByName.Id <> model.Id Then
                        ModelState.AddModelError("Name", String.Format("Switch name already exists in '{0}'.", map.Name))
                        isValid = False
                    End If
                End If

            End If

            Return isValid
        End Function

        Private Function GetUserDataDirectory() As String
            Dim directoryPath = Server.MapPath("~/_UserData")
            If Not Directory.Exists(directoryPath) Then
                Directory.CreateDirectory(directoryPath)
            End If
            Return directoryPath
        End Function

        Private Function GetMapFile() As String
            Return Path.Combine(GetUserDataDirectory(), "Map.json")
        End Function

        Private Function GetAllMaps() As IList(Of MapModel)
            If Not IO.File.Exists(GetMapFile()) Then
                Return New List(Of MapModel)()
            End If
            Return JsonConvert.DeserializeObject(Of IList(Of MapModel))(IO.File.ReadAllText(GetMapFile()))
        End Function

        Private Function GetMap(id As Integer) As MapModel
            Return GetAllMaps().FirstOrDefault(Function(x) x.Id = id)
        End Function

        Sub SaveMap(model As MapModel)
            Dim maps = GetAllMaps()

            If maps.Count > 0 Then
                model.Id = maps.Max(Of Integer)(Function(x) x.Id) + 1
            Else
                model.Id = 1
            End If

            maps.Add(model)
            IO.File.WriteAllText(GetMapFile(), JsonConvert.SerializeObject(maps))
        End Sub

        Sub SaveSwitch(model As SwitchModel)
            Dim maps = GetAllMaps()
            Dim map = maps.FirstOrDefault(Function(x) x.Id = model.MapId)
            Dim allSwitchModels = maps.SelectMany(Function(x) x.SwitchModels).ToList()

            If allSwitchModels.Count > 0 Then
                model.Id = allSwitchModels.Max(Of Integer)(Function(x) x.Id) + 1
            Else
                model.Id = 1
            End If

            map.SwitchModels.Add(model)
            IO.File.WriteAllText(GetMapFile(), JsonConvert.SerializeObject(maps))
        End Sub

        Private Function GetSwitch(id As Integer) As SwitchModel
            Return GetAllMaps().SelectMany(Function(x) x.SwitchModels).FirstOrDefault(Function(x) x.Id = id)
        End Function

    End Class
End Namespace