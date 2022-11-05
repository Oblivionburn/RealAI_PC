namespace Real_AI
{
    partial class Options
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
            this.lbl_ThinkSpeed = new System.Windows.Forms.Label();
            this.ThinkSpeed = new System.Windows.Forms.Label();
            this.ThinkSpeed_Bar = new System.Windows.Forms.ProgressBar();
            this.lbl_LearnFromThinking = new System.Windows.Forms.Label();
            this.LearnFromThinking_CheckBox = new System.Windows.Forms.CheckBox();
            this.lbl_AttentionSpan = new System.Windows.Forms.Label();
            this.AttentionSpan = new System.Windows.Forms.Label();
            this.Attention_Bar = new System.Windows.Forms.ProgressBar();
            this.lbl_Initiate = new System.Windows.Forms.Label();
            this.Initiate_CheckBox = new System.Windows.Forms.CheckBox();
            this.Thinking_CheckBox = new System.Windows.Forms.CheckBox();
            this.lbl_Thinking = new System.Windows.Forms.Label();
            this.Voices = new System.Windows.Forms.ComboBox();
            this.lbl_Voice = new System.Windows.Forms.Label();
            this.TTS_CheckBox = new System.Windows.Forms.CheckBox();
            this.lbl_TTS = new System.Windows.Forms.Label();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pic_ControlSelected = new System.Windows.Forms.PictureBox();
            this.lbl_ControlSelected = new System.Windows.Forms.Label();
            this.pic_ControlHighlight = new System.Windows.Forms.PictureBox();
            this.lbl_ControlHighlight = new System.Windows.Forms.Label();
            this.pic_ProgressText = new System.Windows.Forms.PictureBox();
            this.lbl_ProgressText = new System.Windows.Forms.Label();
            this.pic_ProgressBackground = new System.Windows.Forms.PictureBox();
            this.lbl_ProgressBackground = new System.Windows.Forms.Label();
            this.pic_ControlText = new System.Windows.Forms.PictureBox();
            this.lbl_ControlText = new System.Windows.Forms.Label();
            this.pic_ControlBackground = new System.Windows.Forms.PictureBox();
            this.lbl_ControlBackground = new System.Windows.Forms.Label();
            this.pic_WindowText = new System.Windows.Forms.PictureBox();
            this.lbl_WindowText = new System.Windows.Forms.Label();
            this.lbl_WindowBackground = new System.Windows.Forms.Label();
            this.pic_WindowBackground = new System.Windows.Forms.PictureBox();
            this.color_Background_Window = new System.Windows.Forms.ColorDialog();
            this.color_Text_Window = new System.Windows.Forms.ColorDialog();
            this.color_Background_Control = new System.Windows.Forms.ColorDialog();
            this.color_Text_Control = new System.Windows.Forms.ColorDialog();
            this.color_Background_Progress = new System.Windows.Forms.ColorDialog();
            this.color_Text_Progress = new System.Windows.Forms.ColorDialog();
            this.color_Highlight_Control = new System.Windows.Forms.ColorDialog();
            this.color_Selected_Control = new System.Windows.Forms.ColorDialog();
            this.Tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ControlSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ControlHighlight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ProgressText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ProgressBackground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ControlText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ControlBackground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_WindowText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_WindowBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_ThinkSpeed
            // 
            this.lbl_ThinkSpeed.AutoSize = true;
            this.lbl_ThinkSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ThinkSpeed.Location = new System.Drawing.Point(6, 3);
            this.lbl_ThinkSpeed.Name = "lbl_ThinkSpeed";
            this.lbl_ThinkSpeed.Size = new System.Drawing.Size(216, 17);
            this.lbl_ThinkSpeed.TabIndex = 0;
            this.lbl_ThinkSpeed.Text = "Thinking Speed (in milliseconds):";
            // 
            // ThinkSpeed
            // 
            this.ThinkSpeed.AutoSize = true;
            this.ThinkSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ThinkSpeed.Location = new System.Drawing.Point(225, 3);
            this.ThinkSpeed.Name = "ThinkSpeed";
            this.ThinkSpeed.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ThinkSpeed.Size = new System.Drawing.Size(40, 17);
            this.ThinkSpeed.TabIndex = 2;
            this.ThinkSpeed.Text = "1000";
            // 
            // ThinkSpeed_Bar
            // 
            this.ThinkSpeed_Bar.Location = new System.Drawing.Point(6, 23);
            this.ThinkSpeed_Bar.Maximum = 1000;
            this.ThinkSpeed_Bar.Name = "ThinkSpeed_Bar";
            this.ThinkSpeed_Bar.Size = new System.Drawing.Size(308, 23);
            this.ThinkSpeed_Bar.Step = 1;
            this.ThinkSpeed_Bar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ThinkSpeed_Bar.TabIndex = 3;
            this.ThinkSpeed_Bar.Value = 1000;
            // 
            // lbl_LearnFromThinking
            // 
            this.lbl_LearnFromThinking.AutoSize = true;
            this.lbl_LearnFromThinking.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LearnFromThinking.Location = new System.Drawing.Point(6, 69);
            this.lbl_LearnFromThinking.Name = "lbl_LearnFromThinking";
            this.lbl_LearnFromThinking.Size = new System.Drawing.Size(143, 17);
            this.lbl_LearnFromThinking.TabIndex = 4;
            this.lbl_LearnFromThinking.Text = "Learn From Thinking:";
            // 
            // LearnFromThinking_CheckBox
            // 
            this.LearnFromThinking_CheckBox.AutoSize = true;
            this.LearnFromThinking_CheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LearnFromThinking_CheckBox.Location = new System.Drawing.Point(155, 72);
            this.LearnFromThinking_CheckBox.Name = "LearnFromThinking_CheckBox";
            this.LearnFromThinking_CheckBox.Size = new System.Drawing.Size(12, 11);
            this.LearnFromThinking_CheckBox.TabIndex = 5;
            this.LearnFromThinking_CheckBox.UseVisualStyleBackColor = true;
            this.LearnFromThinking_CheckBox.CheckedChanged += new System.EventHandler(this.LearnFromThinking_CheckBox_CheckedChanged);
            // 
            // lbl_AttentionSpan
            // 
            this.lbl_AttentionSpan.AutoSize = true;
            this.lbl_AttentionSpan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AttentionSpan.Location = new System.Drawing.Point(6, 105);
            this.lbl_AttentionSpan.Name = "lbl_AttentionSpan";
            this.lbl_AttentionSpan.Size = new System.Drawing.Size(187, 17);
            this.lbl_AttentionSpan.TabIndex = 6;
            this.lbl_AttentionSpan.Text = "Attention Span (in seconds):";
            // 
            // AttentionSpan
            // 
            this.AttentionSpan.AutoSize = true;
            this.AttentionSpan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttentionSpan.Location = new System.Drawing.Point(199, 105);
            this.AttentionSpan.Name = "AttentionSpan";
            this.AttentionSpan.Size = new System.Drawing.Size(16, 17);
            this.AttentionSpan.TabIndex = 7;
            this.AttentionSpan.Text = "7";
            // 
            // Attention_Bar
            // 
            this.Attention_Bar.Location = new System.Drawing.Point(6, 125);
            this.Attention_Bar.Maximum = 10;
            this.Attention_Bar.Name = "Attention_Bar";
            this.Attention_Bar.Size = new System.Drawing.Size(308, 23);
            this.Attention_Bar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.Attention_Bar.TabIndex = 8;
            this.Attention_Bar.Value = 7;
            // 
            // lbl_Initiate
            // 
            this.lbl_Initiate.AutoSize = true;
            this.lbl_Initiate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Initiate.Location = new System.Drawing.Point(6, 151);
            this.lbl_Initiate.Name = "lbl_Initiate";
            this.lbl_Initiate.Size = new System.Drawing.Size(140, 17);
            this.lbl_Initiate.TabIndex = 9;
            this.lbl_Initiate.Text = "Initiate Conversation:";
            // 
            // Initiate_CheckBox
            // 
            this.Initiate_CheckBox.AutoSize = true;
            this.Initiate_CheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Initiate_CheckBox.Location = new System.Drawing.Point(155, 154);
            this.Initiate_CheckBox.Name = "Initiate_CheckBox";
            this.Initiate_CheckBox.Size = new System.Drawing.Size(12, 11);
            this.Initiate_CheckBox.TabIndex = 10;
            this.Initiate_CheckBox.UseVisualStyleBackColor = true;
            this.Initiate_CheckBox.CheckedChanged += new System.EventHandler(this.Initiate_CheckBox_CheckedChanged);
            // 
            // Thinking_CheckBox
            // 
            this.Thinking_CheckBox.AutoSize = true;
            this.Thinking_CheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Thinking_CheckBox.Location = new System.Drawing.Point(155, 52);
            this.Thinking_CheckBox.Name = "Thinking_CheckBox";
            this.Thinking_CheckBox.Size = new System.Drawing.Size(12, 11);
            this.Thinking_CheckBox.TabIndex = 12;
            this.Thinking_CheckBox.UseVisualStyleBackColor = true;
            this.Thinking_CheckBox.CheckedChanged += new System.EventHandler(this.Thinking_CheckBox_CheckedChanged);
            // 
            // lbl_Thinking
            // 
            this.lbl_Thinking.AutoSize = true;
            this.lbl_Thinking.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Thinking.Location = new System.Drawing.Point(6, 49);
            this.lbl_Thinking.Name = "lbl_Thinking";
            this.lbl_Thinking.Size = new System.Drawing.Size(66, 17);
            this.lbl_Thinking.TabIndex = 11;
            this.lbl_Thinking.Text = "Thinking:";
            // 
            // Voices
            // 
            this.Voices.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Voices.FormattingEnabled = true;
            this.Voices.Location = new System.Drawing.Point(59, 190);
            this.Voices.Name = "Voices";
            this.Voices.Size = new System.Drawing.Size(256, 21);
            this.Voices.TabIndex = 13;
            this.Voices.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ComboBox_DrawItem);
            this.Voices.SelectedIndexChanged += new System.EventHandler(this.Voices_SelectedIndexChanged);
            // 
            // lbl_Voice
            // 
            this.lbl_Voice.AutoSize = true;
            this.lbl_Voice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Voice.Location = new System.Drawing.Point(6, 191);
            this.lbl_Voice.Name = "lbl_Voice";
            this.lbl_Voice.Size = new System.Drawing.Size(43, 17);
            this.lbl_Voice.TabIndex = 14;
            this.lbl_Voice.Text = "Voice";
            // 
            // TTS_CheckBox
            // 
            this.TTS_CheckBox.AutoSize = true;
            this.TTS_CheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TTS_CheckBox.Location = new System.Drawing.Point(155, 217);
            this.TTS_CheckBox.Name = "TTS_CheckBox";
            this.TTS_CheckBox.Size = new System.Drawing.Size(12, 11);
            this.TTS_CheckBox.TabIndex = 16;
            this.TTS_CheckBox.UseVisualStyleBackColor = true;
            this.TTS_CheckBox.CheckedChanged += new System.EventHandler(this.TTS_CheckBox_CheckedChanged);
            // 
            // lbl_TTS
            // 
            this.lbl_TTS.AutoSize = true;
            this.lbl_TTS.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TTS.Location = new System.Drawing.Point(6, 214);
            this.lbl_TTS.Name = "lbl_TTS";
            this.lbl_TTS.Size = new System.Drawing.Size(109, 17);
            this.lbl_TTS.TabIndex = 15;
            this.lbl_TTS.Text = "Text-to-Speech:";
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.tabPage1);
            this.Tabs.Controls.Add(this.tabPage2);
            this.Tabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.Tabs.Location = new System.Drawing.Point(9, 9);
            this.Tabs.Margin = new System.Windows.Forms.Padding(0);
            this.Tabs.Multiline = true;
            this.Tabs.Name = "Tabs";
            this.Tabs.Padding = new System.Drawing.Point(10, 4);
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(335, 446);
            this.Tabs.TabIndex = 17;
            this.Tabs.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Tabs_DrawItem);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lbl_ThinkSpeed);
            this.tabPage1.Controls.Add(this.TTS_CheckBox);
            this.tabPage1.Controls.Add(this.ThinkSpeed);
            this.tabPage1.Controls.Add(this.lbl_TTS);
            this.tabPage1.Controls.Add(this.ThinkSpeed_Bar);
            this.tabPage1.Controls.Add(this.Voices);
            this.tabPage1.Controls.Add(this.lbl_Voice);
            this.tabPage1.Controls.Add(this.lbl_Thinking);
            this.tabPage1.Controls.Add(this.Thinking_CheckBox);
            this.tabPage1.Controls.Add(this.Initiate_CheckBox);
            this.tabPage1.Controls.Add(this.lbl_LearnFromThinking);
            this.tabPage1.Controls.Add(this.lbl_Initiate);
            this.tabPage1.Controls.Add(this.LearnFromThinking_CheckBox);
            this.tabPage1.Controls.Add(this.Attention_Bar);
            this.tabPage1.Controls.Add(this.lbl_AttentionSpan);
            this.tabPage1.Controls.Add(this.AttentionSpan);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(327, 418);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pic_ControlSelected);
            this.tabPage2.Controls.Add(this.lbl_ControlSelected);
            this.tabPage2.Controls.Add(this.pic_ControlHighlight);
            this.tabPage2.Controls.Add(this.lbl_ControlHighlight);
            this.tabPage2.Controls.Add(this.pic_ProgressText);
            this.tabPage2.Controls.Add(this.lbl_ProgressText);
            this.tabPage2.Controls.Add(this.pic_ProgressBackground);
            this.tabPage2.Controls.Add(this.lbl_ProgressBackground);
            this.tabPage2.Controls.Add(this.pic_ControlText);
            this.tabPage2.Controls.Add(this.lbl_ControlText);
            this.tabPage2.Controls.Add(this.pic_ControlBackground);
            this.tabPage2.Controls.Add(this.lbl_ControlBackground);
            this.tabPage2.Controls.Add(this.pic_WindowText);
            this.tabPage2.Controls.Add(this.lbl_WindowText);
            this.tabPage2.Controls.Add(this.lbl_WindowBackground);
            this.tabPage2.Controls.Add(this.pic_WindowBackground);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(327, 418);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Theme";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pic_ControlSelected
            // 
            this.pic_ControlSelected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_ControlSelected.Location = new System.Drawing.Point(140, 116);
            this.pic_ControlSelected.Name = "pic_ControlSelected";
            this.pic_ControlSelected.Size = new System.Drawing.Size(16, 16);
            this.pic_ControlSelected.TabIndex = 15;
            this.pic_ControlSelected.TabStop = false;
            this.pic_ControlSelected.Click += new System.EventHandler(this.pic_ControlSelected_Click);
            // 
            // lbl_ControlSelected
            // 
            this.lbl_ControlSelected.AutoSize = true;
            this.lbl_ControlSelected.Location = new System.Drawing.Point(6, 116);
            this.lbl_ControlSelected.Name = "lbl_ControlSelected";
            this.lbl_ControlSelected.Size = new System.Drawing.Size(88, 13);
            this.lbl_ControlSelected.TabIndex = 14;
            this.lbl_ControlSelected.Text = "Control Selected:";
            // 
            // pic_ControlHighlight
            // 
            this.pic_ControlHighlight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_ControlHighlight.Location = new System.Drawing.Point(140, 94);
            this.pic_ControlHighlight.Name = "pic_ControlHighlight";
            this.pic_ControlHighlight.Size = new System.Drawing.Size(16, 16);
            this.pic_ControlHighlight.TabIndex = 13;
            this.pic_ControlHighlight.TabStop = false;
            this.pic_ControlHighlight.Click += new System.EventHandler(this.pic_ControlHighlight_Click);
            // 
            // lbl_ControlHighlight
            // 
            this.lbl_ControlHighlight.AutoSize = true;
            this.lbl_ControlHighlight.Location = new System.Drawing.Point(6, 94);
            this.lbl_ControlHighlight.Name = "lbl_ControlHighlight";
            this.lbl_ControlHighlight.Size = new System.Drawing.Size(87, 13);
            this.lbl_ControlHighlight.TabIndex = 12;
            this.lbl_ControlHighlight.Text = "Control Highlight:";
            // 
            // pic_ProgressText
            // 
            this.pic_ProgressText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_ProgressText.Location = new System.Drawing.Point(140, 160);
            this.pic_ProgressText.Name = "pic_ProgressText";
            this.pic_ProgressText.Size = new System.Drawing.Size(16, 16);
            this.pic_ProgressText.TabIndex = 11;
            this.pic_ProgressText.TabStop = false;
            this.pic_ProgressText.Click += new System.EventHandler(this.pic_ProgressText_Click);
            // 
            // lbl_ProgressText
            // 
            this.lbl_ProgressText.AutoSize = true;
            this.lbl_ProgressText.Location = new System.Drawing.Point(6, 160);
            this.lbl_ProgressText.Name = "lbl_ProgressText";
            this.lbl_ProgressText.Size = new System.Drawing.Size(91, 13);
            this.lbl_ProgressText.TabIndex = 10;
            this.lbl_ProgressText.Text = "ProgressBar Text:";
            // 
            // pic_ProgressBackground
            // 
            this.pic_ProgressBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_ProgressBackground.Location = new System.Drawing.Point(140, 138);
            this.pic_ProgressBackground.Name = "pic_ProgressBackground";
            this.pic_ProgressBackground.Size = new System.Drawing.Size(16, 16);
            this.pic_ProgressBackground.TabIndex = 9;
            this.pic_ProgressBackground.TabStop = false;
            this.pic_ProgressBackground.Click += new System.EventHandler(this.pic_ProgressBackground_Click);
            // 
            // lbl_ProgressBackground
            // 
            this.lbl_ProgressBackground.AutoSize = true;
            this.lbl_ProgressBackground.Location = new System.Drawing.Point(6, 138);
            this.lbl_ProgressBackground.Name = "lbl_ProgressBackground";
            this.lbl_ProgressBackground.Size = new System.Drawing.Size(128, 13);
            this.lbl_ProgressBackground.TabIndex = 8;
            this.lbl_ProgressBackground.Text = "ProgressBar Background:";
            // 
            // pic_ControlText
            // 
            this.pic_ControlText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_ControlText.Location = new System.Drawing.Point(140, 72);
            this.pic_ControlText.Name = "pic_ControlText";
            this.pic_ControlText.Size = new System.Drawing.Size(16, 16);
            this.pic_ControlText.TabIndex = 7;
            this.pic_ControlText.TabStop = false;
            this.pic_ControlText.Click += new System.EventHandler(this.pic_ControlText_Click);
            // 
            // lbl_ControlText
            // 
            this.lbl_ControlText.AutoSize = true;
            this.lbl_ControlText.Location = new System.Drawing.Point(6, 72);
            this.lbl_ControlText.Name = "lbl_ControlText";
            this.lbl_ControlText.Size = new System.Drawing.Size(67, 13);
            this.lbl_ControlText.TabIndex = 6;
            this.lbl_ControlText.Text = "Control Text:";
            // 
            // pic_ControlBackground
            // 
            this.pic_ControlBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_ControlBackground.Location = new System.Drawing.Point(140, 50);
            this.pic_ControlBackground.Name = "pic_ControlBackground";
            this.pic_ControlBackground.Size = new System.Drawing.Size(16, 16);
            this.pic_ControlBackground.TabIndex = 5;
            this.pic_ControlBackground.TabStop = false;
            this.pic_ControlBackground.Click += new System.EventHandler(this.pic_ControlBackground_Click);
            // 
            // lbl_ControlBackground
            // 
            this.lbl_ControlBackground.AutoSize = true;
            this.lbl_ControlBackground.Location = new System.Drawing.Point(6, 50);
            this.lbl_ControlBackground.Name = "lbl_ControlBackground";
            this.lbl_ControlBackground.Size = new System.Drawing.Size(104, 13);
            this.lbl_ControlBackground.TabIndex = 4;
            this.lbl_ControlBackground.Text = "Control Background:";
            // 
            // pic_WindowText
            // 
            this.pic_WindowText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_WindowText.Location = new System.Drawing.Point(140, 28);
            this.pic_WindowText.Name = "pic_WindowText";
            this.pic_WindowText.Size = new System.Drawing.Size(16, 16);
            this.pic_WindowText.TabIndex = 3;
            this.pic_WindowText.TabStop = false;
            this.pic_WindowText.Click += new System.EventHandler(this.pic_WindowText_Click);
            // 
            // lbl_WindowText
            // 
            this.lbl_WindowText.AutoSize = true;
            this.lbl_WindowText.Location = new System.Drawing.Point(6, 28);
            this.lbl_WindowText.Name = "lbl_WindowText";
            this.lbl_WindowText.Size = new System.Drawing.Size(73, 13);
            this.lbl_WindowText.TabIndex = 2;
            this.lbl_WindowText.Text = "Window Text:";
            // 
            // lbl_WindowBackground
            // 
            this.lbl_WindowBackground.AutoSize = true;
            this.lbl_WindowBackground.Location = new System.Drawing.Point(6, 6);
            this.lbl_WindowBackground.Name = "lbl_WindowBackground";
            this.lbl_WindowBackground.Size = new System.Drawing.Size(110, 13);
            this.lbl_WindowBackground.TabIndex = 1;
            this.lbl_WindowBackground.Text = "Window Background:";
            // 
            // pic_WindowBackground
            // 
            this.pic_WindowBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_WindowBackground.Location = new System.Drawing.Point(140, 6);
            this.pic_WindowBackground.Name = "pic_WindowBackground";
            this.pic_WindowBackground.Size = new System.Drawing.Size(16, 16);
            this.pic_WindowBackground.TabIndex = 0;
            this.pic_WindowBackground.TabStop = false;
            this.pic_WindowBackground.Click += new System.EventHandler(this.pic_WindowBackground_Click);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(353, 464);
            this.Controls.Add(this.Tabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            this.Tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ControlSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ControlHighlight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ProgressText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ProgressBackground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ControlText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ControlBackground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_WindowText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_WindowBackground)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_ThinkSpeed;
        private System.Windows.Forms.Label ThinkSpeed;
        private System.Windows.Forms.ProgressBar ThinkSpeed_Bar;
        private System.Windows.Forms.Label lbl_LearnFromThinking;
        private System.Windows.Forms.CheckBox LearnFromThinking_CheckBox;
        private System.Windows.Forms.Label lbl_AttentionSpan;
        private System.Windows.Forms.Label AttentionSpan;
        public System.Windows.Forms.ProgressBar Attention_Bar;
        private System.Windows.Forms.Label lbl_Initiate;
        private System.Windows.Forms.CheckBox Initiate_CheckBox;
        private System.Windows.Forms.CheckBox Thinking_CheckBox;
        private System.Windows.Forms.Label lbl_Thinking;
        private System.Windows.Forms.ComboBox Voices;
        private System.Windows.Forms.Label lbl_Voice;
        private System.Windows.Forms.CheckBox TTS_CheckBox;
        private System.Windows.Forms.Label lbl_TTS;
        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lbl_WindowBackground;
        private System.Windows.Forms.PictureBox pic_WindowBackground;
        private System.Windows.Forms.ColorDialog color_Background_Window;
        private System.Windows.Forms.PictureBox pic_WindowText;
        private System.Windows.Forms.Label lbl_WindowText;
        private System.Windows.Forms.ColorDialog color_Text_Window;
        private System.Windows.Forms.PictureBox pic_ControlText;
        private System.Windows.Forms.Label lbl_ControlText;
        private System.Windows.Forms.PictureBox pic_ControlBackground;
        private System.Windows.Forms.Label lbl_ControlBackground;
        private System.Windows.Forms.ColorDialog color_Background_Control;
        private System.Windows.Forms.ColorDialog color_Text_Control;
        private System.Windows.Forms.PictureBox pic_ProgressText;
        private System.Windows.Forms.Label lbl_ProgressText;
        private System.Windows.Forms.PictureBox pic_ProgressBackground;
        private System.Windows.Forms.Label lbl_ProgressBackground;
        private System.Windows.Forms.ColorDialog color_Background_Progress;
        private System.Windows.Forms.ColorDialog color_Text_Progress;
        private System.Windows.Forms.PictureBox pic_ControlSelected;
        private System.Windows.Forms.Label lbl_ControlSelected;
        private System.Windows.Forms.PictureBox pic_ControlHighlight;
        private System.Windows.Forms.Label lbl_ControlHighlight;
        private System.Windows.Forms.ColorDialog color_Highlight_Control;
        private System.Windows.Forms.ColorDialog color_Selected_Control;
    }
}