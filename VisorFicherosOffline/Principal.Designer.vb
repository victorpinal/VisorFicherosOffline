<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Principal
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
        Me.uxtxtSearch = New System.Windows.Forms.TextBox()
        Me.uxTreeFolder = New System.Windows.Forms.TreeView()
        Me.uxImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.uxcmbDeviceNames = New System.Windows.Forms.ComboBox()
        Me.uxSplit = New System.Windows.Forms.SplitContainer()
        Me.uxlstDetail = New System.Windows.Forms.ListView()
        Me.uxColumnName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.uxColumnSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.uxColumnDuration = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.uxColumnVideo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.uxColumnVideoCodec = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.uxColumnAudio = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.uxColumnDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.uxState = New System.Windows.Forms.StatusStrip()
        Me.uxProgress = New System.Windows.Forms.ToolStripProgressBar()
        Me.uxProgressLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.uxBackground = New System.ComponentModel.BackgroundWorker()
        Me.uxbtnReloadFolder = New System.Windows.Forms.Button()
        CType(Me.uxSplit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.uxSplit.Panel1.SuspendLayout()
        Me.uxSplit.Panel2.SuspendLayout()
        Me.uxSplit.SuspendLayout()
        Me.uxState.SuspendLayout()
        Me.SuspendLayout()
        '
        'uxtxtSearch
        '
        Me.uxtxtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.uxtxtSearch.Location = New System.Drawing.Point(300, 12)
        Me.uxtxtSearch.Name = "uxtxtSearch"
        Me.uxtxtSearch.Size = New System.Drawing.Size(695, 20)
        Me.uxtxtSearch.TabIndex = 2
        '
        'uxTreeFolder
        '
        Me.uxTreeFolder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxTreeFolder.FullRowSelect = True
        Me.uxTreeFolder.HideSelection = False
        Me.uxTreeFolder.HotTracking = True
        Me.uxTreeFolder.ImageIndex = 0
        Me.uxTreeFolder.ImageList = Me.uxImageList
        Me.uxTreeFolder.Location = New System.Drawing.Point(0, 0)
        Me.uxTreeFolder.Name = "uxTreeFolder"
        Me.uxTreeFolder.SelectedImageIndex = 0
        Me.uxTreeFolder.ShowNodeToolTips = True
        Me.uxTreeFolder.Size = New System.Drawing.Size(327, 562)
        Me.uxTreeFolder.TabIndex = 3
        '
        'uxImageList
        '
        Me.uxImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.uxImageList.ImageSize = New System.Drawing.Size(16, 16)
        Me.uxImageList.TransparentColor = System.Drawing.Color.Transparent
        '
        'uxcmbDeviceNames
        '
        Me.uxcmbDeviceNames.FormattingEnabled = True
        Me.uxcmbDeviceNames.Location = New System.Drawing.Point(12, 12)
        Me.uxcmbDeviceNames.Name = "uxcmbDeviceNames"
        Me.uxcmbDeviceNames.Size = New System.Drawing.Size(253, 21)
        Me.uxcmbDeviceNames.TabIndex = 0
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
        Me.uxSplit.Size = New System.Drawing.Size(983, 562)
        Me.uxSplit.SplitterDistance = 327
        Me.uxSplit.TabIndex = 4
        '
        'uxlstDetail
        '
        Me.uxlstDetail.AllowColumnReorder = True
        Me.uxlstDetail.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.uxColumnName, Me.uxColumnSize, Me.uxColumnDuration, Me.uxColumnVideo, Me.uxColumnVideoCodec, Me.uxColumnAudio, Me.uxColumnDate})
        Me.uxlstDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxlstDetail.HideSelection = False
        Me.uxlstDetail.Location = New System.Drawing.Point(0, 0)
        Me.uxlstDetail.MultiSelect = False
        Me.uxlstDetail.Name = "uxlstDetail"
        Me.uxlstDetail.Size = New System.Drawing.Size(652, 562)
        Me.uxlstDetail.SmallImageList = Me.uxImageList
        Me.uxlstDetail.TabIndex = 0
        Me.uxlstDetail.UseCompatibleStateImageBehavior = False
        Me.uxlstDetail.View = System.Windows.Forms.View.Details
        '
        'uxColumnName
        '
        Me.uxColumnName.Text = "Name"
        Me.uxColumnName.Width = 307
        '
        'uxColumnSize
        '
        Me.uxColumnSize.Text = "Size"
        Me.uxColumnSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'uxColumnDuration
        '
        Me.uxColumnDuration.Text = "Duration"
        Me.uxColumnDuration.Width = 55
        '
        'uxColumnVideo
        '
        Me.uxColumnVideo.Text = "Video"
        Me.uxColumnVideo.Width = 55
        '
        'uxColumnVideoCodec
        '
        Me.uxColumnVideoCodec.Text = "Codec"
        '
        'uxColumnAudio
        '
        Me.uxColumnAudio.Text = "Audio"
        Me.uxColumnAudio.Width = 40
        '
        'uxColumnDate
        '
        Me.uxColumnDate.Text = "Date"
        '
        'uxState
        '
        Me.uxState.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.uxProgress, Me.uxProgressLabel})
        Me.uxState.Location = New System.Drawing.Point(0, 604)
        Me.uxState.Name = "uxState"
        Me.uxState.Size = New System.Drawing.Size(1007, 22)
        Me.uxState.TabIndex = 5
        Me.uxState.Text = "StatusStrip1"
        '
        'uxProgress
        '
        Me.uxProgress.Name = "uxProgress"
        Me.uxProgress.Size = New System.Drawing.Size(200, 16)
        Me.uxProgress.Step = 1
        Me.uxProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'uxProgressLabel
        '
        Me.uxProgressLabel.AutoSize = False
        Me.uxProgressLabel.Name = "uxProgressLabel"
        Me.uxProgressLabel.Size = New System.Drawing.Size(790, 17)
        Me.uxProgressLabel.Spring = True
        Me.uxProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'uxBackground
        '
        Me.uxBackground.WorkerReportsProgress = True
        Me.uxBackground.WorkerSupportsCancellation = True
        '
        'uxbtnReloadFolder
        '
        Me.uxbtnReloadFolder.AutoSize = True
        Me.uxbtnReloadFolder.Image = Global.VisorFicherosOffline.My.Resources.Resources.folder
        Me.uxbtnReloadFolder.Location = New System.Drawing.Point(271, 11)
        Me.uxbtnReloadFolder.Name = "uxbtnReloadFolder"
        Me.uxbtnReloadFolder.Size = New System.Drawing.Size(24, 23)
        Me.uxbtnReloadFolder.TabIndex = 1
        Me.uxbtnReloadFolder.UseVisualStyleBackColor = True
        '
        'Principal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1007, 626)
        Me.Controls.Add(Me.uxbtnReloadFolder)
        Me.Controls.Add(Me.uxcmbDeviceNames)
        Me.Controls.Add(Me.uxtxtSearch)
        Me.Controls.Add(Me.uxSplit)
        Me.Controls.Add(Me.uxState)
        Me.Name = "Principal"
        Me.Text = "Visor de ficheros offline"
        Me.uxSplit.Panel1.ResumeLayout(False)
        Me.uxSplit.Panel2.ResumeLayout(False)
        CType(Me.uxSplit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.uxSplit.ResumeLayout(False)
        Me.uxState.ResumeLayout(False)
        Me.uxState.PerformLayout()
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
    Friend WithEvents uxColumnDuration As ColumnHeader
    Friend WithEvents uxColumnVideo As ColumnHeader
    Friend WithEvents uxColumnAudio As ColumnHeader
    Friend WithEvents uxState As StatusStrip
    Friend WithEvents uxProgress As ToolStripProgressBar
    Friend WithEvents uxProgressLabel As ToolStripStatusLabel
    Friend WithEvents uxColumnVideoCodec As ColumnHeader
    Friend WithEvents uxImageList As ImageList
    Friend WithEvents uxBackground As System.ComponentModel.BackgroundWorker
End Class
