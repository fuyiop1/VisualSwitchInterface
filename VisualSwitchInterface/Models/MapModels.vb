Imports System.ComponentModel.DataAnnotations

Namespace Models
    Public Class MapModel
        Public Sub New()
            SwitchModels = New List(Of SwitchModel)
        End Sub

        Property FilePath() As String
        Property SwitchModels() As IList(Of SwitchModel)
        Property Width() As Integer
        Property Height() As Integer

        Sub ReadImage(file As String)
            Dim image = Drawing.Image.FromFile(file)
            Width = image.Width
            Height = image.Height
        End Sub
    End Class

    Public Class SwitchModel
        Property Id() As Integer

        <Required()>
        Property Name() As String
        Property CoordX() As Integer
        Property CoordY() As Integer
    End Class
End Namespace