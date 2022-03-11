using System;
using System.IO;
using System.Threading;
using UnityEngine;

namespace Common_Venues
{
    public static class LogWriteRead
    {
        private static StreamWriter _writer;
        private static readonly string ProjectPath = Application.persistentDataPath;
        private static readonly string FileName = Application.productName + ".log";
        private static readonly string GeneralLogPath = Path.Combine(ProjectPath, FileName);

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="dataPath">日志路径</param>
        /// <param name="log">日志内容</param>
        public static void WriteLog(string dataPath, string log)
        {
            FileStream fs;
            StreamWriter sw;
            if (File.Exists(dataPath))
                //验证文件是否存在，有则追加，无则创建
            {
                fs = new FileStream(dataPath, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(dataPath, FileMode.Create, FileAccess.Write);
            }

            sw = new StreamWriter(fs);
            var time = DateTime.Now.ToString("u");
            sw.WriteLine(log + "__记录时间：" + time);
            sw.Close();
            fs.Close();
        }

        public static void WriteLog(string log)
        {
            FileStream fs;
            StreamWriter sw;
            if (File.Exists(GeneralLogPath))
                //验证文件是否存在，有则追加，无则创建
            {
                fs = new FileStream(GeneralLogPath, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(GeneralLogPath, FileMode.Create, FileAccess.Write);
            }

            sw = new StreamWriter(fs);
            var time = DateTime.Now.ToString("u");
            sw.WriteLine(log + "__记录时间：" + time);
            // sw.WriteLine(log);
            sw.Close();
            fs.Close();
        }

        public static void ClearLogs(string dataPath)
        {
            var fileInfo = new FileInfo(dataPath);
            fileInfo.Delete();
        }

        public static void ClearGeneralLog()
        {
            if (File.Exists(GeneralLogPath))
            {
                File.Delete(GeneralLogPath);
            }
        }

        public static void ReadLog(string path)
        {
            var stream = File.Open(path, FileMode.Open);
        }
    }
}