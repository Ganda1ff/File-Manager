namespace File_Manager
{
    partial class TextFileForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextFileForm));
            menuStrip1 = new MenuStrip();
            saveToolStripMenuItem = new ToolStripMenuItem();
            findToolStripMenuItem = new ToolStripMenuItem();
            labelCounter = new Label();
            labelFileName = new Label();
            richTextBoxFile = new RichTextBox();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { saveToolStripMenuItem, findToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new Size(54, 24);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // findToolStripMenuItem
            // 
            findToolStripMenuItem.Name = "findToolStripMenuItem";
            findToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.F;
            findToolStripMenuItem.Size = new Size(51, 24);
            findToolStripMenuItem.Text = "Find";
            findToolStripMenuItem.Click += findToolStripMenuItem_Click;
            // 
            // labelCounter
            // 
            labelCounter.Dock = DockStyle.Bottom;
            labelCounter.Location = new Point(0, 430);
            labelCounter.Name = "labelCounter";
            labelCounter.Size = new Size(800, 20);
            labelCounter.TabIndex = 2;
            // 
            // labelFileName
            // 
            labelFileName.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            labelFileName.Location = new Point(262, 430);
            labelFileName.Name = "labelFileName";
            labelFileName.Size = new Size(538, 25);
            labelFileName.TabIndex = 3;
            // 
            // richTextBoxFile
            // 
            richTextBoxFile.Dock = DockStyle.Fill;
            richTextBoxFile.Location = new Point(0, 28);
            richTextBoxFile.Name = "richTextBoxFile";
            richTextBoxFile.Size = new Size(800, 402);
            richTextBoxFile.TabIndex = 4;
            richTextBoxFile.Text = "";
            richTextBoxFile.TextChanged += richTextBoxFile_TextChanged;
            // 
            // TextFileForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(richTextBoxFile);
            Controls.Add(labelFileName);
            Controls.Add(labelCounter);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "TextFileForm";
            Text = "tEditor";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem saveToolStripMenuItem;
        private Label labelCounter;
        private Label labelFileName;
        private ToolStripMenuItem findToolStripMenuItem;
        private RichTextBox richTextBoxFile;
    }
}