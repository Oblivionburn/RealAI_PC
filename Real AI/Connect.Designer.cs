namespace Real_AI
{
    partial class Connect
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
            this.lbl_IP = new System.Windows.Forms.Label();
            this.IP_Box = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.TestButton = new System.Windows.Forms.Button();
            this.lbl_External_IP = new System.Windows.Forms.Label();
            this.ExternalBox = new System.Windows.Forms.TextBox();
            this.PortBox = new System.Windows.Forms.TextBox();
            this.lbl_Port = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_IP
            // 
            this.lbl_IP.AutoSize = true;
            this.lbl_IP.Location = new System.Drawing.Point(14, 16);
            this.lbl_IP.Name = "lbl_IP";
            this.lbl_IP.Size = new System.Drawing.Size(61, 13);
            this.lbl_IP.TabIndex = 0;
            this.lbl_IP.Text = "IP Address:";
            // 
            // IP_Box
            // 
            this.IP_Box.Location = new System.Drawing.Point(106, 13);
            this.IP_Box.MaxLength = 15;
            this.IP_Box.Name = "IP_Box";
            this.IP_Box.Size = new System.Drawing.Size(100, 20);
            this.IP_Box.TabIndex = 1;
            this.IP_Box.TextChanged += new System.EventHandler(this.IP_Box_TextChanged);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(106, 94);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(100, 23);
            this.ConnectButton.TabIndex = 2;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // TestButton
            // 
            this.TestButton.Location = new System.Drawing.Point(106, 65);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(100, 23);
            this.TestButton.TabIndex = 3;
            this.TestButton.Text = "Test Connection";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // lbl_External_IP
            // 
            this.lbl_External_IP.AutoSize = true;
            this.lbl_External_IP.Location = new System.Drawing.Point(14, 152);
            this.lbl_External_IP.Name = "lbl_External_IP";
            this.lbl_External_IP.Size = new System.Drawing.Size(86, 13);
            this.lbl_External_IP.TabIndex = 4;
            this.lbl_External_IP.Text = "Your External IP:";
            // 
            // ExternalBox
            // 
            this.ExternalBox.Location = new System.Drawing.Point(106, 149);
            this.ExternalBox.Name = "ExternalBox";
            this.ExternalBox.ReadOnly = true;
            this.ExternalBox.Size = new System.Drawing.Size(100, 20);
            this.ExternalBox.TabIndex = 5;
            // 
            // PortBox
            // 
            this.PortBox.Location = new System.Drawing.Point(106, 39);
            this.PortBox.MaxLength = 5;
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(100, 20);
            this.PortBox.TabIndex = 7;
            this.PortBox.TextChanged += new System.EventHandler(this.PortBox_TextChanged);
            // 
            // lbl_Port
            // 
            this.lbl_Port.AutoSize = true;
            this.lbl_Port.Location = new System.Drawing.Point(14, 42);
            this.lbl_Port.Name = "lbl_Port";
            this.lbl_Port.Size = new System.Drawing.Size(29, 13);
            this.lbl_Port.TabIndex = 6;
            this.lbl_Port.Text = "Port:";
            // 
            // Connect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 181);
            this.Controls.Add(this.PortBox);
            this.Controls.Add(this.lbl_Port);
            this.Controls.Add(this.ExternalBox);
            this.Controls.Add(this.lbl_External_IP);
            this.Controls.Add(this.TestButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.IP_Box);
            this.Controls.Add(this.lbl_IP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Connect";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Establish Link";
            this.Load += new System.EventHandler(this.Connect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_IP;
        private System.Windows.Forms.TextBox IP_Box;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.Label lbl_External_IP;
        private System.Windows.Forms.TextBox ExternalBox;
        private System.Windows.Forms.TextBox PortBox;
        private System.Windows.Forms.Label lbl_Port;
    }
}