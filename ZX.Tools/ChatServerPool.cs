using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZX.Tools
{
    public class ChatServerPool
    {
        private static Dictionary<WebSocket, string> userconnections = new Dictionary<WebSocket, string>();


        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static String GetUserByKey(WebSocket conn)
        {
            return userconnections[conn];
        }

        /// <summary>
        /// 获取WebSocket连接
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static WebSocket GetWebSocket(string user)
        {
            return userconnections.FirstOrDefault(t => t.Value == user).Key;
        }


        /// <summary>
        /// 将用户移除连接池
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static bool ReMoveUser(WebSocket conn)
        {
            if (userconnections.ContainsKey(conn))
            {
                userconnections.Remove(conn);
                return true;
            } else
            {
                return false;
            }
        }

        /// <summary>
        /// 向连接池中添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="conn"></param>
        public static void AddUser(string user, WebSocket conn)
        {
            lock(userconnections.Keys)
            {
                foreach (WebSocket conn1 in userconnections.Keys)
                {
                    string cuser = userconnections[conn1];
                    if (cuser.Equals(user))
                    {
                        ReMoveUser(conn1);
                    }
                }
                userconnections.Add(conn, user);
            }
        }

        /// <summary>
        /// 向指定用户发送消息
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="message"></param>
        public static void SendMessageToUser(WebSocket conn, String message)
        {
            if (null != conn && null != userconnections[conn])
            {
                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[2048]);
                buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                conn.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        /// <summary>
        /// 向所有用户发送消息
        /// </summary>
        /// <param name="message"></param>
        public static void SendMessage(string message)
        {
            ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[2048]);
            buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
            foreach (WebSocket conn in userconnections.Keys)
            {
                string user = userconnections[conn];
                if (user.IsNotNullOrEmpty())
                {
                    conn.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);

                }
            }
        }

    }
}
