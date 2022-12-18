using System;
using System.Windows.Forms;
using Real_AI.Util;

namespace Real_AI
{
    public partial class Thinking : Form
    {
        public Thinking()
        {
            InitializeComponent();
        }

        private void Thinking_Load(object sender, EventArgs e)
        {
            try
            {
                SetColors();

                ThinkingBox.GotFocus += ThinkingBox_GotFocus;
                ThinkingBox.LostFocus += ThinkingBox_LostFocus;
            }
            catch (Exception ex)
            {
                Logger.AddLog("Thinking.Thinking_Load", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void SetColors()
        {
            try
            {
                BackColor = AppUtil.background_window;
                ThinkingBox.BackColor = AppUtil.background_control;
                ThinkingBox.ForeColor = AppUtil.text_control;
            }
            catch (Exception ex)
            {
                Logger.AddLog("Thinking.SetColors", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void ThinkingBox_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (Brain.Thinking)
                {
                    MainForm.ThinkTimer.Start();
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Thinking.ThinkingBox_LostFocus", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void ThinkingBox_GotFocus(object sender, EventArgs e)
        {
            try
            {
                MainForm.ThinkTimer.Stop();
            }
            catch (Exception ex)
            {
                Logger.AddLog("Thinking.ThinkingBox_GotFocus", ex.Source, ex.Message, ex.StackTrace);
            }
        }
    }
}
