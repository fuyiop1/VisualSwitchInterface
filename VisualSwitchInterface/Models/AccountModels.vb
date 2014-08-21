Imports System.ComponentModel.DataAnnotations

Namespace Models
    Public Class AccountLoginModel
        <Required()>
        Property Username() As String

        <Required()>
        Property Password() As String

    End Class
End Namespace