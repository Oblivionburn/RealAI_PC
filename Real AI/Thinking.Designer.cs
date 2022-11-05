namespace Real_AI
{
    partial class Thinking
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
            this.ThinkingBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ThinkingBox
            // 
            this.ThinkingBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ThinkingBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ThinkingBox.Location = new System.Drawing.Point(0, 0);
            this.ThinkingBox.Multiline = true;
            this.ThinkingBox.Name = "ThinkingBox";
            this.ThinkingBox.ReadOnly = true;
            this.ThinkingBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ThinkingBox.Size = new System.Drawing.Size(353, 464);
            this.ThinkingBox.TabIndex = 0;
            // 
            // Thinking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 464);
            this.Controls.Add(this.ThinkingBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Thinking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Thinking";
            this.Load += new System.EventHandler(this.Thinking_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox ThinkingBox;
    }
}