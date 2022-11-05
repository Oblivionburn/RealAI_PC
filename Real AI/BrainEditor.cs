using System;
using System.Drawing;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;
using Real_AI.Util;

namespace Real_AI
{
    public partial class BrainEditor : Form
    {
        #region Variables

        string connString;

        int InputsTop = 1;
        int InputIDs_Index;
        List<int> InputIDs = new List<int>();
        SQLiteDataAdapter Adapter_Inputs;
        BindingSource Binding_Inputs;
        DataTable Inputs;
        bool Inputs_Loaded;

        int WordsTop = 1;
        int WordIDs_Index;
        List<int> WordIDs = new List<int>();
        SQLiteDataAdapter Adapter_Words;
        BindingSource Binding_Words;
        DataTable Words;
        bool Words_Loaded;

        int OutputsTop = 1;
        int OutputIDs_Index;
        List<int> OutputIDs = new List<int>();
        SQLiteDataAdapter Adapter_Outputs;
        BindingSource Binding_Outputs;
        DataTable Outputs;
        bool Outputs_Loaded;

        int TopicsTop = 1;
        int TopicIDs_Index;
        List<int> TopicIDs = new List<int>();
        SQLiteDataAdapter Adapter_Topics;
        BindingSource Binding_Topics;
        DataTable Topics;
        bool Topics_Loaded;

        int PreWordsTop = 1;
        int PreWordIDs_Index;
        List<int> PreWordIDs = new List<int>();
        SQLiteDataAdapter Adapter_PreWords;
        BindingSource Binding_PreWords;
        DataTable PreWords;
        bool PreWords_Loaded;

        int ProWordsTop = 1;
        int ProWordIDs_Index;
        List<int> ProWordIDs = new List<int>();
        SQLiteDataAdapter Adapter_ProWords;
        BindingSource Binding_ProWords;
        DataTable ProWords;
        bool ProWords_Loaded;

        #endregion

        #region Constructors

        public BrainEditor()
        {
            FormClosing += BrainEditor_FormClosing;
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void BrainEditor_Load(object sender, EventArgs e)
        {
            SetColors();

            if (!string.IsNullOrEmpty(MainForm.BrainFile))
            {
                InputsTop = 1;

                connString = SqlUtil.GetSqlConnection(MainForm.BrainFile).ConnectionString;

                Inputs_Loaded = false;
                Load_Inputs(connString);
                Inputs_Loaded = true;
                InputsView.FirstDisplayedScrollingRowIndex = 0;
                InputsView.MouseWheel += new MouseEventHandler(InputsBar_Scroll);
                Load_InputIDs();

                Words_Loaded = false;
                Load_Words(connString);
                Words_Loaded = true;
                WordsView.MouseWheel += new MouseEventHandler(WordsBar_Scroll);
                Load_WordIDs();

                Outputs_Loaded = false;
                Load_Outputs(connString);
                Outputs_Loaded = true;
                OutputsView.MouseWheel += new MouseEventHandler(OutputsBar_Scroll);
                Load_OutputIDs();

                Topics_Loaded = false;
                Load_Topics(connString);
                Topics_Loaded = true;
                TopicsView.MouseWheel += new MouseEventHandler(TopicsBar_Scroll);
                Load_TopicIDs();

                PreWords_Loaded = false;
                Load_PreWords(connString);
                PreWords_Loaded = true;
                PreWordsView.MouseWheel += new MouseEventHandler(PreWordsBar_Scroll);
                Load_PreWordIDs();

                ProWords_Loaded = false;
                Load_ProWords(connString);
                ProWords_Loaded = true;
                ProWordsView.MouseWheel += new MouseEventHandler(ProWordsBar_Scroll);
                Load_ProWordIDs();
            }
        }

        private void DisableMouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs h = (HandledMouseEventArgs)e;
            h.Handled = true;
        }

        private void BrainEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            InputsView.MouseWheel -= InputsBar_Scroll;
            WordsView.MouseWheel -= WordsBar_Scroll;
            OutputsView.MouseWheel -= OutputsBar_Scroll;
            TopicsView.MouseWheel -= TopicsBar_Scroll;
            PreWordsView.MouseWheel -= PreWordsBar_Scroll;
            ProWordsView.MouseWheel -= ProWordsBar_Scroll;

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

