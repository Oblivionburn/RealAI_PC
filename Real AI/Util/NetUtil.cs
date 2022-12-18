using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;

namespace Real_AI.Util
{
    public static class NetUtil
    {
        #region Variables

        public static int Port = 32400;
        public static string ConnectedIP;

        public static TcpListener Listener;
        public static ASCIIEncoding Encoder = new ASCIIEncoding();
        public const int BufferSize = 10240;

        public static bool Listening;
        public static bool Stopped;

        public static string Message_Sent;
        public static List<string> Messages_Received;
        public static int Current_MessageReceived;

        public static string Current_FileDirectory;
        public static string Error;

        #endregion

        #region Methods

        public static void Update()
        {
            Messages_Received = new List<string>();

            while (true)
            {
                if (Stopped)
                {
                    Stopped = false;
                    break;
                }

                if (Listening)
                {
                    try
                    {
                        if (Listener != null)
                        {
                            if (ConnectedIP == "localhost")
                            {
                                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RealAI\\link.txt";
                                if (File.Exists(path))
                                {
                                    foreach (string line in File.ReadAllLines(path))
                                    {
                                        if (!string.IsNullOrEmpty(line))
                                        {
                                            string[] parts = line.Split('|');
                                            if (parts.Length == 3)
                                            {
                                                if (parts[0] == "REAL_AI")
                                                {
                                                    string[] id = parts[1].Split('=');
                                                    if (id.Length > 1)
                                                    {
                                                        if (id[1] != MainForm.ID)
                                                        {
                                                            if (!Messages_Received.Contains(line))
                                                            {
                                                                Messages_Received.Add(line);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    Thread.Sleep(2000);
                                }
                            }
                            else if (Listener.Pending())
                            {
                                using (TcpClient client = Listener.AcceptTcpClient())
                                {
                                    
                                    using (NetworkStream stream = client.GetStream())
                                    {
                                        string message = "";
                                        byte[] buffer = new byte[BufferSize];
                                        int read;

                                        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                                        {
                                            message += Encoder.GetString(buffer);
                                        }

                                        if (!string.IsNullOrEmpty(message))
                                        {
                                            Messages_Received.Add(message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.AddLog("NetUtil.Update", ex.Source, ex.Message, ex.StackTrace);
                    }
                }
            }
        }

        public static string Get_CurrentMessage()
        {
            try
            {
                if (Messages_Received.Count > 0)
                {
                    return Messages_Received[Messages_Received.Count - 1];
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog("NetUtil.Get_CurrentMessage", ex.Source, ex.Message, ex.StackTrace);
                return ex.Message;
            }

            return null;
        }

        public static string Start()
        {
            try
            {
                if (Listener != null)
                {
                    Listener.Start();
                    Listening = true;
                }

                return "ok";
            }
            catch (Exception ex)
            {
                Logger.AddLog("NetUtil.Start", ex.Source, ex.Message, ex.StackTrace);
                return ex.Message;
            }
        }

        public static string Stop()
        {
            try
            {
                if (Listener != null)
                {
                    Listener.Stop();
                }
                
                Stopped = true;
                Listening = false;
                return "ok";
            }
            catch (Exception ex)
            {
                Logger.AddLog("NetUtil.Stop", ex.Source, ex.Message, ex.StackTrace);
                return ex.Message;
            }
        }

        public static string TestConnection()
        {
            if (!string.IsNullOrEmpty(ConnectedIP))
            {
                if (ConnectedIP != "localhost")
                {
                    try
                    {
                        using (TcpClient client = new TcpClient(ConnectedIP, Port))
                        {
                            return "Test Result: Successful";
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.AddLog("NetUtil.TestConnection", ex.Source, ex.Message, ex.StackTrace);
                        return ex.Message;
                    }
                }
                else
                {
                    return "Test Result: Successful";
                }
            }
            else
            {
                return "Test Result: Invalid";
            }
        }

        public static string SendMessage(string message)
        {
            try
            {
                using (TcpClient client = new TcpClient(ConnectedIP, Port))
                {
                    using (NetworkStream stream = client.GetStream())
                    {
                        byte[] Message = Encoder.GetBytes(message);
                        stream.Write(Message, 0, Message.Length);
                        stream.Flush();
                    }
                }

                Message_Sent = message;
                return "sent";
            }
            catch (Exception ex)
            {
                Logger.AddLog("NetUtil.SendMessage", ex.Source, ex.Message, ex.StackTrace);
                return ex.Message;
            }            
        }

        #endregion
    }
}
