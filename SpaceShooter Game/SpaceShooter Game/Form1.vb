Public Class Form1
    Dim SRight As Boolean
    Dim SLeft As Boolean
    Dim ShooterSpeed As Integer
    Dim ShotSpeed As Integer
    Dim InvaderSpeed As Integer
    Dim InvaderDrop As Integer
    Const NumOfInvaders As Integer = 30
    Dim IRight(NumOfInvaders) As Boolean
    Dim Invaders(NumOfInvaders) As PictureBox
    Dim x As Integer
    Dim ShotDown As Integer
    Dim Paused As Boolean

    Private Sub TimerMain_Tick(sender As Object, e As EventArgs) Handles TimerMain.Tick
        MoveShooter()
        FireShot()
        MoveInvader()
        CheckHit()
        CheckGameOver()
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyValue = Keys.Right Then
            SRight = True
            SLeft = False
        End If
        If e.KeyValue = Keys.Left Then
            SLeft = True
            SRight = False
        End If

        If e.KeyValue = Keys.Space And shot.Visible = False Then
            My.Computer.Audio.Play(My.Resources.Shoot, AudioPlayMode.Background)
            shot.Top = shooter.Top
            shot.Left = shooter.Left + (shooter.Width / 2) - (shot.Width / 2)
            shot.Visible = True
        End If
    End Sub

    Private Sub MoveShooter()
        If SRight = True And Shooter.Left + Shooter.Width < Me.ClientRectangle.Width Then
            shooter.Left += ShooterSpeed
        End If
        If SLeft = True And Shooter.Left > Me.ClientRectangle.Left Then
            shooter.Left -= ShooterSpeed
        End If
    End Sub

    Private Sub Form1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = "P" Or e.KeyChar = "p" Then
            If Paused = True Then
                TimerMain.Enabled = True
                Timer1.Enabled = True
                Timer2.Enabled = True
                lbpause.Visible = False
                Label1.Visible = False
                Paused = False
            Else
                TimerMain.Enabled = False
                Timer1.Enabled = False
                Timer2.Enabled = False
                lbpause.Visible = True
                Label1.Visible = True
                Paused = True
            End If
        End If
    End Sub

    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.Right Then
            SRight = False
        End If
        If e.KeyValue = Keys.Left Then
            SLeft = False
        End If
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadInvaders()
        LoadSettings()
    End Sub

    Private Sub LoadSettings()
        Paused = False
        ShotSpeed = 35
        ShooterSpeed = 6
        shot.Visible = False
        My.Computer.Audio.Play(My.Resources.Game_Start, AudioPlayMode.Background)

        For Me.x = 1 To NumOfInvaders
            IRight(x) = False
            Invaders(x).Left = (-62 * x) - (x * 5)
            Invaders(x).Top = 0
            Invaders(x).Visible = True
        Next
        shooter.Left = Me.Width / 2
        PictureBox1.Width = 851
        InvaderSpeed = 3
        InvaderDrop = 40
        ShotDown = 0
        SRight = False
        SLeft = False
        TimerMain.Enabled = True
        Timer1.Start()
        Timer2.Start()
        shotenemy.Location = New Point(shotenemy.Location.Y = 3)
        shotenemy1.Location = New Point(shotenemy1.Location.Y = 3)
        shotenemy2.Location = New Point(shotenemy2.Location.Y = 3)
        shotenemy3.Location = New Point(shotenemy3.Location.Y = 3)
        shotenemy4.Location = New Point(shotenemy3.Location.Y = 3)
    End Sub
    Private Sub resetscore()
        lbscorecount.Text = 0
    End Sub

    Private Sub FireShot()
        If shot.Visible = True Then
            shot.Top -= ShotSpeed
        End If

        If shot.Top + shot.Height < Me.ClientRectangle.Top Then
            shot.Visible = False
        End If
    End Sub

    Private Sub MoveInvader()
        For Me.x = 1 To NumOfInvaders
            If IRight(x) = True Then
                Invaders(x).Left += InvaderSpeed
            Else
                Invaders(x).Left -= InvaderSpeed
            End If

            If Invaders(x).Left + Invaders(x).Width > Me.ClientRectangle.Width And IRight(x) = True Then
                IRight(x) = False
                Invaders(x).Top += InvaderDrop
            End If

            If Invaders(x).Left < Me.ClientRectangle.Left And IRight(x) = False Then
                IRight(x) = True
                Invaders(x).Top += InvaderDrop
            End If
        Next
    End Sub

    Private Sub CheckGameOver()
        For Me.x = 1 To NumOfInvaders
            If Invaders(x).Top + Invaders(x).Height >= shooter.Top And Invaders(x).Visible = True Then
                TimerMain.Enabled = False
                Me.x = NumOfInvaders
                MsgBox("Game over! Your score is : " & Val(lbscorecount.Text * 17) & ". Click OK to play again.")
                resetscore()
                PlayAgain()
            End If

        Next

        If ShotDown = NumOfInvaders Then
            TimerMain.Enabled = False
            PlayAgain()
        End If
    End Sub

    Private Sub CheckHit()
        For Me.x = 1 To NumOfInvaders
            If (shot.Top + shot.Height >= Invaders(x).Top) And (shot.Top <= Invaders(x).Top + Invaders(x).Height) And (shot.Left + shot.Width >= Invaders(x).Left) And (shot.Left <= Invaders(x).Left + Invaders(x).Width) And shot.Visible = True And Invaders(x).Visible = True Then
                My.Computer.Audio.Play(My.Resources.Hit, AudioPlayMode.Background)
                Invaders(x).Visible = False
                shot.Visible = False
                ShotDown += 1
                lbscorecount.Text += 1
            End If
        Next
    End Sub
    Private Sub checkdamage()
        If shooter.Bounds.IntersectsWith(shotenemy.Bounds) Then
            shotenemy.Visible = False
            TimerMain.Enabled = False
            Timer1.Enabled = False
            Timer2.Enabled = False
            MsgBox("You died.. Your score is : " & Val(lbscorecount.Text * 17) & ". Press OK to play again!")
            resetscore()
            PlayAgain()
        End If
        If shooter.Bounds.IntersectsWith(shotenemy1.Bounds) Then
            shotenemy1.Visible = False
            TimerMain.Enabled = False
            Timer1.Enabled = False
            Timer2.Enabled = False
            MsgBox("You died.. Your score is : " & Val(lbscorecount.Text * 17) & ". Press OK to play again!")
            resetscore()
            PlayAgain()
        End If
        If shooter.Bounds.IntersectsWith(shotenemy2.Bounds) Then
            shotenemy2.Visible = False
            TimerMain.Enabled = False
            Timer1.Enabled = False
            Timer2.Enabled = False
            MsgBox("You died.. Your score is : " & Val(lbscorecount.Text * 17) & ". Press OK to play again!")
            resetscore()
            PlayAgain()
        End If
        If shooter.Bounds.IntersectsWith(shotenemy3.Bounds) Then
            shotenemy3.Visible = False
            TimerMain.Enabled = False
            Timer1.Enabled = False
            Timer2.Enabled = False
            MsgBox("You died.. Your score is : " & Val(lbscorecount.Text * 17) & ". Press OK to play again!")
            resetscore()
            PlayAgain()
        End If
        If shooter.Bounds.IntersectsWith(shotenemy4.Bounds) Then
            shotenemy4.Visible = False
            TimerMain.Enabled = False
            Timer1.Enabled = False
            Timer2.Enabled = False
            MsgBox("You died.. Your score is : " & Val(lbscorecount.Text * 17) & ". Press OK to play again!")
            resetscore()
            PlayAgain()
        End If
    End Sub

    Private Sub LoadInvaders()
        For Me.x = 1 To NumOfInvaders
            Invaders(x) = New PictureBox
            Invaders(x).Image = My.Resources.enemybmp
            Invaders(x).Width = 50
            Invaders(x).Height = 33
            Invaders(x).BackColor = Color.Transparent
            Invaders(x).SizeMode = PictureBoxSizeMode.StretchImage
            Controls.Add(Invaders(x))
        Next

    End Sub
    Private Sub PlayAgain()
        LoadSettings()
        InvaderSpeed += 1
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        shotenemy.Visible = True
        shotenemy.Left = shooterenemy.Left + (shooterenemy.Width / 2) - (shotenemy.Width / 2)
        shotenemy.Top += 5

        If shotenemy.Location.Y >= 485 Then
            shotenemy.Visible = False
            shotenemy.Location = New Point(shotenemy.Location.Y = 3)
        End If
        checkdamage()


        shotenemy1.Visible = True
        shotenemy1.Left = shooterenemy1.Left + (shooterenemy1.Width / 2) - (shotenemy1.Width / 2)
        shotenemy1.Top += 8

        If shotenemy1.Location.Y >= 485 Then
            shotenemy1.Visible = False
            shotenemy1.Location = New Point(shotenemy1.Location.Y = 3)
        End If
        checkdamage()


        shotenemy2.Visible = True
        shotenemy2.Left = shooterenemy2.Left + (shooterenemy2.Width / 2) - (shotenemy2.Width / 2)
        shotenemy2.Top += 4

        If shotenemy2.Location.Y >= 485 Then
            shotenemy2.Visible = False
            shotenemy2.Location = New Point(shotenemy2.Location.Y = 3)
        End If
        checkdamage()


        shotenemy3.Visible = True
        shotenemy3.Left = shooterenemy3.Left + (shooterenemy3.Width / 2) - (shotenemy3.Width / 2)
        shotenemy3.Top += 7

        If shotenemy3.Location.Y >= 485 Then
            shotenemy3.Visible = False
            shotenemy3.Location = New Point(shotenemy3.Location.Y = 3)
        End If
        checkdamage()

        shotenemy4.Visible = True
        shotenemy4.Left = shooterenemy4.Left + (shooterenemy4.Width / 2) - (shotenemy4.Width / 2)
        shotenemy4.Top += 5

        If shotenemy4.Location.Y >= 485 Then
            shotenemy4.Visible = False
            shotenemy4.Location = New Point(shotenemy4.Location.Y = 3)
        End If
        checkdamage()

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        PictureBox1.Width -= 1
        If PictureBox1.Width = 10 Then
            TimerMain.Enabled = False
            Timer1.Enabled = False
            Timer2.Enabled = False
            MsgBox("You were too slow... Your score is : " & Val(lbscorecount.Text * 17) & ". Click OK to try again.")
            PlayAgain()
            resetscore()
        End If
    End Sub
End Class
