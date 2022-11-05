using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Collections.Generic;
using Real_AI.Util;

namespace Real_AI
{
    public partial class MainForm : Form
    {
        #region Variables

        Options options = new Options();
        BrainEditor editor = new BrainEditor();
        Merge merge = new Merge();
        Thinking thinking = new Thinking();
        Spelling spelling = new Spelling();
        Connect connect = new Connect();
        ReadFile read = new ReadFile();

        public static SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        public static bool tts;
        public static string tts_voice;

        public static string ID;
        public static bool Linked;
        public static Thread network;

        public static List<Thread> threads = new List<Thread>();
        public static System.Windows.Forms.Timer ThinkTimer = new System.Windows.Forms.Timer();
        public static System.Windows.Forms.Timer AttentionTimer = new System.Windows.Forms.Timer();
        public static System.Windows.Forms.Timer ListenTimer = new System.Windows.Forms.Timer();

        string ElapsedTime;
        DateTime StartTime_Elapsed;
        CancellationTokenSource ElapsedTokenSource;
        Task ElapsedTask;

        int previous_progress;
        DateTime StartTime_Remaining;
        CancellationTokenSource RemainingTokenSource;
        Task RemainingTask;
        public static HashSet<float> intervals = new HashSet<float>();

        public static Form mainForm;
        public static TextBox input;
        public static RichTextBox output;
        public static Button enter;
        public static Button encourage;
        public static Button discourage;

        public static MenuStrip mainMenu;
        public static ToolStripMenuItem menu_NewSession;
        public static ToolStripMenuItem menu_CheckUpdate;
        public static ToolStripMenuItem menu_Link;
        public static ToolStripMenuItem menu_Exit;
        public static ToolStripMenuItem menu_NewBrain;
        public static ToolStripMenuItem menu_LoadBrain;
        public static ToolStripMenuItem menu_EditBrain;
        public static ToolStripMenuItem menu_EditOptions;
        public static ToolStripMenuItem menu_ViewThinking;
        public static ToolStripMenuItem menu_WipeMemory;
        public static ToolStripMenuItem menu_MergeBrains;
        public static ToolStripMenuItem menu_FixSpelling;
        public static ToolStripMenuItem menu_ReadFile;

        public static TextProgressBar progressMain;
        public static TextProgressBar progressDetail;
        public static Label label_Output;
        public static Label label_Input;
        public static Label label_ElapsedTime;
        public static Label label_RemainingTime;
        public static string BrainFile;
        public static string Config = Environment.CurrentDirectory + @"\config.ini";
        public static string BrainsDir = Environment.CurrentDirectory + @"\Brains\";
        public static string LogPath = Environment.CurrentDirectory + @"\crashlog.txt";
        public static Version CurrentVersion;
        public static Version LatestVersion;
        public static bool CheckForUpdates;

        public static string CurrentDate;
        public static string HistoryDir = Environment.CurrentDirectory + @"\History\";
        public static string Current_HistoryDir;
        public static string HistoryFile;
        public List<string> History = new List<string>();

        public List<string> Thoughts = new List<string>();

        public static bool Crash;
        public static List<string> Log = new List<string>();

        #endregion

        #region Constructors

        public MainForm()
        {
            Opacity = 0;
            FormClosing += MainForm_FormClosing;

            InitializeComponent();

            menu.Renderer = new MyRenderer();
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }

        #endregion

        #region Methods

