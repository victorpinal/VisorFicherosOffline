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
        Me.uxTreeFolder = New System.Windows.Forms.TreeView()
        Me.uxcmbDeviceNames = New System.Windows.Forms.ComboBox()
        Me.uxbtnReloadFolder = New System.Windows.Forms.Button()
        Me.uxSplit = New System.Windows.Forms.SplitContainer()
        Me.uxlstDetail = New System.Windows.Forms.ListView()
        Me.uxColumnName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.uxColumnSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.uxColumnDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        CType(Me.uxSplit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.uxSplit.Panel1.SuspendLayout()
        Me.uxSplit.Panel2.SuspendLayout()
        Me.uxSplit.SuspendLayout()
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
        'uxTreeFolder
        '
        Me.uxTreeFolder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxTreeFolder.HotTracking = True
        Me.uxTreeFolder.Location = New System.Drawing.Point(0, 0)
        Me.uxTreeFolder.Name = "uxTreeFolder"
        Me.uxTreeFolder.Size = New System.Drawing.Size(284, 575)
        Me.uxTreeFolder.TabIndex = 3
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
        'uxSplit
        '
        Me.uxSplit.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.uxSplit.Location = New System.Drawing.Point(12, 39)
        Me.uxSplit.Name = "uxSplit"
        '
        'uxSplit.Panel1
        '
        Me.uxSplit.Panel1.Controls.Add(Me.uxTreeFolder)
        '
        'uxSplit.Panel2
        '
        Me.uxSplit.Panel2.Controls.Add(Me.uxlstDetail)
        Me.uxSplit.Size = New System.Drawing.Size(852, 575)
        Me.uxSplit.SplitterDistance = 284
        Me.uxSplit.TabIndex = 4
        '
        'uxlstDetail
        '
        Me.uxlstDetail.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.uxColumnName, Me.uxColumnSize, Me.uxColumnDate})
        Me.uxlstDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxlstDetail.Location = New System.Drawing.Point(0, 0)
        Me.uxlstDetail.Name = "uxlstDetail"
        Me.uxlstDetail.Size = New System.Drawing.Size(564, 575)
        Me.uxlstDetail.TabIndex = 0
        Me.uxlstDetail.UseCompatibleStateImageBehavior = False
        Me.uxlstDetail.View = System.Windows.Forms.View.Details
        '
        'uxColumnName
        '
        Me.uxColumnName.Text = "Name"
        Me.uxColumnName.Width = 400
        '
        'uxColumnSize
        '
        Me.uxColumnSize.Text = "Size"
        Me.uxColumnSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'uxColumnDate
        '
        Me.uxColumnDate.Text = "Date"
        '
        'Principal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(876, 626)
        Me.Controls.Add(Me.uxcmbDeviceNames)
        Me.Controls.Add(Me.uxbtnReloadFolder)
        Me.Controls.Add(Me.uxtxtSearch)
        Me.Controls.Add(Me.uxSplit)
        Me.Name = "Principal"
        Me.Text = "Visor de ficheros offline"
        Me.uxSplit.Panel1.ResumeLayout(False)
        Me.uxSplit.Panel2.ResumeLayout(False)
        CType(Me.uxSplit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.uxSplit.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents uxtxtSearch As TextBox
    Friend WithEvents uxTreeFolder As TreeView
    Friend WithEvents uxcmbDeviceNames As ComboBox
    Friend WithEvents uxbtnReloadFolder As Button
    Friend WithEvents uxSplit As SplitContainer
    Friend WithEvents uxlstDetail As ListView
    Friend WithEvents uxColumnName As ColumnHeader
    Friend WithEvents uxColumnSize As ColumnHeader
    Friend WithEvents uxColumnDate As ColumnHeader
End Class
