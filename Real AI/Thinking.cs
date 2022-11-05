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
            SetColors();

            ThinkingBox.GotFocus += ThinkingBox_GotFocus;
            ThinkingBox.LostFocus += ThinkingBox_LostFocus;
        }

        private void SetColors()
        {
            BackColor = AppUtil.background_window;
            ThinkingBox.BackColor = AppUtil.background_control;
            ThinkingBox.ForeColor = AppUtil.text_control;
        }

        private void ThinkingBox_LostFocus(object sender, EventArgs e)
        {
            if (Brain.Thinking)
            {
                MainForm.ThinkTimer.Start();
            }
        }

        private void ThinkingBox_GotFocus(object sender, EventArgs e)
        {
            MainForm.ThinkTimer.Stop();
        }
    }
}
