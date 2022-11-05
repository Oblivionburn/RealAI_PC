namespace Real_AI
{
    partial class Spelling
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
            this.WordList = new System.Windows.Forms.ComboBox();
            this.lbl_SelectWord = new System.Windows.Forms.Label();
            this.NewWordBox = new System.Windows.Forms.TextBox();
            this.lbl_NewWord = new System.Windows.Forms.Label();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.ProgressBar = new Real_AI.TextProgressBar();
            this.SuspendLayout();
            // 
            // WordList
            // 
            this.WordList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.WordList.FormattingEnabled = true;
            this.WordList.Location = new System.Drawing.Point(104, 6);
            this.WordList.Name = "WordList";
            this.WordList.Size = new System.Drawing.Size(254, 21);
            this.WordList.TabIndex = 0;
            this.WordList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ComboBox_DrawItem);
            this.WordList.SelectedIndexChanged += new System.EventHandler(this.WordList_SelectedIndexChanged);
            // 
            // lbl_SelectWord
            // 
            this.lbl_SelectWord.AutoSize = true;
            this.lbl_SelectWord.Location = new System.Drawing.Point(12, 9);
            this.lbl_SelectWord.Name = "lbl_SelectWord";
            this.lbl_SelectWord.Size = new System.Drawing.Size(66, 13);
            this.lbl_SelectWord.TabIndex = 1;
            this.lbl_SelectWord.Text = "Select word:";
            // 
            // NewWordBox
            // 
            this.NewWordBox.Location = new System.Drawing.Point(104, 34);
            this.NewWordBox.Name = "NewWordBox";
            this.NewWordBox.Size = new System.Drawing.Size(254, 20);
            this.NewWordBox.TabIndex = 2;
            // 
            // lbl_NewWord
            // 
            this.lbl_NewWord.AutoSize = true;
            this.lbl_NewWord.Location = new System.Drawing.Point(12, 37);
            this.lbl_NewWord.Name = "lbl_NewWord";
            this.lbl_NewWord.Size = new System.Drawing.Size(83, 13);
            this.lbl_NewWord.TabIndex = 3;
            this.lbl_NewWord.Text = "Type new word:";
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(283, 60);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(75, 23);
            this.UpdateButton.TabIndex = 4;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateDatabase_Click);
            // 
            // ProgressBar
            // 
            this.ProgressBar.CustomText = "";
            this.ProgressBar.ForeColor = System.Drawing.Color.DarkTurquoise;
            this.ProgressBar.Location = new System.Drawing.Point(13, 97);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.ProgressColor = System.Drawing.Color.DarkTurquoise;
            this.ProgressBar.Size = new System.Drawing.Size(345, 23);
            this.ProgressBar.Step = 1;
            this.ProgressBar.TabIndex = 5;
            this.ProgressBar.TextColor = System.Drawing.Color.Black;
            this.ProgressBar.TextFont = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.ProgressBar.VisualMode = Real_AI.ProgressBarDisplayMode.Percentage;
            // 
            // Spelling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 132);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.lbl_NewWord);
            this.Controls.Add(this.NewWordBox);
            this.Controls.Add(this.lbl_SelectWord);
            this.Controls.Add(this.WordList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Spelling";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Spelling";
            this.Load += new System.EventHandler(this.Spelling_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox WordList;
        private System.Windows.Forms.Label lbl_SelectWord;
        private System.Windows.Forms.TextBox NewWordBox;
        private System.Windows.Forms.Label lbl_NewWord;
        private System.Windows.Forms.Button UpdateButton;
        public TextProgressBar ProgressBar;
    }
}