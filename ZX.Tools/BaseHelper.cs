using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace ZX.Tools
{
    public class BaseHelper
    {
        #region POST请求
        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="param">参数</param>
        /// <param name="auth">授权</param>
        /// <param name="cert">证书</param>
        /// <returns></returns>
        public static string HttpPost(string url, string param, string contentType = "application/json", string auth = null, X509Certificate2 cert = null)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            if (cert != null)
            {
                httpWebRequest.ClientCertificates.Add(cert);
            }
            httpWebRequest.ContentType = contentType;
            if (!string.IsNullOrEmpty(auth))
            {
                httpWebRequest.Headers.Add("Authorization", auth);
            }
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 30000;
            if (!string.IsNullOrEmpty(param))
            {
                byte[] btBodys = Encoding.UTF8.GetBytes(param);
                httpWebRequest.ContentLength = btBodys.Length;
                httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);
            }
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();
            httpWebResponse.Close();
            streamReader.Close();
            httpWebRequest.Abort();
            httpWebResponse.Close();
            return responseContent;
        }
        #endregion

        #region GET请求
        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="param">参数</param>
        /// <param name="auth">授权</param>
        /// <returns></returns>
        public static string HttpGet(string url, string param, string auth)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + (param.IsNullOrEmpty() ? "" : "?" + param));
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            if (!string.IsNullOrEmpty(auth))
            {
                request.Headers.Add("Authorization", auth);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }
        #endregion       

        #region 拷贝文件夹
        /// <summary>
        /// 拷贝文件夹
        /// </summary>
        /// <param name="sourceDirectory">原文件夹路径</param>
        /// <param name="destDirectory">目标文件夹路径</param>
        public static void CopyDirectory(string sourceDirectory, string destDirectory)
        {
            try
            {
                //判断源目录和目标目录是否存在，如果不存在，则创建一个目录
                if (!Directory.Exists(sourceDirectory))
                {
                    Directory.CreateDirectory(sourceDirectory);
                }
                if (!Directory.Exists(destDirectory))
                {
                    Directory.CreateDirectory(destDirectory);
                }
                //拷贝文件
                CopyFile(sourceDirectory, destDirectory);
                //拷贝子目录
                //获取所有子目录名称
                string[] directionName = Directory.GetDirectories(sourceDirectory);
                foreach (string directionPath in directionName)
                {
                    //根据每个子目录名称生成对应的目标子目录名称
                    string directionPathTemp = destDirectory + "\\" + directionPath.Substring(sourceDirectory.Length + 1);
                    //递归下去
                    CopyDirectory(directionPath, directionPathTemp);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 拷贝文件
        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="sourceDirectory">源文件路径</param>
        /// <param name="destDirectory">目标文件路径</param>
        public static void CopyFile(string sourceDirectory, string destDirectory)
        {
            try
            {
                //获取所有文件名称
                string[] fileNames = Directory.GetFiles(sourceDirectory);
                foreach (string filePath in fileNames)
                {
                    string fileName = filePath.Substring(sourceDirectory.Length + 1);
                    string filePathTemp = destDirectory + "\\" + fileName;
                    //若不存在，直接复制文件；若存在，覆盖复制
                    if (System.IO.File.Exists(filePathTemp))
                    {
                        System.IO.File.Copy(filePath, filePathTemp, true);
                    }
                    else
                    {
                        System.IO.File.Copy(filePath, filePathTemp);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 直接删除目录下的所有文件及文件夹(保留目录)
        /// 〈 summary〉 
        /// 直接删除目录下的所有文件及文件夹(保留目录)  
        /// 〈 /summary〉 
        /// 〈 param name="strDir"〉目录地址 
        ///〈 /param〉 
        public static void DeleteFiles(string strDir)
        {
            try
            {
                if (Directory.Exists(strDir))
                {
                    string[] strDirs = Directory.GetDirectories(strDir);
                    string[] strFiles = Directory.GetFiles(strDir);
                    foreach (string strFile in strFiles)
                    {
                        string name = Path.GetFileName(strFile);
                        if (!name.Contains(DateTime.Now.ToString("yyyyMMdd")))
                        {
                            System.IO.File.Delete(strFile);
                        }
                    }
                    foreach (string strdir in strDirs)
                    {
                        Directory.Delete(strdir, true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取ip地址
        /// <summary>
        /// 获取ip地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {
            string userIP;
            // HttpRequest Request = HttpContext.Current.Request; 
            HttpRequest Request = HttpContext.Current.Request; // ForumContext.Current.Context.Request; 
            // 如果使用代理，获取真实IP 
            if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
                userIP = Request.ServerVariables["REMOTE_ADDR"];
            else
                userIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (userIP == null || userIP == "")
                userIP = Request.UserHostAddress;
            return userIP;
        }
        #endregion

        #region 返回状态名称
        /// <summary>
        /// 返回状态名称
        /// </summary>
        /// <param name="?">id</param>
        /// <returns></returns>
        public static string GetAuditStatus(int auditStatus)
        {
            string name = "";
            switch (auditStatus)
            {
                case 20:
                    name = "审核中";
                    break;
                case 30:
                    name = "实施部审核通过";
                    break;
                case 40:
                    name = "返回录入人(实施部)";
                    break;
                case 50:
                    name = "设备部审核通过";
                    break;
                case 60:
                    name = "返回实施部(设备部)";
                    break;
                case 70:
                    name = "返回录入人(设备部)";
                    break;
                case 80:
                    name = "返回实施部审核(监管部)";
                    break;
                case 90:
                    name = "返回设备部审核(监管部)";
                    break;
                case 100:
                    name = "返回录入人(监管部)";
                    break;
                case 0:
                    name = "审核通过";
                    break;
                default:
                    name = "用户修改";
                    break;
            }
            return name;
        }
        #endregion
    }
}
