using System;
using System.Drawing;
using System.IO;
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
            try
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

                chk_Thinking.Checked = Brain.Thinking;
                chk_LearnFromThinking.Checked = Brain.LearnFromThinking;
                chk_Initiate.Checked = Brain.Initiate;
                chk_TTS.Checked = MainForm.tts;
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
            catch (Exception ex)
            {
                Logger.AddLog("Options.Options_Load", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MainForm.ResumeTimers();
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Options_FormClosing", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Tabs_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Logger.AddLog("Options.Tabs_DrawItem", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Logger.AddLog("Options.ComboBox_DrawItem", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void SetColors()
        {
            try
            {
                color_Background_Window.Color = AppUtil.background_window;
                color_Text_Window.Color = AppUtil.text_window;
                color_Background_Control.Color = AppUtil.background_control;
                color_Text_Control.Color = AppUtil.text_control;
                color_Highlight_Control.Color = AppUtil.highlight_control;
                color_Selected_Control.Color = AppUtil.selected_control;
                color_Background_Progress.Color = AppUtil.background_progress;
                color_Text_Progress.Color = AppUtil.text_progress;

                BackColor = AppUtil.background_window;
                ForeColor = AppUtil.text_window;

                btn_ExportTheme.BackColor = AppUtil.background_control;
                btn_ExportTheme.ForeColor = AppUtil.text_control;
                btn_ImportTheme.BackColor = AppUtil.background_control;
                btn_ImportTheme.ForeColor = AppUtil.text_control;

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
                lbl_AttentionSpan.ForeColor = AppUtil.text_window;
                AttentionSpan.ForeColor = AppUtil.text_window;
                lbl_Voice.ForeColor = AppUtil.text_window;
                lbl_WindowBackground.ForeColor = AppUtil.text_window;
                lbl_WindowText.ForeColor = AppUtil.text_window;

                chk_Thinking.BackColor = AppUtil.background_window;
                chk_Thinking.ForeColor = AppUtil.text_control;

                chk_LearnFromThinking.BackColor = AppUtil.background_window;
                chk_LearnFromThinking.ForeColor = AppUtil.text_control;

                chk_Initiate.BackColor = AppUtil.background_window;
                chk_Initiate.ForeColor = AppUtil.text_control;

                chk_InputResponding.BackColor = AppUtil.background_window;
                chk_InputResponding.ForeColor = AppUtil.text_control;

                chk_TopicResponding.BackColor = AppUtil.background_window;
                chk_TopicResponding.ForeColor = AppUtil.text_control;

                chk_ProceduralResponding.BackColor = AppUtil.background_window;
                chk_ProceduralResponding.ForeColor = AppUtil.text_control;

                Voices.BackColor = AppUtil.background_control;
                Voices.ForeColor = AppUtil.text_control;

                chk_TTS.BackColor = AppUtil.background_window;
                chk_TTS.ForeColor = AppUtil.text_control;

                MainForm.SetColors();
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.SetColors", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void ThinkSpeed_Bar_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                ThinkTimer.Stop();
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.ThinkSpeed_Bar_MouseUp", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void ThinkSpeed_Bar_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                ThinkTimer.Start();
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.ThinkSpeed_Bar_MouseDown", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void ThinkTimer_Tick(object sender, EventArgs e)
        {
            try
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
                AppUtil.Set_Config("ThinkSpeed", ThinkSpeed_Bar.Value.ToString());
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.ThinkTimer_Tick", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Attention_Bar_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                AttentionTimer.Stop();
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Attention_Bar_MouseUp", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Attention_Bar_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                AttentionTimer.Start();
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Attention_Bar_MouseDown", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void AttentionTimer_Tick(object sender, EventArgs e)
        {
            try
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
                AppUtil.Set_Config("AttentionSpan", Attention_Bar.Value.ToString());
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.AttentionTimer_Tick", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Chk_LearnFromThinking_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Brain.LearnFromThinking = chk_LearnFromThinking.Checked;
                AppUtil.Set_Config("LearnFromThinking", chk_LearnFromThinking.Checked.ToString());
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Chk_LearnFromThinking_CheckedChanged", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Chk_Initiate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Brain.Initiate = chk_Initiate.Checked;
                AppUtil.Set_Config("Initiate", Brain.Initiate.ToString());

                if (Brain.Initiate)
                {
                    MainForm.AttentionTimer.Start();
                }
                else
                {
                    MainForm.AttentionTimer.Stop();
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Chk_Initiate_CheckedChanged", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Chk_Thinking_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Brain.Thinking = chk_Thinking.Checked;
                AppUtil.Set_Config("Thinking", Brain.Thinking.ToString());

                if (!Brain.Thinking)
                {
                    foreach (Thread thread in MainForm.threads)
                    {
                        thread.Abort();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Chk_Thinking_CheckedChanged", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Chk_TTS_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                MainForm.tts = chk_TTS.Checked;
                AppUtil.Set_Config("TTS", MainForm.tts.ToString());
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Chk_TTS_CheckedChanged", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Chk_InputResponding_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                MainForm.InputResponding = chk_InputResponding.Checked;
                AppUtil.Set_Config("InputResponding", MainForm.InputResponding.ToString());
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Chk_InputResponding_CheckedChanged", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Chk_TopicResponding_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                MainForm.TopicResponding = chk_TopicResponding.Checked;
                AppUtil.Set_Config("TopicResponding", MainForm.TopicResponding.ToString());
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Chk_TopicResponding_CheckedChanged", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Chk_ProceduralResponding_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                MainForm.ProceduralResponding = chk_ProceduralResponding.Checked;
                AppUtil.Set_Config("ProceduralResponding", MainForm.ProceduralResponding.ToString());
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Chk_ProceduralResponding_CheckedChanged", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Voices_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string voice = ((ComboBox)sender).SelectedItem.ToString();
                MainForm.tts_voice = voice;
                MainForm.synthesizer.SelectVoice(voice);
                AppUtil.Set_Config("TTS_Voice", voice);
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Voices_SelectedIndexChanged", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Pic_WindowBackground_Click(object sender, EventArgs e)
        {
            try
            {
                if (color_Background_Window.ShowDialog() == DialogResult.OK)
                {
                    Color color = color_Background_Window.Color;
                    string str_color = AppUtil.ColorToHex(color);
                    AppUtil.Set_Color("Color_Background_Window", str_color);

                    AppUtil.background_window = color;
                    pic_WindowBackground.BackColor = color;

                    SetColors();
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Pic_WindowBackground_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Pic_WindowText_Click(object sender, EventArgs e)
        {
            try
            {
                if (color_Text_Window.ShowDialog() == DialogResult.OK)
                {
                    Color color = color_Text_Window.Color;
                    string str_color = AppUtil.ColorToHex(color);
                    AppUtil.Set_Color("Color_Text_Window", str_color);

                    AppUtil.text_window = color;
                    pic_WindowText.BackColor = color;

                    SetColors();
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Pic_WindowText_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Pic_ControlBackground_Click(object sender, EventArgs e)
        {
            try
            {
                if (color_Background_Control.ShowDialog() == DialogResult.OK)
                {
                    Color color = color_Background_Control.Color;
                    string str_color = AppUtil.ColorToHex(color);
                    AppUtil.Set_Color("Color_Background_Control", str_color);

                    AppUtil.background_control = color;
                    pic_ControlBackground.BackColor = color;

                    SetColors();
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Pic_ControlBackground_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Pic_ControlText_Click(object sender, EventArgs e)
        {
            try
            {
                if (color_Text_Control.ShowDialog() == DialogResult.OK)
                {
                    Color color = color_Text_Control.Color;
                    string str_color = AppUtil.ColorToHex(color);
                    AppUtil.Set_Color("Color_Text_Control", str_color);

                    AppUtil.text_control = color;
                    pic_ControlText.BackColor = color;

                    SetColors();
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Pic_ControlText_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Pic_ControlHighlight_Click(object sender, EventArgs e)
        {
            try
            {
                if (color_Highlight_Control.ShowDialog() == DialogResult.OK)
                {
                    Color color = color_Highlight_Control.Color;
                    string str_color = AppUtil.ColorToHex(color);
                    AppUtil.Set_Color("Color_Highlight_Control", str_color);

                    AppUtil.highlight_control = color;
                    pic_ControlHighlight.BackColor = color;

                    SetColors();
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Pic_ControlHighlight_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Pic_ControlSelected_Click(object sender, EventArgs e)
        {
            try
            {
                if (color_Selected_Control.ShowDialog() == DialogResult.OK)
                {
                    Color color = color_Selected_Control.Color;
                    string str_color = AppUtil.ColorToHex(color);
                    AppUtil.Set_Color("Color_Selected_Control", str_color);

                    AppUtil.selected_control = color;
                    pic_ControlSelected.BackColor = color;

                    SetColors();
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Pic_ControlSelected_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Pic_ProgressBackground_Click(object sender, EventArgs e)
        {
            try
            {
                if (color_Background_Progress.ShowDialog() == DialogResult.OK)
                {
                    Color color = color_Background_Progress.Color;
                    string str_color = AppUtil.ColorToHex(color);
                    AppUtil.Set_Color("Color_Background_Progress", str_color);

                    AppUtil.background_progress = color;
                    pic_ProgressBackground.BackColor = color;

                    SetColors();
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Pic_ProgressBackground_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Pic_ProgressText_Click(object sender, EventArgs e)
        {
            try
            {
                if (color_Text_Progress.ShowDialog() == DialogResult.OK)
                {
                    Color color = color_Text_Progress.Color;
                    string str_color = AppUtil.ColorToHex(color);
                    AppUtil.Set_Color("Color_Text_Progress", str_color);

                    AppUtil.text_progress = color;
                    pic_ProgressText.BackColor = color;

                    SetColors();
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Pic_ProgressText_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Btn_ImportTheme_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Filter = "Colors (*.colors)|*.colors",
                    DefaultExt = "*.colors",
                    InitialDirectory = Environment.CurrentDirectory + @"\Themes\"
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MainForm.Colors = dialog.FileName;
                    AppUtil.Set_Config("Colors", Path.GetFileName(MainForm.Colors));
                    AppUtil.Update_Colors();
                    SetColors();
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Btn_ImportTheme_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Btn_ExportTheme_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog
                {
                    Filter = "Colors (*.colors)|*.colors",
                    DefaultExt = "*.colors",
                    InitialDirectory = Path.Combine(Environment.CurrentDirectory, "Themes")
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MainForm.Colors = dialog.FileName;

                    string[] lines =
                    {
                        "Color_Background_Window=" + AppUtil.ColorToHex(AppUtil.background_window),
                        "Color_Background_Control=" + AppUtil.ColorToHex(AppUtil.background_control),
                        "Color_Background_Progress=" + AppUtil.ColorToHex(AppUtil.background_progress),
                        "Color_Text_Window=" + AppUtil.ColorToHex(AppUtil.text_window),
                        "Color_Text_Control=" + AppUtil.ColorToHex(AppUtil.text_control),
                        "Color_Text_Progress=" + AppUtil.ColorToHex(AppUtil.text_progress),
                        "Color_Highlight_Control=" + AppUtil.ColorToHex(AppUtil.highlight_control),
                        "Color_Selected_Control=" + AppUtil.ColorToHex(AppUtil.selected_control)
                    };

                    File.WriteAllLines(MainForm.Colors, lines);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Options.Btn_ExportTheme_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }
    }
}
