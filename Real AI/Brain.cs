using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Real_AI.Util;

namespace Real_AI
{
    public static class Brain
    {
        #region Variables

        public static string Input;
        public static string Response;
        public static string LastResponse;
        public static string CleanInput;
        public static string[] WordArray;
        public static string[] Topics;
        
        public static string Thought;
        public static string LastThought;
        public static string[] WordArray_Thinking;

        public static bool Thinking;
        public static bool LearnFromThinking;
        public static bool Initiate;

        #endregion

        #region Methods

        public static readonly Regex NormalCharacters = new Regex(@"^[\p{L}\p{M}\p{N}\s]+$");

        public static string GapSpecials(string old_string, TextProgressBar progressMain, TextProgressBar progressDetail, CancellationTokenSource TokenSource, bool update_progress)
        {
            if (update_progress)
            {
                if (progressMain.InvokeRequired &&
                    !AppUtil.IsCancelled(TokenSource))
                {
                    progressMain.Invoke((MethodInvoker)delegate
                    {
                        progressMain.CustomText = "Processing data";
                    });
                }
                else
                {
                    progressMain.CustomText = "Processing data";
                }
            }

            int count = 0;
            int total = old_string.Length;

            StringBuilder sb = new StringBuilder();

            for (var i = 0; i < old_string.Length; i++)
            {
                if (AppUtil.IsCancelled(TokenSource))
                {
                    break;
                }

                string value = old_string[i].ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    if (!NormalCharacters.IsMatch(value) &&
                        value != "'" &&
                        value != "’")
                    {
                        if (value == ".")
                        {
                            if (i > 0)
                            {
                                if (old_string[i - 1] != '.')
                                {
                                    sb.Append(" ");
                                }
                                
                                sb.Append(value);
                            }
                            else
                            {
                                sb.Append(value);
                            }

                            if (i < old_string.Length - 1)
                            {
                                if (old_string[i + 1] != ' ' &&
                                    old_string[i + 1] != '.')
                                {
                                    sb.Append(" ");
                                }
                            }
                        }
                        else
                        {
                            sb.Append(" ");
                            sb.Append(value);

                            if (i < old_string.Length - 1)
                            {
                                if (old_string[i + 1] != ' ')
                                {
                                    sb.Append(" ");
                                }
                            }
                        }
                    }
                    else if (value == "\r" ||
                             value == "\n")
                    {

                    }
                    else
                    {
                        sb.Append(value);
                    }
                }
                
                count++;

                if (update_progress)
                {
                    if (progressDetail.InvokeRequired &&
                        !AppUtil.IsCancelled(TokenSource))
                    {
                        progressDetail.Invoke((MethodInvoker)delegate
                        {
                            progressDetail.CustomText = "(" + count + "/" + total + ")";
                            progressDetail.Value = (count * 100) / total;
                        });
                    }
                    else
                    {
                        progressDetail.CustomText = "(" + count + "/" + total + ")";
                        progressDetail.Value = (count * 100) / total;
                    }
                }
            }

            return sb.ToString();
        }

        public static string UnGapSpecials(string old_string)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(old_string);

            for (var i = 1; i < sb.Length; i++)
            {
                if (!NormalCharacters.IsMatch(sb[i].ToString()) &&
                    sb[i].ToString() != ":" &&
                    sb[i - 1] == ' ')
                {
                    sb.Remove(i - 1, 1);

                    if (i > 1)
                    {
                        i--;
                    }
                }
            }

