Imports System.ComponentModel.DataAnnotations

Namespace Models
    Public Class AccountLoginModel
        <Required()>
        Property Username() As String

        <Required()>
        Property Password() As String

        Property IsLocked() As Boolean

    End Class
End Namespace