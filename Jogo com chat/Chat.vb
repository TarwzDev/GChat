Imports System.IO
Imports System.Net.Sockets
Imports System.Text

Public Class Chat
    Public meuip As String
    Public ipamigo As String
    Dim listener As New TcpListener(6060)
    Dim cliente As New TcpClient
    Dim tidf As String
    Dim linhatexto As String
    Dim fluxotexto As IO.StreamReader
    Dim einl As IO.StreamWriter

    Private Sub Chat_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If IO.File.Exists("C:\temp\TempG\profile\imdfundo.txt") Then
            fluxotexto = New IO.StreamReader("C:\temp\TempG\profile\imdfundo.txt")
            linhatexto = fluxotexto.ReadLine
            PictureBox1.ImageLocation = linhatexto
            fluxotexto.Close()
        End If
        Try
            listener.Start()
        Catch ex As Exception
            MessageBox.Show("Erro ao iniciar")
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        cliente = New TcpClient(ipamigo, 6060)
       
        If TextBox2.Text = "" Then
            MsgBox("O campo está vazio", MsgBoxStyle.OkOnly)
        Else

            Dim sw As New StreamWriter(cliente.GetStream())
            sw.Write(TextBox2.Text)
            sw.Flush()
            TextBox1.AppendText(TextBox2.Text + vbNewLine)
            TextBox2.Clear()
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Dim msn As String = ""
        If listener.Pending = True Then
            cliente = listener.AcceptTcpClient
            Dim tr As New StreamReader(cliente.GetStream())
            While tr.Peek > -1
                msn &= Convert.ToChar(tr.Read()).ToString

            End While
            TextBox1.AppendText(msn + vbNewLine)

        End If


    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Panel1.Visible = False
        Button3.Size = New Size(27, 26)
        Button3.Visible = True
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Button3.Size = New Size(20, 20)
        Panel1.Visible = True
        Button3.Visible = False
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Application.Exit()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim ope As New OpenFileDialog

        If ope.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox1.ImageLocation = ope.FileName

            Dim path As String = "c:\temp\TempG\profile\imdfundo.txt"
            Dim context As FileStream = File.Create(path)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(ope.FileName)
            context.Write(info, 0, info.Length)
            context.Close()

        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click, PictureBox1.MouseLeave, Panel2.MouseLeave
        Panel2.Visible = False
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Panel2.Visible = True
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If cliente.Connected = False Then
        Else
            PictureBox2.Visible = False
            Label2.Visible = False
            ProgressBar1.Visible = False
            Timer2.Enabled = False
            PictureBox3.Visible = False
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Form1.Show()
        Me.Close()
    End Sub
End Class