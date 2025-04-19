namespace File_Manager
{
    partial class SearchForm
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
            btnPrev = new Button();
            btnNext = new Button();
            checkBoxMatchCase = new CheckBox();
            textBoxSearch = new TextBox();
            SuspendLayout();
            // 
            // btnPrev
            // 
            btnPrev.Location = new Point(12, 45);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(94, 29);
            btnPrev.TabIndex = 1;
            btnPrev.Text = "Back";
            btnPrev.UseVisualStyleBackColor = true;
            btnPrev.Click += btnPrev_Click;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(112, 45);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(94, 29);
            btnNext.TabIndex = 2;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // checkBoxMatchCase
            // 
            checkBoxMatchCase.AutoSize = true;
            checkBoxMatchCase.Location = new Point(222, 48);
            checkBoxMatchCase.Name = "checkBoxMatchCase";
            checkBoxMatchCase.Size = new Size(122, 24);
            checkBoxMatchCase.TabIndex = 3;
            checkBoxMatchCase.Text = "Case sensitive";
            checkBoxMatchCase.UseVisualStyleBackColor = true;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Location = new Point(12, 12);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(389, 27);
            textBoxSearch.TabIndex = 0;
            // 
            // SearchForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(413, 103);
            Controls.Add(checkBoxMatchCase);
            Controls.Add(btnNext);
            Controls.Add(btnPrev);
            Controls.Add(textBoxSearch);
            MaximizeBox = false;
            Name = "SearchForm";
            Text = "SearchForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnPrev;
        private Button btnNext;
        private CheckBox checkBoxMatchCase;
        private TextBox textBoxSearch;
    }
}