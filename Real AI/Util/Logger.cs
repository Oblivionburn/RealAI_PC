using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Real_AI.Util
{
    public static class Logger
    {
        #region Variables

        public static List<Log> Logs = new List<Log>();

        #endregion

        #region Methods
        
        public static void AddLog(string method, string source, string message, string stack_trace)
        {
            MessageBox.Show(message + Environment.NewLine + stack_trace, "Error in " + method, MessageBoxButtons.OK, MessageBoxIcon.Error);
            Logs.Add(new Log(source, message, stack_trace));
        }

        public static void WriteLog()
        {
            if (Logs.Count > 0)
            {
                string filename = string.Format("{0}_{1:yyyy-MM-dd_hh-mm-ss}{2}", @"\log", DateTime.Now, ".txt");
                string path = Environment.CurrentDirectory + filename;

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < Logs.Count; i++)
                {
                    Log log = Logs[i];
                    sb.AppendLine("TimeStamp: " + log.TimeStamp.ToString());
                    sb.AppendLine("Source: " + log.Source);
                    sb.AppendLine("Message: " + log.Message);
                    sb.AppendLine("Stack Trace: " + log.StackTrace);
                    sb.AppendLine();
                }

                File.WriteAllText(path, sb.ToString());
            }
        }

        #endregion
    }

    public class Log
    {
        public DateTime TimeStamp;
        public string Source;
        public string Message;
        public string StackTrace;

        public Log(string source, string message, string stack_trace)
        {
            TimeStamp = DateTime.Now;
            Source = source;
            Message = message;
            StackTrace = stack_trace;
        }
    }
}