        private void Init()
        {
            mainForm = this;

            input = InputBox;
            output = OutputBox;
            enter = EnterButton;
            encourage = EncourageButton;
            discourage = DiscourageButton;
            label_Output = lbl_Output;
            label_Input = lbl_Input;
            label_ElapsedTime = lbl_ElapsedTime;
            label_RemainingTime = lbl_RemainingTime;

            mainMenu = menu;
            menu_NewSession = NewSession;
            menu_Link = Link;
            menu_Exit = Exit;
            menu_NewBrain = NewBrain;
            menu_LoadBrain = LoadBrain;
            menu_EditBrain = EditBrain;
            menu_EditOptions = EditOptions;
            menu_ViewThinking = ViewThinking;
            menu_WipeMemory = WipeMemory;
            menu_MergeBrains = MergeBrains;
            menu_FixSpelling = FixSpelling;
            menu_ReadFile = ReadFile;

            lbl_ElapsedTime.Text = "";
            lbl_RemainingTime.Text = "";

            progressMain = ProgressBar_Main;
            progressMain.VisualMode = ProgressBarDisplayMode.TextAndPercentage;

            progressDetail = ProgressBar_Detail;
            progressDetail.VisualMode = ProgressBarDisplayMode.Percentage;

            if (!Directory.Exists(BrainsDir))
            {
                Directory.CreateDirectory(BrainsDir);
            }

            if (!Directory.Exists(HistoryDir))
            {
                Directory.CreateDirectory(HistoryDir);
            }

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            CurrentVersion = Version.Parse(fvi.FileVersion);

            if (!File.Exists(Config))
            {
                string[] lines =
                {
                    "Version=" + CurrentVersion,
                    "ID=" + Guid.NewGuid(),
                    "AutoCheckUpdate=False",
                    "LastBrain=Nothing",
                    "ThinkSpeed=1000",
                    "Thinking=True",
                    "LearnFromThinking=False",
                    "AttentionSpan=7",
                    "Initiate=True",
                    "TTS=False",
                    "Color_Background_Window=" + AppUtil.ColorToHex(BackColor),
                    "Color_Background_Control=" + AppUtil.ColorToHex(OutputBox.BackColor),
                    "Color_Background_Progress=" + AppUtil.ColorToHex(Color.DarkTurquoise),
                    "Color_Text_Window=" + AppUtil.ColorToHex(ForeColor),
                    "Color_Text_Control=" + AppUtil.ColorToHex(OutputBox.ForeColor),
                    "Color_Text_Progress=" + AppUtil.ColorToHex(Color.Black),
                    "Color_Highlight_Control=" + AppUtil.ColorToHex(Color.DarkTurquoise),
                    "Color_Selected_Control=" + AppUtil.ColorToHex(Color.DarkGray)
                };

                File.WriteAllLines(Config, lines);
            }
            else
            {
                AppUtil.Set_Version(CurrentVersion.ToString());
                CheckForUpdates = AppUtil.CheckForUpdates();

                if (string.IsNullOrEmpty(AppUtil.Get_Config_Color("Color_Background_Window")))
                {
                    AppUtil.Set_Config_Color("Color_Background_Window", AppUtil.ColorToHex(BackColor));
                }

                if (string.IsNullOrEmpty(AppUtil.Get_Config_Color("Color_Background_Control")))
                {
                    AppUtil.Set_Config_Color("Color_Background_Control", AppUtil.ColorToHex(OutputBox.BackColor));
                }

                if (string.IsNullOrEmpty(AppUtil.Get_Config_Color("Color_Background_Progress")))
                {
                    AppUtil.Set_Config_Color("Color_Background_Progress", AppUtil.ColorToHex(Color.DarkTurquoise));
                }

                if (string.IsNullOrEmpty(AppUtil.Get_Config_Color("Color_Text_Window")))
                {
                    AppUtil.Set_Config_Color("Color_Text_Window", AppUtil.ColorToHex(ForeColor));
                }

                if (string.IsNullOrEmpty(AppUtil.Get_Config_Color("Color_Text_Control")))
                {
                    AppUtil.Set_Config_Color("Color_Text_Control", AppUtil.ColorToHex(OutputBox.ForeColor));
                }

                if (string.IsNullOrEmpty(AppUtil.Get_Config_Color("Color_Text_Progress")))
                {
                    AppUtil.Set_Config_Color("Color_Text_Progress", AppUtil.ColorToHex(Color.Black));
                }

                if (string.IsNullOrEmpty(AppUtil.Get_Config_Color("Color_Highlight_Control")))
                {
                    AppUtil.Set_Config_Color("Color_Highlight_Control", AppUtil.ColorToHex(Color.DarkTurquoise));
                }

                if (string.IsNullOrEmpty(AppUtil.Get_Config_Color("Color_Selected_Control")))
                {
                    AppUtil.Set_Config_Color("Color_Selected_Control", AppUtil.ColorToHex(Color.DarkGray));
                }
            }

            if (File.Exists(LogPath))
            {
                File.Delete(LogPath);
            }
        }

        private void DisplayTips()
        {
            List<string> tips = new List<string>();
            tips.Add("Here are some tips for teaching the AI:");
            tips.Add("");
            tips.Add("1. The AI learns from observing how you respond to what it says... " +
                    "so, if it says \"Hello.\" and you say \"How are you?\" it will learn that \"How are you?\" " +
                    "is a possible response to \"Hello.\". If you say something it has never seen before, it will " +
                    "repeat it to see how -you- would respond to it. Learning by imitation, like a young child, " +
                    "is not the only way it learns as you will soon discover.");
            tips.Add("");
            tips.Add("2. It will generate stuff that sounds nonsensical early on... this is part of the learning process, " +
                    "similar to the way children phrase things in ways that don't quite make sense early on.");
            tips.Add("");
            tips.Add("3. Limit your response to a single sentence or question.");
            tips.Add("");
            tips.Add("4. Use complete sentences when responding. Start with a capital letter and end with a punctuation mark.");
            tips.Add("");
            tips.Add("5. Avoid contractions (use \"it is\" instead of \"it's\").");
            tips.Add("");
            tips.Add("6. The AI cannot see/hear/taste/smell/feel any things you refer to, so it can never have any contextual " +
                    "understanding of what exactly the thing is (the way you understand it). This means it'll " +
                    "never understand you trying to reference it (or yourself) directly, as it can never have a concept of " +
                    "anything external being something different from it without spatial recognition gained from sight/touch/sound.");
            tips.Add("");
            tips.Add("7. In general... keep it simple. The simpler you speak to it, the better it learns.");
            tips.Add("");
            tips.Add("8. For help, check Discord: https://discord.gg/3yJ8rce");
            tips.Add("");
            tips.Add("To get started, create a new brain or load an existing one.");

            StringBuilder sb = new StringBuilder();
            foreach (string line in tips)
            {
                sb.AppendLine(line);
            }

            OutputBox.Text = sb.ToString();
        }

