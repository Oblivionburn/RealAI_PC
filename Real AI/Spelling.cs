using Real_AI.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace Real_AI
{
    public partial class Spelling : Form
    {
        #region Variables

        public static TextProgressBar progressBar;
        private string old_word = "";
        private int index = -1;

        #endregion

        #region Constructors

        public Spelling()
        {
            FormClosing += Spelling_FormClosing;
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void Spelling_Load(object sender, EventArgs e)
        {
            try
            {
                SetColors();

                progressBar = ProgressBar;
                progressBar.Value = 100;

                WordList.Items.Clear();
                WordList.Text = "";

                old_word = "";
                NewWordBox.Text = "";
                index = -1;

                if (!string.IsNullOrEmpty(MainForm.BrainFile))
                {
                    List<Word> words = SqlUtil.Get_Words(MainForm.BrainFile);
                    foreach (Word word in words)
                    {
                        WordList.Items.Add(word.word);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Spelling.Spelling_Load", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void Spelling_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MainForm.ResumeTimers();
            }
            catch (Exception ex)
            {
                Logger.AddLog("Spelling.Spelling_FormClosing", ex.Source, ex.Message, ex.StackTrace);
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
                Logger.AddLog("Spelling.ComboBox_DrawItem", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void SetColors()
        {
            try
            {
                BackColor = AppUtil.background_window;

                lbl_SelectWord.ForeColor = AppUtil.text_window;
                lbl_NewWord.ForeColor = AppUtil.text_window;

                WordList.BackColor = AppUtil.background_control;
                WordList.ForeColor = AppUtil.text_control;
                NewWordBox.BackColor = AppUtil.background_control;
                NewWordBox.ForeColor = AppUtil.text_control;
                UpdateButton.BackColor = AppUtil.background_control;
                UpdateButton.ForeColor = AppUtil.text_control;

                ProgressBar.BackColor = AppUtil.background_progress;
                ProgressBar.ForeColor = AppUtil.text_progress;
                ProgressBar.ProgressColor = AppUtil.background_progress;
                ProgressBar.TextColor = AppUtil.text_progress;
            }
            catch (Exception ex)
            {
                Logger.AddLog("Spelling.SetColors", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void WordList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                old_word = ((ComboBox)sender).SelectedItem.ToString();
                index = ((ComboBox)sender).SelectedIndex;
            }
            catch (Exception ex)
            {
                Logger.AddLog("Spelling.WordList_SelectedIndexChanged", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private void UpdateDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                string new_word = NewWordBox.Text.Trim();

                if (!string.IsNullOrEmpty(MainForm.BrainFile) &&
                    !string.IsNullOrEmpty(old_word) &&
                    !string.IsNullOrEmpty(new_word))
                {
                    progressBar.Value = 0;

                    bool exists = false;
                    for (int i = 0; i < WordList.Items.Count; i++)
                    {
                        if (new_word == WordList.Items[i].ToString() &&
                            i != index)
                        {
                            exists = true;
                            break;
                        }
                    }

                    List<SQLiteCommand> commands = new List<SQLiteCommand>();

                    SQLiteParameter old_word_parm = new SQLiteParameter("@old_word", old_word);
                    old_word_parm.DbType = DbType.String;

                    SQLiteParameter new_word_parm = new SQLiteParameter("@new_word", new_word);
                    new_word_parm.DbType = DbType.String;

                    string sql = @"UPDATE Inputs SET Input = REPLACE(Input, @old_word, @new_word) WHERE INSTR(Input, @old_word) > 0";
                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(new_word_parm);
                    cmd.Parameters.Add(old_word_parm);
                    commands.Add(cmd);

                    if (exists)
                    {
                        int old_priority = SqlUtil.Get_Word_Priority(old_word);

                        SQLiteParameter old_priority_parm = new SQLiteParameter("@priority", old_priority);
                        old_priority_parm.DbType = DbType.Int32;

                        sql = @"UPDATE Words SET Priority = Priority + @priority WHERE Word = @new_word";
                        cmd = new SQLiteCommand(sql);
                        cmd.Parameters.Add(old_priority_parm);
                        cmd.Parameters.Add(new_word_parm);
                        commands.Add(cmd);

                        sql = @"DELETE FROM Words WHERE Word = @old_word";
                        cmd = new SQLiteCommand(sql);
                        cmd.Parameters.Add(old_word_parm);
                        commands.Add(cmd);
                    }

                    sql = @"UPDATE Words SET Word = @new_word WHERE Word = @old_word";
                    cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(new_word_parm);
                    cmd.Parameters.Add(old_word_parm);
                    commands.Add(cmd);

                    sql = @"UPDATE Outputs SET Input = REPLACE(Input, @old_word, @new_word) WHERE INSTR(Input, @old_word) > 0";
                    cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(new_word_parm);
                    cmd.Parameters.Add(old_word_parm);
                    commands.Add(cmd);

                    sql = @"UPDATE Outputs SET Output = REPLACE(Output, @old_word, @new_word) WHERE INSTR(Output, @old_word) > 0";
                    cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(new_word_parm);
                    cmd.Parameters.Add(old_word_parm);
                    commands.Add(cmd);

                    sql = @"UPDATE Topics SET Input = REPLACE(Input, @old_word, @new_word) WHERE INSTR(Input, @old_word) > 0";
                    cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(new_word_parm);
                    cmd.Parameters.Add(old_word_parm);
                    commands.Add(cmd);

                    sql = @"UPDATE Topics SET Topic = @new_word WHERE Topic = @old_word";
                    cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(new_word_parm);
                    cmd.Parameters.Add(old_word_parm);
                    commands.Add(cmd);

                    sql = @"UPDATE PreWords SET Word = @new_word WHERE Word = @old_word";
                    cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(new_word_parm);
                    cmd.Parameters.Add(old_word_parm);
                    commands.Add(cmd);

                    sql = @"UPDATE PreWords SET PreWord = @new_word WHERE PreWord = @old_word";
                    cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(new_word_parm);
                    cmd.Parameters.Add(old_word_parm);
                    commands.Add(cmd);

                    sql = @"UPDATE ProWords SET Word = @new_word WHERE Word = @old_word";
                    cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(new_word_parm);
                    cmd.Parameters.Add(old_word_parm);
                    commands.Add(cmd);

                    sql = @"UPDATE ProWords SET ProWord = @new_word WHERE ProWord = @old_word";
                    cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(new_word_parm);
                    cmd.Parameters.Add(old_word_parm);
                    commands.Add(cmd);

                    SqlUtil.BulkQuery(commands, MainForm.BrainFile, progressBar, null);
                    commands.Clear();

                    DialogResult dialogResult = MessageBox.Show("\"" + old_word + "\" has been changed to \"" + new_word + "\".", "Spelling Fix Complete", MessageBoxButtons.OK);
                    if (dialogResult == DialogResult.OK)
                    {
                        Close();
                    }
                }
                else if (string.IsNullOrEmpty(MainForm.BrainFile))
                {
                    MessageBox.Show("No brain connected to fix spelling in.");
                }
                else if (string.IsNullOrEmpty(old_word))
                {
                    MessageBox.Show("No word selected.");
                }
                else if (string.IsNullOrEmpty(new_word))
                {
                    MessageBox.Show("New word is missing.");
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Spelling.UpdateDatabase_Click", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        #endregion
    }
}