        private void SetColors()
        {
            SearchBox_Inputs.BackColor = AppUtil.background_control;
            SearchBox_Inputs.ForeColor = AppUtil.text_control;
            SearchButton_Inputs.BackColor = AppUtil.background_control;
            SearchButton_Inputs.ForeColor = AppUtil.text_control;
            SearchBox_Words.BackColor = AppUtil.background_control;
            SearchBox_Words.ForeColor = AppUtil.text_control;
            SearchButton_Words.BackColor = AppUtil.background_control;
            SearchButton_Words.ForeColor = AppUtil.text_control;
            SearchBox_Outputs.BackColor = AppUtil.background_control;
            SearchBox_Outputs.ForeColor = AppUtil.text_control;
            SearchButton_Outputs.BackColor = AppUtil.background_control;
            SearchButton_Outputs.ForeColor = AppUtil.text_control;
            SearchBox_Topics.BackColor = AppUtil.background_control;
            SearchBox_Topics.ForeColor = AppUtil.text_control;
            SearchButton_Topics.BackColor = AppUtil.background_control;
            SearchButton_Topics.ForeColor = AppUtil.text_control;
            SearchBox_PreWords.BackColor = AppUtil.background_control;
            SearchBox_PreWords.ForeColor = AppUtil.text_control;
            SearchButton_PreWords.BackColor = AppUtil.background_control;
            SearchButton_PreWords.ForeColor = AppUtil.text_control;
            SearchBox_ProWords.BackColor = AppUtil.background_control;
            SearchBox_ProWords.ForeColor = AppUtil.text_control;
            SearchButton_ProWords.BackColor = AppUtil.background_control;
            SearchButton_ProWords.ForeColor = AppUtil.text_control;

            panel1.BackColor = AppUtil.background_window;
            panel2.BackColor = AppUtil.background_window;
            panel3.BackColor = AppUtil.background_window;
            panel4.BackColor = AppUtil.background_window;
            panel5.BackColor = AppUtil.background_window;
            panel6.BackColor = AppUtil.background_window;
            panel7.BackColor = AppUtil.background_window;
            panel8.BackColor = AppUtil.background_window;
            panel9.BackColor = AppUtil.background_window;
            panel10.BackColor = AppUtil.background_window;
            panel11.BackColor = AppUtil.background_window;
            panel12.BackColor = AppUtil.background_window;

            InputsView.BackgroundColor = AppUtil.background_window;
            InputsView.RowHeadersDefaultCellStyle.BackColor = AppUtil.background_control;
            InputsView.RowHeadersDefaultCellStyle.ForeColor = AppUtil.text_control;
            InputsView.RowHeadersDefaultCellStyle.SelectionBackColor = AppUtil.highlight_control;
            InputsView.RowHeadersDefaultCellStyle.SelectionForeColor = AppUtil.text_control;
            InputsView.ColumnHeadersDefaultCellStyle.BackColor = AppUtil.background_window;
            InputsView.ColumnHeadersDefaultCellStyle.ForeColor = AppUtil.text_window;
            InputsView.DefaultCellStyle.BackColor = AppUtil.background_control;
            InputsView.DefaultCellStyle.ForeColor = AppUtil.text_control;
            InputsView.DefaultCellStyle.SelectionBackColor = AppUtil.highlight_control;
            InputsView.DefaultCellStyle.SelectionForeColor = AppUtil.text_control;

            WordsView.BackgroundColor = AppUtil.background_window;
            WordsView.RowHeadersDefaultCellStyle.BackColor = AppUtil.background_control;
            WordsView.RowHeadersDefaultCellStyle.ForeColor = AppUtil.text_control;
            WordsView.RowHeadersDefaultCellStyle.SelectionBackColor = AppUtil.highlight_control;
            WordsView.RowHeadersDefaultCellStyle.SelectionForeColor = AppUtil.text_control;
            WordsView.ColumnHeadersDefaultCellStyle.BackColor = AppUtil.background_window;
            WordsView.ColumnHeadersDefaultCellStyle.ForeColor = AppUtil.text_window;
            WordsView.DefaultCellStyle.BackColor = AppUtil.background_control;
            WordsView.DefaultCellStyle.ForeColor = AppUtil.text_control;
            WordsView.DefaultCellStyle.SelectionBackColor = AppUtil.highlight_control;
            WordsView.DefaultCellStyle.SelectionForeColor = AppUtil.text_control;

            OutputsView.BackgroundColor = AppUtil.background_window;
            OutputsView.RowHeadersDefaultCellStyle.BackColor = AppUtil.background_control;
            OutputsView.RowHeadersDefaultCellStyle.ForeColor = AppUtil.text_control;
            OutputsView.RowHeadersDefaultCellStyle.SelectionBackColor = AppUtil.highlight_control;
            OutputsView.RowHeadersDefaultCellStyle.SelectionForeColor = AppUtil.text_control;
            OutputsView.ColumnHeadersDefaultCellStyle.BackColor = AppUtil.background_window;
            OutputsView.ColumnHeadersDefaultCellStyle.ForeColor = AppUtil.text_window;
            OutputsView.DefaultCellStyle.BackColor = AppUtil.background_control;
            OutputsView.DefaultCellStyle.ForeColor = AppUtil.text_control;
            OutputsView.DefaultCellStyle.SelectionBackColor = AppUtil.highlight_control;
            OutputsView.DefaultCellStyle.SelectionForeColor = AppUtil.text_control;

            TopicsView.BackgroundColor = AppUtil.background_window;
            TopicsView.RowHeadersDefaultCellStyle.BackColor = AppUtil.background_control;
            TopicsView.RowHeadersDefaultCellStyle.ForeColor = AppUtil.text_control;
            TopicsView.RowHeadersDefaultCellStyle.SelectionBackColor = AppUtil.highlight_control;
            TopicsView.RowHeadersDefaultCellStyle.SelectionForeColor = AppUtil.text_control;
            TopicsView.ColumnHeadersDefaultCellStyle.BackColor = AppUtil.background_window;
            TopicsView.ColumnHeadersDefaultCellStyle.ForeColor = AppUtil.text_window;
            TopicsView.DefaultCellStyle.BackColor = AppUtil.background_control;
            TopicsView.DefaultCellStyle.ForeColor = AppUtil.text_control;
            TopicsView.DefaultCellStyle.SelectionBackColor = AppUtil.highlight_control;
            TopicsView.DefaultCellStyle.SelectionForeColor = AppUtil.text_control;

            PreWordsView.BackgroundColor = AppUtil.background_window;
            PreWordsView.RowHeadersDefaultCellStyle.BackColor = AppUtil.background_control;
            PreWordsView.RowHeadersDefaultCellStyle.ForeColor = AppUtil.text_control;
            PreWordsView.RowHeadersDefaultCellStyle.SelectionBackColor = AppUtil.highlight_control;
            PreWordsView.RowHeadersDefaultCellStyle.SelectionForeColor = AppUtil.text_control;
            PreWordsView.ColumnHeadersDefaultCellStyle.BackColor = AppUtil.background_window;
            PreWordsView.ColumnHeadersDefaultCellStyle.ForeColor = AppUtil.text_window;
            PreWordsView.DefaultCellStyle.BackColor = AppUtil.background_control;
            PreWordsView.DefaultCellStyle.ForeColor = AppUtil.text_control;
            PreWordsView.DefaultCellStyle.SelectionBackColor = AppUtil.highlight_control;
            PreWordsView.DefaultCellStyle.SelectionForeColor = AppUtil.text_control;

            ProWordsView.BackgroundColor = AppUtil.background_window;
            ProWordsView.RowHeadersDefaultCellStyle.BackColor = AppUtil.background_control;
            ProWordsView.RowHeadersDefaultCellStyle.ForeColor = AppUtil.text_control;
            ProWordsView.RowHeadersDefaultCellStyle.SelectionBackColor = AppUtil.highlight_control;
            ProWordsView.RowHeadersDefaultCellStyle.SelectionForeColor = AppUtil.text_control;
            ProWordsView.ColumnHeadersDefaultCellStyle.BackColor = AppUtil.background_window;
            ProWordsView.ColumnHeadersDefaultCellStyle.ForeColor = AppUtil.text_window;
            ProWordsView.DefaultCellStyle.BackColor = AppUtil.background_control;
            ProWordsView.DefaultCellStyle.ForeColor = AppUtil.text_control;
            ProWordsView.DefaultCellStyle.SelectionBackColor = AppUtil.highlight_control;
            ProWordsView.DefaultCellStyle.SelectionForeColor = AppUtil.text_control;
        }

        #region Inputs

        private void Load_Inputs(string connString)
        {
            Inputs = new DataTable();

            Adapter_Inputs = new SQLiteDataAdapter($"SELECT * FROM Inputs WHERE ID >= {InputsTop} AND ID <= {InputsTop + 100}", connString);
            Adapter_Inputs.Fill(Inputs);

            Binding_Inputs = new BindingSource();
            Binding_Inputs.DataSource = Inputs;

            InputsView.DataSource = Binding_Inputs;
            InputsView.CellValueChanged += Update_Inputs;
            InputsView.UserDeletingRow += Delete_Inputs;

            foreach (DataGridViewColumn col in InputsView.Columns)
            {
                if (col.Name == "ID")
                {
                    col.ReadOnly = true;
                    break;
                }
            }
        }

        private void Load_Inputs(string connString, string search)
        {
            Inputs = new DataTable();

            StringBuilder sb = new StringBuilder(search);
            for (int i = 0; i < sb.Length; i++)
            {
                if (sb[i] == '\'')
                {
                    sb.Insert(i, "'");
                    i++;
                }
            }

            Adapter_Inputs = new SQLiteDataAdapter($"SELECT * FROM Inputs WHERE Input = '{search}'", connString);
            Adapter_Inputs.Fill(Inputs);

            Binding_Inputs = new BindingSource();
            Binding_Inputs.DataSource = Inputs;

            InputsView.DataSource = Binding_Inputs;
            InputsView.CellValueChanged += Update_Inputs;
            InputsView.UserDeletingRow += Delete_Inputs;

            foreach (DataGridViewColumn col in InputsView.Columns)
            {
                if (col.Name == "ID")
                {
                    col.ReadOnly = true;
                    break;
                }
            }
        }

