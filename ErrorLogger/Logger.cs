using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErrorLogger
{
    public static class Logger
    {
        private static StreamWriter LogWriter;
        private static object objLock_LogWriter = new object();
        public static void Log(string Message)
        {
            string LogFile = Application.ExecutablePath.Replace(Application.ExecutablePath.Split('\\')[Application.ExecutablePath.Split('\\').Length - 1], "Logs.txt");
            try
            {
                lock (objLock_LogWriter)
                {
                    LogWriter = new StreamWriter(LogFile, true);
                    LogWriter.WriteLine("[" + DateTime.Now.ToString() + "] " + Message);
                    LogWriter.Close();
                }
            }
            catch (Exception ex)
            {
                LogWriter = new StreamWriter(LogFile, true);
                LogWriter.WriteLine("[" + DateTime.Now.ToString() + "] " + "Error in Logging :" + ex.Message);
                LogWriter.Close();
            }
        }
    }
}
