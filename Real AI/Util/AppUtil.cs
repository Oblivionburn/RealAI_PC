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

        public static void Set_Config(string name, string value)
        {
            try
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
            catch (Exception ex)
            {
                Logger.AddLog("AppUtil.Set_Config", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        public static string Get_Config(string name)
        {
            try
            {
                string[] lines = File.ReadAllLines(MainForm.Config);
                foreach (string line in lines)
                {
                    if (line.Contains(name))
                    {
                        string[] results = line.Split('=');
                        if (results.Length > 1)
                        {
                            string value = results[1];
                            if (!string.IsNullOrEmpty(value))
                            {
                                if (value != "Nothing")
                                {
                                    return value;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("AppUtil.Get_Config", ex.Source, ex.Message, ex.StackTrace);
            }

            return null;
        }

        public static void Set_Color(string name, string value)
        {
            try
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

                File.WriteAllLines(MainForm.Colors, lines);
            }
            catch (Exception ex)
            {
                Logger.AddLog("AppUtil.Set_Color", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        public static string Get_Color(string name)
        {
            try
            {
                string[] lines = File.ReadAllLines(MainForm.Colors);
                foreach (string line in lines)
                {
                    if (line.Contains(name))
                    {
                        string[] results = line.Split('=');
                        if (results.Length > 1)
                        {
                            string value = results[1];
                            if (!string.IsNullOrEmpty(value))
                            {
                                if (value != "Nothing")
                                {
                                    return value;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("AppUtil.Get_Color", ex.Source, ex.Message, ex.StackTrace);
            }

            return null;
        }

        public static List<string> GetHistory()
        {
            List<string> history = new List<string>();

            try
            {
                if (!string.IsNullOrEmpty(MainForm.Current_HistoryDir))
                {
                    string date = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString();
                    string file = Path.Combine(MainForm.Current_HistoryDir, date + ".txt");

                    if (File.Exists(file))
                    {
                        history = File.ReadAllLines(file).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("AppUtil.GetHistory", ex.Source, ex.Message, ex.StackTrace);
            }
            
            return history;
        }

        public static void SaveHistory(List<string> history)
        {
            try
            {
                if (!string.IsNullOrEmpty(MainForm.Current_HistoryDir))
                {
                    string date = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString();
                    string file = Path.Combine(MainForm.Current_HistoryDir, date + ".txt");
                    File.WriteAllLines(file, history);
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("AppUtil.SaveHistory", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        public static bool IsCancelled(CancellationTokenSource TokenSource)
        {
            try
            {
                if (TokenSource != null)
                {
                    if (TokenSource.IsCancellationRequested)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("AppUtil.IsCancelled", ex.Source, ex.Message, ex.StackTrace);
            }

            return false;
        }

        public static void UpdateProgress(TextProgressBar progressMain, string value)
        {
            try
            {
                if (progressMain.InvokeRequired)
                {
                    progressMain.Invoke((MethodInvoker)delegate
                    {
                        progressMain.CustomText = value;
                    });
                }
                else
                {
                    progressMain.CustomText = value;
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("AppUtil.UpdateProgress", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        public static void UpdateProgress(TextProgressBar progressMain, int value)
        {
            try
            {
                if (progressMain.InvokeRequired)
                {
                    progressMain.Invoke((MethodInvoker)delegate
                    {
                        progressMain.Value = value;
                    });
                }
                else
                {
                    progressMain.Value = value;
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("AppUtil.UpdateProgress", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        public static void UpdateProgress(CancellationTokenSource TokenSource, TextProgressBar progressMain, string value)
        {
            try
            {
                if (!IsCancelled(TokenSource))
                {
                    if (progressMain.InvokeRequired)
                    {
                        progressMain.Invoke((MethodInvoker)delegate
                        {
                            progressMain.CustomText = value;
                        });
                    }
                    else
                    {
                        progressMain.CustomText = value;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("AppUtil.UpdateProgress", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        public static void UpdateProgress(CancellationTokenSource TokenSource, TextProgressBar progressMain, int value)
        {
            try
            {
                if (!IsCancelled(TokenSource))
                {
                    if (progressMain.InvokeRequired)
                    {
                        progressMain.Invoke((MethodInvoker)delegate
                        {
                            progressMain.Value = value;
                        });
                    }
                    else
                    {
                        progressMain.Value = value;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("AppUtil.UpdateProgress", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        public static void UpdateLabel(Label label, string value)
        {
            try
            {
                if (label.InvokeRequired)
                {
                    label.Invoke((MethodInvoker)delegate
                    {
                        label.Text = value;
                    });
                }
                else
                {
                    label.Text = value;
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("AppUtil.UpdateLabel", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        public static void UpdateTextbox(TextBox textbox, string value)
        {
            try
            {
                if (textbox.InvokeRequired)
                {
                    textbox.Invoke((MethodInvoker)delegate
                    {
                        textbox.Text = value;
                    });
                }
                else
                {
                    textbox.Text = value;
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("AppUtil.UpdateTextbox", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        public static void UpdateButton(Button button, bool enabled)
        {
            try
            {
                if (button.InvokeRequired)
                {
                    button.Invoke((MethodInvoker)delegate
                    {
                        button.Enabled = enabled;
                    });
                }
                else
                {
                    button.Enabled = enabled;
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("AppUtil.UpdateTextbox", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        public static void UpdateLabel(CancellationTokenSource TokenSource, Label label, string value)
        {
            try
            {
                if (!IsCancelled(TokenSource))
                {
                    if (label.InvokeRequired)
                    {
                        label.Invoke((MethodInvoker)delegate
                        {
                            label.Text = value;
                        });
                    }
                    else
                    {
                        label.Text = value;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("AppUtil.UpdateLabel", ex.Source, ex.Message, ex.StackTrace);
            }
        }

        public static string GetTime_Milliseconds(DateTime start)
        {
            string result = "";

            try
            {
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
            }
            catch (Exception ex)
            {
                Logger.AddLog("AppUtil.GetTime_Milliseconds", ex.Source, ex.Message, ex.StackTrace);
            }

            return result;
        }

        public static string ConvertTime_Milliseconds(int milliseconds)
        {
            string result = "";

            try
            {
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
            }
            catch (Exception ex)
            {
                Logger.AddLog("AppUtil.ConvertTime_Milliseconds", ex.Source, ex.Message, ex.StackTrace);
            }
            
            return result;
        }

        public static string ColorToHex(Color c)
        {
            string result = "";

            try
            { 
                result = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
            }
            catch (Exception ex)
            {
                Logger.AddLog("AppUtil.ColorToHex", ex.Source, ex.Message, ex.StackTrace);
            }

            return result;
        }

        public static void Update_Colors()
        {
            try
            {
                background_window = ColorTranslator.FromHtml(Get_Color("Color_Background_Window"));
                text_window = ColorTranslator.FromHtml(Get_Color("Color_Text_Window"));

                background_control = ColorTranslator.FromHtml(Get_Color("Color_Background_Control"));
                text_control = ColorTranslator.FromHtml(Get_Color("Color_Text_Control"));
                highlight_control = ColorTranslator.FromHtml(Get_Color("Color_Highlight_Control"));
                selected_control = ColorTranslator.FromHtml(Get_Color("Color_Selected_Control"));

                background_progress = ColorTranslator.FromHtml(Get_Color("Color_Background_Progress"));
                text_progress = ColorTranslator.FromHtml(Get_Color("Color_Text_Progress"));

                MainForm.SetColors();
            }
            catch (Exception ex)
            {
                Logger.AddLog("AppUtil.Update_Colors", ex.Source, ex.Message, ex.StackTrace);
            }
        }
    }
}
