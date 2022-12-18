namespace Real_AI
{
    partial class Merge
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
            this.first_box = new System.Windows.Forms.TextBox();
            this.lbl_FirstBrain = new System.Windows.Forms.Label();
            this.first_Open = new System.Windows.Forms.Button();
            this.second_Open = new System.Windows.Forms.Button();
            this.lbl_SecondBrain = new System.Windows.Forms.Label();
            this.second_box = new System.Windows.Forms.TextBox();
            this.btn_Merge = new System.Windows.Forms.Button();
            this.merged_Save = new System.Windows.Forms.Button();
            this.lbl_MergedBrain = new System.Windows.Forms.Label();
            this.merged_box = new System.Windows.Forms.TextBox();
            this.ProgressBar = new Real_AI.TextProgressBar();
            this.SuspendLayout();
            // 
            // first_box
            // 
            this.first_box.Location = new System.Drawing.Point(89, 12);
            this.first_box.Name = "first_box";
            this.first_box.Size = new System.Drawing.Size(328, 20);
            this.first_box.TabIndex = 0;
            // 
            // lbl_FirstBrain
            // 
            this.lbl_FirstBrain.AutoSize = true;
            this.lbl_FirstBrain.Location = new System.Drawing.Point(12, 15);
            this.lbl_FirstBrain.Name = "lbl_FirstBrain";
            this.lbl_FirstBrain.Size = new System.Drawing.Size(53, 13);
            this.lbl_FirstBrain.TabIndex = 1;
            this.lbl_FirstBrain.Text = "First Brain";
            // 
            // first_Open
            // 
            this.first_Open.Location = new System.Drawing.Point(423, 10);
            this.first_Open.Name = "first_Open";
            this.first_Open.Size = new System.Drawing.Size(75, 23);
            this.first_Open.TabIndex = 2;
            this.first_Open.Text = "Browse";
            this.first_Open.UseVisualStyleBackColor = true;
            this.first_Open.Click += new System.EventHandler(this.First_Open_Click);
            // 
            // second_Open
            // 
            this.second_Open.Location = new System.Drawing.Point(423, 36);
            this.second_Open.Name = "second_Open";
            this.second_Open.Size = new System.Drawing.Size(75, 23);
            this.second_Open.TabIndex = 5;
            this.second_Open.Text = "Browse";
            this.second_Open.UseVisualStyleBackColor = true;
            this.second_Open.Click += new System.EventHandler(this.Second_Open_Click);
            // 
            // lbl_SecondBrain
            // 
            this.lbl_SecondBrain.AutoSize = true;
            this.lbl_SecondBrain.Location = new System.Drawing.Point(12, 41);
            this.lbl_SecondBrain.Name = "lbl_SecondBrain";
            this.lbl_SecondBrain.Size = new System.Drawing.Size(71, 13);
            this.lbl_SecondBrain.TabIndex = 4;
            this.lbl_SecondBrain.Text = "Second Brain";
            // 
            // second_box
            // 
            this.second_box.Location = new System.Drawing.Point(89, 38);
            this.second_box.Name = "second_box";
            this.second_box.Size = new System.Drawing.Size(328, 20);
            this.second_box.TabIndex = 3;
            // 
            // btn_Merge
            // 
            this.btn_Merge.Location = new System.Drawing.Point(424, 123);
            this.btn_Merge.Name = "btn_Merge";
            this.btn_Merge.Size = new System.Drawing.Size(75, 23);
            this.btn_Merge.TabIndex = 6;
            this.btn_Merge.Text = "Merge";
            this.btn_Merge.UseVisualStyleBackColor = true;
            this.btn_Merge.Click += new System.EventHandler(this.Btn_Merge_Click);
            // 
            // merged_Save
            // 
            this.merged_Save.Location = new System.Drawing.Point(423, 94);
            this.merged_Save.Name = "merged_Save";
            this.merged_Save.Size = new System.Drawing.Size(75, 23);
            this.merged_Save.TabIndex = 9;
            this.merged_Save.Text = "Browse";
            this.merged_Save.UseVisualStyleBackColor = true;
            this.merged_Save.Click += new System.EventHandler(this.Merged_Save_Click);
            // 
            // lbl_MergedBrain
            // 
            this.lbl_MergedBrain.AutoSize = true;
            this.lbl_MergedBrain.Location = new System.Drawing.Point(12, 99);
            this.lbl_MergedBrain.Name = "lbl_MergedBrain";
            this.lbl_MergedBrain.Size = new System.Drawing.Size(70, 13);
            this.lbl_MergedBrain.TabIndex = 8;
            this.lbl_MergedBrain.Text = "Merged Brain";
            // 
            // merged_box
            // 
            this.merged_box.Location = new System.Drawing.Point(89, 96);
            this.merged_box.Name = "merged_box";
            this.merged_box.Size = new System.Drawing.Size(328, 20);
            this.merged_box.TabIndex = 7;
            // 
            // ProgressBar
            // 
            this.ProgressBar.CustomText = "";
            this.ProgressBar.ForeColor = System.Drawing.Color.DarkTurquoise;
            this.ProgressBar.Location = new System.Drawing.Point(13, 152);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.ProgressColor = System.Drawing.Color.DarkTurquoise;
            this.ProgressBar.Size = new System.Drawing.Size(486, 23);
            this.ProgressBar.Step = 1;
            this.ProgressBar.TabIndex = 10;
            this.ProgressBar.TextColor = System.Drawing.Color.Black;
            this.ProgressBar.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.ProgressBar.VisualMode = Real_AI.ProgressBarDisplayMode.TextAndPercentage;
            // 
            // Merge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 183);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.merged_Save);
            this.Controls.Add(this.lbl_MergedBrain);
            this.Controls.Add(this.merged_box);
            this.Controls.Add(this.btn_Merge);
            this.Controls.Add(this.second_Open);
            this.Controls.Add(this.lbl_SecondBrain);
            this.Controls.Add(this.second_box);
            this.Controls.Add(this.first_Open);
            this.Controls.Add(this.lbl_FirstBrain);
            this.Controls.Add(this.first_box);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Merge";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Merge";
            this.Load += new System.EventHandler(this.Merge_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox first_box;
        private System.Windows.Forms.Label lbl_FirstBrain;
        private System.Windows.Forms.Button first_Open;
        private System.Windows.Forms.Button second_Open;
        private System.Windows.Forms.Label lbl_SecondBrain;
        private System.Windows.Forms.TextBox second_box;
        private System.Windows.Forms.Button btn_Merge;
        private System.Windows.Forms.Button merged_Save;
        private System.Windows.Forms.Label lbl_MergedBrain;
        private System.Windows.Forms.TextBox merged_box;
        public TextProgressBar ProgressBar;
    }
}