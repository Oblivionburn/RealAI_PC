
namespace Real_AI
{
    partial class ReadFile
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
            this.lbl_File = new System.Windows.Forms.Label();
            this.FileBox = new System.Windows.Forms.TextBox();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.ProgressBar_Main = new Real_AI.TextProgressBar();
            this.ProgressBar_Detail = new Real_AI.TextProgressBar();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Read = new System.Windows.Forms.Button();
            this.lbl_ElapsedTime = new System.Windows.Forms.Label();
            this.lbl_RemainingTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_File
            // 
            this.lbl_File.AutoSize = true;
            this.lbl_File.Location = new System.Drawing.Point(13, 13);
            this.lbl_File.Name = "lbl_File";
            this.lbl_File.Size = new System.Drawing.Size(26, 13);
            this.lbl_File.TabIndex = 0;
            this.lbl_File.Text = "File:";
            // 
            // FileBox
            // 
            this.FileBox.Location = new System.Drawing.Point(45, 10);
            this.FileBox.Name = "FileBox";
            this.FileBox.Size = new System.Drawing.Size(375, 20);
            this.FileBox.TabIndex = 1;
            this.FileBox.TextChanged += new System.EventHandler(this.FileBox_TextChanged);
            // 
            // btn_Browse
            // 
            this.btn_Browse.Location = new System.Drawing.Point(426, 8);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(75, 23);
            this.btn_Browse.TabIndex = 2;
            this.btn_Browse.Text = "Browse";
            this.btn_Browse.UseVisualStyleBackColor = true;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // ProgressBar_Main
            // 
            this.ProgressBar_Main.CustomText = "";
            this.ProgressBar_Main.Location = new System.Drawing.Point(45, 65);
            this.ProgressBar_Main.Name = "ProgressBar_Main";
            this.ProgressBar_Main.ProgressColor = System.Drawing.Color.DarkTurquoise;
            this.ProgressBar_Main.Size = new System.Drawing.Size(375, 23);
            this.ProgressBar_Main.Step = 1;
            this.ProgressBar_Main.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ProgressBar_Main.TabIndex = 4;
            this.ProgressBar_Main.TextColor = System.Drawing.Color.Black;
            this.ProgressBar_Main.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.ProgressBar_Main.VisualMode = Real_AI.ProgressBarDisplayMode.TextAndPercentage;
            // 
            // ProgressBar_Detail
            // 
            this.ProgressBar_Detail.CustomText = "";
            this.ProgressBar_Detail.Location = new System.Drawing.Point(45, 94);
            this.ProgressBar_Detail.Name = "ProgressBar_Detail";
            this.ProgressBar_Detail.ProgressColor = System.Drawing.Color.DarkTurquoise;
            this.ProgressBar_Detail.Size = new System.Drawing.Size(375, 23);
            this.ProgressBar_Detail.Step = 1;
            this.ProgressBar_Detail.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ProgressBar_Detail.TabIndex = 3;
            this.ProgressBar_Detail.TextColor = System.Drawing.Color.Black;
            this.ProgressBar_Detail.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.ProgressBar_Detail.VisualMode = Real_AI.ProgressBarDisplayMode.TextAndPercentage;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(426, 94);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Read
            // 
            this.btn_Read.Location = new System.Drawing.Point(426, 65);
            this.btn_Read.Name = "btn_Read";
            this.btn_Read.Size = new System.Drawing.Size(75, 23);
            this.btn_Read.TabIndex = 6;
            this.btn_Read.Text = "Read";
            this.btn_Read.UseVisualStyleBackColor = true;
            this.btn_Read.Click += new System.EventHandler(this.btn_Read_Click);
            // 
            // lbl_ElapsedTime
            // 
            this.lbl_ElapsedTime.AutoSize = true;
            this.lbl_ElapsedTime.Location = new System.Drawing.Point(42, 46);
            this.lbl_ElapsedTime.Name = "lbl_ElapsedTime";
            this.lbl_ElapsedTime.Size = new System.Drawing.Size(140, 13);
            this.lbl_ElapsedTime.TabIndex = 7;
            this.lbl_ElapsedTime.Text = "Elapsed Time: 00:00:00.000";
            // 
            // lbl_RemainingTime
            // 
            this.lbl_RemainingTime.AutoSize = true;
            this.lbl_RemainingTime.Location = new System.Drawing.Point(268, 46);
            this.lbl_RemainingTime.Name = "lbl_RemainingTime";
            this.lbl_RemainingTime.Size = new System.Drawing.Size(152, 13);
            this.lbl_RemainingTime.TabIndex = 8;
            this.lbl_RemainingTime.Text = "Remaining Time: 00:00:00.000";
            // 
            // ReadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 126);
            this.Controls.Add(this.lbl_RemainingTime);
            this.Controls.Add(this.lbl_ElapsedTime);
            this.Controls.Add(this.btn_Read);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.ProgressBar_Main);
            this.Controls.Add(this.ProgressBar_Detail);
            this.Controls.Add(this.btn_Browse);
            this.Controls.Add(this.FileBox);
            this.Controls.Add(this.lbl_File);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "ReadFile";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Read File";
            this.Load += new System.EventHandler(this.ReadFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_File;
        private System.Windows.Forms.TextBox FileBox;
        private System.Windows.Forms.Button btn_Browse;
        private TextProgressBar ProgressBar_Detail;
        private TextProgressBar ProgressBar_Main;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Read;
        private System.Windows.Forms.Label lbl_ElapsedTime;
        private System.Windows.Forms.Label lbl_RemainingTime;
    }
}