        private bool StartNewSession()
        {
            bool reset = false;

            History = AppUtil.GetHistory();
            if (History.Count > 0)
            {
                if (History[History.Count - 1] != "[new session]")
                {
                    reset = true;
                }
            }

            if (reset)
            {
                Brain.LastResponse = "";
                Brain.LastThought = "";
                Brain.CleanInput = "";
                Brain.Topics = null;
                Brain.WordArray = null;
                Brain.WordArray_Thinking = null;
                Thoughts.Clear();

                History.Add("[new session]");
                AppUtil.SaveHistory(History);

                if (InputBox.InvokeRequired)
                {
                    InputBox.Invoke((MethodInvoker)delegate
                    {
                        InputBox.Text = "";
                    });
                }
                else
                {
                    InputBox.Text = "";
                }

                if (progressMain.InvokeRequired)
                {
                    progressMain.Invoke((MethodInvoker)delegate
                    {
                        progressMain.CustomText = "New Session Started";
                        progressMain.Value = 100;
                    });
                }
                else
                {
                    progressMain.CustomText = "New Session Started";
                    progressMain.Value = 100;
                }

                Display();
                return true;
            }

            return false;
        }

        private void AddToHistory(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                History = AppUtil.GetHistory();
                History.Add(message);
                AppUtil.SaveHistory(History);

                Display();
            }
        }

        private void Respond()
        {
            AddToHistory("[" + DateTime.Now.ToString("HH:mm:ss") + "] You: " + Brain.Input);

            string response = Brain.Respond(false);
            if (!string.IsNullOrEmpty(response))
            {
                AddToHistory("[" + DateTime.Now.ToString("HH:mm:ss") + "] AI: " + response);
            }

            ElapsedTokenSource.Cancel();
            ElapsedTokenSource.Dispose();
            ElapsedTask = null;

            RemainingTokenSource.Cancel();
            RemainingTokenSource.Dispose();
            RemainingTask = null;

            if (lbl_RemainingTime.InvokeRequired)
            {
                lbl_RemainingTime.Invoke((MethodInvoker)delegate
                {
                    lbl_RemainingTime.Text = "";
                });
            }
            else
            {
                lbl_RemainingTime.Text = "";
            }
            
            if (InputBox.InvokeRequired)
            {
                InputBox.Invoke((MethodInvoker)delegate
                {
                    InputBox.Text = "";
                    InputBox.Enabled = true;
                    InputBox.Focus();
                });
            }
            else
            {
                InputBox.Text = "";
                InputBox.Enabled = true;
                InputBox.Focus();
            }

            if (EnterButton.InvokeRequired)
            {
                EnterButton.Invoke((MethodInvoker)delegate
                {
                    EnterButton.Enabled = true;
                });
            }
            else
            {
                EnterButton.Enabled = true;
            }

            if (tts)
            {
                synthesizer.SpeakAsync(response);
            }

            if (Crash)
            {
                File.WriteAllLines(LogPath, Log);
                Application.Exit();
            }
        }

        private void Encourage()
        {
            bool encourage = false;
            string message = "";

            History = AppUtil.GetHistory();
            if (History.Count > 0)
            {
                if (History[History.Count - 1] != "[encouraged]")
                {
                    encourage = true;
                }

                if (!string.IsNullOrEmpty(Brain.LastResponse) &&
                    History[History.Count - 1].Contains(Brain.LastResponse))
                {
                    message = Brain.LastResponse;
                }
                else if (!string.IsNullOrEmpty(Brain.LastThought) && 
                         History[History.Count - 1].Contains(Brain.LastThought))
                {
                    message = Brain.LastThought;
                }
            }

            if (encourage &&
                !string.IsNullOrEmpty(message))
            {
                if (ProgressBar_Detail.InvokeRequired)
                {
                    ProgressBar_Detail.Invoke((MethodInvoker)delegate
                    {
                        ProgressBar_Detail.Value = 0;
                        ProgressBar_Main.Value = 0;
                    });
                }
                else
                {
                    ProgressBar_Detail.Value = 0;
                    ProgressBar_Main.Value = 0;
                }

                if (Brain.Encourage(message))
                {
                    History.Add("[encouraged]");
                    AppUtil.SaveHistory(History);
                    Display();

                    if (progressMain.InvokeRequired)
                    {
                        progressMain.Invoke((MethodInvoker)delegate
                        {
                            progressMain.CustomText = "Encouraged";
                            progressMain.Value = 100;
                        });
                    }
                    else
                    {
                        progressMain.CustomText = "Encouraged";
                        progressMain.Value = 100;
                    }
                }
            }

            ElapsedTokenSource.Cancel();
            ElapsedTokenSource.Dispose();
            ElapsedTask = null;

            RemainingTokenSource.Cancel();
            RemainingTokenSource.Dispose();
            RemainingTask = null;

            if (lbl_RemainingTime.InvokeRequired)
            {
                lbl_RemainingTime.Invoke((MethodInvoker)delegate
                {
                    lbl_RemainingTime.Text = "";
                });
            }
            else
            {
                lbl_RemainingTime.Text = "";
            }

            if (EncourageButton.InvokeRequired)
            {
                EncourageButton.Invoke((MethodInvoker)delegate
                {
                    EncourageButton.Enabled = true;
                });
            }
            else
            {
                EncourageButton.Enabled = true;
            }

            if (Crash)
            {
                File.WriteAllLines(LogPath, Log);
                Application.Exit();
            }
        }

