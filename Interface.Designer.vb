<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InterfaceWindow
   Inherits System.Windows.Forms.Form

   'Form overrides dispose to clean up the component list.
   <System.Diagnostics.DebuggerNonUserCode()> _
   Protected Overrides Sub Dispose(ByVal disposing As Boolean)
      Try
         If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
         End If
      Finally
         MyBase.Dispose(disposing)
      End Try
   End Sub

   'Required by the Windows Form Designer
   Private components As System.ComponentModel.IContainer

   'NOTE: The following procedure is required by the Windows Form Designer
   'It can be modified using the Windows Form Designer.  
   'Do not modify it using the code editor.
   <System.Diagnostics.DebuggerStepThrough()> _
   Private Sub InitializeComponent()
      Me.components = New System.ComponentModel.Container()
      Me.ImageBox = New System.Windows.Forms.PictureBox()
      Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
      CType(Me.ImageBox, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SuspendLayout()
      '
      'ImageBox
      '
      Me.ImageBox.BackColor = System.Drawing.Color.White
      Me.ImageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
      Me.ImageBox.Dock = System.Windows.Forms.DockStyle.Fill
      Me.ImageBox.Location = New System.Drawing.Point(0, 0)
      Me.ImageBox.Name = "ImageBox"
      Me.ImageBox.Size = New System.Drawing.Size(282, 253)
      Me.ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
      Me.ImageBox.TabIndex = 0
      Me.ImageBox.TabStop = False
      Me.ToolTip.SetToolTip(Me.ImageBox, "Press F1 for help.")
      '
      'InterfaceWindow
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.AutoScroll = True
      Me.ClientSize = New System.Drawing.Size(282, 253)
      Me.Controls.Add(Me.ImageBox)
      Me.KeyPreview = True
      Me.Name = "InterfaceWindow"
      Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
      CType(Me.ImageBox, System.ComponentModel.ISupportInitialize).EndInit()
      Me.ResumeLayout(False)
      Me.PerformLayout()

   End Sub
   Friend WithEvents ImageBox As System.Windows.Forms.PictureBox
   Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
End Class
