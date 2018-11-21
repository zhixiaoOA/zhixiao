using System;
using System.Security.Cryptography;
using System.Web.Security;
using System.Text;
using System.IO;

namespace ZX.Tools
{
    /// <summary>
    /// EnDecrypt
    /// </summary>
    public class EnDecrypt
    {
        /// <summary>
        /// SH1加密小写副本返回（不可逆）
        /// </summary>
        /// <param name="str_sha1_in"></param>
        /// <returns></returns>
        public static string SHA1Hash(string str_sha1_in)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = UTF8Encoding.Default.GetBytes(str_sha1_in);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            return str_sha1_out.Replace("-", "").ToLower();
        }

        /// <summary>
        /// SH1加密小写副本返回(UTF-8编码,不可逆)
        /// </summary>
        /// <param name="str_sha1_in"></param>
        /// <returns></returns>
        public static string EncryptToSHA1(string str_sha1_in)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = Encoding.UTF8.GetBytes(str_sha1_in);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            return str_sha1_out.Replace("-", "").ToLower();
        }

        #region===========MD5加密(不可逆)

        /// <summary>
        /// MD5加密(不可逆)
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <returns>返回加密字符</returns>
        public static string MD5(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] inputText = Encoding.Default.GetBytes(text);
            byte[] buffer = md5.ComputeHash(inputText);
            md5.Clear();
            string str = string.Empty;
            for (int i = 0; i < buffer.Length; i++)
            {
                str += buffer[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }
        #endregion


        /// <summary>
        /// 数字换掉
        /// </summary>
        /// <param name="strpwd">pwd</param>
        /// <returns>pwd</returns>
        public static string ReplaceString(string strpwd)
        {
            string strpwd1 = strpwd.ToString();
            strpwd1 = strpwd1.Replace('0', ')');
            strpwd1 = strpwd1.Replace('1', '!');
            strpwd1 = strpwd1.Replace('2', '@');
            strpwd1 = strpwd1.Replace('3', '#');
            strpwd1 = strpwd1.Replace('4', '$');
            strpwd1 = strpwd1.Replace('5', '%');
            strpwd1 = strpwd1.Replace('6', '^');
            strpwd1 = strpwd1.Replace('7', '&');
            strpwd1 = strpwd1.Replace('8', '*');
            strpwd1 = strpwd1.Replace('9', '(');
            return strpwd1.ToString();
        }


        #region ==========加密(可逆)==========

        /// <summary>
        /// 加密(可逆)
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>加密(可逆)</returns>
        public static string Encrypt(string text)
        {
            return Encrypt(text, "ghiegcge");
        }
        /// <summary>
        /// 加密(可逆)
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="sKey">key</param>
        /// <returns>加密(可逆)</returns>
        public static string Encrypt(string text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inpuText = Encoding.Default.GetBytes(text);

            des.IV = Encoding.Default.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.Key = Encoding.Default.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inpuText, 0, inpuText.Length);
            cs.FlushFinalBlock();
            StringBuilder sb = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }

        #endregion


        #region ============解密(可逆)=========
        /// <summary>
        /// 解密(可逆)
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>解密(可逆)</returns>
        public static string Decrypt(string text)
        {
            return Decrypt(text, "ghiegcge");
        }
        /// <summary>
        /// 解密(可逆)
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="sKey">key</param>
        /// <returns>解密(可逆)</returns>
        public static string Decrypt(string text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputText = new byte[text.Length / 2];
            int i, x;
            for (x = 0; x < text.Length / 2; x++)
            {
                i = Convert.ToInt32(text.Substring(x * 2, 2), 16);
                inputText[x] = (byte)i;
            }
            des.IV = Encoding.Default.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.Key = Encoding.Default.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));

            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(inputText, 0, inputText.Length);
            cs.FlushFinalBlock();

            return Encoding.Default.GetString(ms.ToArray());

        }
        #endregion

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string URLEncryptDES(string encryptString)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes("@)!$)&)^");
                byte[] rgbIV = Encoding.UTF8.GetBytes("Joyosoft");
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
                //return Base24.Default.GetString(mStream.ToArray());
                //return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="decryptString"></param>
        /// <returns></returns>
        public static string URLDecryptDES(string decryptString)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes("@)!$)&)^");
                byte[] rgbIV = Encoding.UTF8.GetBytes("Joyosoft");
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                //byte[] inputByteArray = Encoding.UTF8.GetBytes(decryptString);
                //byte[] inputByteArray = Base24.Default.GetBytes(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
    }
}