        private void Load_InputIDs()
        {
            DataTable ID_Data = new DataTable();
            Adapter_Inputs = new SQLiteDataAdapter($"SELECT DISTINCT ID FROM Inputs ORDER BY ID", connString);
            Adapter_Inputs.Fill(ID_Data);
            foreach (DataRow row in ID_Data.Rows)
            {
                int id = int.Parse(row[0].ToString());
                if (!InputIDs.Contains(id))
                {
                    InputIDs.Add(id);
                }
            }
        }

        private void Delete_Inputs(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (Inputs.Rows.Count > 0 &&
                Inputs_Loaded)
            {
                DataRow row = Inputs.Rows[e.Row.Index];
                if (row.RowState != DataRowState.Deleted)
                {
                    object[] values = Inputs.Rows[e.Row.Index].ItemArray;
                    int id = int.Parse(values[0].ToString());

                    SQLiteParameter id_parm = new SQLiteParameter("@id", id);
                    id_parm.DbType = DbType.Int32;

                    SQLiteParameter[] parms = { id_parm };
                    SqlUtil.ExecuteQuery_Simple("DELETE FROM Inputs WHERE ID = @id", parms);
                }
            }
        }

        private void Update_Inputs(object sender, DataGridViewCellEventArgs e)
        {
            if (Inputs.Rows.Count > 0 &&
                Inputs_Loaded)
            {
                DataGridView view = (DataGridView)sender;
                DataGridViewRow row = view.Rows[e.RowIndex];
                if (e.RowIndex > Inputs.Rows.Count - 1)
                {
                    string input = row.Cells[1].Value.ToString();
                    if (!string.IsNullOrEmpty(input))
                    {
                        SQLiteParameter input_parm = new SQLiteParameter("@input", input);
                        input_parm.DbType = DbType.String;

                        string priority = row.Cells[2].Value.ToString();
                        if (!string.IsNullOrEmpty(priority))
                        {
                            SQLiteParameter priority_parm = new SQLiteParameter("@priority", int.Parse(priority));
                            priority_parm.DbType = DbType.Int32;

                            SQLiteParameter[] parms = { input_parm, priority_parm };
                            SqlUtil.ExecuteQuery_Simple("INSERT INTO Inputs (Input, Priority) VALUES (@input, @priority)", parms);

                            Inputs_Loaded = false;
                            Load_Inputs(connString);
                            Inputs_Loaded = true;
                        }
                        else
                        {
                            SQLiteParameter[] parms = { input_parm };
                            SqlUtil.ExecuteQuery_Simple("INSERT INTO Inputs (Input) VALUES (@input)", parms);

                            Inputs_Loaded = false;
                            Load_Inputs(connString);
                            Inputs_Loaded = true;
                        }
                    }
                }
                else
                {
                    object[] values = Inputs.Rows[e.RowIndex].ItemArray;
                    int id = int.Parse(values[0].ToString());
                    string input = values[1].ToString();

                    SQLiteParameter id_parm = new SQLiteParameter("@id", id);
                    id_parm.DbType = DbType.Int32;

                    SQLiteParameter input_parm = new SQLiteParameter("@input", input);
                    input_parm.DbType = DbType.String;

                    SQLiteParameter priority_parm = new SQLiteParameter("@priority", 1);
                    priority_parm.DbType = DbType.Int32;

                    string priority = row.Cells[3].Value.ToString();
                    if (!string.IsNullOrEmpty(priority))
                    {
                        priority_parm.Value = int.Parse(priority);
                    }

                    SQLiteParameter[] parms = { id_parm, input_parm, priority_parm };

                    string sql = @"
                    UPDATE Inputs 
                    SET 
                        Input = @input,
                        Priority = @priority
                    WHERE ID = @id";

                    SqlUtil.ExecuteQuery_Simple(sql, parms);
                }
            }
        }

