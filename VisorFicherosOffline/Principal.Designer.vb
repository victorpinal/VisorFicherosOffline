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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.uxpnlDevices = New System.Windows.Forms.Panel()
        Me.uxbtnLoadFolder = New System.Windows.Forms.Button()
        Me.uxpnlResumen = New System.Windows.Forms.Panel()
        Me.uxPbDevice = New System.Windows.Forms.ProgressBar()
        Me.uxlblDevice1 = New System.Windows.Forms.Label()
        Me.uxlblDevice2 = New System.Windows.Forms.Label()
        Me.uxlblDevice3 = New System.Windows.Forms.Label()
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
        Me.TableLayoutPanel1.SuspendLayout()
        Me.uxpnlDevices.SuspendLayout()
        Me.uxpnlResumen.SuspendLayout()
        Me.uxStatusBar.SuspendLayout()
        Me.uxPanelFiltros.SuspendLayout()
        Me.SuspendLayout()
        '
        'uxtxtSearch
        '
        Me.uxtxtSearch.Location = New System.Drawing.Point(0, 0)
        Me.uxtxtSearch.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.uxtxtSearch.Name = "uxtxtSearch"
        Me.uxtxtSearch.Size = New System.Drawing.Size(245, 22)
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
        Me.uxTreeFolder.Location = New System.Drawing.Point(4, 43)
        Me.uxTreeFolder.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.uxTreeFolder.Name = "uxTreeFolder"
        Me.uxTreeFolder.SelectedImageIndex = 0
        Me.uxTreeFolder.ShowNodeToolTips = True
        Me.uxTreeFolder.Size = New System.Drawing.Size(334, 701)
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
        Me.uxcmbDeviceNames.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.uxcmbDeviceNames.Name = "uxcmbDeviceNames"
        Me.uxcmbDeviceNames.Size = New System.Drawing.Size(296, 24)
        Me.uxcmbDeviceNames.TabIndex = 0
        '
        'uxSplitExplorer
        '
        Me.uxSplitExplorer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxSplitExplorer.Location = New System.Drawing.Point(7, 39)
        Me.uxSplitExplorer.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.uxSplitExplorer.Name = "uxSplitExplorer"
        '
        'uxSplitExplorer.Panel1
        '
        Me.uxSplitExplorer.Panel1.Controls.Add(Me.TableLayoutPanel1)
        '
        'uxSplitExplorer.Panel2
        '
        Me.uxSplitExplorer.Panel2.Controls.Add(Me.uxlstFiles)
        Me.uxSplitExplorer.Size = New System.Drawing.Size(1330, 830)
        Me.uxSplitExplorer.SplitterDistance = 342
        Me.uxSplitExplorer.SplitterWidth = 5
        Me.uxSplitExplorer.TabIndex = 4
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.uxpnlDevices, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.uxTreeFolder, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.uxpnlResumen, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(342, 830)
        Me.TableLayoutPanel1.TabIndex = 6
        '
        'uxpnlDevices
        '
        Me.uxpnlDevices.Controls.Add(Me.uxcmbDeviceNames)
        Me.uxpnlDevices.Controls.Add(Me.uxbtnLoadFolder)
        Me.uxpnlDevices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxpnlDevices.Location = New System.Drawing.Point(4, 4)
        Me.uxpnlDevices.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.uxpnlDevices.Name = "uxpnlDevices"
        Me.uxpnlDevices.Size = New System.Drawing.Size(334, 31)
        Me.uxpnlDevices.TabIndex = 5
        '
        'uxbtnLoadFolder
        '
        Me.uxbtnLoadFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.uxbtnLoadFolder.AutoSize = True
        Me.uxbtnLoadFolder.Image = Global.VisorFicherosOffline.My.Resources.Resources.folder
        Me.uxbtnLoadFolder.Location = New System.Drawing.Point(302, -1)
        Me.uxbtnLoadFolder.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.uxbtnLoadFolder.Name = "uxbtnLoadFolder"
        Me.uxbtnLoadFolder.Size = New System.Drawing.Size(32, 28)
        Me.uxbtnLoadFolder.TabIndex = 1
        Me.uxbtnLoadFolder.UseVisualStyleBackColor = True
        '
        'uxpnlResumen
        '
        Me.uxpnlResumen.Controls.Add(Me.uxPbDevice)
        Me.uxpnlResumen.Controls.Add(Me.uxlblDevice1)
        Me.uxpnlResumen.Controls.Add(Me.uxlblDevice2)
        Me.uxpnlResumen.Controls.Add(Me.uxlblDevice3)
        Me.uxpnlResumen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxpnlResumen.Location = New System.Drawing.Point(4, 752)
        Me.uxpnlResumen.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.uxpnlResumen.Name = "uxpnlResumen"
        Me.uxpnlResumen.Size = New System.Drawing.Size(334, 74)
        Me.uxpnlResumen.TabIndex = 6
        '
        'uxPbDevice
        '
        Me.uxPbDevice.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.uxPbDevice.Location = New System.Drawing.Point(4, 4)
        Me.uxPbDevice.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.uxPbDevice.MarqueeAnimationSpeed = 0
        Me.uxPbDevice.Name = "uxPbDevice"
        Me.uxPbDevice.Size = New System.Drawing.Size(247, 16)
        Me.uxPbDevice.Step = 1
        Me.uxPbDevice.TabIndex = 0
        '
        'uxlblDevice1
        '
        Me.uxlblDevice1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.uxlblDevice1.Location = New System.Drawing.Point(250, 4)
        Me.uxlblDevice1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.uxlblDevice1.Name = "uxlblDevice1"
        Me.uxlblDevice1.Size = New System.Drawing.Size(80, 16)
        Me.uxlblDevice1.TabIndex = 2
        Me.uxlblDevice1.Text = "Label1"
        Me.uxlblDevice1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'uxlblDevice2
        '
        Me.uxlblDevice2.AutoSize = True
        Me.uxlblDevice2.Location = New System.Drawing.Point(4, 23)
        Me.uxlblDevice2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.uxlblDevice2.Name = "uxlblDevice2"
        Me.uxlblDevice2.Size = New System.Drawing.Size(51, 17)
        Me.uxlblDevice2.TabIndex = 1
        Me.uxlblDevice2.Text = "Label1"
        '
        'uxlblDevice3
        '
        Me.uxlblDevice3.AutoSize = True
        Me.uxlblDevice3.Location = New System.Drawing.Point(4, 42)
        Me.uxlblDevice3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.uxlblDevice3.Name = "uxlblDevice3"
        Me.uxlblDevice3.Size = New System.Drawing.Size(51, 17)
        Me.uxlblDevice3.TabIndex = 3
        Me.uxlblDevice3.Text = "Label1"
        '
        'uxlstFiles
        '
        Me.uxlstFiles.AllowColumnReorder = True
        Me.uxlstFiles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.uxColumnName, Me.uxColumnSize, Me.uxColumnDuration, Me.uxColumnVideo, Me.uxColumnVideoCodec, Me.uxColumnAudio, Me.uxColumnDate})
        Me.uxlstFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.uxlstFiles.HideSelection = False
        Me.uxlstFiles.Location = New System.Drawing.Point(0, 0)
        Me.uxlstFiles.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.uxlstFiles.MultiSelect = False
        Me.uxlstFiles.Name = "uxlstFiles"
        Me.uxlstFiles.ShowItemToolTips = True
        Me.uxlstFiles.Size = New System.Drawing.Size(983, 830)
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
        Me.uxStatusBar.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.uxStatusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.uxProgressBar, Me.uxStatusLabel, Me.uxStatusFiles})
        Me.uxStatusBar.Location = New System.Drawing.Point(7, 869)
        Me.uxStatusBar.Name = "uxStatusBar"
        Me.uxStatusBar.Padding = New System.Windows.Forms.Padding(1, 0, 19, 0)
        Me.uxStatusBar.Size = New System.Drawing.Size(1330, 22)
        Me.uxStatusBar.TabIndex = 3
        Me.uxStatusBar.Text = "StatusStrip1"
        '
        'uxProgressBar
        '
        Me.uxProgressBar.Name = "uxProgressBar"
        Me.uxProgressBar.Size = New System.Drawing.Size(160, 17)
        Me.uxProgressBar.Step = 1
        Me.uxProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.uxProgressBar.Visible = False
        '
        'uxStatusLabel
        '
        Me.uxStatusLabel.Name = "uxStatusLabel"
        Me.uxStatusLabel.Size = New System.Drawing.Size(1310, 16)
        Me.uxStatusLabel.Spring = True
        Me.uxStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'uxStatusFiles
        '
        Me.uxStatusFiles.Name = "uxStatusFiles"
        Me.uxStatusFiles.Size = New System.Drawing.Size(0, 16)
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
        Me.uxPanelFiltros.Location = New System.Drawing.Point(7, 6)
        Me.uxPanelFiltros.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.uxPanelFiltros.Name = "uxPanelFiltros"
        Me.uxPanelFiltros.Size = New System.Drawing.Size(1330, 33)
        Me.uxPanelFiltros.TabIndex = 6
        '
        'uxbntSearch
        '
        Me.uxbntSearch.AutoSize = True
        Me.uxbntSearch.Image = Global.VisorFicherosOffline.My.Resources.Resources.search
        Me.uxbntSearch.Location = New System.Drawing.Point(255, 0)
        Me.uxbntSearch.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.uxbntSearch.Name = "uxbntSearch"
        Me.uxbntSearch.Size = New System.Drawing.Size(105, 33)
        Me.uxbntSearch.TabIndex = 3
        Me.uxbntSearch.Text = "Search"
        Me.uxbntSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.uxbntSearch.UseVisualStyleBackColor = True
        '
        'uxlblFilterbyExtension
        '
        Me.uxlblFilterbyExtension.AutoSize = True
        Me.uxlblFilterbyExtension.Location = New System.Drawing.Point(352, 7)
        Me.uxlblFilterbyExtension.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.uxlblFilterbyExtension.Name = "uxlblFilterbyExtension"
        Me.uxlblFilterbyExtension.Size = New System.Drawing.Size(81, 17)
        Me.uxlblFilterbyExtension.TabIndex = 5
        Me.uxlblFilterbyExtension.Text = "Filter by Ext"
        '
        'uxcmbExtensions
        '
        Me.uxcmbExtensions.FormattingEnabled = True
        Me.uxcmbExtensions.Location = New System.Drawing.Point(436, 2)
        Me.uxcmbExtensions.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.uxcmbExtensions.Name = "uxcmbExtensions"
        Me.uxcmbExtensions.Size = New System.Drawing.Size(83, 24)
        Me.uxcmbExtensions.TabIndex = 4
        '
        'uxchkVideo
        '
        Me.uxchkVideo.AutoSize = True
        Me.uxchkVideo.Location = New System.Drawing.Point(535, 6)
        Me.uxchkVideo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.uxchkVideo.Name = "uxchkVideo"
        Me.uxchkVideo.Size = New System.Drawing.Size(66, 21)
        Me.uxchkVideo.TabIndex = 6
        Me.uxchkVideo.Text = "Video"
        Me.uxchkVideo.UseVisualStyleBackColor = True
        '
        'uxchkFilesOnly
        '
        Me.uxchkFilesOnly.AutoSize = True
        Me.uxchkFilesOnly.Location = New System.Drawing.Point(607, 6)
        Me.uxchkFilesOnly.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.uxchkFilesOnly.Name = "uxchkFilesOnly"
        Me.uxchkFilesOnly.Size = New System.Drawing.Size(89, 21)
        Me.uxchkFilesOnly.TabIndex = 7
        Me.uxchkFilesOnly.Text = "Files only"
        Me.uxchkFilesOnly.UseVisualStyleBackColor = True
        '
        'Principal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1344, 897)
        Me.Controls.Add(Me.uxSplitExplorer)
        Me.Controls.Add(Me.uxPanelFiltros)
        Me.Controls.Add(Me.uxStatusBar)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "Principal"
        Me.Padding = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.Text = "Visor de ficheros offline"
        Me.uxSplitExplorer.Panel1.ResumeLayout(False)
        Me.uxSplitExplorer.Panel2.ResumeLayout(False)
        CType(Me.uxSplitExplorer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.uxSplitExplorer.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.uxpnlDevices.ResumeLayout(False)
        Me.uxpnlDevices.PerformLayout()
        Me.uxpnlResumen.ResumeLayout(False)
        Me.uxpnlResumen.PerformLayout()
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
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents uxpnlResumen As Panel
    Friend WithEvents uxPbDevice As ProgressBar
    Friend WithEvents uxlblDevice2 As Label
    Friend WithEvents uxlblDevice1 As Label
    Friend WithEvents uxlblDevice3 As Label
End Class
