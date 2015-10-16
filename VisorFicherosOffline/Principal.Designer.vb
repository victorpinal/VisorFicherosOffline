<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Principal
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
        Me.uxtxtSearch = New System.Windows.Forms.TextBox()
        Me.uxTree = New System.Windows.Forms.TreeView()
        Me.uxcmbDeviceNames = New System.Windows.Forms.ComboBox()
        Me.uxbtnReloadFolder = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'uxtxtSearch
        '
        Me.uxtxtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.uxtxtSearch.Location = New System.Drawing.Point(304, 12)
        Me.uxtxtSearch.Name = "uxtxtSearch"
        Me.uxtxtSearch.Size = New System.Drawing.Size(560, 20)
        Me.uxtxtSearch.TabIndex = 2
        '
        'uxTree
        '
        Me.uxTree.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.uxTree.HotTracking = True
        Me.uxTree.Location = New System.Drawing.Point(12, 38)
        Me.uxTree.Name = "uxTree"
        Me.uxTree.Size = New System.Drawing.Size(852, 576)
        Me.uxTree.TabIndex = 3
        '
        'uxcmbDeviceNames
        '
        Me.uxcmbDeviceNames.FormattingEnabled = True
        Me.uxcmbDeviceNames.Location = New System.Drawing.Point(12, 12)
        Me.uxcmbDeviceNames.Name = "uxcmbDeviceNames"
        Me.uxcmbDeviceNames.Size = New System.Drawing.Size(253, 21)
        Me.uxcmbDeviceNames.TabIndex = 0
        '
        'uxbtnReloadFolder
        '
        Me.uxbtnReloadFolder.AutoSize = True
        Me.uxbtnReloadFolder.Image = Global.VisorFicherosOffline.My.Resources.Resources.carpeta
        Me.uxbtnReloadFolder.Location = New System.Drawing.Point(271, 10)
        Me.uxbtnReloadFolder.Name = "uxbtnReloadFolder"
        Me.uxbtnReloadFolder.Size = New System.Drawing.Size(27, 23)
        Me.uxbtnReloadFolder.TabIndex = 1
        Me.uxbtnReloadFolder.UseVisualStyleBackColor = True
        '
        'Principal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(876, 626)
        Me.Controls.Add(Me.uxcmbDeviceNames)
        Me.Controls.Add(Me.uxbtnReloadFolder)
        Me.Controls.Add(Me.uxtxtSearch)
        Me.Controls.Add(Me.uxTree)
        Me.Name = "Principal"
        Me.Text = "Visor de ficheros offline"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents uxtxtSearch As TextBox
    Friend WithEvents uxTree As TreeView
    Friend WithEvents uxcmbDeviceNames As ComboBox
    Friend WithEvents uxbtnReloadFolder As Button
End Class
