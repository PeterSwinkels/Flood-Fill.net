'This module's imports and settings.
Option Compare Binary
Option Explicit On
Option Infer Off
Option Strict On

Imports System
Imports System.Drawing
Imports System.Environment
Imports System.Text
Imports System.Windows.Forms

'This module contains this program's main interface window.
Public Class InterfaceWindow
   Private ColorDialogO As New ColorDialog                  'Contains the color dialog.
   Private MatchMode As MatchModesE = MatchModesE.RGBMode   'Contains the color match mode used.
   Private Tolerance As Integer = 0                         'Contains the maximum color difference allowed.

   'This procedure initializes this window.
   Public Sub New()
      Try
         InitializeComponent()

         With My.Application.Info
            Me.Text = $"{ .Title} v{ .Version} - by: { .CompanyName}"
         End With

         With My.Computer.Screen.WorkingArea
            Me.Size = New Size(CInt(.Width / 1.1), CInt(.Height / 1.1))
         End With
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try
   End Sub

   'This procedure handles the user's mouse clicks.
   Private Sub ImageBox_MouseClick(sender As Object, e As MouseEventArgs) Handles ImageBox.MouseClick
      Try
         Dim ImageO As Bitmap = New Bitmap(ImageBox.Image)

         If e.Button = MouseButtons.Left Then
            MousePointer(Busy:=True)
            ImageBox.Image = FloodFill(ImageO, e.X, e.Y, ImageO.GetPixel(e.X, e.Y), ColorDialogO.Color, Tolerance, MatchMode)
            MousePointer(Busy:=False)
         End If
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try
   End Sub

   'This procedure handles the user's key strokes.
   Private Sub InterfaceWindow_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
      Try
         Dim NewMatchMode As String = Nothing
         Dim NewTolerance As String = Nothing
         Dim TextO As New StringBuilder

         Select Case e.KeyCode
            Case Keys.C
               ColorDialogO.ShowDialog()
            Case Keys.M
               For Description As Integer = MATCH_MODES.GetLowerBound(0) To MATCH_MODES.GetUpperBound(0)
                  TextO.Append($"{Description} = {MATCH_MODES(Description)}")
                  If Description < MATCH_MODES.GetUpperBound(0) Then TextO.Append(NewLine)
               Next Description

               NewMatchMode = Microsoft.VisualBasic.InputBox(TextO.ToString(), , CStr(MatchMode))
               If Not NewMatchMode = Nothing Then MatchModesE.TryParse(NewMatchMode, MatchMode)
            Case Keys.P
               With My.Computer
                  If .Clipboard.ContainsImage() Then
                     ImageBox.Image = .Clipboard.GetImage()
                  Else
                     MessageBox.Show("No image on clipboard.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                  End If
               End With
            Case Keys.T
               NewTolerance = Microsoft.VisualBasic.InputBox("New tolerance:", , CStr(Tolerance))
               If Not NewTolerance = Nothing Then Integer.TryParse(NewTolerance, Tolerance)
            Case Keys.F1
               TextO.Append($"F1 = This help.{NewLine}")
               TextO.Append($"C = Select a color.{NewLine}")
               TextO.Append($"M = Select a match mode.{NewLine}")
               TextO.Append($"P = Paste an image.{NewLine}")
               TextO.Append("T = Specifiy tolerance.")
               MessageBox.Show(TextO.ToString(), My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information)
         End Select
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try
   End Sub

   'This procedure adjusts this windows's controls to its new size.
   Private Sub InterfaceWindow_Resize(sender As Object, e As EventArgs) Handles Me.Resize
      Try
         Dim ImageO As Bitmap = Nothing

         With ImageBox
            If .Image IsNot Nothing Then ImageO = New Bitmap(.Image)
            .Size = Me.ClientSize()
            .Image = New Bitmap(.ClientSize.Width, .ClientSize.Height)
            Graphics.FromImage(.Image).FillRectangle(Brushes.White, .ClientRectangle)
            If ImageO IsNot Nothing Then Graphics.FromImage(.Image).DrawImage(ImageO, New Point(0, 0))
         End With
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try
   End Sub

   'This procedure manages the mouse pointer.
   Private Sub MousePointer(Busy As Boolean)
      Try
         Me.Cursor = If(Busy, Cursors.WaitCursor, Cursors.Default)

         For Each Item As Control In Me.Controls
            Item.Cursor = If(Busy, Cursors.WaitCursor, Cursors.Default)
         Next Item
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try
   End Sub
End Class
