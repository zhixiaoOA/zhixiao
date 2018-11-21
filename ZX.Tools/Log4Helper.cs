using System;
using System.Text;
using System.IO;

namespace ZX.Tools
{
    public class Log4Helper
    {
        private static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");

        private static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");

        private static readonly log4net.ILog fatal = log4net.LogManager.GetLogger("fatal");

        private static readonly log4net.ILog warn = log4net.LogManager.GetLogger("warn");

        private static readonly log4net.ILog debug = log4net.LogManager.GetLogger("debug");

        public static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void SetConfig(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        /// <summary>
        /// 写入致命日志
        /// </summary>
        /// <param name="info"></param>
        //public static void WriteFatal(string info)
        //{
        //    if (fatal.IsFatalEnabled)
        //    {
        //        fatal.Info(info);
        //    }
        //}

        /// <summary>
        /// 写入调试信息
        /// </summary>
        /// <param name="info"></param>
        public static void WriteDebug(string info)
        {
            //WriteTxt(info);
            //if (debug.IsDebugEnabled)
            //{
            debug.Info(info);
            //}
        }

        /// <summary>
        /// 写入消息
        /// </summary>
        /// <param name="info"></param>
        public static void WriteInfo(string info)
        {
            //WriteTxt(info);
            //if (loginfo.IsInfoEnabled)
            //{
            loginfo.Info(info);
            //}
        }

        /// <summary>
        /// 写入异常
        /// </summary>
        /// <param name="info"></param>
        /// <param name="se"></param>
        public static void WriteError(string info, Exception ex)
        {
            //WriteTxt(info);
            //#if(DEBUG)
            //            throw ex;
            //#else
            logerror.Error(info, ex);
            //if(logerror.IsErrorEnabled)
            //{
            //    logerror.Error(info,ex);
            //}
            //#endif
        }

        public static void WriteTxt(string err)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/log");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
            FileStream fs = new FileStream(path + "\\" + fileName, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.Write("\r\n" + err);
            sw.Close();
            fs.Close();
        }
    }
}
