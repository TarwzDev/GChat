Imports System.Threading
Imports System.Net.Sockets
Imports System.IO
Public Class Form1
    Dim lot As IO.StreamReader
    Dim confirm As IO.StreamReader
    Dim cag As String
    Dim cag2 As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If IO.File.Exists("C:\Temp\TempG\version\old\version.txt") = True Then
            lot = New StreamReader("C:\Temp\TempG\version\old\version.txt")
            confirm = New StreamReader("C:\Temp\TempG\version\nv\version.txt")
            cag = lot.ReadLine
            cag2 = confirm.ReadLine
            If cag <= cag2 Then
                lot.Close()
                confirm.Close()
                Chat.meuip = TextBox1.Text
                Chat.ipamigo = TextBox2.Text
                Chat.Show()
                Me.Hide()
            ElseIf cag > cag2 Then
                Dim da As New System.Net.WebClient()
                da.DownloadFile("http://chattenata.xp3.biz/atualizar/" & cag & ".exe", "C:\Temp\TempG\atualizar\" & cag & ".exe")
                MsgBox("precisamos atualizar", MsgBoxStyle.OkOnly)
                Process.Start("C:\Temp\TempG\Atualizando")
                Application.Exit()
                lot.Close()
                confirm.Close()
            End If
        ElseIf IO.File.Exists("C:\Temp\TempG\version\old\version.txt") = False Then
            Chat.meuip = TextBox1.Text
            Chat.ipamigo = TextBox2.Text
            Chat.Show()
            Me.Hide()
            lot.Close()
            confirm.Close()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim os As IO.StreamReader
        Dim us As String
        os = New StreamReader("C:\Temp\TempG\version\nv\version.txt")
        us = os.ReadLine
        os.Close()

        Dim osip As IO.StreamReader
        Dim usip As String
        osip = New StreamReader("C:\Temp\TempG\ip.txt")
        usip = osip.ReadLine
        osip.Close()

        If usip = "" Then
        Else
            TextBox1.Text = usip
            TextBox2.Select()
            TextBox2.Focus()

        End If

        Label3.Text = "Versão  " & us
        Dim vv As New System.Net.WebClient()
        vv.DownloadFile("http://chattenata.xp3.biz/version/version.txt", "C:\Temp\TempG\version\old\version.txt")

    End Sub
End Class
