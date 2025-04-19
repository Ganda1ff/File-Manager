namespace File_Manager
{
    partial class ManagerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagerForm));
            treeViewFolders = new TreeView();
            listViewFiles = new ListView();
            contextMenuStrip = new ContextMenuStrip(components);
            createFolderToolStripMenuItem1 = new ToolStripMenuItem();
            createFolderStripMenuItem = new ToolStripMenuItem();
            createTextToolStripMenuItem = new ToolStripMenuItem();
            deleteFolderToolStripMenuItem1 = new ToolStripMenuItem();
            copyFolderToolStripMenuItem1 = new ToolStripMenuItem();
            renameFolderToolStripMenuItem1 = new ToolStripMenuItem();
            propertiesToolStripMenuItem = new ToolStripMenuItem();
            textBoxPath = new TextBox();
            comboBoxThemes = new ComboBox();
            buttonPrev = new Button();
            buttonForward = new Button();
            contextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // treeViewFolders
            // 
            treeViewFolders.Dock = DockStyle.Left;
            treeViewFolders.Location = new Point(0, 0);
            treeViewFolders.Name = "treeViewFolders";
            treeViewFolders.Size = new Size(282, 450);
            treeViewFolders.TabIndex = 0;
            treeViewFolders.BeforeExpand += treeViewFolders_BeforeExpand;
            treeViewFolders.AfterSelect += treeViewFolders_AfterSelect;
            // 
            // listViewFiles
            // 
            listViewFiles.Anchor = AnchorStyles.Bottom;
            listViewFiles.ContextMenuStrip = contextMenuStrip;
            listViewFiles.Location = new Point(288, 51);
            listViewFiles.Margin = new Padding(30);
            listViewFiles.Name = "listViewFiles";
            listViewFiles.Size = new Size(512, 399);
            listViewFiles.TabIndex = 1;
            listViewFiles.UseCompatibleStateImageBehavior = false;
            listViewFiles.DoubleClick += listViewFiles_DoubleClick;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.ImageScalingSize = new Size(20, 20);
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { createFolderToolStripMenuItem1, deleteFolderToolStripMenuItem1, copyFolderToolStripMenuItem1, renameFolderToolStripMenuItem1, propertiesToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(155, 124);
            // 
            // createFolderToolStripMenuItem1
            // 
            createFolderToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { createFolderStripMenuItem, createTextToolStripMenuItem });
            createFolderToolStripMenuItem1.Name = "createFolderToolStripMenuItem1";
            createFolderToolStripMenuItem1.Size = new Size(154, 24);
            createFolderToolStripMenuItem1.Text = "New";
            // 
            // createFolderStripMenuItem
            // 
            createFolderStripMenuItem.Name = "createFolderStripMenuItem";
            createFolderStripMenuItem.Size = new Size(146, 26);
            createFolderStripMenuItem.Text = "Folder";
            createFolderStripMenuItem.Click += btnCreateFolder_Click;
            // 
            // createTextToolStripMenuItem
            // 
            createTextToolStripMenuItem.Name = "createTextToolStripMenuItem";
            createTextToolStripMenuItem.Size = new Size(146, 26);
            createTextToolStripMenuItem.Text = "Text File";
            createTextToolStripMenuItem.Click += btnCreateTextFile_Click;
            // 
            // deleteFolderToolStripMenuItem1
            // 
            deleteFolderToolStripMenuItem1.Name = "deleteFolderToolStripMenuItem1";
            deleteFolderToolStripMenuItem1.ShortcutKeys = Keys.Delete;
            deleteFolderToolStripMenuItem1.Size = new Size(154, 24);
            deleteFolderToolStripMenuItem1.Text = "Delete";
            deleteFolderToolStripMenuItem1.Click += btnDelete_Click;
            // 
            // copyFolderToolStripMenuItem1
            // 
            copyFolderToolStripMenuItem1.Name = "copyFolderToolStripMenuItem1";
            copyFolderToolStripMenuItem1.Size = new Size(154, 24);
            copyFolderToolStripMenuItem1.Text = "Copy";
            copyFolderToolStripMenuItem1.Click += btnCopy_Click;
            // 
            // renameFolderToolStripMenuItem1
            // 
            renameFolderToolStripMenuItem1.Name = "renameFolderToolStripMenuItem1";
            renameFolderToolStripMenuItem1.Size = new Size(154, 24);
            renameFolderToolStripMenuItem1.Text = "Rename";
            renameFolderToolStripMenuItem1.Click += btnRename_Click;
            // 
            // propertiesToolStripMenuItem
            // 
            propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            propertiesToolStripMenuItem.Size = new Size(154, 24);
            propertiesToolStripMenuItem.Text = "Properties";
            propertiesToolStripMenuItem.Click += btnProperties_Click;
            // 
            // textBoxPath
            // 
            textBoxPath.Location = new Point(380, 7);
            textBoxPath.Name = "textBoxPath";
            textBoxPath.Size = new Size(420, 27);
            textBoxPath.TabIndex = 2;
            textBoxPath.KeyDown += textBoxPath_KeyDown;
            // 
            // comboBoxThemes
            // 
            comboBoxThemes.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            comboBoxThemes.FormattingEnabled = true;
            comboBoxThemes.Location = new Point(637, 410);
            comboBoxThemes.Name = "comboBoxThemes";
            comboBoxThemes.Size = new Size(151, 28);
            comboBoxThemes.TabIndex = 3;
            // 
            // buttonPrev
            // 
            buttonPrev.BackgroundImage = (Image)resources.GetObject("buttonPrev.BackgroundImage");
            buttonPrev.BackgroundImageLayout = ImageLayout.Zoom;
            buttonPrev.Location = new Point(288, 0);
            buttonPrev.Name = "buttonPrev";
            buttonPrev.Size = new Size(41, 41);
            buttonPrev.TabIndex = 4;
            buttonPrev.UseVisualStyleBackColor = true;
            buttonPrev.Click += btnPrev_Click;
            // 
            // buttonForward
            // 
            buttonForward.BackgroundImage = (Image)resources.GetObject("buttonForward.BackgroundImage");
            buttonForward.BackgroundImageLayout = ImageLayout.Zoom;
            buttonForward.Location = new Point(333, 0);
            buttonForward.Name = "buttonForward";
            buttonForward.Size = new Size(41, 41);
            buttonForward.TabIndex = 5;
            buttonForward.UseVisualStyleBackColor = true;
            buttonForward.Click += btnForward_Click;
            // 
            // ManagerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonForward);
            Controls.Add(buttonPrev);
            Controls.Add(comboBoxThemes);
            Controls.Add(textBoxPath);
            Controls.Add(listViewFiles);
            Controls.Add(treeViewFolders);
            ForeColor = SystemColors.ActiveCaptionText;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ManagerForm";
            Text = "mFiles";
            contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private TreeView treeViewFolders;
        private ListView listViewFiles;
        private TextBox textBoxPath;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem createFolderToolStripMenuItem1;
        private ToolStripMenuItem deleteFolderToolStripMenuItem1;
        private ToolStripMenuItem copyFolderToolStripMenuItem1;
        private ToolStripMenuItem renameFolderToolStripMenuItem1;
        private ToolStripMenuItem propertiesToolStripMenuItem;
        private CheckBox checkBoxLight;
        private CheckBox checkBoxDark;
        private ComboBox comboBoxThemes;
        private Button buttonPrev;
        private Button buttonForward;
        private ToolStripMenuItem createFolderStripMenuItem;
        private ToolStripMenuItem createTextToolStripMenuItem;
    }
}