        private void Discourage()
        {
            bool discourage = false;
            string message = "";

            History = AppUtil.GetHistory();
            if (History.Count > 0)
            {
                if (History[History.Count - 1] != "[new session]")
                {
                    discourage = true;
                }

                if (!string.IsNullOrEmpty(Brain.LastResponse) &&
                    History[History.Count - 1].Contains(Brain.LastResponse))
                {
                    message = Brain.LastResponse;
                }
                else if (!string.IsNullOrEmpty(Brain.LastThought) &&
                         History[History.Count - 1].Contains(Brain.LastThought))
                {
                    message = Brain.LastThought;
                }
            }

            if (discourage &&
                !string.IsNullOrEmpty(message))
            {
                if (ProgressBar_Detail.InvokeRequired)
                {
                    ProgressBar_Detail.Invoke((MethodInvoker)delegate
                    {
                        ProgressBar_Detail.Value = 0;
                        ProgressBar_Main.Value = 0;
                    });
                }
                else
                {
                    ProgressBar_Detail.Value = 0;
                    ProgressBar_Main.Value = 0;
                }

                if (Brain.Discourage(message))
                {
                    History.Add("[discouraged]");
                    AppUtil.SaveHistory(History);
                    Display();

                    if (progressMain.InvokeRequired)
                    {
                        progressMain.Invoke((MethodInvoker)delegate
                        {
                            progressMain.CustomText = "Discouraged";
                            progressMain.Value = 100;
                        });
                    }
                    else
                    {
                        progressMain.CustomText = "Discouraged";
                        progressMain.Value = 100;
                    }

                    StartNewSession();
                }
            }

            ElapsedTokenSource.Cancel();
            ElapsedTokenSource.Dispose();
            ElapsedTask = null;

            RemainingTokenSource.Cancel();
            RemainingTokenSource.Dispose();
            RemainingTask = null;

            if (lbl_RemainingTime.InvokeRequired)
            {
                lbl_RemainingTime.Invoke((MethodInvoker)delegate
                {
                    lbl_RemainingTime.Text = "";
                });
            }
            else
            {
                lbl_RemainingTime.Text = "";
            }

            if (DiscourageButton.InvokeRequired)
            {
                DiscourageButton.Invoke((MethodInvoker)delegate
                {
                    DiscourageButton.Enabled = true;
                });
            }
            else
            {
                DiscourageButton.Enabled = true;
            }

            if (Crash)
            {
                File.WriteAllLines(LogPath, Log);
                Application.Exit();
            }
        }

