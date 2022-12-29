using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Threading;

namespace Real_AI.Util
{
    public static class SqlUtil
    {
        public static SQLiteConnection BrainConn = new SQLiteConnection();

        public static void Init()
        {
            try
            {
                List<SQLiteCommand> commands = new List<SQLiteCommand>();

                string sql = @"
                CREATE TABLE Inputs
                (
                    ID INTEGER PRIMARY KEY,
                    Input TEXT,
                    Priority INTEGER DEFAULT 1
                )";
                SQLiteCommand cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"CREATE INDEX idx_Inputs ON Inputs (input)";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"
                CREATE TABLE Words
                (
                    ID INTEGER PRIMARY KEY,
                    Word TEXT,
                    Priority INTEGER DEFAULT 1
                )";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"CREATE INDEX idx_Words ON Words (Word)";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"
                CREATE TABLE Outputs
                (
                    ID INTEGER PRIMARY KEY,
                    Input TEXT,
                    Output TEXT,
                    Priority INTEGER DEFAULT 1
                )";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"CREATE INDEX idx_Outputs ON Outputs (Input, Output)";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"
                CREATE TABLE Topics
                (
                    ID INTEGER PRIMARY KEY,
                    Input TEXT,
                    Topic TEXT,
                    Priority INTEGER DEFAULT 1
                )";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"CREATE INDEX idx_Topics ON Topics (Input, Topic)";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"
                CREATE TABLE PreWords
                (
                    ID INTEGER PRIMARY KEY,
                    Word TEXT,
                    PreWord TEXT,
                    Priority INTEGER DEFAULT 1,
                    Distance INTEGER
                )";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"CREATE INDEX idx_PreWords ON PreWords (Word, PreWord, Distance)";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"
                CREATE TABLE ProWords
                (
                    ID INTEGER PRIMARY KEY,
                    Word TEXT,
                    ProWord TEXT,
                    Priority INTEGER DEFAULT 1,
                    Distance INTEGER
                )";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"CREATE INDEX idx_ProWords ON ProWords (Word, ProWord, Distance)";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                BulkQuery(commands, true);
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Init", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        public static void Init(string brainFile)
        {
            try
            {
                List<SQLiteCommand> commands = new List<SQLiteCommand>();

                string sql = @"
                CREATE TABLE Inputs
                (
                    ID INTEGER PRIMARY KEY,
                    Input TEXT,
                    Priority INTEGER DEFAULT 1
                )";
                SQLiteCommand cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"CREATE INDEX idx_Inputs ON Inputs (input)";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"
                CREATE TABLE Words
                (
                    ID INTEGER PRIMARY KEY,
                    Word TEXT,
                    Priority INTEGER DEFAULT 1
                )";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"CREATE INDEX idx_Words ON Words (Word)";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"
                CREATE TABLE Outputs
                (
                    ID INTEGER PRIMARY KEY,
                    Input TEXT,
                    Output TEXT,
                    Priority INTEGER DEFAULT 1
                )";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"CREATE INDEX idx_Outputs ON Outputs (Input, Output)";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"
                CREATE TABLE Topics
                (
                    ID INTEGER PRIMARY KEY,
                    Input TEXT,
                    Topic TEXT,
                    Priority INTEGER DEFAULT 1
                )";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"CREATE INDEX idx_Topics ON Topics (Input, Topic)";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"
                CREATE TABLE PreWords
                (
                    ID INTEGER PRIMARY KEY,
                    Word TEXT,
                    PreWord TEXT,
                    Priority INTEGER DEFAULT 1,
                    Distance INTEGER
                )";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"CREATE INDEX idx_PreWords ON PreWords (Word, PreWord, Distance)";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"
                CREATE TABLE ProWords
                (
                    ID INTEGER PRIMARY KEY,
                    Word TEXT,
                    ProWord TEXT,
                    Priority INTEGER DEFAULT 1,
                    Distance INTEGER
                )";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                sql = @"CREATE INDEX idx_ProWords ON ProWords (Word, ProWord, Distance)";
                cmd = new SQLiteCommand(sql);
                commands.Add(cmd);

                BulkQuery(commands, brainFile, null, null);
                commands.Clear();
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Init", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        public static SQLiteConnection GetSqlConnection(string filePath)
        {
            try
            {
                return new SQLiteConnection("Data Source=" + filePath + "; Version=3;");
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.GetSqlConnection", ex.Source, ex.Message, ex.StackTrace);
            }

            return null;
        }

        public static void ExecuteQuery_Simple(string sql, SQLiteParameter[] parameters)
        {
            try
            {
                string connString = GetSqlConnection(MainForm.BrainFile).ConnectionString;
                using (SQLiteConnection con = new SQLiteConnection(connString))
                {
                    con.Open();

                    using (SQLiteTransaction transaction = con.BeginTransaction())
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                        {
                            if (parameters != null)
                            {
                                if (parameters.Length > 0)
                                {
                                    for (int i = 0; i < parameters.Length; i++)
                                    {
                                        if (parameters[i] != null)
                                        {
                                            cmd.Parameters.Add(parameters[i]);
                                        }
                                    }
                                }
                            }

                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.ExecuteQuery_Simple", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        public static bool BulkQuery(List<SQLiteCommand> commands, bool update_progress)
        {
            int count = 0;
            int total = commands.Count;

            if (update_progress)
            {
                AppUtil.UpdateProgress(MainForm.progressDetail, 0);
            }

            try
            {
                string connString = GetSqlConnection(MainForm.BrainFile).ConnectionString;
                using (SQLiteConnection con = new SQLiteConnection(connString))
                {
                    con.Open();

                    using (SQLiteTransaction transaction = con.BeginTransaction())
                    {
                        foreach (SQLiteCommand command in commands)
                        {
                            using (SQLiteCommand cmd = new SQLiteCommand(command.CommandText, con))
                            {
                                foreach (SQLiteParameter existing in command.Parameters)
                                {
                                    SQLiteParameter parm = new SQLiteParameter();
                                    parm.ParameterName = existing.ParameterName;
                                    parm.Value = existing.Value;
                                    parm.DbType = existing.DbType;
                                    cmd.Parameters.Add(parm);
                                }

                                cmd.ExecuteNonQuery();

                                count++;

                                if (update_progress)
                                {
                                    AppUtil.UpdateProgress(MainForm.progressDetail, (count * 100) / total);
                                }
                            }
                        }

                        transaction.Commit();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.BulkQuery", ex.Source, ex.Message, ex.StackTrace);
            }

            return false;
        }

        public static void BulkQuery(List<SQLiteCommand> commands, string brainFile, TextProgressBar progressBar, CancellationTokenSource TokenSource)
        {
            int count = 0;
            int total = commands.Count;

            if (progressBar != null)
            {
                AppUtil.UpdateProgress(TokenSource, progressBar, "(" + count + "/" + total + ")");
                AppUtil.UpdateProgress(TokenSource, progressBar, 0);
            }

            try
            {
                string connString = GetSqlConnection(brainFile).ConnectionString;
                using (SQLiteConnection con = new SQLiteConnection(connString))
                {
                    con.Open();

                    using (SQLiteTransaction transaction = con.BeginTransaction())
                    {
                        foreach (SQLiteCommand command in commands)
                        {
                            if (AppUtil.IsCancelled(TokenSource))
                            {
                                break;
                            }

                            using (SQLiteCommand cmd = new SQLiteCommand(command.CommandText, con))
                            {
                                foreach (SQLiteParameter existing in command.Parameters)
                                {
                                    if (AppUtil.IsCancelled(TokenSource))
                                    {
                                        break;
                                    }

                                    SQLiteParameter parm = new SQLiteParameter();
                                    parm.ParameterName = existing.ParameterName;
                                    parm.Value = existing.Value;
                                    parm.DbType = existing.DbType;
                                    cmd.Parameters.Add(parm);
                                }

                                cmd.ExecuteNonQuery();

                                count++;

                                if (progressBar != null)
                                {
                                    AppUtil.UpdateProgress(TokenSource, progressBar, "(" + count + "/" + total + ")");
                                    AppUtil.UpdateProgress(TokenSource, progressBar, (count * 100) / total);
                                }
                            }
                        }

                        if (!AppUtil.IsCancelled(TokenSource))
                        {
                            transaction.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.BulkQuery", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        public static DataTable GetData(string sql, SQLiteParameter[] parameters)
        {
            DataTable data = new DataTable();

            try
            {
                if (sql.Length > 0)
                {
                    string connString = GetSqlConnection(MainForm.BrainFile).ConnectionString;
                    using (SQLiteConnection con = new SQLiteConnection(connString))
                    {
                        con.Open();

                        using (SQLiteTransaction transaction = con.BeginTransaction())
                        {
                            using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                            {
                                if (parameters != null)
                                {
                                    if (parameters.Length > 0)
                                    {
                                        for (int i = 0; i < parameters.Length; i++)
                                        {
                                            if (parameters[i] != null)
                                            {
                                                cmd.Parameters.Add(parameters[i]);
                                            }
                                        }
                                    }
                                }

                                using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                                {
                                    da.Fill(data);
                                    transaction.Commit();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.GetData", ex.Source, ex.Message, ex.StackTrace);
            }

            return data;
        }

        public static DataTable GetData(string sql, SQLiteParameter[] parameters, string brainFile)
        {
            DataTable data = new DataTable();

            try
            {
                if (sql.Length > 0)
                {
                    string connString = GetSqlConnection(brainFile).ConnectionString;
                    using (SQLiteConnection con = new SQLiteConnection(connString))
                    {
                        con.Open();

                        using (SQLiteTransaction transaction = con.BeginTransaction())
                        {
                            using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                            {
                                if (parameters != null)
                                {
                                    if (parameters.Length > 0)
                                    {
                                        for (int i = 0; i < parameters.Length; i++)
                                        {
                                            if (parameters[i] != null)
                                            {
                                                cmd.Parameters.Add(parameters[i]);
                                            }
                                        }
                                    }
                                }

                                using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                                {
                                    da.Fill(data);
                                    transaction.Commit();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.GetData", ex.Source, ex.Message, ex.StackTrace);
            }

            return data;
        }

        public static List<SQLiteCommand> Wipe()
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                string[] tables = { "Outputs", "Topics", "PreWords", "ProWords", "Inputs", "Words" };
                foreach (string table in tables)
                {
                    commands.Add(new SQLiteCommand("DELETE FROM " + table));
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Wipe", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<Input> Get_Inputs(string brainFile)
        {
            List<Input> inputs = new List<Input>();

            try
            {
                DataTable data = GetData("SELECT ID, Input, Priority FROM Inputs", null, brainFile);

                foreach (DataRow row in data.Rows)
                {
                    Input new_input = new Input();
                    new_input.id = int.Parse(row.ItemArray[0].ToString());
                    new_input.input = row.ItemArray[1].ToString();
                    new_input.priority = int.Parse(row.ItemArray[2].ToString());
                    inputs.Add(new_input);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Get_Inputs", ex.Source, ex.Message, ex.StackTrace);
            }

            return inputs;
        }

        public static int Get_InputPriority(string input)
        {
            try
            {
                if (!string.IsNullOrEmpty(input))
                {
                    List<SQLiteParameter> parameters = new List<SQLiteParameter>();

                    SQLiteParameter parm = new SQLiteParameter("@input", input);
                    parm.DbType = DbType.String;
                    parameters.Add(parm);

                    DataTable data = GetData("SELECT Priority FROM Inputs WHERE Input = @input", parameters.ToArray());
                    if (data.Rows.Count > 0)
                    {
                        return int.Parse(data.Rows[0].ItemArray[0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Get_InputPriority", ex.Source, ex.Message, ex.StackTrace);
            }

            return 0;
        }

        public static List<SQLiteCommand> Add_NewInput(string input)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                if (!string.IsNullOrEmpty(input))
                {
                    SQLiteParameter parm = new SQLiteParameter("@input", input);
                    parm.DbType = DbType.String;

                    string sql = "INSERT INTO Inputs (Input) SELECT @input WHERE NOT EXISTS (SELECT 1 FROM Inputs WHERE Input = @input)";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(parm);
                    commands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Add_NewInput", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<SQLiteCommand> Increase_InputPriorities(string input)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                if (!string.IsNullOrEmpty(input))
                {
                    SQLiteParameter parm = new SQLiteParameter("@input", input);
                    parm.DbType = DbType.String;

                    string sql = "UPDATE Inputs SET Priority = Priority + 1 WHERE Input = @input";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(parm);
                    commands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Increase_InputPriorities", ex.Source, ex.Message, ex.StackTrace);
            }
            
            return commands;
        }

        public static List<SQLiteCommand> Decrease_InputPriorities(string input)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                if (!string.IsNullOrEmpty(input))
                {
                    SQLiteParameter parm = new SQLiteParameter("@input", input);
                    parm.DbType = DbType.String;

                    string sql = "UPDATE Inputs SET Priority = Priority - 1 WHERE Input = @input";
                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(parm);
                    commands.Add(cmd);

                    string delete_sql = "DELETE FROM Inputs WHERE Priority < 1";
                    SQLiteCommand delete_cmd = new SQLiteCommand(delete_sql);
                    commands.Add(delete_cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Decrease_InputPriorities", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<Word> Get_Words(string brainFile)
        {
            List<Word> words = new List<Word>();

            try
            {
                DataTable data = GetData("SELECT ID, Word, Priority FROM Words", null, brainFile);

                foreach (DataRow row in data.Rows)
                {
                    Word new_word = new Word();
                    new_word.id = int.Parse(row.ItemArray[0].ToString());
                    new_word.word = row.ItemArray[1].ToString();
                    new_word.priority = int.Parse(row.ItemArray[2].ToString());
                    words.Add(new_word);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Get_Words", ex.Source, ex.Message, ex.StackTrace);
            }

            return words;
        }

        public static string Get_RandomWord()
        {
            string word = "";

            try
            {
                DataTable data = GetData("SELECT Word FROM Words ORDER BY RANDOM() LIMIT 1", null);

                if (data.Rows.Count > 0)
                {
                    word = data.Rows[0].ItemArray[0].ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Get_RandomWord", ex.Source, ex.Message, ex.StackTrace);
            }

            return word;
        }

        public static string Get_MinWord(string[] words)
        {
            string word = "";

            try
            {
                if (words.Length > 0)
                {
                    List<string> min_words = new List<string>();

                    int priority = Get_MinPriority_Words(words);

                    string wordSet = WordArray_To_String(words);

                    SQLiteParameter parm = new SQLiteParameter("@priority", priority);
                    parm.DbType = DbType.Int32;

                    SQLiteParameter[] parms = { parm };
                    DataTable data = GetData("SELECT Word FROM Words WHERE Priority = @priority AND Word IN (" + wordSet + ")", parms);

                    foreach (DataRow row in data.Rows)
                    {
                        string min_word = row.ItemArray[0].ToString();
                        min_words.Add(min_word);
                    }

                    if (min_words.Count > 0)
                    {
                        CryptoRandom random = new CryptoRandom();
                        int choice = random.Next(0, min_words.Count);
                        word = min_words[choice];
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Get_MinWord", ex.Source, ex.Message, ex.StackTrace);
            }

            return word;
        }

        public static string[] Get_MinWords(string[] words, bool update_progress)
        {
            List<string> min_words = new List<string>();

            try
            {
                if (words.Length > 0)
                {
                    int count = 0;

                    if (update_progress)
                    {
                        AppUtil.UpdateProgress(MainForm.progressDetail, 0);
                    }

                    int priority = Get_MinPriority_Words(words);

                    string wordSet = WordArray_To_String(words);

                    SQLiteParameter parm = new SQLiteParameter("@priority", priority);
                    parm.DbType = DbType.Int32;

                    SQLiteParameter[] parms = { parm };
                    DataTable data = GetData("SELECT Word FROM Words WHERE Priority = @priority AND Word IN (" + wordSet + ")", parms);

                    int total = data.Rows.Count;

                    foreach (DataRow row in data.Rows)
                    {
                        string word = row.ItemArray[0].ToString();
                        min_words.Add(word);

                        count++;

                        if (update_progress)
                        {
                            AppUtil.UpdateProgress(MainForm.progressDetail, (count * 100) / total);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Get_MinWords", ex.Source, ex.Message, ex.StackTrace);
            }

            return min_words.ToArray();
        }

        public static int Get_MinPriority_Words(string[] words)
        {
            int min = 0;

            try
            {
                if (words.Length > 0)
                {
                    string wordSet = WordArray_To_String(words);

                    DataTable data = GetData("SELECT MIN(Priority) AS MinPriority FROM Words WHERE Word IN (" + wordSet + ")", null);

                    if (data.Rows.Count > 0 &&
                        data.Rows[0].ItemArray.Length > 0)
                    {
                        string result = data.Rows[0].ItemArray[0].ToString();
                        if (!string.IsNullOrEmpty(result))
                        {
                            min = int.Parse(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Get_MinPriority_Words", ex.Source, ex.Message, ex.StackTrace);
            }
            
            return min;
        }

        public static List<SQLiteCommand> Add_Words(string[] words)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                foreach (string word in words)
                {
                    SQLiteParameter parm = new SQLiteParameter("@word", word);
                    parm.DbType = DbType.String;

                    string sql = "INSERT INTO Words (Word) SELECT @word WHERE NOT EXISTS (SELECT 1 FROM Words WHERE Word = @word)";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(parm);
                    commands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Add_Words", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static int Get_Word_Priority(string word)
        {
            int priority = 0;

            try
            {
                if (!string.IsNullOrEmpty(word))
                {
                    SQLiteParameter parm = new SQLiteParameter("@word", word);
                    parm.DbType = DbType.String;

                    SQLiteParameter[] parms = { parm };

                    DataTable data = GetData("SELECT Priority FROM Words WHERE Word = @word", parms);

                    if (data.Rows.Count > 0 &&
                        data.Rows[0].ItemArray.Length > 0)
                    {
                        string result = data.Rows[0].ItemArray[0].ToString();
                        if (!string.IsNullOrEmpty(result))
                        {
                            priority = int.Parse(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Get_Word_Priority", ex.Source, ex.Message, ex.StackTrace);
            }

            return priority;
        }

        public static List<SQLiteCommand> Increase_WordPriorities(string[] words)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                foreach (string word in words)
                {
                    SQLiteParameter parm = new SQLiteParameter("@word", word);
                    parm.DbType = DbType.String;

                    string sql = "UPDATE Words SET Priority = Priority + 1 WHERE Word = @word";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(parm);
                    commands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Increase_WordPriorities", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<SQLiteCommand> Decrease_WordPriorities(string[] words)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                foreach (string word in words)
                {
                    SQLiteParameter parm = new SQLiteParameter("@word", word);
                    parm.DbType = DbType.String;

                    string sql = "UPDATE Words SET Priority = Priority - 1 WHERE Word = @word";
                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(parm);
                    commands.Add(cmd);

                    string delete_sql = "DELETE FROM Words WHERE Priority < 1";
                    SQLiteCommand delete_cmd = new SQLiteCommand(delete_sql);
                    commands.Add(delete_cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Decrease_WordPriorities", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<PreWord> Get_PreWords(string brainFile)
        {
            List<PreWord> pre_words = new List<PreWord>();

            try
            {
                DataTable data = GetData("SELECT ID, Word, PreWord, Priority, Distance FROM PreWords", null, brainFile);

                foreach (DataRow row in data.Rows)
                {
                    PreWord new_pre_word = new PreWord();
                    new_pre_word.id = int.Parse(row.ItemArray[0].ToString());
                    new_pre_word.word = row.ItemArray[1].ToString();
                    new_pre_word.preWord = row.ItemArray[2].ToString();
                    new_pre_word.priority = int.Parse(row.ItemArray[3].ToString());
                    new_pre_word.distance = int.Parse(row.ItemArray[4].ToString());
                    pre_words.Add(new_pre_word);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Get_PreWords", ex.Source, ex.Message, ex.StackTrace);
            }

            return pre_words;
        }

        public static string Get_PreWord(List<string> words, bool for_thinking)
        {
            string result = "";

            try
            {
                if (words.Count > 0)
                {
                    Dictionary<string, int> options = new Dictionary<string, int>();
                    List<DataRow> rows = new List<DataRow>();

                    int count = 1;

                    for (int i = 0; i < words.Count; i++)
                    {
                        if (for_thinking &&
                            !Brain.Thinking)
                        {
                            break;
                        }

                        string word = words[i];

                        SQLiteParameter word_parm = new SQLiteParameter("@word", word);
                        word_parm.DbType = DbType.String;

                        SQLiteParameter distance_parm = new SQLiteParameter("@distance", count);
                        distance_parm.DbType = DbType.String;

                        SQLiteParameter[] parms = { word_parm, distance_parm };
                        DataTable data = GetData("SELECT * FROM PreWords WHERE Word = @word AND Distance = @distance", parms);

                        foreach (DataRow row in data.Rows)
                        {
                            if (for_thinking &&
                                !Brain.Thinking)
                            {
                                break;
                            }

                            rows.Add(row);
                        }

                        count++;
                    }

                    foreach (DataRow row in rows)
                    {
                        if (for_thinking &&
                            !Brain.Thinking)
                        {
                            break;
                        }

                        string pre_word = row.ItemArray[2].ToString();
                        int priority = int.Parse(row.ItemArray[3].ToString());
                        int distance = int.Parse(row.ItemArray[4].ToString());

                        if (distance == 1)
                        {
                            //Get options
                            if (!options.ContainsKey(pre_word))
                            {
                                options.Add(pre_word, priority);
                            }
                        }
                        else
                        {
                            //Reinforce options that match farther distances
                            if (options.Count > 0 &&
                                options.ContainsKey(pre_word))
                            {
                                options[pre_word]++;
                            }
                        }
                    }

                    //Get max priority from options
                    int max = 0;
                    foreach (var set in options)
                    {
                        if (for_thinking &&
                            !Brain.Thinking)
                        {
                            break;
                        }

                        int priority = set.Value;
                        if (priority > max)
                        {
                            max = priority;
                        }
                    }

                    //Randomly select one with bias towards max priority
                    foreach (var set in options)
                    {
                        if (for_thinking &&
                            !Brain.Thinking)
                        {
                            break;
                        }

                        int priority = set.Value;

                        CryptoRandom random = new CryptoRandom();
                        int choice = random.Next(0, max + 1);

                        if (priority >= choice)
                        {
                            result = set.Key;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Get_PreWord", ex.Source, ex.Message, ex.StackTrace);
            }

            return result;
        }

        public static List<SQLiteCommand> Add_PreWords(string[] words, string[] prewords, int[] distances)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                for (int i = 0; i < words.Length; i++)
                {
                    SQLiteParameter word_parm = new SQLiteParameter("@word", words[i]);
                    word_parm.DbType = DbType.String;

                    SQLiteParameter pre_word_parm = new SQLiteParameter("@pre_word", prewords[i]);
                    pre_word_parm.DbType = DbType.String;

                    SQLiteParameter distance_parm = new SQLiteParameter("@distance", distances[i]);
                    distance_parm.DbType = DbType.Int32;

                    string sql = @"
                    INSERT INTO PreWords (Word, PreWord, Distance)
                    SELECT @word, @pre_word, @distance
                    WHERE NOT EXISTS (SELECT 1 FROM PreWords WHERE Word = @word AND PreWord = @pre_word AND Distance = @distance)
                    ";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(word_parm);
                    cmd.Parameters.Add(pre_word_parm);
                    cmd.Parameters.Add(distance_parm);
                    commands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Add_PreWords", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<SQLiteCommand> Increase_PreWordPriorities(string[] words, string[] prewords, int[] distances)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                for (int i = 0; i < words.Length; i++)
                {
                    SQLiteParameter word_parm = new SQLiteParameter("@word", words[i]);
                    word_parm.DbType = DbType.String;

                    SQLiteParameter pre_word_parm = new SQLiteParameter("@pre_word", prewords[i]);
                    pre_word_parm.DbType = DbType.String;

                    SQLiteParameter distance_parm = new SQLiteParameter("@distance", distances[i]);
                    distance_parm.DbType = DbType.Int32;

                    string sql = @"
                    UPDATE PreWords
                    SET Priority = Priority + 1
                    WHERE Word = @word AND PreWord = @pre_word AND Distance = @distance
                    ";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(word_parm);
                    cmd.Parameters.Add(pre_word_parm);
                    cmd.Parameters.Add(distance_parm);
                    commands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Increase_PreWordPriorities", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<SQLiteCommand> Decrease_PreWordPriorities(string[] words, string[] prewords, int[] distances)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                for (int i = 0; i < words.Length; i++)
                {
                    SQLiteParameter word_parm = new SQLiteParameter("@word", words[i]);
                    word_parm.DbType = DbType.String;

                    SQLiteParameter pre_word_parm = new SQLiteParameter("@pre_word", prewords[i]);
                    pre_word_parm.DbType = DbType.String;

                    SQLiteParameter distance_parm = new SQLiteParameter("@distance", distances[i]);
                    distance_parm.DbType = DbType.Int32;

                    string sql = @"
                    UPDATE PreWords
                    SET Priority = Priority - 1
                    WHERE Word = @word AND PreWord = @pre_word AND Distance = @distance
                    ";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(word_parm);
                    cmd.Parameters.Add(pre_word_parm);
                    cmd.Parameters.Add(distance_parm);
                    commands.Add(cmd);
                }

                string delete_sql = @"DELETE FROM PreWords WHERE Priority < 1";
                SQLiteCommand delete_cmd = new SQLiteCommand(delete_sql);
                commands.Add(delete_cmd);
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Decrease_PreWordPriorities", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<ProWord> Get_ProWords(string brainFile)
        {
            List<ProWord> pro_words = new List<ProWord>();

            try
            {
                DataTable data = GetData("SELECT ID, Word, ProWord, Priority, Distance FROM ProWords", null, brainFile);

                foreach (DataRow row in data.Rows)
                {
                    ProWord new_pro_word = new ProWord();
                    new_pro_word.id = int.Parse(row.ItemArray[0].ToString());
                    new_pro_word.word = row.ItemArray[1].ToString();
                    new_pro_word.proWord = row.ItemArray[2].ToString();
                    new_pro_word.priority = int.Parse(row.ItemArray[3].ToString());
                    new_pro_word.distance = int.Parse(row.ItemArray[4].ToString());
                    pro_words.Add(new_pro_word);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Get_ProWords", ex.Source, ex.Message, ex.StackTrace);
            }

            return pro_words;
        }

        public static string Get_ProWord(List<string> words, bool for_thinking)
        {
            string result = "";

            try
            {
                if (words.Count > 0)
                {
                    Dictionary<string, int> options = new Dictionary<string, int>();
                    List<DataRow> rows = new List<DataRow>();

                    int count = 1;

                    for (int i = words.Count - 1; i >= 0; i--)
                    {
                        if (for_thinking &&
                            !Brain.Thinking)
                        {
                            break;
                        }

                        string word = words[i];

                        SQLiteParameter word_parm = new SQLiteParameter("@word", word);
                        word_parm.DbType = DbType.String;

                        SQLiteParameter distance_parm = new SQLiteParameter("@distance", count);
                        distance_parm.DbType = DbType.String;

                        SQLiteParameter[] parms = { word_parm, distance_parm };
                        DataTable data = GetData("SELECT * FROM ProWords WHERE Word = @word AND Distance = @distance", parms);

                        foreach (DataRow row in data.Rows)
                        {
                            if (for_thinking &&
                                !Brain.Thinking)
                            {
                                break;
                            }

                            rows.Add(row);
                        }

                        count++;
                    }

                    foreach (DataRow row in rows)
                    {
                        if (for_thinking &&
                            !Brain.Thinking)
                        {
                            break;
                        }

                        string pro_word = row.ItemArray[2].ToString();
                        int priority = int.Parse(row.ItemArray[3].ToString());
                        int distance = int.Parse(row.ItemArray[4].ToString());

                        if (distance == 1)
                        {
                            //Get options
                            if (!options.ContainsKey(pro_word))
                            {
                                options.Add(pro_word, priority);
                            }
                        }
                        else
                        {
                            //Reinforce options that match farther distances
                            if (options.Count > 0 &&
                                options.ContainsKey(pro_word))
                            {
                                options[pro_word]++;
                            }
                        }
                    }

                    //Get max priority from options
                    int max = 0;
                    foreach (var set in options)
                    {
                        if (for_thinking &&
                            !Brain.Thinking)
                        {
                            break;
                        }

                        int priority = set.Value;
                        if (priority > max)
                        {
                            max = priority;
                        }
                    }

                    //Randomly select one with bias towards max priority
                    foreach (var set in options)
                    {
                        if (for_thinking &&
                            !Brain.Thinking)
                        {
                            break;
                        }

                        int priority = set.Value;

                        CryptoRandom random = new CryptoRandom();
                        int choice = random.Next(0, max + 1);

                        if (priority >= choice)
                        {
                            result = set.Key;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Get_ProWord", ex.Source, ex.Message, ex.StackTrace);
            }

            return result;
        }

        public static List<SQLiteCommand> Add_ProWords(string[] words, string[] prowords, int[] distances)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                for (int i = 0; i < words.Length; i++)
                {
                    SQLiteParameter word_parm = new SQLiteParameter("@word", words[i]);
                    word_parm.DbType = DbType.String;

                    SQLiteParameter pro_word_parm = new SQLiteParameter("@pro_word", prowords[i]);
                    pro_word_parm.DbType = DbType.String;

                    SQLiteParameter distance_parm = new SQLiteParameter("@distance", distances[i]);
                    distance_parm.DbType = DbType.Int32;

                    string sql = @"
                    INSERT INTO ProWords (Word, ProWord, Distance)
                    SELECT @word, @pro_word, @distance
                    WHERE NOT EXISTS (SELECT 1 FROM ProWords WHERE Word = @word AND ProWord = @pro_word AND Distance = @distance)
                    ";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(word_parm);
                    cmd.Parameters.Add(pro_word_parm);
                    cmd.Parameters.Add(distance_parm);
                    commands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Add_ProWords", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<SQLiteCommand> Increase_ProWordPriorities(string[] words, string[] prowords, int[] distances)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                for (int i = 0; i < words.Length; i++)
                {
                    SQLiteParameter word_parm = new SQLiteParameter("@word", words[i]);
                    word_parm.DbType = DbType.String;

                    SQLiteParameter pro_word_parm = new SQLiteParameter("@pro_word", prowords[i]);
                    pro_word_parm.DbType = DbType.String;

                    SQLiteParameter distance_parm = new SQLiteParameter("@distance", distances[i]);
                    distance_parm.DbType = DbType.Int32;

                    string sql = @"
                    UPDATE ProWords
                    SET Priority = Priority + 1
                    WHERE Word = @word AND ProWord = @pro_word AND Distance = @distance
                    ";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(word_parm);
                    cmd.Parameters.Add(pro_word_parm);
                    cmd.Parameters.Add(distance_parm);
                    commands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Increase_ProWordPriorities", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<SQLiteCommand> Decrease_ProWordPriorities(string[] words, string[] prowords, int[] distances)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                for (int i = 0; i < words.Length; i++)
                {
                    SQLiteParameter word_parm = new SQLiteParameter("@word", words[i]);
                    word_parm.DbType = DbType.String;

                    SQLiteParameter pro_word_parm = new SQLiteParameter("@pro_word", prowords[i]);
                    pro_word_parm.DbType = DbType.String;

                    SQLiteParameter distance_parm = new SQLiteParameter("@distance", distances[i]);
                    distance_parm.DbType = DbType.Int32;

                    string sql = @"
                    UPDATE ProWords
                    SET Priority = Priority - 1
                    WHERE Word = @word AND ProWord = @pro_word AND Distance = @distance
                    ";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(word_parm);
                    cmd.Parameters.Add(pro_word_parm);
                    cmd.Parameters.Add(distance_parm);
                    commands.Add(cmd);
                }

                string delete_sql = @"DELETE FROM ProWords WHERE Priority < 1";
                SQLiteCommand delete_cmd = new SQLiteCommand(delete_sql);
                commands.Add(delete_cmd);
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Decrease_ProWordPriorities", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<Topic> Get_Topics(string brainFile)
        {
            List<Topic> topics = new List<Topic>();

            try
            {
                DataTable data = GetData("SELECT ID, Input, Topic, Priority FROM Topics", null, brainFile);

                foreach (DataRow row in data.Rows)
                {
                    Topic new_topic = new Topic();
                    new_topic.id = int.Parse(row.ItemArray[0].ToString());
                    new_topic.input = row.ItemArray[1].ToString();
                    new_topic.topic = row.ItemArray[2].ToString();
                    new_topic.priority = int.Parse(row.ItemArray[3].ToString());
                    topics.Add(new_topic);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Get_Topics", ex.Source, ex.Message, ex.StackTrace);
            }

            return topics;
        }

        public static List<SQLiteCommand> AddTopics(string input, string[] words)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                if (!string.IsNullOrEmpty(input) &&
                    words.Length > 0)
                {
                    foreach (string word in words)
                    {
                        SQLiteParameter input_parm = new SQLiteParameter("@input", input);
                        input_parm.DbType = DbType.String;

                        SQLiteParameter word_parm = new SQLiteParameter("@word", word);
                        word_parm.DbType = DbType.String;

                        //Add new topics
                        string sql = @"
                        INSERT INTO Topics (Input, Topic) 
                        SELECT @input, @word 
                        WHERE NOT EXISTS (SELECT 1 FROM Topics WHERE Input = @input AND Topic = @word)
                        ";

                        SQLiteCommand cmd = new SQLiteCommand(sql);
                        cmd.Parameters.Add(input_parm);
                        cmd.Parameters.Add(word_parm);
                        commands.Add(cmd);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.AddTopics", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<SQLiteCommand> Increase_TopicPriorities(string input, string[] words)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                if (!string.IsNullOrEmpty(input) &&
                    words.Length > 0)
                {
                    foreach (string word in words)
                    {
                        SQLiteParameter input_parm = new SQLiteParameter("@input", input);
                        input_parm.DbType = DbType.String;

                        SQLiteParameter word_parm = new SQLiteParameter("@word", word);
                        word_parm.DbType = DbType.String;

                        //Increase priority for existing topics
                        string sql = @"
                        UPDATE Topics
                        SET Priority = Priority + 1
                        WHERE Input = @input AND Topic = @word
                        ";

                        SQLiteCommand cmd = new SQLiteCommand(sql);
                        cmd.Parameters.Add(input_parm);
                        cmd.Parameters.Add(word_parm);
                        commands.Add(cmd);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Increase_TopicPriorities", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<SQLiteCommand> Decrease_TopicPriorities(string input, string[] words)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                if (!string.IsNullOrEmpty(input) &&
                    words.Length > 0)
                {
                    foreach (string word in words)
                    {
                        SQLiteParameter input_parm = new SQLiteParameter("@input", input);
                        input_parm.DbType = DbType.String;

                        SQLiteParameter word_parm = new SQLiteParameter("@word", word);
                        word_parm.DbType = DbType.String;

                        //Increase priority for existing topics
                        string sql = @"
                        UPDATE Topics
                        SET Priority = Priority - 1
                        WHERE Input = @input AND Topic = @word
                        ";

                        SQLiteCommand cmd = new SQLiteCommand(sql);
                        cmd.Parameters.Add(input_parm);
                        cmd.Parameters.Add(word_parm);
                        commands.Add(cmd);
                    }
                }

                string delete_sql = @"DELETE FROM Topics WHERE Priority < 1";
                SQLiteCommand delete_cmd = new SQLiteCommand(delete_sql);
                commands.Add(delete_cmd);
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Decrease_TopicPriorities", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<SQLiteCommand> Decrease_Topics_Unmatched(string input, string[] words)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                if (!string.IsNullOrEmpty(input) &&
                    words.Length > 0)
                {
                    SQLiteParameter input_parm = new SQLiteParameter("@input", input);
                    input_parm.DbType = DbType.String;

                    string sql = @"
                    UPDATE Topics
                    SET Priority = Priority - 1
                    WHERE Input = @input AND Topic NOT IN (" + WordArray_To_String(words) + ")";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(input_parm);
                    commands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Decrease_Topics_Unmatched", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<SQLiteCommand> Clean_Topics(string input)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                if (!string.IsNullOrEmpty(input))
                {
                    SQLiteParameter input_parm = new SQLiteParameter("@input", input);
                    input_parm.DbType = DbType.String;

                    string sql = "DELETE FROM Topics WHERE Input = @input AND Priority < 1";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(input_parm);
                    commands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Clean_Topics", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static string[] Get_OutputsFromTopics(string[] words, bool update_progress)
        {
            List<string> outputs = new List<string>();

            try
            {
                if (words.Length > 0)
                {
                    int value = 0;

                    if (update_progress)
                    {
                        AppUtil.UpdateProgress(MainForm.progressDetail, 0);
                    }

                    string wordSet = WordArray_To_String(words);

                    //Get all the inputs that contain a matching topic from the wordset
                    DataTable inputsData = GetData("SELECT DISTINCT Input FROM Topics WHERE Topic IN (" + wordSet + ")", null);

                    int total = inputsData.Rows.Count;

                    Dictionary<string, int> output_priority = new Dictionary<string, int>();
                    foreach (DataRow row in inputsData.Rows)
                    {
                        string input = row.ItemArray[0].ToString();

                        List<SQLiteParameter> parameters = new List<SQLiteParameter>();
                        SQLiteParameter parm = new SQLiteParameter("@input", input);
                        parm.DbType = DbType.String;
                        parameters.Add(parm);

                        //Get all the outputs for the given input
                        DataTable outputData = GetData("SELECT DISTINCT Output FROM Outputs WHERE Input = @input", parameters.ToArray());
                        if (outputData.Rows.Count > 0)
                        {
                            string output = outputData.Rows[0].ItemArray[0].ToString();

                            //Get the priority of the output from the Input table
                            int priority = Get_InputPriority(output);

                            //Add output/priority pair as a possibility
                            if (!output_priority.ContainsKey(output))
                            {
                                output_priority.Add(output, priority);
                            }
                        }

                        value++;

                        if (update_progress)
                        {
                            AppUtil.UpdateProgress(MainForm.progressDetail, (value * 100) / total);
                        }
                    }

                    //Get the highest priority output
                    if (output_priority.Count > 0)
                    {
                        int max_priority = 0;
                        foreach (KeyValuePair<string, int> output_pair in output_priority)
                        {
                            if (output_pair.Value > max_priority)
                            {
                                max_priority = output_pair.Value;
                            }
                        }
                            
                        foreach (KeyValuePair<string, int> output_pair in output_priority)
                        {
                            //Add any output matching the highest priority
                            if (output_pair.Value >= max_priority)
                            {
                                outputs.Add(output_pair.Key);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Get_OutputsFromTopics", ex.Source, ex.Message, ex.StackTrace);
            }

            return outputs.ToArray();
        }

        public static List<Output> Get_Outputs(string brainFile)
        {
            List<Output> outputs = new List<Output>();

            try
            {
                DataTable data = GetData("SELECT ID, Input, Output, Priority FROM Outputs", null, brainFile);

                foreach (DataRow row in data.Rows)
                {
                    Output new_output = new Output();
                    new_output.id = int.Parse(row.ItemArray[0].ToString());
                    new_output.input = row.ItemArray[1].ToString();
                    new_output.output = row.ItemArray[2].ToString();
                    new_output.priority = int.Parse(row.ItemArray[3].ToString());
                    outputs.Add(new_output);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Get_Outputs", ex.Source, ex.Message, ex.StackTrace);
            }

            return outputs;
        }

        public static List<SQLiteCommand> Add_Outputs(string input, string output)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                if (!string.IsNullOrEmpty(input) &&
                    !string.IsNullOrEmpty(output))
                {
                    SQLiteParameter input_parm = new SQLiteParameter("@input", input);
                    input_parm.DbType = DbType.String;

                    SQLiteParameter output_parm = new SQLiteParameter("@output", output);
                    output_parm.DbType = DbType.String;

                    string sql = @"
                    INSERT INTO Outputs (Input, Output)
                    SELECT @input, @output
                    WHERE NOT EXISTS (SELECT 1 FROM Outputs WHERE Input = @input AND Output = @output)";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(input_parm);
                    cmd.Parameters.Add(output_parm);
                    commands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Add_Outputs", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<SQLiteCommand> Increase_OutputPriorities(string input, string output)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                if (!string.IsNullOrEmpty(input) &&
                    !string.IsNullOrEmpty(output))
                {
                    SQLiteParameter input_parm = new SQLiteParameter("@input", input);
                    input_parm.DbType = DbType.String;

                    SQLiteParameter output_parm = new SQLiteParameter("@output", output);
                    output_parm.DbType = DbType.String;

                    string sql = @"
                    UPDATE Outputs
                    SET Priority = Priority + 1
                    WHERE Input = @input AND Output = @output";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(input_parm);
                    cmd.Parameters.Add(output_parm);
                    commands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Increase_OutputPriorities", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<SQLiteCommand> Increase_OutputPriorities(string output)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                if (!string.IsNullOrEmpty(output))
                {
                    SQLiteParameter output_parm = new SQLiteParameter("@output", output);
                    output_parm.DbType = DbType.String;

                    string sql = @"
                    UPDATE Outputs
                    SET Priority = Priority + 1
                    WHERE Output = @output";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(output_parm);
                    commands.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Increase_OutputPriorities", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<SQLiteCommand> Decrease_OutputPriorities(string output)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                if (!string.IsNullOrEmpty(output))
                {
                    SQLiteParameter output_parm = new SQLiteParameter("@output", output);
                    output_parm.DbType = DbType.String;

                    string sql = @"
                    UPDATE Outputs
                    SET Priority = Priority - 1
                    WHERE Output = @output";

                    SQLiteCommand cmd = new SQLiteCommand(sql);
                    cmd.Parameters.Add(output_parm);
                    commands.Add(cmd);
                }

                string delete_sql = @"DELETE FROM Outputs WHERE Priority < 1";
                SQLiteCommand delete_cmd = new SQLiteCommand(delete_sql);
                commands.Add(delete_cmd);
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Decrease_OutputPriorities", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static string[] Get_OutputsFromInput(string input, bool update_progress)
        {
            List<string> outputs = new List<string>();

            try
            {
                if (!string.IsNullOrEmpty(input))
                {
                    int count = 0;

                    if (update_progress)
                    {
                        AppUtil.UpdateProgress(MainForm.progressDetail, 0);
                    }

                    SQLiteParameter input_parm = new SQLiteParameter("@input", input);
                    input_parm.DbType = DbType.String;

                    SQLiteParameter[] parms = { input_parm };
                    DataTable data = GetData("SELECT Output FROM Outputs WHERE Input = @input", parms);

                    int total = data.Rows.Count;

                    foreach (DataRow row in data.Rows)
                    {
                        outputs.Add(row.ItemArray[0].ToString());

                        count++;

                        if (update_progress)
                        {
                            AppUtil.UpdateProgress(MainForm.progressDetail, (count * 100) / total);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.Get_OutputsFromInput", ex.Source, ex.Message, ex.StackTrace);
            }

            return outputs.ToArray();
        }

        public static string WordArray_To_String(string[] words)
        {
            string wordSet = "";

            try
            {
                for (int i = 0; i < words.Length; i++)
                {
                    string word = "";

                    //Check word for apostrophe which will break queries
                    string check_word = words[i];
                    for (int j = 0; j < check_word.Length; j++)
                    {
                        char c = check_word[j];
                        if (c == '\'')
                        {
                            word += c;
                            word += '\'';
                        }
                        else
                        {
                            word += c;
                        }
                    }

                    wordSet += "'" + word + "'";
                    if (i < words.Length - 1)
                    {
                        wordSet += ",";
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("SqlUtil.WordArray_To_String", ex.Source, ex.Message, ex.StackTrace);
            }

            return wordSet;
        }
    }
}
