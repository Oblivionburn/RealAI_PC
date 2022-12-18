using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using Real_AI.Util;

namespace Real_AI
{
    public partial class Merge : Form
    {
        #region Variables

        public static TextProgressBar progressBar;

        #endregion

        #region Constructors

        public Merge()
        {
            FormClosing += Merge_FormClosing;
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void Merge_Load(object sender, EventArgs e)
        {
            try
            {
                SetColors();

                progressBar = ProgressBar;
                progressBar.CustomText = "Ready!";
                progressBar.Value = 100;
            }
            catch (Exception ex)
            {
                Logger.AddLog("Merge.Merge_Load", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Merge_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MainForm.ResumeTimers();
            }
            catch (Exception ex)
            {
                Logger.AddLog("Merge.Merge_FormClosing", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void SetColors()
        {
            try
            {
                BackColor = AppUtil.background_window;

                lbl_FirstBrain.ForeColor = AppUtil.text_window;
                lbl_SecondBrain.ForeColor = AppUtil.text_window;
                lbl_MergedBrain.ForeColor = AppUtil.text_window;

                first_box.BackColor = AppUtil.background_control;
                first_box.ForeColor = AppUtil.text_control;
                second_box.BackColor = AppUtil.background_control;
                second_box.ForeColor = AppUtil.text_control;
                merged_box.BackColor = AppUtil.background_control;
                merged_box.ForeColor = AppUtil.text_control;
                first_Open.BackColor = AppUtil.background_control;
                first_Open.ForeColor = AppUtil.text_control;
                second_Open.BackColor = AppUtil.background_control;
                second_Open.ForeColor = AppUtil.text_control;
                merged_Save.BackColor = AppUtil.background_control;
                merged_Save.ForeColor = AppUtil.text_control;
                btn_Merge.BackColor = AppUtil.background_control;
                btn_Merge.ForeColor = AppUtil.text_control;

                ProgressBar.BackColor = AppUtil.background_progress;
                ProgressBar.ForeColor = AppUtil.text_progress;
                ProgressBar.ProgressColor = AppUtil.background_progress;
                ProgressBar.TextColor = AppUtil.text_progress;
            }
            catch (Exception ex)
            {
                Logger.AddLog("Merge.SetColors", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void First_Open_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Filter = "Brains (*.brain)|*.brain",
                    DefaultExt = "*.brain",
                    InitialDirectory = Environment.CurrentDirectory + @"\Brains\"
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    first_box.Text = dialog.FileName;
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Merge.First_Open_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Second_Open_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Filter = "Brains (*.brain)|*.brain",
                    DefaultExt = "*.brain",
                    InitialDirectory = Environment.CurrentDirectory + @"\Brains\"
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    second_box.Text = dialog.FileName;
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Merge.Second_Open_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Merged_Save_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog
                {
                    Filter = "Brains (*.brain)|*.brain",
                    DefaultExt = "*.brain",
                    InitialDirectory = Environment.CurrentDirectory + @"\Brains\"
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    merged_box.Text = dialog.FileName;
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Merge.Merged_Save_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Btn_Merge_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(first_box.Text) &&
                    !string.IsNullOrEmpty(second_box.Text) &&
                    !string.IsNullOrEmpty(merged_box.Text))
                {
                    progressBar.Value = 0;

                    //Get data from first brain
                    List<Input> first_Inputs = SqlUtil.Get_Inputs(first_box.Text);
                    List<Word> first_Words = SqlUtil.Get_Words(first_box.Text);
                    List<Output> first_Outputs = SqlUtil.Get_Outputs(first_box.Text);
                    List<Topic> first_Topics = SqlUtil.Get_Topics(first_box.Text);
                    List<PreWord> first_PreWords = SqlUtil.Get_PreWords(first_box.Text);
                    List<ProWord> first_ProWords = SqlUtil.Get_ProWords(first_box.Text);

                    //Get data from second brain
                    List<Input> second_Inputs = SqlUtil.Get_Inputs(second_box.Text);
                    List<Word> second_Words = SqlUtil.Get_Words(second_box.Text);
                    List<Output> second_Outputs = SqlUtil.Get_Outputs(second_box.Text);
                    List<Topic> second_Topics = SqlUtil.Get_Topics(second_box.Text);
                    List<PreWord> second_PreWords = SqlUtil.Get_PreWords(second_box.Text);
                    List<ProWord> second_ProWords = SqlUtil.Get_ProWords(second_box.Text);

                    //Create and init merged brain
                    SQLiteConnection.CreateFile(merged_box.Text);
                    SqlUtil.Init(merged_box.Text);

                    //Insert merged data
                    List<SQLiteCommand> commands = new List<SQLiteCommand>();
                    commands.AddRange(MergeInputs(first_Inputs, second_Inputs));
                    commands.AddRange(MergeWords(first_Words, second_Words));
                    commands.AddRange(MergeOutputs(first_Outputs, second_Outputs));
                    commands.AddRange(MergeTopics(first_Topics, second_Topics));
                    commands.AddRange(MergePreWords(first_PreWords, second_PreWords));
                    commands.AddRange(MergeProWords(first_ProWords, second_ProWords));
                    SqlUtil.BulkQuery(commands, merged_box.Text, progressBar, null);
                    commands.Clear();

                    string brainFile = Path.GetFileNameWithoutExtension(merged_box.Text);
                    DialogResult dialogResult = MessageBox.Show("\"" + brainFile + "\" has been created.", "Merge Complete", MessageBoxButtons.OK);
                    if (dialogResult == DialogResult.OK)
                    {
                        Close();
                    }
                }
                else if (string.IsNullOrEmpty(first_box.Text))
                {
                    MessageBox.Show("No first brain selected.");
                }
                else if (string.IsNullOrEmpty(second_box.Text))
                {
                    MessageBox.Show("No second brain selected.");
                }
                else if (string.IsNullOrEmpty(merged_box.Text))
                {
                    MessageBox.Show("No merged brain selected.");
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Merge.Btn_Merge_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private List<SQLiteCommand> MergeInputs(List<Input> first_list, List<Input> second_list)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                foreach (Input first in first_list)
                {
                    foreach (Input second in second_list)
                    {
                        if (second.input == first.input &&
                            second.priority > first.priority)
                        {
                            first.priority = second.priority;
                            break;
                        }
                    }
                }

                foreach (Input second in second_list)
                {
                    bool found = false;
                    foreach (Input first in first_list)
                    {
                        if (second.input == first.input)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        first_list.Add(second);
                    }
                }

                foreach (Input first in first_list)
                {
                    SQLiteParameter input_parm = new SQLiteParameter("@input", first.input);
                    input_parm.DbType = DbType.String;

                    SQLiteParameter priority_parm = new SQLiteParameter("@priority", first.priority);
                    priority_parm.DbType = DbType.Int32;

                    string sql = "INSERT INTO Inputs (Input, Priority) VALUES (@input, @priority)";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(input_parm);
                    cmd.Parameters.Add(priority_parm);
                    commands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Merge.MergeInputs", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        private List<SQLiteCommand> MergeWords(List<Word> first_list, List<Word> second_list)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                foreach (Word first in first_list)
                {
                    foreach (Word second in second_list)
                    {
                        if (second.word == first.word &&
                            second.priority > first.priority)
                        {
                            first.priority = second.priority;
                            break;
                        }
                    }
                }

                foreach (Word second in second_list)
                {
                    bool found = false;
                    foreach (Word first in first_list)
                    {
                        if (second.word == first.word)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        first_list.Add(second);
                    }
                }

                foreach (Word first in first_list)
                {
                    SQLiteParameter word_parm = new SQLiteParameter("@word", first.word);
                    word_parm.DbType = DbType.String;

                    SQLiteParameter priority_parm = new SQLiteParameter("@priority", first.priority);
                    priority_parm.DbType = DbType.Int32;

                    string sql = "INSERT INTO Words (Word, Priority) VALUES (@word, @priority)";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(word_parm);
                    cmd.Parameters.Add(priority_parm);
                    commands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Merge.MergeWords", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        private List<SQLiteCommand> MergeOutputs(List<Output> first_list, List<Output> second_list)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                foreach (Output first in first_list)
                {
                    foreach (Output second in second_list)
                    {
                        if (second.input == first.input &&
                            second.output == first.output &&
                            second.priority > first.priority)
                        {
                            first.priority = second.priority;
                            break;
                        }
                    }
                }

                foreach (Output second in second_list)
                {
                    bool found = false;
                    foreach (Output first in first_list)
                    {
                        if (second.input == first.input &&
                            second.output == first.output)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        first_list.Add(second);
                    }
                }

                foreach (Output first in first_list)
                {
                    SQLiteParameter input_parm = new SQLiteParameter("@input", first.input);
                    input_parm.DbType = DbType.String;

                    SQLiteParameter output_parm = new SQLiteParameter("@output", first.output);
                    output_parm.DbType = DbType.String;

                    SQLiteParameter priority_parm = new SQLiteParameter("@priority", first.priority);
                    priority_parm.DbType = DbType.Int32;

                    string sql = "INSERT INTO Outputs (Input, Output, Priority) VALUES (@input, @output, @priority)";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(input_parm);
                    cmd.Parameters.Add(output_parm);
                    cmd.Parameters.Add(priority_parm);
                    commands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Merge.MergeOutputs", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        private List<SQLiteCommand> MergeTopics(List<Topic> first_list, List<Topic> second_list)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                foreach (Topic first in first_list)
                {
                    foreach (Topic second in second_list)
                    {
                        if (second.input == first.input &&
                            second.topic == first.topic &&
                            second.priority > first.priority)
                        {
                            first.priority = second.priority;
                            break;
                        }
                    }
                }

                foreach (Topic second in second_list)
                {
                    bool found = false;
                    foreach (Topic first in first_list)
                    {
                        if (second.input == first.input &&
                            second.topic == first.topic)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        first_list.Add(second);
                    }
                }

                foreach (Topic first in first_list)
                {
                    SQLiteParameter input_parm = new SQLiteParameter("@input", first.input);
                    input_parm.DbType = DbType.String;

                    SQLiteParameter topic_parm = new SQLiteParameter("@topic", first.topic);
                    topic_parm.DbType = DbType.String;

                    SQLiteParameter priority_parm = new SQLiteParameter("@priority", first.priority);
                    priority_parm.DbType = DbType.Int32;

                    string sql = "INSERT INTO Topics (Input, Topic, Priority) VALUES (@input, @topic, @priority)";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(input_parm);
                    cmd.Parameters.Add(topic_parm);
                    cmd.Parameters.Add(priority_parm);
                    commands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Merge.MergeTopics", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        private List<SQLiteCommand> MergePreWords(List<PreWord> first_list, List<PreWord> second_list)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                foreach (PreWord first in first_list)
                {
                    foreach (PreWord second in second_list)
                    {
                        if (second.word == first.word &&
                            second.preWord == first.preWord &&
                            second.distance == first.distance &&
                            second.priority > first.priority)
                        {
                            first.priority = second.priority;
                            break;
                        }
                    }
                }

                foreach (PreWord second in second_list)
                {
                    bool found = false;
                    foreach (PreWord first in first_list)
                    {
                        if (second.word == first.word &&
                            second.preWord == first.preWord &&
                            second.distance == first.distance)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        first_list.Add(second);
                    }
                }

                foreach (PreWord first in first_list)
                {
                    SQLiteParameter word_parm = new SQLiteParameter("@word", first.word);
                    word_parm.DbType = DbType.String;

                    SQLiteParameter preWord_parm = new SQLiteParameter("@preWord", first.preWord);
                    preWord_parm.DbType = DbType.String;

                    SQLiteParameter priority_parm = new SQLiteParameter("@priority", first.priority);
                    priority_parm.DbType = DbType.Int32;

                    SQLiteParameter distance_parm = new SQLiteParameter("@distance", first.distance);
                    distance_parm.DbType = DbType.Int32;

                    string sql = "INSERT INTO PreWords (Word, PreWord, Priority, Distance) VALUES (@word, @preWord, @priority, @distance)";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(word_parm);
                    cmd.Parameters.Add(preWord_parm);
                    cmd.Parameters.Add(priority_parm);
                    cmd.Parameters.Add(distance_parm);
                    commands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Merge.MergePreWords", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        private List<SQLiteCommand> MergeProWords(List<ProWord> first_list, List<ProWord> second_list)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                foreach (ProWord first in first_list)
                {
                    foreach (ProWord second in second_list)
                    {
                        if (second.word == first.word &&
                            second.proWord == first.proWord &&
                            second.distance == first.distance &&
                            second.priority > first.priority)
                        {
                            first.priority = second.priority;
                            break;
                        }
                    }
                }

                foreach (ProWord second in second_list)
                {
                    bool found = false;
                    foreach (ProWord first in first_list)
                    {
                        if (second.word == first.word &&
                            second.proWord == first.proWord &&
                            second.distance == first.distance)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        first_list.Add(second);
                    }
                }

                foreach (ProWord first in first_list)
                {
                    SQLiteParameter word_parm = new SQLiteParameter("@word", first.word);
                    word_parm.DbType = DbType.String;

                    SQLiteParameter proWord_parm = new SQLiteParameter("@proWord", first.proWord);
                    proWord_parm.DbType = DbType.String;

                    SQLiteParameter priority_parm = new SQLiteParameter("@priority", first.priority);
                    priority_parm.DbType = DbType.Int32;

                    SQLiteParameter distance_parm = new SQLiteParameter("@distance", first.distance);
                    distance_parm.DbType = DbType.Int32;

                    string sql = "INSERT INTO ProWords (Word, ProWord, Priority, Distance) VALUES (@word, @proWord, @priority, @distance)";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(word_parm);
                    cmd.Parameters.Add(proWord_parm);
                    cmd.Parameters.Add(priority_parm);
                    cmd.Parameters.Add(distance_parm);
                    commands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Merge.MergeProWords", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        #endregion
    }
}