        private void Display()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string line in History)
            {
                sb.AppendLine(line);
            }

            if (OutputBox.InvokeRequired)
            {
                OutputBox.Invoke((MethodInvoker)delegate
                {
                    OutputBox.Text = sb.ToString();
                    OutputBox.SelectionStart = OutputBox.Text.Length;
                    OutputBox.ScrollToCaret();
                });
            }
            else
            {
                OutputBox.Text = sb.ToString();
                OutputBox.SelectionStart = OutputBox.Text.Length;
                OutputBox.ScrollToCaret();
            }
        }

        private void Thinking()
        {
            if (!string.IsNullOrEmpty(BrainFile) &&
                Brain.Thinking)
            {
                string response = Brain.Think();
                if (!string.IsNullOrEmpty(response))
                {
                    Thoughts.Add("[" + DateTime.Now.ToString("HH:mm:ss") + "] AI: " + response);
                }

                if (Thoughts.Count > 200)
                {
                    Thoughts.RemoveRange(0, Thoughts.Count - 200);
                }

                DisplayThinking();

                if (Crash)
                {
                    File.WriteAllLines(LogPath, Log);
                    Application.Exit();
                }
            }
        }

        private void DisplayThinking()
        {
            if (thinking != null)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < Thoughts.Count; i++)
                {
                    sb.AppendLine(Thoughts[i]);
                }

                if (thinking.InvokeRequired)
                {
                    thinking.ThinkingBox.Invoke((MethodInvoker)delegate
                    {
                        thinking.ThinkingBox.Text = sb.ToString();
                        thinking.ThinkingBox.SelectionStart = thinking.ThinkingBox.Text.Length;
                        thinking.ThinkingBox.ScrollToCaret();
                    });
                }
            }
        }

        public static void ResumeTimers()
        {
            if (Brain.Thinking)
            {
                ThinkTimer.Start();
            }

            if (Brain.Initiate)
            {
                AttentionTimer.Start();
            }
        }

        public static void StopTimers()
        {
            ThinkTimer.Stop();
            AttentionTimer.Stop();
        }

        public static void SetColors()
        {
            mainForm.BackColor = AppUtil.background_window;
            mainForm.ForeColor = AppUtil.text_window;
            label_Input.ForeColor = AppUtil.text_window;
            label_Output.ForeColor = AppUtil.text_window;
            label_ElapsedTime.ForeColor = AppUtil.text_window;
            label_RemainingTime.ForeColor = AppUtil.text_window;
            output.BackColor = AppUtil.background_control;
            output.ForeColor = AppUtil.text_control;
            output.SelectionBackColor = AppUtil.highlight_control;
            output.SelectionColor = AppUtil.selected_control;
            input.BackColor = AppUtil.background_control;
            input.ForeColor = AppUtil.text_control;
            enter.BackColor = AppUtil.background_control;
            enter.ForeColor = AppUtil.text_control;
            encourage.BackColor = AppUtil.background_control;
            encourage.ForeColor = AppUtil.text_control;
            discourage.BackColor = AppUtil.background_control;
            discourage.ForeColor = AppUtil.text_control;
            progressMain.BackColor = AppUtil.background_progress;
            progressMain.ForeColor = AppUtil.text_progress;
            progressMain.ProgressColor = AppUtil.background_progress;
            progressMain.TextColor = AppUtil.text_progress;
            progressDetail.BackColor = AppUtil.background_progress;
            progressDetail.ForeColor = AppUtil.text_progress;
            progressDetail.ProgressColor = AppUtil.background_progress;
            progressDetail.TextColor = AppUtil.text_progress;
            mainMenu.BackColor = AppUtil.background_control;
            mainMenu.ForeColor = AppUtil.text_control;
            menu_NewSession.BackColor = AppUtil.background_control;
            menu_NewSession.ForeColor = AppUtil.text_control;
            menu_Link.BackColor = AppUtil.background_control;
            menu_Link.ForeColor = AppUtil.text_control;
            menu_Exit.BackColor = AppUtil.background_control;
            menu_Exit.ForeColor = AppUtil.text_control;
            menu_NewBrain.BackColor = AppUtil.background_control;
            menu_NewBrain.ForeColor = AppUtil.text_control;
            menu_LoadBrain.BackColor = AppUtil.background_control;
            menu_LoadBrain.ForeColor = AppUtil.text_control;
            menu_EditBrain.BackColor = AppUtil.background_control;
            menu_EditBrain.ForeColor = AppUtil.text_control;
            menu_EditOptions.BackColor = AppUtil.background_control;
            menu_EditOptions.ForeColor = AppUtil.text_control;
            menu_ViewThinking.BackColor = AppUtil.background_control;
            menu_ViewThinking.ForeColor = AppUtil.text_control;
            menu_WipeMemory.BackColor = AppUtil.background_control;
            menu_WipeMemory.ForeColor = AppUtil.text_control;
            menu_MergeBrains.BackColor = AppUtil.background_control;
            menu_MergeBrains.ForeColor = AppUtil.text_control;
            menu_FixSpelling.BackColor = AppUtil.background_control;
            menu_FixSpelling.ForeColor = AppUtil.text_control;
            menu_ReadFile.BackColor = AppUtil.background_control;
            menu_ReadFile.ForeColor = AppUtil.text_control;
        }

        #region Events

        private void MainForm_Load(object sender, EventArgs e)
        {
            Init();

            synthesizer.SetOutputToDefaultAudioDevice();
            
            string lastBrain = AppUtil.Get_LastBrain();
            if (!string.IsNullOrEmpty(lastBrain) &&
                File.Exists(lastBrain))
            {
                BrainFile = lastBrain;
                string BrainName = Path.GetFileNameWithoutExtension(BrainFile);

                SqlUtil.BrainConn = SqlUtil.GetSqlConnection(BrainFile);

                progressMain.CustomText = "Connected to \"" + BrainName + "\" brain";
                progressMain.Value = 100;
                progressDetail.Value = 100;

                Current_HistoryDir = HistoryDir + BrainName;
                if (!Directory.Exists(Current_HistoryDir))
                {
                    Directory.CreateDirectory(Current_HistoryDir);
                }

                StartNewSession();
            }
            else
            {
                progressMain.CustomText = "Not connected to a brain";
                progressMain.Value = 100;
                progressDetail.Value = 100;

                DisplayTips();
            }

            Opacity = 100;

            ID = AppUtil.Get_ID();
            if (string.IsNullOrEmpty(ID))
            {
                ID = Guid.NewGuid().ToString();
                AppUtil.Set_ID(ID);
            }

            ListenTimer.Interval = 1000;
            ListenTimer.Tick += ListenTimer_Tick;

            ThinkTimer.Interval = AppUtil.Get_ThinkSpeed();
            ThinkTimer.Tick += ThinkTimer_Tick;
            ThinkTimer.Start();

            lbl_ElapsedTime.Text = "";
            lbl_RemainingTime.Text = "";

            OutputBox.LinkClicked += OutputBox_LinkClicked;

            Brain.Thinking = AppUtil.Get_Thinking();
            Brain.LearnFromThinking = AppUtil.Get_LearnFromThinking();
            Brain.Initiate = AppUtil.Get_Initiate();
            tts = AppUtil.Get_TTS();
            tts_voice = AppUtil.Get_TTS_Voice();
            AppUtil.Update_Colors();

            AttentionTimer.Interval = AppUtil.Get_AttentionSpan() * 1000;
            AttentionTimer.Tick += AttentionTimer_Tick;

            if (Brain.Initiate)
            {
                AttentionTimer.Start();
            }

            if (!string.IsNullOrEmpty(tts_voice))
            {
                synthesizer.SelectVoice(tts_voice);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            synthesizer.Dispose();

            StopTimers();

            foreach (Thread thread in threads)
            {
                thread.Abort();
            }
            threads.Clear();

            NetUtil.Stop();

            if (network != null)
            {
                network.Abort();
            }
        }

        private void OutputBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void InputBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(InputBox.Text) &&
                Brain.Initiate)
            {
                AttentionTimer.Start();
            }
            else
            {
                AttentionTimer.Stop();
            }
        }

        #endregion

        #region Ticks

        private void AttentionTimer_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BrainFile) &&
                Brain.Initiate)
            {
                string message;

                if (Brain.Thinking &&
                    !string.IsNullOrEmpty(Brain.LastThought))
                {
                    message = Brain.LastThought;
                }
                else
                {
                    message = Brain.Respond(true);
                }

                if (!string.IsNullOrEmpty(message))
                {
                    AddToHistory("[" + DateTime.Now.ToString("HH:mm:ss") + "] AI: " + message);

                    if (tts)
                    {
                        synthesizer.SpeakAsync(message);
                    }

                    if (Linked)
                    {
                        string[] lines = { "REAL_AI|ID=" + ID + "|" + "Message=" + message };

                        if (NetUtil.ConnectedIP == "localhost")
                        {
                            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RealAI\\link.txt";
                            if (File.Exists(path))
                            {
                                bool written = false;
                                while (!written)
                                {
                                    try { File.WriteAllLines(path, lines); }
                                    catch (IOException) {}
                                    finally { written = true; }
                                }
                            }
                        }
                        else
                        {
                            NetUtil.SendMessage(lines[0]);
                        }
                    }
                }

                if (Crash)
                {
                    File.WriteAllLines(LogPath, Log);
                    Application.Exit();
                }
            }
        }

        private void ThinkTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < threads.Count; i++)
            {
                Thread thread = threads[i];
                if (thread.ThreadState == System.Threading.ThreadState.Stopped)
                {
                    threads.Remove(thread);
                    i--;
                }
            }

            if (Brain.Thinking &&
                threads.Count == 0)
            {
                Thread thread = new Thread(Thinking);
                thread.Start();
                threads.Add(thread);
            }
        }

        private void ListenTimer_Tick(object sender, EventArgs e)
        {
            if (Linked)
            {
                string received_message = NetUtil.Get_CurrentMessage();
                if (!string.IsNullOrEmpty(received_message))
                {
                    string[] parts = received_message.Split('|');
                    if (parts.Length == 3)
                    {
                        if (parts[0] == "REAL_AI")
                        {
                            string[] id = parts[1].Split('=');
                            string[] message = parts[2].Split('=');

                            if (id.Length > 1 &&
                                message.Length > 1)
                            {
                                if (id[1] != ID)
                                {
                                    AttentionTimer.Stop();

                                    string received_id = id[1];

                                    string clean_message = message[1].Replace("\0", string.Empty);
                                    Brain.Input = clean_message;
                                    string response = Brain.Respond(false);

                                    AddToHistory(NetUtil.ConnectedIP.ToString() + ": " + Brain.CleanInput);

                                    if (!string.IsNullOrEmpty(response) &&
                                        !string.IsNullOrEmpty(received_id))
                                    {
                                        AddToHistory("[" + DateTime.Now.ToString("HH:mm:ss") + "] AI: " + response);

                                        if (tts)
                                        {
                                            synthesizer.SpeakAsync(response);
                                        }

                                        string[] lines = { "REAL_AI|ID=" + ID + "|" + "Message=" + response };

                                        if (NetUtil.ConnectedIP == "localhost")
                                        {
                                            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RealAI\\link.txt";

                                            bool written = false;
                                            while (!written)
                                            {
                                                try { File.WriteAllLines(path, lines); }
                                                catch (IOException) { }
                                                finally { written = true; }
                                            }
                                        }
                                        else
                                        {
                                            NetUtil.SendMessage(lines[0]);
                                        }

                                        NetUtil.Messages_Received.Clear();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Elapsed()
        {
            while (!ElapsedTokenSource.IsCancellationRequested) 
            {
                ElapsedTime = AppUtil.GetTime_Milliseconds(StartTime_Elapsed);

                if (ElapsedTime != "00:00:00.000")
                {
                    if (lbl_ElapsedTime.InvokeRequired &&
                        !ElapsedTokenSource.IsCancellationRequested)
                    {
                        lbl_ElapsedTime.Invoke((MethodInvoker)delegate
                        {
                            lbl_ElapsedTime.Text = "Elapsed Time: " + ElapsedTime;
                        });
                    }
                }
                else
                {
                    if (lbl_ElapsedTime.InvokeRequired &&
                        !ElapsedTokenSource.IsCancellationRequested)
                    {
                        lbl_ElapsedTime.Invoke((MethodInvoker)delegate
                        {
                            lbl_ElapsedTime.Text = "";
                        });
                    }
                }
            }
        }

        private void Remaining()
        {
            while (!RemainingTokenSource.IsCancellationRequested)
            {
                int count = 0;
                int total = 0;

                string[] text = progressDetail.CustomText.Replace("(", "").Replace(")", "").Split('/');
                if (text[0] != "Ready!" &&
                    !string.IsNullOrEmpty(text[0]))
                {
                    count = int.Parse(text[0]);
                    total = int.Parse(text[1]);
                }

                int progress = count - previous_progress;

                int milliseconds = 0;
                if (previous_progress != count)
                {
                    milliseconds = (int)(DateTime.Now - StartTime_Remaining).TotalMilliseconds;
                    StartTime_Remaining = DateTime.Now;
                }

                float time_per_process = 0;
                if (progress > 0)
                {
                    time_per_process = (float)milliseconds / progress;
                }

                previous_progress = count;
                int remaining_progress = total - count;

                if (time_per_process > 0)
                {
                    intervals.Add(time_per_process);
                }

                int sum = 0;
                int average = 0;
                if (intervals.Count > 0)
                {
                    foreach (int i in intervals)
                    {
                        sum += i;
                    }

                    average = sum / intervals.Count;
                }

                int remaining = 0;
                if (average > 0)
                {
                    remaining = average * remaining_progress;
                }
                else
                {
                    remaining = (int)time_per_process * remaining_progress;
                }

                string time = AppUtil.ConvertTime_Milliseconds(remaining / 100);

                if (time != "00:00:00.000")
                {
                    if (lbl_RemainingTime.InvokeRequired &&
                        !RemainingTokenSource.IsCancellationRequested)
                    {
                        lbl_RemainingTime.Invoke((MethodInvoker)delegate
                        {
                            lbl_RemainingTime.Text = "Remaining Time: " + time;
                        });
                    }
                }
                else
                {
                    if (lbl_RemainingTime.InvokeRequired &&
                        !RemainingTokenSource.IsCancellationRequested)
                    {
                        lbl_RemainingTime.Invoke((MethodInvoker)delegate
                        {
                            lbl_RemainingTime.Text = "";
                        });
                    }
                }
            }

            if (lbl_RemainingTime.InvokeRequired)
            {
                lbl_RemainingTime.Invoke((MethodInvoker)delegate
                {
                    lbl_RemainingTime.Text = "";
                });
            }
        }

        #endregion

        #region Clicks

        private void NewBrain_Click(object sender, EventArgs e)
        {
            StopTimers();

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Brains (*.brain)|*.brain";
            dialog.DefaultExt = "*.brain";
            dialog.InitialDirectory = Environment.CurrentDirectory + @"\Brains\";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                BrainFile = dialog.FileName;

                try
                {
                    SqlUtil.BrainConn = SqlUtil.GetSqlConnection(BrainFile);
                    SQLiteConnection.CreateFile(BrainFile);
                    SqlUtil.Init();

                    string BrainName = Path.GetFileNameWithoutExtension(BrainFile);
                    progressMain.CustomText = "Connected to \"" + BrainName + "\" brain";
                    progressMain.Value = 100;

                    Current_HistoryDir = HistoryDir + BrainName;
                    if (!Directory.Exists(Current_HistoryDir))
                    {
                        Directory.CreateDirectory(Current_HistoryDir);
                    }

                    StartNewSession();

                    ResumeTimers();
                }
                catch (Exception ex)
                {
                    List<string> log = new List<string>();
                    log.Add(ex.Message);
                    log.Add(ex.StackTrace);
                    AppUtil.CrashLog(log);
                }
            }
            else
            {
                ResumeTimers();
            }
        }

        private void LoadBrain_Click(object sender, EventArgs e)
        {
            StopTimers();

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Brains (*.brain)|*.brain";
            dialog.DefaultExt = "*.brain";
            dialog.InitialDirectory = Environment.CurrentDirectory + @"\Brains\";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                BrainFile = dialog.FileName;

                try
                {
                    string BrainName = Path.GetFileNameWithoutExtension(BrainFile);

                    SqlUtil.BrainConn = SqlUtil.GetSqlConnection(BrainFile);

                    progressMain.CustomText = "Connected to \"" + BrainName + "\" brain";
                    progressMain.Value = 100;

                    AppUtil.Set_LastBrain(BrainFile);

                    Current_HistoryDir = HistoryDir + BrainName;
                    if (!Directory.Exists(Current_HistoryDir))
                    {
                        Directory.CreateDirectory(Current_HistoryDir);
                    }

                    Thoughts.Clear();

                    StartNewSession();
                    ResumeTimers();
                }
                catch (Exception ex)
                {
                    List<string> log = new List<string>();
                    log.Add(ex.Message);
                    log.Add(ex.StackTrace);
                    AppUtil.CrashLog(log);
                }
            }
            else
            {
                ResumeTimers();
            }
        }

        private void NewSession_Click(object sender, EventArgs e)
        {
            StopTimers();

            if (!string.IsNullOrEmpty(BrainFile))
            {
                if (StartNewSession())
                {
                    progressMain.CustomText = "New Session Started";
                }

                ResumeTimers();
            }
            else
            {
                DialogResult result = MessageBox.Show("No brain connected to start session with.", "Missing Connection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    ResumeTimers();
                }
            }
        }

        private void WipeMemory_Click(object sender, EventArgs e)
        {
            StopTimers();

            if (!string.IsNullOrEmpty(BrainFile))
            {
                DialogResult result = MessageBox.Show("Are you sure you wish to delete all the data in the \"" + Path.GetFileNameWithoutExtension(BrainFile) + "\" brain?", "Memory Wipe", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    output.Text = "";
                    input.Text = "";
                    thinking.Text = "";

                    Brain.LastResponse = "";
                    Brain.LastThought = "";
                    Brain.CleanInput = "";
                    Brain.Topics = null;
                    Brain.WordArray = null;
                    Brain.WordArray_Thinking = null;
                    Thoughts.Clear();

                    SqlUtil.BulkQuery(SqlUtil.Wipe(), true);
                    AppUtil.SaveHistory(new List<string>());
                    StartNewSession();

                    progressMain.CustomText = "Memory wiped from \"" + Path.GetFileNameWithoutExtension(BrainFile) + "\" brain";

                    ResumeTimers();
                }
                else
                {
                    ResumeTimers();
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("No brain connected to wipe memory from.", "Missing Connection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    ResumeTimers();
                }
            }
        }

        private void EditBrain_Click(object sender, EventArgs e)
        {
            StopTimers();

            editor.ShowDialog();
        }

        private void EditOptions_Click(object sender, EventArgs e)
        {
            StopTimers();

            options.ShowDialog();
        }

        private void ViewThinking_Click(object sender, EventArgs e)
        {
            thinking.Close();
            thinking = new Thinking();
            thinking.Left = Left + Size.Width;
            thinking.Top = Top;
            thinking.Show();
            Focus();
        }

        private void MergeBrains_Click(object sender, EventArgs e)
        {
            StopTimers();

            merge.ShowDialog();
        }

        private void FixSpelling_Click(object sender, EventArgs e)
        {
            StopTimers();

            spelling.ShowDialog();
        }

        private void Link_Click(object sender, EventArgs e)
        {
            if (network != null &&
                NetUtil.Listener != null &&
                NetUtil.Listening)
            {
                NetUtil.Stop();
                network.Abort();
                ListenTimer.Stop();

                Link.Text = "Establish Link";

                EnterButton.Enabled = true;
                InputBox.Enabled = true;
                InputBox.Text = "";
                InputBox.Focus();

                Linked = false;

                DialogResult result = MessageBox.Show("Link has been broken.", "Disconnected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    ResumeTimers();
                }
            }
            else
            {
                StopTimers();

                connect.ShowDialog();
            }
        }

        private void ReadFile_Click(object sender, EventArgs e)
        {
            StopTimers();

            if (!string.IsNullOrEmpty(BrainFile))
            {
                Opacity = 0;
                read.ShowDialog();
            }
            else
            {
                DialogResult result = MessageBox.Show("No brain connected to read file.", "Missing Connection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    ResumeTimers();
                }
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BrainFile))
            {
                ProgressBar_Detail.Value = 0;
                ProgressBar_Main.Value = 0;

                Brain.Input = InputBox.Text;
                InputBox.Enabled = false;
                EnterButton.Enabled = false;

                lbl_ElapsedTime.Text = "";
                StartTime_Elapsed = DateTime.Now;
                ElapsedTokenSource = new CancellationTokenSource();
                ElapsedTask = Task.Factory.StartNew(() => Elapsed(), ElapsedTokenSource.Token);

                intervals.Clear();
                lbl_RemainingTime.Text = "";
                StartTime_Remaining = DateTime.Now;
                RemainingTokenSource = new CancellationTokenSource();
                RemainingTask = Task.Factory.StartNew(() => Remaining(), RemainingTokenSource.Token);

                Thread thread = new Thread(Respond);
                thread.Start();
                threads.Add(thread);
            }
            else
            {
                MessageBox.Show("No brain connected.");
            }
        }

        private void EncourageButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BrainFile))
            {
                EncourageButton.Enabled = false;

                lbl_ElapsedTime.Text = "";
                StartTime_Elapsed = DateTime.Now;
                ElapsedTokenSource = new CancellationTokenSource();
                ElapsedTask = Task.Factory.StartNew(() => Elapsed(), ElapsedTokenSource.Token);

                intervals.Clear();
                lbl_RemainingTime.Text = "";
                StartTime_Remaining = DateTime.Now;
                RemainingTokenSource = new CancellationTokenSource();
                RemainingTask = Task.Factory.StartNew(() => Remaining(), RemainingTokenSource.Token);

                Thread thread = new Thread(Encourage);
                thread.Start();
                threads.Add(thread);
            }
            else
            {
                MessageBox.Show("No brain connected.");
            }
        }

        private void DiscourageButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BrainFile))
            {
                DiscourageButton.Enabled = false;

                lbl_ElapsedTime.Text = "";
                StartTime_Elapsed = DateTime.Now;
                ElapsedTokenSource = new CancellationTokenSource();
                ElapsedTask = Task.Factory.StartNew(() => Elapsed(), ElapsedTokenSource.Token);

                intervals.Clear();
                lbl_RemainingTime.Text = "";
                StartTime_Remaining = DateTime.Now;
                RemainingTokenSource = new CancellationTokenSource();
                RemainingTask = Task.Factory.StartNew(() => Remaining(), RemainingTokenSource.Token);

                Thread thread = new Thread(Discourage);
                thread.Start();
                threads.Add(thread);
            }
            else
            {
                MessageBox.Show("No brain connected.");
            }
        }

        #endregion

        #endregion

        private class MyRenderer : ToolStripProfessionalRenderer
        {
            public MyRenderer() : base(new MyColors()) 
            {
                
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs myMenu)
            {
                if (!myMenu.Item.Selected)
                {
                    base.OnRenderMenuItemBackground(myMenu);
                }
                else
                {
                    Rectangle menuRectangle = new Rectangle(Point.Empty, myMenu.Item.Size);

                    //Fill Color
                    SolidBrush FillBrush = new SolidBrush(AppUtil.highlight_control);
                    myMenu.Graphics.FillRectangle(FillBrush, menuRectangle);

                    //Border Color
                    myMenu.Graphics.DrawRectangle(new Pen(FillBrush.Color), 1, 0, menuRectangle.Width - 2, menuRectangle.Height - 1);
                }
            }
        }

        private class MyColors : ProfessionalColorTable
        {
            public override Color MenuBorder
            {
                get { return AppUtil.background_control; }
            }

            public override Color MenuItemPressedGradientBegin
            {
                get { return AppUtil.selected_control; }
            }

            public override Color MenuItemPressedGradientEnd
            {
                get { return AppUtil.selected_control; }
            }

            public override Color ToolStripDropDownBackground
            {
                get { return AppUtil.background_control; }
            }
        }
    }
}
