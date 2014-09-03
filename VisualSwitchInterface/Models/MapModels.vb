Imports System.ComponentModel.DataAnnotations
Imports Newtonsoft.Json

Namespace Models
    Public Class MapModel
        Public Sub New()
            SwitchModels = New List(Of SwitchModel)
        End Sub
        Property Id() As Integer
        Property Name() As String
        Property FilePath() As String
        Property SwitchModels() As IList(Of SwitchModel)

        <JsonIgnore()>
        Property Width() As Integer

        <JsonIgnore()>
        Property Height() As Integer

        Sub ReadImage(file As String)
            Dim image = Drawing.Image.FromFile(file)
            Width = image.Width
            Height = image.Height
        End Sub
    End Class

    Public Class SwitchModel
        Property Id() As Integer
        Property MapId() As Integer

        <Required()>
        Property Name() As String
        Property CoordX() As Integer
        Property CoordY() As Integer
        Property Width() As Integer
        Property Height() As Integer
    End Class
End Namespace