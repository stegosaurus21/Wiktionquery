<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainFrame
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainFrame))
        Me.animateShow = New System.Windows.Forms.Timer(Me.components)
        Me.animateHide = New System.Windows.Forms.Timer(Me.components)
        Me.holdTimer = New System.Windows.Forms.Timer(Me.components)
        Me.interLoad = New System.Windows.Forms.Timer(Me.components)
        Me.b_forward = New System.Windows.Forms.Button()
        Me.auxBrowser = New System.Windows.Forms.WebBrowser()
        Me.b_back = New System.Windows.Forms.Button()
        Me.display = New System.Windows.Forms.TextBox()
        Me.buttonCheck = New System.Windows.Forms.Timer(Me.components)
        Me.nicon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.cms = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Version101ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ghost = New System.Windows.Forms.Label()
        Me.moveTimer = New System.Windows.Forms.Timer(Me.components)
        Me.cms.SuspendLayout()
        Me.SuspendLayout()
        '
        'animateShow
        '
        Me.animateShow.Interval = 20
        '
        'animateHide
        '
        Me.animateHide.Interval = 20
        '
        'holdTimer
        '
        Me.holdTimer.Interval = 400
        '
        'interLoad
        '
        Me.interLoad.Enabled = True
        Me.interLoad.Interval = 5
        '
        'b_forward
        '
        Me.b_forward.Enabled = False
        Me.b_forward.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.b_forward.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.b_forward.Location = New System.Drawing.Point(26, 1)
        Me.b_forward.Name = "b_forward"
        Me.b_forward.Size = New System.Drawing.Size(26, 23)
        Me.b_forward.TabIndex = 2
        Me.b_forward.Text = ">"
        Me.b_forward.UseVisualStyleBackColor = True
        '
        'auxBrowser
        '
        Me.auxBrowser.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.auxBrowser.Location = New System.Drawing.Point(0, 25)
        Me.auxBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.auxBrowser.Name = "auxBrowser"
        Me.auxBrowser.Size = New System.Drawing.Size(400, 175)
        Me.auxBrowser.TabIndex = 0
        '
        'b_back
        '
        Me.b_back.Enabled = False
        Me.b_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.b_back.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.b_back.Location = New System.Drawing.Point(1, 1)
        Me.b_back.Name = "b_back"
        Me.b_back.Size = New System.Drawing.Size(26, 23)
        Me.b_back.TabIndex = 2
        Me.b_back.Text = "<"
        Me.b_back.UseVisualStyleBackColor = True
        '
        'display
        '
        Me.display.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.display.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.display.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.display.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.display.Location = New System.Drawing.Point(56, 2)
        Me.display.Name = "display"
        Me.display.Size = New System.Drawing.Size(340, 22)
        Me.display.TabIndex = 3
        '
        'buttonCheck
        '
        Me.buttonCheck.Enabled = True
        '
        'nicon
        '
        Me.nicon.ContextMenuStrip = Me.cms
        Me.nicon.Icon = CType(resources.GetObject("nicon.Icon"), System.Drawing.Icon)
        Me.nicon.Text = "Wiktionquery"
        Me.nicon.Visible = True
        '
        'cms
        '
        Me.cms.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToolStripMenuItem, Me.Version101ToolStripMenuItem})
        Me.cms.Name = "cms"
        Me.cms.Size = New System.Drawing.Size(181, 70)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'Version101ToolStripMenuItem
        '
        Me.Version101ToolStripMenuItem.Enabled = False
        Me.Version101ToolStripMenuItem.Name = "Version101ToolStripMenuItem"
        Me.Version101ToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.Version101ToolStripMenuItem.Text = "Version 1.0.4"
        '
        'ghost
        '
        Me.ghost.AutoSize = True
        Me.ghost.Location = New System.Drawing.Point(311, 76)
        Me.ghost.Name = "ghost"
        Me.ghost.Size = New System.Drawing.Size(0, 13)
        Me.ghost.TabIndex = 4
        '
        'moveTimer
        '
        Me.moveTimer.Interval = 20
        '
        'MainFrame
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(400, 200)
        Me.Controls.Add(Me.display)
        Me.Controls.Add(Me.b_forward)
        Me.Controls.Add(Me.b_back)
        Me.Controls.Add(Me.auxBrowser)
        Me.Controls.Add(Me.ghost)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "MainFrame"
        Me.Opacity = 0.8R
        Me.Text = "Wiktionquery"
        Me.cms.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents animateShow As Timer
    Friend WithEvents animateHide As Timer
    Friend WithEvents holdTimer As Timer
    Friend WithEvents interLoad As Timer
    Friend WithEvents b_forward As Button
    Friend WithEvents auxBrowser As WebBrowser
    Friend WithEvents b_back As Button
    Friend WithEvents display As TextBox
    Friend WithEvents buttonCheck As Timer
    Friend WithEvents nicon As NotifyIcon
    Friend WithEvents cms As ContextMenuStrip
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Version101ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ghost As Label
    Friend WithEvents moveTimer As Timer
End Class
