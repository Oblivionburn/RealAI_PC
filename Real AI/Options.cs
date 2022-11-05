using System;
using System.Drawing;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;
using Real_AI.Util;

namespace Real_AI
{
    public partial class Options : Form
    {
        System.Windows.Forms.Timer ThinkTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer AttentionTimer = new System.Windows.Forms.Timer();

        public Options()
        {
            FormClosing += Options_FormClosing;
            
            InitializeComponent();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            SetColors();

            int speed = MainForm.ThinkTimer.Interval;

            ThinkSpeed.Text = speed.ToString();

            ThinkSpeed_Bar.Value = speed;
            ThinkSpeed_Bar.MouseDown += ThinkSpeed_Bar_MouseDown;
            ThinkSpeed_Bar.MouseUp += ThinkSpeed_Bar_MouseUp;

            int span = MainForm.AttentionTimer.Interval / 1000;
            Attention_Bar.Value = span;
            Attention_Bar.MouseDown += Attention_Bar_MouseDown;
            Attention_Bar.MouseUp += Attention_Bar_MouseUp;

            Thinking_CheckBox.Checked = Brain.Thinking;
            LearnFromThinking_CheckBox.Checked = Brain.LearnFromThinking;
            Initiate_CheckBox.Checked = Brain.Initiate;
            TTS_CheckBox.Checked = MainForm.tts;
            Voices.SelectedText = MainForm.tts_voice;

            ThinkTimer.Interval = 100;
            ThinkTimer.Tick += ThinkTimer_Tick;

            AttentionTimer.Interval = 100;
            AttentionTimer.Tick += AttentionTimer_Tick;

            Voices.Items.Clear();
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            foreach (var voice in synthesizer.GetInstalledVoices())
            {
                var info = voice.VoiceInfo;
                Voices.Items.Add(info.Name);
            }
        }

        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.ResumeTimers();
        }

        private void Tabs_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage CurrentTab = Tabs.TabPages[e.Index];
            Rectangle ItemRect = Tabs.GetTabRect(e.Index);
            SolidBrush FillBrush = new SolidBrush(AppUtil.background_window);
            SolidBrush TextBrush = new SolidBrush(AppUtil.text_window);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            //Next we'll paint the TabItem with our Fill Brush
            e.Graphics.FillRectangle(FillBrush, ItemRect);

            //Now draw the text.
            e.Graphics.DrawString(CurrentTab.Text, e.Font, TextBrush, ItemRect, sf);

            //Paint the empty space to the right of the tabs
            Rectangle r = Tabs.GetTabRect(Tabs.TabPages.Count - 1);
            RectangleF rf = new RectangleF(r.X + r.Width, r.Y - 5, Tabs.Width - (r.X + r.Width), r.Height + 5);
            e.Graphics.FillRectangle(FillBrush, rf);

            //Paint the border
            Pen p = new Pen(FillBrush.Color, 8);
            e.Graphics.DrawRectangle(p, CurrentTab.Bounds);

            //Reset any Graphics rotation
            e.Graphics.ResetTransform();

