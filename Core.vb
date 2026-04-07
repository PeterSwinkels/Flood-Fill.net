'This module's imports and settings.
Option Compare Binary
Option Explicit On
Option Infer Off
Option Strict On

Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Environment
Imports System.Linq
Imports System.Math
Imports System.Windows.Forms

'This module contains this program's core procedures.
Public Module CoreModule
   'This enumeration contains the color matching modes.
   Public Enum MatchModesE As Integer
      BrightnessMode   'Luminance matching mode.
      HueMode          'Hue matching mode.
      RGBMode          'RGB matching mode.
   End Enum

   Public ReadOnly MATCH_MODES() As String = {"Luminance", "Hue", "RGB"}   'Contains the color match mode descriptions.

   'This procedure checks whether the two specified colors match within the tolerance specified.
   Private Function ColorsMatch(Color1 As Color, Color2 As Color, Optional Tolerance As Integer = 0, Optional MatchMode As MatchModesE = MatchModesE.RGBMode) As Boolean
      Try
         Select Case MatchMode
            Case MatchModesE.HueMode
               Return (Abs(Color2.GetHue - Color1.GetHue) <= Tolerance)
            Case MatchModesE.BrightnessMode
               Return (Abs((Color2.GetBrightness * Byte.MaxValue) - (Color1.GetBrightness * Byte.MaxValue)) <= Tolerance)
            Case MatchModesE.RGBMode
               Return ({Abs(CInt(Color2.R) - CInt(Color1.R)), Abs(CInt(Color2.G) - CInt(Color1.G)), Abs(CInt(Color2.B) - CInt(Color1.B))}.Average() <= Tolerance)
         End Select
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try

      Return False
   End Function

   'This procudure displays any exceptions that occur.
   Public Sub DisplayException(ExceptionO As Exception)
      Try
         MessageBox.Show(ExceptionO.Message, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error)
      Catch
         [Exit](0)
      End Try
   End Sub

   'This procedure performs a flood fill operation at the specified location.
   Public Function FloodFill(ImageO As Bitmap, x As Integer, y As Integer, OldColor As Color, NewColor As Color, Tolerance As Integer, MatchMode As MatchModesE) As Bitmap
      Try
         Dim Filled As New Bitmap(ImageO.Width, ImageO.Height)
         Dim Nodes As New List(Of Point)
         Dim NewNodes As New List(Of Point)

         With ImageO
            Graphics.FromImage(Filled).FillRectangle(Brushes.Black, Filled.GetBounds(GraphicsUnit.Pixel))

            If Not ColorsMatch(NewColor, OldColor) Then
               Nodes.Add(New Point(x, y))
               Do
                  NewNodes.Clear()
                  For Each Node As Point In Nodes
                     If ColorsMatch(Filled.GetPixel(Node.X, Node.Y), Color.Black) Then
                        If ColorsMatch(.GetPixel(Node.X, Node.Y), OldColor, Tolerance, MatchMode) Then
                           .SetPixel(Node.X, Node.Y, NewColor)
                           Filled.SetPixel(Node.X, Node.Y, Color.White)
                        End If
                     End If
                     For Each CheckNode As Point In {New Point(Node.X - 1, Node.Y), New Point(Node.X + 1, Node.Y), New Point(Node.X, Node.Y - 1), New Point(Node.X, Node.Y + 1)}
                        If CheckNode.X >= 0 AndAlso CheckNode.X < .Width AndAlso CheckNode.Y >= 0 AndAlso CheckNode.Y < .Height Then
                           If ColorsMatch(Filled.GetPixel(CheckNode.X, CheckNode.Y), Color.Black) Then
                              If ColorsMatch(.GetPixel(CheckNode.X, CheckNode.Y), OldColor, Tolerance, MatchMode) Then
                                 .SetPixel(CheckNode.X, CheckNode.Y, NewColor)
                                 Filled.SetPixel(CheckNode.X, CheckNode.Y, Color.White)
                                 NewNodes.Add(CheckNode)
                              End If
                           End If
                        End If
                     Next CheckNode
                  Next Node
                  If Not NewNodes.Any Then Exit Do
                  Nodes = New List(Of Point)(NewNodes)
               Loop
            End If
         End With

         Return ImageO
      Catch ExceptionO As Exception
         DisplayException(ExceptionO)
      End Try

      Return Nothing
   End Function
End Module