        private void InputsBar_Scroll(object sender, MouseEventArgs e)
        {
            try
            {
                HandledMouseEventArgs mouseEventArgs = (HandledMouseEventArgs)e;
                mouseEventArgs.Handled = true;

                int index = InputsView.FirstDisplayedScrollingRowIndex;

                if (mouseEventArgs.Delta > 0 &&
                    InputIDs_Index > 0)
                {
                    InputIDs_Index--;
                    InputsTop = InputIDs[InputIDs_Index];
                    InputsView.FirstDisplayedScrollingRowIndex = Math.Max(0, index - 1);
                    Load_Inputs(connString);
                }
                else if (mouseEventArgs.Delta < 0 &&
                         InputsView.Rows.Count > 2)
                {
                    InputIDs_Index++;
                    InputsTop = InputIDs[InputIDs_Index];
                    InputsView.FirstDisplayedScrollingRowIndex = index + 1;
                    Load_Inputs(connString);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void SearchButton_Inputs_Click(object sender, EventArgs e)
        {
            InputsTop = 1;
            InputsView.FirstDisplayedScrollingRowIndex = InputsTop - 1;

            if (string.IsNullOrEmpty(SearchBox_Inputs.Text))
            {
                Load_Inputs(connString);
            }
            else
            {
                Load_Inputs(connString, SearchBox_Inputs.Text);
            }
        }

        #endregion

        #region Words

        private void Load_Words(string connString)
        {
            Words = new DataTable();

            Adapter_Words = new SQLiteDataAdapter($"SELECT * FROM Words WHERE ID >= {WordsTop} AND ID <= {WordsTop + 100}", connString);
            Adapter_Words.Fill(Words);

            Binding_Words = new BindingSource();
            Binding_Words.DataSource = Words;

            WordsView.DataSource = Binding_Words;
            WordsView.CellValueChanged += Update_Words;
            WordsView.UserDeletingRow += Delete_Words;

            foreach (DataGridViewColumn col in WordsView.Columns)
            {
                if (col.Name == "ID")
                {
                    col.ReadOnly = true;
                    break;
                }
            }
        }

        private void Load_Words(string connString, string search)
        {
            Words = new DataTable();

            StringBuilder sb = new StringBuilder(search);
            for (int i = 0; i < sb.Length; i++)
            {
                if (sb[i] == '\'')
                {
                    sb.Insert(i, "'");
                    i++;
                }
            }

            Adapter_Words = new SQLiteDataAdapter($"SELECT * FROM Words WHERE Word = '{sb.ToString()}'", connString);
            Adapter_Words.Fill(Words);

            Binding_Words = new BindingSource();
            Binding_Words.DataSource = Words;

            WordsView.DataSource = Binding_Words;
            WordsView.CellValueChanged += Update_Words;
            WordsView.UserDeletingRow += Delete_Words;

            foreach (DataGridViewColumn col in WordsView.Columns)
            {
                if (col.Name == "ID")
                {
                    col.ReadOnly = true;
                    break;
                }
            }
        }

        private void Load_WordIDs()
        {
            DataTable ID_Data = new DataTable();
            Adapter_Words = new SQLiteDataAdapter($"SELECT DISTINCT ID FROM Words ORDER BY ID", connString);
            Adapter_Words.Fill(ID_Data);
            foreach (DataRow row in ID_Data.Rows)
            {
                int id = int.Parse(row[0].ToString());
                if (!WordIDs.Contains(id))
                {
                    WordIDs.Add(id);
                }
            }
        }

        private void Delete_Words(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (Words.Rows.Count > 0 &&
                Words_Loaded)
            {
                DataRow row = Words.Rows[e.Row.Index];
                if (row.RowState != DataRowState.Deleted)
                {
                    object[] values = Words.Rows[e.Row.Index].ItemArray;
                    int id = int.Parse(values[0].ToString());

                    SQLiteParameter id_parm = new SQLiteParameter("@id", id);
                    id_parm.DbType = DbType.Int32;

                    SQLiteParameter[] parms = { id_parm };
                    SqlUtil.ExecuteQuery_Simple("DELETE FROM Words WHERE ID = @id", parms);
                }
            }
        }

        private void Update_Words(object sender, DataGridViewCellEventArgs e)
        {
            if (Words.Rows.Count > 0 &&
                Words_Loaded)
            {
                DataGridView view = (DataGridView)sender;
                DataGridViewRow row = view.Rows[e.RowIndex];
                if (e.RowIndex > Words.Rows.Count - 1)
                {
                    string word = row.Cells[1].Value.ToString();
                    if (!string.IsNullOrEmpty(word))
                    {
                        SQLiteParameter word_parm = new SQLiteParameter("@word", word);
                        word_parm.DbType = DbType.String;

                        string priority = row.Cells[2].Value.ToString();
                        if (!string.IsNullOrEmpty(priority))
                        {
                            SQLiteParameter priority_parm = new SQLiteParameter("@priority", int.Parse(priority));
                            priority_parm.DbType = DbType.Int32;

                            SQLiteParameter[] parms = { word_parm, priority_parm };
                            SqlUtil.ExecuteQuery_Simple("INSERT INTO Words (Word, Priority) VALUES (@word, @priority)", parms);

                            Words_Loaded = false;
                            Load_Words(connString);
                            Words_Loaded = true;
                        }
                        else
                        {
                            SQLiteParameter[] parms = { word_parm };
                            SqlUtil.ExecuteQuery_Simple("INSERT INTO Words (Word) VALUES (@word)", parms);

                            Words_Loaded = false;
                            Load_Words(connString);
                            Words_Loaded = true;
                        }
                    }
                }
                else
                {
                    object[] values = Words.Rows[e.RowIndex].ItemArray;
                    int id = int.Parse(values[0].ToString());
                    string word = values[1].ToString();

                    SQLiteParameter id_parm = new SQLiteParameter("@id", id);
                    id_parm.DbType = DbType.Int32;

                    SQLiteParameter word_parm = new SQLiteParameter("@word", word);
                    word_parm.DbType = DbType.String;

                    SQLiteParameter priority_parm = new SQLiteParameter("@priority", 1);
                    priority_parm.DbType = DbType.Int32;

                    string priority = row.Cells[3].Value.ToString();
                    if (!string.IsNullOrEmpty(priority))
                    {
                        priority_parm.Value = int.Parse(priority);
                    }

                    SQLiteParameter[] parms = { id_parm, word_parm, priority_parm };

                    string sql = @"
                    UPDATE Words 
                    SET 
                        Word = @word,
                        Priority = @priority
                    WHERE ID = @id";

                    SqlUtil.ExecuteQuery_Simple(sql, parms);
                }
            }
        }

        private void WordsBar_Scroll(object sender, MouseEventArgs e)
        {
            try
            {
                HandledMouseEventArgs mouseEventArgs = (HandledMouseEventArgs)e;
                mouseEventArgs.Handled = true;

                int index = WordsView.FirstDisplayedScrollingRowIndex;

                if (mouseEventArgs.Delta > 0 &&
                    WordIDs_Index > 0)
                {
                    WordIDs_Index--;
                    WordsTop = WordIDs[WordIDs_Index];
                    WordsView.FirstDisplayedScrollingRowIndex = Math.Max(0, index - 1);
                    Load_Words(connString);
                }
                else if (mouseEventArgs.Delta < 0 &&
                         WordsView.Rows.Count > 2)
                {
                    WordIDs_Index++;
                    WordsTop = WordIDs[WordIDs_Index];
                    WordsView.FirstDisplayedScrollingRowIndex = index + 1;
                    Load_Words(connString);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void SearchButton_Words_Click(object sender, EventArgs e)
        {
            WordsTop = 1;
            WordsView.FirstDisplayedScrollingRowIndex = WordsTop - 1;

            if (string.IsNullOrEmpty(SearchBox_Words.Text))
            {
                Load_Words(connString);
            }
            else
            {
                Load_Words(connString, SearchBox_Words.Text);
            }
        }

        #endregion

        #region Outputs

        private void Load_Outputs(string connString)
        {
            Outputs = new DataTable();

            Adapter_Outputs = new SQLiteDataAdapter($"SELECT * FROM Outputs WHERE ID >= {OutputsTop} AND ID <= {OutputsTop + 100}", connString);
            Adapter_Outputs.Fill(Outputs);

            Binding_Outputs = new BindingSource();
            Binding_Outputs.DataSource = Outputs;

            OutputsView.DataSource = Binding_Outputs;
            OutputsView.CellValueChanged += Update_Outputs;
            OutputsView.UserDeletingRow += Delete_Outputs;

            foreach (DataGridViewColumn col in OutputsView.Columns)
            {
                if (col.Name == "ID")
                {
                    col.ReadOnly = true;
                    break;
                }
            }
        }

        private void Load_Outputs(string connString, string search)
        {
            Outputs = new DataTable();

            StringBuilder sb = new StringBuilder(search);
            for (int i = 0; i < sb.Length; i++)
            {
                if (sb[i] == '\'')
                {
                    sb.Insert(i, "'");
                    i++;
                }
            }

            Adapter_Outputs = new SQLiteDataAdapter($"SELECT * FROM Outputs WHERE Output = '{search}'", connString);
            Adapter_Outputs.Fill(Outputs);

            Binding_Outputs = new BindingSource();
            Binding_Outputs.DataSource = Outputs;

            OutputsView.DataSource = Binding_Outputs;
            OutputsView.CellValueChanged += Update_Outputs;
            OutputsView.UserDeletingRow += Delete_Outputs;

            foreach (DataGridViewColumn col in OutputsView.Columns)
            {
                if (col.Name == "ID")
                {
                    col.ReadOnly = true;
                    break;
                }
            }
        }

        private void Load_OutputIDs()
        {
            DataTable ID_Data = new DataTable();
            Adapter_Outputs = new SQLiteDataAdapter($"SELECT DISTINCT ID FROM Outputs ORDER BY ID", connString);
            Adapter_Outputs.Fill(ID_Data);
            foreach (DataRow row in ID_Data.Rows)
            {
                int id = int.Parse(row[0].ToString());
                if (!OutputIDs.Contains(id))
                {
                    OutputIDs.Add(id);
                }
            }
        }

        private void Delete_Outputs(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (Outputs.Rows.Count > 0 &&
                Outputs_Loaded)
            {
                DataRow row = Outputs.Rows[e.Row.Index];
                if (row.RowState != DataRowState.Deleted)
                {
                    object[] values = Outputs.Rows[e.Row.Index].ItemArray;
                    int id = int.Parse(values[0].ToString());

                    SQLiteParameter id_parm = new SQLiteParameter("@id", id);
                    id_parm.DbType = DbType.Int32;

                    SQLiteParameter[] parms = { id_parm };
                    SqlUtil.ExecuteQuery_Simple("DELETE FROM Outputs WHERE ID = @id", parms);
                }
            }
        }

        private void Update_Outputs(object sender, DataGridViewCellEventArgs e)
        {
            if (Outputs.Rows.Count > 0 &&
                Outputs_Loaded)
            {
                DataGridView view = (DataGridView)sender;
                DataGridViewRow row = view.Rows[e.RowIndex];
                if (e.RowIndex > Outputs.Rows.Count - 1)
                {
                    string input = row.Cells[1].Value.ToString();
                    string output = row.Cells[2].Value.ToString();

                    if (!string.IsNullOrEmpty(input) &&
                        !string.IsNullOrEmpty(output))
                    {
                        SQLiteParameter input_parm = new SQLiteParameter("@input", input);
                        input_parm.DbType = DbType.String;

                        SQLiteParameter output_parm = new SQLiteParameter("@output", output);
                        output_parm.DbType = DbType.String;

                        string priority = row.Cells[3].Value.ToString();
                        if (!string.IsNullOrEmpty(priority))
                        {
                            SQLiteParameter priority_parm = new SQLiteParameter("@priority", int.Parse(priority));
                            priority_parm.DbType = DbType.Int32;

                            SQLiteParameter[] parms = { input_parm, output_parm, priority_parm };
                            SqlUtil.ExecuteQuery_Simple("INSERT INTO Outputs (Input, Output, Priority) VALUES (@input, @output, @priority)", parms);

                            Outputs_Loaded = false;
                            Load_Outputs(connString);
                            Outputs_Loaded = true;
                        }
                        else
                        {
                            SQLiteParameter[] parms = { input_parm, output_parm };
                            SqlUtil.ExecuteQuery_Simple("INSERT INTO Outputs (Input, Output) VALUES (@input, @output)", parms);

                            Outputs_Loaded = false;
                            Load_Outputs(connString);
                            Outputs_Loaded = true;
                        }
                    }
                }
                else
                {
                    object[] values = Outputs.Rows[e.RowIndex].ItemArray;
                    int id = int.Parse(values[0].ToString());
                    string input = values[1].ToString();
                    string output = values[2].ToString();

                    SQLiteParameter id_parm = new SQLiteParameter("@id", id);
                    id_parm.DbType = DbType.Int32;

                    SQLiteParameter input_parm = new SQLiteParameter("@input", input);
                    input_parm.DbType = DbType.String;

                    SQLiteParameter output_parm = new SQLiteParameter("@output", output);
                    output_parm.DbType = DbType.String;

                    SQLiteParameter priority_parm = new SQLiteParameter("@priority", 1);
                    priority_parm.DbType = DbType.Int32;

                    string priority = row.Cells[3].Value.ToString();
                    if (!string.IsNullOrEmpty(priority))
                    {
                        priority_parm.Value = int.Parse(priority);
                    }

                    SQLiteParameter[] parms = { id_parm, input_parm, output_parm, priority_parm };

                    string sql = @"
                    UPDATE Outputs 
                    SET 
                        Input = @input,
                        Output = @output,
                        Priority = @priority
                    WHERE ID = @id";

                    SqlUtil.ExecuteQuery_Simple(sql, parms);
                }
            }
        }

        private void OutputsBar_Scroll(object sender, MouseEventArgs e)
        {
            try
            {
                HandledMouseEventArgs mouseEventArgs = (HandledMouseEventArgs)e;
                mouseEventArgs.Handled = true;

                int index = OutputsView.FirstDisplayedScrollingRowIndex;

                if (mouseEventArgs.Delta > 0 &&
                    OutputIDs_Index > 0)
                {
                    OutputIDs_Index--;
                    OutputsTop = OutputIDs[OutputIDs_Index];
                    OutputsView.FirstDisplayedScrollingRowIndex = Math.Max(0, index - 1);
                    Load_Outputs(connString);
                }
                else if (mouseEventArgs.Delta < 0 &&
                         OutputsView.Rows.Count > 2)
                {
                    OutputIDs_Index++;
                    OutputsTop = OutputIDs[OutputIDs_Index];
                    OutputsView.FirstDisplayedScrollingRowIndex = index + 1;
                    Load_Outputs(connString);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void SearchButton_Outputs_Click(object sender, EventArgs e)
        {
            OutputsTop = 1;
            OutputsView.FirstDisplayedScrollingRowIndex = OutputsTop - 1;

            if (string.IsNullOrEmpty(SearchBox_Outputs.Text))
            {
                Load_Outputs(connString);
            }
            else
            {
                Load_Outputs(connString, SearchBox_Outputs.Text);
            }
        }

        #endregion

        #region Topics

        private void Load_Topics(string connString)
        {
            try
            {
                Topics = new DataTable();

                Adapter_Topics = new SQLiteDataAdapter($"SELECT * FROM Topics WHERE ID >= {TopicsTop} AND ID <= {TopicsTop + 100}", connString);
                Adapter_Topics.Fill(Topics);

                Binding_Topics = new BindingSource();
                Binding_Topics.DataSource = Topics;

                TopicsView.DataSource = Binding_Topics;
                TopicsView.CellValueChanged += Update_Topics;
                TopicsView.UserDeletingRow += Delete_Topics;

                foreach (DataGridViewColumn col in TopicsView.Columns)
                {
                    if (col.Name == "ID")
                    {
                        col.ReadOnly = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void Load_Topics(string connString, string search)
        {
            Topics = new DataTable();

            StringBuilder sb = new StringBuilder(search);
            for (int i = 0; i < sb.Length; i++)
            {
                if (sb[i] == '\'')
                {
                    sb.Insert(i, "'");
                    i++;
                }
            }

            Adapter_Topics = new SQLiteDataAdapter($"SELECT * FROM Topics WHERE Topic = '{search}'", connString);
            Adapter_Topics.Fill(Topics);

            Binding_Topics = new BindingSource();
            Binding_Topics.DataSource = Topics;

            TopicsView.DataSource = Binding_Topics;
            TopicsView.CellValueChanged += Update_Topics;
            TopicsView.UserDeletingRow += Delete_Topics;

            foreach (DataGridViewColumn col in TopicsView.Columns)
            {
                if (col.Name == "ID")
                {
                    col.ReadOnly = true;
                    break;
                }
            }
        }

        private void Load_TopicIDs()
        {
            DataTable ID_Data = new DataTable();
            Adapter_Topics = new SQLiteDataAdapter($"SELECT DISTINCT ID FROM Topics ORDER BY ID", connString);
            Adapter_Topics.Fill(ID_Data);
            foreach (DataRow row in ID_Data.Rows)
            {
                int id = int.Parse(row[0].ToString());
                if (!TopicIDs.Contains(id))
                {
                    TopicIDs.Add(id);
                }
            }
        }

        private void Delete_Topics(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (Topics.Rows.Count > 0 &&
                Topics_Loaded)
            {
                DataRow row = Topics.Rows[e.Row.Index];
                if (row.RowState != DataRowState.Deleted)
                {
                    object[] values = Topics.Rows[e.Row.Index].ItemArray;
                    int id = int.Parse(values[0].ToString());

                    SQLiteParameter id_parm = new SQLiteParameter("@id", id);
                    id_parm.DbType = DbType.Int32;

                    SQLiteParameter[] parms = { id_parm };
                    SqlUtil.ExecuteQuery_Simple("DELETE FROM Topics WHERE ID = @id", parms);
                }
            }
        }

        private void Update_Topics(object sender, DataGridViewCellEventArgs e)
        {
            if (Topics.Rows.Count > 0 &&
                Topics_Loaded)
            {
                DataGridView view = (DataGridView)sender;
                DataGridViewRow row = view.Rows[e.RowIndex];
                if (e.RowIndex > Topics.Rows.Count - 1)
                {
                    string input = row.Cells[1].Value.ToString();
                    string topic = row.Cells[2].Value.ToString();

                    if (!string.IsNullOrEmpty(input) &&
                        !string.IsNullOrEmpty(topic))
                    {
                        SQLiteParameter input_parm = new SQLiteParameter("@input", input);
                        input_parm.DbType = DbType.String;

                        SQLiteParameter topic_parm = new SQLiteParameter("@topic", topic);
                        topic_parm.DbType = DbType.String;

                        string priority = row.Cells[3].Value.ToString();
                        if (!string.IsNullOrEmpty(priority))
                        {
                            SQLiteParameter priority_parm = new SQLiteParameter("@priority", int.Parse(priority));
                            priority_parm.DbType = DbType.Int32;

                            SQLiteParameter[] parms = { input_parm, topic_parm, priority_parm };
                            SqlUtil.ExecuteQuery_Simple("INSERT INTO Topics (Input, Topic, Priority) VALUES (@input, @topic, @priority)", parms);

                            Topics_Loaded = false;
                            Load_Topics(connString);
                            Topics_Loaded = true;
                        }
                        else
                        {
                            SQLiteParameter[] parms = { input_parm, topic_parm };
                            SqlUtil.ExecuteQuery_Simple("INSERT INTO Topics (Input, Topic) VALUES (@input, @topic)", parms);

                            Topics_Loaded = false;
                            Load_Topics(connString);
                            Topics_Loaded = true;
                        }
                    }
                }
                else
                {
                    object[] values = Topics.Rows[e.RowIndex].ItemArray;
                    int id = int.Parse(values[0].ToString());
                    string input = values[1].ToString();
                    string topic = values[2].ToString();

                    SQLiteParameter id_parm = new SQLiteParameter("@id", id);
                    id_parm.DbType = DbType.Int32;

                    SQLiteParameter input_parm = new SQLiteParameter("@input", input);
                    input_parm.DbType = DbType.String;

                    SQLiteParameter topic_parm = new SQLiteParameter("@topic", topic);
                    topic_parm.DbType = DbType.String;

                    SQLiteParameter priority_parm = new SQLiteParameter("@priority", 1);
                    priority_parm.DbType = DbType.Int32;

                    string priority = row.Cells[3].Value.ToString();
                    if (!string.IsNullOrEmpty(priority))
                    {
                        priority_parm.Value = int.Parse(priority);
                    }

                    SQLiteParameter[] parms = { id_parm, input_parm, topic_parm, priority_parm };

                    string sql = @"
                    UPDATE Topics 
                    SET 
                        Input = @input,
                        Topic = @topic,
                        Priority = @priority
                    WHERE ID = @id";

                    SqlUtil.ExecuteQuery_Simple(sql, parms);
                }
            }
        }

        private void TopicsBar_Scroll(object sender, MouseEventArgs e)
        {
            try
            {
                HandledMouseEventArgs mouseEventArgs = (HandledMouseEventArgs)e;
                mouseEventArgs.Handled = true;

                int index = TopicsView.FirstDisplayedScrollingRowIndex;

                if (mouseEventArgs.Delta > 0 &&
                    TopicIDs_Index > 0)
                {
                    TopicIDs_Index--;
                    TopicsTop = TopicIDs[TopicIDs_Index];
                    TopicsView.FirstDisplayedScrollingRowIndex = Math.Max(0, index - 1);
                    Load_Topics(connString);
                }
                else if (mouseEventArgs.Delta < 0 &&
                         TopicsView.Rows.Count > 2)
                {
                    TopicIDs_Index++;
                    TopicsTop = TopicIDs[TopicIDs_Index];
                    TopicsView.FirstDisplayedScrollingRowIndex = index + 1;
                    Load_Topics(connString);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void SearchButton_Topics_Click(object sender, EventArgs e)
        {
            TopicsTop = 1;
            TopicsView.FirstDisplayedScrollingRowIndex = TopicsTop - 1;

            if (string.IsNullOrEmpty(SearchBox_Topics.Text))
            {
                Load_Topics(connString);
            }
            else
            {
                Load_Topics(connString, SearchBox_Topics.Text);
            }
        }

        #endregion

        #region PreWords

        private void Load_PreWords(string connString)
        {
            PreWords = new DataTable();

            Adapter_PreWords = new SQLiteDataAdapter($"SELECT * FROM PreWords WHERE ID >= {PreWordsTop} AND ID <= {PreWordsTop + 100}", connString);
            Adapter_PreWords.Fill(PreWords);

            Binding_PreWords = new BindingSource();
            Binding_PreWords.DataSource = PreWords;

            PreWordsView.DataSource = Binding_PreWords;
            PreWordsView.CellValueChanged += Update_PreWords;
            PreWordsView.UserDeletingRow += Delete_PreWords;

            foreach (DataGridViewColumn col in PreWordsView.Columns)
            {
                if (col.Name == "ID")
                {
                    col.ReadOnly = true;
                    break;
                }
            }
        }

        private void Load_PreWords(string connString, string search)
        {
            PreWords = new DataTable();

            StringBuilder sb = new StringBuilder(search);
            for (int i = 0; i < sb.Length; i++)
            {
                if (sb[i] == '\'')
                {
                    sb.Insert(i, "'");
                    i++;
                }
            }

            Adapter_PreWords = new SQLiteDataAdapter($"SELECT * FROM PreWords WHERE PreWord = '{search}'", connString);
            Adapter_PreWords.Fill(PreWords);

            Binding_PreWords = new BindingSource();
            Binding_PreWords.DataSource = PreWords;

            PreWordsView.DataSource = Binding_PreWords;
            PreWordsView.CellValueChanged += Update_PreWords;
            PreWordsView.UserDeletingRow += Delete_PreWords;

            foreach (DataGridViewColumn col in PreWordsView.Columns)
            {
                if (col.Name == "ID")
                {
                    col.ReadOnly = true;
                    break;
                }
            }
        }

        private void Load_PreWordIDs()
        {
            DataTable ID_Data = new DataTable();
            Adapter_PreWords = new SQLiteDataAdapter($"SELECT DISTINCT ID FROM PreWords ORDER BY ID", connString);
            Adapter_PreWords.Fill(ID_Data);
            foreach (DataRow row in ID_Data.Rows)
            {
                int id = int.Parse(row[0].ToString());
                if (!PreWordIDs.Contains(id))
                {
                    PreWordIDs.Add(id);
                }
            }
        }

        private void Delete_PreWords(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (PreWords.Rows.Count > 0 &&
                PreWords_Loaded)
            {
                DataRow row = PreWords.Rows[e.Row.Index];
                if (row.RowState != DataRowState.Deleted)
                {
                    object[] values = PreWords.Rows[e.Row.Index].ItemArray;
                    int id = int.Parse(values[0].ToString());

                    SQLiteParameter id_parm = new SQLiteParameter("@id", id);
                    id_parm.DbType = DbType.Int32;

                    SQLiteParameter[] parms = { id_parm };
                    SqlUtil.ExecuteQuery_Simple("DELETE FROM PreWords WHERE ID = @id", parms);
                }
            }
        }

        private void Update_PreWords(object sender, DataGridViewCellEventArgs e)
        {
            if (PreWords.Rows.Count > 0 &&
                PreWords_Loaded)
            {
                DataGridView view = (DataGridView)sender;
                DataGridViewRow row = view.Rows[e.RowIndex];
                if (e.RowIndex > PreWords.Rows.Count - 1)
                {
                    string word = row.Cells[1].Value.ToString();
                    string pre_word = row.Cells[2].Value.ToString();

                    if (!string.IsNullOrEmpty(word) &&
                        !string.IsNullOrEmpty(pre_word))
                    {
                        SQLiteParameter word_parm = new SQLiteParameter("@word", word);
                        word_parm.DbType = DbType.String;

                        SQLiteParameter pre_word_parm = new SQLiteParameter("@pre_word", pre_word);
                        pre_word_parm.DbType = DbType.String;

                        List<SQLiteParameter> parms_list = new List<SQLiteParameter>();
                        parms_list.Add(word_parm);
                        parms_list.Add(pre_word_parm);

                        string sql_insert = "INSERT INTO PreWords (Word, PreWord";
                        string sql_values = "VALUES (@word, @pre_word";

                        string priority = row.Cells[3].Value.ToString();
                        if (!string.IsNullOrEmpty(priority))
                        {
                            SQLiteParameter priority_parm = new SQLiteParameter("@priority", int.Parse(priority));
                            priority_parm.DbType = DbType.Int32;

                            parms_list.Add(priority_parm);

                            sql_insert += ", Priority";
                            sql_values += ", @priority";
                        }

                        string distance = row.Cells[4].Value.ToString();
                        if (!string.IsNullOrEmpty(distance))
                        {
                            SQLiteParameter distance_parm = new SQLiteParameter("@distance", int.Parse(distance));
                            distance_parm.DbType = DbType.Int32;
                            parms_list.Add(distance_parm);
                        }
                        else
                        {
                            SQLiteParameter distance_parm = new SQLiteParameter("@distance", 1);
                            distance_parm.DbType = DbType.Int32;
                            parms_list.Add(distance_parm);
                        }
                        sql_insert += ", Distance";
                        sql_values += ", @distance";

                        sql_insert += ")";
                        sql_values += ")";

                        SqlUtil.ExecuteQuery_Simple(sql_insert + sql_values, parms_list.ToArray());

                        PreWords_Loaded = false;
                        Load_PreWords(connString);
                        PreWords_Loaded = true;
                    }
                }
                else
                {
                    object[] values = PreWords.Rows[e.RowIndex].ItemArray;
                    int id = int.Parse(values[0].ToString());
                    string word = values[1].ToString();
                    string pre_word = values[2].ToString();

                    SQLiteParameter id_parm = new SQLiteParameter("@id", id);
                    id_parm.DbType = DbType.Int32;

                    SQLiteParameter word_parm = new SQLiteParameter("@word", word);
                    word_parm.DbType = DbType.String;

                    SQLiteParameter pre_word_parm = new SQLiteParameter("@pre_word", pre_word);
                    pre_word_parm.DbType = DbType.String;

                    SQLiteParameter priority_parm = new SQLiteParameter("@priority", 1);
                    priority_parm.DbType = DbType.Int32;

                    string priority = row.Cells[3].Value.ToString();
                    if (!string.IsNullOrEmpty(priority))
                    {
                        priority_parm.Value = int.Parse(priority);
                    }

                    SQLiteParameter distance_parm = new SQLiteParameter("@distance", 1);
                    distance_parm.DbType = DbType.Int32;

                    string distance = row.Cells[4].Value.ToString();
                    if (!string.IsNullOrEmpty(distance))
                    {
                        distance_parm.Value = int.Parse(distance);
                    }

                    SQLiteParameter[] parms = { id_parm, word_parm, pre_word_parm, priority_parm, distance_parm };

                    string sql = @"
                    UPDATE PreWords 
                    SET 
                        Word = @word,
                        PreWord = @pre_word,
                        Priority = @priority,
                        Distance = @distance
                    WHERE ID = @id";

                    SqlUtil.ExecuteQuery_Simple(sql, parms);
                }
            }
        }

        private void PreWordsBar_Scroll(object sender, MouseEventArgs e)
        {
            try
            {
                HandledMouseEventArgs mouseEventArgs = (HandledMouseEventArgs)e;
                mouseEventArgs.Handled = true;

                int index = PreWordsView.FirstDisplayedScrollingRowIndex;

                if (mouseEventArgs.Delta > 0 &&
                    PreWordIDs_Index > 0)
                {
                    PreWordIDs_Index--;
                    PreWordsTop = PreWordIDs[PreWordIDs_Index];
                    PreWordsView.FirstDisplayedScrollingRowIndex = Math.Max(0, index - 1);
                    Load_PreWords(connString);
                }
                else if (mouseEventArgs.Delta < 0 &&
                         PreWordsView.Rows.Count > 2)
                {
                    PreWordIDs_Index++;
                    PreWordsTop = PreWordIDs[PreWordIDs_Index];
                    PreWordsView.FirstDisplayedScrollingRowIndex = index + 1;
                    Load_PreWords(connString);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void SearchButton_PreWords_Click(object sender, EventArgs e)
        {
            PreWordsTop = 1;
            PreWordsView.FirstDisplayedScrollingRowIndex = PreWordsTop - 1;

            if (string.IsNullOrEmpty(SearchBox_PreWords.Text))
            {
                Load_PreWords(connString);
            }
            else
            {
                Load_PreWords(connString, SearchBox_PreWords.Text);
            }
        }

        #endregion

        #region ProWords

        private void Load_ProWords(string connString)
        {
            ProWords = new DataTable();

            Adapter_ProWords = new SQLiteDataAdapter($"SELECT * FROM ProWords WHERE ID >= {ProWordsTop} AND ID <= {ProWordsTop + 100}", connString);
            Adapter_ProWords.Fill(ProWords);

            Binding_ProWords = new BindingSource();
            Binding_ProWords.DataSource = ProWords;

            ProWordsView.DataSource = Binding_ProWords;
            ProWordsView.CellValueChanged += Update_ProWords;
            ProWordsView.UserDeletingRow += Delete_ProWords;

            foreach (DataGridViewColumn col in ProWordsView.Columns)
            {
                if (col.Name == "ID")
                {
                    col.ReadOnly = true;
                    break;
                }
            }
        }

        private void Load_ProWords(string connString, string search)
        {
            ProWords = new DataTable();

            StringBuilder sb = new StringBuilder(search);
            for (int i = 0; i < sb.Length; i++)
            {
                if (sb[i] == '\'')
                {
                    sb.Insert(i, "'");
                    i++;
                }
            }

            Adapter_ProWords = new SQLiteDataAdapter($"SELECT * FROM ProWords WHERE ProWord = '{search}'", connString);
            Adapter_ProWords.Fill(ProWords);

            Binding_ProWords = new BindingSource();
            Binding_ProWords.DataSource = ProWords;

            ProWordsView.DataSource = Binding_ProWords;
            ProWordsView.CellValueChanged += Update_ProWords;
            ProWordsView.UserDeletingRow += Delete_ProWords;

            foreach (DataGridViewColumn col in ProWordsView.Columns)
            {
                if (col.Name == "ID")
                {
                    col.ReadOnly = true;
                    break;
                }
            }
        }

        private void Load_ProWordIDs()
        {
            DataTable ID_Data = new DataTable();
            Adapter_ProWords = new SQLiteDataAdapter($"SELECT DISTINCT ID FROM ProWords ORDER BY ID", connString);
            Adapter_ProWords.Fill(ID_Data);
            foreach (DataRow row in ID_Data.Rows)
            {
                int id = int.Parse(row[0].ToString());
                if (!ProWordIDs.Contains(id))
                {
                    ProWordIDs.Add(id);
                }
            }
        }

        private void Update_ProWords(object sender, DataGridViewCellEventArgs e)
        {
            if (ProWords.Rows.Count > 0 &&
                ProWords_Loaded)
            {
                DataGridView view = (DataGridView)sender;
                DataGridViewRow row = view.Rows[e.RowIndex];
                if (e.RowIndex > ProWords.Rows.Count - 1)
                {
                    string word = row.Cells[1].Value.ToString();
                    string pro_word = row.Cells[2].Value.ToString();

                    if (!string.IsNullOrEmpty(word) &&
                        !string.IsNullOrEmpty(pro_word))
                    {
                        SQLiteParameter word_parm = new SQLiteParameter("@word", word);
                        word_parm.DbType = DbType.String;

                        SQLiteParameter pro_word_parm = new SQLiteParameter("@pro_word", pro_word);
                        pro_word_parm.DbType = DbType.String;

                        List<SQLiteParameter> parms_list = new List<SQLiteParameter>();
                        parms_list.Add(word_parm);
                        parms_list.Add(pro_word_parm);

                        string sql_insert = "INSERT INTO ProWords (Word, ProWord";
                        string sql_values = "VALUES (@word, @pro_word";

                        string priority = row.Cells[3].Value.ToString();
                        if (!string.IsNullOrEmpty(priority))
                        {
                            SQLiteParameter priority_parm = new SQLiteParameter("@priority", int.Parse(priority));
                            priority_parm.DbType = DbType.Int32;

                            parms_list.Add(priority_parm);

                            sql_insert += ", Priority";
                            sql_values += ", @priority";
                        }

                        string distance = row.Cells[4].Value.ToString();
                        if (!string.IsNullOrEmpty(distance))
                        {
                            SQLiteParameter distance_parm = new SQLiteParameter("@distance", int.Parse(distance));
                            distance_parm.DbType = DbType.Int32;
                            parms_list.Add(distance_parm);
                        }
                        else
                        {
                            SQLiteParameter distance_parm = new SQLiteParameter("@distance", 1);
                            distance_parm.DbType = DbType.Int32;
                            parms_list.Add(distance_parm);
                        }
                        sql_insert += ", Distance";
                        sql_values += ", @distance";

                        sql_insert += ")";
                        sql_values += ")";

                        SqlUtil.ExecuteQuery_Simple(sql_insert + sql_values, parms_list.ToArray());

                        ProWords_Loaded = false;
                        Load_ProWords(connString);
                        ProWords_Loaded = true;
                    }
                }
                else
                {
                    object[] values = ProWords.Rows[e.RowIndex].ItemArray;
                    int id = int.Parse(values[0].ToString());
                    string word = values[1].ToString();
                    string pro_word = values[2].ToString();

                    SQLiteParameter id_parm = new SQLiteParameter("@id", id);
                    id_parm.DbType = DbType.Int32;

                    SQLiteParameter word_parm = new SQLiteParameter("@word", word);
                    word_parm.DbType = DbType.String;

                    SQLiteParameter pro_word_parm = new SQLiteParameter("@pro_word", pro_word);
                    pro_word_parm.DbType = DbType.String;

                    SQLiteParameter priority_parm = new SQLiteParameter("@priority", 1);
                    priority_parm.DbType = DbType.Int32;

                    string priority = row.Cells[3].Value.ToString();
                    if (!string.IsNullOrEmpty(priority))
                    {
                        priority_parm.Value = int.Parse(priority);
                    }

                    SQLiteParameter distance_parm = new SQLiteParameter("@distance", 1);
                    distance_parm.DbType = DbType.Int32;

                    string distance = row.Cells[4].Value.ToString();
                    if (!string.IsNullOrEmpty(distance))
                    {
                        distance_parm.Value = int.Parse(distance);
                    }

                    SQLiteParameter[] parms = { id_parm, word_parm, priority_parm, distance_parm };

                    string sql = @"
                    UPDATE ProWords 
                    SET 
                        Word = @word,
                        ProWord = @pro_word,
                        Priority = @priority,
                        Distance = @distance
                    WHERE ID = @id";

                    SqlUtil.ExecuteQuery_Simple(sql, parms);
                }
            }
        }

        private void Delete_ProWords(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (ProWords.Rows.Count > 0 &&
                ProWords_Loaded)
            {
                DataRow row = ProWords.Rows[e.Row.Index];
                if (row.RowState != DataRowState.Deleted)
                {
                    object[] values = ProWords.Rows[e.Row.Index].ItemArray;
                    int id = int.Parse(values[0].ToString());

                    SQLiteParameter id_parm = new SQLiteParameter("@id", id);
                    id_parm.DbType = DbType.Int32;

                    SQLiteParameter[] parms = { id_parm };
                    SqlUtil.ExecuteQuery_Simple("DELETE FROM ProWords WHERE ID = @id", parms);
                }
            }
        }

        private void ProWordsBar_Scroll(object sender, MouseEventArgs e)
        {
            try
            {
                HandledMouseEventArgs mouseEventArgs = (HandledMouseEventArgs)e;
                mouseEventArgs.Handled = true;

                int index = ProWordsView.FirstDisplayedScrollingRowIndex;

                if (mouseEventArgs.Delta > 0 &&
                    ProWordIDs_Index > 0)
                {
                    ProWordIDs_Index--;
                    ProWordsTop = ProWordIDs[ProWordIDs_Index];
                    ProWordsView.FirstDisplayedScrollingRowIndex = Math.Max(0, index - 1);
                    Load_ProWords(connString);
                }
                else if (mouseEventArgs.Delta < 0 &&
                         ProWordsView.Rows.Count > 2)
                {
                    ProWordIDs_Index++;
                    ProWordsTop = ProWordIDs[ProWordIDs_Index];
                    ProWordsView.FirstDisplayedScrollingRowIndex = index + 1;
                    Load_ProWords(connString);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void SearchButton_ProWords_Click(object sender, EventArgs e)
        {
            ProWordsTop = 1;
            ProWordsView.FirstDisplayedScrollingRowIndex = ProWordsTop - 1;

            if (string.IsNullOrEmpty(SearchBox_ProWords.Text))
            {
                Load_ProWords(connString);
            }
            else
            {
                Load_ProWords(connString, SearchBox_ProWords.Text);
            }
        }

        #endregion

        #endregion
    }
}
