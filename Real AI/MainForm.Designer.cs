namespace Real_AI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewSession = new System.Windows.Forms.ToolStripMenuItem();
            this.Link = new System.Windows.Forms.ToolStripMenuItem();
            this.Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.brainsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewBrain = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadBrain = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditBrain = new System.Windows.Forms.ToolStripMenuItem();
            this.EditOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewThinking = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WipeMemory = new System.Windows.Forms.ToolStripMenuItem();
            this.MergeBrains = new System.Windows.Forms.ToolStripMenuItem();
            this.FixSpelling = new System.Windows.Forms.ToolStripMenuItem();
            this.ReadFile = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_Output = new System.Windows.Forms.Label();
            this.InputBox = new System.Windows.Forms.TextBox();
            this.lbl_Input = new System.Windows.Forms.Label();
            this.EnterButton = new System.Windows.Forms.Button();
            this.OutputBox = new System.Windows.Forms.RichTextBox();
            this.lbl_ElapsedTime = new System.Windows.Forms.Label();
            this.lbl_RemainingTime = new System.Windows.Forms.Label();
            this.EncourageButton = new System.Windows.Forms.Button();
            this.DiscourageButton = new System.Windows.Forms.Button();
            this.ProgressBar_Main = new Real_AI.TextProgressBar();
            this.ProgressBar_Detail = new Real_AI.TextProgressBar();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.brainsToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(353, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewSession,
            this.Link,
            this.Exit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // NewSession
            // 
            this.NewSession.Name = "NewSession";
            this.NewSession.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewSession.Size = new System.Drawing.Size(183, 22);
            this.NewSession.Text = "New Session";
            this.NewSession.Click += new System.EventHandler(this.NewSession_Click);
            // 
            // Link
            // 
            this.Link.Name = "Link";
            this.Link.Size = new System.Drawing.Size(183, 22);
            this.Link.Text = "Establish Link";
            this.Link.Click += new System.EventHandler(this.Link_Click);
            // 
            // Exit
            // 
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(183, 22);
            this.Exit.Text = "Exit";
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // brainsToolStripMenuItem
            // 
            this.brainsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewBrain,
            this.LoadBrain});
            this.brainsToolStripMenuItem.Name = "brainsToolStripMenuItem";
            this.brainsToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.brainsToolStripMenuItem.Text = "Brains";
            // 
            // NewBrain
            // 
            this.NewBrain.Name = "NewBrain";
            this.NewBrain.Size = new System.Drawing.Size(100, 22);
            this.NewBrain.Text = "New";
            this.NewBrain.Click += new System.EventHandler(this.NewBrain_Click);
            // 
            // LoadBrain
            // 
            this.LoadBrain.Name = "LoadBrain";
            this.LoadBrain.Size = new System.Drawing.Size(100, 22);
            this.LoadBrain.Text = "Load";
            this.LoadBrain.Click += new System.EventHandler(this.LoadBrain_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditBrain,
            this.EditOptions});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // EditBrain
            // 
            this.EditBrain.Name = "EditBrain";
            this.EditBrain.Size = new System.Drawing.Size(116, 22);
            this.EditBrain.Text = "Brain";
            this.EditBrain.Click += new System.EventHandler(this.EditBrain_Click);
            // 
            // EditOptions
            // 
            this.EditOptions.Name = "EditOptions";
            this.EditOptions.Size = new System.Drawing.Size(116, 22);
            this.EditOptions.Text = "Options";
            this.EditOptions.Click += new System.EventHandler(this.EditOptions_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewThinking});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // ViewThinking
            // 
            this.ViewThinking.Name = "ViewThinking";
            this.ViewThinking.Size = new System.Drawing.Size(120, 22);
            this.ViewThinking.Text = "Thinking";
            this.ViewThinking.Click += new System.EventHandler(this.ViewThinking_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WipeMemory,
            this.MergeBrains,
            this.FixSpelling,
            this.ReadFile});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // WipeMemory
            // 
            this.WipeMemory.Name = "WipeMemory";
            this.WipeMemory.Size = new System.Drawing.Size(149, 22);
            this.WipeMemory.Text = "Wipe Memory";
            this.WipeMemory.Click += new System.EventHandler(this.WipeMemory_Click);
            // 
            // MergeBrains
            // 
            this.MergeBrains.Name = "MergeBrains";
            this.MergeBrains.Size = new System.Drawing.Size(149, 22);
            this.MergeBrains.Text = "Merge Brains";
            this.MergeBrains.Click += new System.EventHandler(this.MergeBrains_Click);
            // 
            // FixSpelling
            // 
            this.FixSpelling.Name = "FixSpelling";
            this.FixSpelling.Size = new System.Drawing.Size(149, 22);
            this.FixSpelling.Text = "Fix Spelling";
            this.FixSpelling.Click += new System.EventHandler(this.FixSpelling_Click);
            // 
            // ReadFile
            // 
            this.ReadFile.Name = "ReadFile";
            this.ReadFile.Size = new System.Drawing.Size(149, 22);
            this.ReadFile.Text = "Read File";
            this.ReadFile.Click += new System.EventHandler(this.ReadFile_Click);
            // 
            // lbl_Output
            // 
            this.lbl_Output.AutoSize = true;
            this.lbl_Output.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Output.Location = new System.Drawing.Point(12, 28);
            this.lbl_Output.Name = "lbl_Output";
            this.lbl_Output.Size = new System.Drawing.Size(39, 13);
            this.lbl_Output.TabIndex = 3;
            this.lbl_Output.Text = "Output";
            // 
            // InputBox
            // 
            this.InputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputBox.BackColor = System.Drawing.SystemColors.Window;
            this.InputBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.InputBox.Location = new System.Drawing.Point(12, 315);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(254, 23);
            this.InputBox.TabIndex = 0;
            this.InputBox.TextChanged += new System.EventHandler(this.InputBox_TextChanged);
            // 
            // lbl_Input
            // 
            this.lbl_Input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Input.AutoSize = true;
            this.lbl_Input.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Input.Location = new System.Drawing.Point(12, 299);
            this.lbl_Input.Name = "lbl_Input";
            this.lbl_Input.Size = new System.Drawing.Size(31, 13);
            this.lbl_Input.TabIndex = 5;
            this.lbl_Input.Text = "Input";
            // 
            // EnterButton
            // 
            this.EnterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EnterButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.EnterButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.EnterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnterButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.EnterButton.Location = new System.Drawing.Point(272, 315);
            this.EnterButton.Name = "EnterButton";
            this.EnterButton.Size = new System.Drawing.Size(68, 23);
            this.EnterButton.TabIndex = 6;
            this.EnterButton.Text = "Enter";
            this.EnterButton.UseVisualStyleBackColor = true;
            this.EnterButton.Click += new System.EventHandler(this.EnterButton_Click);
            // 
            // OutputBox
            // 
            this.OutputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputBox.Location = new System.Drawing.Point(12, 45);
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.OutputBox.Size = new System.Drawing.Size(329, 240);
            this.OutputBox.TabIndex = 10;
            this.OutputBox.Text = "";
            // 
            // lbl_ElapsedTime
            // 
            this.lbl_ElapsedTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_ElapsedTime.AutoSize = true;
            this.lbl_ElapsedTime.Location = new System.Drawing.Point(12, 379);
            this.lbl_ElapsedTime.Name = "lbl_ElapsedTime";
            this.lbl_ElapsedTime.Size = new System.Drawing.Size(140, 13);
            this.lbl_ElapsedTime.TabIndex = 11;
            this.lbl_ElapsedTime.Text = "Elapsed Time: 00:00:00.000";
            // 
            // lbl_RemainingTime
            // 
            this.lbl_RemainingTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_RemainingTime.AutoSize = true;
            this.lbl_RemainingTime.Location = new System.Drawing.Point(189, 379);
            this.lbl_RemainingTime.Name = "lbl_RemainingTime";
            this.lbl_RemainingTime.Size = new System.Drawing.Size(152, 13);
            this.lbl_RemainingTime.TabIndex = 12;
            this.lbl_RemainingTime.Text = "Remaining Time: 00:00:00.000";
            // 
            // EncourageButton
            // 
            this.EncourageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EncourageButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.EncourageButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.EncourageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EncourageButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.EncourageButton.Location = new System.Drawing.Point(12, 344);
            this.EncourageButton.Name = "EncourageButton";
            this.EncourageButton.Size = new System.Drawing.Size(140, 23);
            this.EncourageButton.TabIndex = 13;
            this.EncourageButton.Text = "Encourage";
            this.EncourageButton.UseVisualStyleBackColor = true;
            this.EncourageButton.Click += new System.EventHandler(this.EncourageButton_Click);
            // 
            // DiscourageButton
            // 
            this.DiscourageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DiscourageButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DiscourageButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DiscourageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiscourageButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DiscourageButton.Location = new System.Drawing.Point(192, 344);
            this.DiscourageButton.Name = "DiscourageButton";
            this.DiscourageButton.Size = new System.Drawing.Size(148, 23);
            this.DiscourageButton.TabIndex = 14;
            this.DiscourageButton.Text = "Discourage";
            this.DiscourageButton.UseVisualStyleBackColor = true;
            this.DiscourageButton.Click += new System.EventHandler(this.DiscourageButton_Click);
            // 
            // ProgressBar_Main
            // 
            this.ProgressBar_Main.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar_Main.CustomText = "";
            this.ProgressBar_Main.ForeColor = System.Drawing.Color.DarkTurquoise;
            this.ProgressBar_Main.Location = new System.Drawing.Point(12, 400);
            this.ProgressBar_Main.Name = "ProgressBar_Main";
            this.ProgressBar_Main.ProgressColor = System.Drawing.Color.DarkTurquoise;
            this.ProgressBar_Main.Size = new System.Drawing.Size(329, 23);
            this.ProgressBar_Main.Step = 1;
            this.ProgressBar_Main.TabIndex = 9;
            this.ProgressBar_Main.TextColor = System.Drawing.Color.Black;
            this.ProgressBar_Main.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.ProgressBar_Main.VisualMode = Real_AI.ProgressBarDisplayMode.CurrProgress;
            // 
            // ProgressBar_Detail
            // 
            this.ProgressBar_Detail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar_Detail.CustomText = "";
            this.ProgressBar_Detail.ForeColor = System.Drawing.Color.DarkTurquoise;
            this.ProgressBar_Detail.Location = new System.Drawing.Point(12, 429);
            this.ProgressBar_Detail.Name = "ProgressBar_Detail";
            this.ProgressBar_Detail.ProgressColor = System.Drawing.Color.DarkTurquoise;
            this.ProgressBar_Detail.Size = new System.Drawing.Size(329, 23);
            this.ProgressBar_Detail.Step = 1;
            this.ProgressBar_Detail.TabIndex = 7;
            this.ProgressBar_Detail.TextColor = System.Drawing.Color.Black;
            this.ProgressBar_Detail.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.ProgressBar_Detail.VisualMode = Real_AI.ProgressBarDisplayMode.CurrProgress;
            // 
            // MainForm
            // 
            this.AcceptButton = this.EnterButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(353, 464);
            this.Controls.Add(this.DiscourageButton);
            this.Controls.Add(this.EncourageButton);
            this.Controls.Add(this.lbl_RemainingTime);
            this.Controls.Add(this.lbl_ElapsedTime);
            this.Controls.Add(this.OutputBox);
            this.Controls.Add(this.ProgressBar_Main);
            this.Controls.Add(this.ProgressBar_Detail);
            this.Controls.Add(this.EnterButton);
            this.Controls.Add(this.lbl_Input);
            this.Controls.Add(this.InputBox);
            this.Controls.Add(this.lbl_Output);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Real AI";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.Label lbl_Output;
        private System.Windows.Forms.Label lbl_Input;
        private System.Windows.Forms.ToolStripMenuItem EditBrain;
        private System.Windows.Forms.ToolStripMenuItem ViewThinking;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditOptions;
        private System.Windows.Forms.ToolStripMenuItem WipeMemory;
        private System.Windows.Forms.ToolStripMenuItem Exit;
        private System.Windows.Forms.ToolStripMenuItem MergeBrains;
        public System.Windows.Forms.TextBox InputBox;
        public System.Windows.Forms.Button EnterButton;
        private System.Windows.Forms.ToolStripMenuItem NewSession;
        private System.Windows.Forms.ToolStripMenuItem FixSpelling;
        public TextProgressBar ProgressBar_Detail;
        public TextProgressBar ProgressBar_Main;
        private System.Windows.Forms.ToolStripMenuItem brainsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewBrain;
        private System.Windows.Forms.ToolStripMenuItem LoadBrain;
        public System.Windows.Forms.RichTextBox OutputBox;
        private System.Windows.Forms.ToolStripMenuItem Link;
        private System.Windows.Forms.ToolStripMenuItem ReadFile;
        private System.Windows.Forms.Label lbl_ElapsedTime;
        private System.Windows.Forms.Label lbl_RemainingTime;
        public System.Windows.Forms.Button EncourageButton;
        public System.Windows.Forms.Button DiscourageButton;
    }
}

