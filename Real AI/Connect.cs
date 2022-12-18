using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Real_AI.Util;

namespace Real_AI
{
    public partial class Connect : Form
    {
        #region Variables

        

        #endregion

        #region Constructors

        public Connect()
        {
            FormClosing += Connect_FormClosing;
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void Connect_Load(object sender, EventArgs e)
        {
            try
            {
                SetColors();

                if (MainForm.network == null)
                {
                    MainForm.network = new Thread(NetUtil.Update);
                    MainForm.network.Start();
                }

                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        NetUtil.ConnectedIP = ip.ToString();
                        break;
                    }
                }

                IP_Box.Text = "localhost";
                PortBox.Text = NetUtil.Port.ToString();

                string externalIP = new WebClient().DownloadString("http://ifconfig.me/ip");
                ExternalBox.Text = externalIP.Substring(0, externalIP.Length);
            }
            catch (Exception ex)
            {
                Logger.AddLog("Connect.Connect_Load", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Connect_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MainForm.ResumeTimers();
            }
            catch (Exception ex)
            {
                Logger.AddLog("Connect.Connect_FormClosing", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void SetColors()
        {
            try
            {
                BackColor = AppUtil.background_window;

                lbl_IP.ForeColor = AppUtil.text_window;
                lbl_Port.ForeColor = AppUtil.text_window;
                lbl_External_IP.ForeColor = AppUtil.text_window;

                IP_Box.BackColor = AppUtil.background_control;
                IP_Box.ForeColor = AppUtil.text_control;
                PortBox.BackColor = AppUtil.background_control;
                PortBox.ForeColor = AppUtil.text_control;
                TestButton.BackColor = AppUtil.background_control;
                TestButton.ForeColor = AppUtil.text_control;
                ConnectButton.BackColor = AppUtil.background_control;
                ConnectButton.ForeColor = AppUtil.text_control;
                ExternalBox.BackColor = AppUtil.background_control;
                ExternalBox.ForeColor = AppUtil.text_control;
            }
            catch (Exception ex)
            {
                Logger.AddLog("Connect.SetColors", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IP_Box.Text != "localhost")
                {
                    NetUtil.ConnectedIP = IP_Box.Text;
                }

                NetUtil.Listener = new TcpListener(IPAddress.Parse(NetUtil.ConnectedIP), NetUtil.Port);

                if (IP_Box.Text == "localhost")
                {
                    NetUtil.ConnectedIP = IP_Box.Text;
                }

                NetUtil.Listening = true;
                NetUtil.Start();

                MainForm.input.Enabled = false;
                MainForm.enter.Enabled = false;
                MainForm.input.Text = "[Connected to " + IP_Box.Text + ":" + NetUtil.Port + "]";
                MainForm.menu_Link.Text = "Disconnect Link";
                MainForm.Linked = true;
                MainForm.ListenTimer.Start();

                Close();
            }
            catch (Exception ex)
            {
                Logger.AddLog("Connect.ConnectButton_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(NetUtil.TestConnection());
            }
            catch (Exception ex)
            {
                Logger.AddLog("Connect.TestButton_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void IP_Box_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bool okay = true;

                if (IP_Box.Text != "localhost")
                {
                    string[] array = IP_Box.Text.Split('.');
                    if (array.Length == 4)
                    {
                        foreach (string num in array)
                        {
                            if (int.TryParse(num, out _))
                            {
                                int number = int.Parse(num);
                                if (number < 0 || number > 255)
                                {
                                    okay = false;
                                    break;
                                }
                            }
                            else
                            {
                                okay = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        okay = false;
                    }
                }

                if (okay)
                {
                    IP_Box.ForeColor = AppUtil.text_control;
                }
                else
                {
                    IP_Box.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Connect.IP_Box_TextChanged", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void PortBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(PortBox.Text, out _))
                {
                    PortBox.ForeColor = AppUtil.text_control;
                    NetUtil.Port = int.Parse(PortBox.Text);
                }
                else
                {
                    PortBox.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Connect.PortBox_TextChanged", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        #endregion
    }
}
