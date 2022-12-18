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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.lbl_ThinkSpeed = new System.Windows.Forms.Label();
            this.ThinkSpeed = new System.Windows.Forms.Label();
            this.ThinkSpeed_Bar = new System.Windows.Forms.ProgressBar();
            this.chk_LearnFromThinking = new System.Windows.Forms.CheckBox();
            this.lbl_AttentionSpan = new System.Windows.Forms.Label();
            this.AttentionSpan = new System.Windows.Forms.Label();
            this.Attention_Bar = new System.Windows.Forms.ProgressBar();
            this.chk_Initiate = new System.Windows.Forms.CheckBox();
            this.chk_Thinking = new System.Windows.Forms.CheckBox();
            this.Voices = new System.Windows.Forms.ComboBox();
            this.lbl_Voice = new System.Windows.Forms.Label();
            this.chk_TTS = new System.Windows.Forms.CheckBox();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chk_ProceduralResponding = new System.Windows.Forms.CheckBox();
            this.chk_TopicResponding = new System.Windows.Forms.CheckBox();
            this.chk_InputResponding = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_ImportTheme = new System.Windows.Forms.Button();
            this.btn_ExportTheme = new System.Windows.Forms.Button();
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
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
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
            this.toolTip.SetToolTip(this.lbl_ThinkSpeed, "The rate at which the AI will respond to itself if Thinking is enabled.");
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
            this.ThinkSpeed_Bar.Location = new System.Drawing.Point(9, 23);
            this.ThinkSpeed_Bar.Maximum = 1000;
            this.ThinkSpeed_Bar.Name = "ThinkSpeed_Bar";
            this.ThinkSpeed_Bar.Size = new System.Drawing.Size(305, 23);
            this.ThinkSpeed_Bar.Step = 1;
            this.ThinkSpeed_Bar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ThinkSpeed_Bar.TabIndex = 3;
            this.ThinkSpeed_Bar.Value = 1000;
            // 
            // chk_LearnFromThinking
            // 
            this.chk_LearnFromThinking.AutoSize = true;
            this.chk_LearnFromThinking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_LearnFromThinking.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_LearnFromThinking.Location = new System.Drawing.Point(9, 79);
            this.chk_LearnFromThinking.Name = "chk_LearnFromThinking";
            this.chk_LearnFromThinking.Size = new System.Drawing.Size(155, 21);
            this.chk_LearnFromThinking.TabIndex = 5;
            this.chk_LearnFromThinking.Text = "Learn From Thinking";
            this.toolTip.SetToolTip(this.chk_LearnFromThinking, "Enabling this option will make the AI learn from its thoughts as if they were use" +
        "r input.");
            this.chk_LearnFromThinking.UseVisualStyleBackColor = true;
            this.chk_LearnFromThinking.CheckedChanged += new System.EventHandler(this.Chk_LearnFromThinking_CheckedChanged);
            // 
            // lbl_AttentionSpan
            // 
            this.lbl_AttentionSpan.AutoSize = true;
            this.lbl_AttentionSpan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AttentionSpan.Location = new System.Drawing.Point(6, 116);
            this.lbl_AttentionSpan.Name = "lbl_AttentionSpan";
            this.lbl_AttentionSpan.Size = new System.Drawing.Size(187, 17);
            this.lbl_AttentionSpan.TabIndex = 6;
            this.lbl_AttentionSpan.Text = "Attention Span (in seconds):";
            this.toolTip.SetToolTip(this.lbl_AttentionSpan, "The time the AI will wait before speaking again if Initiate Conversation is enabl" +
        "ed.");
            // 
            // AttentionSpan
            // 
            this.AttentionSpan.AutoSize = true;
            this.AttentionSpan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttentionSpan.Location = new System.Drawing.Point(199, 116);
            this.AttentionSpan.Name = "AttentionSpan";
            this.AttentionSpan.Size = new System.Drawing.Size(16, 17);
            this.AttentionSpan.TabIndex = 7;
            this.AttentionSpan.Text = "7";
            // 
            // Attention_Bar
            // 
            this.Attention_Bar.Location = new System.Drawing.Point(9, 136);
            this.Attention_Bar.Maximum = 10;
            this.Attention_Bar.Name = "Attention_Bar";
            this.Attention_Bar.Size = new System.Drawing.Size(305, 23);
            this.Attention_Bar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.Attention_Bar.TabIndex = 8;
            this.Attention_Bar.Value = 7;
            // 
            // chk_Initiate
            // 
            this.chk_Initiate.AutoSize = true;
            this.chk_Initiate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_Initiate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Initiate.Location = new System.Drawing.Point(9, 165);
            this.chk_Initiate.Name = "chk_Initiate";
            this.chk_Initiate.Size = new System.Drawing.Size(152, 21);
            this.chk_Initiate.TabIndex = 10;
            this.chk_Initiate.Text = "Initiate Conversation";
            this.toolTip.SetToolTip(this.chk_Initiate, "Enabling this option makes the AI more real-time and won\'t just wait for user inp" +
        "ut before speaking.");
            this.chk_Initiate.UseVisualStyleBackColor = true;
            this.chk_Initiate.CheckedChanged += new System.EventHandler(this.Chk_Initiate_CheckedChanged);
            // 
            // chk_Thinking
            // 
            this.chk_Thinking.AutoSize = true;
            this.chk_Thinking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_Thinking.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Thinking.Location = new System.Drawing.Point(9, 52);
            this.chk_Thinking.Name = "chk_Thinking";
            this.chk_Thinking.Size = new System.Drawing.Size(78, 21);
            this.chk_Thinking.TabIndex = 12;
            this.chk_Thinking.Text = "Thinking";
            this.toolTip.SetToolTip(this.chk_Thinking, "Enabling this option will make the AI converse with itself (internal dialogue).");
            this.chk_Thinking.UseVisualStyleBackColor = true;
            this.chk_Thinking.CheckedChanged += new System.EventHandler(this.Chk_Thinking_CheckedChanged);
            // 
            // Voices
            // 
            this.Voices.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Voices.FormattingEnabled = true;
            this.Voices.Location = new System.Drawing.Point(59, 309);
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
            this.lbl_Voice.Location = new System.Drawing.Point(6, 310);
            this.lbl_Voice.Name = "lbl_Voice";
            this.lbl_Voice.Size = new System.Drawing.Size(43, 17);
            this.lbl_Voice.TabIndex = 14;
            this.lbl_Voice.Text = "Voice";
            // 
            // chk_TTS
            // 
            this.chk_TTS.AutoSize = true;
            this.chk_TTS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_TTS.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_TTS.Location = new System.Drawing.Point(9, 336);
            this.chk_TTS.Name = "chk_TTS";
            this.chk_TTS.Size = new System.Drawing.Size(121, 21);
            this.chk_TTS.TabIndex = 16;
            this.chk_TTS.Text = "Text-to-Speech";
            this.toolTip.SetToolTip(this.chk_TTS, "Enabling this option will make the AI speak audibly using Windows native Text-to-" +
        "Speech engine.");
            this.chk_TTS.UseVisualStyleBackColor = true;
            this.chk_TTS.CheckedChanged += new System.EventHandler(this.Chk_TTS_CheckedChanged);
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
            this.tabPage1.Controls.Add(this.chk_ProceduralResponding);
            this.tabPage1.Controls.Add(this.chk_TopicResponding);
            this.tabPage1.Controls.Add(this.chk_InputResponding);
            this.tabPage1.Controls.Add(this.lbl_ThinkSpeed);
            this.tabPage1.Controls.Add(this.chk_TTS);
            this.tabPage1.Controls.Add(this.ThinkSpeed);
            this.tabPage1.Controls.Add(this.ThinkSpeed_Bar);
            this.tabPage1.Controls.Add(this.Voices);
            this.tabPage1.Controls.Add(this.lbl_Voice);
            this.tabPage1.Controls.Add(this.chk_Thinking);
            this.tabPage1.Controls.Add(this.chk_Initiate);
            this.tabPage1.Controls.Add(this.chk_LearnFromThinking);
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
            // chk_ProceduralResponding
            // 
            this.chk_ProceduralResponding.AutoSize = true;
            this.chk_ProceduralResponding.Checked = true;
            this.chk_ProceduralResponding.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_ProceduralResponding.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_ProceduralResponding.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_ProceduralResponding.Location = new System.Drawing.Point(9, 261);
            this.chk_ProceduralResponding.Name = "chk_ProceduralResponding";
            this.chk_ProceduralResponding.Size = new System.Drawing.Size(173, 21);
            this.chk_ProceduralResponding.TabIndex = 22;
            this.chk_ProceduralResponding.Text = "Procedural Responding";
            this.toolTip.SetToolTip(this.chk_ProceduralResponding, resources.GetString("chk_ProceduralResponding.ToolTip"));
            this.chk_ProceduralResponding.UseVisualStyleBackColor = true;
            this.chk_ProceduralResponding.CheckedChanged += new System.EventHandler(this.Chk_ProceduralResponding_CheckedChanged);
            // 
            // chk_TopicResponding
            // 
            this.chk_TopicResponding.AutoSize = true;
            this.chk_TopicResponding.Checked = true;
            this.chk_TopicResponding.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_TopicResponding.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_TopicResponding.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_TopicResponding.Location = new System.Drawing.Point(9, 234);
            this.chk_TopicResponding.Name = "chk_TopicResponding";
            this.chk_TopicResponding.Size = new System.Drawing.Size(183, 21);
            this.chk_TopicResponding.TabIndex = 21;
            this.chk_TopicResponding.Text = "Topic-based Responding";
            this.toolTip.SetToolTip(this.chk_TopicResponding, "Enabling this option will allow the AI to respond by looking up learned outputs t" +
        "o inputs \r\nwith topics that match the topics identified in the received input.");
            this.chk_TopicResponding.UseVisualStyleBackColor = true;
            this.chk_TopicResponding.CheckedChanged += new System.EventHandler(this.Chk_TopicResponding_CheckedChanged);
            // 
            // chk_InputResponding
            // 
            this.chk_InputResponding.AutoSize = true;
            this.chk_InputResponding.Checked = true;
            this.chk_InputResponding.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_InputResponding.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_InputResponding.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_InputResponding.Location = new System.Drawing.Point(9, 207);
            this.chk_InputResponding.Name = "chk_InputResponding";
            this.chk_InputResponding.Size = new System.Drawing.Size(179, 21);
            this.chk_InputResponding.TabIndex = 20;
            this.chk_InputResponding.Text = "Whole Input Responding";
            this.toolTip.SetToolTip(this.chk_InputResponding, "Enabling this option will allow the AI to respond by looking up learned outputs t" +
        "o inputs \r\nthat match the received input as a whole.");
            this.chk_InputResponding.UseVisualStyleBackColor = true;
            this.chk_InputResponding.CheckedChanged += new System.EventHandler(this.Chk_InputResponding_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_ImportTheme);
            this.tabPage2.Controls.Add(this.btn_ExportTheme);
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
            // btn_ImportTheme
            // 
            this.btn_ImportTheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_ImportTheme.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_ImportTheme.Location = new System.Drawing.Point(9, 389);
            this.btn_ImportTheme.Name = "btn_ImportTheme";
            this.btn_ImportTheme.Size = new System.Drawing.Size(147, 23);
            this.btn_ImportTheme.TabIndex = 17;
            this.btn_ImportTheme.Text = "Import";
            this.btn_ImportTheme.UseVisualStyleBackColor = true;
            this.btn_ImportTheme.Click += new System.EventHandler(this.Btn_ImportTheme_Click);
            // 
            // btn_ExportTheme
            // 
            this.btn_ExportTheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ExportTheme.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_ExportTheme.Location = new System.Drawing.Point(174, 389);
            this.btn_ExportTheme.Name = "btn_ExportTheme";
            this.btn_ExportTheme.Size = new System.Drawing.Size(147, 23);
            this.btn_ExportTheme.TabIndex = 16;
            this.btn_ExportTheme.Text = "Export";
            this.btn_ExportTheme.UseVisualStyleBackColor = true;
            this.btn_ExportTheme.Click += new System.EventHandler(this.Btn_ExportTheme_Click);
            // 
            // pic_ControlSelected
            // 
            this.pic_ControlSelected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_ControlSelected.Location = new System.Drawing.Point(140, 116);
            this.pic_ControlSelected.Name = "pic_ControlSelected";
            this.pic_ControlSelected.Size = new System.Drawing.Size(16, 16);
            this.pic_ControlSelected.TabIndex = 15;
            this.pic_ControlSelected.TabStop = false;
            this.pic_ControlSelected.Click += new System.EventHandler(this.Pic_ControlSelected_Click);
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
            this.pic_ControlHighlight.Click += new System.EventHandler(this.Pic_ControlHighlight_Click);
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
            this.pic_ProgressText.Click += new System.EventHandler(this.Pic_ProgressText_Click);
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
            this.pic_ProgressBackground.Click += new System.EventHandler(this.Pic_ProgressBackground_Click);
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
            this.pic_ControlText.Click += new System.EventHandler(this.Pic_ControlText_Click);
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
            this.pic_ControlBackground.Click += new System.EventHandler(this.Pic_ControlBackground_Click);
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
            this.pic_WindowText.Click += new System.EventHandler(this.Pic_WindowText_Click);
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
            this.pic_WindowBackground.Click += new System.EventHandler(this.Pic_WindowBackground_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 0;
            this.toolTip.AutoPopDelay = 32767;
            this.toolTip.InitialDelay = 0;
            this.toolTip.ReshowDelay = 0;
            this.toolTip.UseAnimation = false;
            this.toolTip.UseFading = false;
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
        private System.Windows.Forms.CheckBox chk_LearnFromThinking;
        private System.Windows.Forms.Label lbl_AttentionSpan;
        private System.Windows.Forms.Label AttentionSpan;
        public System.Windows.Forms.ProgressBar Attention_Bar;
        private System.Windows.Forms.CheckBox chk_Initiate;
        private System.Windows.Forms.CheckBox chk_Thinking;
        private System.Windows.Forms.ComboBox Voices;
        private System.Windows.Forms.Label lbl_Voice;
        private System.Windows.Forms.CheckBox chk_TTS;
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
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox chk_ProceduralResponding;
        private System.Windows.Forms.CheckBox chk_TopicResponding;
        private System.Windows.Forms.CheckBox chk_InputResponding;
        private System.Windows.Forms.Button btn_ExportTheme;
        private System.Windows.Forms.Button btn_ImportTheme;
    }
}