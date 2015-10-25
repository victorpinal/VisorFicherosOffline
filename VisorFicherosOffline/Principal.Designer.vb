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
        Me.uxSplitExplorer = New System.Windows.Forms.SplitContainer()
        Me.uxpnlDevices = New System.Windows.Forms.Panel()
        Me.uxbtnLoadFolder = New System.Windows.Forms.Button()
        Me.uxlstFiles = New System.Windows.Forms.ListView()
        Me.uxColumnName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.uxColumnSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.uxColumnDuration = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.uxColumnVideo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.uxColumnVideoCodec = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.uxColumnAudio = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.uxColumnDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.uxStatusBar = New System.Windows.Forms.StatusStrip()
        Me.uxProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.uxStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.uxStatusFiles = New System.Windows.Forms.ToolStripStatusLabel()
        Me.uxBackground = New System.ComponentModel.BackgroundWorker()
        Me.uxPanelFiltros = New System.Windows.Forms.Panel()
        Me.uxbntSearch = New System.Windows.Forms.Button()
        Me.uxlblFilterbyExtension = New System.Windows.Forms.Label()
        Me.uxcmbExtensions = New System.Windows.Forms.ComboBox()
        Me.uxchkVideo = New System.Windows.Forms.CheckBox()
        Me.uxchkFilesOnly = New System.Windows.Forms.CheckBox()
        CType(Me.uxSplitExplorer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.uxSplitExplorer.Panel1.SuspendLayout()
        Me.uxSplitExplorer.Panel2.SuspendLayout()
        Me.uxSplitExplorer.SuspendLayout()
        Me.uxpnlDevices.SuspendLayout()
        Me.uxStatusBar.SuspendLayout()
        Me.uxPanelFiltros.SuspendLayout()
        Me.SuspendLayout()
        '
        'uxtxtSearch
        '
        Me.uxtxtSearch.Location = New System.Drawing.Point(0, 0)
        Me.uxtxtSearch.Multiline = True
        Me.uxtxtSearch.Name = "uxtxtSearch"
        Me.uxtxtSearch.Size = New System.Drawing.Size(185, 23)
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
        Me.uxTreeFolder.Location = New System.Drawing.Point(0, 25)
        Me.uxTreeFolder.Name = "uxTreeFolder"
        Me.uxTreeFolder.SelectedImageIndex = 0
        Me.uxTreeFolder.ShowNodeToolTips = True
        Me.uxTreeFolder.Size = New System.Drawing.Size(257, 645)
        Me.uxTreeFolder.TabIndex = 0
        '
        'uxImageList
        '
        Me.uxImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.uxImageList.ImageSize = New System.Drawing.Size(16, 16)
        Me.uxImageList.TransparentColor = System.Drawing.Color.Transparent
        '
        'uxcmbDeviceNames
        '
        Me.uxcmbDeviceNames.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.uxcmbDeviceNames.FormattingEnabled = True
        Me.uxcmbDeviceNames.Location = New System.Drawing.Point(0, 0)
        Me.uxcmbDeviceNames.Name = "uxcmbDeviceNames"
        Me.uxcmbDeviceNames.Size = New System.Drawing.Size(230, 21)
        Me.uxcmbDeviceNames.TabIndex = 0
        '
        'uxSplitExplorer
        '
        Me.uxSplitExplorer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxSplitExplorer.Location = New System.Drawing.Point(5, 32)
        Me.uxSplitExplorer.Name = "uxSplitExplorer"
        '
        'uxSplitExplorer.Panel1
        '
        Me.uxSplitExplorer.Panel1.Controls.Add(Me.uxTreeFolder)
        Me.uxSplitExplorer.Panel1.Controls.Add(Me.uxpnlDevices)
        '
        'uxSplitExplorer.Panel2
        '
        Me.uxSplitExplorer.Panel2.Controls.Add(Me.uxlstFiles)
        Me.uxSplitExplorer.Size = New System.Drawing.Size(998, 670)
        Me.uxSplitExplorer.SplitterDistance = 257
        Me.uxSplitExplorer.TabIndex = 4
        '
        'uxpnlDevices
        '
        Me.uxpnlDevices.Controls.Add(Me.uxcmbDeviceNames)
        Me.uxpnlDevices.Controls.Add(Me.uxbtnLoadFolder)
        Me.uxpnlDevices.Dock = System.Windows.Forms.DockStyle.Top
        Me.uxpnlDevices.Location = New System.Drawing.Point(0, 0)
        Me.uxpnlDevices.Name = "uxpnlDevices"
        Me.uxpnlDevices.Size = New System.Drawing.Size(257, 25)
        Me.uxpnlDevices.TabIndex = 5
        '
        'uxbtnLoadFolder
        '
        Me.uxbtnLoadFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.uxbtnLoadFolder.AutoSize = True
        Me.uxbtnLoadFolder.Image = Global.VisorFicherosOffline.My.Resources.Resources.folder
        Me.uxbtnLoadFolder.Location = New System.Drawing.Point(233, -1)
        Me.uxbtnLoadFolder.Name = "uxbtnLoadFolder"
        Me.uxbtnLoadFolder.Size = New System.Drawing.Size(24, 23)
        Me.uxbtnLoadFolder.TabIndex = 1
        Me.uxbtnLoadFolder.UseVisualStyleBackColor = True
        '
        'uxlstFiles
        '
        Me.uxlstFiles.AllowColumnReorder = True
        Me.uxlstFiles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.uxColumnName, Me.uxColumnSize, Me.uxColumnDuration, Me.uxColumnVideo, Me.uxColumnVideoCodec, Me.uxColumnAudio, Me.uxColumnDate})
        Me.uxlstFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxlstFiles.HideSelection = False
        Me.uxlstFiles.Location = New System.Drawing.Point(0, 0)
        Me.uxlstFiles.MultiSelect = False
        Me.uxlstFiles.Name = "uxlstFiles"
        Me.uxlstFiles.ShowItemToolTips = True
        Me.uxlstFiles.Size = New System.Drawing.Size(737, 670)
        Me.uxlstFiles.SmallImageList = Me.uxImageList
        Me.uxlstFiles.TabIndex = 0
        Me.uxlstFiles.UseCompatibleStateImageBehavior = False
        Me.uxlstFiles.View = System.Windows.Forms.View.Details
        '
        'uxColumnName
        '
        Me.uxColumnName.Text = "Name"
        Me.uxColumnName.Width = 370
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
        Me.uxColumnDate.Width = 80
        '
        'uxStatusBar
        '
        Me.uxStatusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.uxProgressBar, Me.uxStatusLabel, Me.uxStatusFiles})
        Me.uxStatusBar.Location = New System.Drawing.Point(5, 702)
        Me.uxStatusBar.Name = "uxStatusBar"
        Me.uxStatusBar.Size = New System.Drawing.Size(998, 22)
        Me.uxStatusBar.TabIndex = 3
        Me.uxStatusBar.Text = "StatusStrip1"
        '
        'uxProgressBar
        '
        Me.uxProgressBar.Name = "uxProgressBar"
        Me.uxProgressBar.Size = New System.Drawing.Size(120, 16)
        Me.uxProgressBar.Step = 1
        Me.uxProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.uxProgressBar.Visible = False
        '
        'uxStatusLabel
        '
        Me.uxStatusLabel.Name = "uxStatusLabel"
        Me.uxStatusLabel.Size = New System.Drawing.Size(983, 17)
        Me.uxStatusLabel.Spring = True
        Me.uxStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'uxStatusFiles
        '
        Me.uxStatusFiles.Name = "uxStatusFiles"
        Me.uxStatusFiles.Size = New System.Drawing.Size(0, 17)
        '
        'uxBackground
        '
        Me.uxBackground.WorkerReportsProgress = True
        Me.uxBackground.WorkerSupportsCancellation = True
        '
        'uxPanelFiltros
        '
        Me.uxPanelFiltros.Controls.Add(Me.uxtxtSearch)
        Me.uxPanelFiltros.Controls.Add(Me.uxbntSearch)
        Me.uxPanelFiltros.Controls.Add(Me.uxlblFilterbyExtension)
        Me.uxPanelFiltros.Controls.Add(Me.uxcmbExtensions)
        Me.uxPanelFiltros.Controls.Add(Me.uxchkVideo)
        Me.uxPanelFiltros.Controls.Add(Me.uxchkFilesOnly)
        Me.uxPanelFiltros.Dock = System.Windows.Forms.DockStyle.Top
        Me.uxPanelFiltros.Location = New System.Drawing.Point(5, 5)
        Me.uxPanelFiltros.Name = "uxPanelFiltros"
        Me.uxPanelFiltros.Size = New System.Drawing.Size(998, 27)
        Me.uxPanelFiltros.TabIndex = 6
        '
        'uxbntSearch
        '
        Me.uxbntSearch.AutoSize = True
        Me.uxbntSearch.Image = Global.VisorFicherosOffline.My.Resources.Resources.search
        Me.uxbntSearch.Location = New System.Drawing.Point(191, 0)
        Me.uxbntSearch.Name = "uxbntSearch"
        Me.uxbntSearch.Size = New System.Drawing.Size(67, 23)
        Me.uxbntSearch.TabIndex = 3
        Me.uxbntSearch.Text = "Search"
        Me.uxbntSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.uxbntSearch.UseVisualStyleBackColor = True
        '
        'uxlblFilterbyExtension
        '
        Me.uxlblFilterbyExtension.AutoSize = True
        Me.uxlblFilterbyExtension.Location = New System.Drawing.Point(264, 6)
        Me.uxlblFilterbyExtension.Name = "uxlblFilterbyExtension"
        Me.uxlblFilterbyExtension.Size = New System.Drawing.Size(61, 13)
        Me.uxlblFilterbyExtension.TabIndex = 5
        Me.uxlblFilterbyExtension.Text = "Filter by Ext"
        '
        'uxcmbExtensions
        '
        Me.uxcmbExtensions.FormattingEnabled = True
        Me.uxcmbExtensions.Location = New System.Drawing.Point(327, 2)
        Me.uxcmbExtensions.Name = "uxcmbExtensions"
        Me.uxcmbExtensions.Size = New System.Drawing.Size(63, 21)
        Me.uxcmbExtensions.TabIndex = 4
        '
        'uxchkVideo
        '
        Me.uxchkVideo.AutoSize = True
        Me.uxchkVideo.Location = New System.Drawing.Point(401, 5)
        Me.uxchkVideo.Name = "uxchkVideo"
        Me.uxchkVideo.Size = New System.Drawing.Size(53, 17)
        Me.uxchkVideo.TabIndex = 6
        Me.uxchkVideo.Text = "Video"
        Me.uxchkVideo.UseVisualStyleBackColor = True
        '
        'uxchkFilesOnly
        '
        Me.uxchkFilesOnly.AutoSize = True
        Me.uxchkFilesOnly.Location = New System.Drawing.Point(455, 5)
        Me.uxchkFilesOnly.Name = "uxchkFilesOnly"
        Me.uxchkFilesOnly.Size = New System.Drawing.Size(69, 17)
        Me.uxchkFilesOnly.TabIndex = 7
        Me.uxchkFilesOnly.Text = "Files only"
        Me.uxchkFilesOnly.UseVisualStyleBackColor = True
        '
        'Principal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.uxSplitExplorer)
        Me.Controls.Add(Me.uxPanelFiltros)
        Me.Controls.Add(Me.uxStatusBar)
        Me.Name = "Principal"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.Text = "Visor de ficheros offline"
        Me.uxSplitExplorer.Panel1.ResumeLayout(False)
        Me.uxSplitExplorer.Panel2.ResumeLayout(False)
        CType(Me.uxSplitExplorer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.uxSplitExplorer.ResumeLayout(False)
        Me.uxpnlDevices.ResumeLayout(False)
        Me.uxpnlDevices.PerformLayout()
        Me.uxStatusBar.ResumeLayout(False)
        Me.uxStatusBar.PerformLayout()
        Me.uxPanelFiltros.ResumeLayout(False)
        Me.uxPanelFiltros.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents uxtxtSearch As TextBox
    Friend WithEvents uxTreeFolder As TreeView
    Friend WithEvents uxcmbDeviceNames As ComboBox
    Friend WithEvents uxbtnLoadFolder As Button
    Friend WithEvents uxSplitExplorer As SplitContainer
    Friend WithEvents uxlstFiles As ListView
    Friend WithEvents uxColumnName As ColumnHeader
    Friend WithEvents uxColumnSize As ColumnHeader
    Friend WithEvents uxColumnDate As ColumnHeader
    Friend WithEvents uxColumnDuration As ColumnHeader
    Friend WithEvents uxColumnVideo As ColumnHeader
    Friend WithEvents uxColumnAudio As ColumnHeader
    Friend WithEvents uxStatusBar As StatusStrip
    Friend WithEvents uxProgressBar As ToolStripProgressBar
    Friend WithEvents uxStatusLabel As ToolStripStatusLabel
    Friend WithEvents uxColumnVideoCodec As ColumnHeader
    Friend WithEvents uxImageList As ImageList
    Friend WithEvents uxBackground As System.ComponentModel.BackgroundWorker
    Friend WithEvents uxStatusFiles As ToolStripStatusLabel
    Friend WithEvents uxpnlDevices As Panel
    Friend WithEvents uxPanelFiltros As Panel
    Friend WithEvents uxbntSearch As Button
    Friend WithEvents uxcmbExtensions As ComboBox
    Friend WithEvents uxlblFilterbyExtension As Label
    Friend WithEvents uxchkFilesOnly As CheckBox
    Friend WithEvents uxchkVideo As CheckBox
End Class
