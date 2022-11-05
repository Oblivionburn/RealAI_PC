using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;

namespace Real_AI.Util
{
    public static class AppUtil
    {
        public static Color background_window;
        public static Color text_window;
        public static Color background_control;
        public static Color text_control;
        public static Color highlight_control;
        public static Color selected_control;
        public static Color background_progress;
        public static Color text_progress;

        public static void Set_ID(string value)
        {
            List<string> lines = File.ReadAllLines(MainForm.Config).ToList();

            bool found = false;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains("ID"))
                {
                    found = true;
                    lines[i] = "ID=" + value;
                    break;
                }
            }

            if (!found)
            {
                lines.Add("ID=" + value);
            }

            File.WriteAllLines(MainForm.Config, lines);
        }

        public static string Get_ID()
        {
            string[] lines = File.ReadAllLines(MainForm.Config);
            foreach (string line in lines)
            {
                if (line.Contains("ID"))
                {
                    string[] results = line.Split('=');
                    if (results.Length > 1)
                    {
                        if (!string.IsNullOrEmpty(results[1]))
                        {
                            return results[1];
                        }
                    }
                }
            }

            return null;
        }

        public static void Set_ThinkSpeed(int speed)
        {
            List<string> lines = File.ReadAllLines(MainForm.Config).ToList();

            bool found = false;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains("ThinkSpeed"))
                {
                    found = true;
                    lines[i] = "ThinkSpeed=" + speed.ToString();
                    break;
                }
            }

            if (!found)
            {
                lines.Add("ThinkSpeed=" + speed.ToString());
            }

            File.WriteAllLines(MainForm.Config, lines);
        }

        public static int Get_ThinkSpeed()
        {
            string[] lines = File.ReadAllLines(MainForm.Config);
            foreach (string line in lines)
            {
                if (line.Contains("ThinkSpeed"))
                {
                    string[] results = line.Split('=');
                    if (results.Length > 1)
                    {
                        if (!string.IsNullOrEmpty(results[1]))
                        {
                            return int.Parse(results[1]);
                        }
                    }
                }
            }

            return 1000;
        }

        public static void Set_Thinking(string value)
        {
            List<string> lines = File.ReadAllLines(MainForm.Config).ToList();

            bool found = false;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains("Thinking"))
                {
                    found = true;
                    lines[i] = "Thinking=" + value;
                    break;
                }
            }

            if (!found)
            {
                lines.Add("Thinking=" + value);
            }

            File.WriteAllLines(MainForm.Config, lines);
        }

        public static bool Get_Thinking()
        {
            string[] lines = File.ReadAllLines(MainForm.Config);
            foreach (string line in lines)
            {
                if (line.Contains("Thinking"))
                {
                    string[] results = line.Split('=');
                    if (results.Length > 1)
                    {
                        if (!string.IsNullOrEmpty(results[1]))
                        {
                            if (results[1] == "True")
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static void Set_LearnFromThinking(string value)
        {
            List<string> lines = File.ReadAllLines(MainForm.Config).ToList();

            bool found = false;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains("LearnFromThinking"))
                {
                    found = true;
                    lines[i] = "LearnFromThinking=" + value;
                    break;
                }
            }

            if (!found)
            {
                lines.Add("LearnFromThinking=" + value);
            }

            File.WriteAllLines(MainForm.Config, lines);
        }

        public static bool Get_LearnFromThinking()
        {
            string[] lines = File.ReadAllLines(MainForm.Config);
            foreach (string line in lines)
            {
                if (line.Contains("LearnFromThinking"))
                {
                    string[] results = line.Split('=');
                    if (results.Length > 1)
                    {
                        if (!string.IsNullOrEmpty(results[1]))
                        {
                            if (results[1] == "True")
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static void Set_AttentionSpan(int value)
        {
            List<string> lines = File.ReadAllLines(MainForm.Config).ToList();

            bool found = false;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains("AttentionSpan"))
                {
                    found = true;
                    lines[i] = "AttentionSpan=" + value;
                    break;
                }
            }

            if (!found)
            {
                lines.Add("AttentionSpan=" + value);
            }

            File.WriteAllLines(MainForm.Config, lines);
        }

        public static int Get_AttentionSpan()
        {
            string[] lines = File.ReadAllLines(MainForm.Config);
            foreach (string line in lines)
            {
                if (line.Contains("AttentionSpan"))
                {
                    string[] results = line.Split('=');
                    if (results.Length > 1)
                    {
                        if (!string.IsNullOrEmpty(results[1]))
                        {
                            return int.Parse(results[1]);
                        }
                    }
                }
            }

            return 7;
        }

        public static void Set_Initiate(string value)
        {
            List<string> lines = File.ReadAllLines(MainForm.Config).ToList();

            bool found = false;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains("Initiate"))
                {
                    found = true;
                    lines[i] = "Initiate=" + value;
                    break;
                }
            }

            if (!found)
            {
                lines.Add("Initiate=" + value);
            }

            File.WriteAllLines(MainForm.Config, lines);
        }

        public static bool Get_Initiate()
        {
            string[] lines = File.ReadAllLines(MainForm.Config);
            foreach (string line in lines)
            {
                if (line.Contains("Initiate"))
                {
                    string[] results = line.Split('=');
                    if (results.Length > 1)
                    {
                        if (!string.IsNullOrEmpty(results[1]))
                        {
                            if (results[1] == "True")
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static void Set_TTS(string value)
        {
            List<string> lines = File.ReadAllLines(MainForm.Config).ToList();

            bool found = false;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains("TTS"))
                {
                    found = true;
                    lines[i] = "TTS=" + value;
                    break;
                }
            }

            if (!found)
            {
                lines.Add("TTS=" + value);
            }

            File.WriteAllLines(MainForm.Config, lines);
        }

        public static bool Get_TTS()
        {
            string[] lines = File.ReadAllLines(MainForm.Config);
            foreach (string line in lines)
            {
                if (line.Contains("TTS"))
                {
                    string[] results = line.Split('=');
                    if (results.Length > 1)
                    {
                        if (!string.IsNullOrEmpty(results[1]))
                        {
                            if (results[1] == "True")
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static void Set_TTS_Voice(string value)
        {
            List<string> lines = File.ReadAllLines(MainForm.Config).ToList();

            bool found = false;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains("TTS_Voice"))
                {
                    found = true;
                    lines[i] = "TTS_Voice=" + value;
                    break;
                }
            }

            if (!found)
            {
                lines.Add("TTS_Voice=" + value);
            }

            File.WriteAllLines(MainForm.Config, lines);
        }

        public static string Get_TTS_Voice()
        {
            string[] lines = File.ReadAllLines(MainForm.Config);
            foreach (string line in lines)
            {
                if (line.Contains("TTS_Voice"))
                {
                    string[] results = line.Split('=');
                    if (results.Length > 1)
                    {
                        if (!string.IsNullOrEmpty(results[1]))
                        {
                            return results[1];
                        }
                    }
                }
            }

            return null;
        }

        public static void Set_Config_Color(string name, string value)
        {
            List<string> lines = File.ReadAllLines(MainForm.Config).ToList();

            bool found = false;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains(name))
                {
                    found = true;
                    lines[i] = name + "=" + value;
                    break;
                }
            }

            if (!found)
            {
                lines.Add(name + "=" + value);
            }

            File.WriteAllLines(MainForm.Config, lines);
        }

        public static string Get_Config_Color(string name)
        {
            string[] lines = File.ReadAllLines(MainForm.Config);
            foreach (string line in lines)
            {
                if (line.Contains(name))
                {
                    string[] results = line.Split('=');
                    if (results.Length > 1)
                    {
                        if (!string.IsNullOrEmpty(results[1]))
                        {
                            return results[1];
                        }
                    }
                }
            }

            return null;
        }

        public static bool CheckForUpdates()
        {
            string[] lines = File.ReadAllLines(MainForm.Config);
            foreach (string line in lines)
            {
                if (line.Contains("AutoCheckUpdate"))
                {
                    string[] results = line.Split('=');
                    if (results.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(results[1]))
                        {
                            if (results[1] == "True")
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static void Set_LastBrain(string brainFile)
        {
            List<string> lines = File.ReadAllLines(MainForm.Config).ToList();

            bool found = false;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains("LastBrain"))
                {
                    found = true;
                    lines[i] = "LastBrain=" + brainFile;
                    break;
                }
            }

            if (!found)
            {
                lines.Add("LastBrain=" + brainFile);
            }

            File.WriteAllLines(MainForm.Config, lines);
        }

        public static string Get_LastBrain()
        {
            string[] lines = File.ReadAllLines(MainForm.Config);
            foreach (string line in lines)
            {
                if (line.Contains("LastBrain"))
                {
                    string[] results = line.Split('=');
                    if (results.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(results[1]))
                        {
                            if (results[1] != "Nothing")
                            {
                                return results[1];
                            }
                        }
                    }
                }
            }

            return null;
        }

        public static void Set_Version(string version)
        {
            List<string> lines = File.ReadAllLines(MainForm.Config).ToList();

            bool found = false;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains("Version"))
                {
                    found = true;
                    lines[i] = "Version=" + version;
                    break;
                }
            }

            if (!found)
            {
                lines.Add("Version=" + version);
            }

            File.WriteAllLines(MainForm.Config, lines);
        }

        public static List<string> GetHistory()
        {
            List<string> history = new List<string>();

            if (!string.IsNullOrEmpty(MainForm.Current_HistoryDir))
            {
                string date = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString();
                string file = Path.Combine(MainForm.Current_HistoryDir, date + ".txt");

                if (File.Exists(file))
                {
                    history = File.ReadAllLines(file).ToList();
                }
            }
            
            return history;
        }

        public static void SaveHistory(List<string> history)
        {
            if (!string.IsNullOrEmpty(MainForm.Current_HistoryDir))
            {
                string date = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString();
                string file = Path.Combine(MainForm.Current_HistoryDir, date + ".txt");
                File.WriteAllLines(file, history);
            }
        }

        public static bool IsCancelled(CancellationTokenSource TokenSource)
        {
            if (TokenSource != null)
            {
                if (TokenSource.IsCancellationRequested)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void UpdateMain(string value)
        {
            if (MainForm.progressMain.InvokeRequired)
            {
                MainForm.progressMain.Invoke((MethodInvoker)delegate
                {
                    MainForm.progressMain.CustomText = value;
                });
            }
            else
            {
                MainForm.progressMain.CustomText = value;
            }
        }

        public static void UpdateMain(int value)
        {
            if (MainForm.progressMain.InvokeRequired)
            {
                MainForm.progressMain.Invoke((MethodInvoker)delegate
                {
                    MainForm.progressMain.Value = value;
                });
            }
            else
            {
                MainForm.progressMain.Value = value;
            }
        }

        public static void UpdateDetail(int value)
        {
            if (MainForm.progressDetail.InvokeRequired)
            {
                MainForm.progressDetail.Invoke((MethodInvoker)delegate
                {
                    MainForm.progressDetail.Value = value;
                });
            }
            else
            {
                MainForm.progressDetail.Value = value;
            }
        }

        public static void CrashLog(List<string> Log)
        {
            MainForm.Crash = true;

            foreach (string log in Log)
            {
                MainForm.Log.Add(log);
            }
        }

        public static string GetTime_Milliseconds(DateTime start)
        {
            string result = "";

            int milliseconds = (int)(DateTime.Now - start).TotalMilliseconds;

            int seconds = 0;
            for (int i = 1000; i <= milliseconds;)
            {
                seconds++;
                milliseconds -= 1000;
            }

            int minutes = 0;
            for (int i = 60; i <= seconds;)
            {
                minutes++;
                seconds -= 60;
            }

            int hours = 0;
            for (int i = 60; i <= minutes;)
            {
                hours++;
                minutes -= 60;
            }

            if (hours > 9)
            {
                result += hours;
            }
            else
            {
                result += "0" + hours;
            }

            if (minutes > 9)
            {
                result += ":" + minutes;
            }
            else
            {
                result += ":0" + minutes;
            }

            if (seconds > 9)
            {
                result += ":" + seconds;
            }
            else
            {
                result += ":0" + seconds;
            }

            if (milliseconds > 99)
            {
                result += "." + milliseconds;
            }
            else if (milliseconds > 9)
            {
                result += ".0" + milliseconds;
            }
            else
            {
                result += ".00" + milliseconds;
            }

            return result;
        }

        public static string ConvertTime_Milliseconds(int milliseconds)
        {
            string result = "";

            int seconds = 0;
            for (int i = 1000; i <= milliseconds;)
            {
                seconds++;
                milliseconds -= 1000;
            }

            int minutes = 0;
            for (int i = 60; i <= seconds;)
            {
                minutes++;
                seconds -= 60;
            }

            int hours = 0;
            for (int i = 60; i <= minutes;)
            {
                hours++;
                minutes -= 60;
            }

            if (hours > 9)
            {
                result += hours;
            }
            else
            {
                result += "0" + hours;
            }

            if (minutes > 9)
            {
                result += ":" + minutes;
            }
            else
            {
                result += ":0" + minutes;
            }

            if (seconds > 9)
            {
                result += ":" + seconds;
            }
            else
            {
                result += ":0" + seconds;
            }

            if (milliseconds > 99)
            {
                result += "." + milliseconds;
            }
            else if (milliseconds > 9)
            {
                result += ".0" + milliseconds;
            }
            else
            {
                result += ".00" + milliseconds;
            }

            return result;
        }

        public static string ColorToHex(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        public static void Update_Colors()
        {
            background_window = ColorTranslator.FromHtml(Get_Config_Color("Color_Background_Window"));
            text_window = ColorTranslator.FromHtml(Get_Config_Color("Color_Text_Window"));

            background_control = ColorTranslator.FromHtml(Get_Config_Color("Color_Background_Control"));
            text_control = ColorTranslator.FromHtml(Get_Config_Color("Color_Text_Control"));
            highlight_control = ColorTranslator.FromHtml(Get_Config_Color("Color_Highlight_Control"));
            selected_control = ColorTranslator.FromHtml(Get_Config_Color("Color_Selected_Control"));

            background_progress = ColorTranslator.FromHtml(Get_Config_Color("Color_Background_Progress"));
            text_progress = ColorTranslator.FromHtml(Get_Config_Color("Color_Text_Progress"));

            MainForm.SetColors();
        }
    }
}
