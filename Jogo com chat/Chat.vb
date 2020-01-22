Imports System.IO
Imports System.Net.Sockets
Imports System.Text
Imports System.Net

Public Class Chat
    Public meuip As String
    Public ipamigo As String
    Dim listener As New TcpListener(6060)
    Dim cliente As New TcpClient
    Dim tidf As String
    Dim linhatexto As String
    Dim fluxotexto As IO.StreamReader
    Dim nome As String = ""
    Dim fluxoli As IO.StreamReader
    Dim env As String = "Recebido de  "
    Dim ven As String = "Você enviou  "

    Dim k7 As String = ""

    Private Sub Chat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'aqui é o que muda o nome do enviado e recebido

        fluxoli = New IO.StreamReader("C:\Temp\TempG\continue.txt")
        nome = fluxoli.ReadLine
        fluxoli.Close()

        'aqui é o que muda a imagem do fundo
        If IO.File.Exists("C:\temp\TempG\profile\imdfundo.txt") Then
            fluxotexto = New IO.StreamReader("C:\temp\TempG\profile\imdfundo.txt")
            linhatexto = fluxotexto.ReadLine
            PictureBox1.ImageLocation = linhatexto
            fluxotexto.Close()
        End If
        Label1.Visible = False
        Label2.Visible = False

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
            TextBox1.AppendText((ven & TextBox2.Text & "   ") + vbNewLine)
            Dim sw As New StreamWriter(cliente.GetStream())
            sw.Write(env & nome & "   " & TextBox2.Text & "   ")
            sw.Flush()
            sw.Close()
            TextBox2.Text = ""
        End If
    End Sub

    Dim msn As String = ""

    Public Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick


        If listener.Pending = True Then

            cliente = listener.AcceptTcpClient

            Dim tr As New StreamReader(cliente.GetStream())
            While tr.Peek > -1
                msn &= Convert.ToChar(tr.Read()).ToString
                k7 = msn
            End While
            TextBox1.AppendText(msn + vbNewLine)
            msn = ""
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

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Panel2.Visible = False
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Panel2.Visible = True
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs)
        Form1.Show()
        Me.Close()
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown

        If e.KeyCode = 13 And TextBox2.Text <> "" Then
            cliente = New TcpClient(ipamigo, 6060)
            TextBox1.AppendText((ven & TextBox2.Text & "   ") + vbNewLine)
            Dim sw As New StreamWriter(cliente.GetStream())
            sw.Write(env & nome & "   " & TextBox2.Text & "   ")
            sw.Flush()
            sw.Close()
            TextBox2.Text = ""
        End If
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick

        If My.Computer.Network.Ping(ipamigo, 1000) Then
            PictureBox2.Visible = False
            PictureBox3.Visible = False
            ProgressBar1.Visible = False
            Button8.Visible = False
            Label3.Visible = False
            Timer4.Enabled = False
        End If



    End Sub

    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles Button8.Click

        Application.Exit()
       
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click

    End Sub


    'aqui agora é para o jogo

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If k7.Contains(":desligar") = True Then
            msn = ""
            k7 = ""
            MsgBox("Iremos fechar o seu conputador", MsgBoxStyle.OkOnly)

        ElseIf k7.Contains(":matrix") = True Then
            Process.Start("C:\Temp\TempG\conf\scripts\matrix.bat")
            msn = ""
            k7 = ""
        ElseIf k7.Contains(":whatsapp") = True Then
            My.Computer.Audio.Play("C:\Temp\TempG\conf\scripts\2.wav",
       AudioPlayMode.WaitToComplete)
            msn = ""
            k7 = ""
        ElseIf k7.Contains(":buuu") = True Then
            Process.Start("C:\Temp\TempG\conf\scripts\deslig.bat")
            msn = ""
            k7 = ""
        ElseIf k7.Contains(":help") = True Then
            TextBox1.AppendText(vbNewLine & "':help', ':buu', ':whatsapp', ':matrix', ':desligar'" & vbNewLine)
            msn = ""
            k7 = ""

        End If

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Panel3.Location = New Point(25, 307)
        Panel3.Visible = True
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Panel3.Location = New Point(15, 241)
        Panel3.Visible = False
    End Sub
End Class