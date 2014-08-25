Namespace Models
    Public Class MapModel
        Public Sub New()
            SwitchModels = New List(Of SwitchModel)
        End Sub

        Property FilePath() As String
        Property SwitchModels() As IList(Of SwitchModel)
    End Class

    Public Class SwitchModel
        Property Id() As Integer
        Property Name() As String
        Property CoordX() As Double
        Property CoordY() As Double
    End Class
End Namespace