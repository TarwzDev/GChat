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
        If IO.File.Exists("C:\Temp\TempG\conf\imdfundo.txt") Then
            fluxotexto = New IO.StreamReader("C:\Temp\TempG\conf\imdfundo.txt")
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
        ElseIf TextBox2.Text.Contains(":help") Or TextBox2.Text.Contains(":desligar") Or TextBox2.Text.Contains(":booo") Or TextBox2.Text.Contains(":matrix") Or TextBox2.Text.Contains(":whatsapp") Then
            TextBox1.AppendText((ven & TextBox2.Text & "   ") + vbNewLine)
            Dim sw As New StreamWriter(cliente.GetStream())
            sw.Write(env & nome & " o comando  " & TextBox2.Text & "   ")
            sw.Flush()
            sw.Close()
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
            If k7.Contains(":help") = True Or k7.Contains("?Rejeitado?") = True Or k7.Contains("?aceito?") Or k7.Contains("?fui?") = True Or k7.Contains("?ativarnotjv?") = True Or k7.Contains("?closejv?") = True Or k7.Contains("B1") = True Or k7.Contains("B2") = True Or k7.Contains("B3") = True Or k7.Contains("B4") = True Or k7.Contains("B5") = True Or k7.Contains("B6") = True Or k7.Contains("B7") = True Or k7.Contains("B8") = True Or k7.Contains("B9") = True Then
            Else
                TextBox1.AppendText(msn + vbNewLine)
                msn = ""
            End If
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
        ope.Filter = "Images |*.jpg; *.jpeg; *.png"
        If ope.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox1.ImageLocation = ope.FileName

            Dim path As String = "C:\Temp\TempG\conf\imdfundo.txt"
            Dim context As FileStream = File.Create(path)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(ope.FileName)
            context.Write(info, 0, info.Length)
            context.Close()
            Panel2.Visible = False

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
            If TextBox2.Text.Contains(":desligar") Or TextBox2.Text.Contains(":booo") Or TextBox2.Text.Contains(":matrix") Or TextBox2.Text.Contains(":whatsapp") Then
                TextBox1.AppendText((ven & TextBox2.Text & "   ") + vbNewLine)
                Dim sw As New StreamWriter(cliente.GetStream())
                sw.Write(env & nome & " o comando  " & TextBox2.Text & "   ")
                sw.Flush()
                sw.Close()
                TextBox2.Text = ""
            Else

                TextBox1.AppendText((ven & TextBox2.Text & "   ") + vbNewLine)
                Dim sw As New StreamWriter(cliente.GetStream())
                sw.Write(env & nome & "   " & TextBox2.Text & "   ")
                sw.Flush()
                sw.Close()
                TextBox2.Text = ""
            End If
        End If

        If e.KeyData = Keys.A And Panel1.Visible = False Or e.KeyData = Keys.A And Panel2.Visible = False Then

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


    'aqui é a parte dos comandos kkkk

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If ipamigo = meuip Then
            conectado.Text = "Solitário"
        ElseIf ipamigo <> meuip Then
            conectado.Text = "Conectado"
        End If

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
            TextBox1.AppendText(vbNewLine & "':help', ':buuu', ':whatsapp', ':matrix', ':desligar'" & vbNewLine)
            msn = ""
            k7 = ""
        ElseIf k7.Contains("?ativarnotjv?") = True Then
            Panel5.Visible = True
            msn = ""
            k7 = ""
            'rejeitado e aceito
        ElseIf k7.Contains("?Rejeitado?") = True Then
            MsgBox("Você foi REJEITADO ^-^", MsgBoxStyle.OkOnly)
            Panel5.Visible = False
            Panel3.Visible = False
            msn = ""
            k7 = ""
        ElseIf k7.Contains("?aceito?") = True Then
            msn = ""
            k7 = ""
            MsgBox("Você foi Aceito ^-^", MsgBoxStyle.OkOnly)
            Panel5.Visible = False
            Panel3.Visible = False
            Panel4.Location = New Point(290, 75)
            Panel4.Visible = True
            jv = 1
            jvm = 0

        ElseIf k7.Contains("?fui?") = True Then
            jvm = 1
            msn = ""
            k7 = ""
        End If

        If jv = 1 And ipamigo <> meuip Then    'cores do jogo da velha

            If k7.Contains("B1") = True Then
                Button12.BackColor = Color.Red
                msn = ""
                k7 = ""
            ElseIf k7.Contains("B2") = True Then
                Button14.BackColor = Color.Red
                msn = ""
                k7 = ""
            ElseIf k7.Contains("B3") = True Then
                Button15.BackColor = Color.Red
                msn = ""
                k7 = ""
            ElseIf k7.Contains("B4") = True Then
                Button16.BackColor = Color.Red
                msn = ""
                k7 = ""
            ElseIf k7.Contains("B5") = True Then
                Button17.BackColor = Color.Red
                msn = ""
                k7 = ""
            ElseIf k7.Contains("B6") = True Then
                Button18.BackColor = Color.Red
                msn = ""
                k7 = ""
            ElseIf k7.Contains("B7") = True Then
                Button19.BackColor = Color.Red
                msn = ""
                k7 = ""
            ElseIf k7.Contains("B8") = True Then
                Button20.BackColor = Color.Red
                msn = ""
                k7 = ""
            ElseIf k7.Contains("B9") = True Then
                Button21.BackColor = Color.Red
                msn = ""
                k7 = ""
            ElseIf k7.Contains("?ppa?") Then
                pm += 1
                msn = ""
                k7 = ""
            End If

            If Button12.BackColor = cr And Button14.BackColor = cr And Button15.BackColor = cr Or Button16.BackColor = cr And Button17.BackColor = cr And Button18.BackColor = cr Or Button19.BackColor = cr And Button20.BackColor = cr And Button21.BackColor = cr Or Button19.BackColor = cr And Button16.BackColor = cr And Button12.BackColor = cr Or Button14.BackColor = cr And Button17.BackColor = cr And Button20.BackColor = cr Or Button15.BackColor = cr And Button18.BackColor = cr And Button21.BackColor = cr Or Button12.BackColor = cr And Button17.BackColor = cr And Button21.BackColor = cr Or Button15.BackColor = cr And Button17.BackColor = cr And Button19.BackColor = cr Then
                MsgBox("você perdeu", MsgBoxStyle.OkOnly)
                Button12.BackColor = Color.Lime
                Button14.BackColor = Color.Lime
                Button15.BackColor = Color.Lime
                Button16.BackColor = Color.Lime
                Button17.BackColor = Color.Lime
                Button18.BackColor = Color.Lime
                Button19.BackColor = Color.Lime
                Button20.BackColor = Color.Lime
                Button21.BackColor = Color.Lime
            ElseIf Button12.BackColor = cb And Button14.BackColor = cb And Button15.BackColor = cb Or Button16.BackColor = cb And Button17.BackColor = cb And Button18.BackColor = cb Or Button19.BackColor = cb And Button20.BackColor = cb And Button21.BackColor = cb Or Button19.BackColor = cb And Button16.BackColor = cb And Button12.BackColor = cb Or Button14.BackColor = cb And Button17.BackColor = cb And Button20.BackColor = cb Or Button15.BackColor = cb And Button18.BackColor = cb And Button21.BackColor = cb Or Button12.BackColor = cb And Button17.BackColor = cb And Button21.BackColor = cb Or Button15.BackColor = cb And Button17.BackColor = cb And Button19.BackColor = cb Then
                MsgBox("você ganhou", MsgBoxStyle.OkOnly)
                cliente = New TcpClient(ipamigo, 6060)
                Dim sw As New StreamWriter(cliente.GetStream())
                sw.Write("?ppa?")
                sw.Flush()
                sw.Close()
                TextBox2.Text = ""
                Button12.BackColor = Color.Lime
                Button14.BackColor = Color.Lime
                Button15.BackColor = Color.Lime
                Button16.BackColor = Color.Lime
                Button17.BackColor = Color.Lime
                Button18.BackColor = Color.Lime
                Button19.BackColor = Color.Lime
                Button20.BackColor = Color.Lime
                Button21.BackColor = Color.Lime
            ElseIf Button12.BackColor <> Color.Lime And Button14.BackColor <> Color.Lime And Button15.BackColor <> Color.Lime And Button16.BackColor <> Color.Lime And Button17.BackColor <> Color.Lime And Button18.BackColor <> Color.Lime And Button19.BackColor <> Color.Lime And Button20.BackColor <> Color.Lime And Button21.BackColor <> Color.Lime Then
                Button12.BackColor = Color.Lime
                Button14.BackColor = Color.Lime
                Button15.BackColor = Color.Lime
                Button16.BackColor = Color.Lime
                Button17.BackColor = Color.Lime
                Button18.BackColor = Color.Lime
                Button19.BackColor = Color.Lime
                Button20.BackColor = Color.Lime
                Button21.BackColor = Color.Lime
                MsgBox("Deu empate", MsgBoxStyle.OkOnly)

                If jvm = 0 Then
                    Label7.Text = "Vez de " & nome
                ElseIf jvm = 1 Then
                    Label7.Text = "Sua vez"
                End If
                Label8.Text = pm
                Label9.Text = pa
            ElseIf k7.Contains("?closejv?") Then
                msn = ""
                k7 = ""
                pa = 0
                pm = 0
                ativo = ""
                jv = 0
                jvm = 0
                Panel4.Visible = False
                MsgBox("Seu amigo fechou o jogo ;-;", MsgBoxStyle.OkOnly)
            End If

        End If


    End Sub
    Dim pa As Integer = 0
    Dim pm As Integer = 0
    Dim cb As ValueType = Color.Blue
    Dim cr As ValueType = Color.Red
    Dim ativo As String = ""
    Dim jv As Integer = 0
    Dim jvm As Integer = 0

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Panel3.Location = New Point(25, 307)
        Panel3.Visible = True
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Panel3.Location = New Point(15, 241)
        Panel3.Visible = False
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click

        If meuip = ipamigo Then
            Panel3.Visible = False
            MsgBox("Você esta usando o mesmo ip")

        ElseIf meuip <> ipamigo Then
            Panel5.Location = New Point(77, 37)
            cliente = New TcpClient(ipamigo, 6060)
            Dim sw As New StreamWriter(cliente.GetStream())
            sw.Write("?ativarnotjv?")
            sw.Flush()
            sw.Close()
            TextBox2.Text = ""
        End If
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        cliente = New TcpClient(ipamigo, 6060)
        Dim sw As New StreamWriter(cliente.GetStream())
        sw.Write("?Rejeitado?")
        Panel5.Visible = False
        Panel3.Visible = False
        sw.Flush()
        sw.Close()
        TextBox2.Text = ""
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        Panel4.Location = New Point(290, 75)
        cliente = New TcpClient(ipamigo, 6060)
        Dim sw As New StreamWriter(cliente.GetStream())
        sw.Write("?aceito?")
        jvm = 1
        jv = 1
        Panel5.Visible = False
        Panel3.Visible = False
        Panel4.Visible = True
        sw.Flush()
        sw.Close()
        TextBox2.Text = ""
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        If jv = 1 And jvm = 1 And Button1.BackColor = Color.Lime Then
            cliente = New TcpClient(ipamigo, 6060)
            Dim sw As New StreamWriter(cliente.GetStream())
            sw.Write("?fui? B1")
            Button12.BackColor = Color.Blue
            jvm = 0
        ElseIf jv = 1 And jvm = 0 And Button12.BackColor = Color.Lime Then
            MsgBox("Não é sua vez", MsgBoxStyle.OkOnly)
        ElseIf jv = 1 And jvm = 0 And Button12.BackColor <> Color.Lime Then
            MsgBox("essa casa já está ocupada", MsgBoxStyle.OkOnly)

        End If
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        If jv = 1 And jvm = 1 And Button20.BackColor = Color.Lime Then
            cliente = New TcpClient(ipamigo, 6060)
            Dim sw As New StreamWriter(cliente.GetStream())
            sw.Write("?fui? B8")
            Button20.BackColor = Color.Blue
            jvm = 0
        ElseIf jv = 1 And jvm = 0 And Button20.BackColor = Color.Lime Then
            MsgBox("Não é sua vez", MsgBoxStyle.OkOnly)
        ElseIf jv = 1 And jvm = 0 And Button20.BackColor <> Color.Lime Then
            MsgBox("essa casa já está ocupada", MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        If jv = 1 And jvm = 1 And Button19.BackColor = Color.Lime Then
            cliente = New TcpClient(ipamigo, 6060)
            Dim sw As New StreamWriter(cliente.GetStream())
            sw.Write("?fui? B7")
            Button19.BackColor = Color.Blue
            jvm = 0
        ElseIf jv = 1 And jvm = 0 And Button19.BackColor = Color.Lime Then
            MsgBox("Não é sua vez", MsgBoxStyle.OkOnly)
        ElseIf jv = 1 And jvm = 0 And Button19.BackColor <> Color.Lime Then
            MsgBox("essa casa já está ocupada", MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        If jv = 1 And jvm = 1 And Button18.BackColor = Color.Transparent Then
            cliente = New TcpClient(ipamigo, 6060)
            Dim sw As New StreamWriter(cliente.GetStream())
            sw.Write("?fui? B6")
            Button18.BackColor = Color.Blue
            jvm = 0
        ElseIf jv = 1 And jvm = 0 And Button18.BackColor = Color.Lime Then
            MsgBox("Não é sua vez", MsgBoxStyle.OkOnly)
        ElseIf jv = 1 And jvm = 0 And Button18.BackColor <> Color.Lime Then
            MsgBox("essa casa já está ocupada", MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        If jv = 1 And jvm = 1 And Button17.BackColor = Color.Lime Then
            cliente = New TcpClient(ipamigo, 6060)
            Dim sw As New StreamWriter(cliente.GetStream())
            sw.Write("?fui? B5")
            Button17.BackColor = Color.Blue
            jvm = 0
        ElseIf jv = 1 And jvm = 0 And Button17.BackColor = Color.Lime Then
            MsgBox("Não é sua vez", MsgBoxStyle.OkOnly)
        ElseIf jv = 1 And jvm = 0 And Button17.BackColor <> Color.Lime Then
            MsgBox("essa casa já está ocupada", MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        If jv = 1 And jvm = 1 And Button16.BackColor = Color.Lime Then
            cliente = New TcpClient(ipamigo, 6060)
            Dim sw As New StreamWriter(cliente.GetStream())
            sw.Write("?fui? B4")
            Button16.BackColor = Color.Blue
            jvm = 0
        ElseIf jv = 1 And jvm = 0 And Button16.BackColor = Color.Lime Then
            MsgBox("Não é sua vez", MsgBoxStyle.OkOnly)
        ElseIf jv = 1 And jvm = 0 And Button16.BackColor <> Color.Lime Then
            MsgBox("essa casa já está ocupada", MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        If jv = 1 And jvm = 1 And Button15.BackColor = Color.Lime Then
            cliente = New TcpClient(ipamigo, 6060)
            Dim sw As New StreamWriter(cliente.GetStream())
            sw.Write("?fui? B3")
            Button15.BackColor = Color.Blue
            jvm = 0
        ElseIf jv = 1 And jvm = 0 And Button15.BackColor = Color.Lime Then
            MsgBox("Não é sua vez", MsgBoxStyle.OkOnly)
        ElseIf jv = 1 And jvm = 0 And Button15.BackColor <> Color.Lime Then
            MsgBox("essa casa já está ocupada", MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        If jv = 1 And jvm = 1 And Button14.BackColor = Color.Lime Then
            cliente = New TcpClient(ipamigo, 6060)
            Dim sw As New StreamWriter(cliente.GetStream())
            sw.Write("?fui? B2")
            Button14.BackColor = Color.Blue
            jvm = 0
        ElseIf jv = 1 And jvm = 0 And Button14.BackColor = Color.Lime Then
            MsgBox("Não é sua vez", MsgBoxStyle.OkOnly)
        ElseIf jv = 1 And jvm = 0 And Button14.BackColor <> Color.Lime Then
            MsgBox("essa casa já está ocupada", MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        If jv = 1 And jvm = 1 And Button21.BackColor = Color.Lime Then
            cliente = New TcpClient(ipamigo, 6060)
            Dim sw As New StreamWriter(cliente.GetStream())
            sw.Write("?fui? B9")
            Button21.BackColor = Color.Blue
            jvm = 0
        ElseIf jv = 1 And jvm = 0 And Button21.BackColor = Color.Lime Then
            MsgBox("Não é sua vez", MsgBoxStyle.OkOnly)
        ElseIf jv = 1 And jvm = 0 And Button21.BackColor <> Color.Lime Then
            MsgBox("essa casa já está ocupada", MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        pa = 0
        pm = 0
        ativo = ""
        jv = 0
        jvm = 0
        Panel4.Visible = False
        cliente = New TcpClient(ipamigo, 6060)
        Dim sw As New StreamWriter(cliente.GetStream())
        sw.Write("?closejv?")
        sw.Flush()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        MsgBox("Em breve kkkk", MsgBoxStyle.OkOnly)
    End Sub
End Class