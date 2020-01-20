Imports System.Threading
Imports System.Net.Sockets
Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Chat.meuip = TextBox1.Text
        Chat.ipamigo = TextBox2.Text
        Chat.Show()
        Me.Hide()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub
End Class