            return sb.ToString();
        }

        private static void PrepInput()
        {
            CleanInput = RulesCheck(Input);
            WordArray = GapSpecials(CleanInput, MainForm.progressMain, MainForm.progressDetail, null, true).Trim(' ').Split(' ');

            if (WordArray.Length > 0)
            {
                if (MainForm.progressMain.InvokeRequired)
                {
                    MainForm.progressMain.Invoke((MethodInvoker)delegate
                    {
                        MainForm.progressMain.CustomText = "Saving data...";
                    });
                }
                else
                {
                    MainForm.progressMain.CustomText = "Saving data...";
                }

                List<SQLiteCommand> commands = new List<SQLiteCommand>();
                commands.AddRange(AddInputs(CleanInput));
                commands.AddRange(AddWords(WordArray));
                commands.AddRange(AddPreWords(WordArray));
                commands.AddRange(AddProWords(WordArray));
                SqlUtil.BulkQuery(commands, true);
                commands.Clear();
            }
        }

        public static List<SQLiteCommand> AddInputs(string input)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();
            commands.AddRange(SqlUtil.Increase_InputPriority(input));
            commands.AddRange(SqlUtil.Add_NewInput(input));
            return commands;
        }

        public static List<SQLiteCommand> AddWords(string[] word_array)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();
            commands.AddRange(SqlUtil.Increase_WordPriorities(word_array));
            commands.AddRange(SqlUtil.Add_Words(word_array));

            return commands;
        }

        public static List<SQLiteCommand> AddPreWords(string[] word_array)
        {
            List<string> words = new List<string>();
            List<string> pre_words = new List<string>();
            List<int> distances = new List<int>();

            for (int i = 1; i < word_array.Length; i++)
            {
                int count = 1;
                for (int j = i - 1; j >= 0; j--)
                {
                    if (!string.IsNullOrEmpty(word_array[i]) &&
                        !string.IsNullOrEmpty(word_array[j]))
                    {
                        words.Add(word_array[i]);
                        pre_words.Add(word_array[j]);
                        distances.Add(count);
                        count++;
                    }
                }
            }

            List<SQLiteCommand> commands = new List<SQLiteCommand>();
            commands.AddRange(SqlUtil.Increase_PreWordPriorities(words.ToArray(), pre_words.ToArray(), distances.ToArray()));
            commands.AddRange(SqlUtil.Add_PreWords(words.ToArray(), pre_words.ToArray(), distances.ToArray()));

            return commands;
        }

        public static List<SQLiteCommand> AddProWords(string[] word_array)
        {
            List<string> words = new List<string>();
            List<string> pro_words = new List<string>();
            List<int> distances = new List<int>();

            for (int i = 0; i < word_array.Length - 1; i++)
            {
                var count = 1;
                for (int j = i + 1; j <= word_array.Length - 1; j++)
                {
                    if (!string.IsNullOrEmpty(word_array[i]) &&
                        !string.IsNullOrEmpty(word_array[j]))
                    {
                        words.Add(word_array[i]);
                        pro_words.Add(word_array[j]);
                        distances.Add(count);
                        count++;
                    }
                }
            }

            List<SQLiteCommand> commands = new List<SQLiteCommand>();
            commands.AddRange(SqlUtil.Increase_ProWordPriorities(words.ToArray(), pro_words.ToArray(), distances.ToArray()));
            commands.AddRange(SqlUtil.Add_ProWords(words.ToArray(), pro_words.ToArray(), distances.ToArray()));

            return commands;
        }

        private static void UpdateTopics(string input, string[] topics, bool update_progress)
        {
            MainForm.progressMain.CustomText = "Updating topics...";

            List<SQLiteCommand> commands = new List<SQLiteCommand>();
            commands.AddRange(SqlUtil.Increase_TopicPriorities(input, topics));
            commands.AddRange(SqlUtil.AddTopics(input, topics));
            commands.AddRange(SqlUtil.Decrease_Topics_Unmatched(input, topics));
            commands.AddRange(SqlUtil.Clean_Topics(input));
            SqlUtil.BulkQuery(commands, update_progress);
            commands.Clear();
        }

        private static void UpdateOutputs(string input, string output, bool update_progress)
        {
            if (update_progress)
            {
                MainForm.progressMain.CustomText = "Adding potential output...";
            }

            List<SQLiteCommand> commands = new List<SQLiteCommand>();
            commands.AddRange(SqlUtil.Increase_OutputPriorities(output, input));
            commands.AddRange(SqlUtil.Add_Outputs(output, input));
            SqlUtil.BulkQuery(commands, update_progress);
            commands.Clear();
        }

        public static string Respond(bool initiating)
        {
            Response = "";

            try
            {
                List<string> outputs = new List<string>();

                if (initiating)
                {
                    if (WordArray != null)
                    {
                        if (WordArray.Length > 0)
                        {
                            //Get topic-based response
                            if (Topics != null)
                            {
                                if (Topics.Length > 0)
                                {
                                    string[] inputs = SqlUtil.Get_InputsFromTopics(Topics, false);

                                    if (!MainForm.Crash)
                                    {
                                        foreach (string input in inputs)
                                        {
                                            outputs.AddRange(SqlUtil.Get_OutputsFromInput(input, false).ToList());
                                        }
                                    }
                                }
                            }
                            
                            if (!MainForm.Crash)
                            {
                                //Get direct response
                                if (outputs.Count == 0)
                                {
                                    if (!string.IsNullOrEmpty(CleanInput))
                                    {
                                        outputs.AddRange(SqlUtil.Get_OutputsFromInput(CleanInput, false).ToList());
                                    }
                                }
                            }

                            if (!MainForm.Crash)
                            {
                                //Get generated response
                                if (outputs.Count == 0)
                                {
                                    string min_word = SqlUtil.Get_MinWord(WordArray);

                                    if (!MainForm.Crash)
                                    {
                                        outputs.Add(GenerateResponse(min_word, false, false));
                                    }
                                }
                            }
                        }
                    }

                    if (outputs.Count == 0)
                    {
                        string word = SqlUtil.Get_RandomWord();
                        outputs.Add(GenerateResponse(word, false, false));
                    }
                }
                else
                {
                    PrepInput();

                    if (WordArray.Length > 0)
                    {
                        AppUtil.UpdateMain(10);

                        //Add input as output to last response
                        if (!string.IsNullOrEmpty(LastResponse))
                        {
                            UpdateOutputs(CleanInput, LastResponse, true);
                            AppUtil.UpdateMain(20);
                        }

                        if (!MainForm.Crash)
                        {
                            AppUtil.UpdateMain("Getting topics from input...");

                            //Get lowest priority words from input
                            Topics = SqlUtil.Get_MinWords(WordArray, true);
                            AppUtil.UpdateMain(30);
                            MainForm.intervals.Clear();

                            if (!MainForm.Crash &&
                                Topics.Length > 0)
                            {
                                //Add words as topics for input
                                UpdateTopics(CleanInput, Topics, true);
                                AppUtil.UpdateMain(40);
                                MainForm.intervals.Clear();
                            }

                            //Get topic-based response
                            if (!MainForm.Crash &&
                                Topics.Length > 0)
                            {
                                AppUtil.UpdateMain("Getting inputs with topics...");
                                
                                string[] inputs = SqlUtil.Get_InputsFromTopics(Topics, true);
                                AppUtil.UpdateMain(50);
                                MainForm.intervals.Clear();

                                if (!MainForm.Crash)
                                {
                                    foreach (string input in inputs)
                                    {
                                        AppUtil.UpdateMain("Getting topic-based outputs...");
                                        outputs.AddRange(SqlUtil.Get_OutputsFromInput(input, true).ToList());
                                    }

                                    AppUtil.UpdateMain(60);
                                    MainForm.intervals.Clear();
                                }
                            }
                        }

                        if (!MainForm.Crash)
                        {
                            //Get direct response
                            if (outputs.Count == 0)
                            {
                                AppUtil.UpdateMain("Getting direct output...");
                                
                                outputs.AddRange(SqlUtil.Get_OutputsFromInput(CleanInput, true).ToList());
                                AppUtil.UpdateMain(70);
                                MainForm.intervals.Clear();
                            }
                        }

                        if (!MainForm.Crash)
                        {
                            //Get generated response
                            if (outputs.Count == 0)
                            {
                                AppUtil.UpdateMain("Generating response...");
                                
                                string min_word = SqlUtil.Get_MinWord(WordArray);
                                AppUtil.UpdateMain(80);
                                MainForm.intervals.Clear();

                                if (!MainForm.Crash)
                                {
                                    outputs.Add(GenerateResponse(min_word, true, false));
                                    AppUtil.UpdateMain(90);
                                    MainForm.intervals.Clear();
                                }
                            }
                        }

                        AppUtil.UpdateDetail(100);
                        AppUtil.UpdateMain(100);
                        MainForm.intervals.Clear();
                    }
                }

                if (!MainForm.Crash)
                {
                    //Choose a response from outputs
                    if (outputs.Count > 0)
                    {
                        CryptoRandom random = new CryptoRandom();
                        int choice = random.Next(0, outputs.Count);

                        Response = outputs[choice];

                        if (!MainForm.Crash &&
                            !string.IsNullOrEmpty(Response))
                        {
                            //Clean up response
                            Response = RulesCheck(Response);

                            if (!string.IsNullOrEmpty(Response))
                            {
                                //Set response as last response
                                LastResponse = Response;

                                //Interrupt thinking
                                LastThought = "";
                                AppUtil.UpdateMain("Responded");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                List<string> log = new List<string>();
                log.Add(ex.Message);
                log.Add(ex.StackTrace);
                AppUtil.CrashLog(log);
            }
            
            return Response;
        }

        public static string GenerateResponse(string topic, bool update_progress, bool for_thinking)
        {
            string response = "";

            if (!string.IsNullOrEmpty(topic))
            {
                int count = 0;

                if (update_progress)
                {
                    AppUtil.UpdateDetail(0);
                }

                List<string> response_words = new List<string>();
                bool words_found = true;

                //Start with topic word
                response_words.Add(topic);

                //Add pre-words
                while (words_found)
                {
                    if (for_thinking &&
                        !Thinking)
                    {
                        break;
                    }

                    //Get pre-word
                    string pre_word = SqlUtil.Get_PreWord(response_words, for_thinking);
                    if (!string.IsNullOrEmpty(pre_word))
                    {
                        //Add it
                        response_words.Insert(0, pre_word);

                        count++;
                        if (count > 100)
                        {
                            count = 100;
                        }

                        if (update_progress)
                        {
                            AppUtil.UpdateDetail(count);
                        }
                    }
                    else
                    {
                        break;
                    }

                    if (for_thinking && 
                        !Thinking)
                    {
                        break;
                    }

                    //Check for duplication
                    if (response_words.Count > 0)
                    {
                        List<string> new_response_words = HandleDuplication(response_words);
                        if (new_response_words.Count < response_words.Count)
                        {
                            break;
                        }
                    }

                    if (for_thinking &&
                        !Thinking)
                    {
                        break;
                    }
                }

                //Add pro-words
                while (words_found)
                {
                    if (for_thinking &&
                        !Thinking)
                    {
                        break;
                    }

                    //Get pro-word
                    string pro_word = SqlUtil.Get_ProWord(response_words, for_thinking);
                    if (!string.IsNullOrEmpty(pro_word))
                    {
                        //Add it
                        response_words.Add(pro_word);

                        count++;
                        if (count > 100)
                        {
                            count = 100;
                        }

                        if (update_progress)
                        {
                            AppUtil.UpdateDetail(count);
                        }
                    }
                    else
                    {
                        break;
                    }

                    if (for_thinking &&
                        !Thinking)
                    {
                        break;
                    }

                    //Check for duplication
                    if (response_words.Count > 0)
                    {
                        List<string> new_response_words = HandleDuplication(response_words);
                        if (new_response_words.Count < response_words.Count)
                        {
                            break;
                        }
                    }

                    if (for_thinking &&
                        !Thinking)
                    {
                        break;
                    }
                }

                //Turn list of words into single string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < response_words.Count; i++)
                {
                    if (for_thinking &&
                        !Thinking)
                    {
                        break;
                    }

                    sb.Append(response_words[i]);

                    if (i < response_words.Count - 1)
                    {
                        sb.Append(" ");
                    }
                }

                if ((for_thinking && Thinking) ||
                    !for_thinking)
                {
                    response = sb.ToString();
                }
            }

            return response;
        }

        public static List<string> HandleDuplication(List<string> words)
        {
            List<string> results = new List<string>(words);

            bool dup_found = false;
            int dup_startIndex = 0;
            int dup_wordCount = 0;

            if (results.Count >= 4)
            {
                for (int length = 4; length <= results.Count; length += 2)
                {
                    int count = (int)Math.Floor((decimal)length / 2);

                    for (var i = 0; i <= results.Count - length; i++)
                    {
                        var first_chunk = "";
                        var second_chunk = "";

                        for (var c = i; c < count + i; c++)
                        {
                            first_chunk += results[c];
                            second_chunk += results[count + c];
                        }

                        if (first_chunk == second_chunk)
                        {
                            dup_found = true;
                            dup_startIndex = i;
                            dup_wordCount = count;
                            break;
                        }
                    }

                    if (dup_found)
                    {
                        break;
                    }
                }

                if (dup_found)
                {
                    results.RemoveRange(dup_startIndex, dup_wordCount);
                }
            }

            return results;
        }

        public static string RulesCheck(string old_string)
        {
            string new_string = "";

            if (old_string.Length > 0)
            {
                new_string = old_string;

                //Capitalize first word
                char first_letter = new_string[0];
                if (first_letter != char.ToUpper(first_letter))
                {
                    new_string = char.ToUpper(first_letter) + new_string.Substring(1);
                }

                //Remove spaces before special characters
                new_string = UnGapSpecials(new_string);

                //Set ending punctuation if missing
                char last_letter = new_string[new_string.Length - 1];
                if (NormalCharacters.IsMatch(last_letter.ToString()))
                {
                    new_string += ".";
                }
            }

            return new_string;
        }

        private static void PrepThought(string respond_to)
        {
            WordArray_Thinking = GapSpecials(respond_to, null, null, null, false).Trim(' ').Split(' ');
            if (WordArray_Thinking.Length > 0)
            {
                if (!string.IsNullOrEmpty(WordArray_Thinking[0]) &&
                    LearnFromThinking)
                {
                    List<SQLiteCommand> commands = new List<SQLiteCommand>();
                    commands.AddRange(AddInputs(respond_to));
                    commands.AddRange(AddWords(WordArray_Thinking));
                    commands.AddRange(AddPreWords(WordArray_Thinking));
                    commands.AddRange(AddProWords(WordArray_Thinking));
                    SqlUtil.BulkQuery(commands, false);
                }
            }
        }

        public static string Think()
        {
            Thought = "";

            try
            {
                string respond_to = "";

                List<string> outputs = new List<string>();

                bool okay = false;

                if (!string.IsNullOrEmpty(LastThought))
                {
                    respond_to = LastThought;
                }
                else if (!string.IsNullOrEmpty(CleanInput))
                {
                    respond_to = CleanInput;
                }

                if (!string.IsNullOrEmpty(respond_to))
                {
                    PrepThought(respond_to);
                    if (WordArray_Thinking.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(WordArray_Thinking[0]))
                        {
                            if (!MainForm.Crash)
                            {
                                string[] topics = SqlUtil.Get_MinWords(WordArray_Thinking, false);

                                if (!MainForm.Crash &&
                                    topics.Length > 0)
                                {
                                    if (LearnFromThinking)
                                    {
                                        UpdateTopics(respond_to, topics, false);
                                    }
                                }

                                //Get topic-based response
                                if (!MainForm.Crash &&
                                    topics.Length > 0)
                                {
                                    string[] inputs = SqlUtil.Get_InputsFromTopics(topics, false);

                                    if (!MainForm.Crash)
                                    {
                                        foreach (string input in inputs)
                                        {
                                            outputs.AddRange(SqlUtil.Get_OutputsFromInput(input, false).ToList());
                                        }
                                    }
                                }
                            }

                            if (!MainForm.Crash)
                            {
                                //Get direct response
                                if (outputs.Count == 0)
                                {
                                    outputs.AddRange(SqlUtil.Get_OutputsFromInput(respond_to, false).ToList());
                                }
                            }

                            if (!MainForm.Crash)
                            {
                                //Get generated response
                                if (outputs.Count == 0)
                                {
                                    string min_word = SqlUtil.Get_MinWord(WordArray_Thinking);

                                    if (!MainForm.Crash)
                                    {
                                        outputs.Add(GenerateResponse(min_word, false, true));
                                    }
                                }
                            }

                            if (outputs.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(outputs[0]))
                                {
                                    okay = true;
                                }
                            }
                        }
                    }
                }
                
                if (!okay)
                {
                    string word = SqlUtil.Get_RandomWord();
                    outputs.Add(GenerateResponse(word, false, true));
                }

                if (!MainForm.Crash)
                {
                    //Choose a response from outputs
                    if (outputs.Count > 0)
                    {
                        CryptoRandom random = new CryptoRandom();
                        int choice = random.Next(0, outputs.Count);

                        Thought = outputs[choice];

                        if (!MainForm.Crash &&
                            !string.IsNullOrEmpty(Thought))
                        {
                            //Clean up response
                            Thought = RulesCheck(Thought);

                            if (!string.IsNullOrEmpty(Thought))
                            {
                                if (!string.IsNullOrEmpty(LastThought) &&
                                    LearnFromThinking)
                                {
                                    UpdateOutputs(Thought, LastThought, false);
                                }

                                //Set response as last response
                                LastThought = Thought;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                List<string> log = new List<string>();
                log.Add(ex.Message);
                log.Add(ex.StackTrace);
                AppUtil.CrashLog(log);
            }

            return Thought;
        }

        #endregion
    }
}
