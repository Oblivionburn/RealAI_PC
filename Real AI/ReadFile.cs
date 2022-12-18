using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using Real_AI.Util;

namespace Real_AI
{
    public partial class ReadFile : Form
    {
        #region Variables

        string ElapsedTime;
        DateTime StartTime_Elapsed;
        CancellationTokenSource ElapsedTokenSource;
        Task ElapsedTask;

        int previous_progress;
        DateTime StartTime_Remaining;
        CancellationTokenSource RemainingTokenSource;
        Task RemainingTask;
        HashSet<float> intervals = new HashSet<float>();

        private static TextProgressBar progressBar_Main;
        private static TextProgressBar progressBar_Detail;

        event EventHandler FinishedEvent;
        string readFileName;

        CancellationTokenSource ReadingTokenSource;
        Task ReadingTask;

        #endregion

        #region Constructors

        public ReadFile()
        {
            FormClosing += ReadFile_FormClosing;
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void ReadFile_Load(object sender, EventArgs e)
        {
            try
            {
                SetColors();

                btn_Read.Enabled = false;
                btn_Cancel.Enabled = false;

                progressBar_Main = ProgressBar_Main;
                progressBar_Main.CustomText = "Ready!";
                progressBar_Main.Value = 100;

                progressBar_Detail = ProgressBar_Detail;
                progressBar_Detail.CustomText = "Ready!";
                progressBar_Detail.Value = 100;

                lbl_ElapsedTime.Text = "";
                lbl_RemainingTime.Text = "";
            }
            catch (Exception ex)
            {
                Logger.AddLog("ReadFile.ReadFile_Load", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void ReadFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MainForm.mainForm.Opacity = 100;
                MainForm.ResumeTimers();
            }
            catch (Exception ex)
            {
                Logger.AddLog("ReadFile.ReadFile_FormClosing", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void SetColors()
        {
            try
            {
                BackColor = AppUtil.background_window;

                lbl_File.ForeColor = AppUtil.text_window;
                lbl_ElapsedTime.ForeColor = AppUtil.text_window;
                lbl_RemainingTime.ForeColor = AppUtil.text_window;

                FileBox.BackColor = AppUtil.background_control;
                FileBox.ForeColor = AppUtil.text_control;
                btn_Browse.BackColor = AppUtil.background_control;
                btn_Browse.ForeColor = AppUtil.text_control;
                btn_Read.BackColor = AppUtil.background_control;
                btn_Read.ForeColor = AppUtil.text_control;
                btn_Cancel.BackColor = AppUtil.background_control;
                btn_Cancel.ForeColor = AppUtil.text_control;

                ProgressBar_Main.BackColor = AppUtil.background_progress;
                ProgressBar_Main.ForeColor = AppUtil.text_progress;
                ProgressBar_Main.ProgressColor = AppUtil.background_progress;
                ProgressBar_Main.TextColor = AppUtil.text_progress;
                ProgressBar_Detail.BackColor = AppUtil.background_progress;
                ProgressBar_Detail.ForeColor = AppUtil.text_progress;
                ProgressBar_Detail.ProgressColor = AppUtil.background_progress;
                ProgressBar_Detail.TextColor = AppUtil.text_progress;
            }
            catch (Exception ex)
            {
                Logger.AddLog("ReadFile.SetColors", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Elapsed()
        {
            try
            {
                while (!ElapsedTokenSource.IsCancellationRequested)
                {
                    ElapsedTime = AppUtil.GetTime_Milliseconds(StartTime_Elapsed);

                    if (ElapsedTime != "00:00:00.000")
                    {
                        AppUtil.UpdateLabel(ElapsedTokenSource, lbl_ElapsedTime, "Elapsed Time: " + ElapsedTime);
                    }
                    else
                    {
                        AppUtil.UpdateLabel(ElapsedTokenSource, lbl_ElapsedTime, "");
                    }
                }

                AppUtil.UpdateLabel(lbl_ElapsedTime, "");
            }
            catch (Exception ex)
            {
                Logger.AddLog("ReadFile.Elapsed", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Remaining()
        {
            try
            {
                while (!RemainingTokenSource.IsCancellationRequested)
                {
                    int count = 0;
                    int total = 0;

                    string[] text = progressBar_Detail.CustomText.Replace("(", "").Replace(")", "").Split('/');
                    if (text[0] != "Ready!")
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
                        AppUtil.UpdateLabel(RemainingTokenSource, lbl_RemainingTime, "Remaining Time: " + time);
                    }
                    else
                    {
                        AppUtil.UpdateLabel(RemainingTokenSource, lbl_RemainingTime, "");
                    }
                }

                AppUtil.UpdateLabel(lbl_RemainingTime, "");
            }
            catch (Exception ex)
            {
                Logger.AddLog("ReadFile.Remaining", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void FileBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(FileBox.Text))
                {
                    btn_Read.Enabled = true;
                }
                else
                {
                    btn_Read.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("ReadFile.FileBox_TextChanged", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Btn_Browse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Filter = "txt (*.txt)|*.txt",
                    DefaultExt = "*.txt"
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    readFileName = dialog.FileName;
                    FileBox.Text = readFileName;

                    if (!string.IsNullOrEmpty(FileBox.Text))
                    {
                        btn_Read.Enabled = true;
                    }
                    else
                    {
                        btn_Read.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("ReadFile.Btn_Browse_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Btn_Read_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar_Main.Value = 0;
                progressBar_Detail.Value = 0;

                lbl_ElapsedTime.Text = "";
                StartTime_Elapsed = DateTime.Now;
                ElapsedTokenSource = new CancellationTokenSource();
                ElapsedTask = Task.Factory.StartNew(() => Elapsed(), ElapsedTokenSource.Token);

                intervals.Clear();
                lbl_RemainingTime.Text = "";
                StartTime_Remaining = DateTime.Now;
                RemainingTokenSource = new CancellationTokenSource();
                RemainingTask = Task.Factory.StartNew(() => Remaining(), RemainingTokenSource.Token);

                FinishedEvent -= ReadingDone;
                FinishedEvent += ReadingDone;

                ReadingTokenSource = new CancellationTokenSource();
                ReadingTask = Task.Factory.StartNew(() => ProcessFile(), ReadingTokenSource.Token);

                FileBox.Enabled = false;
                btn_Browse.Enabled = false;
                btn_Read.Enabled = false;

                btn_Cancel.Enabled = true;
            }
            catch (Exception ex)
            {
                Logger.AddLog("ReadFile.Btn_Read_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (RemainingTask != null)
                {
                    try
                    {
                        ReadingTokenSource.Cancel();
                        ReadingTokenSource.Dispose();
                        RemainingTask = null;
                    }
                    catch (ObjectDisposedException)
                    {
                        //Ignore ReadingTokenSource already disposed
                    }
                    finally
                    {
                        RemainingTask = null;
                    }

                    FileBox.Enabled = true;
                    btn_Browse.Enabled = true;
                    btn_Read.Enabled = true;

                    btn_Cancel.Enabled = false;

                    try
                    {
                        ElapsedTokenSource.Cancel();
                        ElapsedTokenSource.Dispose();
                        ElapsedTask = null;
                    }
                    catch (ObjectDisposedException)
                    {
                        //Ignore ElapsedTokenSource already disposed
                    }
                    finally
                    {
                        ElapsedTask = null;
                    }

                    lbl_ElapsedTime.Text = "";

                    try
                    {
                        RemainingTokenSource.Cancel();
                        RemainingTokenSource.Dispose();
                        RemainingTask = null;
                    }
                    catch (ObjectDisposedException)
                    {
                        //Ignore RemainingTokenSource already disposed
                    }
                    finally
                    {
                        RemainingTask = null;
                    }

                    lbl_RemainingTime.Text = "";
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("ReadFile.Btn_Cancel_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void ReadingDone(object sender, EventArgs arg)
        {
            try
            {
                try
                {
                    ReadingTokenSource.Cancel();
                    ReadingTokenSource.Dispose();
                    RemainingTask = null;
                }
                catch (ObjectDisposedException)
                {
                    //Ignore ReadingTokenSource already disposed
                }
                finally
                {
                    RemainingTask = null;
                }

                try
                {
                    ElapsedTokenSource.Cancel();
                    ElapsedTokenSource.Dispose();
                    ElapsedTask = null;
                }
                catch (ObjectDisposedException)
                {
                    //Ignore ElapsedTokenSource already disposed
                }
                finally
                {
                    ElapsedTask = null;
                }

                lbl_ElapsedTime.Text = "";

                try
                {
                    RemainingTokenSource.Cancel();
                    RemainingTokenSource.Dispose();
                    RemainingTask = null;
                }
                catch (ObjectDisposedException)
                {
                    //Ignore RemainingTokenSource already disposed
                }
                finally
                {
                    RemainingTask = null;
                }

                lbl_RemainingTime.Text = "";

                string file = Path.GetFileNameWithoutExtension(readFileName);

                DialogResult result = MessageBox.Show("\"" + file + "\" has been read.\nTotal Processing Time: " + ElapsedTime, "Read File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    FileBox.Enabled = true;
                    btn_Browse.Enabled = true;
                    btn_Read.Enabled = false;
                    btn_Cancel.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("ReadFile.ReadingDone", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void ProcessFile()
        {
            try
            {
                int count = 0;
                int total = 0;

                StringBuilder sb = new StringBuilder();

                List<string> words = new List<string>();
                List<string> inputs = new List<string>();
                List<SQLiteCommand> commands = new List<SQLiteCommand>();

                StartTime_Remaining = DateTime.Now;
                if (progressBar_Main.InvokeRequired &&
                    !ReadingTokenSource.IsCancellationRequested)
                {
                    progressBar_Main.Invoke((MethodInvoker)delegate
                    {
                        progressBar_Main.CustomText = "Reading file";
                        progressBar_Main.Value = 25;
                        intervals.Clear();
                    });
                }

                string[] lines = File.ReadAllLines(readFileName);

                total = lines.Length;

                AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, "(" + count + "/" + total + ")");
                AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, 0);

                foreach (string line in lines)
                {
                    if (ReadingTokenSource.IsCancellationRequested)
                    {
                        break;
                    }

                    string new_line = line.Trim();
                    if (new_line.Length > 0)
                    {
                        bool replaced_semicolon = false;

                        for (var i = 0; i < line.Length; i++)
                        {
                            if (ReadingTokenSource.IsCancellationRequested)
                            {
                                break;
                            }

                            string value = line[i].ToString();
                            if (Brain.NormalCharacters.IsMatch(value) &&
                                value != " ")
                            {
                                if (replaced_semicolon)
                                {
                                    replaced_semicolon = false;
                                    sb.Append(value.ToUpper());
                                }
                                else
                                {
                                    sb.Append(value);
                                }
                            }
                            else if (value == "." ||
                                     value == "!" ||
                                     value == "?" ||
                                     value == "," ||
                                     value == "'" ||
                                     value == "’" ||
                                     value == ":" ||
                                     value == ";" ||
                                     value == " ")
                            {
                                if (value == ";")
                                {
                                    replaced_semicolon = true;
                                    value = ".";
                                }

                                sb.Append(value);
                            }
                        }

                        sb.Append(" ");
                    }

                    count++;

                    AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, "(" + count + "/" + total + ")");
                    AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, (count * 100) / total);
                }

                if (!ReadingTokenSource.IsCancellationRequested)
                {
                    AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, "(0/" + sb.ToString().Length + ")");
                    AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, 0);

                    string[] gap_array = Brain.GapSpecials(sb.ToString(), progressBar_Main, progressBar_Detail, ReadingTokenSource, true).Split(' ');
                    foreach (string word in gap_array)
                    {
                        if (ReadingTokenSource.IsCancellationRequested)
                        {
                            break;
                        }

                        if (!string.IsNullOrEmpty(word))
                        {
                            words.Add(word);
                        }
                    }
                }

                if (!ReadingTokenSource.IsCancellationRequested)
                {
                    string[] words_array = words.ToArray();
                    if (words_array.Length > 0)
                    {
                        if (progressBar_Main.InvokeRequired &&
                            !ReadingTokenSource.IsCancellationRequested)
                        {
                            progressBar_Main.Invoke((MethodInvoker)delegate
                            {
                                progressBar_Main.CustomText = "Identifying sentences";
                                progressBar_Main.Value = 50;
                                intervals.Clear();
                            });
                        }

                        count = 0;
                        total = words_array.Length;

                        AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, "(" + count + "/" + total + ")");
                        AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, 0);

                        StringBuilder input = new StringBuilder();
                        for (int i = 0; i < words_array.Length; i++)
                        {
                            if (ReadingTokenSource.IsCancellationRequested)
                            {
                                break;
                            }

                            input.Append(words_array[i]);
                            input.Append(" ");

                            if (words_array[i] == "?" ||
                                words_array[i] == "!")
                            {
                                inputs.Add(input.ToString().Trim());
                                input = new StringBuilder();
                            }
                            else if (words_array[i] == ".")
                            {
                                if (i < words_array.Length - 1 &&
                                    i > 0)
                                {
                                    if (words_array[i + 1] != "." &&
                                        words_array[i - 1] != ".")
                                    {
                                        inputs.Add(input.ToString().Trim());
                                        input = new StringBuilder();
                                    }
                                }
                            }

                            count++;

                            AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, "(" + count + "/" + total + ")");
                            AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, (count * 100) / total);
                        }
                    }

                    words.Clear();
                }

                if (!ReadingTokenSource.IsCancellationRequested)
                {
                    if (inputs.Count > 0)
                    {
                        if (progressBar_Main.InvokeRequired &&
                            !ReadingTokenSource.IsCancellationRequested)
                        {
                            progressBar_Main.Invoke((MethodInvoker)delegate
                            {
                                progressBar_Main.CustomText = "Prepping data";
                                progressBar_Main.Value = 75;
                                intervals.Clear();
                            });
                        }

                        count = 0;
                        total = inputs.Count;

                        AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, "(" + count + "/" + total + ")");
                        AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, 0);

                        foreach (string existing in inputs)
                        {
                            if (ReadingTokenSource.IsCancellationRequested)
                            {
                                break;
                            }

                            string[] WordArray = existing.Split(' ');
                            if (WordArray.Length > 0)
                            {
                                commands.AddRange(Brain.AddInputs(Brain.RulesCheck(existing)));
                                commands.AddRange(Brain.AddWords(WordArray));
                                commands.AddRange(Brain.AddPreWords(WordArray));
                                commands.AddRange(Brain.AddProWords(WordArray));

                                count++;

                                AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, "(" + count + "/" + total + ")");
                                AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, (count * 100) / total);
                            }
                        }
                    }

                    inputs.Clear();
                }

                if (!ReadingTokenSource.IsCancellationRequested)
                {
                    if (progressBar_Main.InvokeRequired &&
                        !ReadingTokenSource.IsCancellationRequested)
                    {
                        progressBar_Main.Invoke((MethodInvoker)delegate
                        {
                            progressBar_Main.CustomText = "Saving data";
                            progressBar_Main.Value = 100;
                            intervals.Clear();
                        });
                    }

                    SqlUtil.BulkQuery(commands, MainForm.BrainFile, progressBar_Detail, ReadingTokenSource);
                }

                commands.Clear();

                AppUtil.UpdateProgress(progressBar_Main, "Ready!");
                AppUtil.UpdateProgress(progressBar_Main, 0);

                AppUtil.UpdateProgress(progressBar_Detail, "Ready!");
                AppUtil.UpdateProgress(progressBar_Detail, 0);

                GC.Collect();
            }
            catch (Exception ex)
            {
                Logger.AddLog("ReadFile.ProcessFile", ex.Source, ex.Message, ex.StackTrace);
            }
            finally
            {
                if (!ReadingTokenSource.IsCancellationRequested)
                {
                    BeginInvoke(FinishedEvent, this, EventArgs.Empty);
                }
            }
        }

        #endregion
    }
}