            //Finally, we should Dispose of our brushes.
            FillBrush.Dispose();
            TextBrush.Dispose();
        }

        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }

            ComboBox combo = sender as ComboBox;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(AppUtil.highlight_control), e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(combo.BackColor), e.Bounds);
            }
                
            e.Graphics.DrawString(combo.Items[e.Index].ToString(), e.Font, new SolidBrush(combo.ForeColor), new Point(e.Bounds.X, e.Bounds.Y));
            e.DrawFocusRectangle();
        }

        private void SetColors()
        {
            BackColor = AppUtil.background_window;
            ForeColor = AppUtil.text_window;

            pic_WindowBackground.BackColor = AppUtil.background_window;
            pic_WindowText.BackColor = AppUtil.text_window;
            pic_ControlBackground.BackColor = AppUtil.background_control;
            pic_ControlText.BackColor = AppUtil.text_control;
            pic_ControlHighlight.BackColor = AppUtil.highlight_control;
            pic_ControlSelected.BackColor = AppUtil.selected_control;
            pic_ProgressBackground.BackColor = AppUtil.background_progress;
            pic_ProgressText.BackColor = AppUtil.text_progress;

            foreach (TabPage tab in Tabs.TabPages)
            {
                tab.BackColor = AppUtil.background_window;
                tab.ForeColor = AppUtil.text_window;
            }

            ThinkSpeed_Bar.BackColor = AppUtil.background_window;
            ThinkSpeed_Bar.ForeColor = AppUtil.background_progress;

            Attention_Bar.BackColor = AppUtil.background_window;
            Attention_Bar.ForeColor = AppUtil.background_progress;

            lbl_ThinkSpeed.ForeColor = AppUtil.text_window;
            ThinkSpeed.ForeColor = AppUtil.text_window;
            lbl_Thinking.ForeColor = AppUtil.text_window;
            lbl_LearnFromThinking.ForeColor = AppUtil.text_window;
            lbl_AttentionSpan.ForeColor = AppUtil.text_window;
            AttentionSpan.ForeColor = AppUtil.text_window;
            lbl_Initiate.ForeColor = AppUtil.text_window;
            lbl_Voice.ForeColor = AppUtil.text_window;
            lbl_TTS.ForeColor = AppUtil.text_window;
            lbl_WindowBackground.ForeColor = AppUtil.text_window;
            lbl_WindowText.ForeColor = AppUtil.text_window;

            Thinking_CheckBox.BackColor = AppUtil.background_control;
            Thinking_CheckBox.ForeColor = AppUtil.text_control;

            LearnFromThinking_CheckBox.BackColor = AppUtil.background_control;
            LearnFromThinking_CheckBox.ForeColor = AppUtil.text_control;

            Initiate_CheckBox.BackColor = AppUtil.background_control;
            Initiate_CheckBox.ForeColor = AppUtil.text_control;

            Voices.BackColor = AppUtil.background_control;
            Voices.ForeColor = AppUtil.text_control;
            

            TTS_CheckBox.BackColor = AppUtil.background_control;
            TTS_CheckBox.ForeColor = AppUtil.text_control;

            MainForm.SetColors();
        }

        private void ThinkSpeed_Bar_MouseUp(object sender, MouseEventArgs e)
        {
            ThinkTimer.Stop();
        }

        private void ThinkSpeed_Bar_MouseDown(object sender, MouseEventArgs e)
        {
            ThinkTimer.Start();
        }

        private void ThinkTimer_Tick(object sender, EventArgs e)
        {
            Point location = ThinkSpeed_Bar.PointToScreen(Point.Empty);
            int position = Cursor.Position.X - location.X;
            int percent = (position * 100) / ThinkSpeed_Bar.Width;

            int value = ((percent * 1000) / 100);
            if (value > 1000)
            {
                value = 1000;
            }
            else if (value < 100)
            {
                value = 100;
            }

            ThinkSpeed_Bar.Value = value;

            ThinkSpeed.Text = ThinkSpeed_Bar.Value.ToString();
            MainForm.ThinkTimer.Interval = ThinkSpeed_Bar.Value;
            AppUtil.Set_ThinkSpeed(ThinkSpeed_Bar.Value);
        }

        private void Attention_Bar_MouseUp(object sender, MouseEventArgs e)
        {
            AttentionTimer.Stop();
        }

        private void Attention_Bar_MouseDown(object sender, MouseEventArgs e)
        {
            AttentionTimer.Start();
        }

        private void AttentionTimer_Tick(object sender, EventArgs e)
        {
            Point location = Attention_Bar.PointToScreen(Point.Empty);
            int position = Cursor.Position.X - location.X;
            int percent = (position * 100) / Attention_Bar.Width;

            int value = ((percent * 10) / 100) + 1;
            if (value > 10)
            {
                value = 10;
            }
            else if (value < 1)
            {
                value = 1;
            }

            Attention_Bar.Value = value;

            AttentionSpan.Text = Attention_Bar.Value.ToString();
            MainForm.AttentionTimer.Interval = Attention_Bar.Value * 1000;
            AppUtil.Set_AttentionSpan(Attention_Bar.Value);
        }

        private void LearnFromThinking_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Brain.LearnFromThinking = LearnFromThinking_CheckBox.Checked;
            AppUtil.Set_LearnFromThinking(LearnFromThinking_CheckBox.Checked.ToString());
        }

        private void Initiate_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Brain.Initiate = Initiate_CheckBox.Checked;
            AppUtil.Set_Initiate(Brain.Initiate.ToString());

            if (Brain.Initiate)
            {
                MainForm.AttentionTimer.Start();
            }
            else
            {
                MainForm.AttentionTimer.Stop();
            }
        }

        private void Thinking_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Brain.Thinking = Thinking_CheckBox.Checked;
            AppUtil.Set_Thinking(Brain.Thinking.ToString());

            if (!Brain.Thinking)
            {
                foreach (Thread thread in MainForm.threads)
                {
                    thread.Abort();
                }
            }
        }

        private void Voices_SelectedIndexChanged(object sender, EventArgs e)
        {
            string voice = ((ComboBox)sender).SelectedItem.ToString();
            MainForm.tts_voice = voice;
            MainForm.synthesizer.SelectVoice(voice);
            AppUtil.Set_TTS_Voice(voice);
        }

        private void TTS_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.tts = TTS_CheckBox.Checked;
            AppUtil.Set_TTS(MainForm.tts.ToString());
        }

        private void pic_WindowBackground_Click(object sender, EventArgs e)
        {
            if (color_Background_Window.ShowDialog() == DialogResult.OK)
            {
                Color color = color_Background_Window.Color;
                string str_color = AppUtil.ColorToHex(color);
                AppUtil.Set_Config_Color("Color_Background_Window", str_color);

                AppUtil.background_window = color;
                pic_WindowBackground.BackColor = color;

                SetColors();
            }
        }

        private void pic_WindowText_Click(object sender, EventArgs e)
        {
            if (color_Text_Window.ShowDialog() == DialogResult.OK)
            {
                Color color = color_Text_Window.Color;
                string str_color = AppUtil.ColorToHex(color);
                AppUtil.Set_Config_Color("Color_Text_Window", str_color);

                AppUtil.text_window = color;
                pic_WindowText.BackColor = color;

                SetColors();
            }
        }

        private void pic_ControlBackground_Click(object sender, EventArgs e)
        {
            if (color_Background_Control.ShowDialog() == DialogResult.OK)
            {
                Color color = color_Background_Control.Color;
                string str_color = AppUtil.ColorToHex(color);
                AppUtil.Set_Config_Color("Color_Background_Control", str_color);

                AppUtil.background_control = color;
                pic_ControlBackground.BackColor = color;

                SetColors();
            }
        }

        private void pic_ControlText_Click(object sender, EventArgs e)
        {
            if (color_Text_Control.ShowDialog() == DialogResult.OK)
            {
                Color color = color_Text_Control.Color;
                string str_color = AppUtil.ColorToHex(color);
                AppUtil.Set_Config_Color("Color_Text_Control", str_color);

                AppUtil.text_control = color;
                pic_ControlText.BackColor = color;

                SetColors();
            }
        }

        private void pic_ControlHighlight_Click(object sender, EventArgs e)
        {
            if (color_Highlight_Control.ShowDialog() == DialogResult.OK)
            {
                Color color = color_Highlight_Control.Color;
                string str_color = AppUtil.ColorToHex(color);
                AppUtil.Set_Config_Color("Color_Highlight_Control", str_color);

                AppUtil.highlight_control = color;
                pic_ControlHighlight.BackColor = color;

                SetColors();
            }
        }

        private void pic_ControlSelected_Click(object sender, EventArgs e)
        {
            if (color_Selected_Control.ShowDialog() == DialogResult.OK)
            {
                Color color = color_Selected_Control.Color;
                string str_color = AppUtil.ColorToHex(color);
                AppUtil.Set_Config_Color("Color_Selected_Control", str_color);

                AppUtil.selected_control = color;
                pic_ControlSelected.BackColor = color;

                SetColors();
            }
        }

        private void pic_ProgressBackground_Click(object sender, EventArgs e)
        {
            if (color_Background_Progress.ShowDialog() == DialogResult.OK)
            {
                Color color = color_Background_Progress.Color;
                string str_color = AppUtil.ColorToHex(color);
                AppUtil.Set_Config_Color("Color_Background_Progress", str_color);

                AppUtil.background_progress = color;
                pic_ProgressBackground.BackColor = color;

                SetColors();
            }
        }

        private void pic_ProgressText_Click(object sender, EventArgs e)
        {
            if (color_Text_Progress.ShowDialog() == DialogResult.OK)
            {
                Color color = color_Text_Progress.Color;
                string str_color = AppUtil.ColorToHex(color);
                AppUtil.Set_Config_Color("Color_Text_Progress", str_color);

                AppUtil.text_progress = color;
                pic_ProgressText.BackColor = color;

                SetColors();
            }
        }
    }
}
