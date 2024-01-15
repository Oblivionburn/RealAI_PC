using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            StringBuilder sb = new StringBuilder();

            try
            {
                if (update_progress)
                {
                    AppUtil.UpdateProgress(TokenSource, progressMain, "Processing data");
                    AppUtil.UpdateProgress(TokenSource, progressMain, 30);
                }

                int count = 0;
                int total = old_string.Length;

                for (int i = 0; i < total; i++)
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
                        AppUtil.UpdateProgress(TokenSource, progressDetail, "(" + count + "/" + total + ")");
                        AppUtil.UpdateProgress(TokenSource, progressDetail, (count * 100) / total);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Brain.GapSpecials", ex.Source, ex.Message, ex.StackTrace);
            }

            return sb.ToString();
        }

        public static string UnGapSpecials(string old_string)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                int length = old_string.Length;
                for (int i = 0; i < length; i++)
                {
                    string value = old_string[i].ToString();

                    if (i < length - 1)
                    {
                        string next_value = old_string[i + 1].ToString();
                        if (!NormalCharacters.IsMatch(next_value) &&
                            next_value != ":" &&
                            value == " ")
                        {
                            //Skip spaces before special characters
                        }
                        else
                        {
                            sb.Append(value);
                        }
                    }
                    else
                    {
                        sb.Append(value);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Brain.UnGapSpecials", ex.Source, ex.Message, ex.StackTrace);
            }

            return sb.ToString();
        }

        private static void PrepInput()
        {
            try
            {
                CleanInput = RulesCheck(Input);
                WordArray = GapSpecials(CleanInput, MainForm.progressMain, MainForm.progressDetail, null, true).Trim(' ').Split(' ');

                if (WordArray.Length > 0)
                {
                    AppUtil.UpdateProgress(MainForm.progressMain, "Saving data");

                    List<SQLiteCommand> commands = new List<SQLiteCommand>();
                    commands.AddRange(AddInputs(CleanInput));
                    commands.AddRange(AddWords(WordArray));
                    commands.AddRange(AddPreWords(WordArray));
                    commands.AddRange(AddProWords(WordArray));
                    SqlUtil.BulkQuery(commands, true);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Brain.PrepInput", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        public static bool Encourage(string message)
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    string[] word_array = GapSpecials(message, MainForm.progressMain, MainForm.progressDetail, null, true).Trim(' ').Split(' ');
                    if (word_array.Length > 1)
                    {
                        AppUtil.UpdateProgress(MainForm.progressMain, "Encouraging");

                        List<string> words = new List<string>();
                        List<string> pre_words = new List<string>();
                        List<string> pro_words = new List<string>();
                        List<int> distances = new List<int>();
                        List<SQLiteCommand> commands = new List<SQLiteCommand>();

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

                        commands.AddRange(SqlUtil.Increase_PreWordPriorities(words.ToArray(), pre_words.ToArray(), distances.ToArray()));

                        words = new List<string>();
                        distances = new List<int>();

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

                        commands.AddRange(SqlUtil.Increase_ProWordPriorities(words.ToArray(), pro_words.ToArray(), distances.ToArray()));

                        commands.AddRange(SqlUtil.Increase_OutputPriorities(message));
                        commands.AddRange(SqlUtil.Increase_InputPriorities(message));
                        commands.AddRange(SqlUtil.Increase_WordPriorities(word_array));

                        return SqlUtil.BulkQuery(commands, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Brain.Encourage", ex.Source, ex.Message, ex.StackTrace);
            }

            return false;
        }

        public static bool Discourage(string message)
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    string[] word_array = GapSpecials(message, MainForm.progressMain, MainForm.progressDetail, null, true).Trim(' ').Split(' ');
                    if (word_array.Length > 1)
                    {
                        AppUtil.UpdateProgress(MainForm.progressMain, "Discouraging");

                        List<string> words = new List<string>();
                        List<string> pre_words = new List<string>();
                        List<string> pro_words = new List<string>();
                        List<int> distances = new List<int>();
                        List<SQLiteCommand> commands = new List<SQLiteCommand>();

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

                        commands.AddRange(SqlUtil.Decrease_PreWordPriorities(words.ToArray(), pre_words.ToArray(), distances.ToArray()));

                        words = new List<string>();
                        distances = new List<int>();

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

                        commands.AddRange(SqlUtil.Decrease_ProWordPriorities(words.ToArray(), pro_words.ToArray(), distances.ToArray()));

                        commands.AddRange(SqlUtil.Decrease_TopicPriorities(message, word_array));
                        commands.AddRange(SqlUtil.Decrease_OutputPriorities(message));
                        commands.AddRange(SqlUtil.Decrease_InputPriorities(message));
                        commands.AddRange(SqlUtil.Decrease_WordPriorities(word_array));

                        return SqlUtil.BulkQuery(commands, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Brain.Discourage", ex.Source, ex.Message, ex.StackTrace);
            }

            return false;
        }

        public static List<SQLiteCommand> AddInputs(string input)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                commands.AddRange(SqlUtil.Increase_InputPriorities(input));
                commands.AddRange(SqlUtil.Add_NewInput(input));
            }
            catch (Exception ex)
            {
                Logger.AddLog("Brain.AddInputs", ex.Source, ex.Message, ex.StackTrace);
            }
            
            return commands;
        }

        public static List<SQLiteCommand> AddWords(string[] word_array)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                commands.AddRange(SqlUtil.Increase_WordPriorities(word_array));
                commands.AddRange(SqlUtil.Add_Words(word_array));
            }
            catch (Exception ex)
            {
                Logger.AddLog("Brain.AddWords", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<SQLiteCommand> AddPreWords(string[] word_array)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                List<string> words = new List<string>();
                List<string> pre_words = new List<string>();
                List<int> distances = new List<int>();

                int length = word_array.Length;
                for (int i = 1; i < length; i++)
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

                commands.AddRange(SqlUtil.Increase_PreWordPriorities(words.ToArray(), pre_words.ToArray(), distances.ToArray()));
                commands.AddRange(SqlUtil.Add_PreWords(words.ToArray(), pre_words.ToArray(), distances.ToArray()));
            }
            catch (Exception ex)
            {
                Logger.AddLog("Brain.AddPreWords", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        public static List<SQLiteCommand> AddProWords(string[] word_array)
        {
            List<SQLiteCommand> commands = new List<SQLiteCommand>();

            try
            {
                List<string> words = new List<string>();
                List<string> pro_words = new List<string>();
                List<int> distances = new List<int>();

                int length = word_array.Length - 1;
                for (int i = 0; i < length; i++)
                {
                    var count = 1;
                    for (int j = i + 1; j <= length; j++)
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

                commands.AddRange(SqlUtil.Increase_ProWordPriorities(words.ToArray(), pro_words.ToArray(), distances.ToArray()));
                commands.AddRange(SqlUtil.Add_ProWords(words.ToArray(), pro_words.ToArray(), distances.ToArray()));
            }
            catch (Exception ex)
            {
                Logger.AddLog("Brain.AddProWords", ex.Source, ex.Message, ex.StackTrace);
            }

            return commands;
        }

        private static void UpdateTopics(string input, string[] topics, bool update_progress)
        {
            try
            {
                AppUtil.UpdateProgress(MainForm.progressMain, "Updating topics");

                List<SQLiteCommand> commands = new List<SQLiteCommand>();
                commands.AddRange(SqlUtil.Increase_TopicPriorities(input, topics));
                commands.AddRange(SqlUtil.AddTopics(input, topics));
                commands.AddRange(SqlUtil.Decrease_Topics_Unmatched(input, topics));
                commands.AddRange(SqlUtil.Clean_Topics(input));
                SqlUtil.BulkQuery(commands, update_progress);
            }
            catch (Exception ex)
            {
                Logger.AddLog("Brain.UpdateTopics", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        private static void UpdateOutputs(string input, string output, bool update_progress)
        {
            try
            {
                if (update_progress)
                {
                    AppUtil.UpdateProgress(MainForm.progressMain, "Adding potential output");
                }

                List<SQLiteCommand> commands = new List<SQLiteCommand>();
                commands.AddRange(SqlUtil.Increase_OutputPriorities(output, input));
                commands.AddRange(SqlUtil.Add_Outputs(output, input));
                SqlUtil.BulkQuery(commands, update_progress);
            }
            catch (Exception ex)
            {
                Logger.AddLog("Brain.UpdateOutputs", ex.Source, ex.Message, ex.StackTrace);
            }
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
                            if (Topics != null &&
                                MainForm.TopicResponding)
                            {
                                //Get topic-based response
                                if (Topics.Length > 0)
                                {
                                    outputs.AddRange(SqlUtil.Get_OutputsFromTopics(Topics, false));
                                }
                            }
                            
                            if (outputs.Count == 0 &&
                                MainForm.InputResponding)
                            {
                                //Get direct response
                                if (!string.IsNullOrEmpty(CleanInput))
                                {
                                    outputs.AddRange(SqlUtil.Get_OutputsFromInput(CleanInput, false).ToList());
                                }
                            }

                            if (outputs.Count == 0 &&
                                MainForm.ProceduralResponding)
                            {
                                //Get generated response
                                string min_word = SqlUtil.Get_MinWord(WordArray);
                                outputs.Add(GenerateResponse(min_word, false, false));
                            }
                        }
                    }

                    //Still got nothing? Say something random.
                    if (outputs.Count == 0 &&
                        MainForm.ProceduralResponding)
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
                        AppUtil.UpdateProgress(MainForm.progressMain, 10);
                        MainForm.intervals.Clear();

                        //Add input as output to last response
                        if (!string.IsNullOrEmpty(LastResponse))
                        {
                            AppUtil.UpdateProgress(MainForm.progressMain, "Adding input as output...");

                            UpdateOutputs(CleanInput, LastResponse, true);
                        }
                        AppUtil.UpdateProgress(MainForm.progressMain, 20);
                        MainForm.intervals.Clear();

                        AppUtil.UpdateProgress(MainForm.progressMain, "Getting topic(s) from input...");

                        //Get lowest priority words from input
                        Topics = SqlUtil.Get_MinWords(WordArray, true);

                        AppUtil.UpdateProgress(MainForm.progressMain, 30);
                        MainForm.intervals.Clear();

                        if (Topics.Length > 0)
                        {
                            AppUtil.UpdateProgress(MainForm.progressMain, "Adding new topic(s)...");

                            //Add words as topics for input
                            UpdateTopics(CleanInput, Topics, true);
                        }
                        AppUtil.UpdateProgress(MainForm.progressMain, 40);
                        MainForm.intervals.Clear();

                        //Get topic-based response
                        if (Topics.Length > 0 &&
                            MainForm.TopicResponding)
                        {
                            AppUtil.UpdateProgress(MainForm.progressMain, "Getting outputs from topics...");

                            //Get highest priority output(s) from matching topics
                            outputs.AddRange(SqlUtil.Get_OutputsFromTopics(Topics, true));
                        }
                        AppUtil.UpdateProgress(MainForm.progressMain, 50);
                        MainForm.intervals.Clear();

                        if (outputs.Count == 0 &&
                            MainForm.InputResponding)
                        {
                            AppUtil.UpdateProgress(MainForm.progressMain, "Getting direct output...");

                            //Get direct response
                            outputs.AddRange(SqlUtil.Get_OutputsFromInput(CleanInput, true).ToList());
                        }
                        AppUtil.UpdateProgress(MainForm.progressMain, 60);
                        MainForm.intervals.Clear();

                        string min_word = "";
                        if (outputs.Count == 0)
                        {
                            AppUtil.UpdateProgress(MainForm.progressMain, "Getting best topic...");

                            //Get lowest priority word
                            min_word = SqlUtil.Get_MinWord(WordArray);
                        }
                        AppUtil.UpdateProgress(MainForm.progressMain, 70);
                        MainForm.intervals.Clear();

                        if (outputs.Count == 0 &&
                            !string.IsNullOrEmpty(min_word) &&
                            MainForm.ProceduralResponding)
                        {
                            AppUtil.UpdateProgress(MainForm.progressMain, "Generating response...");

                            //Generate response from lowest priority word
                            outputs.Add(GenerateResponse(min_word, true, false));
                        }
                        AppUtil.UpdateProgress(MainForm.progressMain, 80);
                        MainForm.intervals.Clear();

                        string random_word = "";
                        if (outputs.Count == 0)
                        {
                            AppUtil.UpdateProgress(MainForm.progressMain, "Getting random word...");

                            //Still got nothing? Pick a random word.
                            random_word = SqlUtil.Get_RandomWord();
                        }
                        AppUtil.UpdateProgress(MainForm.progressMain, 90);
                        MainForm.intervals.Clear();

                        if (outputs.Count == 0 &&
                            !string.IsNullOrEmpty(random_word) &&
                            MainForm.ProceduralResponding)
                        {
                            AppUtil.UpdateProgress(MainForm.progressMain, "Generating response...");

                            //Generate response from random word
                            outputs.Add(GenerateResponse(random_word, false, false));
                        }
                        AppUtil.UpdateProgress(MainForm.progressMain, 100);
                        MainForm.intervals.Clear();
                    }
                }

                if (outputs.Count > 0)
                {
                    //Choose a response from outputs at random
                    CryptoRandom random = new CryptoRandom();
                    int choice = random.Next(0, outputs.Count);

                    Response = outputs[choice];

                    if (!string.IsNullOrEmpty(Response))
                    {
                        //Clean up response
                        Response = RulesCheck(Response);

                        if (!string.IsNullOrEmpty(Response))
                        {
                            //Set response as last response
                            LastResponse = Response;

                            //Interrupt thinking
                            LastThought = "";
                            AppUtil.UpdateProgress(MainForm.progressMain, "Responded");
                        }
                    }
                }

                AppUtil.UpdateProgress(MainForm.progressDetail, 100);
            }
            catch (Exception ex)
            {
                Logger.AddLog("Brain.Respond", ex.Source, ex.Message, ex.StackTrace);
            }
            
            return Response;
        }

        public static string GenerateResponse(string topic, bool update_progress, bool for_thinking)
        {
            string response = "";

            try
            {
                if (!string.IsNullOrEmpty(topic))
                {
                    int count = 0;

                    if (update_progress)
                    {
                        AppUtil.UpdateProgress(MainForm.progressDetail, 0);
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
                                AppUtil.UpdateProgress(MainForm.progressDetail, count);
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
                                AppUtil.UpdateProgress(MainForm.progressDetail, count);
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
            }
            catch (Exception ex)
            {
                Logger.AddLog("Brain.GenerateResponse", ex.Source, ex.Message, ex.StackTrace);
            }

            return response;
        }

        public static List<string> HandleDuplication(List<string> words)
        {
            List<string> results = new List<string>(words);

            try
            {
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
            }
            catch (Exception ex)
            {
                Logger.AddLog("Brain.HandleDuplication", ex.Source, ex.Message, ex.StackTrace);
            }

            return results;
        }

        public static string RulesCheck(string old_string)
        {
            string new_string = "";

            try
            {
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
                    if (new_string.Length > 0)
                    {
                        char last_letter = new_string[new_string.Length - 1];
                        if (NormalCharacters.IsMatch(last_letter.ToString()))
                        {
                            new_string += ".";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("Brain.RulesCheck", ex.Source, ex.Message, ex.StackTrace);
            }

            return new_string;
        }

        private static void PrepThought(string respond_to)
        {
            try
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
            catch (Exception ex)
            {
                Logger.AddLog("Brain.PrepThought", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        public static string Think()
        {
            Thought = "";

            try
            {
                string respond_to = "";
                List<string> outputs = new List<string>();

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
                            string[] topics = SqlUtil.Get_MinWords(WordArray_Thinking, false);

                            if (topics.Length > 0)
                            {
                                if (LearnFromThinking)
                                {
                                    UpdateTopics(respond_to, topics, false);
                                }
                            }

                            if (topics.Length > 0 &&
                                MainForm.TopicResponding)
                            {
                                //Get topic-based response
                                outputs.AddRange(SqlUtil.Get_OutputsFromTopics(topics, false));
                            }

                            if (outputs.Count == 0 &&
                                MainForm.InputResponding)
                            {
                                //Get direct response
                                outputs.AddRange(SqlUtil.Get_OutputsFromInput(respond_to, false).ToList());
                            }

                            if (outputs.Count == 0 &&
                                MainForm.ProceduralResponding)
                            {
                                //Get generated response
                                string min_word = SqlUtil.Get_MinWord(WordArray_Thinking);

                                if (!string.IsNullOrEmpty(min_word))
                                {
                                    outputs.Add(GenerateResponse(min_word, false, true));
                                }
                            }

                            if (outputs.Count == 0 &&
                                MainForm.ProceduralResponding)
                            {
                                string word = SqlUtil.Get_RandomWord();
                                outputs.Add(GenerateResponse(word, false, true));
                            }
                        }
                    }
                }

                if (outputs.Count > 0)
                {
                    //Choose a response from outputs
                    CryptoRandom random = new CryptoRandom();
                    int choice = random.Next(0, outputs.Count);

                    Thought = outputs[choice];

                    if (!string.IsNullOrEmpty(Thought))
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
            catch (Exception ex)
            {
                Logger.AddLog("Brain.Think", ex.Source, ex.Message, ex.StackTrace);
            }

            return Thought;
        }

        #endregion
    }
}
