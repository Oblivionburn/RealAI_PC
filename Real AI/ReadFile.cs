using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
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

        CancellationTokenSource RemainingTokenSource;
        Task RemainingTask;
        private static int intervalIndex = 0;
        private static List<int> intervals = new List<int>();
        private static int previous_progress;
        private static DateTime StartTime_Remaining;
        private static bool timeMain;
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

                    Block(0.00001);
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

                    if (timeMain)
                    {
                        int index = progressBar_Main.CustomText.IndexOf("(");
                        if (index > -1)
                        {
                            string mainText = progressBar_Main.CustomText.Substring(index);
                            string[] text = mainText.Replace("Batch: ", "").Replace("(", "").Replace(")", "").Split('/');

                            if (text.Length > 1 &&
                                text[0] != "Ready!" &&
                                text[0] != "Done!")
                            {
                                count = int.Parse(text[0]);
                                total = int.Parse(text[1]);
                            }
                        }
                    }
                    else
                    {
                        string[] text = progressBar_Detail.CustomText.Replace("(", "").Replace(")", "").Split('/');
                        if (text.Length > 1 &&
                            text[0] != "Ready!" &&
                            text[0] != "Done!")
                        {
                            count = int.Parse(text[0]);
                            total = int.Parse(text[1]);
                        }
                    }

                    int milliseconds = 0;
                    if (previous_progress != count)
                    {
                        milliseconds = (int)(DateTime.Now - StartTime_Remaining).TotalMilliseconds;
                        StartTime_Remaining = DateTime.Now;
                    }

                    int progress = count - previous_progress;
                    int time_per_process = 0;
                    if (progress > 0)
                    {
                        time_per_process = milliseconds / progress;
                    }

                    int intervalCount = intervals.Count;

                    if (time_per_process > 0)
                    {
                        if (intervalCount >= 1000)
                        {
                            intervals[intervalIndex] = time_per_process;

                            intervalIndex++;
                            if (intervalIndex > intervalCount - 1)
                            {
                                intervalIndex = 0;
                            }
                        }
                        else
                        {
                            intervals.Add(time_per_process);
                        }
                    }

                    int sum = 0;
                    float average = 0;

                    intervalCount = intervals.Count;
                    if (intervalCount > 0)
                    {
                        for (int i = 0; i < intervalCount; i++)
                        {
                            sum += intervals[i];
                        }

                        average = sum / intervalCount;
                    }

                    int remaining_progress = total - count;
                    int remaining = 0;
                    if (average > 0)
                    {
                        remaining = (int)((average * remaining_progress) / 5);
                    }
                    else if (time_per_process > 0)
                    {
                        remaining = (int)((time_per_process * remaining_progress) / 5);
                    }

                    string time = AppUtil.ConvertTime_Milliseconds(remaining);

                    if (time != "00:00:00.000")
                    {
                        AppUtil.UpdateLabel(RemainingTokenSource, lbl_RemainingTime, "Remaining Time: " + time);
                    }
                    else
                    {
                        AppUtil.UpdateLabel(RemainingTokenSource, lbl_RemainingTime, "");
                    }

                    previous_progress = count;

                    Block(0.000001);
                }

                AppUtil.UpdateLabel(lbl_RemainingTime, "");
            }
            catch (Exception ex)
            {
                Logger.AddLog("ReadFile.Remaining", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private static void Block(double durationSeconds)
        {
            var durationTicks = Math.Round(durationSeconds * Stopwatch.Frequency);
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedTicks < durationTicks)
            {

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

                        AppUtil.UpdateProgress(progressBar_Main, "Ready!");
                        AppUtil.UpdateProgress(progressBar_Main, 0);

                        AppUtil.UpdateProgress(progressBar_Detail, "Ready!");
                        AppUtil.UpdateProgress(progressBar_Detail, 0);
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

                timeMain = false;
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
                        previous_progress = 0;
                        intervals.Clear();
                    });
                }

                string[] lines = File.ReadAllLines(readFileName);

                total = lines.Length;

                AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, "(" + count + "/" + total + ")");
                AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, 0);

                for (int l = 0; l < total; l++)
                {
                    if (ReadingTokenSource.IsCancellationRequested)
                    {
                        break;
                    }

                    string line = lines[l];

                    string new_line = line.Trim();
                    if (new_line.Length > 0)
                    {
                        bool replaced_semicolon = false;

                        int lineLength = new_line.Length;
                        for (int i = 0; i < lineLength; i++)
                        {
                            if (ReadingTokenSource.IsCancellationRequested)
                            {
                                break;
                            }

                            string value = new_line[i].ToString();

                            if (!string.IsNullOrEmpty(value))
                            {
                                if (value == "\r" ||
                                    value == "\n" ||
                                    value == Environment.NewLine)
                                {

                                }
                                else if (Brain.NormalCharacters.IsMatch(value))
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
                                else if (value != "'" &&
                                         value != "’")
                                {
                                    if (value == ".")
                                    {
                                        if (i > 0)
                                        {
                                            if (new_line[i - 1] != '.')
                                            {
                                                sb.Append(" ");
                                            }

                                            sb.Append(value);
                                        }
                                        else
                                        {
                                            sb.Append(value);
                                        }

                                        if (i < lineLength - 1)
                                        {
                                            if (new_line[i + 1] != ' ' &&
                                                new_line[i + 1] != '.')
                                            {
                                                sb.Append(" ");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (value == ";")
                                        {
                                            replaced_semicolon = true;
                                            value = ".";
                                        }

                                        sb.Append(" " + value);

                                        if (i < lineLength - 1 &&
                                            new_line[i + 1] != ' ')
                                        {
                                            sb.Append(" ");
                                        }
                                    }
                                }
                                else
                                {
                                    sb.Append(value);
                                }
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
                    string[] words_array = sb.ToString().Split(' ');
                    if (words_array.Length > 0)
                    {
                        StartTime_Remaining = DateTime.Now;
                        if (progressBar_Main.InvokeRequired &&
                            !ReadingTokenSource.IsCancellationRequested)
                        {
                            progressBar_Main.Invoke((MethodInvoker)delegate
                            {
                                progressBar_Main.CustomText = "Identifying sentences";
                                progressBar_Main.Value = 50;
                                previous_progress = 0;
                                intervals.Clear();
                            });
                        }

                        count = 0;
                        total = words_array.Length;

                        AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, "(" + count + "/" + total + ")");
                        AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, 0);

                        StringBuilder input = new StringBuilder();
                        for (int i = 0; i < total; i++)
                        {
                            if (ReadingTokenSource.IsCancellationRequested)
                            {
                                break;
                            }

                            string word = words_array[i];

                            input.Append(word);

                            if (word == "?" ||
                                word == "!")
                            {
                                inputs.Add(input.ToString().Trim());
                                input = new StringBuilder();
                            }
                            else if (word == ".")
                            {
                                if (i < total - 1 &&
                                    i > 0)
                                {
                                    if (words_array[i + 1] != "." &&
                                        words_array[i - 1] != ".")
                                    {
                                        inputs.Add(input.ToString().Trim());
                                        input = new StringBuilder();
                                    }
                                }
                                else if (i == total - 1)
                                {
                                    inputs.Add(input.ToString().Trim());
                                }
                            }
                            else if (word != " ")
                            {
                                input.Append(" ");
                            }

                            count++;

                            AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, "(" + count + "/" + total + ")");
                            AppUtil.UpdateProgress(ReadingTokenSource, progressBar_Detail, (count * 100) / total);
                        }
                    }
                }

                if (!ReadingTokenSource.IsCancellationRequested)
                {
                    if (inputs.Count > 0)
                    {
                        total = inputs.Count;

                        StartTime_Remaining = DateTime.Now;
                        previous_progress = 0;
                        intervals.Clear();

                        for (int i = 0; i < total; i++)
                        {
                            if (ReadingTokenSource.IsCancellationRequested)
                            {
                                break;
                            }

                            if (progressBar_Main.InvokeRequired &&
                                !ReadingTokenSource.IsCancellationRequested)
                            {
                                progressBar_Main.Invoke((MethodInvoker)delegate
                                {
                                    progressBar_Main.CustomText = "Saving data (Batch: " + (i + 1).ToString() + "/" + total + ")";
                                    progressBar_Main.Value = 75;
                                });
                            }
                            timeMain = true;

                            string input = inputs[i];
                            if (input.Length > 0)
                            {
                                string cleanInput = Brain.RulesCheck(input);
                                if (!string.IsNullOrEmpty(cleanInput))
                                {
                                    commands.AddRange(Brain.AddInputs(cleanInput));
                                }

                                string[] word_array = input.Split(' ');
                                if (word_array.Length > 0)
                                {
                                    commands.AddRange(Brain.AddWords(word_array));
                                    commands.AddRange(Brain.AddPreWords(word_array));
                                    commands.AddRange(Brain.AddProWords(word_array));
                                }

                                SqlUtil.BulkQuery(commands, MainForm.BrainFile, progressBar_Detail, ReadingTokenSource);
                                commands.Clear();
                            }
                        }

                        inputs.Clear();
                    }
                }

                AppUtil.UpdateProgress(progressBar_Main, "Done!");
                AppUtil.UpdateProgress(progressBar_Main, 100);

                AppUtil.UpdateProgress(progressBar_Detail, "Done!");
                AppUtil.UpdateProgress(progressBar_Detail, 100);

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
